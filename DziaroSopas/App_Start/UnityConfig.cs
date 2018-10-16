using System.Web.Mvc;
using ServiceLayer;
using ServiceLayer.Inventory;
using Unity;
using Unity.Mvc5;

namespace DziaroSopas
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IInventorySerivce, InventoryService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}