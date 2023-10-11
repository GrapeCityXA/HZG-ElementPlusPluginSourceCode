/// <reference path = "Base.ts" />
namespace ElementCellTypes {
    interface IInputCellTypeParam {
        type: "text" | "password" | "textarea";
        maxlength?: object;
        showWordLimit: boolean;
        placeholder: string;
        clearable: boolean;
        IsDisabled: boolean;
        prefixIcon: Icon;
        suffixIcon: Icon;
        ReadOnly: boolean;
        resize: string;
    }

    export class InputCellType extends InputCellTypeBase<IInputCellTypeParam> {
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const self = this;

            this.addCustomClass("el-input-custom");
            const cellType = this.cellType;
            const option = {
                template: `
<el-input v-model="input"
:type="type" :maxlength="maxlength" :show-word-limit="showWordLimit" :readonly="readonly"
:placeholder="placeholder" :clearable="clearable" :showPassword="showPassword" :resize="resize"
ref="elInput"
:disabled="disabled" :suffixIcon="suffixIcon" :prefixIcon="prefixIcon"
@blur="handleBlur"
@change="handleChange">
</el-input>
`,

                data() {
                    return {
                        input: null,
                        type: cellType.type === "password" ? "text" : cellType.type,
                        maxlength: self.getMaxLength(),
                        showWordLimit: cellType.showWordLimit,
                        placeholder: cellType.placeholder,
                        clearable: cellType.clearable,
                        showPassword: cellType.type === "password",
                        disabled: cellType.IsDisabled,
                        prefixIcon: "",
                        suffixIcon: "",
                        readonly: undefined,
                        resize: cellType.resize,
                    };
                },
                methods: {
                    handleBlur() {
                        self.validate();
                    },
                    handleChange() {
                        self.commitValue();
                    },
                    getValue() {
                        return this.input;
                    },
                    setValue(value) {
                        this.input = value;
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
                },
                mounted() {
                    const refs = this.$refs?.elInput?.$refs;
                    const input = refs?.input ?? refs?.textarea;

                    if (input) {
                        const inputJqel = $(input);

                        self.fontDom = inputJqel;
                        if (cellType.type !== "textarea") {
                            Forguncy.FocusMoveHelper.AllowMoveFocusByEnterKey(inputJqel, false);
                        }
                    }
                }
            };

            this.createVueApp(option);

            this.onDependenceCellValueChanged(() => {
                if (this.isFormula(cellType.maxlength)) {
                    this.vue.maxlength = this.getMaxLength();
                }
            });
            super.onPageLoaded(info);

            super.getIconComponent(cellType.prefixIcon, icon => this.vue.prefixIcon = icon);
            super.getIconComponent(cellType.suffixIcon, icon => this.vue.suffixIcon = icon);
        }

        public getMaxLength() {
            const maxLength = (<IInputCellTypeParam>this.CellElement.CellType).maxlength;
            const value = this.calcNumber(maxLength);
            if (isNaN(value) || value < 1) {
                return null;
            }
            return value;
        }
        public Focus() {
            this.vue.$refs.elInput.focus();
        }
        public Select() {
            this.vue.$refs.elInput.select();
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.InputCellType, ElementUI", ElementCellTypes.InputCellType);