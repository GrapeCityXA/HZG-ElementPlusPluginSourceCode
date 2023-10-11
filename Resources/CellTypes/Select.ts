/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    interface INormalItem {
        value: any;
        label: any;
    }

    interface IfilterInServerOptions {
        defaultMaxOptionsCount: number;
        maxFilterResultCount: number;
        matchMethod: "contains" | "startWith";
        loadingText: string;
    }

    interface ISelectCellTypeParam {
        useBinding: boolean;
        options: INormalItem[];
        bindingOptions: any;
        placeholder: string;
        IsDisabled: boolean;
        clearable: boolean;
        collapseTags: boolean;
        filterable: boolean;
        multiple: boolean;
        allowCreate: boolean;
        reserveKeyword: boolean;
        multipleLimit: number;
        noMatchText: string;
        noDataText: string;
        filterInServer: boolean;
        filterInServerOptions: IfilterInServerOptions;
        AllowAddEmptyItem: boolean;
        EmptyItemLabel: string;
    }

    export class SelectCellType extends InputCellTypeBase<ISelectCellTypeParam> {

        public setDataSource(dataSource: any[]) {
            const cloneDataSource = [...dataSource];
            if (this.cellType.AllowAddEmptyItem) {

                const label = this.cellType.EmptyItemLabel ?? "";

                if (!label) {
                    this.vue.placeholder = null;
                }

                /**
                 * Element Plus 不接受一个null,会报 warning
                 */
                cloneDataSource.unshift({ value: "", label });
            }
            return this.vue.setOptions(cloneDataSource);
        }

        public defaultQueryDataOption: Forguncy.Plugin.queryDataOption = {
            top: this.cellType.filterInServerOptions.defaultMaxOptionsCount,
            queryConditions: [],
            distinct: true
        };

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {

            const optionStr = this.getCustomSlotByPath(SlotPath.selectOption);
            const prefixStr = this.getCustomSlotByPath(SlotPath.selectPrefix);

            const self = this;

            const styleInfo = this._cellStyle ?? {};

            const fontSize = styleInfo.FontSize && styleInfo.FontSize > 0 ? styleInfo.FontSize : "";

            const cellType = this.cellType;
            this.addCustomClass("el-select-custom");
            let options = [];
            if (cellType.options && !cellType.useBinding) {
                options = cellType.options.map(o => ({ ...o, value: o.value ?? ""}));
            }
            let remoteFilterBinding = '';
            if (cellType.useBinding && cellType.filterInServer) {
                remoteFilterBinding = ':remote-method="remoteMethod"';
            }
            const { CssClassName } = this.CellElement;
            const popperClass = CssClassName ? `${CssClassName}-popper` : "";
            const option = {
                template: `
<el-select
    v-model="value"
    :placeholder="placeholder"
    :disabled = "disabled"
    :clearable= "clearable"
    :collapse-tags = "collapseTags"
    :filterable = "filterable"
    :multiple = "multiple"
    :allow-create = "allowCreate"
    :reserve-keyword = "reserveKeyword"
    :multiple-limit = "multipleLimit"
    :no-match-text = "noMatchText"
    :no-data-text = "noDataText"
    :remote="filterInServer"
    popper-class="${popperClass}"
    ${remoteFilterBinding}
    :loading-text="loadingText"
    :loading="loading"
    default-first-option
    ref="elInput"
    @change="handleChange" style="height:100%;width:100%">
        <el-option
              style="font-size:${fontSize}px;"
              v-for="option in options"
              :key="option.value"
              :label="option.label"
              :value="option.value"
        >
            ${optionStr}
        </el-option>
        ${prefixStr ? "<template #prefix>" + prefixStr + "</template>" : ""}
    </el-select>
`,
                data() {
                    return {
                        disabled: cellType.IsDisabled,
                        value: "",
                        options: options,
                        placeholder: cellType.placeholder,
                        clearable: cellType.clearable,
                        collapseTags: cellType.collapseTags,
                        filterable: cellType.filterable,
                        multiple: cellType.multiple,
                        allowCreate: cellType.allowCreate,
                        reserveKeyword: cellType.reserveKeyword,
                        multipleLimit: cellType.multipleLimit,
                        noMatchText: cellType.noMatchText,
                        noDataText: cellType.noDataText,
                        loading: false,
                        filterInServer: cellType.filterInServer,
                        loadingText: cellType.filterInServerOptions.loadingText,
                    };
                },
                methods: {
                    handleChange() {
                        self.commitValue();
                        self.validate();
                    },
                    getValue() {
                        if (cellType.multiple && this.value instanceof Array) {
                            return this.value.join(",");
                        }
                        return this.value;
                    },
                    setValue(value) {
                        this.setValueInternal(value, true);
                    },
                    setValueInternal(value, loadNotMathItems = false) {
                        if (value) {
                            let arrayValue;
                            if (cellType.multiple) {
                                if (value instanceof Array) {
                                    arrayValue = value;
                                }
                                else {
                                    arrayValue = (value + "").split(",");
                                }
                            }
                            else {
                                arrayValue = [value];
                            }

                            const notMatchItems = this.updateValueByItems(arrayValue);

                            if (cellType.multiple) {
                                this.value = arrayValue;
                            }
                            else {
                                this.value = arrayValue[0];
                            }
                            if (loadNotMathItems) {
                                this.queryNotMatchItemsFromServer(notMatchItems);
                            }
                        }
                        else {
                            this.value = "";
                        }
                    },
                    queryNotMatchItemsFromServer(notMatchItems: Array<any>) {
                        if (!notMatchItems || notMatchItems.length === 0) {
                            return;
                        }
                        if (cellType.filterInServer) {
                            const queryDataOption = <Forguncy.Plugin.queryDataOption>{
                                queryConditions: [],
                                relationType: Forguncy.Plugin.relationType.Or
                            };
                            for (const notMatchItem of notMatchItems) {
                                queryDataOption.queryConditions.push({
                                    columnName: "value",
                                    compareType: Forguncy.Plugin.compareType.EqualsTo,
                                    compareValue: notMatchItem
                                });
                            }
                            self.getBindingDataSourceValue(cellType.bindingOptions, queryDataOption, newOptions => {
                                if (newOptions && newOptions.length > 0) {
                                    for (const newOption of newOptions) {
                                        this.options.push(newOption);
                                    }
                                }
                                this.setValueInternal(this.value);
                            });
                        }
                    },
                    updateValueByItems(arrayValue) {
                        const notMatchItems = [];
                        if (this.options) {
                            for (let i = 0; i < arrayValue.length; i++) {
                                const value = arrayValue[i];
                                if (!value && value !== 0) {
                                    continue;
                                }
                                let match = false;
                                for (const option of this.options) {
                                    if (option.value == value) {
                                        arrayValue[i] = option.value;
                                        match = true;
                                        break;
                                    }
                                }
                                if (!match) {
                                    notMatchItems.push(value);
                                }
                            }
                        }
                        return notMatchItems;
                    },
                    remoteMethod(query: any) {
                        let queryDataOption = this.defaultQueryDataOption;
                        if (query) {
                            queryDataOption = <Forguncy.Plugin.queryDataOption>{
                                queryConditions: [{
                                    columnName: "label",
                                    compareType: cellType.filterInServerOptions.matchMethod === "startWith" ?
                                        Forguncy.Plugin.compareType.BeginsWith : Forguncy.Plugin.compareType.Contains,
                                    compareValue: query + ""
                                }],
                                distinct: true,
                                top: cellType.filterInServerOptions.maxFilterResultCount
                            };
                        }
                        this.loading = true;
                        SupportDataSourceCellType.refreshDataWithOption(self, cellType.bindingOptions, dataSource => {
                            self.setDataSource(dataSource);
                            this.loading = false;
                        }, queryDataOption, false);
                    },
                    disable() {
                        this.disabled = true;
                    },
                    enable() {
                        this.disabled = false;
                    },
                    setOptions(options) {
                        if (options) {
                            this.options = options.map(o => ({ ...o, value: o.value ?? "" }));
                        } else {
                            this.options = [];
                        }
                        if (Array.isArray(this.value)) {
                            const values = [];
                            const set = new Set(this.value.map(toString));
                            
                            options.forEach(option=>{
                                if(set.has(option.value.toString())){
                                    values.push(option.value);
                                }
                            })
                            this.value = values;
                        } else {
                            if (!options.find(o => o.value == this.value)) {
                                this.value = "";
                            }
                        }
                        self.commitValue();
                    }
                },
                mounted() {
                    const input = $("input", $(this.$el));
                    if (input.length > 0) {
                        self.fontDom = input;
                        self.setFontToDom();
                    }
                }
            };

            this.createVueApp(option);

            if (cellType.useBinding) {
                if (cellType.filterable && cellType.filterInServer) {
                    SupportDataSourceCellType.refreshDataWithOption(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource), this.defaultQueryDataOption);
                } else {
                    self.ReloadBindingItems();
                }
            }
            super.onPageLoaded(info);
        }

        private getSelectedItem() {
            const { value, options } = this.vue;

            let filterHandler;

            if (value instanceof Array) {
                filterHandler = (option) => value.includes(option.value);
            } else {
                filterHandler = (option) => value === option.value;
            }

            return options.filter(filterHandler);
        }

        public ReloadBindingItems() {
            const cellType = <ISelectCellTypeParam>this.CellElement.CellType;
            SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource), <Forguncy.Plugin.queryDataOption>{
                distinct: true
            });
        }

        public reload() {
            if (this.cellType.useBinding) {
                this.ReloadBindingItems();
            }
        }

        public SetDataSourceByStringArray(dataSource: string[]) {
            if (!dataSource) {
                dataSource = [];
            }
            if (typeof dataSource === "string") {
                dataSource = JSON.parse(dataSource);
            }
            const objSource = dataSource.map(i => <any>{
                "value": i,
                "label": i,
            });
            this.setDataSource(objSource);
        }

        public SetDataSourceByObjArray(dataSource: any[], valueProperty: string, labelProperty: string) {
            if (!dataSource) {
                dataSource = [];
            }
            if (typeof dataSource === "string") {
                dataSource = JSON.parse(dataSource);
            }
            if (!labelProperty) {
                labelProperty = valueProperty;
            }
            const objSource = dataSource.map(i => ({
                ...i,
                "value": i[valueProperty],
                "label": i[labelProperty],
            }));
            this.setDataSource(objSource);
        }

        public Focus() {
            this.vue.$refs.elInput.focus();
        }

        public GetSelectedText() {
            return {
                SelectedText: this.getSelectedItem().map(item => item.label).join(",")
            };
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.SelectCellType, ElementUI", ElementCellTypes.SelectCellType);