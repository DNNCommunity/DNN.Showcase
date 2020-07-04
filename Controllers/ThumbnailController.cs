using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Dnn.Showcase
{
    //[SupportedModules("Dnn.Showcase")]
    //[ValidateAntiForgeryToken]
    public class ThumbnailController : DnnApiController
    {
        [HttpPost]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Save(ThumbnailDTO dto)
        {
            try
            {
                int portal_id = PortalSettings.PortalId;
                string directory_path = "/portals/" + portal_id.ToString() + "/DNN_Showcase/";
                string directory_map_path = Request.GetHttpContext().Server.MapPath(directory_path);
                DirectoryInfo di = new DirectoryInfo(directory_map_path);
                if (!di.Exists)
                {
                    di.Create();
                }

                var index = dto.image.IndexOf("base64");

                string base64 = dto.image.Substring(index + 7);
                var img = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64)));

                string file_name = dto.name + ".jpg";
                img.Save(directory_map_path + file_name, System.Drawing.Imaging.ImageFormat.Jpeg);

                return Request.CreateResponse(HttpStatusCode.OK, directory_path + file_name);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}


