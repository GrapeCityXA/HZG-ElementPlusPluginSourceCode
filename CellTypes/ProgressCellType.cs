using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Plugin;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using ElementUI.Properties;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GrapeCity.Forguncy.Commands;

namespace ElementUI
{
    public class ProgressCellTypeDesigner : CellTypeDesigner<ProgressCellType>
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            Brush color;
            switch (this.CellType.status)
            {
                case ProgressStatus.success:
                    color = ColorHelper.Success;
                    break;
                case ProgressStatus.exception:
                    color = ColorHelper.Error;
                    break;
                case ProgressStatus.warning:
                    color = ColorHelper.Warning;
                    break;
                default:
                    color = drawingHelper.GetBrush(this.CellType.color);
                    break;
            }

            var cellStyleViewModel = StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper);
            if (this.CellType.type == ProgressType.line)
            {
                var control = new LineProgressDrawingObject(this.CellType, color, cellStyleViewModel, drawingHelper);
                return control;
            }
            else if (this.CellType.type == ProgressType.circle ||
                this.CellType.type == ProgressType.dashboard)
            {
                var control = new CircleProgressDrawingObject(drawingHelper, this.CellType, color, cellStyleViewModel);
                return control;
            }
            return base.GetDrawingControl(cellInfo, drawingHelper);
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Progress.png")]
    [OrderWeight((int)Utils.OrderWeight.Progress)]
    [Designer("ElementUI.ProgressCellTypeDesigner, ElementUI")]
    public class ProgressCellType : ElementCellTypeBase, ICommandCellType
    {
        [SRDisplayName(nameof(Resources.Common_ValueChangeCommand))]
        public List<Command> CommandList { get; set; } = new List<Command>();

        public CommandExcuteKind CommandExcuteKind => CommandExcuteKind.OnValueChanged;

        [SRDisplayName(nameof(Resources.ProgressCellType_type))]
        public ProgressType type { get; set; } = ProgressType.line;

        [SRDisplayName(nameof(Resources.ProgressCellType_strokeWidth))]
        [DefaultValue(6d)]
        public double strokeWidth { get; set; } = 6;

        [SRDisplayName(nameof(Resources.ProgressCellType_status))]
        public ProgressStatus status { get; set; } = ProgressStatus.none;

        [SRDisplayName(nameof(Resources.ProgressCellType_color))]
        [ColorProperty]
        public string color { get; set; } = "Accent 1";

        [SRDisplayName(nameof(Resources.ProgressCellType_showText))]
        [DefaultValue(true)]
        public bool showText { get; set; } = true;

        [SRDisplayName(nameof(Resources.ProgressCellType_textInside))]
        public bool textInside { get; set; }

        [FormulaProperty]
        [SRDisplayName(nameof(Resources.ProgressCellType_textFormula))]
        [SRDescription(nameof(Resources.ProgressCellType_textFormula_Description))]
        public object textFormula { get; set; }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.ProgressCellType_SetBackgroundColor))]
        [SRDescription(nameof(Resources.ProgressCellType_SetBackgroundColor_Description))]
        public void SetBackgroundColor(
            [SRItemDisplayName(nameof(Resources.ProgressCellType_color))]
            [ColorProperty]
            string color = "Accent 1")
        {
        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.ProgressCellType_SetStatus))]
        public void SetStatus(
               [SRItemDisplayName(nameof(Resources.ProgressCellType_status))]
               ProgressStatus status = ProgressStatus.none)
        {
        }

        public override string ToString()
        {
            return Resources.Progress;
        }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(textFormula))
            {
                return showText;
            }
            if (propertyName == nameof(textInside))
            {
                return type == ProgressType.line && showText;
            }
            if (propertyName == nameof(color) || propertyName == nameof(showText))
            {
                return status == ProgressStatus.none;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }
    }
    public enum ProgressType
    {
        [SRDescription(nameof(Resources.ProgressCellType_line))]
        line,
        [SRDescription(nameof(Resources.ProgressCellType_circle))]
        circle,
        [SRDescription(nameof(Resources.PaginationCellType_dashboard))]
        dashboard
    }

    public enum ProgressStatus
    {
        [SRDescription(nameof(Resources.ProgressCellType_none))]
        none,
        [SRDescription(nameof(Resources.ProgressCellType_success))]
        success,
        [SRDescription(nameof(Resources.ProgressCellType_exception))]
        exception,
        [SRDescription(nameof(Resources.ProgressCellType_warning))]
        warning,
    }
}
