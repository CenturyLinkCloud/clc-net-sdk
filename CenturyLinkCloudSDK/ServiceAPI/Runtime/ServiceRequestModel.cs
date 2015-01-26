namespace CenturyLinkCloudSDK.ServiceAPI.Runtime
{
    /// <summary>
    /// This base class inherited by all service request models provides an array that can store request body data.
    /// The purpose of this array is to be used by requests that do not need to be serialized from specific types,
    /// but rather contain an unnamed array of data. The UnNamedArray property is used in the Invoke method,
    /// of the ServiceAPI base class, in order to determine the type of serialization to perform.
    /// </summary>
    public class ServiceRequestModel
    {
        public string[] UnNamedArray { get; set; }
    }
}
