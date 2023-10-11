using ElementUI.WpfControls.DrawingObject;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using ElementUI.Properties;
using ElementUI.WpfControls;

namespace ElementUI
{
    public class AvatarCellTypeDesigner : CellTypeDesigner<AvatarCellType>
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new AvatarDrawingObject(cellInfo, drawingHelper, this.CellType);
            return control;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Avatar.png")]
    [OrderWeight((int)Utils.OrderWeight.Avatar)]
    [Designer("ElementUI.AvatarCellTypeDesigner, ElementUI")]
    public class AvatarCellType : ElementCellTypeBase, ICommandCellType
    {
        [SRDisplayName(nameof(Resources.AvatarCellType_CommandList))]
        public List<Command> CommandList { get; set; } = new List<Command>();

        public CommandExcuteKind CommandExcuteKind => CommandExcuteKind.Click;

        private object _icon;

        [SRDisplayName(nameof(Resources.AvatarCellType_icon))]
        [IconProperty(DefaultIconColor = "white")]
        public object icon
        {
            get
            {
                return _icon;
            }
            set
            {
                if (value is ImageValue || value is null)
                {
                    _icon = value;
                }
            }
        }

        [SRDisplayName(nameof(Resources.AvatarCellType_shape))]
        public AvatarShape shape { get; set; } = AvatarShape.circle;

        [SRDisplayName(nameof(Resources.AvatarCellType_fit))]
        [DefaultValue(AvatarFit.cover)]
        public AvatarFit fit { get; set; } = AvatarFit.cover;

        [FormulaProperty]
        [SRDisplayName(nameof(Resources.AvatarCellType_badge))]
        public object badge { get; set; }

        [SRDisplayName(nameof(Resources.AvatarCellType_showSystemAvatar))]
        [BoolProperty]
        [SRDescription(nameof(Resources.AvatarCellType_showSystemAvatar_Description))]
        public bool showSystemAvatar { get; set; }

        public override string ToString()
        {
            return Resources.Avatar;
        }
    }

    public enum AvatarShape
    {
        [SRDescription(nameof(Resources.AvatarCellType_circle))]
        circle,

        [SRDescription(nameof(Resources.AvatarCellType_square))]
        square
    }

    public enum AvatarFit
    {
        [SRDescription(nameof(Resources.AvatarCellType_fill))]
        fill,
        [SRDescription(nameof(Resources.AvatarCellType_contain))]
        contain,
        [SRDescription(nameof(Resources.AvatarCellType_cover))]
        cover,
        [SRDescription(nameof(Resources.AvatarCellType_none))]
        none,
        [SRDescription(nameof(Resources.AvatarCellType_scaleDown))]
        scaleDown
    }
}
