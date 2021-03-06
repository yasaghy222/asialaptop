﻿using Mapster;
using Src.Models.Data;
using Src.Models.Service.Repository;
using Src.Models.Utitlity;
using Src.Models.ViewData.Base;
using Src.Models.ViewData.Table;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static Src.App_Start.FilterConfig;

namespace Src.Controllers.Api
{
    public class AuthorizeApi : ActionFilterAttribute
    {
        #region variable
        protected object Data;
        #endregion

        #region authorize & authenticate
        Common.Result IsAuthorize(string token)
        {
            string hashToken = Function.GenerateHash(token);
            using (ALDBEntities aLDB = new ALDBEntities())
            {
                Tbl_Admin admin = aLDB.Tbl_Admin.Single(item => item.Token == hashToken && item.Status);
                if (admin != null)
                {
                    if (admin.Status)
                    {
                        return new Common.Result
                        {
                            Message = Common.ResultMessage.OK,
                            Data = admin.RoleID
                        };
                    }
                    else
                    {
                        return new Common.Result
                        {
                            Message = Common.ResultMessage.AccountIsBlock,
                        };
                    }
                }
                else
                {
                    return new Common.Result
                    {
                        Message = Common.ResultMessage.NotFound
                    };
                }
            }
        }
        Common.Result HasPermisstion(int roleID, string action, string controller)
        {
            return new Common.Result
            {
                Message = Common.ResultMessage.OK
            };
        }
        #endregion

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            #region get info
            var isPublicAction = context.ActionContext.ActionDescriptor.GetCustomAttributes<PublicAction>().Any();
            void SetResponse(object data) => context.Response = context.Request.CreateResponse(data);
            #endregion

            #region check is public
            if (!isPublicAction)
            {
                #region get authorize info
                var token = context.ActionContext.ControllerContext.RouteData.Values["Token"].ToString();
                var action = context.ActionContext.ActionDescriptor.ActionName;
                var controller = context.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                #endregion

                #region authorize
                var result = IsAuthorize(token);
                if (result.Message == Common.ResultMessage.OK)
                {
                    #region check permission
                    result = HasPermisstion((int)result.Data, action, controller);
                    if (result.Message != Common.ResultMessage.OK)
                    {
                        SetResponse(result);
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region validate model state
            HttpMethod method = context.Request.Method;
            if (method == HttpMethod.Post)
            {
                if (!context.ActionContext.ModelState.IsValid)
                {
                    SetResponse(new Common.Result { Message = Common.ResultMessage.BadRequest });
                }
            }
            #endregion

            base.OnActionExecuted(context);
        }
    }

    [AuthorizeApi]
    public class BaseApiController : ApiController
    {
        #region variable
        protected object Data;
        protected IUnitOfWork _unitOfWork;
        protected NameValueCollection FormData;
        protected Common.Result Result = new Common.Result();
        #endregion

        public BaseApiController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        #region common function
        /// <summary>
        /// return list of product brands
        /// </summary>
        /// <returns></returns>
        protected async Task<ICollection<Common.Select>> GetBrandList() => await Task.Run(() => _unitOfWork.ProcBrand.Get().Adapt<ICollection<Common.Select>>());

        /// <summary>
        /// return list of product cat
        /// </summary>
        /// <returns></returns>
        protected async Task<ICollection<Common.Tree>> GetCatList() => await Task.Run(() => _unitOfWork.ProcCat.Get().Adapt<ICollection<Common.Tree>>());
        #endregion
    }
}
