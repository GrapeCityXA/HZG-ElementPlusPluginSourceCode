/// <reference path = "Base.ts" />
namespace ElementCellTypes {
    interface IPaginationCellTypeParam {
        PagingChangeCommands: Forguncy.Plugin.ICustomCommandObject;
        ListviewName: string;
        pageSize: number;
        pagerCount: number;
        layout: PageLayout[];
        pageSizes: PageSize[];
        prevText: string;
        nextText: string;
        small: boolean;
        background: boolean;
        hideOnSinglePage: boolean;
    }

    interface PageLayout {
        layoutItem: string;
    }
    interface PageSize {
        value: number;
    }
    interface IPaginationCommand {
        CommandList: object[];
        valueParamName: string;
    }
    export class PaginationCellType extends ElementCellTypeBase<IPaginationCellTypeParam> {
        _listview: Forguncy.ListView;
        _pagingChangedCallBack: Function;
        public createContent() {
            const cellType = this.cellType;
            if (cellType.ListviewName) {
                Forguncy.ForguncyData.initListviewPaginationInfo(this.runTimePageName, cellType.ListviewName, cellType.pageSize);
            }

            return super.createContent();
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const self = this;
            this.addCustomClass("el-pagination-custom");
            const cellType = this.cellType;
            if (cellType.ListviewName) {
                this._listview = Forguncy.Page.getListViews(true)
                    .filter(i => i.getName() === cellType.ListviewName && i.getRunTimePageName() === this.runTimePageName)[0];
            }
            const listview = this._listview;
            const option = {
                template: `<div class="fgc-pagination">
<el-pagination
    :small="small"
    :background="background"
    :page-size="pageSize"
    :total=total
    :pager-count="pagerCount"
    :layout="layout"
    :page-sizes="pageSizes"
    :prev-text="prevText"
    :next-text="nextText"
    :disabled="disabled"
    :hide-on-single-page="hideOnSinglePage"
    v-model:currentPage="currentPage"

    @size-change="onSizeChanged"
    @current-change="onCurrentChanged"
  >
  </el-pagination>
</div>`,
                data() {
                    return {
                        small: cellType.small,
                        background: cellType.background,
                        pageSize: Number(cellType.pageSize),
                        total: 100,
                        pagerCount: cellType.pagerCount,
                        layout: cellType.layout.map(i => i.layoutItem).join(","),
                        pageSizes: cellType.pageSizes.map(i => i.value),
                        prevText: cellType.prevText,
                        nextText: cellType.nextText,
                        disabled: undefined,
                        currentPage: 0,
                        hideOnSinglePage: cellType.hideOnSinglePage,
                    };
                },
                methods: {
                    onSizeChanged(size) {
                        this.pageSize = size;
                        this.updateSize();
                        this.executeCommand();
                    },
                    onCurrentChanged() {
                        this.updatePage();
                        this.executeCommand();
                        self.commitValue();
                    },
                    executeCommand() {
                        if (cellType.PagingChangeCommands?.Commands?.length) {
                            const commands = cellType.PagingChangeCommands;
                            const initParam = {};
                            initParam[commands.ParamProperties["pageSize"]] = this.pageSize;
                            initParam[commands.ParamProperties["currentPage"]] = this.currentPage;
                            initParam[commands.ParamProperties["total"]] = this.total;
                            self.executeCustomCommandObject(commands, initParam, "CurrentPageChangeCommands_" + self.ID);
                        }
                    },
                    setValue(value) {
                        let pageIndex = Math.max(1, Number(value));
                        if (Number.isNaN(pageIndex)) {
                            pageIndex = 1;
                        }
                        if (pageIndex !== this.currentPage) {
                            this.currentPage = pageIndex;
                            this.updatePage();
                        }
                    },
                    getValue() {
                        return this.currentPage;
                    },
                    updateSize() {
                        if (listview) {
                            listview.usePaginationDisplay(this.pageSize);
                        }
                    },
                    updatePage() {
                        if (listview) {
                            listview.goToSpecifiedPage(this.currentPage);
                        }
                    },
                    disable() {
                        this.disabled = true;
                    },
                    enable() {
                        this.disabled = false;
                    }
                },
                watch: {
                    pageSize(value) {
                        if (typeof value !== "number") {
                            const numerValue = Number(value);
                            this.value = isNaN(numerValue) ? null : numerValue;
                        }
                    }
                }
            };
            this.createVueApp(option);

            if (listview) {
                const currentInfo = listview.getPaginationInfo();
                this.vue.total = currentInfo?.TotalRowCount;
                this.vue.currentPage = currentInfo ? currentInfo.PageIndex : 1;
                this.vue.pageSize = currentInfo?.MaxRowCountOfOnePage ?? this.vue.pageSize;

                this._pagingChangedCallBack = (data: any, info: Forguncy.PageingInfoChangedEventArg) => {
                    this.vue.total = info.TotalRowCount;
                    this.vue.currentPage = info.CurrentPageIndex;
                    this.vue.pageSize = info.MaxRowCountOfOnePage;
                };
                listview.bind(Forguncy.ListViewEvents.PageingInfoChanged, this._pagingChangedCallBack);
            }
            super.onPageLoaded(info);
        }

        destroy() {
            super.destroy();
            if (this._listview) {
                this._listview.unbind(Forguncy.ListViewEvents.PageingInfoChanged, this._pagingChangedCallBack);
            }
        }

        // RunTimeMethods
        SetPageSize(pageSize) {
            pageSize = Number(pageSize);
            this.vue.pageSize = pageSize;
            this.vue.updateSize();
        }
        SetCurrentPageIndex(index) {
            index = Number(index);
            this.vue.currentPage = index;
            this.vue.updatePage();
        }
        SetTotal(total) {
            total = Number(total);
            this.vue.total = total;
            this.vue.updateSize();
        }
        ExecuteCommand() {
            this.vue.executeCommand();
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.CellTypes.PaginationCellType, ElementUI", ElementCellTypes.PaginationCellType);