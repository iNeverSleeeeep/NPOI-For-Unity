using System;

namespace NPOI.OpenXml4Net.OPC
{
	/// Storage class for configuration storage parameters.
	/// TODO xml syntax checking is no longer done with DOM4j parser -&gt; remove the schema or do it ?
	///
	/// @author CDubettier, Julen Chable
	/// @version 1.0
	public class Configuration
	{
		private static string pathForXmlSchema = Environment.CurrentDirectory + "\\src\\schemas";

		public static string PathForXmlSchema
		{
			get
			{
				return pathForXmlSchema;
			}
			set
			{
				pathForXmlSchema = value;
			}
		}
	}
}
