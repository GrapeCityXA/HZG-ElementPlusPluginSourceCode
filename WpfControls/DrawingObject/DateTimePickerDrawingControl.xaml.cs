using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Plugin;
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

namespace ElementUI.WpfControls
{
    /// <summary>
    /// Interaction logic for DatePickerDrawingControl.xaml
    /// </summary>
    public partial class DateTimePickerDrawingControl : UserControl
    {
        public DateTimePickerDrawingControl(DatePickerCellType cellType, CellTypeViewModelStyle cellStyle, IDrawingHelper drawingHelper)
        {
            var viewModel = new DateTimePickerBoxDrawingControlViewModel();
            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = ColorHelper.Text;
            }
            viewModel.CellStyle = cellStyle;

            viewModel.PrefixIcon = IconHelper.GetIcon((ImageValue)cellType.prefixIcon, drawingHelper) ?? IconHelper.GetBuiltInIcon("date", drawingHelper, "#c0c4cc");
            viewModel.SuffixIcon = cellType.clearable ? IconHelper.GetBuiltInIcon("close", drawingHelper, "#c0c4cc") : null;
            viewModel.IsEnabled = !cellType.IsDisabled;
            viewModel.RangeSeparator = cellType.rangeSeparator;
            viewModel.StartPlaceholder = cellType.startPlaceholder;
            viewModel.EndPlaceholder = cellType.endPlaceholder;
            this.DataContext = viewModel;

            InitializeComponent();
        }
        public DateTimePickerDrawingControl(TimePickerCellType cellType, CellTypeViewModelStyle cellStyle, IDrawingHelper drawingHelper)
        {
            var viewModel = new DateTimePickerBoxDrawingControlViewModel();
            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = ColorHelper.Text;
            }

            viewModel.CellStyle = cellStyle;
            viewModel.PrefixIcon = IconHelper.GetIcon(cellType.prefixIcon, drawingHelper) ?? IconHelper.GetBuiltInIcon("time", drawingHelper, "#c0c4cc");
            viewModel.SuffixIcon = cellType.clearable ? IconHelper.GetBuiltInIcon("close", drawingHelper, "#c0c4cc") : null;
            viewModel.IsEnabled = !cellType.IsDisabled;
            viewModel.RangeSeparator = cellType.rangeSeparator;
            viewModel.StartPlaceholder = cellType.startPlaceholder;
            viewModel.EndPlaceholder = cellType.endPlaceholder;
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }

    public class DateTimePickerBoxDrawingControlViewModel : ModelBase
    {
        private bool _isEnabled = true;

        private CellTypeViewModelStyle _cellStyle;

        private Brush _background = Brushes.Transparent;

        public string RangeSeparator { get; set; }

        public string StartPlaceholder { get; set; }

        public string EndPlaceholder { get; set; }

        public object PrefixIcon { get; set; }

        public object SuffixIcon { get; set; }

        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    this._isEnabled = value;
                    if (!value)
                    {
                        this.CellStyle.Foreground = ColorHelper.BorderColorLightest;
                    }
                    this.OnPropertyChanged();
                }
            }
        }

        public Brush Background
        {
            get
            {
                if (this.IsEnabled == false)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F7FA"));
                }
                return this._background;
            }
            set
            {
                if (_background != value)
                {
                    this._background = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public CellTypeViewModelStyle CellStyle
        {
            get
            {
                return this._cellStyle;
            }
            set
            {
                if (_cellStyle != value)
                {
                    this._cellStyle = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Visibility PrefixIconVisibility
        {
            get
            {
                return PrefixIcon != null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility SuffixIconVisibility
        {
            get
            {
                return SuffixIcon != null ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
