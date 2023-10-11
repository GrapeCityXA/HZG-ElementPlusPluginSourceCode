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

namespace ElementUI.WpfControls
{
    /// <summary>
    /// Interaction logic for CellStyleTextBlock.xaml
    /// </summary>
    public partial class CellStyleTextBlock : UserControl
    {
        #region Text

        public static readonly DependencyProperty TextProperty =
                     DependencyProperty.Register(
                         "Text",
                         typeof(string),
                         typeof(CellStyleTextBlock),
                         new PropertyMetadata(null, OnTextChangedChunk));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChangedChunk(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CellStyleTextBlock).OnTextChanged(e);
        }
        protected virtual void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ViewModel.Text = e.NewValue as string;

        }

        #endregion

        #region CellStyle

        public static readonly DependencyProperty CellStyleProperty =
                     DependencyProperty.Register(
                         "CellStyle",
                         typeof(CellTypeViewModelStyle),
                         typeof(CellStyleTextBlock),
                         new PropertyMetadata(null, OnCellStyleChangedChunk));

        public CellTypeViewModelStyle CellStyle
        {
            get { return (CellTypeViewModelStyle)GetValue(CellStyleProperty); }
            set { SetValue(CellStyleProperty, value); }
        }

        private static void OnCellStyleChangedChunk(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CellStyleTextBlock).OnCellStyleChanged(e);
        }
        protected virtual void OnCellStyleChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ViewModel.Style = e.NewValue as CellTypeViewModelStyle;
        }

        #endregion

        public CellStyleTextBlock()
        {
            InitializeComponent();
            this.root.DataContext = new CellStyleTextBlockViewModel();
        }

        public CellStyleTextBlockViewModel ViewModel
        {
            get
            {
                return this.root.DataContext as CellStyleTextBlockViewModel;
            }
        }
    }

    public class CellStyleTextBlockViewModel : ModelBase
    {
        private string _text;
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if (_text != value)
                {
                    this._text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private CellTypeViewModelStyle _style;
        public CellTypeViewModelStyle Style
        {
            get
            {
                return this._style;
            }
            set
            {
                if (_style != value)
                {
                    this._style = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
