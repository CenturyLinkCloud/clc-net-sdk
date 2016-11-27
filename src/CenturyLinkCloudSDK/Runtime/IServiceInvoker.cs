using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Runtime
{
    public interface IServiceInvoker
    {
        Task<TResponse> Invoke<TResponse>(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
            where TResponse : class;
    }
}
