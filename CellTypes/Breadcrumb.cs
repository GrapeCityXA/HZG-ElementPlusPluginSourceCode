using ElementUI.WpfControls.DrawingObject;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ElementUI.Properties;
using ElementUI.WpfControls;

namespace ElementUI
{
    public class BreadcrumbCellTypeDesigner : CellTypeDesigner<BreadcrumbCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new BreadcrumbDrawingObject(cellInfo, StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper), this.CellType);
            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            if (context.Cell != null && string.IsNullOrEmpty(context.Cell.Value?.ToString()))
            {
                context.Cell.Value = Resources.BreadcrumbCellType_NullPreview;
            }
            this.CellType.separator = "/";
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Breadcrumb.png")]
    [OrderWeight((int)Utils.OrderWeight.Breadcrumb)]
    [Designer("ElementUI.BreadcrumbCellTypeDesigner, ElementUI")]
    public class BreadcrumbCellType : ElementCellTypeBase, ISupportDefaultValue
    {
        [SRDisplayName(nameof(Resources.Breadcrumb_ClickCommand))]
        [SRCustomCommandObject(InitParamProperties = "pageName", InitParamValues = nameof(Resources.Breadcrumb_ClickCommand_InitParamValues))]
        public object ClickCommand { get; set; }

        [FormulaProperty]
        [SRDisplayName(nameof(Resources.Common_DefaultValue))]
        public object DefaultValue { get; set; }

        [SRDisplayName(nameof(Resources.Breadcrumb_separator))]
        [ComboProperty(ValueList = "/|>", IsSelectOnly = false)]
        public string separator { get; set; }
        public bool NeedFormatDefaultValue => false;

        public override string ToString()
        {
            return Resources.Breadcrumb;
        }

        public override ForguncyDisabledSettings GetDisabledPropertySettings(ForguncyPageKind pageType)
        {
            return ForguncyDisabledSettings.TopAlignment |
                ForguncyDisabledSettings.MiddleAlignment |
                ForguncyDisabledSettings.BottomAlignment |
                ForguncyDisabledSettings.RightAlignment |
                ForguncyDisabledSettings.CenterAlignment |
                ForguncyDisabledSettings.LeftAlignment;
        }
    }
}
