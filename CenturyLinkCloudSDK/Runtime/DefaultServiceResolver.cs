using CenturyLinkCloudSDK.ServiceModels;
using CenturyLinkCloudSDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    internal class DefaultServiceResolver : IServiceResolver
    {
        static readonly IDictionary<Type, Func<Authentication, IServiceInvoker, ServiceBase>> resolvers =
            new Dictionary<Type, Func<Authentication, IServiceInvoker, ServiceBase>>
            {
                { typeof(AuthenticationService), 
                    (a, i) => new AuthenticationService(i) },
                { typeof(DataCenterService),
                    (a, i) => new DataCenterService(a, i, ResolveImpl<GroupService>(a, i)) },
                { typeof(GroupService),
                    (a, i) => new GroupService(a, i, ResolveImpl<ServerService>(a, i)) },
                { typeof(ServerService),
                    (a, i) => new ServerService(a, i) },
                { typeof(QueueService),
                    (a, i) => new QueueService(a, i) },
                { typeof(AlertService),
                    (a, i) => new AlertService(a, i) },
                { typeof(BillingService),
                    (a, i) => new BillingService(a, i, ResolveImpl<DataCenterService>(a, i)) },
                { typeof(AccountService),
                    (a, i) => new AccountService(a, i, ResolveImpl<DataCenterService>(a, i)) }
            };

        static T ResolveImpl<T>(Authentication authentication, IServiceInvoker serviceInvoker)
            where T : ServiceBase
        {
            return (T)resolvers[typeof(T)](authentication, serviceInvoker);
        }

        public T Resolve<T>(Authentication authentication, IServiceInvoker serviceInvoker)
            where T : ServiceBase
        {
            return DefaultServiceResolver.ResolveImpl<T>(authentication, serviceInvoker);
        }
    }
}
