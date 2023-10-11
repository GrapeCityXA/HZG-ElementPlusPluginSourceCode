using ElementUI.WpfControls;
using ElementUI.WpfControls.DrawingObject;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElementUI.Properties;

namespace ElementUI
{
    public class UploadCellTypeDesigner : CellTypeDesigner<UploadCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var buttonBackground = drawingHelper.GetBrush("Accent 1");
            if (this.CellType.IsDisabled)
            {
                buttonBackground.Opacity = 0.5;
            }
            if (this.CellType.listType == UploadListType.upload ||
                this.CellType.listType == UploadListType.pictureList)
            {
                var control = new UploadFileDrawingObject(drawingHelper, this.CellType, buttonBackground);
                return control;
            }
            else
            {
                return new UploadPictureListDrawingObject(drawingHelper, this.CellType);
            }
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            this.CellType.buttonText = Resources.UploadCellType_buttonText_DefaultValue;
            this.CellType.tipText = Resources.UploadCellType_tipText_DefaultValue;
            this.CellType.OverFileCountLimitError = Resources.UploadCellType_OverFileCountLimitError;
            this.CellType.OverFileSizeLimitError = Resources.UploadCellType_OverFileSizeLimitError;
            this.CellType.FileFormatError = Resources.UploadCellType_FileFormatError;
            this.CellType.accept = ".jpg, .jpeg, .png";
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Upload.png")]
    [OrderWeight((int)Utils.OrderWeight.Upload)]
    [Designer("ElementUI.UploadCellTypeDesigner, ElementUI")]
    public class UploadCellType : InputCellTypeBase,ISupportReadOnly
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        [Browsable(false)]
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        [Browsable(false)]
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        [Browsable(false)]
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        #endregion

        [SRDisplayName(nameof(Resources.UploadCellType_limit))]
        [SRIntProperty(Min = 1, AllowNull = true, Watermark = nameof(Resources.NoLimit))]
        public int? limit { get; set; }

        [SRDisplayName(nameof(Resources.UploadCellType_accept))]
        public string accept { get; set; }

        double? _sizeLimit;
        [SRDisplayName(nameof(Resources.UploadCellType_sizeLimit))]
        [SRDoubleProperty(Min = 0, AllowNull = true, Watermark = nameof(Resources.NoLimit))]
        public double? sizeLimit
        {
            get
            {
                return _sizeLimit;
            }
            set
            {
                if(value == 0)
                {
                    value = null;
                }
                _sizeLimit = value;
            }
        }

        [SRDisplayName(nameof(Resources.UploadCellType_listType))]
        public UploadListType listType { get; set; } = UploadListType.upload;

        [SRDisplayName(nameof(Resources.UploadCellType_buttonText))]
        public string buttonText { get; set; }

        [SRDisplayName(nameof(Resources.UploadCellType_tipText))]
        public string tipText { get; set; }

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.UploadCellType_multiple))]
        [DefaultValue(true)]
        public bool multiple { get; set; } = true;

        [SRDisplayName(nameof(Resources.UploadCellType_IsDisabled))]
        public override bool IsDisabled { get; set; }

        [SRDisplayName(nameof(Resources.UploadCellType_ReadOnly))]
        public bool ReadOnly { get; set; }

        [Browsable(false)]
        [SaveJsonIgnore]
        [ServerProperty]
        public UploadLimit UploadLimit
        {
            get
            {
                return new UploadLimit()
                {
                    ExtensionFilter = this.accept,
                    SizeLimit = this.sizeLimit ?? 0
                };
            }
        }

        [Browsable(false)]
        public string OverFileCountLimitError { get; set; }
        [Browsable(false)]
        public string OverFileSizeLimitError { get; set; }
        [Browsable(false)]
        public string FileFormatError { get; set; }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(buttonText) || propertyName == nameof(tipText))
            {
                return listType == UploadListType.upload || listType == UploadListType.pictureList;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.Upload;
        }
    }

    public enum UploadListType
    {
        [SRDescription(nameof(Resources.UploadCellType_upload))]
        upload,
        [SRDescription(nameof(Resources.UploadCellType_pictureList))]
        pictureList,
        [SRDescription(nameof(Resources.UploadCellType_pictureCard))]
        pictureCard,
    }

    public class UploadLimit : IUploadLimit
    {
        public UploadLimit()
        {
            ExtensionFilter = "";
            SizeLimit = 0;
        }
        [DefaultValue("")]
        public string ExtensionFilter { get; set; }

        [DefaultValue(0d)]
        public double SizeLimit { get; set; }
    }
}
