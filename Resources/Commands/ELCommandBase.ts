namespace ElementCommands {
    export class ElementCommandBase extends Forguncy.Plugin.CommandBase {
        constructor() {
            (<any>window["elementUITheme"])?.UpdateCssAndAppend();
            super();
        }
    }
}