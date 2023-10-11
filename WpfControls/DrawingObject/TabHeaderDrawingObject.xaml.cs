using ElementUI.Properties;
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

namespace ElementUI.WpfControls.DrawingObject
{
    /// <summary>
    /// Interaction logic for TabHeaderDrawingObject.xaml
    /// </summary>
    public partial class TabHeaderDrawingObject : UserControl
    {
        public TabHeaderDrawingObject(IDrawingHelper drawingHelper, TabHeader cellType, ICellInfo cellInfo)
        {
            this.DataContext = new TabHeaderDrawingObjectViewModel(drawingHelper, cellType, cellInfo);
            InitializeComponent();
        }

        public TabHeaderDrawingObjectViewModel ViewModel
        {
            get
            {
                return this.DataContext as TabHeaderDrawingObjectViewModel;
            }
        }
    }
    public class TabHeaderDrawingObjectViewModel : ModelBase
    {
        public TabHeaderDrawingObjectViewModel(IDrawingHelper drawingHelper, TabHeader cellType, ICellInfo cellInfo)
        {
            var items = cellType.Tabs;
       
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                Items.Add(new TabItem()
                {
                    Text = item.Name,
                    BorderBrush = GetBorderBrush(i, cellType, drawingHelper),
                    BorderThickness = GetBorderThickness(i, cellType),
                    OuterBorderBrush = GetOuterBorderBrush(i, cellType, drawingHelper),
                    OuterBorderThickness = GetOuterBorderThickness(i, cellType),
                    Background = GetBackground(i, cellType, drawingHelper),
                    Padding = GetPadding(cellType, i),
                    Foreground = GetForeground(i, cellType, drawingHelper),
                    MinWidth = GetMinWidth(drawingHelper, cellType, items.Count),
                    TextMargin = GetTextMargin(cellType),
                    TextHorizontalAlignment = GetTextHorizontalAlignment(cellType),
                });
            }

            if(!(IsPostionTopOrBottom(cellType)))
            {
                this.Orientation = Orientation.Vertical;
            }

            if (cellType.position == "bottom")
            {
                // 与position为top时相反
                this.GridRow1Height = "*";
                this.GridRow2Height = "auto";
                this.ItemsGridRow = 1;
                this.LastBorderGridRow = 0;
            }

            if(cellType.type != "default")
            {
                LastBorderBrush = drawingHelper.GetBrush("#e4e7ed");
                LastBorderThickness = GetLastBorderThickness(cellType);
                LastBorderBackground = cellType.type == "border-card" ? drawingHelper.GetBrush("#f5f7fa") : null;
            }
            VerticalLastBorderVisibility = IsPostionTopOrBottom(cellType) ? Visibility.Collapsed : Visibility.Visible;
        }

        private Thickness GetLastBorderThickness(TabHeader cellType)
        {
            if (cellType.position == "top")
            {
                return new Thickness(0, 0, 0, 1);
            }
            else if (cellType.position == "bottom")
            {
                return new Thickness(0, 1, 0, 0);
            }
            return new Thickness(1, 0, 1, 1);
        }

        private bool IsPostionTopOrBottom(TabHeader cellType)
        {
            return cellType.position == "top" || cellType.position == "bottom";
        }

        private double GetMinWidth(IDrawingHelper drawingHelper, TabHeader cellType, int count)
        {
            if(IsPostionTopOrBottom(cellType) && cellType.stretch)
            {
                var width = drawingHelper.GetImageCellSize().Width;
                return width / count;
            }
            else if(!IsPostionTopOrBottom(cellType))
            {
                return drawingHelper.GetImageCellSize().Width;
            }
            return 0;
        }

        private Brush GetBorderBrush(int index, TabHeader cellType, IDrawingHelper drawingHelper)
        {
            if (cellType.type == "default")
            {
                if (index == 0)
                {
                    return drawingHelper.GetBrush("Accent 1");
                }
            }

            return null;
        }
        private Brush GetOuterBorderBrush(int index, TabHeader cellType, IDrawingHelper drawingHelper)
        {
            if (cellType.type != "default")
            {
                return drawingHelper.GetBrush("#e4e7ed");
            }

            return null;
        }
        private Thickness GetOuterBorderThickness(int index, TabHeader cellType)
        {
            if (cellType.type != "default")
            {
                if (index == 0)
                {
                    if (cellType.position == "top")
                    {
                        return new Thickness(1, 1, 1, 0);
                    }
                    else if (cellType.position == "bottom")
                    {
                        return new Thickness(1, 0, 1, 1);
                    }
                    return new Thickness(1);
                }
                else
                {
                    if (cellType.type == "border-card")
                    {
                        if(index == 0)
                        {
                            return IsPostionTopOrBottom(cellType) ? new Thickness(1, 0, 0, 1) : new Thickness(1, 1, 1, 0);
                        }
                        if (cellType.position == "top")
                        {
                            return new Thickness(0, 0, 0, 1);
                        }
                        else if (cellType.position == "bottom")
                        {
                            return new Thickness(0, 1, 0, 0);
                        }
                        return new Thickness(1, 0, 1, 0);
                    }
                    else
                    {
                        return new Thickness(1);
                    }
                }
            }
            return new Thickness(0);
        }
        private Brush GetForeground(int index, TabHeader cellType, IDrawingHelper drawingHelper)
        {
            if (index == 0)
            {
                return drawingHelper.GetBrush("Accent 1");
            }
            if(cellType.type == "border-card")
            {
                return ColorHelper.Info;
            }
            return ColorHelper.DarkText;
        } 
        private Brush GetBackground(int index, TabHeader cellType, IDrawingHelper drawingHelper)
        {
            if (index == 0)
            {
                if (cellType.type == "border-card")
                {
                    return Brushes.White;
                }
                return null;
            }
            if(cellType.type == "border-card")
            {
                return drawingHelper.GetBrush("#f5f7fa");
            }
            return null;
        }
        private Thickness GetPadding(TabHeader cellType, int index)
        {
            if(!IsPostionTopOrBottom(cellType))
            {
                return new Thickness(0);
            }
            if (cellType.type == "default")
            {
                if (index == 0)
                {
                    return new Thickness(0, 0, 20, 0);
                }
            }
            return new Thickness(20, 0, 20, 0);
        }
        private Thickness GetBorderThickness(int index, TabHeader cellType)
        {
            if (cellType.type == "default")
            {

                if (index == 0)
                {
                    switch (cellType.position)
                    {
                        case "left":
                            return new Thickness(0, 0, 2, 0);
                        case "right":
                            return new Thickness(2, 0, 0, 0);
                        default:
                            return new Thickness(0, 0, 0, 2);

                    }
                }

            }
            return new Thickness(0);
        }

        private Thickness GetTextMargin(TabHeader cellType)
        {
            switch (cellType.position)
            {
                case "top":
                    return new Thickness(0);
                case "bottom":
                    return new Thickness(0);
                default:
                    return new Thickness(20, 0, 20, 0);
            }
        }
        private HorizontalAlignment GetTextHorizontalAlignment(TabHeader cellType)
        {
            switch (cellType.position)
            {
                case "left":
                    return HorizontalAlignment.Right;
                case "right":
                    return HorizontalAlignment.Left;
                default:
                    return HorizontalAlignment.Center;
            }
        }
        public List<TabItem> Items { get; set; } = new List<TabItem>();

        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        public Thickness LastBorderThickness { get; set; }
        public Brush LastBorderBrush { get; set; }

        public Brush LastBorderBackground { get; set; }

        public Visibility VerticalLastBorderVisibility { get; set; }

        // last border 处于第几行
        public int LastBorderGridRow { get; set; } = 1;

        // tabs 处于第几行
        public int ItemsGridRow { get; set; } = 0;

        // TabHeaderDrawingObject.xaml Grid 布局中第一行的高度
        public string GridRow1Height { get; set; } = "auto";

        // TabHeaderDrawingObject.xaml Grid 布局中第二行的高度
        public string GridRow2Height { get; set; } = "*";
    }

    public class TabItem 
    {
        public Thickness Padding { get; set; }
        public Brush BorderBrush { get; set; }
        public Thickness BorderThickness { get; set; }
        public Brush OuterBorderBrush { get; set; }
        public Thickness OuterBorderThickness { get; set; }
        public string Text { get; set; }
        public Brush Foreground { get; set; }
        public Brush Background { get; set; }
        public double MinWidth { get; set; } = 0;

        public Thickness TextMargin { get; set; }
        public HorizontalAlignment TextHorizontalAlignment { get; set; }
    }
}
