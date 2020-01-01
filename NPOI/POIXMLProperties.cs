using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.OpenXmlFormats;
using System.IO;

namespace NPOI
{
	/// Wrapper around the two different kinds of OOXML properties
	///  a document can have
	public class POIXMLProperties
	{
		private OPCPackage pkg;

		private CoreProperties core;

		private ExtendedProperties ext;

		private CustomProperties cust;

		private PackagePart extPart;

		private PackagePart custPart;

		private static ExtendedPropertiesDocument NEW_EXT_INSTANCE;

		private static CustomPropertiesDocument NEW_CUST_INSTANCE;

		/// Returns the core document properties
		public CoreProperties CoreProperties
		{
			get
			{
				return core;
			}
		}

		/// Returns the extended document properties
		public ExtendedProperties ExtendedProperties
		{
			get
			{
				return ext;
			}
		}

		/// Returns the custom document properties
		public CustomProperties CustomProperties
		{
			get
			{
				return cust;
			}
		}

		static POIXMLProperties()
		{
			NEW_EXT_INSTANCE = new ExtendedPropertiesDocument();
			NEW_EXT_INSTANCE.AddNewProperties();
			NEW_CUST_INSTANCE = new CustomPropertiesDocument();
			NEW_CUST_INSTANCE.AddNewProperties();
		}

		public POIXMLProperties(OPCPackage docPackage)
		{
			pkg = docPackage;
			core = new CoreProperties((PackagePropertiesPart)pkg.GetPackageProperties());
			PackageRelationshipCollection relationshipsByType = pkg.GetRelationshipsByType("http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties");
			if (relationshipsByType.Size == 1)
			{
				extPart = pkg.GetPart(relationshipsByType.GetRelationship(0));
				ExtendedPropertiesDocument props = ExtendedPropertiesDocument.Parse(extPart.GetInputStream());
				ext = new ExtendedProperties(props);
			}
			else
			{
				extPart = null;
				ext = new ExtendedProperties(NEW_EXT_INSTANCE.Copy());
			}
			PackageRelationshipCollection relationshipsByType2 = pkg.GetRelationshipsByType("http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties");
			if (relationshipsByType2.Size == 1)
			{
				custPart = pkg.GetPart(relationshipsByType2.GetRelationship(0));
				CustomPropertiesDocument props2 = CustomPropertiesDocument.Parse(custPart.GetInputStream());
				cust = new CustomProperties(props2);
			}
			else
			{
				custPart = null;
				cust = new CustomProperties(NEW_CUST_INSTANCE.Copy());
			}
		}

		/// Commit Changes to the underlying OPC namespace
		public virtual void Commit()
		{
			if (extPart == null && !NEW_EXT_INSTANCE.ToString().Equals(ext.props.ToString()))
			{
				try
				{
					PackagePartName packagePartName = PackagingUriHelper.CreatePartName("/docProps/app.xml");
					pkg.AddRelationship(packagePartName, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties");
					extPart = pkg.CreatePart(packagePartName, "application/vnd.openxmlformats-officedocument.extended-properties+xml");
				}
				catch (InvalidFormatException ex)
				{
					throw new POIXMLException(ex);
				}
			}
			if (custPart == null && !NEW_CUST_INSTANCE.ToString().Equals(cust.props.ToString()))
			{
				try
				{
					PackagePartName packagePartName2 = PackagingUriHelper.CreatePartName("/docProps/custom.xml");
					pkg.AddRelationship(packagePartName2, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties");
					custPart = pkg.CreatePart(packagePartName2, "application/vnd.openxmlformats-officedocument.custom-properties+xml");
				}
				catch (InvalidFormatException ex2)
				{
					throw new POIXMLException(ex2);
				}
			}
			if (extPart != null)
			{
				Stream outputStream = extPart.GetOutputStream();
				ext.props.Save(outputStream);
				outputStream.Close();
			}
			if (custPart != null)
			{
				Stream outputStream2 = custPart.GetOutputStream();
				cust.props.Save(outputStream2);
				outputStream2.Close();
			}
		}
	}
}
