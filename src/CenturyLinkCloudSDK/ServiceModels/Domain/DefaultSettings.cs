
namespace CenturyLinkCloudSDK.ServiceModels
{
    public class DefaultSettings
    {
        public IntegerLimit Cpu { get; set; }

        public IntegerLimit MemoryGB { get; set; }

        public StringLimit NetworkId { get; set; }

        public StringLimit PrimaryDns { get; set; }

        public StringLimit SecondaryDns { get; set; }

        public StringLimit TemplateName { get; set; }
    }
}
