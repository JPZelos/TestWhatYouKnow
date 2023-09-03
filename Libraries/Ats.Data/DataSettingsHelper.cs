using TWYK.Core.Data;
using TWYK.Core.Domain;
using TWYK.Core.Infrastructure;

namespace TWYK.Data
{
    public class DataSettingsHelper
    {
        private static bool? _databaseIsInstalled;
       
        public static bool DatabaseIsInstalled() {
           
            var _customerRepository = EngineContext.Current.Resolve<IRepository<Customer>>();
            return _customerRepository.CanConnectToDb(new Customer());
        }
    }
}