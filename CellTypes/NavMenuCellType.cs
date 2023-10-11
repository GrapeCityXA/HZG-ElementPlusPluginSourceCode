using ElementUI.WpfControls.DrawingObject;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ElementUI.Properties;
using ElementUI.WpfControls;

namespace ElementUI
{
    public class NavMenuCellTypeDesigner : CellTypeDesigner<NavMenuCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new NavMenuDrawingControl(this.CellType, drawingHelper);

            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            var list = JsonConvert.DeserializeObject<List<MenuNode>>(Properties.Resources.DefaultTreeSource);
            foreach (var item in list)
            {
                item.expend = true; // 默认展开一层
            }
            this.CellType.options = list.OfType<ITreeNode>().ToList();
            this.CellType.backgroundColor = "#ffffff";
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/NavMenu.png")]
    [OrderWeight((int)Utils.OrderWeight.NavMenu)]
    [Designer("ElementUI.NavMenuCellTypeDesigner, ElementUI")]
    public class NavMenuCellType : ElementCellTypeBase
    {
        [SRDisplayName(nameof(Resources.NavMenuCellType_SelectCommand))]
        [SRCustomCommandObject(InitParamProperties = "value|label|parentId", InitParamValues = nameof(Resources.NavMenuCellType_SelectCommand_InitParamValues))]
        public object SelectCommand { get; set; }
        
        [SRDisplayName(nameof(Resources.NavMenuCellType_ClicktCommand))]
        [SRCustomCommandObject(InitParamProperties = "value|label|parentId", InitParamValues = nameof(Resources.NavMenuCellType_SelectCommand_InitParamValues))]
        public object ClickCommand { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_useBinding))]
        [BoolProperty]
        [MergableProperty(false)]
        public bool useBinding { get; set; } = false;

        [SRTreeProperty(IndentLevel = 1, NodeType = typeof(MenuNode), DefaultNodeName = nameof(Resources.NavMenuCellType_options_DefaultNodeName))]
        [SRDisplayName(nameof(Resources.NavMenuCellType_options))]
        public List<ITreeNode> options { get; set; } = new List<ITreeNode>();

        [SRBindingDataSourceProperty(Columns = nameof(Resources.NavMenuCellType_bindingOptions_Columns), IndentLevel = 1, IsIdPidStructure = true)]
        [SRDisplayName(nameof(Resources.NavMenuCellType_bindingOptions))]
        public object bindingOptions { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_mode))]
        [SRRadioGroupProperty(ValueList = "vertical|horizontal", DisplayList = nameof(Resources.NavMenuCellType_mode_DisplayList))]
        public string mode { get; set; } = "vertical";

        [SRDisplayName(nameof(Resources.NavMenuCellType_collapse))]
        [SRDescription(nameof(Resources.NavMenuCellType_Collapse_Description))]
        [BoolProperty]
        public bool collapse { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_collapseTransition))]
        [BoolProperty]
        public bool collapseTransition { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_uniqueOpened))]
        [BoolProperty]
        public bool uniqueOpened { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_DefaultExpansion))]
        [SRComboProperty(ValueList = "expand|collapse", DisplayList = nameof(Resources.NavMenuCellType_DefaultExpansion_DisplayName))]
        public string DefaultExpansion { get; set; } = "expand";

        [SRDisplayName(nameof(Resources.NavMenuCellType_menuTrigger))]
        [SRComboProperty(ValueList = "hover|click", DisplayList = nameof(Resources.NavMenuCellType_menuTrigger_DisplayList))]
        public string menuTrigger { get; set; } = "hover";

        string _theme = "light";
        [SRDisplayName(nameof(Resources.NavMenuCellType_theme))]
        [SRComboProperty(ValueList = "light|dark|custom", DisplayList = nameof(Resources.NavMenuCellType_theme_DisplayList))]
        public string theme
        {
            get
            {
                return _theme;
            }
            set
            {
                if (_theme != value)
                {
                    if (_theme == "custom")
                    {
                        lastCustomBackgroundColor = backgroundColor;
                        lastCustomTextColor = textColor;
                        lastCustomActiveTextColor = activeTextColor;
                    }

                    _theme = value;
                    if (_theme == "light")
                    {
                        backgroundColor = "#ffffff";
                        textColor = "#303133";
                        activeTextColor = "Accent 1";
                    }
                    else if (_theme == "dark")
                    {
                        backgroundColor = "#545c64";
                        textColor = "#fff";
                        activeTextColor = "#ffd04b";
                    }
                    else if (_theme == "custom")
                    {
                        backgroundColor = lastCustomBackgroundColor ?? backgroundColor;
                        textColor = lastCustomTextColor ?? textColor;
                        activeTextColor = lastCustomActiveTextColor ?? activeTextColor;
                    }
                }
            }
        }

        [Browsable(false)]
        public string lastCustomBackgroundColor { get; set; }
        [Browsable(false)]
        public string lastCustomTextColor { get; set; }
        [Browsable(false)]
        public string lastCustomActiveTextColor { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_backgroundColor))]
        [ColorProperty(SupportNoFill=true)]
        public string backgroundColor { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_textColor))]
        [ColorProperty]
        public string textColor { get; set; } = "#303133";

        [SRDisplayName(nameof(Resources.NavMenuCellType_activeTextColor))]
        [ColorProperty]
        public string activeTextColor { get; set; } = "Accent 1";

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(backgroundColor) ||
                propertyName == nameof(textColor) ||
                propertyName == nameof(activeTextColor))
            {
                return theme == "custom";
            }
            if (propertyName == nameof(options))
            {
                return !useBinding;
            }
            if (propertyName == nameof(menuTrigger))
            {
                return mode == "horizontal";
            } 
            if (propertyName == nameof(collapseTransition))
            {
                return mode == "vertical";
            }
            if (propertyName == nameof(bindingOptions))
            {
                return useBinding;
            }
            if (propertyName == nameof(collapse) ||
                propertyName == nameof(uniqueOpened))
            {
                return mode == "vertical";
            }
            if (propertyName == nameof(DefaultExpansion)) {
                return mode == "vertical" && !uniqueOpened && useBinding;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.NavMenu;
        }

        [SRDisplayName(nameof(Resources.NavMenuCellType_GetSelectPath))]
        [RunTimeMethod]
        [SRDescription(nameof(Resources.NavMenuCellType_GetSelectPath_Description))]
        public GetSelectPathResult GetSelectPath(
            [SRComboProperty(ValueList ="valuePath|labelPath", DisplayList =nameof(Resources.NavMenuCellType_GetSelectPath_DisplayList))]
            [SRItemDisplayName(nameof(Resources.NavMenuCellType_type))]
            string type = "valuePath")
        {
            return null;
        }

        [SRDisplayName(nameof(Resources.MethodName_ReloadBindingItems))]
        [SRDescription(nameof(Resources.MethodName_ReloadBindingItems_Description))]
        [RunTimeMethod]
        public void ReloadBindingItems()
        {
        }
        [SRDisplayName(nameof(Resources.NavMenuCellType_SetCollapse))]
        [RunTimeMethod]
        public void SetCollapse([BoolProperty][SRItemDisplayName(nameof(Resources.NavMenuCellType_SetCollapse_Collapse))] bool collapse = true)
        {

        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.HideItems_Display))]
        public void HideItems(
            [SRItemDisplayName(nameof(Resources.HideItemsParam_Items))]
            [SRDescription(nameof(Resources.HideItemsParam_ItemsDescription))]
            object items)
        {
        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.RunTimeMethod_SetBadge))]
        public void SetBadge(
            [SRItemDisplayName(nameof(Resources.RunTimeMethod_SetBadge_itemValue))]
            string itemValue,
            [SRItemDisplayName(nameof(Resources.RunTimeMethod_SetBadge_badgeValue))]
            string badgeValue)
        {

        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(ReloadBindingItems))
            {
                return useBinding;
            }
            if (name == nameof(SetCollapse))
            {
                return mode == "vertical";
            }
            return base.GetRunTimeMethodVisible(name);
        }

        public class GetSelectPathResult
        {
            [SRDisplayName(nameof(Resources.NavMenuCellType_PathArray))]
            public object PathArray { get; set; }
        }
    }
    public class MenuNode : TreeNodeBase<MenuNode>
    {
        [SRDisplayName(nameof(Resources.NavMenuCellType_icon))]
        [IconProperty(SupportUseCellColor = true)]
        public ImageValue icon { get; set; }


        [SRDisplayName(nameof(Resources.NavMenuCellType_notification))]
        [FormulaProperty]
        public object notification { get; set; }

        [SRDisplayName(nameof(Resources.NavMenuCellType_expend))]
        public bool expend { get; set; }
    }
}
