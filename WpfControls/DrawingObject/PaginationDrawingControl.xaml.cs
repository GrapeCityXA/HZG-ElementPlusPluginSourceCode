using ElementUI.CellTypes;
using GrapeCity.Forguncy.CellTypes;
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
    public partial class PaginationDrawingControl : UserControl
    {
        public PaginationDrawingControl(CellTypeViewModelStyle cellStyle, PaginationCellType cellType, IDrawingHelper drawingHelper)
        {
            var viewModel = new PaginationDrawingControlViewModel(drawingHelper);
            viewModel.Background = cellType.background;
            viewModel.Small = cellType.small;
            viewModel.PrevText = cellType.prevText;
            viewModel.NextText = cellType.nextText;
            viewModel.PageCount = cellType.pagerCount;
            viewModel.Layout = cellType.layout;
            if (cellStyle.Foreground == null)
            {
                cellStyle.Foreground = ColorHelper.Text;
            }
            this.DataContext = viewModel;
            InitializeComponent();

        }
    }

    public class PageItemViewModel
    {
        public string Text { get; set; }

        public PaginationDrawingControlViewModel Owner { get; set; }
    }

    public class BaseControlViewModel
    {
        public PaginationDrawingControlViewModel Owner { get; set; }
    }

    public class SizesControlViewModel : BaseControlViewModel { }

    public class PrevControlViewModel : BaseControlViewModel { }

    public class PagerControlViewModel : BaseControlViewModel { }

    public class NextControlViewModel : BaseControlViewModel { }

    public class JumperControlViewModel : BaseControlViewModel { }

    public class TotalControlViewModel : BaseControlViewModel { }

    public class PaginationDrawingControlViewModel : ModelBase
    {
        public PaginationDrawingControlViewModel(IDrawingHelper drawingHelper)
        {
            arrowDownIcon = IconHelper.GetBuiltInIcon("arrow-down", drawingHelper);
            arrowRightIcon = IconHelper.GetBuiltInIcon("arrow-right", drawingHelper);
            arrowLeftIcon = IconHelper.GetBuiltInIcon("arrow-left", drawingHelper);
        }

        private int _pageCount;

        private string _prevText;

        private string _nextText;

        public bool Small { get; set; }

        public bool Background { get; set; }

        public List<PageItemViewModel> PageList { get; set; } = new List<PageItemViewModel> { };

        public Visibility PrevIconVisibility { get; set; } = Visibility.Collapsed;

        public Visibility NextIconVisibility { get; set; } = Visibility.Collapsed;

        public Visibility PrevTextVisibility { get; set; } = Visibility.Visible;

        public Visibility NextTextVisibility { get; set; } = Visibility.Visible;

        public object arrowDownIcon { get; set; }
        public object arrowLeftIcon { get; set; }
        public object arrowRightIcon { get; set; }

        public string PrevText
        {
            get => this._prevText;
            set
            {
                this._prevText = value;
                if (string.IsNullOrEmpty(this._prevText))
                {
                    this.PrevIconVisibility = Visibility.Visible;
                    this.PrevTextVisibility = Visibility.Collapsed;
                }
                else
                {
                    this.PrevIconVisibility = Visibility.Collapsed;
                    this.PrevTextVisibility = Visibility.Visible;
                }
            }
        }

        public string NextText
        {
            get => this._nextText;
            set
            {
                this._nextText = value;
                if (string.IsNullOrEmpty(this._nextText))
                {
                    this.NextIconVisibility = Visibility.Visible;
                    this.NextTextVisibility = Visibility.Collapsed;
                }
                else
                {
                    this.NextIconVisibility = Visibility.Collapsed;
                    this.NextTextVisibility = Visibility.Visible;
                }
            }
        }

        private List<PageLayout> _layout;

        public List<PageLayout> Layout
        {
            get
            {
                return _layout;
            }
            set
            {
                this._layout = value;

                var leftControlLayouts = new List<BaseControlViewModel>();
                var rightControlLayouts = new List<BaseControlViewModel>();
                var sourceControlLayouts = leftControlLayouts;

                foreach (var item in _layout)
                {

                    switch (item.layoutItem)
                    {
                        case "sizes":
                            sourceControlLayouts.Add(new SizesControlViewModel()
                            {
                                Owner = this,
                            });
                            break;
                        case "prev":
                            sourceControlLayouts.Add(new PrevControlViewModel()
                            {
                                Owner = this,
                            });
                            break;
                        case "pager":
                            sourceControlLayouts.Add(new PagerControlViewModel()
                            {
                                Owner = this,
                            });
                            break;
                        case "next":
                            sourceControlLayouts.Add(new NextControlViewModel()
                            {
                                Owner = this,
                            });
                            break;
                        case "jumper":
                            sourceControlLayouts.Add(new JumperControlViewModel()
                            {
                                Owner = this,
                            });
                            break;
                        case "total":
                            sourceControlLayouts.Add(new TotalControlViewModel()
                            {
                                Owner = this,
                            });
                            break;
                        case "->":
                            sourceControlLayouts = rightControlLayouts;
                            break;
                    }
                }

                this.LeftControlLayouts = leftControlLayouts;
                this.RightControlLayouts = rightControlLayouts;
            }
        }

        public int PageCount
        {
            get => this._pageCount;
            set
            {
                this._pageCount = value;

                var pageList = new List<PageItemViewModel>();
                for (int i = 1; i < _pageCount; i++)
                {
                    pageList.Add(this.CreatePageItem(i.ToString()));
                }
                pageList.Add(this.CreatePageItem("..."));
                pageList.Add(this.CreatePageItem("100"));

                this.PageList = pageList;
            }
        }

        public PageItemViewModel CreatePageItem(string text)
        {
            return new PageItemViewModel
            {
                Text = text,
                Owner = this
            };
        }

        public List<BaseControlViewModel> LeftControlLayouts { get; set; } = new List<BaseControlViewModel>();

        public List<BaseControlViewModel> RightControlLayouts { get; set; } = new List<BaseControlViewModel>();
    }
}
