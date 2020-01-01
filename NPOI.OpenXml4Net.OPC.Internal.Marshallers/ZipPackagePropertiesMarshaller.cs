using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace NPOI.OpenXml4Net.OPC.Internal.Marshallers
{
	/// Package core properties marshaller specialized for zipped package.
	///
	/// @author Julien Chable
	public class ZipPackagePropertiesMarshaller : PackagePropertiesMarshaller
	{
		public override bool Marshall(PackagePart part, Stream out1)
		{
			if (!(out1 is ZipOutputStream))
			{
				throw new ArgumentException("ZipOutputStream expected!");
			}
			ZipOutputStream zipOutputStream = (ZipOutputStream)out1;
			string zipItemNameFromOPCName = ZipHelper.GetZipItemNameFromOPCName(part.PartName.URI.ToString());
			ZipEntry entry = new ZipEntry(zipItemNameFromOPCName);
			try
			{
				zipOutputStream.PutNextEntry(entry);
				base.Marshall(part, out1);
				StreamHelper.SaveXmlInStream(xmlDoc, out1);
				zipOutputStream.CloseEntry();
			}
			catch (IOException ex)
			{
				throw new OpenXml4NetException(ex.Message);
			}
			catch
			{
				return false;
			}
			return true;
		}
	}
}
