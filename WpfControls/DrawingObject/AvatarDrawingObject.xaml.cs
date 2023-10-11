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
    public partial class AvatarDrawingObject : UserControl
    {
        public AvatarDrawingObject(ICellInfo cellInfo, IDrawingHelper drawingHelper, AvatarCellType cellType)
        {
            var viewModel = new AvatarDrawingObjectViewModel();
            viewModel.Shape = cellType.shape;
            viewModel.Icon = IconHelper.GetIcon((ImageValue)cellType.icon, drawingHelper);
            viewModel.Value = cellInfo.Value?.ToString();
            viewModel.HelpText = drawingHelper.GetHelpTextForPage();
            viewModel.AvatarFit = cellType.fit;
            this.DataContext = viewModel;

            var constraint = drawingHelper.GetImageCellSize();
            var size = Math.Min(constraint.Width, constraint.Height);
            if (viewModel.Shape == AvatarShape.circle)
            {
                viewModel.CornerRadius = new CornerRadius(size / 2);
                viewModel.Clip = new RectangleGeometry(new Rect(0, 0, size, size), size / 2, size / 2);
            }
            else
            {
                viewModel.CornerRadius = new CornerRadius(4);
                viewModel.Clip = new RectangleGeometry(new Rect(0, 0, size, size), 4, 4);
            }
            viewModel.Height = size;
            viewModel.Width = size;

            InitializeComponent();
        }
    }

    public class AvatarDrawingObjectViewModel : ModelBase
    {
        public Geometry Clip { get; set; }
        public CornerRadius CornerRadius { get; set; }

        public AvatarShape Shape { get; set; }

        public object Icon { get; set; }

        public Visibility IconVisibility
        {
            get
            {
                return Image == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public string Value { get; set; }

        public string Text
        {
            get
            {
                if (this.Icon == null &&
                    string.IsNullOrEmpty(HelpText) &&
                    !string.IsNullOrEmpty(Value) &&
                    !Value.StartsWith("http://") &&
                    !Value.StartsWith("https://"))
                {
                    return Value;
                }
                return null;
            }
        }

        public Uri Image
        {
            get
            {
                if (Value != null  && 
                    (Value.StartsWith("http://") ||
                    Value.StartsWith("https://")))
                {
                    try
                    {
                        return new Uri(Value);
                    }
                    catch (Exception)
                    {
                        // Do nothing
                    }
                }
                return null;
            }
        }
        public Stretch Stretch
        {
            get
            {
                switch (AvatarFit)
                {
                    case AvatarFit.fill:
                        return Stretch.Fill;
                    case AvatarFit.contain:
                        return Stretch.Uniform;
                    case AvatarFit.cover:
                        return Stretch.UniformToFill;
                    case AvatarFit.none:
                        return Stretch.None;
                    case AvatarFit.scaleDown:
                        return Stretch.UniformToFill;
                    default:
                        break;
                }
                return Stretch.UniformToFill;
            }
        }

        public StretchDirection StretchDirection
        {
            get
            {
                if(AvatarFit == AvatarFit.scaleDown)
                {
                    return StretchDirection.DownOnly;
                }
                return StretchDirection.Both;
            }
        }

        public string HelpText { get; set; }
        public AvatarFit AvatarFit { get; set; }
        public Visibility TextVisibility
        {
            get
            {
                return string.IsNullOrEmpty(HelpText) && Icon == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public double Width { get; set; } = 40;
        public double Height { get; set; } = 40;
    }
}
