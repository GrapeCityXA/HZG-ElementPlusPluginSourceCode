using ElementUI.WpfControls.DrawingObject;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using ElementUI.Properties;

namespace ElementUI
{
    public class RateCellTypeDesigner : CellTypeDesigner<RateCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new RateDrawingObject(drawingHelper, this.CellType);
            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            string[] colorDefaults = new string[] { "#99A9BF", "#F7BA2A", "#FF9900" };

            foreach (var item in colorDefaults)
            {
                CellType.colors.Add(new ColorItem()
                {
                    color = item
                });
            }

            string[] textDefaults = Resources.RateCellType_defaultSetting.Split(',');
            foreach (var item in textDefaults)
            {
                CellType.texts.Add(new RateNormalItem()
                {
                    label = item
                });
            }
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Rate.png")]
    [OrderWeight((int)Utils.OrderWeight.Rate)]
    [Designer("ElementUI.RateCellTypeDesigner, ElementUI")]
    public class RateCellType : ElementCellTypeBase, ICommandCellType, ISupportReadOnly
    {
        [SRDisplayName(nameof(Resources.RateCellType_CommandList))]
        public List<Command> CommandList { get; set; } = new List<Command>();

        public CommandExcuteKind CommandExcuteKind => CommandExcuteKind.OnValueChanged;

        [SRDisplayName(nameof(Resources.RateCellType_max))]
        [DefaultValue(5)]
        public int max { get; set; } = 5;

        [SRDisplayName(nameof(Resources.RateCellType_lowThreshold))]
        [DefaultValue(2)]
        public int lowThreshold { get; set; } = 2;

        [SRDisplayName(nameof(Resources.RateCellType_highThreshold))]
        [DefaultValue(4)]
        public int highThreshold { get; set; } = 4;

        [SRDisplayName(nameof(Resources.RateCellType_DisplayContentType))]
        public DisplayContentType DisplayContentType { get; set; } = DisplayContentType.None;

        [SRDisplayName(nameof(Resources.RateCellType_texts))]
        [ListProperty(IndentLevel = 1)]
        public List<RateNormalItem> texts { get; set; } = new List<RateNormalItem>();

        [SRDisplayName(nameof(Resources.RateCellType_voidColor))]
        [ColorProperty]
        public string voidColor { get; set; } = "#C6D1DE";

        [SRDisplayName(nameof(Resources.RateCellType_colors))]
        [SRDescription(nameof(Resources.RateCellType_colors_Description))]
        [ListProperty(MaxCount = 3)]
        public List<ColorItem> colors { get; set; } = new List<ColorItem>();

        [SRDisplayName(nameof(Resources.RateCellType_ReadOnly))]
        [BoolProperty]
        public bool ReadOnly { get; set; }

        [SRDisplayName(nameof(Resources.RateCellType_disabledVoidColor))]
        [ColorProperty]
        public string disabledVoidColor { get; set; } = "#EFF2F7";

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.RateCellType_allowHalf))]
        public bool allowHalf { get; set; }

        public override string ToString()
        {
            return Resources.Rate;
        }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(texts))
            {
                return DisplayContentType == DisplayContentType.Text;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }
    }
    public enum DisplayContentType
    {
        [SRDescription(nameof(Resources.RateCellType_None))]
        None,
        [SRDescription(nameof(Resources.RateCellType_Text))]
        Text,
        [SRDescription(nameof(Resources.RateCellType_Score))]
        Score,
    }
    public class RateNormalItem
    {
        [SRDisplayName(nameof(Resources.RateCellType_label))]
        public string label { get; set; }
    }
    public class ColorItem
    {
        [SRDisplayName(nameof(Resources.RateCellType_color))]
        [ColorProperty]
        [Required]
        public string color { get; set; }
    }
}
