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

            internal static class Account
            {
                internal static string GetRecentActivity { get { return "{0}/v2/search/activities"; } }
            }

            internal static class Alerts
            {
                internal static string GetAlertPoliciesForAccount { get { return "{0}/v2/alertPolicies/{1}"; } }
            }

            internal static class Authentication
            {
                internal static string Login { get { return "{0}/v2/authentication/login"; } }
            }

            internal static class Billing
            {
                internal static string GetAccountBillingDetails { get { return "{0}/v2/billing/{1}/total"; } }

                internal static string GetGroupBillingDetails { get { return "{0}/v2/groups/{1}/{2}/billing"; } }

                internal static string GetServerResourceUnitPricing { get { return "{0}/v2/billing/{1}/serverPricing/{2}"; } }
            }

            internal static class DataCenter
            {
                internal static string GetDataCenters { get { return "{0}/v2/datacenters/{1}{2}"; } }

                internal static string GetDataCenter { get { return "{0}/v2/datacenters/{1}/{2}{3}"; } }

                internal static string GetRecentActivity { get { return Account.GetRecentActivity; } }
            }

            internal static class Group
            {
                internal static string GetGroup { get { return "{0}/v2/groups/{1}/{2}"; } }

                internal static string GetRecentActivity { get { return Account.GetRecentActivity; } }
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

                internal static string StartMaintenance { get { return "{0}/v2/operations/{1}/servers/startMaintenance"; } }

                internal static string StopMaintenance { get { return "{0}/v2/operations/{1}/servers/stopMaintenance"; } }
                
                internal static string GetServerStatistics { get { return "{0}/v2/servers/{1}/{2}/statistics{3}"; } }
            }

            internal static class Querystring
            {
                internal const string IncludeGroupLinks = "?groupLinks=true";
                internal const string GetLatestStatistics = "?type=latest";
                internal const string IncludeTotalAssets = "?totals=true";
                internal const string IncludeGroupLinksAndTotalAssets = "?groupLinks=true&totals=true";
            }   
        }

        internal static class Metrics
        {
            internal const string Cpu = "cpu";
            internal const string Memory = "memory";
            internal const string Disk = "disk";

            internal const string Bytes = "B";
            internal const string KiloBytes = "KB";
            internal const string MegaBytes = "MB";
            internal const string GigaBytes = "GB";
            internal const string TeraBytes = "TB";
        }

        internal static class ExceptionMessages
        {
            internal const string DefaultServiceExceptionMessage = "A service error has occured.";
            internal const string AlertGenerationExceptionMessage = "An error occured while trying to generate alerts.";
            internal const string DataCenterGroupDoesNotHaveRootHardwareGroup = "Data Center Group {0} did not return a Root Hardware Group.";
            internal const string GroupDoesNotHaveServers = "Group {0} did not return any Servers.";
        }

        internal static class AlertMessages
        {
            internal const string CpuAlert = "CPU is above threshold";
            internal const string MemoryAlert = "Memory is above threshold";
            internal const string DiskUsageAlert = "Disk Usage is above threshold";
        }

        internal static class GeneralMessages
        {
            internal const string NegativeNumberNotValid = "Negative numbers are not valid in this context.";
            internal const string NullNotValid = "Nulls are not valid in this context.";
            internal const string RoundingNotAccounted = "Number rounding not accounted for.";
        }

        internal static class EntityTypes
        {
            internal const string Server = "Server";
            internal const string Group = "Group";
        }
    }
}
