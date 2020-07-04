using System;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Dnn.Showcase
{
    public class SiteJob : DotNetNuke.Services.Scheduling.SchedulerClient
    {
        public SiteJob(DotNetNuke.Services.Scheduling.ScheduleHistoryItem objScheduleHistoryItem) : base()
        {
            ScheduleHistoryItem = objScheduleHistoryItem;
        }

        public override void DoWork()
        {
            try
            {
                string strMessage = this.Processing();
                ScheduleHistoryItem.Succeeded = true;
                ScheduleHistoryItem.AddLogNote("Succeeded. " + strMessage);
            }
            catch (Exception exc)
            {
                ScheduleHistoryItem.Succeeded = false;
                ScheduleHistoryItem.AddLogNote("Failed. " + exc.Message);
                Errored(ref exc);
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
            }
        }

        public string Processing()
        {
            DataContext dc = new DataContext();
            SiteController siteController = new SiteController();

            string message = "";

            var sites = dc.Community_Showcase_Sites.Where(i => i.is_active == true).ToList();

            foreach (var site in sites)
            {
                //PortalController objPortalController = new PortalController();
                //PortalInfo objPortalSettings = objPortalController.GetPortal(objSite.PortalID);
                //ModuleController objModules = new ModuleController();
                //Hashtable objModuleSettings = objModules.GetModuleSettings(objSite.ModuleID);

                int intRefresh = 7;
                if (DateTime.Now.Subtract(site.created_date).Days % intRefresh == 0)
                {
                    string url = site.url;
                    message += "<br />Processing: " + url + "<br />";
                    if (siteController.ValidateSite(site))
                    {
                        message += "- Site Validated Successfully" + "<br />";

                        //var image = WebsiteThumbnail.Capture(site.url, 1920, 1080);
                        //image.Save(HostingEnvironment.MapPath(site.thumbnail));

                        //message += "- Created New Thumbnail: " + site.thumbnail + "<br />";
                    }
                    else
                    {
                        message += "- Validation Issue... Disabling Site" + "<br />";
                        site.is_active = false;
                        dc.SubmitChanges();
                        //UserInfo objUser = UserController.GetUserById(objSite.PortalID, objSite.UserID);
                        //if ((objUser != null))
                        //{
                        //    try
                        //    {
                        //        DotNetNuke.Services.Mail.Mail.SendEmail(objUser.Email, objPortalSettings.Email, objPortalSettings.PortalName + " Site Gallery", "Your Site " + strURL + " Could Not Be Validated And Was Removed From The Gallery.");
                        //    }
                        //    catch
                        //    {
                        //        // error sending email
                        //    }
                        //}
                    }
                }
            }

            return message;
        }

    }
}


