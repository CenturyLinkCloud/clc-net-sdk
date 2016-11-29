using CenturyLinkCloudSDK.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    /// <summary>
    /// A stub for resolving dependencies
    /// This is more separation of concerns than extensibility since
    /// the services aren't public right now anyway.
    /// </summary>
    internal interface IServiceResolver
    {
        T Resolve<T>(Authentication authentication, IServiceInvoker serviceInvoker)
            where T : ServiceBase;
    }

    internal static class ServiceResolver
    {
        public static T Resolve<T>(this IServiceResolver resolver, Authentication authentication)
            where T : ServiceBase
        {
            return resolver.Resolve<T>(authentication, Configuration.ServiceInvoker);
        }
    }
}
