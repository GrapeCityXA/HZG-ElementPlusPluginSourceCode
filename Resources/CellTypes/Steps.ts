/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    interface IStepsCellTypeOptions {
        value: any;
        title: string;
        description: string;
        icon: Icon;
        iconComponent: any;
    }

    enum StepsStatus {
        wait,
        process,
        finish,
        error,
        success,
    }

    interface IStepsCellTypeParam {
        options: IStepsCellTypeOptions[];
        layout: "horizontal" | "vertical";
        simple: boolean;
        alignCenter: boolean;
        processStatus: StepsStatus;
        finishStatus: StepsStatus;
        useBinding: boolean;
        bindingOptions: any;
    }

    export class StepsCellType extends ElementCellTypeBase<IStepsCellTypeParam> {
        public setDataSource(dataSource) {
            return this.vue.setOptions(dataSource);
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;
            const self = this;
            const containerId = `${this.uId}-el-steps`;
            this.addCustomClass("el-steps-custom");

            const option = {
                el: "#" + this.uId,
                template: `<div id="${containerId}" style="width: 100%;height: 100%;">
                                 <el-steps 
                                     :active="active" 
                                     :direction="direction" 
                                     :simple="simple" 
                                     :align-center="alignCenter" 
                                     :process-status="processStatus"
                                     :finish-status="finishStatus"
                                 > 
                                    <el-step 
                                        v-for="item in options" 
                                        :key="item.title" 
                                        :title="item.title" 
                                        :icon="item.iconComponent" 
                                        :description="item.description" 
                                    />
                                 </el-steps>
                            </div>
                          `,
                data() {
                    return {
                        active: -1,
                        value: null,
                        options: cellType.useBinding ? [] : cellType.options,
                        simple: cellType.simple && cellType.layout === "horizontal",
                        alignCenter: cellType.alignCenter,
                        direction: cellType.layout,
                        processStatus: StepsStatus[cellType.processStatus],
                        finishStatus: StepsStatus[cellType.finishStatus],
                    };
                },
                methods: {
                    getValue() {
                        return this.value;
                    },
                    setValue(value) {
                        this.value = value;
                        this.updateActive();
                    },
                    setOptions(options) {
                        this.options = options;
                        this.updateActive();
                        self.recalcIcon();
                    },
                    updateActive() {

                        if (this.options?.length) {
                            for (let i = 0; i < this.options.length; i++) {
                                const option = this.options[i];
                                // 这里认为字符串数字与数字等价，双等号不能去掉
                                // eslint-disable-next-line
                                if (option.value == this.value) {
                                    this.active = i;
                                    return;
                                }
                            }
                        }
                        if (this.value || this.value === 0) {
                            this.active = this.options?.length;
                        }
                        else {
                            this.active = -1;
                        }

                    }
                }
            };

            this.createVueApp(option);
            self.recalcIcon();
            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource));
            }
        }
        recalcIcon() {
            for (let i = 0; i < this.vue.options.length; i++) {
                const option = <IStepsCellTypeOptions>this.vue.options[i];
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
        UpdateProcessState(state: StepsStatus) {
            this.vue.processStatus = StepsStatus[state];
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.StepsCellType, ElementUI", ElementCellTypes.StepsCellType);