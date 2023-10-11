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
    /// Interaction logic for TransferDrawingControl.xaml
    /// </summary>
    public partial class TransferDrawingControl : UserControl
    {
        public TransferDrawingControl(IDrawingHelper drawingHelper, TransferCellType cellType)
        {
            var viewModel = new TransferDrawingControlViewModel();
            viewModel.PrimaryColor = drawingHelper.GetBrush("Accent 1");
            viewModel.CanSearch = cellType.filterable;
            viewModel.PlaceHolder = cellType.filterPlaceholder;
            var items = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                items.Add(Properties.Resources.TransferCellType_DefaultItemName + (i + 1).ToString());
            }
            viewModel.SetSourceList(items.Take(5).ToList(), cellType.leftTitle);
            viewModel.SetTargetList(items.Skip(5).ToList(), cellType.rightTitle);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class TransferDrawingControlViewModel : ModelBase
    {
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
                SourceList.CanSearch = _canSearch;
                TargetList.CanSearch = _canSearch;
                OnPropertyChanged();
            }
        }

        public Brush PrimaryColor { get; set; }

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
                SourceList.PlaceHolder = _placeHolder;
                TargetList.PlaceHolder = _placeHolder;
                OnPropertyChanged();
            }
        }

        private TransferListControlViewModel _sourceList = new TransferListControlViewModel();

        public TransferListControlViewModel SourceList
        {
            get
            {
                return _sourceList;
            }
            set
            {
                _sourceList = value;
                OnPropertyChanged();
            }
        }

        private TransferListControlViewModel _targetList = new TransferListControlViewModel();

        public TransferListControlViewModel TargetList
        {
            get
            {
                return _targetList;
            }
            set
            {
                _targetList = value;
                OnPropertyChanged();
            }
        }

        public void SetSourceList(List<string> sourceList, string header)
        {
            SourceList.Items = new ObservableCollection<ItemInfo>();

            for (var i = 0; i < sourceList.Count; i++)
            {
                SourceList.Items.Add(new ItemInfo
                {
                    PrimaryColor = this.PrimaryColor,
                    Content = sourceList[i],
                    IsChecked = i < 2,
                    IsDisabled = i > 2
                });
            }
            SourceList.HeaderText = header;
            SourceList.HeaderCount = "2/" + sourceList.Count;
        }

        public void SetTargetList(List<string> targetList, string header)
        {
            TargetList.Items = new ObservableCollection<ItemInfo>();
            for (var i = 0; i < targetList.Count; i++)
            {
                TargetList.Items.Add(new ItemInfo
                {
                    PrimaryColor = this.PrimaryColor,
                    Content = targetList[i],
                    IsChecked = i < 2
                });
            }
            TargetList.HeaderText = header;
            TargetList.HeaderCount = "2/" + targetList.Count;
        }
    }
}
