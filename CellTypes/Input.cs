using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ElementUI.Properties;

namespace ElementUI
{
    public class InputCellTypeDesigner : CellTypeDesigner<InputCellType>
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new InputBoxDrawingControl(cellInfo, drawingHelper, StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper), this.CellType);
            return control;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Input.png")]
    [OrderWeight((int)Utils.OrderWeight.Input)]
    [Designer("ElementUI.InputCellTypeDesigner, ElementUI")]
    public class InputCellType : InputCellTypeBase, ISupportReadOnly
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        #endregion

        [SRDisplayName(nameof(Resources.Input_type))]
        [SRRadioGroupProperty(ValueList = "text|textarea|password", DisplayList = nameof(Resources.InputCellType_type_DisplayList))]
        public string type { get; set; } = "text";

        [SRDisplayName(nameof(Resources.Input_maxlength))]
        [FormulaProperty]
        public object maxlength { get; set; }

        [SRDisplayName(nameof(Resources.Input_showWordLimit))]
        public bool showWordLimit { get; set; }

        private object _prefixIcon;

        private object _suffixIcon;

        [SRDisplayName(nameof(Resources.Input_placeholder))]
        [SearchableProperty]
        public string placeholder { get; set; }
        [SRDisplayName(nameof(Resources.Input_prefixIcon))]
        [IconProperty]
        public object prefixIcon
        {
            get
            {
                return _prefixIcon;
            }
            set
            {
                if (value is ImageValue || value is null)
                {
                    _prefixIcon = value;
                }
            }
        }

        [SRDisplayName(nameof(Resources.Input_suffixIcon))]
        [IconProperty]
        public object suffixIcon
        {
            get
            {
                return _suffixIcon;
            }
            set
            {
                if (value is ImageValue || value is null)
                {
                    _suffixIcon = value;
                }
            }
        }

        [SRDisplayName(nameof(Resources.Input_resize))]
        [SRDescription(nameof(Resources.InputCellType_resize_Description))]
        [SRComboProperty(ValueList = "none|horizontal|vertical|both", DisplayList = nameof(Resources.InputCellType_resize_DisplayList))]
        public string resize { get; set; } = "none";

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.Input_ReadOnly))]
        public bool ReadOnly { get; set; }

        public override bool IsDisabled { get; set; }

        [SRDisplayName(nameof(Resources.Input_clearable))]
        public bool clearable { get; set; }

        [SRDisplayName(nameof(Resources.Input_showPassword))]
        [Browsable(false)]
        public bool showPassword
        {
            get
            {
                return this.type == "password";
            }
            set
            {
                if (value)
                {
                    this.type = "password";
                }
            }
        }

        public override bool NeedFormatDefaultValue => false;

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(showWordLimit))
            {
                return maxlength != null && type != "password";
            }
            if (propertyName == nameof(showPassword))
            {
                return type == "text";
            }
            if (propertyName == nameof(resize))
            {
                return type == "textarea";
            }
            if (propertyName == nameof(prefixIcon) || propertyName == nameof(suffixIcon))
            {
                return type != "textarea";
            }
            if (propertyName == nameof(clearable))
            {
                return type != "textarea";
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.Input;
        }

        [SRDisplayName(nameof(Resources.Input_Focus))]
        [RunTimeMethod]
        [SRDescription(nameof(Resources.InputCellType_Focus_Description))]
        public void Focus()
        {
        }

        [SRDisplayName(nameof(Resources.Input_Select))]
        [SRDescription(nameof(Resources.InputCellType_Select_Description))]
        [RunTimeMethod]
        public void Select()
        {
        }
    }
}
