using GrapeCity.Forguncy.CellTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for SliderDrawingControl.xaml
    /// </summary>
    public partial class SliderDrawingControl : UserControl
    {
        public SliderDrawingControl(IDrawingHelper drawingHelper, SliderCellType cellType)
        {
            var inputHeight = cellType.showInput ? 32 : 0;
            var width = drawingHelper.GetImageCellSize().Width - 24 - (cellType.showInput ? 132 : 0);
            var height = drawingHelper.GetImageCellSize().Height - 24 - inputHeight; 
            if (width < 0)
            {
                InitializeComponent();
                return;
            }
            var viewModel = new SliderDrawingControlViewModel();
            viewModel.Margin = cellType.layout == "vertical" ?  new Thickness(0, 12, 0, 12) : new Thickness(12, 0, 12, 0);
            viewModel.Disabled = cellType.IsDisabled;
            viewModel.ShowInput = cellType.showInput && !cellType.range;
            viewModel.Vertical = cellType.layout == "vertical";
            viewModel.ShowInputControls = cellType.showInputControls;
            viewModel.IsRange = cellType.range;
            double.TryParse((cellType.min ?? 0).ToString(), out var min);

            if (!double.TryParse((cellType.max ?? 0).ToString(), out var max))
            {
                max = 100;
            }
            if (cellType.Marks != null && cellType.Marks.Count > 0)
            {
                if (min < max)
                {
                    foreach (var mark in cellType.Marks)
                    {
                        if (mark.Value < min || mark.Value > max)
                        {
                            continue;
                        }

                        viewModel.Stops.Add(new StopViewModel()
                        {
                            Left = width * (mark.Value - min) / (max - min),
                            Top = height - height * (mark.Value - min) / (max - min)
                        });

                        var textBlock = new TextBlock { Text = mark.Label, FontSize = 14 };
                        // auto sized
                        textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        textBlock.Arrange(new Rect(textBlock.DesiredSize));

                        viewModel.MarkerText.Add(new MarkerTextViewModel()
                        {
                            MarkerText = mark.Label,
                            Left = width * (mark.Value - min) / (max - min) - textBlock.ActualWidth / 2,
                            Top = height - (height * (mark.Value - min) / (max - min)) - textBlock.ActualHeight / 2,
                        });
                    }
                }
            }
            if (cellType.showStops)
            {
                for (int i = 6; i < 10; i++)
                {
                    viewModel.Stops.Add(new StopViewModel()
                    {
                        Left = width * i / 10,
                        Top = height * (i - 5) / 10
                    });
                }
            }
            viewModel.ShowStops = viewModel.Stops.Count > 0;

            viewModel.BorderBrush = drawingHelper.GetBrush("Accent 1");
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class SliderDrawingControlViewModel : ModelBase
    {
        public Thickness Margin { get; set; }

        private bool _showInput;

        public bool ShowInput
        {
            get
            {
                if (_disabled)
                {
                    return false;
                }
                return _showInput;
            }
            set
            {
                if (_showInput != value)
                {
                    _showInput = value;
                    if (_showInput == false)
                    {
                        IsRange = false;
                    }
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(InputControlsVisibility));
                }
            }
        }

        private bool _vertical;

        public bool Vertical
        {
            get
            {
                return _vertical;
            }
            set
            {
                if (_vertical != value)
                {
                    _vertical = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _showInputControls;

        public bool ShowInputControls
        {
            get
            {
                if (_disabled)
                {
                    return false;
                }
                return _showInputControls;
            }
            set
            {
                if (_showInputControls != value)
                {
                    _showInputControls = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(InputControlsVisibility));
                }
            }
        }

        private bool _disabled;

        public bool Disabled
        {
            get
            {
                return _disabled;
            }
            set
            {
                _disabled = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(SliderBarColor));
            }
        }

        private bool _isRange;

        public bool IsRange
        {
            get
            {
                return _isRange;
            }
            set
            {
                _isRange = value;
                OnPropertyChanged();
            }
        }

        private bool _showStops;

        public bool ShowStops
        {
            get
            {
                if (_disabled)
                {
                    return false;
                }
                return _showStops;
            }
            set
            {
                _showStops = value;
                OnPropertyChanged();
            }
        }

        public string SliderBarColor
        {
            get
            {
                return Disabled ? "#C0C4CC" : BorderBrush.ToString();
            }
        }

        public ObservableCollection<StopViewModel> _stops = new ObservableCollection<StopViewModel>();

        public ObservableCollection<StopViewModel> Stops
        {
            get
            {
                return _stops;
            }
            set
            {
                _stops = value;
                OnPropertyChanged();
            }
        }

        public List<MarkerTextViewModel> MarkerText { get; set; } = new List<MarkerTextViewModel>();

        public bool InputControlsVisibility
        {
            get
            {
                return ShowInput && ShowInputControls;
            }
        }

        public Brush BorderBrush { get; set; }
    }

    public class StopViewModel
    {
        public double Left { get; set; }
        public double Top { get; set; }
    }
    public class MarkerTextViewModel
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public string MarkerText { get; set; }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class BoolToVisibilityInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
