/// <reference path = "ELCommandBase.ts" />
namespace ElementCommands {

    interface ShowNotificationParam {
        Duration?: string;
        Message?: string;
        Context?: string;
        Offset?: string;
        ShowClose?: boolean;
        Title?: string;
        Command?: Forguncy.Plugin.ICustomCommandObject;
        Type?: string;
        Position?: string;
    }

    export class ShowNotification extends ElementCommandBase {

        public execute() {
            const {
                Title,
                Duration,
                Message,
                Context,
                Position,
                ShowClose,
                Type,
                Offset,
                Command
            } = <ShowNotificationParam>this.CommandParam;
            const duration = this.evaluateFormula(Duration) ?? 0;
            const title = this.evaluateFormula(Title);
            const offset = Number(this.evaluateFormula(Offset)) ?? 0;
            const type = this.evaluateFormula(Type);
            const message = this.evaluateFormula(Message) ?? type;
            const context = this.evaluateFormula(Context);
            const position = this.evaluateFormula(Position) ?? "top-right";
            const clickCommands = Command;
            const showClose = !!ShowClose;
            let onClick = null;
            let customClass;
            let noticy = null;
            if (clickCommands?.Commands && clickCommands.Commands.length > 0) {
                onClick = () => {
                    const param = {};
                    param[clickCommands.ParamProperties["title"]] = title;
                    param[clickCommands.ParamProperties["message"]] = message;
                    param[clickCommands.ParamProperties["context"]] = context;
                    this.executeCustomCommandObject(clickCommands, param, "Notice");
                    noticy.close();
                };
                customClass = "fgc-notice-element-clickable";
            }

            noticy = window.ElementPlus.ElNotification({
                title: title,
                message: message,
                type: type,
                duration: duration,
                position: position,
                showClose: showClose,
                offset: offset,
                onClick: onClick,
                customClass: customClass
            });
        }
    }
}

Forguncy.Plugin.CommandFactory.registerCommand("ElementUI.Commands.ShowNotification, ElementUI", ElementCommands.ShowNotification);
