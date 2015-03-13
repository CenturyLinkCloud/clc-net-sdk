using CenturyLinkCloudSDK.Runtime;
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

    internal static class NumberExtensions
    {
        internal static string RoundNumberToNearestUpperLimit(this int number)
        {
            if(number < 0)
            {
                return Constants.GeneralMessages.NegativeNumberNotValid;
            }

            if (number <= 999)
            {
                return number.ToString();
            }

            if (number >= 1000 && number <= 999999)
            {
                var roundedNumber = Math.Truncate((double)number / 1000);
                return string.Format("{0}{1}", roundedNumber, "K");
            }

            if (number >= 1000000 && number <= 999999999)
            {
                var roundedNumber = Math.Truncate((double)number / 1000000);
                return string.Format("{0}{1}", roundedNumber, "M");
            }

            return Constants.GeneralMessages.RoundingNotAccounted;
        }
    }
}
