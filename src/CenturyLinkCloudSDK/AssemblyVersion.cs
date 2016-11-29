using System;
using System.Reflection;

namespace CenturyLinkCloudSDK
{
    internal static class AssemblyVersion
    {
        public static string Version
        {
            get
            {
                Assembly assembly = typeof(AssemblyVersion).GetTypeInfo().Assembly;
                Version version = assembly.GetName().Version;

                return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
            }
        }
    }
}
