using Autofac;
using TWYK.Core.Infrastructure;
using TWYK.Core.Infrastructure.DependencyManagement;
using TWYK.Web.Factories;

namespace TWYK.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        #region Implementation of IDependencyRegistrar

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder) {

            //Factories
            builder.RegisterType<ShoppingCartModelFactory>().As<IShoppingCartModelFactory>().InstancePerLifetimeScope();
        }

        public int Order => 1;

        #endregion
    }
}