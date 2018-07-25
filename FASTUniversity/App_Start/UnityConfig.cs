using FASTUniversity.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace FASTUniversity.App_Start
{
	public class UnityConfig
	{
        public static void RegisterComponents()
        {
            var container = new UnityContainer();


            container.RegisterType<UnitOfWork>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}