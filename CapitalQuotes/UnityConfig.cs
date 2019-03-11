using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace CapitalQuotes
{
    class UnityConfig
    {
        public static IUnityContainer container;
        public IUnityContainer RegisterDependencies()
        {
            container = new UnityContainer();
            // Nlog
            Logger _logger = NLog.LogManager.GetCurrentClassLogger();
            _logger.Info("[UnityConfig RegisterDependencies()] Registering log..");
            container.RegisterInstance(typeof(Logger), _logger);

            // Google pub/sub

            // Capital API instance.
            container.RegisterType<SKAPI.CapitalConnection>(new InjectionConstructor(_logger));

                        
            return container;
        }
    }
}
