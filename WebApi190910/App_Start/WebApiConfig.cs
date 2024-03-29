﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi190910.ActionFilters;

namespace WebApi190910
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            config.Filters.Add(new MyExceptionAttribute());

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            //json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
