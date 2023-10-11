/// <reference path = "Base.ts" />

namespace ElementCellTypes {

    enum ProgressType {
        line,
        circle,
        dashboard,
    }

    enum ProgressStatus {
        none,
        success,
        exception,
        warning,
    }

    interface IProgressCellTypeParam {
        color: string;
        percentage: number;
        textInside: boolean;
        showText: boolean;
        textFormula: string;
        type: ProgressType;
        status: ProgressStatus;
        strokeWidth: number;
    }

    export class ProgressCellType extends ElementCellTypeBase<IProgressCellTypeParam> {
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const cellType = this.cellType;
            const self = this;
            const containerId = `${this.uId}`;

            const alignItems = cellType.type === ProgressType.line ? "" : "align-items:center";
            const option = {
                template: `<div class="fgc-el-progress" style="${alignItems};">
                              <el-progress
                                  :percentage="percentage"
                                  :stroke-width="strokeWidth"
                                  :width="width"
                                  :type="type"
                                  :text-inside="textInside"
                                  :status="status"
                                  :color="color"
                                  :show-text="showText"
                                  :format="format"
                              />
                        </div>
                          `,
                data() {
                    return {
                        percentage: 0,
                        strokeWidth: cellType.strokeWidth,
                        width: 126,
                        showText: !!cellType.showText,
                        textInside: !!cellType.textInside && cellType.type === ProgressType.line,
                        type: ProgressType[cellType.type],
                        status: cellType.status === ProgressStatus.none ? null : ProgressStatus[cellType.status],
                        color: cellType.status === ProgressStatus.none ? Forguncy.ConvertToCssColor(cellType.color) : undefined,
                    };
                },
                mounted() {
                    const container: HTMLElement = document.getElementById(containerId);

                    if (!container) {
                        return;
                    }

                    const { offsetHeight: height, offsetWidth: width } = container;

                    if (this.type !== ProgressType[ProgressType.line]) {
                        this.width = Math.min(width, height);
                    }

                    self.fontDom = $(".el-progress__text,.el-progress-bar__innerText", self.getContainer()).find("span");
                },
                methods: {
                    getValue() {
                        return this.percentage;
                    },
                    setValue(value) {
                        this.percentage = Number(value);
                    },
                    format(percentage) {
                        if (cellType.textFormula) {
                            return self.evaluateFormula(cellType.textFormula);
                        }
                        return percentage + "%";
                    }
                }
            };
            this.createVueApp(option);
            this.onDependenceCellValueChanged(() => {
                if (this.isFormula(cellType.textFormula)) {
                    const old = this.vue.percentage;
                    this.vue.percentage = old + 1;
                    // 强行触发一次percentage change，否则的话format方法不走
                    setTimeout(() => {
                        this.vue.percentage = old;
                    });
                }
            });

            super.onPageLoaded(info);
        }
        protected clickable(): boolean {
            return false;
        }

        public SetBackgroundColor(color: string) {
            this.vue.status = null;
            this.vue.color = Forguncy.ConvertToCssColor(color);
        }

        public SetStatus(status: ProgressStatus) {
            const newStatus = status === ProgressStatus.none ? null : ProgressStatus[status];

            if (newStatus) {
                this.vue.color = undefined;
            }

            this.vue.status = newStatus;
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.ProgressCellType, ElementUI", ElementCellTypes.ProgressCellType);
