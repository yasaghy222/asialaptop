﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Src.Models.ViewData.Table
{
    public class Admin
    {

        public class ViewAdmin
        {
            public string Name { get; set; }
            public string Family { get; set; }
            public int RoleId { get; set; }
            public int RoleName { get; set; }
            public bool Status { get; set; }
        }

        public class AInfo
        {
            public string FullName { get; set; }
            public string RoleName { get; set; }
            public int RoleID { get; set; }
            public string Token { get; set; }
        }
    }
}