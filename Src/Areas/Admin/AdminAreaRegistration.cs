﻿using System.Web.Mvc;

namespace Src.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin/Dashboard",
                "Admin/Dashboard",
                new { action = "Dashboard", Controller = "Dashboard" }
            );

            context.MapRoute(
               "Al-Manage/Login",
               "Al-Manage/Login",
               new { action = "Login", Controller = "Account" }
            );

            context.MapRoute(
             "AL-Manage",
             "AL-Manage",
             new { action = "Index", Controller = "Dashboard" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}