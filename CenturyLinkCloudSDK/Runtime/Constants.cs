namespace CenturyLinkCloudSDK.Runtime
{
    /// <summary>
    /// Contains constants for public usage.
    /// </summary>
    internal static class Constants
    {

        internal static class ServiceUris
        {
            internal const string JsonMediaType = "application/json";
            internal const string ApiBaseAddress = "https://api.tier3.com";

            internal static class Authentication
            {
                internal static string Login { get { return Configuration.BaseUri + "/v2/authentication/login"; } }
            }

            internal static class DataCenter
            {
                internal static string GetDataCenters { get { return Configuration.BaseUri + "/v2/datacenters/{0}"; } }
                internal static string GetDataCenter { get { return Configuration.BaseUri + "/v2/datacenters/{0}/{1}"; } }
                internal static string GetDataCenterGroup { get { return Configuration.BaseUri + "/v2/datacenters/{0}/{1}?groupLinks=true"; } }
            }

            internal static class Group
            {
                internal static string GetGroup { get { return Configuration.BaseUri + "/v2/groups/{0}/{1}"; } }
            }

            internal static class Queue
            {
                internal static string GetStatus { get { return Configuration.BaseUri + "/v2/operations/{0}/status/{1}"; } }
            }

            internal static class Server
            {
                internal static string GetServer { get { return Configuration.BaseUri + "/v2/servers/{0}/{1}"; } }
                internal static string PauseServer { get { return Configuration.BaseUri + "/v2/operations/{0}/servers/pause"; } }
                internal static string PowerOnServer { get { return Configuration.BaseUri + "/v2/operations/{0}/servers/powerOn"; } }
                internal static string PowerOffServer { get { return Configuration.BaseUri + "/v2/operations/{0}/servers/powerOff"; } }
                internal static string RebootServer { get { return Configuration.BaseUri + "/v2/operations/{0}/servers/reboot"; } }
                internal static string ShutDownServer { get { return Configuration.BaseUri + "/v2/operations/{0}/servers/shutDown"; } }
                internal static string ResetServer { get { return Configuration.BaseUri + "/v2/operations/{0}/servers/reset"; } }
            }

            internal static class Querystring
            {
                internal const string IncludeGroupLinks = "?groupLinks=true";
            }           
        }

        internal static class ExceptionMessages
        {
            internal const string ServiceExceptionMessage = "A service error has occured.";
            internal const string DataCenterGroupDoesNotHaveRootHardwareGroup = "Data Center Group {0} did not provide a Root Hardware Group.";
        }
    }
}
