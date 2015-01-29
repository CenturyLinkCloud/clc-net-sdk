namespace CenturyLinkCloudSDK.ServiceModels.Common
{
    /// <summary>
    /// POCO class used for deserialization/serialization of data provided to or returned from API calls.
    /// </summary>
    public class PortDetail
    {
        public string Protocol { get; set; }

        public int Port { get; set; }

        public int PortTo { get; set; }
    }
}
