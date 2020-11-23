using Autofac;
using ProjectFacebook.ApplicationLayer.Services.Abstraction;
using ProjectFacebook.ApplicationLayer.Services.Concrete;
using ProjectFacebook.DomainLayer.UnitOfWork.Abstraction;
using ProjectFacebook.InfrastructureLayer.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.IoC.Containers
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            
        }
    }
}
