using Newtonsoft.Json.Linq;
using System;

namespace CenturyLinkCloudSDK.Extensions
{
    /// <summary>
    /// This class contains extension methods.
    /// </summary>
    internal static class StringExtensions
    {
        internal static JObject TryParseJson(this String jsonString)
        {
            try
            {
                var jsonObject = JObject.Parse(jsonString);
                return jsonObject;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
