using MyPersonalProject.Services;
using MyPersonalProject.Services.Interfaces;
using MyPersonalProject.Services.Services;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace MyPersonalProject.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IFileUploadService, FileUploadService>();

            container.RegisterType<IDataProvider, SqlDataProvider>(
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);


            //container.RegisterType<IPrincipal>(new TransientLifetimeManager(),
            //        new InjectionFactory(con => HttpContext.Current.User));
        }
    }
}