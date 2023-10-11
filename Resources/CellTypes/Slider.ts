/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    type TMarks = { Value: number, Label: any }[];
    
    interface ISliderCellTypeParam {
        min: any;
        max: any;
        step: any;
        layout: "horizontal" | "vertical";
        formatTooltipStr: string;
        IsDisabled: boolean;
        showInput: boolean;
        showInputControls: boolean;
        showStops: boolean;
        showTooltip: boolean;
        range: boolean;
        Marks: TMarks
    }

    export class SliderCellType extends InputCellTypeBase<ISliderCellTypeParam> {

        public convertToObjectMarks(marks: TMarks){
            return marks.reduce((obj, item) => (obj[item.Value] = item.Label, obj), {});
        }
        
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;

            const self = this;

            const isVertical = cellType.layout === "vertical";

            this.getContainer().css("overflow", "");

            const marks =this.convertToObjectMarks(cellType.Marks);

            const option = {
                template: `<div id="${this.uId}-el-slider" :class="['fgc-slider', 'hasMarks',{ 'is-vertical': vertical }]">
                               <el-slider
                                   v-model="value" :min="min" :max="max" :disabled="disabled" :showTooltip="showTooltip"
                                   :step="step" :show-input="showInput" :show-input-controls="showInputControls"
                                   :show-stops="showStops" :range="range" :vertical="vertical" :height="height"
                                   :formatTooltip="formatTooltip" :marks="marks"
                                   @change="handleChange"
                               />
                            </div> 
                          `,
                data() {
                    return {
                        value: null,
                        height: null,
                        min: self.calcNumber(cellType.min),
                        max: self.calcNumber(cellType.max),
                        disabled: undefined,
                        step: self.calcNumber(cellType.step),
                        showTooltip: !!cellType.showTooltip,
                        showInput: cellType.showInput,
                        showInputControls: !!cellType.showInputControls,
                        showStops: cellType.showStops,
                        range: cellType.range,
                        vertical: isVertical,
                        marks
                    };
                },
                mounted() {
                    if (this.vertical) {

                        const element = document.getElementById(`${self.uId}-el-slider`);

                        const height = (element.offsetHeight - 24) - (this.showInput ? 58 : 0);

                        this.height = `${height}px`;
                    }
                },
                methods: {
                    getValue() {
                        if (Array.isArray(this.value)) {
                            return this.value.join(",");
                        }
                        return this.value;
                    },
                    setValue(value) {
                        if (self.isEmpty(value)) {
                            this.value = undefined;
                        }
                        else if (typeof value === "number") {
                            this.value = value;
                        } else if (typeof value === "string") {
                            this.value = value.indexOf(',') !== -1 ?
                                value.split(",").map(i => Number(i)) :
                                Number(value);
                        }
                    },
                    disable() {
                        this.disabled = true;
                    },
                    enable() {
                        this.disabled = false;
                    },
                    handleChange() {
                        self.commitValue();
                    },
                    formatTooltip(value) {
                        if (cellType.formatTooltipStr) {
                            self.setContextVariableValue("Value", value);
                            try {
                                return self.evaluateFormula(cellType.formatTooltipStr);
                            }
                            finally {
                                self.clearContextVariableValue("Value");
                            }
                        }
                        return value;
                    }
                }
            };
            this.onDependenceCellValueChanged(() => {
                if (this.isFormula(cellType.min)) {
                    this.vue.min = this.calcNumber(cellType.min);
                }
                if (this.isFormula(cellType.max)) {
                    this.vue.max = this.calcNumber(cellType.max);
                }
                if (this.isFormula(cellType.step)) {
                    this.vue.step = this.calcNumber(cellType.step);
                }
            });
            this.createVueApp(option);
            super.onPageLoaded(info);
        }

        public SetMinValue(value) {
            this.vue.min = Number(value);
        }
        public SetMaxValue(value) {
            this.vue.max = Number(value);
        }
        
        public SetMarks(marks){
            this.vue.marks = this.convertToObjectMarks(marks?.$values || []);
        }

        public GetSelectedRange() {
            const value = this.vue.value;
            if (value instanceof Array) {
                return {
                    StartValue: value[0],
                    EndValue: value[1]
                };
            }
            return {};
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.SliderCellType, ElementUI", ElementCellTypes.SliderCellType);