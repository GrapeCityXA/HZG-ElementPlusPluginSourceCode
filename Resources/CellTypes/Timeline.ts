/// <reference path = "Base.ts" />

namespace ElementCellTypes {

    enum TimelinePlacement {
        top,
        bottom
    }

    type TSort = "default" | "ascTimestamp" | "descTimestamp";

    interface ITimelineCellTypeParam {
        format?: string;
        useBinding: boolean;
        hideTimestamp: boolean;
        sort: TSort;
        placement: TimelinePlacement;
        bindingOptions: any;
        options: TimelineItem[];
        NodeSize: "normal" | "large";
    }

    interface TimelineItem {
        content: string;
        timestamp: string | number;
        icon: Icon;
        iconComponent: any;
        color: string;
    }

    function formatDate(date: Date, fmt: string) {
        let o = {
            "M+": date.getMonth() + 1,
            "d+": date.getDate(),
            "H+": date.getHours(),
            "m+": date.getMinutes(),
            "s+": date.getSeconds(),
            "q+": Math.floor((date.getMonth() + 3) / 3),
            "S": date.getMilliseconds()
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
        };
        for (const k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    }


    export class TimelineCellType extends ElementCellTypeBase<ITimelineCellTypeParam> {

        public setDataSource(dataSource) {
            return this.vue.setOptions(dataSource);
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;

            const self = this;
            const option = {
                el: "#" + this.uId,
                template: `<el-scrollbar>
                                <el-timeline>
                                    <el-timeline-item
                                         v-for="(activity, index) in activities"
                                         :key="index"
                                         :timestamp="activity.timestamp"
                                         :icon="activity.iconComponent"
                                         :color="activity.color"
                                         :size="nodeSize"
                                         :hide-timestamp="hideTimestamp"
                                         :placement="placement"
                                         >
                                      {{activity.content}}
                                    </el-timeline-item>
                                 </el-timeline>
                            </el-scrollbar>
                          `,
                data() {
                    return {
                        activities: [],
                        hideTimestamp: cellType.hideTimestamp,
                        placement: TimelinePlacement[cellType.placement],
                        nodeSize: cellType.NodeSize
                    };
                },
                methods: {
                    getValue() {
                        return this.active;
                    },
                    setValue(value) {
                        this.active = value;
                    },
                    sort(type: TSort, a, b) {
                        let value1 = a.timestamp;
                        if (typeof (a.timestamp) === "string") {
                            value1 = new Date(a.timestamp);
                        }
                        let value2 = b.timestamp;
                        if (typeof (b.timestamp) === "string") {
                            value2 = new Date(b.timestamp);
                        }

                        return type === "ascTimestamp" ? value1 - value2 : value2 - value1;
                    },
                    formatOption(option: TimelineItem) {
                        const { timestamp, color } = option;

                        const isNumber = typeof (timestamp) === "number" || !isNaN(Number(timestamp));

                        const getDate = (isOADate = true) => {

                            if (isOADate) {

                                const date = Forguncy.ConvertOADateToDate(<number>timestamp);

                                const isInvalidDate = isNaN(date.getTime());

                                return isInvalidDate ? getDate(false) : date;
                            }

                            const dateString = isNumber ? Number(timestamp) : (<string>timestamp)?.replace(/-/g, "/");

                            return new Date(dateString);
                        };

                        const date = getDate();

                        const newTimestamp = self.isValidDate(date)
                            ? formatDate(date, cellType.format || "yyyy/MM/dd")
                            : timestamp;

                        return {
                            ...option,
                            timestamp: newTimestamp,
                            color: Forguncy.ConvertToCssColor(color),
                        };
                    },
                    setOptions(options) {
                        const activities = options.map(this.formatOption);

                        this.activities = cellType.sort === "default" ? activities : activities.sort(this.sort.bind(this, cellType.sort));

                        self.recalcIcon();
                    },
                }
            };

            this.createVueApp(option);

            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource));
            } else {
                this.vue.setOptions(cellType.options);
            }
            self.recalcIcon();
        }
        recalcIcon() {
            for (let i = 0; i < this.vue.activities.length; i++) {
                const option = <TimelineItem>this.vue.activities[i];
                const icon = <Icon>option?.icon;
                this.getIconComponent(icon, icon => option.iconComponent = icon);
            }
        }
        public ReloadBindingItems() {
            const cellType = this.cellType;
            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource));
            }
        }
        public reload() {
            this.ReloadBindingItems();
        }
        public SetDataSource(dataSource: any[], contentProperty: string, timestempProperty: string) {
            if (!dataSource) {
                dataSource = [];
            }
            if (typeof dataSource === "string") {
                dataSource = JSON.parse(dataSource);
            }
            const objSource = dataSource.map(i => <any>{
                "content": i[contentProperty],
                "timestamp": i[timestempProperty],
            });
            this.setDataSource(objSource);
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.TimelineCellType, ElementUI", ElementCellTypes.TimelineCellType);