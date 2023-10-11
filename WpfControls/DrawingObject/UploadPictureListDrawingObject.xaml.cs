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
    /// Interaction logic for UploadPictureListDrawingObject.xaml
    /// </summary>
    public partial class UploadPictureListDrawingObject : UserControl
    {
        public UploadPictureListDrawingObject(IDrawingHelper drawingHelper, UploadCellType uploadCellType)
        {
            var viewModel = new UploadPictureListDrawingObjectViewModel(drawingHelper);
            viewModel.ReadOnlyVisibility = uploadCellType.ReadOnly ? Visibility.Collapsed : Visibility.Visible;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class UploadPictureListDrawingObjectViewModel
    {
        public UploadPictureListDrawingObjectViewModel(IDrawingHelper drawingHelper)
        {
            plusIcon = IconHelper.GetBuiltInIcon("plus", drawingHelper, "#8c939d");
        }
        public Visibility ReadOnlyVisibility { get; set; }
        public object plusIcon { get; set; } 
    }
}
