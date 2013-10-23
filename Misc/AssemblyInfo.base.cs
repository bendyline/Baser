using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyCompany("Bendyline, LLC")]
[assembly: AssemblyCopyright("Copyright © Bendyline LLC 2013")]
[assembly: AssemblyTrademark("")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.%BUILDNUMBER%.%REVISIONNUMBER%")]


public class AssemblyInfo
{
	public static String Copyright = "Copyright © Bendyline LLC 2013";
	public static int MajorVersion = 1;
	public static int MinorVersion = 0;
	public static int BuildNumber = %BUILDNUMBER%;
	public static int RevisionNumber = %REVISIONNUMBER%;
}