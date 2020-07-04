using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Dnn.Showcase
{
    //[SupportedModules("Dnn.Showcase")]
    //[ValidateAntiForgeryToken]
    public class CategoryController : DnnApiController
    {
        DataContext dc = new DataContext();

        [HttpGet]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [AllowAnonymous]
        public HttpResponseMessage Get(Nullable<int> skip, Nullable<int> take)
        {
            try
            {
                IQueryable<Community_Showcase_Category> query = dc.Community_Showcase_Categories.AsQueryable();

                // skip
                if (skip.HasValue)
                {
                    query = query.Skip(skip.GetValueOrDefault());
                }

                // take
                if (take.HasValue)
                {
                    query = query.Take(take.GetValueOrDefault());
                }

                query = query.OrderBy(i => i.name);

                List<CategoryDTO> dtos = new List<CategoryDTO>();

                foreach (Community_Showcase_Category category in query)
                {
                    CategoryDTO categoryDTO = category.ToDto();
                    dtos.Add(categoryDTO);
                }
                return Request.CreateResponse(HttpStatusCode.OK, dtos);
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
                Community_Showcase_Category item = dc.Community_Showcase_Categories.Where(i => i.id == id).SingleOrDefault();

                if (item == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, item.ToDto()); ;
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage Post(CategoryDTO dto)
        {
            try
            {
                Community_Showcase_Category category = dto.ToItem(null);

                int user_id = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID;

                dc.Community_Showcase_Categories.InsertOnSubmit(category);

                dc.SubmitChanges();
                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage Put(CategoryDTO dto)
        {
            try
            {
                Community_Showcase_Category category = dc.Community_Showcase_Categories.Where(i => i.id == dto.id).SingleOrDefault();
                category = dto.ToItem(category);

                dc.SubmitChanges();
                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Community_Showcase_Category item = dc.Community_Showcase_Categories.Where(i => i.id == id).SingleOrDefault();

                if (item == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                dc.Community_Showcase_Categories.DeleteOnSubmit(item);
                dc.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}


