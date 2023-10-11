namespace ElementCellTypes {
    export class FileHelper {
        public static IsAttachment(str: string): boolean {
            return /^[0-9a-f]{8}-?[0-9a-f]{4}-?[1-5][0-9a-f]{3}-?[89ab][0-9a-f]{3}-?[0-9a-f]{12}_.+$/i.test(str);
        }
        public static GetUploadImageSrc(str: string): string {
            return Forguncy.Helper.SpecialPath.getBaseUrl() + "Upload/" + str;
        }
    }
}