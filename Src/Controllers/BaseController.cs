﻿using Src.Models.Service.Repository;
using Src.Models.ViewData.Base;
using System.Linq;
using System.Web.Mvc;

namespace Src.Controllers
{
    public class BaseController : Controller
    {
        #region variable
        protected object Data;
        protected IUnitOfWork _unitOfWork;
        protected Common.Resualt Resualt = new Common.Resualt();
        #endregion

        public BaseController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        #region authorize & check action
        Common.Resualt IsAuthorize(string token)
        {
            bool isExsit = true;
            if (isExsit)
            {
                bool status = true;

                if (status)
                {
                    return Resualt = new Common.Resualt
                    {
                        Message = Common.ResualtMessage.OK,
                    };
                }
                else
                {
                    return Resualt = new Common.Resualt
                    {
                        Message = Common.ResualtMessage.AccountIsBlock,
                    };
                }
            }
            else
            {
                return Resualt = new Common.Resualt
                {
                    Message = Common.ResualtMessage.NotFound
                };
            }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region variable
            bool IsPublicAction;
            string Token, Action, Controller;
            string[] Login = { "logout", "changepass" },
                     outLogin = { "index", "login", "register", "resetpass" };
            #endregion

            #region get info
            Token = filterContext.RouteData.Values["Token"].ToString();
            Action = filterContext.RouteData.Values["Action"].ToString();
            Controller = filterContext.RouteData.Values["Controller"].ToString();
            IsPublicAction = filterContext.ActionDescriptor.GetCustomAttributes(true).Count() > 0;
            ActionResult GetResponse(Common.Resualt resualt, string redirectPath = null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    if (redirectPath != null)
                    {
                        return new JsonResult
                        {
                            Data = new { resualt, Redirect = redirectPath }
                        };
                    }
                    else
                    {
                        return new JsonResult
                        {
                            Data = resualt
                        };
                    }
                }
                else
                {
                    ViewBag.Resualt = resualt;
                    if (redirectPath != null)
                    {
                        return new RedirectResult(redirectPath);
                    }
                    else
                    {
                        return new ViewResult();
                    }
                }
            }
            #endregion

            #region check is public
            if (IsPublicAction)
            {
                Resualt.Message = Common.ResualtMessage.OK;
                filterContext.Result = GetResponse(Resualt);
            }
            else
            {
                #region authorize
                Resualt = IsAuthorize(Token);
                if (Resualt.Message != Common.ResualtMessage.OK && !outLogin.Contains(Action))
                {
                    filterContext.Result = GetResponse(Resualt, "/Account");
                }
                else if (Resualt.Message == Common.ResualtMessage.OK && !Login.Contains(Action))
                {
                    filterContext.Result = GetResponse(Resualt, "/");
                }
                #endregion
            }
            #endregion

            base.OnActionExecuted(filterContext);
        }
        #endregion
    }
}