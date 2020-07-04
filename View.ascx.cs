using DotNetNuke.Services.Exceptions;
using System;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Modules;

namespace Dnn.Showcase
{
    partial class View : ModuleBase, IActionable
    {
        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                if (IsEditable)
                {
                    Actions.Add(GetNextActionID(), "Manage Sites", ModuleActionType.AddContent, "", "", EditUrl("SiteList"), false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                    Actions.Add(GetNextActionID(), "Manage Categories", ModuleActionType.AddContent, "", "", EditUrl("CategoryList"), false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                }
                return Actions;
            }
        }

        protected new void Page_Load(Object sender, EventArgs e)
        {
            
            try
            {
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }      
    }
}
