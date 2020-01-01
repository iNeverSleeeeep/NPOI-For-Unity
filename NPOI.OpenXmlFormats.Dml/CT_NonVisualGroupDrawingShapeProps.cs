using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_NonVisualGroupDrawingShapeProps
	{
		private CT_GroupLocking grpSpLocksField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_GroupLocking grpSpLocks
		{
			get
			{
				return grpSpLocksField;
			}
			set
			{
				grpSpLocksField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_NonVisualGroupDrawingShapeProps Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NonVisualGroupDrawingShapeProps cT_NonVisualGroupDrawingShapeProps = new CT_NonVisualGroupDrawingShapeProps();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "grpSpLocks")
				{
					cT_NonVisualGroupDrawingShapeProps.grpSpLocks = CT_GroupLocking.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_NonVisualGroupDrawingShapeProps.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_NonVisualGroupDrawingShapeProps;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (grpSpLocks != null)
			{
				grpSpLocks.Write(sw, "grpSpLocks");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
