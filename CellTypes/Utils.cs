using GrapeCity.Forguncy.CellTypes;
using System.Collections.Generic;
using System.Linq;

namespace ElementUI
{
    public class Utils
    {
        public enum OrderWeight
        {
            Input,
            InputNumber,
            Select,
            Cascader,
            DatePicker,
            TimePicker,
            Calendar,
            Avatar,
            Upload,
            Tag,
            Rate,
            Slider,
            Pagination,
            NavMenu,
            TabHeader,
            Breadcrumb,
            Progress,
            Steps,
            Timeline,
            Transfer,
            BackupTop
        };

        public static List<UIPermission> GetDefaultPermission(params UIPermissionScope[] scopes)
        {
            return scopes.Select(scope => new UIPermission()
            {
                Scope = scope,
                AllowRoles = new List<string>() { "FGC_Anonymous" }
            }).ToList();
        }
    }
}
