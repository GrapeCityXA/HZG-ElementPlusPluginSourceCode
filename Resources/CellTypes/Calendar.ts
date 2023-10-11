/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    interface IOption {
        value: number | string;
        date: Date;
        text: string;
    }

    interface ICalendarCellTypeParam {
        firstDayOfWeek: number;
        bindingOptions: any;
        DBClickCommand: Forguncy.Plugin.ICustomCommandObject;
        ScheduleClickCommand: Forguncy.Plugin.ICustomCommandObject;
        ScheduleDBClickCommand: Forguncy.Plugin.ICustomCommandObject;
        options: IOption[];
    }

    interface IDateCell {
        type: string;
        isSelected: boolean;
        day: string;
        date: Date;
    }

    export class CalendarCellType extends ElementCellTypeBase<ICalendarCellTypeParam> {

        private _timer;

        private cacheOldValue;

        private clearTimer() {
            clearTimeout(this._timer);
        }

        private resetTimer(fn: Function) {
            clearTimeout(this._timer);
            this._timer = setTimeout(fn, 300);
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const dateCellStr = this.getCustomSlotByPath(SlotPath.calendarCell);
            const calendarHeaderStr = this.getCustomSlotByPath(SlotPath.calendarHeader);
            const self = this;
            const cellType = this.cellType;
            this.addCustomClass("el-calendar-custom");
            const template = `
            <el-calendar v-model="value">
                <template #dateCell="{ data }">
                     ${dateCellStr || self.getDefaultDateCellSlot()}
                </template>
                ${calendarHeaderStr ? "<template #header='data'>" + calendarHeaderStr + "</template>" : ""}
            </el-calendar>
`;
            const option = {
                template,
                data() {
                    return {
                        value: null,
                        options: [],
                        scheduleMap: {},
                        insufficientHeight: false,
                        firstDayOfWeek: cellType.firstDayOfWeek,
                        calendarContainerStyle: null,
                    };
                },
                mounted() {
                    const height = Math.floor($(".el-calendar-day", this.$el).height());
                    if (height <= 30) {
                        this.insufficientHeight = true;
                    }
                    this.calendarContainerStyle = `height:${height}px;`;
                },
                created() {
                    ElementUtils.Dayjs.toggleLocale({ weekStart: cellType.firstDayOfWeek });
                },
                methods: {
                    getValue() {
                        return Forguncy.ConvertDateToOADate(this.value);
                    },
                    setValue(value) {
                        self.cacheOldValue = value ? Forguncy.ConvertOADateToDate(value) : null;
                        this.value = self.cacheOldValue;
                    },
                    formatDay(value: string) {
                        return value.split('-')[2];
                    },
                    getScheduleMapKeyByDate(date: Date) {
                        return date?.toLocaleDateString();
                    },
                    clearScheduleMap() {
                        this.scheduleMap = {};
                    },
                    fillScheduleMap() {
                        this.options.forEach((option): any => {
                            const isOADate = !isNaN(Number(option.date));

                            const date: Date = isOADate ?
                                Forguncy.ConvertOADateToDate(option.date) :
                                new Date(option.date);

                            const key = this.getScheduleMapKeyByDate(date);

                            if (this.scheduleMap[key]) {
                                this.scheduleMap[key].push(option);
                            } else {
                                this.scheduleMap[key] = [option];
                            }

                        });
                    },
                    getOptionsByDay(dateCell: IDateCell) {
                        const key = this.getScheduleMapKeyByDate(dateCell.date);
                        return (this.scheduleMap[key] || []);
                    },
                    handleDoubleClick(data: IDateCell) {
                        const command = cellType.DBClickCommand;

                        if (!command) {
                            return;
                        }

                        self.executeCustomCommandObject(command, {
                            [command.ParamProperties["date"]]: data.date
                        }, "doubleClick");
                    },

                    scheduleOnClickOrDBClick(type: "click" | "dbclick", option: IOption) {
                        const command = type === "click" ? cellType.ScheduleClickCommand : cellType.ScheduleDBClickCommand;

                        if (!command) {
                            return;
                        }

                        const { ParamProperties } = command;
                        const initParam = {
                            [ParamProperties["value"]]: option.value,
                            [ParamProperties["text"]]: option.text,
                            [ParamProperties["date"]]: option.date,
                        };
                        self.executeCustomCommandObject(command, initParam, "schedule" + type);
                    },
                    scheduleOnClick(option: IOption) {
                        self.resetTimer(() => this.scheduleOnClickOrDBClick("click", option));
                    },
                    scheduleOnDoubleClick(event, option: IOption) {
                        self.clearTimer();
                        if (cellType.DBClickCommand) {
                            event.stopPropagation();
                        }
                        this.scheduleOnClickOrDBClick("dbclick", option);
                    }
                },
                watch: {
                    value(newValue, oldValue) {
                        if (self.cacheOldValue?.getTime() !== newValue?.getTime()) {
                            self.commitValue();
                        }
                    },
                    options(newValue, oldValue) {
                        this.clearScheduleMap();
                        this.fillScheduleMap();
                    }
                }
            };

            this.createVueApp(option);

            SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => {
                this.vue.options = dataSource;
            });

            super.onPageLoaded(info);
        }
        public reload() {
            const cellType = this.cellType;
            SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => {
                this.vue.options = dataSource;
            });
        }
        private getDefaultDateCellSlot() {
            return `
            <div class="calendar-container" :style="calendarContainerStyle" v-if="calendarContainerStyle" @dblclick="handleDoubleClick(data)">
                <div class="calendar-date-container">
                    <div>{{ formatDay(data.day)}}</div>
                    <div>
                        <div class="fgc-calendar-ellipsis" v-if="insufficientHeight && getOptionsByDay(data).length"/>
                    </div>
                </div>
                <div class="calendar-schedule-container" v-if="!insufficientHeight">
                    <el-scrollbar>
                        <div class="calendar-schedule-item" v-for="option in getOptionsByDay(data)" 
                            @click="scheduleOnClick(option)" @dblclick="scheduleOnDoubleClick($event,option)">
                            <div :title="option.text" class="calendar-schedule-item-text">{{option.text}}</div>
                        </div>
                    </el-scrollbar>
                </div>
            </div>`;
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.CalendarCellType, ElementUI", ElementCellTypes.CalendarCellType);