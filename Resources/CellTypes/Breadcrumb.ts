/// <reference path = "Base.ts" />
namespace ElementCellTypes {
    interface IBreadcrumbCellTypeParam {
        DefaultValue: any;
        separator: string;
        ClickCommand: Forguncy.Plugin.ICustomCommandObject;
    }
    export class BreadcrumbCellType extends ElementCellTypeBase<IBreadcrumbCellTypeParam> {
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;
            const self = this;
            const option = {
                el: "#" + this.uId,
                template: `<el-breadcrumb :separator="separator">
<template v-for="(path,index) in paths">
  <el-breadcrumb-item><a :href="path.href" @click="ItemClick(path,index)">{{path.pageName}}</a></el-breadcrumb-item>
</template>
</el-breadcrumb>`,
                data() {
                    return {
                        paths: [],
                        pageName: undefined,
                        separator: cellType.separator
                    };
                },
                methods: {
                    ItemClick(node, index) {
                        if (index !== this.paths.length - 1 && cellType.ClickCommand?.Commands?.length) {
                            const initValue = {};
                            initValue[cellType.ClickCommand.ParamProperties["pageName"]] = node.pageName;
                            self.executeCustomCommandObject(cellType.ClickCommand, initValue);
                        }
                        event.preventDefault();
                    },
                    getValue() {
                        return this.pageName;
                    },
                    setValue(value) {
                        if (value instanceof Array) {
                            value = value.join(cellType.separator);
                        }
                        this.pageName = value?.toString();
                        this.initPath();
                    },
                    initPath() {
                        if (this.pageName) {
                            const pathStrs = this.pageName.split(cellType.separator).filter(i => i);
                            const baseUrl = Forguncy.Helper.SpecialPath.getBaseUrl();
                            this.paths = pathStrs.map(i => <any>{
                                pageName: i,
                                href: baseUrl + i
                            });
                            this.$nextTick(_ => {
                                const container = $(this.$el);
                                self.fontDom = $("a,span", container);
                                self.setFontToDom();
                            });
                        }
                        else {
                            this.paths = [];
                        }
                    }
                }
            };
            this.createVueApp(option);

            super.onPageLoaded(info);
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.BreadcrumbCellType, ElementUI", ElementCellTypes.BreadcrumbCellType);