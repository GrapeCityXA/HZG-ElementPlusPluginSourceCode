using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ElementUI.Properties;

namespace ElementUI.Commands
{
    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/MessageBox.png")]
    [SRDisplayName(nameof(Resources.ShowMessageBox_ShowMessageBox))]
    public class ShowMessageBox : ELCommandBase
    {
        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessageBox_Title))]
        [Required]
        public object Title { get; set; }

        [FormulaProperty(AcceptsReturn = true)]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessageBox_Message))]
        public object Message { get; set; }

        [SearchableProperty]
        [SRComboProperty(ValueList = "none|success|warning|info|error", DisplayList = nameof(Resources.ShowMessage_ShowMessage_Type_DisplayList))]
        [SRDisplayName(nameof(Resources.ShowMessageBox_Type))]
        public string Type { get; set; } = "none";

        [SRDisplayName(nameof(Resources.ShowMessageBox_ShowConfirmButton))]
        [DefaultValue(true)]
        public bool ShowConfirmButton { get; set; } = true;

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessageBox_ConfirmButtonText))]
        public object ConfirmButtonText { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessageBox_ShowCancelButton))]
        public bool ShowCancelButton { get; set; }

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessageBox_CancelButtonText))]
        public object CancelButtonText { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessageBox_ShowClose))]
        [DefaultValue(true)]
        public bool ShowClose { get; set; } = true;

        [SRDisplayName(nameof(Resources.ShowMessageBox_DialogResult))]
        [SRDescription(nameof(Resources.ShowMessageBox_DialogResultDescription))]
        [ResultToProperty]
        public string DialogResult { get; set; }

        [ObjectProperty(ObjType = typeof(AdvancedSetting))]
        [SRDisplayName(nameof(Resources.AdvancedSettings))]
        public AdvancedSetting AdvancedSettings { get; set; } = new AdvancedSetting();

        public override bool GetDesignerPropertyVisible(string propertyName, CommandScope commandScope)
        {
            if (propertyName == nameof(ConfirmButtonText))
            {
                return ShowConfirmButton;
            }

            if (propertyName == nameof(CancelButtonText))
            {
                return ShowCancelButton;
            }

            return base.GetDesignerPropertyVisible(propertyName, commandScope);
        }

        public override string ToString()
        {
            return Resources.ShowMessageBox;
        }

        public override void InitDefaultPropertyValues()
        {
            base.InitDefaultPropertyValues();
            this.Title = Resources.ShowMessage_Title_DefaultValue;
            this.Message = Resources.ShowMessage_Message_Box_DefaultValue;
            this.ConfirmButtonText = Resources.ShowMessage_ConfirmButtonText_DefaultValue;
            this.CancelButtonText = Resources.ShowMessage_CancelButtonText_DefaultValue;
        }
    }

    public class AdvancedSetting : ObjectPropertyBase
    {
        [SRDisplayName(nameof(Resources.ShowMessageBox_ShowInput))]
        public bool ShowInput { get; set; } = false;

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessageBox_InputPlaceholder))]
        public object InputPlaceholder { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessageBox_InputType))]
        public InputType InputType { get; set; } = InputType.text;

        [SRDisplayName(nameof(Resources.ShowMessageBox_InputBoxResult))]
        [ResultToProperty]
        public string InputBoxResult { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessageBox_Center))]
        public bool Center { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessageBox_RoundButton))]
        public bool RoundButton { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessageBox_CloseOnClickModal))]
        public bool CloseOnClickModal { get; set; } = false;

        [SRDisplayName(nameof(Resources.ShowMessageBox_CloseOnPressEscape))]
        public bool CloseOnPressEscape { get; set; } = false;

        [SRDisplayName(nameof(Resources.ShowMessageBox_DistinguishCancelAndClose))]
        [SRDescription(nameof(Resources.ShowMessage_DistinguishCancelAndClose_Description))]
        public bool DistinguishCancelAndClose { get; set; } = false;

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (
                propertyName == nameof(InputPlaceholder) ||
                propertyName == nameof(InputType) ||
                propertyName == nameof(RoundButton) ||
                propertyName == nameof(InputBoxResult)
            )
            {
                return ShowInput;
            }

            return base.GetDesignerPropertyVisible(propertyName);
        }
    }

    public enum InputType
    {
        [SRDescription(nameof(Resources.ShowMessageBox_text))]
        text,

        [SRDescription(nameof(Resources.ShowMessageBox_textarea))]
        textarea,
    }
}