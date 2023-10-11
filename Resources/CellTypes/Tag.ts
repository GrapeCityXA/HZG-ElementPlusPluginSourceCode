/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    interface ITagCellTypeParam {
        ClickCommand: Forguncy.Plugin.ICustomCommandObject;
        ColorList: { color: string }[];
        size: string;
        effect: string;
        itemSpace: number;
        hit: boolean;
        distinct: boolean;
        disableTransitions: boolean;
        separator: string;
        allowAdd: boolean;
        addButtonSettings: AddButtonSettings;
    }
    interface item {
        label: string;
        color: string;
        border: string;
        background: string;
        itemSpace: number;
        cursor: string;
    }
    interface AddButtonSettings {
        width: number;
        text: string;
        space: number;
    }

    export class TagCellType extends InputCellTypeBase<ITagCellTypeParam> {

        getColor(color) {
            color = Forguncy.ConvertToCssColor(color);
            switch (this.cellType.effect) {
                case "dark":
                    return "#fff";
                case "light":
                    return color;
                case "plain":
                    return color;
                default:
            }
        };

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;
            const self = this;
            const colorList = cellType.ColorList;
            let marginBottom = "";

            const { WordWrap } = this.CellElement.StyleInfo;

            if (WordWrap) {
                marginBottom = ",marginBottom:itemSpace+'px'";
            }

            this.fontSelector = [".el-tag__content", ".el-button-new-tag"];

            let html = '';

            for (let i = 0; i < colorList.length; i++) {
                const color = this.getColor(colorList[i % colorList.length].color);

                const isDark = this.cellType.effect === "dark";

                html += `#${this.uId} .fgc-tag-container-tag-${i}  svg {
                             color: ${color};
                         }
                         #${this.uId} .fgc-tag-container-tag-${i} svg:hover{
                             color: #fff;
                         }
                         #${this.uId} .fgc-tag-container-tag-${i} .el-icon:hover{
                             background-color: ${!isDark ? color : Forguncy.ColorHelper.UpdateTint(color, 30, 128)};
                         }
`
            }

            this.getStyleContainerElement("tags").innerHTML = html;

            const template = `
<el-scrollbar class="fgc-tag-scrollbar ${WordWrap ? 'fgc-tag-word-wrap' : ''}">
    <div class="fgc-tag-container">
            <el-tag
                ref="tags"
                v-for="item in items"
                :key="item.key"
                :size="size"
                :effect="effect"
                :hit="hit"
                :closable="!disabled&&!readonly"
                :disableTransitions="disableTransitions"
                :class="item.class"
                @click="onClick"
                @close="onClose"
                :color="item.background"
                :style="{marginRight:item.itemSpace+'px'${marginBottom},cursor:item.cursor, borderColor:item.border, color: item.color}"
                >
                {{ item.label }}
             </el-tag>
            <el-input
                  :size="addButtonSize"
                  class="el-input-new-tag"
                  v-if="inputVisible"
                  v-model="inputValue"
                  ref="saveTagInput"
                  @keyup.enter.native="handleInputConfirm"
                  @blur="handleInputConfirm"
                  :style="{display:!disabled&&!readonly&&allowAdd?'':'none', flexShrink:0,width:addButtonWidth+'px'${marginBottom}}"
                >
            </el-input>
            <el-button clickable="true" :size="addButtonSize" :style="{display:!disabled&&!readonly&&allowAdd?'':'none',flexShrink:0, width:addButtonWidth+'px'${marginBottom}}" v-else class="el-button-new-tag" @click="showInput">{{addButtonText}}</el-button>
    </div>
</el-scrollbar>
`;
            
            const option = {
                template,
                data() {
                    return {
                        items: [],
                        size: cellType.size === "mini" ? "small" : cellType.size,
                        effect: cellType.effect,
                        hit: !!cellType.hit,
                        disabled: false,
                        readonly: false,
                        itemSpace: cellType.itemSpace,
                        allowAdd: cellType.allowAdd,
                        disableTransitions: !!cellType.disableTransitions,
                        inputVisible: false,
                        addButtonWidth: cellType.addButtonSettings.width,
                        addButtonText: cellType.addButtonSettings.text ?? "",
                        addButtonSize: this.getAddButtonSize(cellType.size),
                        inputValue: ''
                    };
                },
                methods: {
                    getValue() {
                        return this.items ? this.items.map(i => i.label).join(cellType.separator) : null;
                    },
                    setValue(value) {
                        const hasAddButton = !this.readonly && !this.disabled && cellType.allowAdd;
                        if (value || value === 0) {
                            let strItems = (value + "").split(cellType.separator).filter(i => i !== "");
                            if (cellType.distinct) {
                                strItems = this.distinct(strItems);
                            }
                            this.items = strItems.map((value, index, array) => <item>{
                                key: cellType.distinct ? value : index,
                                label: value,
                                class: `fgc-tag-container-tag-${index % colorList.length}`,
                                background: this.getBackground(colorList[index % colorList.length].color),
                                color: this.getColor(colorList[index % colorList.length].color),
                                border: this.getBorder(colorList[index % colorList.length].color),
                                itemSpace: index !== array.length - 1 ? cellType.itemSpace : (hasAddButton ? cellType.addButtonSettings.space : 0),
                                cursor: cellType.ClickCommand?.Commands?.length ? "pointer" : ""
                            });
                        }
                        else {
                            this.items = [];
                        }
                    },
                    onClick(event: PointerEvent) {
                        if (cellType.ClickCommand?.Commands?.length) {
                            const index = this.getEventIndex(event);
                            if (index >= 0) {
                                const initValue = {};
                                initValue[cellType.ClickCommand.ParamProperties["itemName"]] = this.items[index].label;
                                self.executeCustomCommandObject(cellType.ClickCommand, initValue);
                            }
                        }
                    },
                    onClose(event: PointerEvent) {
                        const index = this.getEventIndex(event);
                        if (index >= 0) {
                            this.items.splice(index, 1);
                            self.commitValue();
                        }
                    },
                    getAddButtonSize(size) {

                        switch (size) {
                            case "large":
                                return "default";

                            default:
                                return "small";
                        }
                    },
                    getEventIndex(event: PointerEvent) {
                        const clickItem = $(<any>event.target, self.getContainer()).parents(".el-tag")[0];

                        const tags = $(".el-tag", self.getContainer());

                        for (let i = 0; i < tags.length; i++) {
                            const item = tags[i];
                            if (item === clickItem) {
                                return i;
                            }
                        }
                    },
                    distinct(arr): any[] {
                        const result = [];
                        const obj = {};
                        for (const i of arr) {
                            if (obj[i]) {
                                continue;
                            }
                            obj[i] = true;
                            result.push(i);
                        }
                        return result;
                    },
                    getBorder(color) {
                        color = Forguncy.ConvertToCssColor(color);
                        if (this.hit) {
                            return color;
                        }
                        switch (this.effect) {
                            case "dark":
                                return color;
                            case "light":
                                return Forguncy.ColorHelper.UpdateTint(color, 0.8, 255);
                            case "plain":
                                return Forguncy.ColorHelper.UpdateTint(color, 0.6, 255);
                            default:
                        }
                    },
                    getColor(color) {
                        color = Forguncy.ConvertToCssColor(color);
                        switch (this.effect) {
                            case "dark":
                                return "#fff";
                            case "light":
                                return color;
                            case "plain":
                                return color;
                            default:
                        }
                    },
                    getBackground(color) {
                        color = Forguncy.ConvertToCssColor(color);
                        switch (this.effect) {
                            case "dark":
                                return color;
                            case "light":
                                return Forguncy.ColorHelper.UpdateTint(color, 0.9, 255);
                            case "plain":
                                return "#fff";
                            default:
                        }
                    },
                    showInput() {
                        this.inputVisible = true;
                        this.$nextTick(_ => {
                            this.$refs.saveTagInput.$refs.input.focus();
                        });
                    },

                    handleInputConfirm() {
                        const inputValue = this.inputValue;
                        if (inputValue) {
                            const currentValue = this.getValue();
                            this.setValue(currentValue ? currentValue + cellType.separator + inputValue : inputValue);
                            self.commitValue();
                        }
                        this.inputVisible = false;
                        this.inputValue = '';
                    },
                    disable() {
                        this.disabled = true;
                    },
                    enable() {
                        this.disabled = false;
                    },
                    setReadOnly(value) {
                        this.readonly = value;
                    },
                }
            };
            this.createVueApp(option);
            super.onPageLoaded(info);
        }

        public disable() {
            super.disable();
            this.refreshClickable();
        }

        public enable() {
            super.enable();
            this.refreshClickable();
        }

        public setReadOnly(value) {
            super.setReadOnly(value);
            this.refreshClickable();
        }

        refreshClickable() {
            this.getContainer()?.attr("clickable", this.clickable() ? true : "");
            this.getContainer()?.css("cursor", this.clickable() ? "default" : "");
        }

        protected clickable(): boolean {
            const cellType = this.cellType;
            return (!this.isReadOnly() && !this.isDisabled()) || (cellType?.ClickCommand?.Commands && cellType.ClickCommand.Commands.length > 0);
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.Tag, ElementUI", ElementCellTypes.TagCellType);