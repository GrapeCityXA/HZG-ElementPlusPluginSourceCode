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
    /// Interaction logic for RateDrawingObject.xaml
    /// </summary>
    public partial class RateDrawingObject : UserControl
    {
        public RateDrawingObject(IDrawingHelper drawingHelper, RateCellType cellType)
        {
            var viewModel = new RateDrawingObjectViewModel();
            viewModel.drawingHelper = drawingHelper;
            if (cellType.colors != null && cellType.colors.Count >= 1)
            {
                try
                {
                    string color = cellType.colors[1].color;
                    if (color != null)
                    {
                        viewModel.Color = color;
                    }
                }
                catch
                {
                    //Do nothing
                }
            }
            if (cellType.DisplayContentType == DisplayContentType.Text)
            {
                if (cellType.texts != null && cellType.texts.Count >= 1)
                {
                    var text = cellType.texts[0].label;
                    if (cellType.texts.Count == 5)
                    {
                        text = cellType.texts[2].label;
                    }
                    else if (cellType.texts.Count >= 2)
                    {
                        text = cellType.texts[1].label;
                    }
                    viewModel.Text = text;
                }
            }
            else if (cellType.DisplayContentType == DisplayContentType.Score)
            {
                viewModel.Text = "3";
            }
            viewModel.OffColor = cellType.voidColor;
            if (cellType.ReadOnly)
            {
                viewModel.OffColor = cellType.disabledVoidColor;
            }
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class RateDrawingObjectViewModel
    {
        public IDrawingHelper drawingHelper { get; set; }
        public string Color { get; set; } = "#C6D1DE";
        public string OffColor { get; set; } = "#C6D1DE";

        public object OffIcon
        {
            get
            {
                return IconHelper.GetBuiltInIcon("starOff", drawingHelper, OffColor);
            }
        }
        public object OnIcon
        {
            get
            {
                return IconHelper.GetBuiltInIcon("starOn", drawingHelper, Color);
            }
        }

        public string Text { get; set; }

        public Brush TextColor { get; set; } = ColorHelper.FromStr("#1F2D3D");
    }
}
