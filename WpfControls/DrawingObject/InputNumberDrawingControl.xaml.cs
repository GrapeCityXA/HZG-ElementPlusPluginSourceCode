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
    /// Interaction logic for InputBoxDrawingControl.xaml
    /// </summary>
    public partial class InputNumberDrawingControl : UserControl
    {
        public InputNumberDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper, InputNumberCellType cellType, CellTypeViewModelStyle cellStyle)
        {
            var viewModel = new InputNumberDrawingControlViewModel(drawingHelper);

            var text = cellInfo.Text;
            viewModel.CellStyle = cellStyle;
            viewModel.Text = drawingHelper.GetHelpTextForPage() ?? text;
            viewModel.PlaceHolder = cellType.placeholder;
            viewModel.IsEnabled = !cellType.IsDisabled;
            viewModel.ShowControl = cellType.controls;
            viewModel.ShowControlRight = cellType.controlsPosition;
            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = ColorHelper.Text;
            }
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class InputNumberDrawingControlViewModel : ModelBase
    {
        public InputNumberDrawingControlViewModel(IDrawingHelper drawingHelper)
        {
            minusIcon = IconHelper.GetBuiltInIcon("minus", drawingHelper);
            plusIcon = IconHelper.GetBuiltInIcon("plus", drawingHelper);
            upIcon = IconHelper.GetBuiltInIcon("up", drawingHelper);
            downIcon = IconHelper.GetBuiltInIcon("down", drawingHelper);
        }

        private string _text;
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if (_text != value)
                {
                    this._text = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(PlaceHolderVisibility));
                }
            }
        }

        private string _placeHolder;
        public string PlaceHolder
        {
            get
            {
                return this._placeHolder;
            }
            set
            {
                if (_placeHolder != value)
                {
                    this._placeHolder = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private bool _showControl;
        public bool ShowControl
        {
            get
            {
                return this._showControl;
            }
            set
            {
                if (_showControl != value)
                {
                    this._showControl = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(PlusMinusIconVisibility));
                    this.OnPropertyChanged(nameof(SpinIconVisbility));
                }
            }
        }

        private bool _showControlRight;
        public bool ShowControlRight
        {
            get
            {
                return this._showControlRight;
            }
            set
            {
                if (_showControlRight != value)
                {
                    this._showControlRight = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(PlusMinusIconVisibility));
                    this.OnPropertyChanged(nameof(SpinIconVisbility));
                }
            }
        }

        public Visibility PlaceHolderVisibility
        {
            get
            {
                return string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility PlusMinusIconVisibility
        {
            get
            {
                return ShowControl && !ShowControlRight ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility SpinIconVisbility
        {
            get
            {
                return ShowControl && ShowControlRight ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private bool _isEnabled = true;
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

        private Brush _background = Brushes.Transparent;
        public Brush Background
        {
            get
            {
                if(this.IsEnabled == false)
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

        public object minusIcon { get; set; }
        public object plusIcon { get; set; }
        public object upIcon { get; set; }
        public object downIcon { get; set; }
    }
}
