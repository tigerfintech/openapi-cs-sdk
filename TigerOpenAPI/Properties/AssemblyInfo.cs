using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("TigerOpenAPI")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Tiger Brokers")]
[assembly: AssemblyProduct("TigerOpenAPI")]
[assembly: AssemblyCopyright("Copyright 2014-2023 Tiger Brokers")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Expose internals to the test project so unit tests can reach internal helpers
// such as StockPriceUtil.FindTickSize without making them part of the public API.
[assembly: InternalsVisibleTo("TigerOpenAPI.Tests")]

[assembly: ComVisible(false)]

//[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.2.3")]
