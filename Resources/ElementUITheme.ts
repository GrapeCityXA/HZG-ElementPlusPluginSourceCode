namespace ElementTheme {

    const colorDictionary = {
        "--el-color-primary": "Accent 1",
        "--el-color-primary-light-1": "Accent 1 10",
        "--el-color-primary-light-2": "Accent 1 20",
        "--el-color-primary-light-3": "Accent 1 30",
        "--el-color-primary-light-4": "Accent 1 40",
        "--el-color-primary-light-5": "Accent 1 50",
        "--el-color-primary-light-6": "Accent 1 60",
        "--el-color-primary-light-7": "Accent 1 70",
        "--el-color-primary-light-8": "Accent 1 80",
        "--el-color-primary-light-9": "Accent 1 90",
        "--el-color-primary-dark-2": "Accent 1 -20",
    };

    let updated = false;

    export function updateCssAndAppend() {
        if (updated) {
            return;
        }

        let cssStr = "";

        for (let key in colorDictionary) {
            cssStr += `${key}:${Forguncy.ConvertToCssColor(colorDictionary[key])};`;
        }

        cssStr = `:root{ ${cssStr} }`;

        const style = document.createElement("style");
        style.type = "text/css";
        style.id = "FGC_Element_Plus_Variables";
        style.innerHTML = cssStr;
        document.getElementsByTagName("head")[0].appendChild(style);

        updated = true;
    }
}