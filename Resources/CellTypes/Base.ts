namespace ElementCellTypes {
    enum CultureEnum {
        CN = "ElementPlusLocaleZhCn",
        EN = "ElementPlusLocaleEn",
        KR = 'ElementPlusLocaleKo',
        JA = 'ElementPlusLocaleJa'
    }

    export interface Icon {
        Name: string;
        BuiltIn?: boolean;
        UseCellTypeForeColor?: boolean;
        Color?: string;
    }
    export const iconComponent = Vue.defineComponent({
        template: `<el-icon v-html="icon" />`,
        props: {
            icon: String
        }
    });
    export const imageComponent = Vue.defineComponent({
        template: `<el-image class="el-icon" fit="contain" :src="src" />`,
        props: {
            src: String
        }
    });

    export const getBase64FromSvgElement = (svgStr: string) => {
        return 'data:image/svg+xml;base64,' + btoa(unescape(encodeURIComponent(svgStr)));
    };

    export class ElementCellTypeBase<T = object> extends Forguncy.Plugin.CellTypeBase {
        protected uId;
        public vue;
        private _vueApp;
        private _fontDom;
        private _fontSelector: string | string[];
        private _styleContainerIdSet: Set<string> = new Set<string>();
        public vueContainer: JQuery;
        public cellType: T = <any>this.CellElement.CellType;

        public get fontDom() {
            return this._fontDom;
        }

        public set fontDom(value) {
            this._fontDom = value;
            this.setFontToDom();
        }

        public get fontSelector() {
            return this._fontSelector;
        }

        public set fontSelector(value) {
            this._fontSelector = value;
            this.setFontStyleElement();
        }

        public createVueApp(option: any) {
            const self = this;

            if (option.beforeCreate) {
                const beforeCreate = option.beforeCreate;

                option.beforeCreate = function () {
                    self.vue = this;
                    beforeCreate();
                };
            } else {
                option.beforeCreate = function () {
                    self.vue = this;
                };
            }

            const vueApp = Vue.createApp(option);
            vueApp.use(window.ElementPlus, {
                locale: window[CultureEnum[Forguncy.RS.Culture]],
            });
            vueApp.mount(`#${this.uId}`);
            this._vueApp = vueApp;
        }

        protected _cellStyle: Forguncy.Plugin.StyleMetaData = {};
        public setFontStyle(styleInfo: Forguncy.Plugin.StyleMetaData) {
            const currentStyleInfo = this._cellStyle;
            currentStyleInfo.FontFamily = styleInfo.FontFamily;
            currentStyleInfo.FontStyle = styleInfo.FontStyle;
            currentStyleInfo.FontSize = styleInfo.FontSize;
            currentStyleInfo.FontWeight = styleInfo.FontWeight;
            currentStyleInfo.Foreground = styleInfo.Foreground;
            currentStyleInfo.Strikethrough = styleInfo.Strikethrough;
            currentStyleInfo.Underline = styleInfo.Underline;
            this.setFontToDom();
            this.setFontStyleElement();
        }

        protected getStyles() {
            const styleInfo = this._cellStyle ?? {};

            const styles = {
                "font-family": styleInfo.FontFamily ?? "",
                "font-size": styleInfo.FontSize && styleInfo.FontSize > 0 ? styleInfo.FontSize : "",
                "font-style": styleInfo.FontStyle ? styleInfo.FontStyle.toLowerCase() : "",
                "font-weight": styleInfo.FontWeight ? styleInfo.FontWeight.toLowerCase() : "",
            };
            const textDecoration = [];
            if (styleInfo.Underline) {
                textDecoration.push("underline");
            }
            if (styleInfo.Strikethrough) {
                textDecoration.push("line-through");
            }

            styles["text-decoration"] = textDecoration.join(" ");
            styles["color"] = styleInfo.Foreground ? Forguncy.ConvertToCssColor(styleInfo.Foreground) : "";

            return styles;
        }

        public getStyleContainerElement(prefix?: string) {
            const id = `${this.uId}-style-${prefix}`;
            this._styleContainerIdSet.add(id);

            const el = document.getElementById(id);
            if (el) {
                return el;
            }

            const style = document.createElement("style");
            style.type = "text/css";
            style.id = id;
            document.getElementsByTagName("head")[0].appendChild(style);

            return style;
        }

        protected setFontStyleElement() {
            if (!this._fontSelector) {
                return;
            }

            const styles = this.getStyles();

            let str = "";

            Object.keys(styles).forEach((key) => {
                let value = styles[key];
                if (!value) {
                    return;
                }
                if (key === "font-size") {
                    value += "px";
                }
                str += `${key}:${value} !important;`;
            });

            const selectorArray = typeof this._fontSelector === 'string' ? [this.fontSelector] : this.fontSelector;

            const selector = (<string[]>selectorArray).map(item => `.${this.uId} ${item}`).join(",");

            this.getStyleContainerElement("font").innerHTML = `${selector}{${str}}`;
        }

        protected setFontToDom() {
            this.fontDom?.css(this.getStyles());
        }

        public static generateID(prefix = "fgc-el") {
            return `${prefix}-${new Date().getTime().toString(36)}-${Math.random().toString(36).slice(2)}`;
        }

        public createContent() {
            ElementTheme.updateCssAndAppend();

            this.uId = ElementCellTypeBase.generateID();
            // 外边加一层Div，设置ID，否则在输入值是ValidateTooltip不消失
            const container = $(`<div id=${this.ID}><div id ="${this.uId}" class="${this.uId}" style='width:100%;height:100%'></div></div>`)
                .css("width", "100%")
                .css("height", "100%");

            this._cellStyle = this.CellElement.StyleInfo;
            this.vueContainer = container;
            return container;
        }

        public addCustomClass(className) {
            this.getContainer().addClass(className);
        }

        private loop() {
            return undefined;
        }

        public setValueToElement(jelement: JQuery, value: any) {
            return (this.vue?.setValue || this.loop)(value);
        }

        public getValueFromElement() {
            return (this.vue?.getValue || this.loop)();
        }
        public disable() {
            super.disable();
            this.onIsDisabledChanged();
        }

        public enable() {
            super.enable();
            this.onIsDisabledChanged();
        }

        private onIsDisabledChanged() {
            if (!this.vue) {
                return;
            }

            if (this.isDisabled()) {
                this.vue.disable?.();
            } else {
                this.vue.enable?.();
            }
        }

        public setReadOnly(value: boolean): void {
            super.setReadOnly(value);
            return (this.vue?.setReadOnly || this.loop)(this.isReadOnly());
        }

        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            this.setFontToDom();
            this.setReadOnly(info.isReadOnly);

            if (info.isDisabled) {
                this.disable();
            }
            else {
                this.enable();
            }
            if (this.vue?.setValue) {
                this.vue.setValue(info.value);
            }
        }
        public calcNumber(value) {
            if (value === 0) {
                return value;
            }
            if (value) {
                if (typeof (value) === "string") {
                    value = this.evaluateFormula(value);
                }
                return Number(value);
            }
        }

        public isFormula(value) {
            return !this.isEmpty(value) && value.toString()[0] === "=";
        }

        public destroy() {
            this._vueApp?.unmount();
            this._fontDom = null;
            this.fontSelector = null;
            this._styleContainerIdSet.forEach(eid => document.getElementById(eid)?.remove());
            super.destroy();
        }

        public getIconComponent(icon: Icon, callback: Function) {
            this.getIcon(icon, (result) => {
                if (result.isSvg) {
                    callback(Vue.h(iconComponent, { icon: result.icon }), result);
                }
                else {
                    callback(Vue.h(imageComponent, { src: result.icon }), result);
                }
            });
        }

        public getIcon(icon: Icon, callback: (result: IconResult) => void): void {
            if (icon) {
                if (typeof (icon) === "string") {
                    let src = <string>icon;
                    if (ElementCellTypeBase.isAttachment(src)) {
                        src = Forguncy.Helper.SpecialPath.getUploadImageFolderPathInServer() + encodeURIComponent(src);
                    }
                    callback({
                        icon: src,
                        isSvg: false
                    });
                }
            }
            if (!icon?.Name) {
                return;
            }
            let src: string;
            if (icon.BuiltIn) {
                src = Forguncy.Helper.SpecialPath.getBuiltInImageFolderPath() + icon.Name;
            }
            else {
                src = Forguncy.Helper.SpecialPath.getImageEditorUploadImageFolderPath() + encodeURIComponent(icon.Name);
            }
            if (Forguncy.ImageDataHelper.IsSvg(src)) {
                $.get(src, (data) => {
                    const svg = $(data.documentElement);
                    Forguncy.ImageHelper.preHandleSvg(svg, icon.UseCellTypeForeColor ? "currentColor" : icon.Color);
                    callback({
                        "icon": svg[0].outerHTML,
                        isSvg: true
                    });
                });
            }
            else {
                callback({
                    icon: src,
                    isSvg: false
                });
            }
        }

        public static isAttachment(src: string): boolean {
            if (typeof src !== "string") {
                return false;
            }
            src = src.toLowerCase();
            if (src.indexOf("http") === 0) {
                return false;
            }
            if (src.length < 37 || src[36] !== "_") {
                return false;
            }
            // FORGUNCY-5372 [VideoPlayer]External video set as the FilePreviewer cell, the video cannot play in runtime
            //if (src[src.length - 1] !== "|") {
            //    return false;
            //}
            // ---------------------
            return true;
        }

        public isEmpty(v: any) {
            return v === null || v === undefined || v === "";
        }

        public refreshUI() {
            (this.vue?.$forceUpdate || this.loop)();
        }

        public isValidDate(date: any) {
            return date instanceof Date && !isNaN(date.getTime());
        }

        public getCustomSlotByPath(slotPath: SlotPath): string {
            const slot = this.getPropertyByPath((<any>window).FgcElement, slotPath.split("."));
            let slotStr = "";
            if (slot) {
                const classNames = this.CellElement.CssClassName?.split(" ") ?? [];
                for (const name of classNames) {
                    const selectSlotFunc = slot[name];
                    slotStr = selectSlotFunc ? selectSlotFunc() : "";
                    slotStr = typeof slotStr === "string" ? slotStr : "";
                    break;
                }
            }

            return slotStr;
        }

        public getPropertyByPath(target: any, pathes: Array<string>): any {
            let result = target ?? {};
            for (const item of pathes) {
                const property = result[item];
                if (property !== undefined) {
                    result = property;
                } else {
                    return null;
                }
            }
            return result;
        }
    }

    export enum SlotPath {
        selectPrefix = "SelectSlots.Prefix",
        selectOption = "SelectSlots.Option",
        datePickerRangeSeparater = "DatePickerSlots.RangeSeparater",
        datePickerCell = "DatePickerSlots.Cell",
        cascaderNode = "CascaderSlots.Node",
        calendarCell = "CalendarSlots.Cell",
        calendarHeader = "CalendarSlots.Header",
        menuItemContent = "MenuItemSlots.Content",
        backTopContent = "BackTopSlots.Content"
    }

    export interface IconResult {
        icon: string;
        isSvg: boolean;
    }

    export class InputCellTypeBaseParam {
    }

    export class InputCellTypeBase<T = object> extends ElementCellTypeBase<T> {
        private _inputTextCache: string;
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            this._inputTextCache = this.getValueFromElement()?.toString();
            this.getContainer().keyup(() => {
                if (this._inputTextCache !== this.getValueFromElement()?.toString()) {
                    this.hideValidateTooltip();
                }
                this._inputTextCache = this.getValueFromElement()?.toString();
            });
            super.onPageLoaded(info);
        }
        public setValueToElement(jElement: JQuery, value: any) {
            super.setValueToElement(jElement, value);
            this._inputTextCache = this.getValueFromElement()?.toString();
        }
    }

    export class SupportDataSourceCellType {

        public static refreshData(cellType: ElementCellTypeBase, bindingDataSourceModel, callBack, options: Forguncy.Plugin.queryDataOption = null) {
            this.refreshDataWithOption(cellType, bindingDataSourceModel, callBack, options);
        }

        public static refreshDataWithOption(cellType: ElementCellTypeBase, bindingDataSourceModel, callBack, options: Forguncy.Plugin.queryDataOption, watchOnDependenceChange: boolean = true) {
            cellType.getBindingDataSourceValue(bindingDataSourceModel, options, (dataSource) => {
                callBack(dataSource);
            });
            if (watchOnDependenceChange) {
                cellType.onDependenceCellValueChanged(() => {
                    this.refreshDataWithOption(cellType, bindingDataSourceModel, callBack, options, false);
                });
            }
        }
    }
}
