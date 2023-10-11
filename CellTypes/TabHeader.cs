using ElementUI.WpfControls.DrawingObject;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElementUI.Properties;
using System.ComponentModel.DataAnnotations;

namespace ElementUI
{
    public class TabHeaderCellTypeDesigner : CellTypeDesigner<TabHeader>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new TabHeaderDrawingObject(drawingHelper, this.CellType, cellInfo);

            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            if (context.Cell != null && string.IsNullOrEmpty(context.Cell.Value?.ToString()))
            {
                var index = 1;
                CellType.Tabs = Properties.Resources.TabHeaderDefaultValue.Split('/')
                    .Select(name => new Tab { Value = (index++).ToString(), Name = name }).ToList();
            }
        }
    }


    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/TabHeader.png")]
    [OrderWeight((int)Utils.OrderWeight.TabHeader)]
    [Designer("ElementUI.TabHeaderCellTypeDesigner, ElementUI")]
    public class TabHeader : ElementCellTypeBase, ISupportDefaultValue
    {
        [SRDisplayName(nameof(Resources.TabHeader_ClickCommand))]
        [SRCustomCommandObject(InitParamProperties = "itemIndex|itemValue|itemText", InitParamValues = nameof(Resources.TabHeaderCellType_ClickCommand_InitParamValues))]
        public object ClickCommand { get; set; }

        [FormulaProperty]
        [SRDisplayName(nameof(Resources.Common_DefaultValue))]
        public object DefaultValue { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_useBinding))]
        [BoolProperty]
        [MergableProperty(false)]
        public bool useBinding { get; set; } = false;

        [SRDisplayName(nameof(Resources.TabHeader_tabs))]
        [ListProperty(IndentLevel = 1)]
        public List<Tab> Tabs { get; set; } = new List<Tab>();

        [SRBindingDataSourceProperty(Columns = nameof(Resources.TabHeader_bindingOptions_Columns), IndentLevel = 1)]
        [SRDisplayName(nameof(Resources.SelectCellType_bindingOptions))]
        public object bindingOptions { get; set; }

        [SRDisplayName(nameof(Resources.TabHeader_type))]
        [SRComboProperty(ValueList = "default|card|border-card", DisplayList = nameof(Resources.TabHeaderCellType_type_DisplayList))]
        public string type { get; set; } = "default";

        [SRDisplayName(nameof(Resources.TabHeader_position))]
        [SRComboProperty(ValueList = "top|bottom|left|right", DisplayList = nameof(Resources.TabHeaderCellType_position_DisplayList))]
        public string position { get; set; } = "top";

        [SRDisplayName(nameof(Resources.TabHeader_stretch))]
        [BoolProperty]
        public bool stretch { get; set; }

        public bool NeedFormatDefaultValue => false;

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.TabHeader_HideItems_Display))]
        public void HideItems(
            [SRItemDisplayName(nameof(Resources.TabHeader_HideItemsParam_Items))]
            [SRDescription(nameof(Resources.TabHeader_HideItemsParam_ItemsDescription))]
            object items)
        {
        }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(stretch))
            {
                return position == "top" || position == "bottom";
            }
            if (propertyName == nameof(Tabs))
            {
                return !useBinding;
            }
            if (propertyName == nameof(bindingOptions))
            {
                return useBinding;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.TabHeader;
        }

    }

    public class Tab
    {
        [SRDisplayName(nameof(Resources.TabHeader_tab_value))]
        [Required]
        public string Value { get; set; }

        [SRDisplayName(nameof(Resources.TabHeader_tab_name))]
        [Required]
        public string Name { get; set; }
    }
}
