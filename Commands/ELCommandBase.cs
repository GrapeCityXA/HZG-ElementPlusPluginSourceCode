using ElementUI.Properties;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementUI
{
    [DependenceRuntimModule("vue3")]
    [SRCategory(nameof(Resources.ElementUI_DisplayName))]
    [CommandSupportUsingScope(CommandPageScope.AllPCPage)]
    public abstract class ELCommandBase : Command
    {
        public override CommandScope GetCommandScope()
        {
            return CommandScope.Cell | CommandScope.ListView | CommandScope.PageLoad | CommandScope.ShareCommand;
        }
    }
}
