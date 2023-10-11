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
    /// Interaction logic for UploadFileDrawingObject.xaml
    /// </summary>
    public partial class UploadFileDrawingObject : UserControl
    {
        public UploadFileDrawingObject(IDrawingHelper drawingHelper, UploadCellType uploadCellType, Brush buttonBackground)
        {
            var viewModel = new UploadFileDrawingObjectViewModel(drawingHelper);
            viewModel.ButtonText = uploadCellType.buttonText;
            viewModel.ButtonBackground = buttonBackground;
            viewModel.TipText = uploadCellType.tipText;
            viewModel.Init(uploadCellType);
            viewModel.PrimaryColor = drawingHelper.GetBrush("Accent 1");
            viewModel.ReadOnlyVisibility = uploadCellType.ReadOnly ? Visibility.Collapsed : Visibility.Visible;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class UploadFileDrawingObjectViewModel
    {
        public UploadFileDrawingObjectViewModel(IDrawingHelper drawingHelper)
        {
            for (int i = 0; i < 3; i++)
            {
                Items.Add(new UploadFileDrawingObjectItemViewModel(drawingHelper) { Text = Properties.Resources.UploadCellType_File + (i + 1) });
            }
        }
        public string ButtonText { get; set; }
        public string TipText { get; set; }
        public Brush PrimaryColor { get; set; }
        public Brush ButtonBackground { get; set; }
        public Brush TipForeground { get; set; } = ColorHelper.Text;
        public Visibility ReadOnlyVisibility { get; set; }
        public List<UploadFileDrawingObjectItemViewModel> Items { get; set; } = new List<UploadFileDrawingObjectItemViewModel>();

        public void Init(UploadCellType cellType)
        {
            switch (cellType.listType)
            {
                case UploadListType.upload:
                    Items.ForEach(i => i.ShowFile = Visibility.Visible);
                    break;
                case UploadListType.pictureCard:
                    break;
                case UploadListType.pictureList:
                    Items.ForEach(i => i.ShowRepeater = Visibility.Visible);
                    break;
                default:
                    break;
            }
        }
    }

    public class UploadFileDrawingObjectItemViewModel
    {
        public UploadFileDrawingObjectItemViewModel(IDrawingHelper drawingHelper)
        {
            documentIcon = IconHelper.GetBuiltInIcon("document", drawingHelper, "#909399");
            circleCheckIcon = IconHelper.GetBuiltInIcon("circleCheck", drawingHelper, "#67C23A");
        }

        public string Text { get; set; }
        public Brush TextColor { get; set; } = ColorHelper.Text;
        public Visibility ShowFile { get; set; } = Visibility.Collapsed;
        public Visibility ShowRepeater { get; set; } = Visibility.Collapsed;
        public object documentIcon { get; set; }
        public object circleCheckIcon { get; set; }
    }

}
