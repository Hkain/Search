using BusnesLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Controllers.FactoryController
{
    public class Composition
    {
        private readonly IControllerFactory controllerFactory;

        public Composition()
        {
            controllerFactory = CreateControllerFactory();
        }

        public IControllerFactory ControllerFactory
        {
            get { return controllerFactory; }
        }

        private static IControllerFactory CreateControllerFactory()
        {
            string connString = ConfigurationManager.ConnectionStrings["UserModelContainer"].ConnectionString;

            string userRepositoryTypeName = ConfigurationManager.AppSettings["SqlRepositoryType"];

            string folder = AppDomain.CurrentDomain.BaseDirectory + @"bin\";

            string[] files = Directory.GetFiles(folder, "DataAcces.dll");
            Type t = null;
            foreach (string file in files)
            {
                Assembly assembly = Assembly.LoadFile(file);
                foreach (Type type in assembly.GetTypes())
                {

                    foreach (object attr in type.GetCustomAttributes(false))
                    {
                        InfoAttributes infoAttr = attr as InfoAttributes;
                        if (infoAttr != null && infoAttr.Name == NameSignatyre.SqlRepository)
                        {
                            t = type;
                        }
                    }
                }
            }

            var repository = (UserRepository)Activator.CreateInstance(t, connString);
            var controllerFactory = new UserControllerFactory(repository);
            return controllerFactory;
        }
    }

    internal class UserControllerFactory : DefaultControllerFactory
    {
        private readonly Dictionary<string, Func<RequestContext, IController>> controllerMap;
        public UserControllerFactory(UserRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            controllerMap = new Dictionary<string, Func<RequestContext, IController>>();
            controllerMap["Home"] = ctx => new HomeController(repository);
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return this.controllerMap[controllerName](requestContext);
        }
    }

}