/// <reference path = "Base.ts" />
namespace ElementCellTypes {
    interface TabItem {
        Value: any;
        ValueStr: string;
        Name: string;
    }
    interface ITabHeaderCellTypeParam {
        ClickCommand: Forguncy.Plugin.ICustomCommandObject;
        type: "default" | "card" | "border-card";
        position: "top" | "bottom" | "left" | "right";
        stretch: boolean;
        DefaultIndex: number;
        Tabs: TabItem[];
        useBinding: boolean;
        bindingOptions: any;
    }

    const transitionDuration = 0.3;

    const setElTransitionDurationPropertyTimeOut = 0.3 * 1000;

    export class TabHeaderCellType extends ElementCellTypeBase<ITabHeaderCellTypeParam> {

        _setElTransitionDurationPropertyTimer;

        public setElTransitionDurationProperty(delay: boolean = false) {
            const handler = () => document.documentElement.style.setProperty('--el-transition-duration', transitionDuration + "s");

            if (delay) {
                if (this._setElTransitionDurationPropertyTimer) {
                    clearTimeout(this._setElTransitionDurationPropertyTimer);
                }
                this._setElTransitionDurationPropertyTimer = setTimeout(handler, setElTransitionDurationPropertyTimeOut);
            } else {
                handler();
            }
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;
            const self = this;
            let tabs = [];
            if (!cellType.useBinding && cellType.Tabs) {
                cellType.Tabs.every(i => i.ValueStr = i.Value?.toString());
                tabs = cellType.Tabs;
            }

            this.addCustomClass("el-tabs-custom");
            const activeName = (cellType.DefaultIndex - 1).toString();
            const classStr = "fgc-" + cellType.type;
            const option = {
                template: `<el-tabs v-model="activeName" class="${classStr}" :type="type" :stretch="stretch" :tab-position="position" @tab-click="ItemClick">
<template v-for="(tab,index) in tabs">
  <el-tab-pane :name="tab.ValueStr" :label="tab.Name"></el-tab-pane>
</template>
</el-tabs>`,
                data() {
                    return {
                        tabs,
                        activeName,
                        type: cellType.type === "default" ? undefined : cellType.type,
                        position: cellType.position,
                        stretch: cellType.stretch
                    };
                },
                methods: {
                    getValue() {
                        return this.activeName;
                    },
                    setValue(value) {
                        document.documentElement.style.setProperty('--el-transition-duration', "0s");
                        this.activeName = value?.toString();

                        self.setElTransitionDurationProperty(true);
                    },
                    ItemClick(tab) {
                        self.setElTransitionDurationProperty(false);

                        setTimeout(() => {
                            const index = Number(tab.index);
                            if (cellType.ClickCommand?.Commands?.length) {
                                const initValue = {};
                                initValue[cellType.ClickCommand.ParamProperties["itemIndex"]] = index + 1;
                                initValue[cellType.ClickCommand.ParamProperties["itemValue"]] = this.tabs[index].Value;
                                initValue[cellType.ClickCommand.ParamProperties["itemText"]] = tab.props.label;
                                self.executeCustomCommandObject(cellType.ClickCommand, initValue);
                            }
                            self.commitValue();
                        }, transitionDuration * 1000);
                    },
                }
            };
            this.createVueApp(option);

            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => {
                    if (dataSource) {
                        dataSource.every(i => i.ValueStr = i.Value?.toString());
                    }
                    this.vue.tabs = dataSource ?? [];
                });
            }
            super.onPageLoaded(info);
        }

        public HideItems(value) {
            const itemArray: string[] = value?.split(',')?.map((item) => item.toString());

            if (!itemArray?.length) {
                return;
            }

            this.vue.tabs = this.vue.tabs.filter(tab => !itemArray.includes(tab.Value?.toString()));
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.TabHeader, ElementUI", ElementCellTypes.TabHeaderCellType);