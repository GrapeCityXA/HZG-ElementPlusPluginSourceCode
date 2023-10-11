/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    enum RateDisplayContentType {
        None,
        Text,
        Scope
    }

    interface IRateCellTypeParam {
        max: number;
        showText: boolean;
        showScore: boolean;
        texts: { label: string }[];
        colors: { color: string }[];
        voidColor: string;
        disabledVoidColor: string;
        allowHalf: boolean;
        lowThreshold: number;
        highThreshold: number;
        DisplayContentType: RateDisplayContentType;
    }

    export class RateCellType extends ElementCellTypeBase<IRateCellTypeParam> {
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;

            const self = this;

            const { DisplayContentType } = cellType;

            const option = {
                template: `
                        <div style="display:flex;align-items:center;width:100%;height:100%">
                            <el-rate
                                v-model="value"
                                :max="max"
                                :disabled="disabled"
                                :show-text="showText"
                                :show-score="showScore"
                                :texts="texts"
                                :colors="colors"
                                :void-color="voidColor"
                                :disabled-void-color="disabledVoidColor	"
                                :allow-half="allowHalf"
                                :low-threshold="lowThreshold"
                                :high-threshold="highThreshold"
                                @change="handleChange"
                           />
                       </div>
                          `,
                data() {
                    return {
                        value: null,
                        max: cellType.max,
                        disabled: undefined,
                        allowHalf: cellType.allowHalf,
                        lowThreshold: cellType.lowThreshold,
                        highThreshold: cellType.highThreshold,
                        voidColor: Forguncy.ConvertToCssColor(cellType.voidColor),
                        disabledVoidColor: Forguncy.ConvertToCssColor(cellType.disabledVoidColor),
                        texts: cellType.texts?.map(({ label }) => label),
                        colors: cellType.colors?.map(({ color }) => Forguncy.ConvertToCssColor(color)),
                        showText: DisplayContentType === RateDisplayContentType.Text,
                        showScore: DisplayContentType === RateDisplayContentType.Scope,
                    };
                },
                mounted() {
                    self.fontDom = $(".el-rate__text", self.getContainer());
                },
                methods: {
                    getValue() {
                        return this.value;
                    },
                    setValue(value: number) {
                        this.value = Math.min(value, this.max);
                    },
                    setReadOnly(readOnly) {
                        this.disabled = readOnly;
                    },
                    handleChange() {
                        self.commitValue();
                    }
                }
            };
            this.createVueApp(option);
            super.onPageLoaded(info);
        }

        public setReadOnly(value) {
            super.setReadOnly(value);
            this.refreshClickable();
        }

        refreshClickable() {
            if (this.vue?.$el) {
                $(this.vue.$el).parent()?.attr("clickable", this.clickable() ? true : "");
            }
        }

        protected clickable(): boolean {
            return !this.isReadOnly() && !this.isDisabled();
        }
    }

}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.RateCellType, ElementUI", ElementCellTypes.RateCellType);