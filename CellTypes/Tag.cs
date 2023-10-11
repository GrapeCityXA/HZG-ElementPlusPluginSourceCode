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

namespace ElementUI
{
    public class TagCellTypeDesigner : CellTypeDesigner<Tag>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new TagDrawingObject(drawingHelper, this.CellType, cellInfo);

            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            for (int i = 0; i < 6; i++)
            {
                this.CellType.ColorList.Add(new ColorItem()
                {
                    color = "Accent " + (i + 1),
                });
            }
            this.CellType.addButtonSettings.text = Resources.TagCellType_addButtonText_DefaultValue;
            this.CellType.separator = ",";
        }
    }


    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Tag.png")]
    [OrderWeight((int)Utils.OrderWeight.Tag)]
    [Designer("ElementUI.TagCellTypeDesigner, ElementUI")]
    public class Tag : InputCellTypeBase, ISupportReadOnly
    {
        [SRDisplayName(nameof(Resources.Common_ValueChangeCommand))]
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }

        [SRDisplayName(nameof(Resources.Tag_ClickCommand))]
        [SRCustomCommandObject(InitParamProperties = "itemName", InitParamValues = nameof(Resources.TagCellType_ClickCommand_InitParamValues))]
        public object ClickCommand { get; set; }

        #region Override基类属性，仅仅是为了确保属性顺序

        [Browsable(false)]
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        [Browsable(false)]
        public override bool IsDisabled { get; set; }
        #endregion

        [SRDisplayName(nameof(Resources.Tag_ColorList))]
        [ListProperty(MinCount = 1)]
        [SRDescription(nameof(Resources.TagCellType_ColorList_Description))]
        public List<ColorItem> ColorList { get; set; } = new List<ColorItem>();

        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        [SRDisplayName(nameof(Resources.Tag_separator))]
        [ComboProperty(ValueList = ",|/|>", IsSelectOnly = false)]
        public string separator { get; set; }

        private string _size = "large";

        [SRDisplayName(nameof(Resources.Tag_size))]
        [SRComboProperty(ValueList = "large|default|small", DisplayList = nameof(Resources.TagCellType_size_DisplayList))]
        public string size
        {
            get
            {
                return _size;
            }
            set
            {
                /**
                 * 处理Element2 升级 Element Plus 兼容性问题
                 * Mini 变更为 Small
                 **/
                _size = value == "mini" ? "small" : value;
            }
        }

        [SRDisplayName(nameof(Resources.Tag_effect))]
        [SRComboProperty(ValueList = "dark|light|plain", DisplayList = nameof(Resources.TagCellType_effect_DisplayList))]
        public string effect { get; set; } = "light";

        [SRDisplayName(nameof(Resources.Tag_itemSpace))]
        [IntProperty(Min = 0)]
        [DefaultValue(10)]
        public int itemSpace { get; set; } = 10;

        [SRDisplayName(nameof(Resources.Tag_allowAdd))]
        [BoolProperty]
        [DefaultValue(true)]
        public bool allowAdd { get; set; } = true;

        [SRDisplayName(nameof(Resources.Tag_addButtonSettings))]
        [ObjectProperty(ObjType = typeof(AddButtonSettings), IndentLevel = 1)]
        public AddButtonSettings addButtonSettings { get; set; } = new AddButtonSettings();

        [SRCategoryHeader(nameof(Resources.Category_Options))]
        [SRDisplayName(nameof(Resources.Tag_distinct))]
        [SRDescription(nameof(Resources.TagCellType_distinct_Description))]
        [DefaultValue(true)]
        public bool distinct { get; set; } = true;

        [SRDisplayName(nameof(Resources.Tag_disableTransitions))]
        [Browsable(false)]
        public bool disableTransitions { get; set; }

        [SRDisplayName(nameof(Resources.Tag_hit))]
        public bool hit { get; set; }

        [SRDisplayName(nameof(Resources.Tag_ReadOnly))]
        public bool ReadOnly { get; set; }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(hit))
            {
                return this.effect != "dark";
            }
            if (propertyName == nameof(addButtonSettings))
            {
                return this.allowAdd;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.Tag;
        }

        public override bool NeedFormatDefaultValue => false;
    }

    public class AddButtonSettings : ObjectPropertyBase
    {
        [SRDisplayName(nameof(Resources.Tag_width))]
        [IntProperty(Min = 10)]
        public int width { get; set; } = 100;

        [SRDisplayName(nameof(Resources.Tag_text))]
        public string text { get; set; }

        [SRDisplayName(nameof(Resources.Tag_space))]
        [DefaultValue(20d)]
        [DoubleProperty(Min = 0)]
        public double space { get; set; } = 20;
    }
}
