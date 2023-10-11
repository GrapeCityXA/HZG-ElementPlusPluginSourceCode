/// <reference path = "ELCommandBase.ts" />
namespace ElementCommands {
    interface ShowMessageParam {
        Center?: boolean;
        Duration?: string;
        Message?: string;
        Offset?: string;
        ShowClose?: boolean;
        Type?: string;
    }

    export class ShowMessage extends ElementCommandBase {

        public execute() {
            const { Center, Duration, Message, Offset, ShowClose, Type } = <ShowMessageParam>this.CommandParam;

            const duration = this.evaluateFormula(Duration) ?? 0;
            const offset = this.evaluateFormula(Offset) ?? 20;
            const type = this.evaluateFormula(Type);
            const message = this.evaluateFormula(Message) ?? "";
            const center = !!Center;
            const showClose = !!ShowClose;

            window.ElementPlus.ElMessage({
                duration,
                message,
                type,
                center,
                showClose,
                offset: Number(offset),
            });
        }
    }
}

Forguncy.Plugin.CommandFactory.registerCommand("ElementUI.Commands.ShowMessage, ElementUI", ElementCommands.ShowMessage);