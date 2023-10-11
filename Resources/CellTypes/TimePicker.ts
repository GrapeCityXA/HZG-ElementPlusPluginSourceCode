/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    interface ITimePickerCellTypeParam {
        placeholder: string;
        prefixIcon: Icon;
        ReadOnly: boolean;
        IsDisabled: boolean;
        editable: boolean;
        clearable: boolean;
        StartTime: number;
        EndTime: number;
        step: number;
        mode: string;
        isRange: boolean;
        rangeSeparator: string;
        startPlaceholder: string;
        endPlaceholder: string;
    }

    export class TimePickerCellType extends InputCellTypeBase<ITimePickerCellTypeParam> {

        public resolveTimeNumber(value: number) {
            return value < 10 ? `0${value}` : value;
        }

        public parseTime(value = 0, needSeconds = true) {
            const date = Forguncy.ConvertOADateToDate(value);
            const hours = date.getHours();
            const minutes = date.getMinutes();
            const seconds = date.getSeconds();

            let time = `${this.resolveTimeNumber(hours)}:${this.resolveTimeNumber(minutes)}`;

            if (needSeconds) {
                time += ":" + this.resolveTimeNumber(seconds);
            }
            return { hours, minutes, seconds, time };
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const self = this;
            const cellType = this.cellType;

            const { CssClassName } = this.CellElement;
            const popperClass = CssClassName ? `${CssClassName}-popper` : "";

            const rangeTemplate = `
:is-range="true"
:range-separator="rangeSeparator"
:start-placeholder="startPlaceholder"
:end-placeholder="endPlaceholder"
`;

            const pickerTemplate = `
<el-time-picker v-model="input"
:clearable="clearable"
:disabled="disabled"
:editable="editable"
:placeholder="placeholder"
:prefix-icon="prefixIcon"
:format="format"
:disabled-hours="disabledHours"
:disabled-minutes="disabledMinutes"
:disabled-seconds="disabledSeconds"
:readonly="readOnly"
popper-class="${popperClass}"
${cellType.isRange ? rangeTemplate : ""}
@change="handleChange">
</el-time-picker>`;

            const selectTemplate = `
<el-time-select v-model="input"
:clearable="clearable"
:disabled="disabled"
:editable="editable"
:step="step"
:start="start"
:end="end"
:placeholder="placeholder"
:prefix-icon="prefixIcon"
:readonly="readOnly"
popper-class="${popperClass}"
@change="handleChange">
</el-time-select>`;

            const isSelect = cellType.mode === "select";

            const useTemplate = isSelect ? selectTemplate : pickerTemplate;

            this.addCustomClass(isSelect ? "el-time-select-custom" : "el-time-picker-custom");

            const {
                time: startTime,
                hours: startHour,
                minutes: startMinutes,
                seconds: startSeconds
            } = this.parseTime(cellType.StartTime);

            const {
                time: endTime,
                hours: endHour,
                minutes: endMinutes,
                seconds: endSeconds
            } = this.parseTime(cellType.EndTime);

            const option = {
                el: "#" + this.uId,
                template: useTemplate,
                data() {
                    return {
                        input: null,
                        clearable: this.boolConvertor(cellType.clearable),
                        disabled: cellType.IsDisabled,
                        editable: this.boolConvertor(cellType.editable),
                        readOnly: undefined,
                        placeholder: cellType.placeholder,
                        prefixIcon: "",
                        rangeSeparator: cellType.rangeSeparator,
                        startPlaceholder: cellType.startPlaceholder,
                        endPlaceholder: cellType.endPlaceholder,
                        step: self.parseTime(cellType.step, false).time,
                        format: "HH:mm:ss",
                        start: startTime,
                        end: endTime
                    };
                },
                methods: {
                    handleChange() {
                        self.commitValue();
                        self.validate();
                    },
                    makeRange(start: number, end: number) {
                        const result: number[] = [];
                        for (let i = start; i <= end; i++) {
                            result.push(i);
                        }
                        return result;
                    },
                    disabledHours() {
                        return this.makeRange(0, startHour - 1).concat(this.makeRange(endHour + 1, 23));
                    },
                    disabledMinutes(hour: number) {
                        if (hour === startHour) {
                            return this.makeRange(0, startMinutes - 1);
                        }
                        if (hour === endHour) {
                            return this.makeRange(endMinutes + 1, 59);
                        }
                    },
                    disabledSeconds(hour: number, minute: number) {
                        if (hour === startHour && minute === startMinutes) {
                            return this.makeRange(0, startSeconds - 1);
                        }
                        if (hour === endHour && minute === endMinutes) {
                            return this.makeRange(endSeconds + 1, 59);
                        }
                    },
                    convertDateToOADate(value: Date) {
                        const date = Forguncy.ConvertOADateToDate(0);

                        date.setHours(value.getHours());
                        date.setMinutes(value.getMinutes());
                        date.setSeconds(value.getSeconds());

                        return Forguncy.ConvertDateToOADate(date);
                    },
                    getValue() {
                        if (!this.input) {
                            return null;
                        }
                        if (cellType.isRange) {
                            return this.input.map(this.convertDateToOADate).join(",");
                        }
                        const inputIsDate = this.input instanceof Date;

                        return this.convertDateToOADate(inputIsDate ? this.input : new Date(`1970/1/1 ${this.input}`));
                    },
                    setValue(value) {
                        const convertValueToInput = () => {
                            if (!value) {
                                return null;
                            }
                            if (cellType.mode === "select") {
                                return self.parseTime(value, false).time;
                            }
                            if (typeof value === "number") {
                                return Forguncy.ConvertOADateToDate(value);
                            }
                            if (typeof value === "string") {
                                const convertTimeToDateOptions = { year: 1970, month: 0, day: 1 };

                                if (cellType.isRange) {
                                    return value.split(",").map((item) => DateUtil.ConverTimeToDate(item, convertTimeToDateOptions));
                                }

                                return DateUtil.ConverTimeToDate(value, convertTimeToDateOptions);
                            }
                            return value;
                        };

                        this.input = convertValueToInput();

                    },
                    disable() {
                        this.disabled = true;
                    },
                    enable() {
                        this.disabled = false;
                    },
                    setReadOnly(value) {
                        this.readOnly = value;
                    },
                    boolConvertor(value) {
                        return value ? true : false;
                    },
                },
                mounted() {
                    self.fontDom = $("input", `#${self.uId}`);
                    self.setFontToDom();
                }
            };

            this.createVueApp(option);

            super.onPageLoaded(info);

            super.getIconComponent(cellType.prefixIcon, icon => this.vue.prefixIcon = icon);

        }

        public GetSelectedRange() {
            const { input: value } = this.vue;

            const [startDaTe, endDate]: Date[] = value instanceof Array ? value : [];


            return {
                StartValue: Forguncy.ConvertDateToOADate(startDaTe) - Math.floor(Forguncy.ConvertDateToOADate(startDaTe)),
                EndValue: Forguncy.ConvertDateToOADate(endDate) - Math.floor(Forguncy.ConvertDateToOADate(endDate)),
            };
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.TimePickerCellType, ElementUI", ElementCellTypes.TimePickerCellType);