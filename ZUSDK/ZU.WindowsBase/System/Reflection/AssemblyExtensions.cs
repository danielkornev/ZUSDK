using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Reflection
{
	public static partial class AssemblyExtensions
	{
		// source: http://www.java2s.com/Code/CSharp/Reflection/Detectswhetheranassemblyisimplementingaspecificinterfaceornot.htm


		/// <summary>
		/// Detects whether an assembly is implementing a specific interface or not.
		/// </summary>
		/// <param name="interfaceName">The String containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <returns>true if one of the assembly classes implements the specified interface; otherwise, false.</returns>
		public static bool IsInterfaceImplemented(this Assembly assembly, string interfaceName)
		{
			// ensure interface name is not null
			if (interfaceName != string.Empty && interfaceName != null && interfaceName.Length > 0)
			{
				// Next we'll loop through all the Types found in the assembly
				foreach (Type pluginType in assembly.GetTypes())
				{
					if (pluginType.IsPublic) // Only look at public types
					{
						if (!pluginType.IsAbstract)  // Only look at non-abstract types
						{
							try
							{
								// Gets a type object of the interface we need the assembly to match
								Type typeInterface = pluginType.GetInterface(interfaceName, true);

								// Make sure the interface we want to use actually exists
								if (typeInterface != null)
								{
									return true;
								}
							}
							catch (ReflectionTypeLoadException ex)
							{
								StringBuilder sb = new StringBuilder();
								foreach (Exception exSub in ex.LoaderExceptions)
								{
									sb.AppendLine(exSub.Message);
									FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
									if (exFileNotFound != null)
									{
										if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
										{
											sb.AppendLine("Fusion Log:");
											sb.AppendLine(exFileNotFound.FusionLog);
										}
									}
									sb.AppendLine();
								}
								string errorMessage = sb.ToString();
								//Display or log the error based on your application.
							}
							catch (Exception ex)
							{
								string errorMessage = ex.Message;
							}
						}
					}
				}
			}
			return false;
		}
	} // class
} // namespace
