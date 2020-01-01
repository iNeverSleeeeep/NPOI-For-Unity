using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC;
using System;
using System.IO;

namespace NPOI.Util
{
	/// Provides handy methods to work with OOXML namespaces
	///
	/// @author Yegor Kozlov
	public class PackageHelper
	{
		public static OPCPackage Open(Stream is1)
		{
			try
			{
				return OPCPackage.Open(is1);
			}
			catch (InvalidFormatException ex)
			{
				throw new POIXMLException(ex);
			}
		}

		/// Clone the specified namespace.
		///
		/// @param   pkg   the namespace to clone
		/// @param   file  the destination file
		/// @return  the Cloned namespace
		public static OPCPackage Clone(OPCPackage pkg, string path)
		{
			OPCPackage oPCPackage = OPCPackage.Create(path);
			PackageRelationshipCollection relationships = pkg.Relationships;
			foreach (PackageRelationship item in relationships)
			{
				PackagePart part = pkg.GetPart(item);
				if (item.RelationshipType.Equals("http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties"))
				{
					CopyProperties(pkg.GetPackageProperties(), oPCPackage.GetPackageProperties());
				}
				else
				{
					oPCPackage.AddRelationship(part.PartName, item.TargetMode.Value, item.RelationshipType);
					PackagePart packagePart = oPCPackage.CreatePart(part.PartName, part.ContentType);
					Stream outputStream = packagePart.GetOutputStream();
					IOUtils.Copy(part.GetInputStream(), outputStream);
					outputStream.Close();
					if (part.HasRelationships)
					{
						Copy(pkg, part, oPCPackage, packagePart);
					}
				}
			}
			oPCPackage.Close();
			return OPCPackage.Open(path);
		}

		/// Creates an empty file in the default temporary-file directory,
		public string CreateTempFile()
		{
			return TempFile.GetTempFilePath("poi-ooxml-", ".tmp");
		}

		/// Recursively copy namespace parts to the destination namespace
		private static void Copy(OPCPackage pkg, PackagePart part, OPCPackage tgt, PackagePart part_tgt)
		{
			PackageRelationshipCollection relationships = part.Relationships;
			if (relationships != null)
			{
				foreach (PackageRelationship item in relationships)
				{
					if (item.TargetMode == TargetMode.External)
					{
						part_tgt.AddExternalRelationship(item.TargetUri.ToString(), item.RelationshipType, item.Id);
					}
					else
					{
						Uri targetUri = item.TargetUri;
						if (targetUri.Fragment != null)
						{
							part_tgt.AddRelationship(targetUri, item.TargetMode.Value, item.RelationshipType, item.Id);
						}
						else
						{
							PackagePartName partName = PackagingUriHelper.CreatePartName(item.TargetUri);
							PackagePart part2 = pkg.GetPart(partName);
							part_tgt.AddRelationship(part2.PartName, item.TargetMode.Value, item.RelationshipType, item.Id);
							if (!tgt.ContainPart(part2.PartName))
							{
								PackagePart packagePart = tgt.CreatePart(part2.PartName, part2.ContentType);
								Stream outputStream = packagePart.GetOutputStream();
								IOUtils.Copy(part2.GetInputStream(), outputStream);
								outputStream.Close();
								Copy(pkg, part2, tgt, packagePart);
							}
						}
					}
				}
			}
		}

		/// Copy core namespace properties
		///
		/// @param src source properties
		/// @param tgt target properties
		private static void CopyProperties(PackageProperties src, PackageProperties tgt)
		{
			tgt.SetCategoryProperty(src.GetCategoryProperty());
			tgt.SetContentStatusProperty(src.GetContentStatusProperty());
			tgt.SetContentTypeProperty(src.GetContentTypeProperty());
			tgt.SetCreatorProperty(src.GetCreatorProperty());
			tgt.SetDescriptionProperty(src.GetDescriptionProperty());
			tgt.SetIdentifierProperty(src.GetIdentifierProperty());
			tgt.SetKeywordsProperty(src.GetKeywordsProperty());
			tgt.SetLanguageProperty(src.GetLanguageProperty());
			tgt.SetRevisionProperty(src.GetRevisionProperty());
			tgt.SetSubjectProperty(src.GetSubjectProperty());
			tgt.SetTitleProperty(src.GetTitleProperty());
			tgt.SetVersionProperty(src.GetVersionProperty());
		}
	}
}
