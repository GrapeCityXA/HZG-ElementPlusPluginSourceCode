using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Automation;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private DialogUserControl _dialogControl;

        public DialogUserControl DialogControl
        {
            get
            {
                return this._dialogControl;
            }
            set
            {
                if (this._dialogControl != value)
                {
                    this._dialogControl = value;
                    this._dialogControl.CommitValue += _dialogControl_CommitValue;
                    AutomationProperties.SetAutomationId(this, $"FATWindow_{_dialogControl.GetType().Name}");
                }
            }
        }

        private void _dialogControl_CommitValue(object sender, EventArgs e)
        {
            if (this.DialogControl.Validate())
            {
                this.DialogResult = true;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is UIElement)
            {
                (sender as UIElement).Focus();
            }
            if (DialogControl.Validate() == true)
            {
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var win = GetWindow(button);
            if (win == null)
            {
                return;
            }

            win.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    OKButton_Click(sender, e);
                    break;

                case Key.Escape:
                    CancelButton_Click(sender, e);
                    break;
            }
        }
    }

    public interface IValidateValueDialog
    {
        bool Validate();
    }

    public class DialogUserControl : UserControl, IValidateValueDialog
    {
        public event EventHandler CommitValue;

        protected virtual void OnCommitValue()
        {
            if (this.CommitValue != null)
            {
                this.CommitValue(this, EventArgs.Empty);
            }
        }

        public virtual bool Validate()
        {
            return true;
        }
    }

    public class ModelBase : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
