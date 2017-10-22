using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace Wiki
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<Models.Biz.Interfaces.IArticleRepository, Models.DAL.Articles>();
            container.RegisterType<Models.Biz.Interfaces.IUtilisateurRepository, Models.DAL.Utilisateurs>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}

//Add your Unity registrations in the RegisterComponents method of the UnityConfig class. All components that implement IDisposable should be
//registered with the HierarchicalLifetimeManager to ensure that they are properly disposed at the end of the request.

//It is not necessary to register your controllers with Unity.