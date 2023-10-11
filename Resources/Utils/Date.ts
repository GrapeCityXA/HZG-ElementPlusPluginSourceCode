namespace ElementCellTypes {
    interface ConverTimeToDateOptions {
        year: number;
        /* 0-11 */
        month: number;
        day: number;
    }

    export class DateUtil {

        private static isInvalidDate(date: Date) {
            return !date || !date.getTime();
        }

        private static InteralConvertToDate(value: any) {
            if (!value) {
                return null;
            }
            if (value instanceof Date) {
                return value;
            }
            const numerValue = Number(value);

            const isOADate = !isNaN(numerValue);

            if (isOADate) {
                return Forguncy.ConvertOADateToDate(numerValue);
            }

            return new Date(value);
        }

        /**
         * 将时间转为日期
         * @param value
         */
        private static InteralConvertTimeToDate(value) {
            const date = DateUtil.InteralConvertToDate(value);
            return this.isInvalidDate(date) ? new Date(`1970/1/1 ${value}`) : date;
        }

        /**
         * 将一个值转为一个日期
         * @param value
         * @param effectiveValue    转换失败时返回的值，默认是 Invalid Date
         */
        static ConvertToDate(value: any, effectiveValue: any = "Invalid Date") {
            const date = DateUtil.InteralConvertToDate(value);
            return this.isInvalidDate(date) ? effectiveValue : date;
        }

        /**
         * 将一个时间转为一个Date
         * @param value             时间字符串
         * @param options           传入年月日，默认是1970年1月1号
         * @param effectiveValue    转换失败时返回的值，默认是 Invalid Date
         */
        static ConverTimeToDate(value: any, options?: ConverTimeToDateOptions, effectiveValue: any = "Invalid Date") {
            const date = DateUtil.InteralConvertTimeToDate(value);
            if (this.isInvalidDate(date)) {
                return effectiveValue;
            }
            if (options.year) {
                date.setFullYear(options.year);
            }
            if (options.month || options.month === 0) {
                date.setMonth(options.month);
            }
            if (options.day) {
                date.setDate(options.day);
            }
            return date;
        }

    }
}