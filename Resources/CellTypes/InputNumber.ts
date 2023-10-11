/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    interface IInputNumberCellTypeParam {
        min?: any;
        max?: any;
        step: any;
        stepStrictly: boolean;
        precision: number;
        IsDisabled: boolean;
        controlsPosition: boolean;
        placeholder: string;
        controls: boolean;
    }

    export class InputNumberCellType extends InputCellTypeBase<IInputNumberCellTypeParam> {

        public inputValue;

        public setValueToElement(jelement: JQuery, value: any) {

            if (value === null) {
                return this.vue?.setValue(undefined);
            }

            return super.setValueToElement(jelement, value);
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const self = this;
            const cellType = this.cellType;

            this.addCustomClass("el-input-number-custom");
            if (cellType.controlsPosition) {
                this.addCustomClass("el-input-number-custom-right");
            }

            const option = {
                template: `
<el-input-number v-model="input"
:min="min" :max="max" :step="step" :step-strictly="stepStrictly" :precision="precision" :disabled="disabled"
:controls-position="controlsPosition" :placeholder="placeholder" :controls="controls"
@blur="blurHandler"
@change="changedHandler"
ref="elInput"
>
</el-input-number>
`,
                data() {
                    return {
                        input: undefined,
                        min: self.calcNumber(cellType.min),
                        max: self.calcNumber(cellType.max),
                        step: self.calcNumber(cellType.step),
                        stepStrictly: cellType.stepStrictly,
                        precision: cellType.precision,
                        disabled: cellType.IsDisabled,
                        controlsPosition: cellType.controlsPosition === true ? "right" : "",
                        placeholder: cellType.placeholder,
                        controls: cellType.controls ? true : false,
                    };
                },
                methods: {
                    getValue() {
                        return this.input;
                    },
                    setValue(value) {
                        if (value !== null && value !== undefined && value !== "") {
                            this.input = Number(value);
                        }
                        else {
                            this.input = undefined;
                        }
                        if (self.isEmpty(self.inputValue)) {
                            self.inputValue = this.input;
                        }
                    },
                    disable() {
                        this.disabled = true;
                    },
                    enable() {
                        this.disabled = false;
                    },
                    blurHandler() {
                        self.validate();
                    },
                    changedHandler() {
                        self.validate();
                    }
                },
                mounted() {
                    const inputJqel = $("input", self.vueContainer);
                    self.fontDom = inputJqel;

                    inputJqel.on("input", () => {
                        self.inputValue = inputJqel.val();
                    });

                    inputJqel.on("blur", () => {
                        if (self.inputValue === "") {
                            this.input = undefined;
                        }
                    });

                    Forguncy.FocusMoveHelper.AllowMoveFocusByEnterKey(inputJqel, false);
                },
                watch: {
                    input(value) {
                        if (!this.stepStrictly) {
                            self.commitValue();
                            return;
                        }
                        if (value % this.step === 0) {
                            self.commitValue();
                        }
                    }
                }
            };

            this.createVueApp(option);

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
            super.onPageLoaded(info);
        }
        public SetMinValue(value) {
            this.vue.min = Number(value);
        }
        public SetMaxValue(value) {
            this.vue.max = Number(value);
        }
        public SetStep(value) {
            this.vue.step = Number(value);
        }
        public Focus() {
            this.vue.$refs.elInput.focus();
        }
    }

}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.InputNumberCellType, ElementUI", ElementCellTypes.InputNumberCellType);