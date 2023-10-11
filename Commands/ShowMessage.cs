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
    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Message.png")]
    [SRDisplayName(nameof(Resources.ShowMessage_ShowMessage))]
    public class ShowMessage : ELCommandBase
    {
        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessage_Message))]
        [DefaultValue(null)]
        [Required]
        public object Message { get; set; }

        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessage_Type))]
        [SRComboProperty(ValueList = "success|warning|info|error", DisplayList = nameof(Resources.Type_DisplayList))]
        public string Type { get; set; } = "info";

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessage_Duration))]
        [SRDescription(nameof(Resources.ShowMessage_Duration_Description))]
        public object Duration { get; set; }

        [FormulaProperty]
        [SearchableProperty]
        [SRDisplayName(nameof(Resources.ShowMessage_Offset))]
        [SRDescription(nameof(Resources.ShowMessage_Offset_Description))]
        [DefaultValue(20)]
        public object Offset { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessage_ShowClose))]
        [DefaultValue(false)]
        public bool ShowClose { get; set; }

        [SRDisplayName(nameof(Resources.ShowMessage_Center))]
        [DefaultValue(false)]
        public bool Center { get; set; }

        public override string ToString()
        {
            return Resources.ShowMessage;
        }

        public override void InitDefaultPropertyValues()
        {
            base.InitDefaultPropertyValues();
            this.Message = Resources.ShowMessage_Message_DefaultValue;
            this.Duration = 3000;
            this.Offset = 20;
        }
    }
}
