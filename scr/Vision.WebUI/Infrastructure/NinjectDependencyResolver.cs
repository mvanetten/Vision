using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Vision.Domain.Abstract;
using Vision.Domain.Concrete;
using Ninject;
using Microsoft.AspNet.Identity;

namespace Vision.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver 
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type ServiceType)
        {
            return kernel.TryGet(ServiceType);
        }

        public IEnumerable<object> GetServices(Type ServiceType)
        {
            return kernel.GetAll(ServiceType);
        }

        public void AddBindings()
        {
            kernel.Bind<IDocumentRepository>().To<DocumentRepository>();
            kernel.Bind<IContactRepository>().To<ContactRepository>();
            kernel.Bind<ITaxRepository>().To<TaxRepository>();
            kernel.Bind<ISettingRepository>().To<SettingRepository>();
            //
            kernel.Bind<IMailSender>().To<MailSender>();
        }
    }
}