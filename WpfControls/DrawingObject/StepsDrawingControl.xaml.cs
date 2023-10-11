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
    /// Interaction logic for StepsDrawingControl.xaml
    /// </summary>
    public partial class StepsDrawingControl : UserControl
    {
        public StepsDrawingControl(IDrawingHelper drawingHelper, StepsCellType cellType)
        {
            var viewModel = new StepsDrawingControlViewModel();
            var options = new List<StepsOptionlItem>();
            options.Add(new StepsOptionlItem()
            {
                title = Properties.Resources.StepsCellType_step1,
                description = Properties.Resources.StepsCellType_step1_Description,
            });
            options.Add(new StepsOptionlItem()
            {
                title = Properties.Resources.StepsCellType_step2,
                description = Properties.Resources.StepsCellType_step2_Description,
            });
            options.Add(new StepsOptionlItem()
            {
                title = Properties.Resources.StepsCellType_step3,
                description = Properties.Resources.StepsCellType_step3_Description,
            });
            viewModel.Vertical = cellType.layout == "vertical";
            viewModel.Center = cellType.alignCenter;
            viewModel.Simple = cellType.simple && cellType.layout != "vertical";

            viewModel.SetNodes(options, drawingHelper);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class StepsDrawingControlViewModel : ModelBase
    {
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

        private bool _center;
        public bool Center
        {
            get
            {
                return _center;
            }
            set
            {
                if (_center != value)
                {
                    _center = value;
                    if (Nodes != null)
                    {
                        foreach (var node in Nodes)
                        {
                            node.Center = _center;
                        }
                    }
                    OnPropertyChanged();
                }
            }
        }

        private bool _simple;

        public bool Simple
        {
            get
            {
                return _simple;
            }
            set
            {
                if (_simple != value)
                {
                    _simple = value;

                    if (_simple && Nodes != null && Nodes.LastOrDefault() != _empty)
                    {
                        Nodes.Add(_empty);
                    }
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<StepsNode> _nodes;

        public ObservableCollection<StepsNode> Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                if (_nodes != value)
                {
                    _nodes = value;
                    OnPropertyChanged();
                }
            }
        }

        private StepsNode _empty { get; set; } = new StepsNode { BorderVisibility = false };

        public void SetNodes(List<StepsOptionlItem> items, IDrawingHelper drawingHelper)
        {
            Nodes = new ObservableCollection<StepsNode>();

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                Nodes.Add(new StepsNode
                {
                    Description = item.description,
                    Title = item.title,
                    Icon = i == 0 ? IconHelper.GetBuiltInIcon("circleCheckFull", drawingHelper, "#C0C4CC") : IconHelper.GetBuiltInIcon("circleCheck", drawingHelper, "#C0C4CC"),
                    BorderVisibility = item != items.LastOrDefault(),
                    LeftBorderVisibility = item != items.FirstOrDefault(),
                    FontColor = item == items.FirstOrDefault() ? "#303133" : "#C0C4CC",
                    Center = Center
                });
            }

            if (Simple)
            {
                Nodes.Add(_empty);
            }
        }
    }

    public class StepsNode
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public object Icon { get; set; }

        public bool BorderVisibility { get; set; }

        public string FontColor { get; set; }

        public bool Center { get; set; }

        public bool LeftBorderVisibility { get; set; }
    }


    public class BoolToHorizontalAlignmentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return HorizontalAlignment.Center;
            }

            return HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
