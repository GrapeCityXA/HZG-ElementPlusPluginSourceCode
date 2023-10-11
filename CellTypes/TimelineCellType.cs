using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using ElementUI.Properties;
using System.ComponentModel.DataAnnotations;

namespace ElementUI
{
    public class TimelineCellTypeDesigner : CellTypeDesigner<TimelineCellType>, ISupportPropertyInitialize
    {
        private List<TimelineOptionItem> GetDefaultOptions()
        {
            return new List<TimelineOptionItem>
            {
                new TimelineOptionItem()
                {
                    content = Resources.Timeline_Default1,
                    timestamp = "2018-04-15"
                },

                new TimelineOptionItem()
                {
                    content = Resources.Timeline_Default2,
                    timestamp = "2018-04-13"
                },

                new TimelineOptionItem()
                {
                    content = Resources.Timeline_Default3,
                    timestamp = "2018-04-11"
                }
            };
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            CellType.options = GetDefaultOptions();
            CellType.format = "yyyy-MM-dd";
        }

        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var cellStyle = StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper);
            var control = new TimelineDrawingControl(drawingHelper, this.CellType, cellStyle, GetDefaultOptions());
            return control;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Timeline.png")]
    [OrderWeight((int)Utils.OrderWeight.Timeline)]
    [Designer("ElementUI.TimelineCellTypeDesigner, ElementUI")]
    public class TimelineCellType : ElementCellTypeBase
    {
        [SRDisplayName(nameof(Resources.TimelineCellType_useBinding))]
        [BoolProperty]
        [MergableProperty(false)]
        public bool useBinding { get; set; } = false;

        [SRDisplayName(nameof(Resources.TimelineCellType_options))]
        [ListProperty(IndentLevel = 1)]
        public List<TimelineOptionItem> options { get; set; } = new List<TimelineOptionItem>();

        [SRBindingDataSourceProperty(Columns = nameof(Resources.Timeline_bindingOptions_Columns), ColumnsDescription= nameof(Resources.Timeline_bindingOptions_Columns_Description), IndentLevel = 1)]
        [SRDisplayName(nameof(Resources.TimelineCellType_bindingOptions))]
        public object bindingOptions { get; set; }

        [SRDisplayName(nameof(Resources.TimelineCellType_Sort))]
        [SRComboProperty(ValueList = "default|ascTimestamp|descTimestamp", DisplayList = nameof(Resources.TimelineCellType_Sort_items))]
        public string sort { get; set; } = "default";

        [SRDisplayName(nameof(Resources.TimelineCellType_NodeSize))]
        [SRComboProperty(ValueList = "normal|large", DisplayList = nameof(Resources.Timeline_NodeSize_DisplayList))]
        public string NodeSize { get; set; } = "normal";

        [SRDisplayName(nameof(Resources.TimelineCellType_hideTimestamp))]
        [BoolProperty]
        public bool hideTimestamp { get; set; }

        [SRDisplayName(nameof(Resources.TimelineCellType_placement))]
        [DefaultValue(Placement.bottom)]
        public Placement placement { get; set; } = Placement.bottom;

        [SRDisplayName(nameof(Resources.TimelineCellType_format))]
        [SRComboProperty(ValueList = nameof(Resources.Timeline_format_ValueList), IsSelectOnly = false)]
        public string format { get; set; }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.Timeline_SetDataSource))]
        [SRDescription(nameof(Resources.Timeline_SetDataSource_Description))]
        public void SetDataSource(
                [SRItemDisplayName(nameof(Resources.Timeline_dataSource))]
                [Required]
                string dataSource,
                [SRItemDisplayName(nameof(Resources.Timeline_contentProperty))]
                [Required]
                string contentProperty = "content",
                [SRItemDisplayName(nameof(Resources.Timeline_timestampProperty))]
                [Required]
                string timestampProperty = "timestamp")
        {

        }
        [SRDisplayName(nameof(Resources.MethodName_ReloadBindingItems))]
        [SRDescription(nameof(Resources.MethodName_ReloadBindingItems_Description))]
        [RunTimeMethod]
        public void ReloadBindingItems()
        {
        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(ReloadBindingItems))
            {
                return useBinding;
            }
            return base.GetRunTimeMethodVisible(name);
        }
        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(options))
            {
                return !useBinding;
            }
            if (propertyName == nameof(bindingOptions))
            {
                return useBinding;
            }
            if (propertyName == nameof(placement) || (propertyName == nameof(format)))
            {
                return !hideTimestamp;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.Timeline;
        }
    }

    public enum Placement
    {
        [SRDescription(nameof(Resources.TimelineCellType_top))]
        top,
        [SRDescription(nameof(Resources.TimelineCellType_bottom))]
        bottom
    }

    public class TimelineOptionItem
    {
        [SRDisplayName(nameof(Resources.TimelineCellType_content))]
        public string content { get; set; }

        [SRDisplayName(nameof(Resources.TimelineCellType_timestamp))]
        public string timestamp { get; set; }

        [SRDisplayName(nameof(Resources.TimelineCellType_color))]
        [ColorProperty]
        public string color { get; set; }

        private object _icon;

        [SRDisplayName(nameof(Resources.TimelineCellType_icon))]
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
    }
}
