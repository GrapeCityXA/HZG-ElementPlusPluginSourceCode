using ElementUI.Properties;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElementUI
{
    [DependenceRuntimModule("vue3")]
    [SRCategory(nameof(Resources.ElementUI_DisplayName))]
    [SupportUsingScope(PageScope.AllPCPage, ListViewScope.None)]
    public abstract class ElementCellTypeBase : CellType, IDisablePropertySettings
    {
        public virtual ForguncyDisabledSettings GetDisabledPropertySettings(ForguncyPageKind pageType)
        {
            return ForguncyDisabledSettings.Bold |
                ForguncyDisabledSettings.Underline |
                ForguncyDisabledSettings.Italic |
                ForguncyDisabledSettings.TopAlignment |
                ForguncyDisabledSettings.MiddleAlignment |
                ForguncyDisabledSettings.BottomAlignment |
                ForguncyDisabledSettings.RightAlignment |
                ForguncyDisabledSettings.CenterAlignment |
                ForguncyDisabledSettings.LeftAlignment;
        }
    }

    public abstract class InputCellTypeBase : ElementCellTypeBase,
        ISupportDefaultValue,
        ICommandCellType,
        ISupportUIPermission,
        ISupportDataValidation,
        ISupportDisable
    {
        [SRDisplayName(nameof(Resources.Common_CommandList))]
        public virtual List<Command> CommandList { get; set; } = new List<Command>();
        public virtual CommandExcuteKind CommandExcuteKind => CommandExcuteKind.OnValueChanged;

        [SRDisplayName(nameof(Resources.Common_DataValidationLink))]
        [JsonIgnore]
        [DefaultValue(null)]
        public virtual DataValidationLink DataValidationLink { get; set; }

        List<UIPermission> _uIPermissions;
        [SRDisplayName(nameof(Resources.Common_UIPermissions))]
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public virtual List<UIPermission> UIPermissions
        {
            get
            {
                if (_uIPermissions == null)
                {
                    List<UIPermissionScope> permissions = new List<UIPermissionScope>() { UIPermissionScope.Enable, UIPermissionScope.Visible };
                    if(this is ISupportReadOnly)
                    {
                        permissions.Add(UIPermissionScope.Editable);
                    }
                    _uIPermissions = Utils.GetDefaultPermission(permissions.ToArray());
                }
                return _uIPermissions;
            }
            set
            {
                if (value != null)
                {
                    value = value.Where(i =>
                    {
                        if (i.Scope == UIPermissionScope.Editable)
                        {
                            return this is ISupportReadOnly;
                        }
                        return true;
                    }).ToList();
                }
                _uIPermissions = value;
            }
        }

        [FormulaProperty]
        [SRDisplayName(nameof(Resources.Common_DefaultValue))]
        public virtual object DefaultValue { get; set; }

        public virtual bool NeedFormatDefaultValue
        {
            get
            {
                return true;
            }
        }

        [SRDisplayName(nameof(Resources.Common_IsDisabled))]
        public virtual bool IsDisabled
        {
            get;
            set;
        }

        public override SupportFeatures SupportFeatures
        {
            get
            {
                return SupportFeatures.ShouldCheckDirtyWhenLeavePage;
            }
        }

        public override ForguncyDisabledSettings GetDisabledPropertySettings(ForguncyPageKind pageType)
        {
            return ForguncyDisabledSettings.TopAlignment |
                ForguncyDisabledSettings.MiddleAlignment |
                ForguncyDisabledSettings.BottomAlignment |
                ForguncyDisabledSettings.RightAlignment |
                ForguncyDisabledSettings.CenterAlignment |
                ForguncyDisabledSettings.LeftAlignment;
        }
    }

    internal class SRDisplayNameAttribute : DisplayNameAttribute
    {
        public SRDisplayNameAttribute(string name)
            : base(ResourceHelper.GetString(name))
        {
        }
    }
    internal class SRFormulaPropertyAttribute : FormulaPropertyAttribute
    {
        public SRFormulaPropertyAttribute()
        {
        }
        public override string ContextVariables 
        { 
            get => ResourceHelper.GetString(base.ContextVariables); 
            set => base.ContextVariables = value;
        }
    }
    internal class SRDescriptionAttribute : DescriptionAttribute
    {
        public SRDescriptionAttribute(string name)
            : base(ResourceHelper.GetString(name))
        {
        }
    }

    internal class SRCategoryAttribute : CategoryAttribute
    {
        public SRCategoryAttribute(string category)
            : base(ResourceHelper.GetString(category))
        {
        }
    }
    internal class SRItemDisplayNameAttribute : ItemDisplayNameAttribute
    {
        public SRItemDisplayNameAttribute(string name)
            : base(ResourceHelper.GetString(name))
        {
        }
    }

    internal class SRCustomCommandObjectAttribute : CustomCommandObjectAttribute
    {
        public override string InitParamValues
        {
            get => ResourceHelper.GetString(base.InitParamValues);
            set => base.InitParamValues = value;
        }
    }
    internal class SRBindingDataSourcePropertyAttribute : BindingDataSourcePropertyAttribute
    {
        public override string Columns
        {
            get => ResourceHelper.GetString(base.Columns);
            set => base.Columns = value;
        }   
        public override string ColumnsDescription
        {
            get => ResourceHelper.GetString(base.ColumnsDescription);
            set => base.ColumnsDescription = value;
        }
    }
    internal class SRTreePropertyAttribute : TreePropertyAttribute
    {
        public override string DefaultNodeName
        {
            get => ResourceHelper.GetString(base.DefaultNodeName);
            set => base.DefaultNodeName = value;
        }
    }  
    internal class SRComboPropertyAttribute : ComboPropertyAttribute
    {
        public override string DisplayList
        {
            get => ResourceHelper.GetString(base.DisplayList);
            set => base.DisplayList = value;
        }
        public override string ValueList
        {
            get => ResourceHelper.GetString(base.ValueList);
            set => base.ValueList = value;
        }
    }   
    internal class SRRadioGroupPropertyAttribute : RadioGroupPropertyAttribute
    {
        public override string DisplayList
        {
            get => ResourceHelper.GetString(base.DisplayList);
            set => base.DisplayList = value;
        }
        public override string ValueList
        {
            get => ResourceHelper.GetString(base.ValueList);
            set => base.ValueList = value;
        }
    }      
    internal class SRCategoryHeaderAttribute : CategoryHeaderAttribute
    {
        public SRCategoryHeaderAttribute(string name)
            : base(ResourceHelper.GetString(name))
        {
        }
    }
    internal class SRIntPropertyAttribute : IntPropertyAttribute
    {
        public override string Watermark 
        {
            get => ResourceHelper.GetString(base.Watermark); 
            set => base.Watermark = value; 
        }
    }
    internal class SRDoublePropertyAttribute : DoublePropertyAttribute
    {
        public override string Watermark 
        {
            get => ResourceHelper.GetString(base.Watermark); 
            set => base.Watermark = value; 
        }
    }
    internal class IconPropertyAttribute : ImageValuePropertyAttribute
    {
        public IconPropertyAttribute()
        {
            this.SupportUseCellColor = false;
            this.DefaultActiveTabIndex = 1;
            this.DefaultIconColor = "#c0c4cc";
        }
    }
    static class ResourceHelper
    {
        public static string GetString(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            var srStr = Resources.ResourceManager.GetString(name);
            if(srStr == null)
            {
                return name;
            }
            return srStr;
        }
    }
}
