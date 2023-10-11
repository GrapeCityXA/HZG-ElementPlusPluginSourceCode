/// <reference path = "Base.ts" />

namespace ElementCellTypes {

    enum TransferTargetOrder {
        original,
        push,
        unshift,
    }

    interface ITransferCellTypeParam {
        filterable: boolean;
        filterPlaceholder: string;
        leftTitle: string;
        rightTitle: string;
        useBinding: boolean;
        bindingOptions: any;
        targetOrder: TransferTargetOrder;
        options: { content: string; timestamp: string }[];
    }

    export class TransferCellType extends InputCellTypeBase<ITransferCellTypeParam> {
        public setDataSource(dataSource) {
            return this.vue.setOptions(dataSource);
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;

            const containerId = `${this.uId}-el-transfer`;

            const self = this;

            const formatData = (data = []) => data.map(({ key, label, disabled = false }) => ({
                label: label?.toString(),
                key: key?.toString(),
                disabled: !!disabled
            }));

            const option = {
                template: `<div id="${containerId}" style="width: 100%;height: 100%;overflow: auto">
                                <el-transfer
                                     v-model="value"
                                     :titles="titles"
                                     :data="data"
                                     :filterable="filterable"
                                     :filter-placeholder="filterPlaceholder"
                                     :target-order="targetOrder"
                                     @change="handleChange"
                                     :render-content="renderContent"
                                 />
                            </div>
                          `,
                data() {
                    return {
                        value: [],
                        titles: [cellType.leftTitle, cellType.rightTitle],
                        filterable: !!cellType.filterable,
                        filterPlaceholder: cellType.filterPlaceholder,
                        data: cellType.useBinding ? [] : formatData(cellType.options),
                        targetOrder: TransferTargetOrder[cellType.targetOrder]
                    };
                },
                methods: {
                    getValue() {
                        if (this.value instanceof Array) {
                            return this.value.join(",");
                        }
                        return this.value;
                    },
                    setValue(value) {
                        let list = [];
                        if (value instanceof Array) {
                            list = value;
                        }
                        else {
                            list = typeof value === "string" ? value.split(",") : [value];
                        }

                        this.value = list.filter(item => this.data.find(({ key }) => key === item));
                    },
                    setOptions(options) {
                        this.data = formatData(options);
                    },
                    handleChange() {
                        this.value = this.value?.filter(val => this.data.find(({ key }) => key === val));
                        self.commitValue();
                    },
                    renderContent(h, option) {
                        return self.customRender(h, option);
                    }
                }
            };

            this.createVueApp(option);

            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource));
            }

            super.onPageLoaded(info);
        }
        public ReloadBindingItems() {
            const cellType = <ITransferCellTypeParam>this.CellElement.CellType;
            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource));
            }
        }
        public reload() {
            this.ReloadBindingItems();
        }
        public customRender(h, option) {
            return h("span", option.label);
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.TransferCellType, ElementUI", ElementCellTypes.TransferCellType);
