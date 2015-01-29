
namespace CenturyLinkCloudSDK.ServiceModels.Interfaces
{
    /// <summary>
    /// Interface all service responses must inherit from. It provides a Response property of Object type
    /// that must be present in all responses in order to streamline the deserialization of JSON data.
    /// The Response property is converted to the appropriate model in the response classes that implement this interface.
    /// </summary>
    internal interface IServiceResponse
    {
        object Response { get; set; }
    }
}
