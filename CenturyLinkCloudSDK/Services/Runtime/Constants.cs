namespace CenturyLinkCloudSDK.Services.Runtime
{
    /// <summary>
    /// Contains constants for public usage.
    /// </summary>
    public static class Constants
    {

        public static class ServiceUris
        {
            public const string JsonMediaType = "application/json";
            public const string ApiBaseAddress = "https://api.tier3.com";

            public static class Authentication
            {
                public const string Login = "https://api.tier3.com/v2/authentication/login";
            }

            public static class DataCenter
            {
                public const string GetDataCenters = "https://api.tier3.com/v2/datacenters/{0}";
                public const string GetDataCenter = "https://api.tier3.com/v2/datacenters/{0}/{1}";
                public const string GetDataCenterGroup = "https://api.tier3.com/v2/datacenters/{0}/{1}?groupLinks=true";
            }

            public static class Group
            {
                public const string GetGroup = "https://api.tier3.com/v2/groups/{0}/{1}";
            }

            public static class Queue
            {
                public const string GetStatus = "https://api.tier3.com/v2/operations/{0}/status/{1}";
            }

            public static class Server
            {
                public const string GetServer = "https://api.tier3.com/v2/servers/{0}/{1}";
                public const string PauseServer = "https://api.tier3.com/v2/operations/{0}/servers/pause";
                public const string PowerOnServer = "https://api.tier3.com/v2/operations/{0}/servers/powerOn";
                public const string PowerOffServer = "https://api.tier3.com/v2/operations/{0}/servers/powerOff";
                public const string RebootServer = "https://api.tier3.com/v2/operations/{0}/servers/reboot";
                public const string ShutDownServer = "https://api.tier3.com/v2/operations/{0}/servers/shutDown";
                public const string ResetServer = "https://api.tier3.com/v2/operations/{0}/servers/reset";
            }

            public static class Querystring
            {
                public const string IncludeGroupLinks = "?groupLinks=true";
            }           
        }

        public static class ExceptionMessages
        {
            public const string ServiceExceptionMessage = "A service error has occured.";
        }
    }
}
