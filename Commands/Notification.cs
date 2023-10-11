using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementUI.Properties;

namespace ElementUI.Commands
{

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Notification.png")]
    [SRDisplayName(nameof(Resources.Notification_ShowNotification))]
    public class ShowNotification : ELCommandBase
    {
        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.Notification_Title))]
        [DefaultValue(null)]
        [Required]
        public object Title { get; set; }

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.Notification_Message))]
        [DefaultValue(null)]
        [Required]
        public object Message { get; set; }

        [SearchableProperty]
        [SRDisplayName(nameof(Resources.Notification_Type))]
        [SRComboProperty(ValueList = "success|warning|info|error", DisplayList = nameof(Resources.Type_DisplayList))]
        public string Type { get; set; } = "success";

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.Notification_Duration))]
        [SRDescription(nameof(Resources.ShowNotification_Duration_Description))]
        public object Duration { get; set; }

        [SearchableProperty]
        [SRDisplayName(nameof(Resources.Notification_Position))]
        [SRComboProperty(ValueList = "top-right|top-left|bottom-right|bottom-left", DisplayList = nameof(Resources.Notification_Position_DisplayList))]
        public string Position { get; set; } = "top-right";

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.Notification_Offset))]
        [SRDescription(nameof(Resources.ShowNotification_Offset_Description))]
        [DefaultValue(0)]
        public object Offset { get; set; } = 0;

        [SRDisplayName(nameof(Resources.Notification_ShowClose))]
        [DefaultValue(true)]
        public bool ShowClose { get; set; } = true;

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.Notification_Context))]
        [DefaultValue(null)]
        [SRDescription(nameof(Resources.Notification_Context_Description))]
        public object Context { get; set; }

        [SRCustomCommandObject(InitParamProperties = "title|message|context", InitParamValues = nameof(Resources.ShowNotification_Command_InitParamValues))]
        [SRDisplayName(nameof(Resources.Notification_Command))]
        public object Command { get; set; }

        public override string ToString()
        {
            return Resources.Notification;
        }

        public override void InitDefaultPropertyValues()
        {
            base.InitDefaultPropertyValues();
            this.Title = Resources.ShowNotification_Title_DefaultValue;
            this.Message = Resources.ShowNotification_Message_DefaultValue;
            this.Duration = 4500;
        }
    }
}
