﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace LGTBWeb
{
    public class IngItem
    {
        public string Amount { get; set; }
        public string Measurement { get; set; }
        public string Ingredient { get; set; }
        public string Price { get; set; }
    }
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Session["RecID"] = 0;
            Session["Rec"] = "";
            Session["UserList"] = new List<IngItem>();
        }
    }
}