using NPOI.OpenXml4Net.OPC;
using NPOI.Util;
using System;
using System.Reflection;

namespace NPOI.XSSF.UserModel
{
	/// Instantiates sub-classes of POIXMLDocumentPart depending on their relationship type
	///
	/// @author Yegor Kozlov
	public class XSSFFactory : POIXMLFactory
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(XSSFFactory));

		private static XSSFFactory inst = new XSSFFactory();

		private XSSFFactory()
		{
		}

		public static XSSFFactory GetInstance()
		{
			return inst;
		}

		public override POIXMLDocumentPart CreateDocumentPart(POIXMLDocumentPart parent, PackageRelationship rel, PackagePart part)
		{
			POIXMLRelation instance = XSSFRelation.GetInstance(rel.RelationshipType);
			if (instance != null && !(instance.RelationClass == null))
			{
				try
				{
					Type relationClass = instance.RelationClass;
					ConstructorInfo constructor = relationClass.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[2]
					{
						typeof(PackagePart),
						typeof(PackageRelationship)
					}, null);
					return (POIXMLDocumentPart)constructor.Invoke(new object[2]
					{
						part,
						rel
					});
				}
				catch (Exception ex)
				{
					throw new POIXMLException(ex);
				}
			}
			logger.Log(1, "using default POIXMLDocumentPart for " + rel.RelationshipType);
			return new POIXMLDocumentPart(part, rel);
		}

		public override POIXMLDocumentPart CreateDocumentPart(POIXMLRelation descriptor)
		{
			try
			{
				Type relationClass = descriptor.RelationClass;
				ConstructorInfo constructor = relationClass.GetConstructor(new Type[0]);
				return (POIXMLDocumentPart)constructor.Invoke(new object[0]);
			}
			catch (Exception ex)
			{
				throw new POIXMLException(ex);
			}
		}
	}
}
