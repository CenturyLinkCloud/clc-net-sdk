using CenturyLinkCloudSDK.Runtime;
using CenturyLinkCloudSDK.ServiceModels;
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
        internal static string RoundNumberToNearestUpperLimit(this int? number)
        {
            if (number == null)
            {
                return Constants.GeneralMessages.NullNotValid;
            }

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

        internal static AssetMeasure ConvertAssetMeasure(this int number, string unitOfMeasure)
        {
            if (number < 0 || string.IsNullOrEmpty(unitOfMeasure))
            {
                return null;
            }

            var assetMeasure = new AssetMeasure();

            if(unitOfMeasure == Constants.Metrics.Bytes)
            {
                if (number < 1024)
                {
                    assetMeasure.Total = number;
                    assetMeasure.UnitOfMeasure = unitOfMeasure;
                    return assetMeasure;
                }

                if (number >= 1024 && number < 1048576)
                {
                    var convertedNumber = Math.Truncate((double)number / 1024);

                    assetMeasure.Total = (int)convertedNumber;
                    assetMeasure.UnitOfMeasure = Constants.Metrics.KiloBytes;
                    return assetMeasure;
                }

                if (number >= 1048576 && number < 1073741824)
                {
                    var convertedNumber = Math.Truncate((double)number / 1048576);
                    assetMeasure.Total = (int)convertedNumber;
                    assetMeasure.UnitOfMeasure = Constants.Metrics.MegaBytes;
                    return assetMeasure;
                }
            }

            if (unitOfMeasure == Constants.Metrics.KiloBytes)
            {
                if (number < 1024)
                {
                    assetMeasure.Total = number;
                    assetMeasure.UnitOfMeasure = unitOfMeasure;
                    return assetMeasure;
                }

                if (number >= 1024 && number < 1048576)
                {
                    var convertedNumber = Math.Truncate((double)number / 1024);

                    assetMeasure.Total = (int)convertedNumber;
                    assetMeasure.UnitOfMeasure = Constants.Metrics.MegaBytes;
                    return assetMeasure;
                }

                if (number >= 1048576 && number < 1073741824)
                {
                    var convertedNumber = Math.Truncate((double)number / 1048576);
                    assetMeasure.Total = (int)convertedNumber;
                    assetMeasure.UnitOfMeasure = Constants.Metrics.GigaBytes;
                    return assetMeasure;
                }
            }

            if (unitOfMeasure == Constants.Metrics.MegaBytes)
            {
                if (number < 1024)
                {
                    assetMeasure.Total = number;
                    assetMeasure.UnitOfMeasure = unitOfMeasure;
                    return assetMeasure;
                }

                if (number >= 1024 && number < 1048576)
                {
                    var convertedNumber = Math.Truncate((double)number / 1024);

                    assetMeasure.Total = (int)convertedNumber;
                    assetMeasure.UnitOfMeasure = Constants.Metrics.GigaBytes;
                    return assetMeasure;
                }

                if (number >= 1048576 && number < 1073741824)
                {
                    var convertedNumber = Math.Truncate((double)number / 1048576);
                    assetMeasure.Total = (int)convertedNumber;
                    assetMeasure.UnitOfMeasure = Constants.Metrics.TeraBytes;
                    return assetMeasure;
                }
            }

            if (unitOfMeasure == Constants.Metrics.GigaBytes)
            {
                if (number < 1024)
                {
                    assetMeasure.Total = number;
                    assetMeasure.UnitOfMeasure = unitOfMeasure;
                    return assetMeasure;
                }

                if (number >= 1024)
                {
                    var convertedNumber = Math.Truncate((double)number / 1024);

                    assetMeasure.Total = (int)convertedNumber;
                    assetMeasure.UnitOfMeasure = Constants.Metrics.TeraBytes;
                    return assetMeasure;
                }
            }

            return null;
        }
    }
}
