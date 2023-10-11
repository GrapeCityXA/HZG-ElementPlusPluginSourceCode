/// <reference path = "Base.ts" />

namespace ElementCellTypes {


    interface IDatePickerCellTypeParam {
        ReadOnly: boolean;
        IsDisabled: boolean;
        editable: boolean;
        clearable: boolean;
        placeholder: string;
        type: string;
        format: string;
        prefixIcon: Icon;
        firstDayOfWeek: number;
        rangeSeparator: string;
        startPlaceholder: string;
        endPlaceholder: string;
        DefaultValue: string;
        DefautWeekFormat: string;
    }

    export class DatePickerCellType extends InputCellTypeBase<IDatePickerCellTypeParam> {
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {

            const separaterStr = this.getCustomSlotByPath(SlotPath.datePickerRangeSeparater);
            const cellStr = this.getCustomSlotByPath(SlotPath.datePickerCell);

            const self = this;
            this.addCustomClass("el-date-picker-custom");
            const cellType = this.cellType;

            const rangeTemplate = `
:is-range="true"
:start-placeholder="startPlaceholder"
:end-placeholder="endPlaceholder"
`;

            const isRange = cellType.type.indexOf("range") !== -1;

            const { CssClassName } = this.CellElement;
            const popperClass = CssClassName ? `${CssClassName}-popper` : "";

            const template = `
<el-date-picker v-model="input"
:type="type"
:clearable="clearable"
:disabled="disabled"
:editable="editable"
:placeholder="placeholder"
:readonly="readOnly"
:format="format"
:prefix-icon="prefixIcon"
popper-class="${popperClass}"
:picker-options="pickerOptions"
${cellType.type.indexOf("range") !== -1 ? rangeTemplate : ""}
@change="handleChange">
    <template #range-separator v-if="type.indexOf('range') !== -1">
        ${separaterStr || cellType.rangeSeparator}
    </template>
    <template #default="cell">
        ${cellStr}
    </template>
</el-date-picker>
`;
            const option = {
                template: template,
                data() {
                    return {
                        input: null,
                        type: cellType.type,
                        clearable: this.boolConvertor(cellType.clearable),
                        disabled: cellType.IsDisabled,
                        editable: this.boolConvertor(cellType.editable),
                        format: this.getformat(),
                        placeholder: cellType.placeholder,
                        prefixIcon: "",
                        readOnly: undefined,
                        rangeSeparator: cellType.rangeSeparator,
                        startPlaceholder: cellType.startPlaceholder,
                        endPlaceholder: cellType.endPlaceholder,
                        pickerOptions: this.getPickerOptions()
                    };
                },
                methods: {
                    handleChange() {
                        self.commitValue();
                        self.validate();
                    },
                    getValue() {
                        if (!this.input) {
                            return null;
                        }
                        if (this.input.length) {
                            return this.input.map(Forguncy.ConvertDateToOADate).join(",");
                        }
                        return Forguncy.ConvertDateToOADate(this.input);
                    },
                    setValue(value) {
                        const covertValueToInput = () => {
                            if (!value) {
                                return null;
                            }
                            if (typeof value === "number") {
                                return Forguncy.ConvertOADateToDate(value);
                            }
                            if (typeof value === "string") {
                                if (isRange) {
                                    const callbackfn = (item) => DateUtil.ConvertToDate(item);
                                    return value.split(",").map(callbackfn);
                                }

                                const numerValue = Number(value);
                                return isNaN(numerValue) ?
                                    new Date(value) :
                                    Forguncy.ConvertOADateToDate(numerValue);
                            }

                            return value;
                        };

                        this.input = covertValueToInput();
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
                    getPickerOptions() {
                        return {
                            firstDayOfWeek: cellType.firstDayOfWeek + 0
                        };
                    },
                    getformat() {
                        let format;

                        if (!cellType?.format?.trim()?.length && cellType.type === "week") {
                            format = cellType.DefautWeekFormat;
                        } else {
                            format = cellType.format;
                        }

                        return format?.replace("WW", "ww")
                            .replace("yyyy", "YYYY")
                            .replace("dd", "DD");
                    },
                    isInvalidDate(value: unknown) {
                        if (!(value instanceof Date)) {
                            return true;
                        }
                        return isNaN(value.getTime());
                    }
                },
                created() {
                    ElementUtils.Dayjs.toggleLocale({ weekStart: cellType.firstDayOfWeek });
                },
                mounted() {
                    self.fontDom = $('input', self.vueContainer)
                        .add(".el-date-editor", self.vueContainer);
                },
                watch: {
                    input(value: Date | Date[]) {
                        const { isInvalidDate } = this;

                        const getLegalValue = () => {

                            if (!value) {
                                return null;
                            }

                            if (!isRange) {
                                return isInvalidDate(value) ? null : value;
                            }

                            if (!(value instanceof Array)) {
                                return [];
                            }

                            if (isInvalidDate(value[0]) || isInvalidDate(value[1])) {
                                return [];
                            }

                            return value;

                        };

                        const legalValue = getLegalValue();

                        if (legalValue !== value) {
                            this.input = legalValue;
                        }
                    }
                }
            };

            this.createVueApp(option);

            super.onPageLoaded(info);
            super.getIconComponent(cellType.prefixIcon, icon => this.vue.prefixIcon = icon);
        }

        public GetSelectedRange() {
            const { input: value } = this.vue;

            const [startDate, endDate]: Date[] = value || [];

            return {
                StartValue: startDate,
                EndValue: endDate,
            };
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.DatePickerCellType, ElementUI", ElementCellTypes.DatePickerCellType);