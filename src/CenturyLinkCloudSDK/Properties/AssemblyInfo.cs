using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;


#if NET_4_5
[assembly: AssemblyTitle("CenturyLink Cloud SDK .NET 4.5")]
#elif PORTABLE
[assembly: AssemblyTitle("CenturyLink Cloud SDK Portable")]
#else
[assembly: AssemblyTitle("CenturyLink Cloud SDK")]
#endif

[assembly: AssemblyDescription("")]
[assembly: AssemblyCulture("")]
//[assembly: CLSCompliant(true)]

//#if !PORTABLE
//[assembly: AllowPartiallyTrustedCallers]
//#endif