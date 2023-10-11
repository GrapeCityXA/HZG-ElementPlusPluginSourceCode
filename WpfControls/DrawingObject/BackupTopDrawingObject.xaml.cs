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

namespace ElementUI.WpfControls.DrawingObject
{
    /// <summary>
    /// Interaction logic for AvatarDrawingObject.xaml
    /// </summary>
    public partial class BackupTopDrawingObject : UserControl
    {
        public BackupTopDrawingObject(IDrawingHelper drawingHelper)
        {
            var viewModel = new BackupTopDrawingObjectViewModel();

            viewModel.Icon = IconHelper.GetBuiltInIcon("caretTop", drawingHelper, "Accent 1");

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }

    public class BackupTopDrawingObjectViewModel : ModelBase
    {
        public object Icon { get; set; }
    }
}
