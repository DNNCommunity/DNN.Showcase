using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Dnn.Showcase
{
    //[SupportedModules("Dnn.Showcase")]
    //[ValidateAntiForgeryToken]
    public class SiteController : DnnApiController
    {
        DataContext dc = new DataContext();

        [HttpGet]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [AllowAnonymous]
        public HttpResponseMessage Get(

            Nullable<int> page_number = null,
            int page_size = 999,
            string order_by = "name",
            bool ascending = true,

            Nullable<int> category_id = null,
            Nullable<int> user_id = null,
            Nullable<bool> active = null,
            Nullable<bool> random = null
            )
        {
            try
            {
                IQueryable<Community_Showcase_Site> query = dc.Community_Showcase_Sites.AsQueryable();

                // user_id
                if (user_id.HasValue)
                {
                    query = query.Where(i => i.user_id == user_id.GetValueOrDefault());
                }

                // active
                if (active.HasValue)
                {
                    query = query.Where(i => i.is_active == active.GetValueOrDefault());
                }

                // category_id
                if (category_id.HasValue)
                {
                    query = query
                        .Where(i => i.Community_Showcase_SiteCategories
                            .Select(x => x.category_id)
                            .Contains(category_id.GetValueOrDefault()));
                }

                //// random
                //if (random.GetValueOrDefault() == true)
                //{
                //    query = query.OrderBy(i => Guid.NewGuid());
                //}
                //else
                //{
                    // order
                    if (ascending)
                    {
                        query = query.OrderBy(order_by);
                    }
                    else
                    {
                        query = query.OrderByDescending(order_by);
                    }
                //}

                int total_items = query.Count();
                var mod = total_items % page_size;
                var total_pages = (total_items / page_size) + (mod == 0 ? 0 : 1);


                // skip
                var skip = page_size * (page_number - 1);
                if (skip.HasValue)
                {
                    query = query.Skip(skip.GetValueOrDefault());
                }

                // take                
                query = query.Take(page_size);

                var list = query.Select(i => new SiteDTO()
                {
                    id = i.id,
                    name = i.name,
                    is_active = i.is_active,
                    thumbnail = i.thumbnail
                }).ToList();

                var paged_results = new PagedResults<SiteDTO>
                {
                    items = list,
                    page_number = page_number.GetValueOrDefault(),
                    page_size = list.Count,
                    total_pages = total_pages,
                    total_items = total_items
                };

                return Request.CreateResponse(HttpStatusCode.OK, paged_results);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [AllowAnonymous]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Community_Showcase_Site item = dc.Community_Showcase_Sites.Where(i => i.id == id).SingleOrDefault();

                if (item == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, item.ToDto());
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Post(SiteDTO dto)
        {
            try
            {
                Community_Showcase_Site site = dto.ToItem(null);

                int user_id = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID;

                site.user_id = user_id;
                site.created_date = DateTime.Now;

                dc.Community_Showcase_Sites.InsertOnSubmit(site);

                // categories
                foreach (SiteCategoryDTO siteCategoryDTO in dto.site_categories)
                {
                    Community_Showcase_SiteCategory site_category = new Community_Showcase_SiteCategory()
                    {
                        category_id = siteCategoryDTO.category_id
                    };
                    site.Community_Showcase_SiteCategories.Add(site_category);
                }

                var valid = ValidateSite(site);
                if (valid)
                {
                    dc.SubmitChanges();

                    // move temp image
                    var old_path = System.Web.Hosting.HostingEnvironment.MapPath(site.thumbnail);
                    FileInfo fi = new FileInfo(old_path);
                    if (fi.Exists)
                    {
                        string file_path = "/DNN_Showcase/" + site.id.ToString("00000") + ".jpg";
                        string new_path = PortalSettings.HomeDirectoryMapPath + file_path;

                        FileInfo old_file = new FileInfo(new_path);
                        {
                            if (old_file.Exists)
                            {
                                old_file.Delete();
                            }
                        }

                        fi.MoveTo(new_path);
                        site.thumbnail = PortalSettings.HomeDirectory + file_path;
                    }

                    dc.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, dto);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Put(SiteDTO dto)
        {
            try
            {
                Community_Showcase_Site site = dc.Community_Showcase_Sites.Where(i => i.id == dto.id).SingleOrDefault();
                site = dto.ToItem(site);

                // categories
                dc.Community_Showcase_SiteCategories.DeleteAllOnSubmit(site.Community_Showcase_SiteCategories);
                foreach (SiteCategoryDTO siteCategoryDTO in dto.site_categories)
                {
                    Community_Showcase_SiteCategory site_category = new Community_Showcase_SiteCategory()
                    {
                        category_id = siteCategoryDTO.category_id
                    };
                    site.Community_Showcase_SiteCategories.Add(site_category);
                }

                var valid = ValidateSite(site);
                if (valid)
                {
                    // move temp image
                    var old_path = System.Web.Hosting.HostingEnvironment.MapPath(site.thumbnail);
                    FileInfo fi = new FileInfo(old_path);
                    if (fi.Exists)
                    {
                        string file_path = "/DNN_Showcase/" + site.id.ToString("00000") + ".jpg";
                        string new_path = PortalSettings.HomeDirectoryMapPath + file_path;

                        FileInfo old_file = new FileInfo(new_path);
                        {
                            if (old_file.Exists)
                            {
                                old_file.Delete();
                            }
                        }

                        fi.MoveTo(new_path);
                        site.thumbnail = PortalSettings.HomeDirectory + file_path;
                    }

                    dc.SubmitChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, dto);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, dto);
                }
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Community_Showcase_Site item = dc.Community_Showcase_Sites.Where(i => i.id == id).SingleOrDefault();

                if (item == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                dc.Community_Showcase_Sites.DeleteOnSubmit(item);
                dc.SubmitChanges();

                var file_path = System.Web.Hosting.HostingEnvironment.MapPath(item.thumbnail);
                FileInfo fi = new FileInfo(file_path);
                if (fi.Exists)
                {
                    fi.Delete();
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [NonAction]
        public bool ValidateSite(Community_Showcase_Site site)
        {
            bool valid = true;

            // first validate if the URL exists
            valid = ValidateURL(site.url);
            if (!valid && site.url.IndexOf("https://") != -1)
            {
                // if it was a secure URL that failed, try the unsecure version
                valid = ValidateURL(site.url.Replace("https://", "http://"));
            }

            Uri objUri = new Uri(site.url);
            string strValidation = objUri.Scheme + "://" + objUri.Host + "/js/dnn.js";
            valid = ValidateURL(strValidation);
            if (!valid && strValidation.IndexOf("https://") != -1)
            {
                valid = ValidateURL(strValidation.Replace("https://", "http://"));
            }

            return valid;
        }

        [NonAction]
        private bool ValidateURL(string url)
        {
            return ValidateURL(url, "");
        }

        [NonAction]
        private bool ValidateURL(string url, string phrase)
        {
            bool valid = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                request.Referer = url;
                request.Timeout = 10000;
                request.KeepAlive = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebResponse objHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    if (phrase != "")
                    {
                        StreamReader reader = new StreamReader(objHttpWebResponse.GetResponseStream());
                        string content = reader.ReadToEnd();
                        reader.Close();
                        if (content.ToLower().IndexOf(phrase.ToLower()) != -1)
                        {
                            valid = true;
                        }
                    }
                    else
                    {
                        valid = true;
                    }
                }
                objHttpWebResponse.Close();
            }
            catch
            {
                // invalid
            }
            return valid;
        }

        [NonAction]
        public string CreateThumbnail(Community_Showcase_Site site, string home_directory_map_path, string home_directory)
        {
            string thumbnail = "";

            int width = 1980;
            int height = 1080;

            // create directory if necessary
            DirectoryInfo di = new DirectoryInfo(home_directory_map_path + "\\DNN_Showcase");
            if (!di.Exists)
            {
                di.Create();
            }

            try
            {
                thumbnail = home_directory_map_path + "/DNN_Showcase/" + "Site" + site.id.ToString("00000") + ".jpg";

                //var image = WebsiteThumbnail.Capture(site.url, width, height);
                //image.Save(thumbnail);

                //thumbnail = "/Portals/" + PortalSettings.PortalId.ToString() + "/DNN_Showcase/" + "Site" + site.id.ToString("00000") + ".jpg";
            }
            catch (Exception ex)
            {
                throw ex;
                // error
            }

            return thumbnail;
        }

        //[NonAction]
        //public string RefreshThumbnail(Community_Showcase_Site site)
        //{
        //    int width = 1980;
        //    int height = 1080;

        //    try
        //    {
        //        var image = WebsiteThumbnail.Capture(site.url, width, height);
        //        image.Save(this.Request.GetHttpContext().Server.MapPath(site.thumbnail));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        // error
        //    }

        //    return site.thumbnail;
        //}

    }
}


