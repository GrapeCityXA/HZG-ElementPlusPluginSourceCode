using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Plugin;
using System;
using System.Windows;
using System.Windows.Media;

namespace ElementUI.WpfControls
{
    public class ColorHelper
    {
        static ColorHelper()
        {
            var converter = new BrushConverter();
            Success = converter.ConvertFrom("#67C23A") as Brush;
            Info = converter.ConvertFrom("#909399") as Brush;
            Warning = converter.ConvertFrom("#E6A23C") as Brush;
            Error = converter.ConvertFrom("#F56C6C") as Brush;
            Text = converter.ConvertFrom("#606266") as Brush;
            DarkText = converter.ConvertFrom("#303133") as Brush;
            BorderColorLighter = converter.ConvertFrom("#EBEEF5") as Brush;
            BorderColorLightest = converter.ConvertFrom("#c0c4cc") as Brush;
            Border = converter.ConvertFrom("#DCDFE6") as Brush;
        }

        public static Brush Success { get; set; }
        public static Brush Info { get; set; }
        public static Brush Warning { get; set; }

        internal static Brush FromStr(string str)
        {
            return new BrushConverter().ConvertFrom(str) as Brush;
        }

        public static Brush Error { get; set; }
        public static Brush Text { get; set; }
        public static Brush DarkText { get; set; }
        public static Brush BorderColorLighter { get; set; }
        public static Brush BorderColorLightest { get; set; }
        public static Brush Border { get; set; }

        public static Color UpdateTint(Color baseColor, float tint)
        {
            if (tint == 0.0)
                return baseColor;

            HlsColor color = new HlsColor(baseColor);
            if (tint > 0)
            {
                int num1 = (240 - color.Luminosity);
                float num2 = num1 * tint;
                return ColorFromHLS(color.Hue, color.Luminosity + (int)num2, color.Saturation, baseColor.A);
            }
            else
            {
                float num2 = color.Luminosity * -tint;
                return ColorFromHLS(color.Hue, color.Luminosity - (int)num2, color.Saturation, baseColor.A);
            }
        }

        public static Color ColorFromHLS(int hue, int luminosity, int saturation, byte alpha)
        {
            return HlsColor.ColorFromHLS(hue, luminosity, saturation, alpha);
        }
        public struct HlsColor
        {
            private const int ShadowAdj = -333;
            private const int HilightAdj = 500;
            private const int WatermarkAdj = -50;
            private const int Range = 240;
            private const int HLSMax = 240;
            private const int RGBMax = 0xff;
            private const int Undefined = 160;
            private byte alpha;
            private int hue;
            private int saturation;
            private int luminosity;
            public HlsColor(Color color)
            {
                int r = color.R;
                int g = color.G;
                int b = color.B;
                alpha = color.A;
                int num4 = Math.Max(Math.Max(r, g), b);
                int num5 = Math.Min(Math.Min(r, g), b);
                int num6 = num4 + num5;
                this.luminosity = ((num6 * 240) + 0xff) / 510;
                int num7 = num4 - num5;
                if (num7 == 0)
                {
                    this.saturation = 0;
                    this.hue = 160;
                }
                else
                {
                    if (this.luminosity <= 120)
                    {
                        this.saturation = ((num7 * 240) + (num6 / 2)) / num6;
                    }
                    else
                    {
                        this.saturation = ((num7 * 240) + ((510 - num6) / 2)) / (510 - num6);
                    }
                    int num8 = (((num4 - r) * 40) + (num7 / 2)) / num7;
                    int num9 = (((num4 - g) * 40) + (num7 / 2)) / num7;
                    int num10 = (((num4 - b) * 40) + (num7 / 2)) / num7;
                    if (r == num4)
                    {
                        this.hue = num10 - num9;
                    }
                    else if (g == num4)
                    {
                        this.hue = (80 + num8) - num10;
                    }
                    else
                    {
                        this.hue = (160 + num9) - num8;
                    }
                    if (this.hue < 0)
                    {
                        this.hue += 240;
                    }
                    if (this.hue > 240)
                    {
                        this.hue -= 240;
                    }
                }
            }

            public int Luminosity
            {
                get { return this.luminosity; }
            }

            public int Hue
            {
                get { return this.hue; }
            }

            public int Saturation
            {
                get { return this.saturation; }
            }

            public override bool Equals(object o)
            {
                if (!(o is HlsColor))
                {
                    return false;
                }
                HlsColor color = (HlsColor)o;
                return (((this.hue == color.hue) && (this.saturation == color.saturation)) && (this.luminosity == color.luminosity));
            }

            public override int GetHashCode()
            {
                return (((this.hue << 6) | (this.saturation << 2)) | this.luminosity);
            }

            private int NewLuma(int luminosity, int n, bool scale)
            {
                if (n == 0)
                {
                    return luminosity;
                }
                if (scale)
                {
                    if (n > 0)
                    {
                        return (int)(((luminosity * (0x3e8 - n)) + (0xf1L * n)) / 0x3e8L);
                    }
                    return ((luminosity * (n + 0x3e8)) / 0x3e8);
                }
                int num = luminosity;
                num += (int)((n * 240L) / 0x3e8L);
                if (num < 0)
                {
                    num = 0;
                }
                if (num > 240)
                {
                    num = 240;
                }
                return num;
            }

            public static Color ColorFromHLS(int hue, int luminosity, int saturation, byte alpha)
            {
                byte num;
                byte num2;
                byte num3;
                if (saturation == 0)
                {
                    num = num2 = num3 = (byte)((luminosity * 0xff) / 240);
                }
                else
                {
                    int num5;
                    if (luminosity <= 120)
                    {
                        num5 = ((luminosity * (240 + saturation)) + 120) / 240;
                    }
                    else
                    {
                        num5 = (luminosity + saturation) - (((luminosity * saturation) + 120) / 240);
                    }
                    int num4 = (2 * luminosity) - num5;
                    num = (byte)(((HueToRGB(num4, num5, hue + 80) * 0xff) + 120) / 240);
                    num2 = (byte)(((HueToRGB(num4, num5, hue) * 0xff) + 120) / 240);
                    num3 = (byte)(((HueToRGB(num4, num5, hue - 80) * 0xff) + 120) / 240);
                }
                return Color.FromArgb(alpha, num, num2, num3);
            }

            private static int HueToRGB(int n1, int n2, int hue)
            {
                if (hue < 0)
                {
                    hue += 240;
                }
                if (hue > 240)
                {
                    hue -= 240;
                }
                if (hue < 40)
                {
                    return (n1 + ((((n2 - n1) * hue) + 20) / 40));
                }
                if (hue < 120)
                {
                    return n2;
                }
                if (hue < 160)
                {
                    return (n1 + ((((n2 - n1) * (160 - hue)) + 20) / 40));
                }
                return n1;
            }
        }
    }

    public class IconHelper
    {
        public static object GetIcon(ImageValue image, IDrawingHelper drawingHelper, string color = null)
        {
            if (string.IsNullOrEmpty(image?.Name))
            {
                return null;
            }
            var path = drawingHelper.GetForguncyImageFullFileName(image.Name, image.BuiltIn);
            return drawingHelper.GetForguncyImageControl(path, image.UseCellTypeForeColor && color != null ? color : image.Color);
        }

        public static object GetBuiltInIcon(string name, IDrawingHelper drawingHelper, string color = "#c0c4cc")
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var folder = System.IO.Path.GetDirectoryName(assemblyLocation);
            var iconFile = System.IO.Path.Combine(folder, "Resources", "images", name + ".svg");

            return drawingHelper.GetSvgControl(iconFile, color);
        }
    }

    public class StyleHelper
    {
        public static CellTypeViewModelStyle CreateStyleViewModel(IStyleInfo style, IDrawingHelper drawingHelper)
        {
            var result = new CellTypeViewModelStyle();

            if(style.FontSize > 0)
            {
                result.FontSize = style.FontSize;
            }
            if (!string.IsNullOrEmpty(style.FontFamily))
            {
                result.FontFamily = drawingHelper.GetFontFamily(style.FontFamily);
            }
            result.TextDecoration = new TextDecorationCollection();
            if (style.Underline == true)
            {
                result.TextDecoration.Add(TextDecorations.Underline);
            }
            if (style.Strikethrough == true)
            {
                result.TextDecoration.Add(TextDecorations.Strikethrough);
            }
            if(style.ForegroundStr != null)
            {
                result.Foreground = drawingHelper.GetBrush(style.ForegroundStr);
            }
            if(style.FontWeight?.ToLower() == "bold")
            {
                result.FontWeight = FontWeights.Bold;
            }
            if (style.FontStyle?.ToLower() == "italic")
            {
                result.FontStyle = FontStyles.Italic;
            }

            return result;
        }
    }

    public class CellTypeViewModelStyle : ModelBase
    {

        private Brush _foreground;
        public Brush Foreground
        {
            get
            {
                return this._foreground;
            }
            set
            {
                if (_foreground != value)
                {
                    this._foreground = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get
            {
                return this._fontSize;
            }
            set
            {
                if (_fontSize != value)
                {
                    this._fontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private FontFamily _fontFamily;
        public FontFamily FontFamily
        {
            get
            {
                return this._fontFamily;
            }
            set
            {
                if (_fontFamily != value)
                {
                    this._fontFamily = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private FontStyle _fontStyle;
        public FontStyle FontStyle
        {
            get
            {
                return this._fontStyle;
            }
            set
            {
                if (_fontStyle != value)
                {
                    this._fontStyle = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private FontWeight _fontWeight;
        public FontWeight FontWeight
        {
            get
            {
                return this._fontWeight;
            }
            set
            {
                if (_fontWeight != value)
                {
                    this._fontWeight = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private TextDecorationCollection _textDecoration;
        public TextDecorationCollection TextDecoration
        {
            get
            {
                return this._textDecoration;
            }
            set
            {
                if (_textDecoration != value)
                {
                    this._textDecoration = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public virtual object Clone()
        {
            return PluginUtilities.CommonClone(this);
        }
    }
}
