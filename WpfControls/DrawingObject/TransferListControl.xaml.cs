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
    /// Interaction logic for TransferListControl.xaml
    /// </summary>
    public partial class TransferListControl : UserControl
    {
        public TransferListControl()
        {
            InitializeComponent();
            this.DataContextChanged += TransferListControl_DataContextChanged;
        }

        private void TransferListControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is TransferListControlViewModel viewModel)
            {
                // 这里不用绑定，而是使用直接操作控件，原因是使用绑定会导致生成图片时出错，在容器中和图文列表中无法显示设计时预览
                header.Text = viewModel.HeaderText;
                headerCount.Text = viewModel.HeaderCount;
                search.Visibility = viewModel.CanSearch ? Visibility.Visible : Visibility.Collapsed;
                placeHolder.Text = viewModel.PlaceHolder;
                listBox.ItemsSource = viewModel.Items;
            }
        }
    }

    public class TransferListControlViewModel : ModelBase
    {
        private string _headerText;
        public string HeaderText
        {
            get
            {
                return _headerText;
            }
            set
            {
                _headerText = value;
                OnPropertyChanged();
            }
        }

        private string _headerCount;

        public string HeaderCount
        {
            get
            {
                return _headerCount;
            }
            set
            {
                _headerCount = value;
                OnPropertyChanged();
            }
        }

        private string _placeHolder;

        public string PlaceHolder
        {
            get
            {
                return _placeHolder;
            }
            set
            {
                _placeHolder = value;
                OnPropertyChanged();
            }
        }

        private bool _canSearch;

        public bool CanSearch
        {
            get
            {
                return _canSearch;
            }
            set
            {
                _canSearch = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ItemInfo> _items;

        public ObservableCollection<ItemInfo> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

    }

    public class ItemInfo
    {
        public Brush PrimaryColor { get; set; }

        public bool IsChecked { get; set; }

        public string Content { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsEnabled { get { return !IsDisabled; } }
    }
}
