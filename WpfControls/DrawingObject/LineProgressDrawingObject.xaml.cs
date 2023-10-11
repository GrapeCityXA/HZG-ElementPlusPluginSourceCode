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
    public partial class LineProgressDrawingObject : UserControl
    {
        public LineProgressDrawingObject(ProgressCellType cellType, Brush color, CellTypeViewModelStyle cellStyle, IDrawingHelper drawingHelper)
        {
            var viewModel = new LineProgressDrawingObjectViewModel();
            viewModel.DrawingHelper = drawingHelper;
            viewModel.Color = color;
            viewModel.ShowText = cellType.showText;
            viewModel.TextInside = cellType.textInside;
            viewModel.StrokeWidth = cellType.strokeWidth;
            viewModel.Status = cellType.status;
            var width = cellType.strokeWidth;
            viewModel.Corner = new CornerRadius(width / 2);
            viewModel.Height = width;
            viewModel.Text = string.IsNullOrEmpty(cellType.textFormula?.ToString()) ? "50%" : cellType.textFormula?.ToString();

            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = viewModel.TextColor;
            }

            viewModel.CellStyle = cellStyle;
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
    public class LineProgressDrawingObjectViewModel : ModelBase
    {
        public IDrawingHelper DrawingHelper { get; set; }
        private bool _textInside = true;
        public bool TextInside
        {
            get
            {
                return this._textInside;
            }
            set
            {
                if (_textInside != value)
                {
                    this._textInside = value;
                    if (value)
                    {
                        TextColor = Brushes.White;
                        TextColumn = 0;
                    }
                    else
                    {
                        TextColor = ColorHelper.Text;
                        TextColumn = 2;
                    }
                    this.OnPropertyChanged();
                }
            }
        }
        public CornerRadius Corner { get; set; }
        public Brush Color { get; set; }
        public bool ShowText { get; set; }
        public Visibility TextVisibility
        {
            get
            {
                if ((ShowText && Status == ProgressStatus.none) ||
                    (ShowText && TextInside))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }
        public Brush TextColor { get; set; } = Brushes.White;
        public Brush Background { get; set; } = ColorHelper.BorderColorLighter;
        public int TextColumn { get; set; } = 0;
        public double StrokeWidth { get; set; }
        public double Height { get; set; }

        public string Text { get; set; } = "50%";

        public double IconSize
        {
            get
            {
                return 12 + StrokeWidth * 0.4;
            }
        }

        public double TextMinWidth
        {
            get
            {
                return TextInside ? 0 : 36;
            }
        }

        public Visibility IconVisibility
        {
            get
            {
                if ((ShowText && Status != ProgressStatus.none && !TextInside))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Brush IconColor
        {
            get
            {
                switch (this.Status)
                {
                    case ProgressStatus.none:
                        break;
                    case ProgressStatus.success:
                        return ColorHelper.Success;
                    case ProgressStatus.exception:
                        return ColorHelper.Error;
                    case ProgressStatus.warning:
                        return ColorHelper.Warning;
                    default:
                        break;
                }
                return null;
            }
        }

        public object Icon
        {
            get
            {
                switch (this.Status)
                {
                    case ProgressStatus.none:
                        break;
                    case ProgressStatus.success:
                        return IconHelper.GetBuiltInIcon("circleCheck", DrawingHelper, "#67C23A");
                    case ProgressStatus.exception:
                        return IconHelper.GetBuiltInIcon("close", DrawingHelper, "#F56C6C");
                    case ProgressStatus.warning:
                        return IconHelper.GetBuiltInIcon("warning", DrawingHelper, "#E6A23C");
                    default:
                        break;
                }
                return null;
            }
        }


        public ProgressStatus Status { get; set; }


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
