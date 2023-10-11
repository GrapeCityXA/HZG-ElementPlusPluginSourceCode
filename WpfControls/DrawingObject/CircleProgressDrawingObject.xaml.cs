using GrapeCity.Forguncy.CellTypes;
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
    /// Interaction logic for LineProgressDrawingObject.xaml
    /// </summary>
    public partial class CircleProgressDrawingObject : UserControl
    {
        public CircleProgressDrawingObject(IDrawingHelper drawingHelper, ProgressCellType cellType,
            Brush color, CellTypeViewModelStyle cellStyle)
        {
            var viewModel = new CircleProgressDrawingObjectViewModel();
            viewModel.DrawingHelper = drawingHelper;
            viewModel.Color = color;
            viewModel.StrokeWidth = cellType.strokeWidth;
            viewModel.isDashboard = cellType.type == ProgressType.dashboard;
            viewModel.Init(cellType);

            var constraint = drawingHelper.GetImageCellSize();
            var minSize = Math.Min(constraint.Width, constraint.Height);
            viewModel.Corner = new CornerRadius(minSize / 2);
            viewModel.ProgressCorner = new CornerRadius(0, minSize, 0, 0);
            viewModel.Width = minSize;
            viewModel.Height = minSize;
            if (viewModel.isDashboard)
            {
                viewModel.Clip = new RectangleGeometry(new Rect(0, 0, minSize, minSize * 0.85));
                viewModel.ProgressCorner = new CornerRadius(0, 0, 0, minSize);
            }

            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = ColorHelper.Text;
            }

            viewModel.CellStyle = cellStyle;

            viewModel.FontSize = 2 + minSize / 100 * 11.111111;

            this.DataContext = viewModel;
            InitializeComponent();

        }
    }
    public class CircleProgressDrawingObjectViewModel : ModelBase
    {
        public IDrawingHelper DrawingHelper { get; set; }
        public bool isDashboard { get; set; } = false;
        public CornerRadius Corner { get; set; }
        public CornerRadius ProgressCorner { get; set; }
        public Brush Color { get; set; }
        public Brush Background { get; set; } = ColorHelper.BorderColorLighter;
        public Visibility ShowText { get; set; } = Visibility.Collapsed;
        public Visibility ShowIcon { get; set; } = Visibility.Collapsed;
        public object Icon { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Geometry Clip { get; set; }
        public Visibility CircleVisibility { get => isDashboard ? Visibility.Collapsed : Visibility.Visible; }
        public Visibility DashboardVisibility { get => isDashboard ? Visibility.Visible : Visibility.Collapsed; }
        public string Text { get; set; }
        public double FontSize { get; set; }
        public void Init(ProgressCellType cellType)
        {
            switch (cellType.status)
            {
                case ProgressStatus.none:
                    this.ShowText = cellType.showText ? Visibility.Visible : Visibility.Collapsed;
                    break;
                case ProgressStatus.success:
                    Icon = IconHelper.GetBuiltInIcon("circleCheck", DrawingHelper, "#67C23A");
                    this.ShowIcon = Visibility.Visible;
                    break;
                case ProgressStatus.exception:
                    Icon = IconHelper.GetBuiltInIcon("close", DrawingHelper, "#F56C6C");
                    this.ShowIcon = Visibility.Visible;
                    break;
                case ProgressStatus.warning:
                    Icon = IconHelper.GetBuiltInIcon("warning", DrawingHelper, "#E6A23C");
                    this.ShowIcon = Visibility.Visible;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(cellType.textFormula?.ToString()))
            {
                Text = cellType.textFormula?.ToString();
            }
            else if (cellType.type == ProgressType.circle)
            {
                Text = "25%";
            }
            else
            {
                Text = "20%";
            }
        }

        private double strokeWidth;

        public double StrokeWidth
        {
            get { return strokeWidth; }
            set
            {
                strokeWidth = value;
                BorderThickness = new Thickness(value);
                BorderThickness2 = new Thickness(0, value, value, 0);
                BorderThickness3 = new Thickness(value, 0, 0, value);
            }
        }


        public Thickness BorderThickness { get; set; }
        public Thickness BorderThickness2 { get; set; }
        public Thickness BorderThickness3 { get; set; }

        private CellTypeViewModelStyle _cellStyle;
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
    }
}
