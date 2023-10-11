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
using ElementUI.Properties;

namespace ElementUI.WpfControls.DrawingObject
{
    /// <summary>
    /// Interaction logic for TagDrawingObject.xaml
    /// </summary>
    public partial class TagDrawingObject : UserControl
    {
        public TagDrawingObject(IDrawingHelper drawingHelper, Tag cellType, ICellInfo cellInfo)
        {
            var viewModel = new TagDrawingObjectViewModel(drawingHelper, cellType, cellInfo);
            viewModel.SeperateStr = cellType.separator;
            viewModel.SetItems(cellInfo.Text, cellType, drawingHelper);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
    public class TagDrawingObjectViewModel : ModelBase
    {
        IDrawingHelper _drawingHelper;
        Tag _cellType;
        ICellInfo _cellInfo;
        public TagDrawingObjectViewModel(IDrawingHelper drawingHelper, Tag cellType, ICellInfo cellInfo)
        {
            _drawingHelper = drawingHelper;
            _cellType = cellType;
            _cellInfo = cellInfo;
        }

        public void SetItems(string text, Tag cellType, IDrawingHelper drawingHelper)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = Resources.TagCellType_DefaultValue;
            }
            var strs = text.Split(new string[] { this.SeperateStr }, StringSplitOptions.RemoveEmptyEntries);
            var itemList = new List<object>();
            if (cellType.distinct)
            {
                strs = strs.Distinct().ToArray();
            }
            for (int i = 0; i < strs.Length; i++)
            {
                var str = strs[i];
                var item = new TagItem() { Text = str };
                var isLast = (i == strs.Length - 1);
                if (isLast)
                {
                    item.Margin = new Thickness(0, 0, 0, _cellInfo.WordWrap ? cellType.itemSpace : 0);
                }
                else
                {
                    item.Margin = new Thickness(0, 0, cellType.itemSpace, _cellInfo.WordWrap ? cellType.itemSpace : 0);
                }
                var color = cellType.ColorList[i % cellType.ColorList.Count].color;
                item.Foreground = GetColor(cellType, color);
                item.Border = GetBorder(cellType, color);
                item.Background = GetBackground(cellType, color);
                item.Padding = GetPadding(cellType);
                item.Height = GetHeight(cellType);
                item.IconWidth = GetIconWidth(cellType);
                item.closeIcon = IconHelper.GetBuiltInIcon("close2", drawingHelper, (item.Foreground as SolidColorBrush).Color.ToString());
                if (!_cellType.ReadOnly && !_cellType.IsDisabled)
                {
                    item.CloseButtonVisibility = Visibility.Visible;
                }
                itemList.Add(item);
            }

            if (_cellType.allowAdd && !_cellType.ReadOnly && !_cellType.IsDisabled)
            {
                itemList.Add(new AddButtonItem(_cellType, _cellInfo));
            }
            this.Items = itemList;
        }
        public string SeperateStr { get; set; }
        
        public Visibility NoWrapVisibiliy
        {
            get
            {
                return _cellInfo.WordWrap ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        public Visibility WrapVisibiliy
        {
            get
            {
                return !_cellInfo.WordWrap ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        public List<object> Items { get; set; }

        public Brush GetBorder(Tag cellType, string color)
        {
            var brush = _drawingHelper.GetBrush(color);
            if (cellType.hit)
            {
                return brush;
            }
            switch (cellType.effect)
            {
                case "dark":
                    return brush;
                case "light":
                    return new SolidColorBrush(ColorHelper.UpdateTint(((SolidColorBrush)brush).Color, 0.8f));
                case "plain":
                    return new SolidColorBrush(ColorHelper.UpdateTint(((SolidColorBrush)brush).Color, 0.6f));
                default:
                    return null;
            }
        }
        public Brush GetColor(Tag cellType, string color)
        {
            var brush = _drawingHelper.GetBrush(color);
            switch (cellType.effect)
            {
                case "dark":
                    return Brushes.White;
                case "light":
                    return brush;
                case "plain":
                    return brush;
                default:
                    return null;
            }
        }
        public Brush GetBackground(Tag cellType, string color)
        {
            var brush = _drawingHelper.GetBrush(color);
            switch (cellType.effect)
            {
                case "dark":
                    return brush;
                case "light":
                    return new SolidColorBrush(ColorHelper.UpdateTint(((SolidColorBrush)brush).Color, 0.9f));
                case "plain":
                    return Brushes.White;
                default:
                    return null;
            }
        }

        public Thickness GetPadding(Tag cellType)
        {
            switch (cellType.size)
            {
                case "large":
                    return new Thickness(10, 0, 10, 0);
                case "small":
                    return new Thickness(6, 0, 6, 0);
                default:
                    return new Thickness(8, 0, 8, 0);
            }
        }
        public double GetHeight(Tag cellType)
        {
            switch (cellType.size)
            {
                case "large":
                    return 32;
                case "default":
                    return 24;
                default:
                    return 20;
            }
        }  
        public double GetIconWidth(Tag cellType)
        {
            switch (cellType.size)
            {
                case "small":
                    return 16;
                case "large":
                    return 20;
                default:
                    return 18;
            }
        }
    }

    public class AddButtonItem
    {
        Tag _cellType;
        ICellInfo _cellInfo;
        public AddButtonItem(Tag cellType, ICellInfo cellInfo)
        {
            _cellType = cellType;
            _cellInfo = cellInfo;
        }

        public double AddButtonHeight
        {
            get
            {
                return GetAddButtonHeight(this._cellType);
            }
        }
        public double AddButtonWidth
        {
            get
            {
                return _cellType.addButtonSettings.width;
            }
        }
        public string AddButtonText
        {
            get
            {
                return _cellType.addButtonSettings.text;
            }
        }

        public Thickness AddButtonMargin
        {
            get
            {
                return new Thickness(_cellType.addButtonSettings.space, 0, 0, _cellInfo.WordWrap ? _cellType.itemSpace : 0);
            }
        }

        public Brush AddButtonForeground
        {
            get
            {
                return ColorHelper.Text;
            }
        }
        public Brush AddButtonBorder
        {
            get
            {
                return ColorHelper.Border;
            }
        }
        public double AddButtonFontSize
        {
            get
            {
                return _cellType.size == "large" || _cellType.size == "default" ? 14 : 12;
            }
        }
        public double GetAddButtonHeight(Tag cellType)
        {
            switch (cellType.size)
            {
                case "large":
                    return 32;
                default:
                    return 24;
            }
        }
    }

    public class TagItem
    {
        public string Text { get; set; }
        public Thickness Margin { get; set; }
        public Brush Foreground { get; set; }
        public Brush Border { get; set; }
        public Brush Background { get; set; }
        public Visibility CloseButtonVisibility { get; set; } = Visibility.Collapsed;
        public Thickness Padding { get; set; }
        public double Height { get; set; }
        public double IconWidth { get; set; }

        public object closeIcon { get; set; }
    }
}
