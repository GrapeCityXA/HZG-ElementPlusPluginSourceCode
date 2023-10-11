/// <reference path = "ELCommandBase.ts" />
namespace ElementCommands {

    enum ShowMessBoxInputType {
        text,
        textarea,
        password,
    }

    interface AdvancedSetting {
        ShowInput?: boolean;
        InputPlaceholder?: any;
        InputType?: ShowMessBoxInputType;
        InputBoxResult?: string;
        Center?: boolean;
        DistinguishCancelAndClose?: boolean;
        RoundButton?: boolean;
        CloseOnClickModal?: boolean;
        CloseOnPressEscape?: boolean;
    }

    interface ShowMessageBoxParam {
        Title?: string;
        Message?: string;
        Type?: string;
        ShowConfirmButton?: boolean;
        ConfirmButtonText?: string;
        ShowCancelButton?: boolean;
        CancelButtonText?: string;
        ShowClose?: boolean;
        DialogResult?: string;
        AdvancedSettings: AdvancedSetting;
    }

    type TAction = "confirm" | "cancel" | "close";

    export class ShowMessageBox extends ElementCommandBase {

        public execute() {
            const {
                Title, Message, ShowClose, Type,
                ShowConfirmButton, ConfirmButtonText,
                ShowCancelButton, CancelButtonText,
                DialogResult,
                AdvancedSettings: {
                    CloseOnClickModal, CloseOnPressEscape,
                    ShowInput, InputPlaceholder, InputType, Center, RoundButton,
                    InputBoxResult, DistinguishCancelAndClose
                }
            } = <ShowMessageBoxParam>this.CommandParam;

            const title = this.evaluateFormula(Title);
            const message = this.evaluateFormula(Message);
            const type = this.evaluateFormula(Type);

            const showConfirmButton = !!ShowConfirmButton;
            const confirmButtonText = this.evaluateFormula(ConfirmButtonText);

            const showCancelButton = !!ShowCancelButton;
            const cancelButtonText = this.evaluateFormula(CancelButtonText);

            const showClose = !!ShowClose;
            const closeOnClickModal = !!CloseOnClickModal;
            const closeOnPressEscape = !!CloseOnPressEscape;

            const showInput = !!ShowInput;
            const inputPlaceholder = this.evaluateFormula(InputPlaceholder);
            const inputType = ShowMessBoxInputType[InputType];
            const center = !!Center;
            const roundButton = !!RoundButton;

            const distinguishCancelAndClose = !!DistinguishCancelAndClose;

            const callback = (parameter: TAction | { value: string; action: TAction }, instance) => {
                if (typeof parameter === "string") {
                    Forguncy.CommandHelper.setVariableValue(DialogResult, parameter);
                } else {
                    Forguncy.CommandHelper.setVariableValue(DialogResult, parameter.action);
                    Forguncy.CommandHelper.setVariableValue(InputBoxResult, parameter.value);
                }
                this.CommandExecutingInfo.suspend = false;
            };
            Forguncy.PageBuilder.hidePageLoadingCover();
            window.ElementPlus.ElMessageBox({
                title,
                message,
                type,
                showClose,
                showConfirmButton,
                confirmButtonText,
                showCancelButton,
                cancelButtonText,
                closeOnClickModal,
                closeOnPressEscape,
                showInput,
                inputPlaceholder,
                inputType,
                center,
                roundButton,
                callback,
                distinguishCancelAndClose,
                customClass: "fgc-el-message-box"
            });
            this.CommandExecutingInfo.suspend = true;
        }
    }

    window.addEventListener("popstate", () => $(".fgc-el-message-box").parents(".el-overlay.is-message-box").remove());
}

Forguncy.Plugin.CommandFactory.registerCommand("ElementUI.Commands.ShowMessageBox, ElementUI", ElementCommands.ShowMessageBox);
