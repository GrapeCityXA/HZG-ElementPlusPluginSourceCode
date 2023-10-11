using ElementUI.WpfControls.DrawingObject;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using ElementUI.Properties;

namespace ElementUI
{
    public class BackupTopCellTypeDesigner : CellTypeDesigner<BackupTopCellType>
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new BackupTopDrawingObject(drawingHelper);
            return control;
        }
    }

    [Designer("ElementUI.BackupTopCellTypeDesigner, ElementUI")]
    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/BackupTop.png")]
    [OrderWeight((int)Utils.OrderWeight.BackupTop)]
    public class BackupTopCellType : ElementCellTypeBase
    {
        [SRDisplayName(nameof(Resources.BackupTopCellType_VisibilityHeight))]
        [IntProperty(Min = 0)]
        [DefaultValue(200)]
        public int VisibilityHeight { get; set; } = 200;

        [SRDisplayName(nameof(Resources.BackupTopCellType_Right))]
        [IntProperty(Min = 0, AllowNull = true)]
        [DefaultValue(0)]
        public int? Right { get; set; } = 0;

        [SRDisplayName(nameof(Resources.BackupTopCellType_Bottom))]
        [IntProperty(Min = 0, AllowNull = true)]
        [DefaultValue(0)]
        public int? Bottom { get; set; } = 0;

        public override string ToString()
        {
            return Resources.BackupTopCellType_PluginName;
        }
    }
}
