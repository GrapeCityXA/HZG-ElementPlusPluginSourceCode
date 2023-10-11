using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using GrapeCity.Forguncy.CellTypes;

namespace ElementUI.WpfControls
{
    /// <summary>
    /// Interaction logic for CalendarCellTypeDrawingControl.xaml
    /// </summary>
    public partial class CalendarCellTypeDrawingControl : UserControl
    {
        public CalendarCellTypeDrawingControl(IDrawingHelper drawingHelper, CalendarCellType cellType)
        {
            var viewModel = new CalendarCellTypeDrawingControlViewModel(drawingHelper);
            viewModel.FirstDayOfWeek = cellType.firstDayOfWeek;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class CalendarCellTypeDrawingControlViewModel : ModelBase
    {
        IDrawingHelper _drawingHelper;
        public CalendarCellTypeDrawingControlViewModel(IDrawingHelper drawingHelper)
        {
            _drawingHelper = drawingHelper;
            _weekList.AddRange(Properties.Resources.CalendarCellType_Weekdays.Split(','));
            // 添加两遍，方便处理第一日不是周一的情况
            _weekList.AddRange(Properties.Resources.CalendarCellType_Weekdays.Split(','));
        }
        
        private List<string> _weekList = new List<string>();

        private int _totalDay = 42;

        private int _firstDayOfWeek;

        public int FirstDayOfWeek
        {
            get
            {
                return _firstDayOfWeek;
            }
            set
            {
                if (_firstDayOfWeek != value)
                {
                    _firstDayOfWeek = value;
                    SetWeek();
                    SetDay();
                    OnPropertyChanged();
                }
            }
        }

        private string _today = DateTime.Now.ToString(Properties.Resources.CalendarCellType_DefaultFormatString);

        public string Today
        {
            get
            {
                return _today;
            }
            set
            {
                if (_today == value)
                {
                    _today = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _week = new ObservableCollection<string>();

        public ObservableCollection<string> Week
        {
            get { return _week; }
            set {  _week = value; OnPropertyChanged(); }    
        }


        private ObservableCollection<DayInfo> _day = new ObservableCollection<DayInfo>();

        public ObservableCollection<DayInfo> Day
        {
            get { return _day; }
            set { _day = value; OnPropertyChanged(); }
        }

        private void SetWeek()
        {
            Week.Clear();
            for (var i = FirstDayOfWeek - 1; i < FirstDayOfWeek + 6; i++)
            {
                Week.Add(_weekList[i]);
            }
        }

        private void SetDay()
        {
            Day.Clear();

            var format = "dd";
            var today = DateTime.Now;
            // 当前日期
            var first = new DateTime(today.Year, today.Month, 1);
            var dayOfWeek = (int)first.DayOfWeek;
            var currentDay = dayOfWeek == 0 ? 6 : dayOfWeek - 1;
            var lastMonthDays = Week.IndexOf(_weekList[currentDay]);

            for (var i = lastMonthDays; i > 0; i--)
            {
                Day.Add(new DayInfo
                {
                    Day = first.AddDays(0 - i).ToString(format),
                    Color = "#c0c4cc"
                });
            }

            var daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

            for (var i = 0; i < daysInMonth; i++)
            {
                if (i == today.Day - 1)
                {
                    Day.Add(new DayInfo
                    {
                        Day = first.AddDays(i).ToString(format),
                        Color = _drawingHelper.GetBrush("Accent 1")?.ToString()
                    });
                    continue;
                }

                Day.Add(new DayInfo
                {
                    Day = first.AddDays(i).ToString(format),
                    Color = "#000000"
                });
            }

            var count = _totalDay - Day.Count;
            for (var i = 0; i < count; i++)
            {
                Day.Add(new DayInfo
                {
                    Day = first.AddDays(daysInMonth +  i).ToString(format),
                    Color = "#c0c4cc"
                });
            }

        }
    }

    public class DayInfo
    {
        public string Day { get; set; }
        public string Color { get; set; }
    }
}
