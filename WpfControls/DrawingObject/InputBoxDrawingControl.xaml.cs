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
    /// Interaction logic for InputBoxDrawingControl.xaml
    /// </summary>
    public partial class InputBoxDrawingControl : UserControl
    {
        public InputBoxDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper, CellTypeViewModelStyle cellStyle, InputCellTypeBase cellType)
        {
            var viewModel = new InputBoxDrawingControlViewModel();
            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = ColorHelper.Text;
            }
            viewModel.CellStyle = cellStyle;
            viewModel.PlaceholderCellStyle =  (CellTypeViewModelStyle)cellStyle.Clone();
            viewModel.PlaceholderCellStyle.Foreground = ColorHelper.BorderColorLightest;

            var text = cellInfo.Text;
            viewModel.Text = drawingHelper.GetHelpTextForPage() ?? text;
            viewModel.IsEnabled = !cellType.IsDisabled;

            switch (cellType)
            {
                case CascaderCellType cascader:
                    viewModel.SuffixIcon = IconHelper.GetBuiltInIcon("arrow-down", drawingHelper, "#c0c4cc");
                    viewModel.PlaceHolder = cascader.placeholder;
                    break;
                case SelectCellType select:
                    viewModel.SuffixIcon = IconHelper.GetBuiltInIcon("arrow-down", drawingHelper, "#c0c4cc");
                    viewModel.PlaceHolder = select.placeholder;
                    break;
                case InputCellType input:
                    if (input.showPassword && text != null)
                    {
                        text = string.Join("", text.Select(i => "*"));
                    }
                    viewModel.Text = drawingHelper.GetHelpTextForPage() ?? text;
                    if (input.type != "textarea")
                    {
                        viewModel.SuffixIcon = IconHelper.GetIcon((ImageValue)input.suffixIcon, drawingHelper);
                        viewModel.PrefixIcon = IconHelper.GetIcon((ImageValue)input.prefixIcon, drawingHelper);
                    }
                    viewModel.PlaceHolder = input.placeholder;

                    break;
                case DatePickerCellType dataPicker:
                    viewModel.PrefixIcon = IconHelper.GetIcon((ImageValue)dataPicker.prefixIcon, drawingHelper) ?? IconHelper.GetBuiltInIcon("date", drawingHelper, "#c0c4cc");
                    viewModel.SuffixIcon = dataPicker.clearable ? IconHelper.GetBuiltInIcon("close", drawingHelper, "#c0c4cc") : null;
                    viewModel.PlaceHolder = dataPicker.placeholder;
                    break;
                case TimePickerCellType timePicker:
                    var prefixIcon = IconHelper.GetIcon(timePicker.prefixIcon, drawingHelper);
                    if (prefixIcon == null)
                    {
                        if (timePicker.mode == "picker")
                        {
                            prefixIcon = IconHelper.GetBuiltInIcon("time", drawingHelper, "#c0c4cc");
                        }
                    }
                    viewModel.PrefixIcon = prefixIcon;

                    object suffixIcon = null;
                    if (timePicker.mode == "picker")
                    {
                        if (timePicker.clearable)
                        {
                            suffixIcon = IconHelper.GetBuiltInIcon("close", drawingHelper, "#c0c4cc");
                        }
                    }
                    else
                    {
                        suffixIcon = IconHelper.GetBuiltInIcon("arrow-down", drawingHelper, "#c0c4cc");
                    }

                    viewModel.SuffixIcon = suffixIcon;
                    viewModel.PlaceHolder = timePicker.placeholder;
                    break;
                default:
                    break;
            }
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class InputBoxDrawingControlViewModel : ModelBase
    {
        public InputBoxDrawingControlViewModel()
        {
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

        private object _prefixIcon;
        public object PrefixIcon
        {
            get
            {
                return this._prefixIcon;
            }
            set
            {
                if (_prefixIcon != value)
                {
                    this._prefixIcon = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(SuffixIconVisibility));
                }
            }
        }


        private object _suffixIcon;
        public object SuffixIcon
        {
            get
            {
                return this._suffixIcon;
            }
            set
            {
                if (_suffixIcon != value)
                {
                    this._suffixIcon = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(PrefixIconVisibility));
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
        
        private CellTypeViewModelStyle _placeholderCellStyle;

        public CellTypeViewModelStyle PlaceholderCellStyle
        {
            get
            {
                return this._placeholderCellStyle;
            }
            set
            {
                if (_placeholderCellStyle != value)
                {
                    this._placeholderCellStyle = value;
                    this.OnPropertyChanged();
                }
            }
        }

    }
}
