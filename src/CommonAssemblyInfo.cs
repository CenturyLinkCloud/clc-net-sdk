using System.Reflection;

[assembly: AssemblyCompany("CenturyLink Cloud")]
[assembly: AssemblyProduct("CenturyLink Cloud SDK")]
[assembly: AssemblyCopyright("Apache 2.0 License")]
[assembly: AssemblyTrademark("")]

#if PORTABLE
[assembly: AssemblyMetadata("PCL", "True")]
#endif

#if DEBUG
#if NET_4_5
[assembly: AssemblyConfiguration(".NET 4.5 Debug")]
#elif PORTABLE
[assembly: AssemblyConfiguration("Portable Debug")]
#else
[assembly: AssemblyConfiguration("Debug")]
#endif
#else
#if NET_4_5
[assembly: AssemblyConfiguration(".NET 4.5")]
#elif PORTABLE
[assembly: AssemblyConfiguration("Portable")]
#else
[assembly: AssemblyConfiguration("")]
#endif
#endif