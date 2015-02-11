using Newtonsoft.Json.Linq;
using System;

namespace CenturyLinkCloudSDK.Extensions
{
    /// <summary>
    /// This class contains extension methods.
    /// </summary>
    public static class StringExtensions
    {
        public static string CreateDeserializableJsonString(this String jsonString)
        {
            var result = string.Format("{0}" + jsonString.Replace("{", "{{").Replace("}", "}}") + "{1}", "{ \"Response\":", "}");
            return result;
        }

        public static JObject TryParseJson(this String jsonString)
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
