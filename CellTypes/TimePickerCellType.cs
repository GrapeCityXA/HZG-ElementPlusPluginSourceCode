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

namespace ElementUI
{

    public class TimePickerCellTypeDesigner : CellTypeDesigner<TimePickerCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var cellStyle = StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper);

            if (CellType.isRange && CellType.mode == "picker")
            {
                var control = new DateTimePickerDrawingControl(this.CellType, cellStyle, drawingHelper);
                return control;
            }
            else
            {
                var control = new InputBoxDrawingControl(cellInfo, drawingHelper, cellStyle, this.CellType);
                return control;
            }
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            this.CellType.startPlaceholder = Resources.TimePickerCellType_startPlaceholder_DefaultValue;
            this.CellType.rangeSeparator = Resources.TimePickerCellType_rangeSeparator_DefaultValue;
            this.CellType.endPlaceholder = Resources.TimePickerCellType_endPlaceholder_DefaultValue;
            this.CellType.StartTime = new TimeSpan(9, 0, 0);
            this.CellType.EndTime = new TimeSpan(18, 0, 0);
            this.CellType.step = new TimeSpan(0, 15, 0);
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/TimePicker.png")]
    [OrderWeight((int)Utils.OrderWeight.TimePicker)]
    [Designer("ElementUI.TimePickerCellTypeDesigner, ElementUI")]
    public class TimePickerCellType : InputCellTypeBase, ISupportReadOnly
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }

        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        #endregion

        [SRDisplayName(nameof(Resources.TimePickerCellType_StartTime))]
        public TimeSpan StartTime { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_EndTime))]
        public TimeSpan EndTime { get; set; }


        [SRComboProperty(ValueList = "select|picker", DisplayList = nameof(Resources.TimePickerCellType_mode_DisplayList))]
        [SRDisplayName(nameof(Resources.TimePickerCellType_mode))]
        public string mode { get; set; } = "picker";

        [SRDisplayName(nameof(Resources.TimePickerCellType_step))]
        public TimeSpan step { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_isRange))]
        [BoolProperty]
        public bool isRange { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_startPlaceholder))]
        public string startPlaceholder { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_rangeSeparator))]
        public string rangeSeparator { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_endPlaceholder))]
        public string endPlaceholder { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_placeholder))]
        public string placeholder { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_prefixIcon))]
        [IconProperty]
        public ImageValue prefixIcon { get; set; }

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.TimePickerCellType_clearable))]
        [DefaultValue(true)]
        public bool clearable { get; set; } = true;

        [SRDisplayName(nameof(Resources.TimePickerCellType_editable))]
        public bool editable { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_ReadOnly))]
        public bool ReadOnly { get; set; }

        [SRDisplayName(nameof(Resources.TimePickerCellType_IsDisabled))]
        public override bool IsDisabled { get; set; }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(step))
            {
                return mode == "select";
            }
            if (propertyName == nameof(isRange))
            {
                return mode == "picker";
            }
            if (propertyName == nameof(placeholder))
            {
                return !isRange || mode == "select";
            }

            if (propertyName == nameof(rangeSeparator) || propertyName == nameof(startPlaceholder) || propertyName == nameof(endPlaceholder))
            {
                return isRange && mode == "picker";
            }

            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.TimePicker;
        }

        [SRDisplayName(nameof(Resources.TimePickerCellType_GetSelectedRange))]
        [RunTimeMethod]
        public DatePickerRangeResult GetSelectedRange()
        {
            return null;
        }

        public class DatePickerRangeResult
        {
            [SRDisplayName(nameof(Resources.TimePickerCellType_StartValue))]
            public int StartValue { get; set; }

            [SRDisplayName(nameof(Resources.TimePickerCellType_EndValue))]
            public int EndValue { get; set; }
        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(GetSelectedRange))
            {
                return isRange && mode == "picker";
            }
            return base.GetRunTimeMethodVisible(name);
        }
    }
}


