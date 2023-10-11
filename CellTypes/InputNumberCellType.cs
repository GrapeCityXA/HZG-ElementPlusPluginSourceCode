using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
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
    public class InputNumberCellTypeDesigner : CellTypeDesigner<InputNumberCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new InputNumberDrawingControl(cellInfo, drawingHelper, this.CellType, StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper));
            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            this.CellType.min = 0;
            this.CellType.max = 100;
            this.CellType.step = 1;
        }
    }


    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/InputNumber.png")]
    [OrderWeight((int)Utils.OrderWeight.InputNumber)]
    [Designer("ElementUI.InputNumberCellTypeDesigner, ElementUI")]
    public class InputNumberCellType : InputCellTypeBase
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        #endregion

        [SRDisplayName(nameof(Resources.InputNumberCellType_min))]
        [FormulaProperty]
        public object min { get; set; }

        [SRDisplayName(nameof(Resources.InputNumberCellType_max))]
        [FormulaProperty]
        public object max { get; set; }

        [SRDisplayName(nameof(Resources.InputNumberCellType_step))]
        [FormulaProperty]
        public object step { get; set; }

        [SRDisplayName(nameof(Resources.InputNumberCellType_stepStrictly))]
        public bool stepStrictly { get; set; }

        [SRDisplayName(nameof(Resources.InputNumberCellType_precision))]
        public int precision { get; set; }

        [SRDisplayName(nameof(Resources.InputNumberCellType_placeholder))]
        public string placeholder { get; set; }

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.InputNumberCellType_controls))]
        [DefaultValue(true)]
        public bool controls { get; set; } = true;

        [SRDisplayName(nameof(Resources.InputNumberCellType_controlsPosition))]
        public bool controlsPosition { get; set; }

        [SRDisplayName(nameof(Resources.InputNumberCellType_IsDisabled))]
        public override bool IsDisabled { get; set; }

        public override string ToString()
        {
            return Resources.InputNumber;
        }
        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(controlsPosition))
            {
                return controls;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        [SRDisplayName(nameof(Resources.InputNumberCellType_SetMinValue))]
        [RunTimeMethod]
        public void SetMinValue([SRItemDisplayName(nameof(Resources.InputNumberCellType_min))][Required] object value)
        {
        }
        [SRDisplayName(nameof(Resources.InputNumberCellType_SetMaxValue))]
        [RunTimeMethod]
        public void SetMaxValue([SRItemDisplayName(nameof(Resources.InputNumberCellType_max))][Required] object value)
        {
        }
        [SRDisplayName(nameof(Resources.InputNumberCellType_SetStep))]
        [RunTimeMethod]
        public void SetStep([SRItemDisplayName(nameof(Resources.InputNumberCellType_step))][Required] object value)
        {
        }
        [SRDisplayName(nameof(Resources.InputNumberCellType_Focus))]
        [RunTimeMethod]
        [SRDescription(nameof(Resources.InputNumberCellType_Focus_Description))]
        public void Focus()
        {
        }
    }
}
