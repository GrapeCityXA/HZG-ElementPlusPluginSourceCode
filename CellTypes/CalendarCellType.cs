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
    public class CalendarCellTypeDesigner : CellTypeDesigner<CalendarCellType>
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new CalendarCellTypeDrawingControl(drawingHelper, this.CellType);
            return control;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Calendar.png")]
    [Designer("ElementUI.CalendarCellTypeDesigner, ElementUI")]
    [OrderWeight((int)Utils.OrderWeight.Calendar)]
    public class CalendarCellType : ElementCellTypeBase, ICommandCellType, ISupportDefaultValue
    {
        [Browsable(false)]
        public List<Command> CommandList
        {
            get
            {
                return ((CustomCommandObject)ValueChangedCommand)?.Commands;
            }
            set { }
        }

        [SRDisplayName(nameof(Resources.Common_DefaultValue))]
        [FormulaProperty]
        public object DefaultValue { get; set; }

        [SRCategoryHeader(nameof(Resources.CalendarProjectCommand))]
        [SRDisplayName(nameof(Resources.CalendarCellType_CommandList))]
        [CustomCommandObject(IndentLevel = 1)]
        public object ValueChangedCommand { get; set; }

        public CommandExcuteKind CommandExcuteKind => CommandExcuteKind.OnValueChanged;

        [SRDisplayName(nameof(Resources.CalendarCellType_DBClickCommand))]
        [SRCustomCommandObject(InitParamProperties = "date", IndentLevel = 1, InitParamValues = nameof(Resources.CalendarCellType_DBClickCommand_InitParamValues))]
        public object DBClickCommand { get; set; }

        [SRCategoryHeader(nameof(Resources.CalendarScheduleCommand))]
        [SRDisplayName(nameof(Resources.CalendarCellType_ClickCommand))]
        [SRCustomCommandObject(InitParamProperties = "value|date|text", IndentLevel = 1, InitParamValues = nameof(Resources.CalendarCellType_Schedul_InitParamValues))]
        public object ScheduleClickCommand { get; set; }

        [SRDisplayName(nameof(Resources.CalendarCellType_DBClickCommand))]
        [SRCustomCommandObject(InitParamProperties = "value|date|text", IndentLevel = 1, InitParamValues = nameof(Resources.CalendarCellType_Schedul_InitParamValues))]
        public object ScheduleDBClickCommand { get; set; }

        [SRBindingDataSourceProperty(Columns = nameof(Resources.CalendarCellType_bindingOptions_Columns))]
        [SRDisplayName(nameof(Resources.CalendarCellType_bindingOptions))]
        public object bindingOptions { get; set; }

        [SRDisplayName(nameof(Resources.CalendarCellType_firstDayOfWeek))]
        [SRComboProperty(ValueList = "1|2|3|4|5|6|7", DisplayList = nameof(Resources.DatePickerCellType_firstDayWeek_DisplayList))]
        public int firstDayOfWeek { get; set; } = 1;

        public bool NeedFormatDefaultValue => true;

        public override string ToString()
        {
            return Resources.Calendar;
        }
    }

    public class CalendarOptionItem
    {
        [SRDisplayName(nameof(Resources.CalendarCellType_date))]
        public string date { get; set; }

        [SRDisplayName(nameof(Resources.CalendarCellType_text))]
        public string text { get; set; }

    }
}
