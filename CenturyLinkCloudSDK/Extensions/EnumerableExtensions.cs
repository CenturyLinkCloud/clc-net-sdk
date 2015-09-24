using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.Extensions
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Splits the sequence into up to numberOfPartitions sequences of relatively equal size
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="source">The sequence</param>
        /// <param name="numberOfPartitions">The number of sequences to split into</param>
        /// <returns>A set of sequences that when concatenated would return source</returns>
        static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int numberOfPartitions)
        {
            var elementsPerChunk = (int)Math.Ceiling((decimal)source.Count() / (decimal)numberOfPartitions);
            var chunk = new List<T>();
            foreach (var i in source)
            {
                chunk.Add(i);
                if (chunk.Count == elementsPerChunk)
                {
                    yield return chunk;
                    chunk = new List<T>();
                }
            }
            if (chunk.Count > 0) yield return chunk;
        }

        /// <summary>
        /// Based on pfxteam stuff from here: http://blogs.msdn.com/b/pfxteam/archive/2012/03/05/10278165.aspx
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="U">The result type</typeparam>
        /// <param name="source">The sequence</param>
        /// <param name="body">The body</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task that completes when tasks for the body of each source element have been completed</returns>
        internal static Task<IEnumerable<U>> SelectEachAsync<T, U>(this IEnumerable<T> source, Func<T, Task<U>> body, CancellationToken cancellationToken)
        {
            return SelectEachAsync(source, Configuration.MaxConcurrentBulkHttpRequests, body, cancellationToken);
        }

        /// <summary>
        /// Based on pfxteam stuff from here: http://blogs.msdn.com/b/pfxteam/archive/2012/03/05/10278165.aspx
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <typeparam name="U">The result type</typeparam>
        /// <param name="source">The sequence</param>
        /// <param name="dop">The degree of parallelism</param>
        /// <param name="body">The body</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task that completes when tasks for the body of each source element have been completed</returns>
        internal static Task<IEnumerable<U>> SelectEachAsync<T, U>(this IEnumerable<T> source, int dop, Func<T, Task<U>> body, CancellationToken cancellationToken)
        {
            
            return 
                Task.WhenAll(
                    source
                        .Partition(dop)
                        .Select(
                            t =>
                                Task.Run(
                                    async delegate
                                    {                                        
                                        var result = new List<U>();
                                        foreach (var i in t) { result.Add(await body(i)); };
                                        return result;
                                    }, cancellationToken)))
                    .ContinueWith(t => t.Result.SelectMany(x => x));
        }
    }
}
