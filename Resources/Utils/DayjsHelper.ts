namespace ElementUtils {

    type IOptions = { [name: string]: any };

    const { dayjs } = window.ElementPlus;

    export class Dayjs {

        private static _localeIndex = 0;

        private static _localeCache: Map<string, string> = new Map();

        private static _defaultLocaleName = dayjs.locale();

        private static _createLocale(options: IOptions) {
            const localeName = (++Dayjs._localeIndex).toString();

            const locale = {
                name: options.name ?? localeName,
                ...dayjs[Dayjs._defaultLocaleName],
                ...options
            };

            dayjs.locale(locale, null, true);

            Dayjs._localeCache.set(JSON.stringify(options), localeName);

            return localeName;
        }

        private static _delayResetLocale() {
            setTimeout(() => dayjs.locale(Dayjs._defaultLocaleName));
        }

        static toggleLocale(options: IOptions) {
            const localeName = Dayjs._localeCache.get(JSON.stringify(options)) ?? Dayjs._createLocale(options);

            dayjs.locale(localeName);

            Dayjs._delayResetLocale();
        }
    }
}