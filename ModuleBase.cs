using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client.ClientResourceManagement;
using System;

namespace Dnn.Showcase
{
    public class ModuleBase : PortalModuleBase
    {
        protected string ApiUrlBase
        {
            get
            {
                if (DotNetNuke.Application.DotNetNukeContext.Current.Application.CurrentVersion.Major < 9)
                {
                    return "/desktopmodules/Dnn.Showcase/api";
                }
                return "/api/Dnn.Showcase";
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);

            ClientResourceManager.RegisterStyleSheet(this.Page, ResolveUrl("https://use.fontawesome.com/releases/v5.7.2/css/all.css"), 1);
            ClientResourceManager.RegisterStyleSheet(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/plugins/angular-toastr/angular-toastr.min.css"), 1);            
            ClientResourceManager.RegisterStyleSheet(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/plugins/slim/slim.min.css"), 2);
            
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("https://ajax.googleapis.com/ajax/libs/angularjs/1.7.8/angular.min.js"), 2);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("https://ajax.googleapis.com/ajax/libs/angularjs/1.7.8/angular-messages.min.js"), 3);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("https://ajax.googleapis.com/ajax/libs/angularjs/1.7.8/angular-animate.min.js"), 3);
            
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/plugins/angular-toastr/angular-toastr.tpls.min.js"), 5);            
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/plugins/ui.bootstrap/ui-bootstrap-tpls-3.0.6.min.js"), 6);            
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/plugins/slim/slim.angular.js"), 7);

            // app
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/app.js"), 7);

            // services            
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/services/site.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/services/category.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/services/thumbnail.js"), 15);

            // directives
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/directives/view.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/directives/site-list.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/directives/category-list.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/directives/modal.js"), 15);

            // controllers
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/view.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/admin-site-list.js"), 15);

            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/thumbnail/thumbnail-upload.js"), 15);

            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/category/category-list.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/category/category-edit.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/category/category-delete.js"), 15);

            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/site/site-list.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/site/site-edit.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/site/site-delete.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/site/site-list-modal.js"), 15);
            ClientResourceManager.RegisterScript(this.Page, ResolveUrl("/DesktopModules/Dnn.Showcase/app/controllers/site/site-detail.js"), 15);
        }
    }
}