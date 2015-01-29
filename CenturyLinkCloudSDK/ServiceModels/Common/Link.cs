namespace CenturyLinkCloudSDK.ServiceModels.Common
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class Link
    {
        public string Rel { get; set; }

        public string Href { get; set; }

        public string[] Verbs { get; set; }

        public string Id { get; set; }
    }
}
