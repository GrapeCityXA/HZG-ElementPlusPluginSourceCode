declare namespace Forguncy {
    const enum ForguncySupportCultures {
        English = "EN",
        Chinese = "CN",
        Japanese = "JA",
        Korean = "KR",
    }

    class FocusMoveHelper {
        public static AllowMoveFocusByEnterKey(control: JQuery, isInListview: boolean): void
    }

    class RS {
        static Culture: ForguncySupportCultures;
    }

    class ImageDataHelper {
        public static IsSvg: (src: string) => boolean;
    }

    class ImageHelper {
        public static preHandleSvg(svg: JQuery, color: string);
    }

    class ForguncyData {
        static initListviewPaginationInfo(runTimePageName: string, listviewName: string, pageSize: number);
    }

    class ColorHelper {
        public static UpdateTint(baseColor: string, tint: number, alpha: number): string
    }

    class ModuleLoader {
        static getCdnUrl(url: string);
    }

    class PageBuilder {
        public static get pageLoadingCover(): JQuery;
        public static get pageLoadingText(): JQuery;
        public static hidePageLoadingCover(): void;
        public static showPageLoadingCover(color?: string): void;
    }
}

declare namespace Forguncy.Plugin {
    interface CellContentElement {
        ServerPropertiesId?: { [propertyName: string]: string };
        CssClassName?: string;
    }

    interface CellTypeBase {
        runTimePageName: string;
        onPageLoaded(info: CellTypeInfo): void;
        getValueFromDataModel(): any;
    }
    interface CellTypeInfo {
        value: any;
        isReadOnly: boolean;
        isDisabled: boolean;
        styleData: StyleMetaData;
    }
    interface CommandBase {
        CommandExecutingInfo: {
            suspend: boolean;
        };
    }
}