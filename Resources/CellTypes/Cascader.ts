/// <reference path = "Base.ts" />
namespace ElementCellTypes {
    enum ExpendTrigger {
        Click,
        Hover
    }

    interface ICascaderCellTypeParam {
        useBinding: boolean;
        options: any;
        bindingOptions: any;
        placeholder: string;
        IsDisabled: boolean;
        clearable: boolean;
        showAllLevels: boolean;
        collapseTags: boolean;
        separator: string;
        filterable: boolean;
        expandTrigger: ExpendTrigger;
        multiple: boolean;
        emitPath: boolean;
        checkStrictly: boolean;
        tagType: "success" | "info" | "warning" | "danger";
    }

    export class CascaderCellType extends InputCellTypeBase<ICascaderCellTypeParam> {
        public setDataSource(dataSource) {
            return this.vue.setOptions(dataSource);
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const nodeStr = this.getCustomSlotByPath(SlotPath.cascaderNode);
            const self = this;
            const cellType = this.cellType;
            let options = [];
            if (!cellType.useBinding && cellType.options) {
                options = cellType.options;
            }
            this.addCustomClass("el-cascader-custom");

            const emitPath = cellType.emitPath && !cellType.multiple;

            const valueIsArrayStructure = emitPath || cellType.multiple;

            const { CssClassName } = this.CellElement;
            const popperClass = CssClassName ? `${CssClassName}-popper` : "";

            const option = {
                template: `
<el-cascader
v-model="value"
:options="options"
:props="props"
:placeholder="placeholder"
:disabled = "disabled"
:clearable= "clearable"
:show-all-levels = "showAllLevels"
:collapse-tags = "collapseTags"
:separator = "separator"
:filterable = "filterable"
:tag-type="tagType"
popper-class="${popperClass}"
ref="elInput"
@change="handleChange"
style="height:100%;width:100%"
>
    ${nodeStr ? "<template #default='{ node, data }'>" + nodeStr + "</template>" : ""}
</el-cascader>
`,
                data() {
                    return {
                        disabled: cellType.IsDisabled,
                        value: null,
                        props: {
                            expandTrigger: cellType.expandTrigger === ExpendTrigger.Click ? "click" : "hover",
                            multiple: cellType.multiple,
                            emitPath: !!emitPath,
                            checkStrictly: cellType.checkStrictly
                        },
                        options: options,
                        tagType: cellType.tagType,
                        placeholder: cellType.placeholder,
                        clearable: cellType.clearable,
                        showAllLevels: cellType.showAllLevels ? true : false,
                        collapseTags: cellType.collapseTags,
                        separator: cellType.separator,
                        filterable: cellType.filterable
                    };
                },
                methods: {
                    handleChange() {
                        self.commitValue();
                        self.validate();
                    },
                    getValue() {
                        const value = this.value;

                        if (value instanceof Array) {
                            return value.join(",");
                        }
                        return value;
                    },
                    setValue(value) {
                        if (self.isEmpty(value)) {
                            this.value = null;
                            return;
                        }

                        if (valueIsArrayStructure) {
                            let values = [];

                            const stringValues: string[] = value.toString().split(",");

                            if (cellType.multiple) {
                                const flatOptions = TreeHelper.flat(this.options);
                                const options = flatOptions.filter(o => stringValues.includes(o.value?.toString()));
                                values = options.map(o => o.value);
                            } else {
                                stringValues.reduce((options: ITreeNodeBase[], cur) => {
                                    if (!Array.isArray(options) || !options.length) {
                                        return [];
                                    }
                                    // 此处比较需要忽略类型，使用双等号
                                    // eslint-disable-next-line
                                    const item = options.find(item => item.value == cur);
                                    if (!item) {
                                        return [];
                                    }
                                    values.push(item.value);
                                    return item.children;
                                }, this.options);
                            }
                            this.value = values;
                        } else {
                            const flatOptions = TreeHelper.flat(this.options);

                            // 这里认为字符串数字与数字等价，双等号不能去掉
                            // eslint-disable-next-line
                            const option = flatOptions.find(o => o.value == value);
                            this.value = option?.value ?? null;
                        }
                    },
                    disable() {
                        this.disabled = true;
                    },
                    enable() {
                        this.disabled = false;
                    },
                    setOptions(options) {
                        if (options) {
                            this.options = options;
                        } else {
                            this.options = [];
                        }
                    }
                },
                mounted() {
                    self.fontDom = $("input", `#${self.uId}`);
                    self.setFontToDom();
                }
            };
            this.createVueApp(option);

            self.ReloadBindingItems();

            super.onPageLoaded(info);
        }

        public ReloadBindingItems() {
            const cellType = this.cellType;
            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions,
                    data => this.setDataSource(TreeHelper.build(data)));
            }
        }

        public reload() {
            this.ReloadBindingItems();
        }

        public SetDataSourceByObjTree(dataSource: any[], valueProperty, labelProperty, childrenProperty) {
            if (typeof dataSource === "string") {
                dataSource = JSON.parse(dataSource);
            }
            if (childrenProperty) {
                childrenProperty = childrenProperty.split('|');
            }
            if (valueProperty) {
                valueProperty = valueProperty.split('|');
            }
            labelProperty = labelProperty ? labelProperty.split('|') : valueProperty;

            const source = this.buildTree(dataSource, valueProperty, labelProperty, childrenProperty);
            this.setDataSource(source);
        }

        private buildTree(dataSource: any[], valueProperty: string[], labelProperty: string[], childrenProperty: string[]) {
            if (!dataSource) {
                return undefined;
            }
            return dataSource.map(i => <any>{
                value: this.getSubPropertyValue(i, valueProperty),
                label: this.getSubPropertyValue(i, labelProperty),
                children: this.buildTree(this.getSubPropertyValue(i, childrenProperty), valueProperty, labelProperty, childrenProperty),
            });
        }

        private getSubPropertyValue(obj, subProperties: string[]) {
            for (const prop of subProperties) {
                if (obj[prop] !== undefined) {
                    return obj[prop];
                }
            }
            return undefined;
        }

        public SetDataSourceByIdPidTable(dataSource: any[], valueProperty, labelProperty, parentValue) {
            if (typeof dataSource === "string") {
                dataSource = JSON.parse(dataSource);
            }
            const treeObj = TreeHelper.build(dataSource.map(i => <IItem>{
                ...i,
                value: i[valueProperty],
                label: i[labelProperty],
                parentValue: i[parentValue],
            }));
            this.setDataSource(treeObj);
        }

        public GetCheckedNodes(leafOnly) {
            if (typeof (leafOnly) === "string") {
                leafOnly = !(leafOnly.toLowerCase() === "false" || leafOnly.toLowerCase() === "0");
            }
            else {
                leafOnly = !!leafOnly;
            }
            return {
                CheckedItems: this.vue.$refs.elInput.getCheckedNodes(leafOnly).map(i => <any>{
                    "value": i.value,
                    "label": i.label,
                })
            };
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.CascaderCellType, ElementUI", ElementCellTypes.CascaderCellType);