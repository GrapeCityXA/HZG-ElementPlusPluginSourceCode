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
using System.ComponentModel.DataAnnotations;

namespace ElementUI
{
    public class SliderCellTypeDesigner : CellTypeDesigner<SliderCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new SliderDrawingControl(drawingHelper, this.CellType);

            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            this.CellType.min = 0;
            this.CellType.max = 100;
            this.CellType.step = 1;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Slider.png")]
    [OrderWeight((int)Utils.OrderWeight.Slider)]
    [Designer("ElementUI.SliderCellTypeDesigner, ElementUI")]
    public class SliderCellType : InputCellTypeBase
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        [Browsable(false)]
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        
        [SRDisplayName(nameof(Resources.SliderCellType_Marks))] 
        [ListProperty]
        public List<Mark> Marks { get; set; } = new List<Mark>();
        
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        #endregion

        [SRDisplayName(nameof(Resources.SliderCellType_min))]
        [FormulaProperty]
        public object min { get; set; }

        [SRDisplayName(nameof(Resources.SliderCellType_max))]
        [FormulaProperty]
        public object max { get; set; }

        [SRDisplayName(nameof(Resources.SliderCellType_step))]
        [FormulaProperty]
        public object step { get; set; }

        [SRDisplayName(nameof(Resources.layout))]
        [SRRadioGroupProperty(ValueList = "horizontal|vertical", DisplayList = nameof(Resources.layout_DisplayList))]
        public string layout { get; set; } = "horizontal";

        [SRDisplayName(nameof(Resources.SliderCellType_range))]
        [BoolProperty]
        public bool range { get; set; }

        [SRDisplayName(nameof(Resources.SliderCellType_showInput))]
        [BoolProperty]
        public bool showInput { get; set; }

        [SRDisplayName(nameof(Resources.SliderCellType_showInputControls))]
        [DefaultValue(false)]
        [BoolProperty]
        public bool showInputControls { get; set; } = false;

        [SRDisplayName(nameof(Resources.SliderCellType_showStops))]
        [SRDescription(nameof(Resources.SliderCellType_showSteps_Desciption))]
        [BoolProperty]
        public bool showStops { get; set; }

        [SRDisplayName(nameof(Resources.SliderCellType_showTooltip))]
        [DefaultValue(true)]
        [BoolProperty]
        public bool showTooltip { get; set; } = true;

        [SRDisplayName(nameof(Resources.SliderCellType_formatTooltipStr))]
        [SRFormulaProperty(ContextVariables = nameof(Resources.SliderCellType_formatTooltipStr_VariableContext))]
        public object formatTooltipStr { get; set; }

        [SRCategoryHeader(nameof(Resources.Other))]
        public override bool IsDisabled { get; set; }

        public override string ToString()
        {
            return Resources.Slider;
        }
        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(showInputControls))
            {
                return showInput == true && range == false;
            }
            if (propertyName == nameof(showInput))
            {
                return range == false;
            }
            if (propertyName == nameof(formatTooltipStr))
            {
                return showTooltip;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if(name == nameof(GetSelectedRange))
            {
                return range;
            }
            return base.GetRunTimeMethodVisible(name);
        }

        [SRDisplayName(nameof(Resources.SliderCellType_SetMinValue))]
        [RunTimeMethod]
        public void SetMinValue([SRItemDisplayName(nameof(Resources.SliderCellType_min))][Required] object value)
        {
        }
        [SRDisplayName(nameof(Resources.SliderCellType_SetMaxValue))]
        [RunTimeMethod]
        public void SetMaxValue([SRItemDisplayName(nameof(Resources.SliderCellType_max))][Required] object value)
        {
        }

        [SRDisplayName(nameof(Resources.SliderCellType_SetMarks))]
        [RunTimeMethod]
        public void SetMarks(
            [SRItemDisplayName(nameof(Resources.SliderCellType_SetMarks_Marks))] 
            [ListProperty]List<Mark> marks)
        {
            
        }
        
        [SRDisplayName(nameof(Resources.SliderCellType_GetSelectedRange))]
        [RunTimeMethod]
        public SliderRangeResult GetSelectedRange()
        {
            return null;
        }

        public class SliderRangeResult
        {
            [SRDisplayName(nameof(Resources.SliderCellType_StartValue))]
            public int StartValue { get; set; }
            [SRDisplayName(nameof(Resources.SliderCellType_EndValue))]
            public int EndValue { get; set; }
        }

        public class Mark
        {
            [SRDisplayName(nameof(Resources.Common_Value))]
            [DoubleProperty]
            public double Value { get; set; }
            
            [SRDisplayName(nameof(Resources.SliderCellType_label))]
            public string Label { get; set; }
        }
    }
}
