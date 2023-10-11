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
    public class DatePickerCellTypeDesigner : CellTypeDesigner<DatePickerCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var cellStyle = StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper);

            if (CellType.type.Contains("range"))
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
            this.CellType.startPlaceholder = Resources.DatePickerCellType_startPlaceholder_DefaultValue;
            this.CellType.rangeSeparator = Resources.DatePickerCellType_rangeSeparator_DefaultValue;
            this.CellType.endPlaceholder = Resources.DatePickerCellType_endPlaceholder_DefaultValue;
            this.CellType.DefautWeekFormat = Resources.Data_DefautWeekFormat;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/DatePicker.png")]
    [Designer("ElementUI.DatePickerCellTypeDesigner, ElementUI")]
    [OrderWeight((int)Utils.OrderWeight.DatePicker)]
    public class DatePickerCellType : InputCellTypeBase, ISupportReadOnly
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        #endregion


        [SRDisplayName(nameof(Resources.DatePickerCellType_type))]
        [SRComboProperty(ValueList = "year|month|date|week|datetime|monthrange|daterange|datetimerange", DisplayList = nameof(Resources.DatePickerCellType_type_DisplayList))]
        public string type { get; set; } = "date";

        [SRDisplayName(nameof(Resources.DatePickerCellType_placeholder))]
        public string placeholder { get; set; }

        [SRDisplayName(nameof(Resources.DatePickerCellType_startPlaceholder))]
        public string startPlaceholder { get; set; }

        [SRDisplayName(nameof(Resources.DatePickerCellType_rangeSeparator))]
        public string rangeSeparator { get; set; }

        [SRDisplayName(nameof(Resources.DatePickerCellType_endPlaceholder))]
        public string endPlaceholder { get; set; }

        [SRDisplayName(nameof(Resources.DatePickerCellType_format))]
        [SRComboProperty(ValueList = nameof(Resources.DatePickerCellType_format_ValueList), IsSelectOnly = false)]
        public string format { get; set; }

        [SRComboProperty(ValueList = "1|2|3|4|5|6|7", DisplayList = nameof(Resources.DatePickerCellType_firstDayWeek_DisplayList))]
        [SRDisplayName(nameof(Resources.DatePickerCellType_firstDayOfWeek))]
        public int firstDayOfWeek { get; set; } = 1;

        private object _prefixIcon;

        [SRDisplayName(nameof(Resources.DatePickerCellType_prefixIcon))]
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

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.DatePickerCellType_clearable))]
        public bool clearable { get; set; }

        [SRDisplayName(nameof(Resources.DatePickerCellType_editable))]
        public bool editable { get; set; }

        [SRDisplayName(nameof(Resources.DatePickerCellType_ReadOnly))]
        public bool ReadOnly { get; set; }

        public override bool IsDisabled { get; set; }

        [Browsable(false)]
        public string DefautWeekFormat { get; set; }

        public override string ToString()
        {
            return Resources.DatePicker;
        }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(rangeSeparator) || propertyName == nameof(startPlaceholder) || propertyName == nameof(endPlaceholder))
            {
                return type.Contains("range");
            }

            if (propertyName == nameof(placeholder))
            {
                return !type.Contains("range");
            }
            if (propertyName == nameof(firstDayOfWeek))
            {
                return type.Contains("date") || type == "week";
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        [SRDisplayName(nameof(Resources.DatePickerCellType_GetSelectedRange))]
        [RunTimeMethod]
        public DatePickerRangeResult GetSelectedRange()
        {
            return null;
        }

        public class DatePickerRangeResult
        {
            [SRDisplayName(nameof(Resources.DatePickerCellType_StartValue))]
            public int StartValue { get; set; }

            [SRDisplayName(nameof(Resources.DatePickerCellType_EndValue))]
            public int EndValue { get; set; }
        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(GetSelectedRange))
            {
                return type.Contains("range");
            }
            return base.GetRunTimeMethodVisible(name);
        }
    }
}
