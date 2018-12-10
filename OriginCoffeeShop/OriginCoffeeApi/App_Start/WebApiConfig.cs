using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using OriginCoffeeApi.App_Start;
using CML.Business;
using CML.Models.Model;
using Unity.Lifetime;

namespace OriginCoffeeApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //Dependency Injection
            var container = new UnityContainer();
            container.RegisterType<IProductCategoriesBusiness<ProductCategories>, ProductCategoriesBusiness<ProductCategories>>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
