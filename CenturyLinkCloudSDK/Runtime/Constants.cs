﻿namespace CenturyLinkCloudSDK.Runtime
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
                internal static string Login { get { return "{0}/v2/authentication/login"; } }
            }

            internal static class DataCenter
            {
                internal static string GetDataCenters { get { return "{0}/v2/datacenters/{1}"; } }
                internal static string GetDataCenter { get { return "{0}/v2/datacenters/{1}/{2}"; } }
                internal static string GetDataCenterGroup { get { return "{0}/v2/datacenters/{1}/{2}?groupLinks=true"; } }
            }

            internal static class Group
            {
                internal static string GetGroup { get { return "{0}/v2/groups/{1}/{2}"; } }
                internal static string GetGroupBillingDetails { get { return "{0}/v2/groups/{1}/{2}/billing"; } }
            }

            internal static class Queue
            {
                internal static string GetStatus { get { return "{0}/v2/operations/{1}/status/{2}"; } }
            }

            internal static class Server
            {
                internal static string GetServer { get { return "{0}/v2/servers/{1}/{2}"; } }
                internal static string PauseServer { get { return "{0}/v2/operations/{1}/servers/pause"; } }
                internal static string PowerOnServer { get { return "{0}/v2/operations/{1}/servers/powerOn"; } }
                internal static string PowerOffServer { get { return "{0}/v2/operations/{1}/servers/powerOff"; } }
                internal static string RebootServer { get { return "{0}/v2/operations/{1}/servers/reboot"; } }
                internal static string ShutDownServer { get { return "{0}/v2/operations/{1}/servers/shutDown"; } }
                internal static string ResetServer { get { return "{0}/v2/operations/{1}/servers/reset"; } }
            }

            internal static class Querystring
            {
                internal const string IncludeGroupLinks = "?groupLinks=true";
            }   
        
            internal static class Alerts
            {
                internal static string GetAlertPoliciesForAccount { get { return "{0}/v2/alertPolicies/{1}"; } }
            }
        }

        internal static class ExceptionMessages
        {
            internal const string ServiceExceptionMessage = "A service error has occured.";
            internal const string DataCenterGroupDoesNotHaveRootHardwareGroup = "Data Center Group {0} did not return a Root Hardware Group.";
            internal const string GroupDoesNotHaveServers = "Group {0} did not return any Servers.";
        }
    }
}
