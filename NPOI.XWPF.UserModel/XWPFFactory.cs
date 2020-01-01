using NPOI.OpenXml4Net.OPC;
using NPOI.Util;
using System;
using System.Reflection;

namespace NPOI.XWPF.UserModel
{
	/// @author Yegor Kozlov
	public class XWPFFactory : POIXMLFactory
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(XWPFFactory));

		private static XWPFFactory inst = new XWPFFactory();

		private XWPFFactory()
		{
		}

		public static XWPFFactory GetInstance()
		{
			return inst;
		}

		public override POIXMLDocumentPart CreateDocumentPart(POIXMLDocumentPart parent, PackageRelationship rel, PackagePart part)
		{
			POIXMLRelation instance = XWPFRelation.GetInstance(rel.RelationshipType);
			if (instance != null && !(instance.RelationClass == null))
			{
				try
				{
					Type relationClass = instance.RelationClass;
					try
					{
						ConstructorInfo constructor = relationClass.GetConstructor(new Type[3]
						{
							typeof(POIXMLDocumentPart),
							typeof(PackagePart),
							typeof(PackageRelationship)
						});
						return constructor.Invoke(new object[3]
						{
							parent,
							part,
							rel
						}) as POIXMLDocumentPart;
					}
					catch (Exception)
					{
						ConstructorInfo constructor2 = relationClass.GetConstructor(new Type[2]
						{
							typeof(PackagePart),
							typeof(PackageRelationship)
						});
						return constructor2.Invoke(new object[2]
						{
							part,
							rel
						}) as POIXMLDocumentPart;
					}
				}
				catch (Exception ex2)
				{
					throw new POIXMLException(ex2);
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
				ConstructorInfo constructor = relationClass.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
				return constructor.Invoke(new object[0]) as POIXMLDocumentPart;
			}
			catch (Exception ex)
			{
				throw new POIXMLException(ex);
			}
		}
	}
}
