using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Plugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TimelineDrawingControl.xaml
    /// </summary>
    public partial class TimelineDrawingControl : UserControl
    {
        public TimelineDrawingControl(IDrawingHelper drawingHelper, TimelineCellType cellType, CellTypeViewModelStyle cellStyle, List<TimelineOptionItem> defaultOptions)
        {
            var viewModel = new TimelineDrawingControlViewModel();
            var nodes = cellType.options.Count > 0 ? cellType.options : defaultOptions;
            viewModel.HideTimestamp = cellType.hideTimestamp;
            viewModel.Placement = cellType.placement;
            viewModel.SetNodes(nodes, drawingHelper);
            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = ColorHelper.Text;
            }
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class Node
    {
        public string Content { get; set; }

        public string Timestamp { get; set; }

        public object Icon { get; set; }

        public Brush Color { get; set; }

        public Brush TextBlockForeground { get; set; } = ColorHelper.Info;

        public Visibility Visibility { get; set; } = Visibility.Visible;

        public Visibility TopTimestampVisibility { get; set; }

        public Visibility BottomTimestamptVisibility { get; set; }
    }

    public class TimelineDrawingControlViewModel : ModelBase
    {
        public bool HideTimestamp { get; set; }

        public Placement Placement { get; set; }

        public ObservableCollection<Node> Nodes { get; set; } = new ObservableCollection<Node>();

        public Visibility GetTopTimestampVisibility()
        {
            if (!HideTimestamp && Placement == Placement.top)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public Visibility GetBottomTimestampVisibility()
        {
            if (!HideTimestamp && Placement == Placement.bottom)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public void SetNodes(List<TimelineOptionItem> nodes, IDrawingHelper drawingHelper)
        {
            var topTimestampVisibility = GetTopTimestampVisibility();
            var bottomTimestampVisibility = GetBottomTimestampVisibility();

            foreach (var option in nodes)
            {
                Nodes.Add(new Node
                {
                    Content = option.content,
                    Timestamp = option.timestamp,
                    Icon = IconHelper.GetIcon((ImageValue)option.icon, drawingHelper),
                    Color = drawingHelper.GetBrush(option.color ?? "#e4e7ed"),
                    TopTimestampVisibility = topTimestampVisibility,
                    BottomTimestamptVisibility = bottomTimestampVisibility,
                });
            };
            if (Nodes.Count > 0)
            {
                Nodes[Nodes.Count - 1].Visibility = Visibility.Collapsed;
            }
        }
    }
}
