/// <reference path = "Base.ts" />
namespace ElementCellTypes {
    interface INavMenuCellTypeParam {
        mode: string;
        useBinding: boolean;
        options: IMenuNode[];
        bindingOptions: any;
        backgroundColor: string;
        textColor: string;
        activeTextColor: string;
        SelectCommand: Forguncy.Plugin.ICustomCommandObject;
        ClickCommand: Forguncy.Plugin.ICustomCommandObject;
        collapse: boolean;
        uniqueOpened: boolean;
        collapseTransition: boolean;
        menuTrigger: "hover" | "click";
        DefaultExpansion: "expand" | "collapse";
    }

    interface ITreeNode extends ITreeNodeBase {
        index: string;
    }
    interface IMenuNode extends ITreeNode {
        icon: Icon;
        svgIcon: string;
        iconSrc: string;
        label: string;
        notification: string;
        notice: any;
        expend: string;
        children: IMenuNode[];
    }

    const { Page } = Forguncy;

    const scrollTopData: { [name: string]: number } = {};

    export class NavMenuCellType extends ElementCellTypeBase<INavMenuCellTypeParam> {

        private _pageName = this.IsInMasterPage ? Page.getMasterPageName() : Page.getPageName();

        private _scrollBarDataKey = `${this._pageName}_${this.ID}`;

        private _firstRender = true;

        public setDataSource(dataSource) {
            return this.vue.setOptions(dataSource);
        }

        public generateIndex(options, parentIndex = null) {
            return options.map((option, index) => {
                const key = parentIndex !== null ? `${parentIndex}-${index}` : index.toString();

                if (option?.children?.length) {
                    option.children = this.generateIndex(option.children, key);
                }
                return { ...option, index: key };
            });
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {

            this.addCustomClass("el-menu-custom");
            const self = this;

            const cellType = this.cellType;
            const { CssClassName } = this.CellElement;

            let options = [];
            let flatOptions = [];

            const openeds: Set<string> = new Set();

            let defaultOpeneds = [];

            const isVertical = cellType.mode === "vertical";

            if (!cellType.useBinding && cellType.options) {

                options = this.generateIndex(cellType.options);
                flatOptions = TreeHelper.flat(options);

                NavMenuCellType.recalcNotice(options, this);

                if (isVertical) {
                    defaultOpeneds = this.getDefaultOpeneds(cellType.options);
                    defaultOpeneds.forEach((index) => openeds.add(index));
                }
            }

            this.addCustomClass(cellType.mode + "-menu");

            let intervalTimer = null;

            let timeoutTimer = null;

            const clearTimers = () => {
                clearInterval(intervalTimer);
                clearInterval(timeoutTimer);
            };

            const subMenuPopperClass = CssClassName ? `${CssClassName}-popper` : "";

            const contentStr = this.getCustomSlotByPath(SlotPath.menuItemContent);

            const menuItemOption = {
                name: "menu-item",
                template: `
                    <el-menu-item :index="item.index" :key="item.value">
                        ${contentStr || self.getDefaultMenuItemSlot()}
                    </el-menu-item>
                `,
                props: {
                    item: Object,
                },
            };

            const subMenuOption = {
                name: "sub-menu",
                template: `
                    <el-sub-menu :index="item.index" :key="item.value" popper-class="${subMenuPopperClass}" @click.stop="handleClick(item.index)">
                        <template #title>
                           <el-icon  v-if="item.svgIcon" v-html="item.svgIcon" />
                           <el-image v-if="item.iconSrc" class="el-icon" fit="contain" :src="item.iconSrc" />
                           <span class="fgc-el-menu-transparent">{{item.label}}</span>
                           <el-badge :value="item.notice" class="fgc-menu-badge-item" />
                        </template>
                    
                        <template v-for="subItem in item.children">
                            <sub-menu  v-if="subItem && subItem.children && subItem.children.length" :item="subItem"  @click.stop="handleClick(subItem.index)"/>
                            <menu-item v-else :item="subItem" />
                        </template>
                    
                    </el-sub-menu>
                `,
                props: {
                    item: Object
                },
                components: {
                    "menu-item": menuItemOption,
                },
                methods: {
                    handleClick(index) {
                        this.$emit("handleClick", index);
                    }
                }
            };

            const option = {
                template: `
<el-scrollbar>
    <el-menu
        v-if="options?.length"
        ref="menu"
        :mode="mode"
        :background-color="backgroundColor"
        :text-color="textColor"
        :active-text-color="activeTextColor"
        :default-openeds="defaultOpeneds"
        :default-active="defaultActive"
        :unique-opened="uniqueOpened"
        :collapse="collapse"
        :collapse-transition="collapseTransition"
        :menu-trigger="menuTrigger"
        :ellipsis="false"
        @select="handleSelect"
        @open="handleOpen"
        @close="handleClose"
    >
        <template v-for="item in options">
            <sub-menu v-if="item?.children?.length" :item="item" @handleClick="handleClick"/>
            <menu-item v-else :item="item" />
        </template>
    </el-menu>
</el-scrollbar>
`,
                components: {
                    "sub-menu": subMenuOption,
                    "menu-item": menuItemOption,
                },
                data() {
                    return {
                        defaultActive: null,
                        selectedValue: null,
                        selectedKey: null,
                        options,
                        mode: cellType.mode,
                        collapse: isVertical && !!cellType.collapse,
                        uniqueOpened: cellType.uniqueOpened,
                        menuTrigger: cellType.menuTrigger,
                        collapseTransition: !!cellType.collapseTransition,
                        backgroundColor: Forguncy.ConvertToCssColor(cellType.backgroundColor) ?? "transparent",
                        textColor: Forguncy.ConvertToCssColor(cellType.textColor),
                        activeTextColor: Forguncy.ConvertToCssColor(cellType.activeTextColor),
                        defaultOpeneds: defaultOpeneds,
                        ellipsis: true,
                        openeds,
                        flatOptions
                    };
                },
                mounted() {
                    this.elMenuOnRendered();
                },
                watch: {
                    options: {
                        deep: true,
                        handler: function () {
                            this.flatOptions = TreeHelper.flat(this.options);
                        }
                    }
                },
                methods: {
                    elMenuOnRendered() {
                        if (!this.$refs.menu) {
                            return;
                        }
                        const scrollTop = () => {
                            const scrollBarElement = this.getScrollBarElement();
                            if (scrollBarElement) {
                                scrollBarElement.scrollTop(scrollTopData[self._scrollBarDataKey]);
                                clearTimers();
                            }
                        };

                        if (!isVertical) {
                            $(".el-menu", self.vueContainer)[0].style.setProperty('--el-menu-item-height', (self.vueContainer.height() - 3) + "px");
                        }

                        timeoutTimer = setTimeout(clearTimers, 2000);

                        intervalTimer = setInterval(scrollTop, 50);
                    },
                    getScrollBarElement() {
                        return $(".el-scrollbar__wrap", $(this.$el));
                    },
                    setScrollTopValue() {
                        scrollTopData[self._scrollBarDataKey] = this.getScrollBarElement()?.scrollTop();
                    },
                    handleSelect(key: string) {
                        this.setScrollTopValue();
                        this.handleSelectInternal(key);
                    },
                    getNodeAndParentNode(index) {
                        let node;
                        let pNode;
                        const pNodeIndex = index.substring(0, index.lastIndexOf("-"));
                        this.flatOptions.forEach((option:IMenuNode)=>{
                            if(option.index === index){
                                node = option;
                                return;
                            }
                            if(option.index === pNodeIndex){
                                pNode = option;
                            }
                        });

                        return { node, pNode };
                    },
                    handleClick(index) {
                        const { node, pNode } = this.getNodeAndParentNode(index);
                        this.execClickCommand(node, pNode)
                    },
                    execClickCommand(node, pNode) {
                        const commands = cellType.ClickCommand;
                        const initValue = {};
                        if (commands?.ParamProperties) {
                            initValue[commands.ParamProperties["value"]] = node?.value;
                            initValue[commands.ParamProperties["label"]] = node?.label;
                            initValue[commands.ParamProperties["parentId"]] = pNode?.value;
                        }

                        self.executeCustomCommandObject(commands, initValue, "click");
                    },
                    handleSelectInternal(key: string, preventCommandExecution = false) {
                        this.selectedKey = key;
                        const { node, pNode } = this.getNodeAndParentNode(key);

                        this.selectedValue = node?.value;
                        self.commitValue();

                        if (preventCommandExecution) {
                            return;
                        }
                        const commands = cellType.SelectCommand;
                        const initValue = {};
                        if (commands?.ParamProperties) {
                            initValue[commands.ParamProperties["value"]] = node?.value;
                            initValue[commands.ParamProperties["label"]] = node?.label;
                            initValue[commands.ParamProperties["parentId"]] = pNode?.value;
                        }

                        self.executeCustomCommandObject(commands, initValue, "select");
                        this.execClickCommand(node, pNode);
                    },
                    handleOpen(index) {
                        this.openeds.add(index);
                    },
                    handleClose(index) {
                        this.openeds.delete(index);
                    },
                    openMenuItem(index) {
                        this.handleOpen(index);
                        this.$refs.menu.open(index);
                    },
                    closeMenuItem(index) {
                        this.handleClose(index);
                        this.$refs.menu.close(index);

                    },
                    getNodeKey(value: string) {
                        return this.flatOptions?.find(option => option.value === value)?.index;
                    },
                    getValue() {
                        return this.selectedValue;
                    },
                    setValue(value) {
                        const key = this.getNodeKey(value);
                        if (key) {
                            this.handleSelectInternal(key, true);
                        }
                        this.defaultActive = key;
                    },
                    setOptions(options) {

                        const optionsTree = self.generateIndex(options ? TreeHelper.build(options) : []);

                        const isExpand = !cellType.uniqueOpened && cellType.DefaultExpansion === "expand" && isVertical;

                        const flatOptions = <ITreeNode[]>TreeHelper.flat(optionsTree);

                        const { _firstRender } = self;

                        if (_firstRender && isExpand) {
                            self._firstRender = false;

                            const defaultOpeneds = [];

                            flatOptions.forEach((option) => {
                                if (option?.children?.length) {
                                    defaultOpeneds.push(option.index);
                                }
                            });
                            this.defaultOpeneds = defaultOpeneds;
                            defaultOpeneds.forEach((index) => this.openeds.add(index));
                        }

                        if (!isVertical) {
                            this.ellipsis = false;
                        } else if (!_firstRender) {
                            document.documentElement.style.setProperty('--el-transition-duration', "0s");
                        }

                        this.options = optionsTree;
                        this.flatOptions = flatOptions;

                        /**
                         * 这是Element的bug
                         * bug表现： 不管容器宽度有多宽，只会显示两个menu，其余都会被隐藏
                         */
                        if (!isVertical) {
                            this.$nextTick(() => this.ellipsis = true);
                        } else {
                            if(_firstRender){
                                this.elMenuOnRendered();
                            }else if (isExpand) {
                                this.$nextTick(() => {
                                    flatOptions.forEach((option) => option?.children?.length && this.$refs?.menu.open(option.index));
                                    this.$nextTick(() => {
                                        document.documentElement.style.setProperty('--el-transition-duration', "0.3s");
                                    });
                                });
                            }
                        }

                        NavMenuCellType.recalcIcon(this.options, self);
                    },
                    setCollapse(collapse) {
                        if (collapse) {
                            if (cellType.collapseTransition) {
                                const openeds = this.options.filter(option=>this.openeds.has(option.index));

                                const timeout = openeds.length ? 300 : 0;

                                openeds.forEach(({index}) => this.closeMenuItem(index));

                                setTimeout(() => {
                                    $(".fgc-el-menu-transparent", self.vueContainer).css("opacity", "0");
                                    this.collapse = true;
                                }, timeout);

                            } else {
                                this.collapse = true;
                            }
                        } else {
                            this.collapse = false;
                        }
                    },
                    refreshNotice() {
                        NavMenuCellType.recalcNotice(this.options, self);
                    }
                },
                beforeDestroy() {
                    clearTimers();
                }
            };

            this.createVueApp(option);

            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource));
            }

            this.onDependenceCellValueChanged(() => {
                this.vue.refreshNotice();
            });

            super.onPageLoaded(info);

            NavMenuCellType.recalcIcon(this.vue.options, this);

        }

        private getDefaultMenuItemSlot() {
            return `
            <el-icon  v-if="item.svgIcon" v-html="item.svgIcon" />
            <el-image v-if="item.iconSrc" class="el-icon" fit="contain" :src="item.iconSrc" />
            <template #title>
                <span class="fgc-el-menu-transparent">{{item.label}}</span>
                <el-badge :value="item.notice" class="fgc-menu-badge-item" />
            </template>`;
        }

        public ReloadBindingItems() {
            const cellType = this.cellType;
            if (cellType.useBinding) {
                SupportDataSourceCellType.refreshData(this, cellType.bindingOptions, dataSource => this.setDataSource(dataSource));
            }
        }
        static recalcNotice(option: IMenuNode[], cellType: NavMenuCellType) {
            const nodes = TreeHelper.flat(option);
            for (const node of nodes) {
                const menuNode = <IMenuNode>node;
                if (menuNode && menuNode.notification) {
                    const result = cellType.evaluateFormula(menuNode.notification);
                    if (result) {
                        menuNode.notice = result + "";
                    }
                    else {
                        menuNode.notice = "";
                    }
                }
            }
        }
        static recalcIcon(option: IMenuNode[], cellType: NavMenuCellType) {
            const nodes = TreeHelper.flat(option);
            for (const node of nodes) {
                const menuNode = <IMenuNode>node;
                if (typeof (menuNode.icon) === "string") {
                    let src = <string>menuNode.icon;
                    if (this.isAttachment(menuNode.icon)) {
                        src = Forguncy.Helper.SpecialPath.getUploadImageFolderPathInServer() + encodeURIComponent(menuNode.icon);
                    }
                    menuNode.iconSrc = src;
                }
                else {
                    if (menuNode && (<Icon>menuNode.icon)?.Name) {

                        cellType.getIcon(menuNode.icon, icon => {
                            if (icon.isSvg) {
                                menuNode.svgIcon = icon.icon;
                            }
                            else {
                                menuNode.iconSrc = icon.icon;
                            }
                        });
                    }
                }
            }
        }
        getDefaultOpeneds(option: IMenuNode[], baseIndex = "", result = []) {

            for (let i = 0; i < option?.length; i++) {

                const item = option[i];

                if (item?.children) {

                    if (item.expend) {
                        result.push(baseIndex + i);
                    }

                    this.getDefaultOpeneds(item.children, `${baseIndex}${i}-`, result);
                }
            }

            return result;
        }

        static getNodePath(key: string, options: IMenuNode[]) {
            if (!key) {
                return null;
            }
            const keys = key.split("-");

            let node = options.find(option => option.index === keys[0]);
            let index = keys[0];
            const nodes = [node];

            for (let i = 1; i < keys.length; i++) {
                index += "-" + keys[i];
                node = node.children.find(option => option.index === index);
                nodes.push(node);
            }
            return nodes;
        }

        //RunTime Method
        public GetSelectPath(type: string) {
            const nullValue = {
                PathArray: null
            };
            let key = this.vue.selectedKey;
            if (!key) {
                const value = this.getValueFromDataModel();
                if (this.vue) {
                    key = this.vue.getNodeKey(value);
                }
                if (!key) {
                    return nullValue;
                }
            }
            const nodes = NavMenuCellType.getNodePath(key, this.vue.options);
            if (nodes) {
                if (type === "valuePath") {
                    return {
                        PathArray: nodes.map(i => i.value)
                    };
                }
                else {
                    return {
                        PathArray: nodes.map(i => i.label)
                    };
                }
            }
            return nullValue;
        }
        public reload() {
            this.ReloadBindingItems();
        }
        // RunTimeMethod
        public SetCollapse(collapse) {
            this.vue.setCollapse(collapse);
        }
        // RunTimeMethod
        public HideItems(items) {
            if (!items) {
                return;
            }

            const itemArray = items.split(',');

            const options = <IMenuNode[]>this.vue.options;

            this.HideItemsInternal(options, itemArray);
        }
        // RunTimeMethod
        public HideItemsInternal(options: IMenuNode[], hideArray: string[]) {
            if (!options) {
                return;
            }
            for (let i = options.length - 1; i >= 0; i--) {
                const option = options[i];
                // 此处比较需要忽略类型，使用双等号
                // eslint-disable-next-line
                if (hideArray.find((value) => option.value == value)) {
                    options.splice(i, 1);
                }
                else {
                    this.HideItemsInternal(option.children, hideArray);
                }
            }
        }
        // RunTimeMethod
        public SetBadge(itemValue, badgeValue) {
            this.SetBadgeInternal(this.vue.options, itemValue, badgeValue);
        }

        SetBadgeInternal(options: IMenuNode[], itemValue, badgeValue) {
            if (!options) {
                return;
            }
            for (let i = options.length - 1; i >= 0; i--) {
                const option = options[i];

                // eslint-disable-next-line
                if (option.value == itemValue) {
                    option.notice = badgeValue?.toString();
                }

                this.SetBadgeInternal(option.children, itemValue, badgeValue);
            }
        }
    }
    export class CellHelper {
        static setValueToCell(context: Forguncy.FormulaCalcContext, cellLocationFormula: any, value: any): void {
            if (!cellLocationFormula) {
                return;
            }
            const cellLocation = Forguncy.Helper.getCellLocation(cellLocationFormula, context);
            const cell = Forguncy.Page.getCellByLocation(cellLocation);
            if (cell) {
                cell.setValue(value);
            }
            else {
                Forguncy.CommandHelper.setVariableValue(cellLocationFormula, value);
            }
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.NavMenuCellType, ElementUI", ElementCellTypes.NavMenuCellType);