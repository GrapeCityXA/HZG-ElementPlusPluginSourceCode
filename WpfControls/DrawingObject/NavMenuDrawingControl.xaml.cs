using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace ElementUI.WpfControls.DrawingObject
{
    /// <summary>
    /// Interaction logic for NavMenuDrawingControl.xaml
    /// </summary>
    public partial class NavMenuDrawingControl : UserControl
    {
        public NavMenuDrawingControl(NavMenuCellType navMenu, IDrawingHelper drawingHelper)
        {
            var viewModel = new NavMenuDrawingControlViewModel();
            viewModel.IsVertical = navMenu.mode == "vertical";
            var cellSize = drawingHelper.GetImageCellSize();
            var maxItemCount = int.MaxValue;
            if (viewModel.IsVertical)
            {
                maxItemCount = (int)cellSize.Height / 56 + 1;
                viewModel.Height = 56;
            }
            else
            {
                viewModel.Height = cellSize.Height;
            }
            viewModel.Background = drawingHelper.GetBrush(navMenu.backgroundColor);
            var options = navMenu.options;
            if (options == null || options.Count == 0)
            {
                var list = JsonConvert.DeserializeObject<List<MenuNode>>(Properties.Resources.DefaultTreeSource);
                options = list.OfType<ITreeNode>().ToList();
            }
            AddItems(navMenu, viewModel, drawingHelper, options, GetDefaultItem(navMenu, viewModel, options), maxItemCount);

            this.DataContext = viewModel;
            InitializeComponent();
        }

        private ITreeNode GetDefaultItem(NavMenuCellType navMenu, NavMenuDrawingControlViewModel viewModel, List<ITreeNode> options)
        {
            if (!viewModel.IsVertical || navMenu.collapse)
            {
                return options.FirstOrDefault();
            }
            var first = options.FirstOrDefault();
            if (first.Children != null && first.Children.Any())
            {
                return GetDefaultItem(navMenu, viewModel, first.Children?.ToList());
            }
            return first;
        }

        private void AddItems(NavMenuCellType navMenu, NavMenuDrawingControlViewModel viewModel, IDrawingHelper drawingHelper, List<ITreeNode> options, ITreeNode defaultItem, int maxItemCount, int level = 0)
        {
            if (options == null || options.Count == 0)
            {
                return;
            }
            if (!viewModel.IsVertical || navMenu.collapse)
            {
                if (level != 0)
                {
                    return;
                }
            }

            foreach (var item in options)
            {
                bool isActive = item == defaultItem;
                var textColor = isActive ? navMenu.activeTextColor : navMenu.textColor;
                var itemViewModel = new MenuItemViewModel()
                {
                    Background = drawingHelper.GetBrush(navMenu.backgroundColor),
                    Foreground = drawingHelper.GetBrush(textColor),
                    Text = navMenu.collapse && viewModel.IsVertical ? null : item.Text,
                    BorderHorizontalAlignment = navMenu.collapse ? HorizontalAlignment.Left : HorizontalAlignment.Stretch,
                    Margin = new Thickness(level * 20, 0, 0, 0),
                    TextGridWidth = navMenu.collapse && !viewModel.IsVertical ? GridLength.Auto : new GridLength(1, GridUnitType.Star),
                    BorderThickness = viewModel.IsVertical ? new Thickness(0, 0, 1, 0) : (isActive ? new Thickness(0, 0, 0, 3) : new Thickness(0, 0, 0, 1)),
                    BorderBrush = (viewModel.IsVertical || !isActive) ? drawingHelper.GetBrush("#dcdfe6") : drawingHelper.GetBrush(textColor),
                    Height = viewModel.Height
                };
                if (item is MenuNode menuItem)
                {
                    if (menuItem.icon != null)
                    {
                        itemViewModel.Icon = IconHelper.GetIcon(menuItem.icon, drawingHelper, textColor);
                    }
                }
                if (item.Children?.Any() == true && (!navMenu.collapse || !viewModel.IsVertical))
                {
                    itemViewModel.ArrowIcon = IconHelper.GetBuiltInIcon("down", drawingHelper, textColor);
                }
                viewModel.Items.Add(itemViewModel);

                if (viewModel.Items.Count > maxItemCount)
                {
                    return;
                }
                AddItems(navMenu, viewModel, drawingHelper, item.Children?.ToList(), defaultItem, maxItemCount, level + 1);
            }
        }
    }

    public class NavMenuDrawingControlViewModel
    {
        public bool IsVertical { get; set; }

        public Orientation Orientation
        {
            get
            {
                return IsVertical ? Orientation.Vertical : Orientation.Horizontal;
            }
        }
        public Brush Background { get; set; }
        public double Height { get; set; }
        public List<MenuItemViewModel> Items { get; set; } = new List<MenuItemViewModel>();
    }

    public class MenuItemViewModel
    {
        public HorizontalAlignment BorderHorizontalAlignment { get; set; }
        public Thickness Margin { get; set; }
        public Thickness BorderThickness { get; set; }
        public Brush BorderBrush { get; set; }
        public Visibility IconVisiblity
        {
            get
            {
                return Icon != null ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public object Icon { get; set; }
        public string Text { get; set; }
        public Visibility TextVisibility
        {
            get
            {
                return !string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Brush Foreground { get; set; }
        public Brush Background { get; set; }

        public object ArrowIcon { get; set; }
        public Visibility ArrowIconVisibility
        {
            get
            {
                return ArrowIcon != null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public GridLength TextGridWidth { get; set; }

        public double Height { get; set; }
    }
}
