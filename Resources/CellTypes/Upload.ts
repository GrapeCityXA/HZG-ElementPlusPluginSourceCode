/// <reference path = "Base.ts" />
namespace ElementCellTypes {

    interface IUploadCellTypeParam {
        multiple: boolean;
        limit?: number;
        accept: string;
        sizeLimit: number;
        listType: UploadListType;
        buttonText: string;
        tipText: string;
        OverFileCountLimitError: string;
        OverFileSizeLimitError: string;
        FileFormatError: string;
    }

    enum UploadListType {
        upload,
        pictureList,
        pictureCard,
    }

    export class UploadCellType extends InputCellTypeBase<IUploadCellTypeParam> {
        public onPageLoaded(info: Forguncy.Plugin.CellTypeInfo) {
            const self = this;

            const cellType = this.cellType;

            const bindingStr =
                `action="#"
:multiple="multiple"
:limit="limit"
:on-exceed="handleExceed"
:on-success="handleSuccess"
:on-remove="handleRemove"
:disabled="disabled || readOnly"
:on-preview="handlePreview"
:before-upload="handleBeforeUpload"
:http-request="httpRequest"
:accept="accept"
:file-list="fileList" `;

            this.addCustomClass("el-upload-custom");

            const getTemplate = () => {

                if (cellType.listType === UploadListType.upload) {

                    return `<el-upload class="upload-demo" ${bindingStr} list-type="text">
                                <el-button :disabled="disabled" size="default" type="primary" v-if="!readOnly">{{buttonText}}</el-button>
                                <template #tip>
                                    <div class="el-upload__tip" :style="{ display: tipDisplay }" v-if="!readOnly">{{tipText}}</div>
                                </template>
                            </el-upload>`;
                }


                if (cellType.listType === UploadListType.pictureCard) {
                    return `<el-upload ${bindingStr} list-type="picture-card">
                                <el-icon>${ElementIconMap.get("plus")}</el-icon>
                            </el-upload>`;
                }

                if (cellType.listType === UploadListType.pictureList) {
                    return `<el-upload ${bindingStr} list-type="picture" class="el-upload-picture-custom">
                                <el-button :disabled="disabled" type="primary" v-if="!readOnly">{{buttonText}}</el-button>

                                <template #tip v-if="!readOnly">
                                    <div class="el-upload__tip" :style="{ display: tipDisplay }" >{{tipText}}</div>
                                </template>
                            </el-upload>`;
                }
            };

            const { CssClassName } = this.CellElement;

            const dialogCustomClass = CssClassName ? `${CssClassName}-upload-dialog` : "";

            const template = `<el-scrollbar>${getTemplate()}
                                  <el-dialog append-to-body v-model="dialogVisible" custom-class="fgc-upload-dialog ${dialogCustomClass}">
                                      <img style="max-width:100%;object-fit:contain;" :src="dialogImageUrl" alt="" />
                                  </el-dialog>
                              </el-scrollbar>`;


            const option = {
                el: "#" + this.uId,
                template: template,
                data() {
                    return {
                        multiple: cellType.multiple,
                        limit: cellType.limit,
                        accept: cellType.accept,
                        sizeLimit: cellType.sizeLimit,
                        buttonText: cellType.buttonText,
                        tipText: cellType.tipText,
                        tipDisplay: cellType.tipText ? "" : "none",
                        disabled: undefined,
                        fileList: [],
                        dialogImageUrl: '',
                        dialogVisible: false,
                        readOnly: null,
                        successSet:new Set()
                    };
                },
                methods: {
                    getValue() {
                        let fileList = this.fileList;
                        if (this.updatingFileList) {
                            fileList = this.updatingFileList;
                        }
                        const text = fileList.map(i => i.fgc_fileName).join("|");
                        return text ? text + "|" : null;
                    },
                    setValue(value) {
                        if (value === this.getValue()) {
                            return;
                        }
                        this.updatingFileList = null;
                        if (value) {
                            const values = value.split("|");
                              this.fileList = values.filter(i => i).map(i => {
                                if (i && i.length > 37 && i.charAt(36) === "_") {
                                    const fileName = i.substring(37);
                                    return {
                                        isBoundData:true,
                                        name: fileName,
                                        fgc_fileName: i,
                                        url: UploadCellType.getFileUrl(i)
                                    };
                                }
                            });
                        } else {
                            this.fileList = [];
                        }
                    },
                    disable() {
                        this.disabled = true;
                        self.addCustomClass("disable");
                    },
                    enable() {
                        this.disabled = false;
                        self.getContainer().removeClass("disable");
                    },
                    setReadOnly(readOnly) {
                        if (cellType.listType === UploadListType.pictureCard) {
                            $(".el-upload--picture-card", self.getContainer()).css("display", readOnly ? "none" : "flex");
                        }
                        this.readOnly = readOnly;
                    },
                    handleSuccess(response, file, fileList) {
                        this.successSet.add(file.uid)
                        file.fgc_fileName = response;
                        file.url = UploadCellType.getFileUrl(response);
                        this.updatingFileList = fileList;
                        self.commitValue();
                    },
                    handleExceed(files, fileList) {
                        let error = cellType.OverFileCountLimitError;
                        error = error.replace("${limit}", this.limit);
                        error = error.replace("${length}", files.length);
                        error = error.replace("${total}", files.length + fileList.length);
                        this.$message.error(error);
                    },
                    httpRequest(data) {
                        const root = Forguncy.Helper.SpecialPath.getBaseUrl();
                        const uploadPath = root + "FileDownloadUpload/Upload";
                        const formData = new FormData();
                        formData.append("file", data.file);
                        formData.append("uploadLimitId", self.CellElement.ServerPropertiesId.UploadLimit);
                        $.ajax({
                            url: uploadPath, //Server script to process data
                            type: 'POST',
                            success: (responseData) => {
                                data.onSuccess(responseData);
                            },
                            error: () => {
                                data.onError();
                            },
                            data: formData,  // Form data
                            cache: false,  //Options to tell jQuery not to process data or worry about content-type.
                            contentType: false,
                            processData: false,
                            headers: {  //Options to tell server return data with specified type
                                "Accept": "application/json"
                            }
                        });
                    },
                    handlePreview(file) {
                        if(!file.isBoundData && !this.successSet.has(file.uid)){
                            return;
                        }
                        if (UploadCellType.isImage(file.name)) {
                            this.dialogImageUrl = file.url;
                            this.dialogVisible = true;
                        } else {
                            (<any>Forguncy).Common.download(file.url);
                        }
                    },
                    handleBeforeUpload(file) {
                        if (cellType.accept) {
                            const exts = cellType.accept.split(",").filter(i => i).map(i => i.trim());
                            if (!exts.some(i => file.name.endsWith(i))) {
                                this.$message.error(cellType.FileFormatError);
                                return false;
                            }
                        }
                        if (cellType.sizeLimit) {
                            if (file.size / 1024 / 1024 > cellType.sizeLimit) {
                                this.$message.error(cellType.OverFileSizeLimitError);
                                return false;
                            }
                        }
                        return true;
                    },
                    handleRemove(file, fileList) {
                        this.updatingFileList = fileList;
                        self.commitValue();
                    }
                }
            };

            this.createVueApp(option);
            super.onPageLoaded(info);
        }

        static getFileUrl(fileName: string) {
            return Forguncy.Helper.SpecialPath.getBaseUrl() + Forguncy.ModuleLoader.getCdnUrl("FileDownloadUpload/Download?file=" + encodeURIComponent(fileName));
        }

        static isImage(fileName: string) {
            if (fileName) {
                const pointIndex = fileName.lastIndexOf(".");
                if (pointIndex) {
                    const extension = fileName.toLowerCase().substring(pointIndex + 1, fileName.length);
                    return ["jpg", "jpeg", "png", "gif", "eps", "svg", "bmp", "tif", "tiff"].some(i => i === extension);
                }
            }
            return false;
        }
    }
}

Forguncy.Plugin.CellTypeHelper.registerCellType("ElementUI.UploadCellType, ElementUI", ElementCellTypes.UploadCellType);