/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    enum AvatarShape {
        circle,
        square
    }

    // test webhook

    enum AvatarFit {
        fill,
        contain,
        cover,
        none,
        "scale-down"
    }

    interface IAvatarCellTypeParam {
        icon: Icon;
        src: string;
        fit: AvatarFit;
        shape: AvatarShape;
        CommandList: object[];
        badge: string;
        showSystemAvatar: boolean;
    }

    export class AvatarCellType extends ElementCellTypeBase<IAvatarCellTypeParam> {

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;
            const self = this;
            const badge = this.evaluateFormula(cellType.badge);

            this.addCustomClass("el-avatar-custom");

            if (cellType.shape === AvatarShape.circle) {
                this.addCustomClass("el-avatar-circle-custom");
            }

            const option = {
                template: `
<el-badge :value="badge" :hidden="!badge">
        <el-avatar
            :src="src"
            :icon="icon"            
            :fit="fit"
            :text="text"
            :shape="shape"
            :size="size">{{text}}
        </el-avatar>
    </el-badge>`,
                data() {
                    return {
                        icon: "",
                        src: cellType.src,
                        fit: AvatarFit[cellType.fit],
                        shape: AvatarShape[cellType.shape],
                        size: null,
                        badge: badge,
                        text: undefined,
                    };
                },
                mounted() {
                    this.size = Math.min(self.vueContainer.width(), self.vueContainer.height());

                    const hasCommandList = cellType.CommandList && cellType.CommandList.length;

                    const avatarElement = $(".el-avatar", self.vueContainer);

                    if (hasCommandList) {
                        avatarElement.css("cursor", "pointer");
                        avatarElement.click(() => (self.executeCommand(cellType.CommandList)));
                    }

                    self.fontDom = $(".el-avatar", self.getContainer());
                },
                methods: {
                    getValue() {
                        return this.src;
                    },
                    setValue(value) {
                        if (!value) {
                            this.src = self._defaultSrc;
                            this.text = undefined;
                            return;
                        }
                        if (cellType.showSystemAvatar) {
                            this.src = Forguncy.Helper.SpecialPath.getBaseUrl() + "Account/UserImage?getDefault=false&userName=" + value;
                            this.text = value;
                            return;
                        }
                        if (value && FileHelper.IsAttachment(value)) {
                            value = FileHelper.GetUploadImageSrc(value);
                        }
                        else if (value) {
                            const lowCase = (value + "").toLowerCase();
                            if (lowCase.indexOf("http://") !== 0 && lowCase.indexOf("https://") !== 0) {
                                this.text = value;
                            }
                        }
                        this.src = value;
                    }
                }
            };
            this.createVueApp(option);

            this.onDependenceCellValueChanged(() => {
                if (this.isFormula(cellType.badge)) {
                    this.vue.badge = this.evaluateFormula(cellType.badge);
                }
            });
            this.getContainer().css("overflow", "");
            super.onPageLoaded(info);

            super.getIconComponent(cellType.icon, (icon, result: IconResult) => {
                if (result.isSvg) {
                    self._defaultSrc = getBase64FromSvgElement(result.icon);
                } else {
                    self._defaultSrc = result.icon;
                }
                if (this.isEmpty(this.vue.src)) {
                    this.vue.src = self._defaultSrc;
                }
            });
        }
        private _defaultSrc: string;
        public onWindowResized() {
            this.vue.size = Math.min(this.vueContainer.width(), this.vueContainer.height());
        }
        protected clickable(): boolean {
            const cellType = <IAvatarCellTypeParam>this.CellElement.CellType;
            return cellType.CommandList && cellType.CommandList.length > 0;
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.AvatarCellType, ElementUI", ElementCellTypes.AvatarCellType);