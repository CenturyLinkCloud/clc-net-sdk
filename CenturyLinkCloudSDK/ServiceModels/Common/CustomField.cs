namespace CenturyLinkCloudSDK.ServiceModels.Common
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class CustomField
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string DisplayValue { get; set; }
    }
}
