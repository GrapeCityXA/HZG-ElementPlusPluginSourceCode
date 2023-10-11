/// <reference path = "Base.ts" />
namespace ElementCellTypes {
    interface IBackupTopCellTypeParam {
        VisibilityHeight: number;
        Right: number;
        Bottom: number;
    }

    export class BackupTopCellType extends ElementCellTypeBase<IBackupTopCellTypeParam> {
        private containerId: string;
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;
            this.containerId = `${this.uId}-el-backuptop`;

            const initData = {};

            // do nothing
            Object.assign(initData, {
                style: '',
                right: cellType.Right,
                bottom: cellType.Bottom
            });

            const contentSlot = this.getCustomSlotByPath(SlotPath.backTopContent);

            const findHasScrollBarDom = (el: JQuery) => {
                try {
                    if (!el) {
                        return null;
                    }
                    const overflowY = el.css("overflow-y");

                    if (overflowY === "auto" || overflowY === "scroll") {
                        if (el[0].scrollHeight > el[0].offsetHeight) {
                            return el;
                        }
                    }

                    const parent = el?.parent();

                    return parent ? findHasScrollBarDom(parent) : null;
                } catch {
                    return null;
                }
            };

            let targetTemplate;

            const el: JQuery | null = findHasScrollBarDom(this.vueContainer);

            if (el?.length) {
                if (!el.attr("id")) {
                    el.attr("id", ElementCellTypeBase.generateID("fgc-el-backupTop-random-id"));
                }

                targetTemplate = `target="#${el.attr("id")}"`;
            }

            const option = {
                el: `#${this.uId}`,
                template: `
<el-backtop id="${this.containerId}" :style="style" :visibilityHeight="visibilityHeight" :right="right" :bottom="bottom" ${targetTemplate} >
    ${contentSlot || info?.value || ""}
</el-backtop>`,
                data() {
                    return Object.assign({
                        visibilityHeight: cellType.VisibilityHeight,
                    }, initData);
                },
            };
            this.createVueApp(option);
            super.onPageLoaded(info);
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.BackupTopCellType, ElementUI", ElementCellTypes.BackupTopCellType);