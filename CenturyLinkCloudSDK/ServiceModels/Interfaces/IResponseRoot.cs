
namespace CenturyLinkCloudSDK.ServiceModels.Interfaces
{
    /// <summary>
    /// Interface all service responses must inherit from. It provides a Response property of generic type
    /// that must be present in all responses in order to streamline the deserialization of JSON data.
    /// </summary>
    internal interface IResponseRoot<T>
    {
        T Response { get; set; }
    }
}
