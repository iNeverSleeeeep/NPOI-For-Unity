using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Backdrop
	{
		private CT_Point3D anchorField;

		private CT_Vector3D normField;

		private CT_Vector3D upField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_Point3D anchor
		{
			get
			{
				return anchorField;
			}
			set
			{
				anchorField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Vector3D norm
		{
			get
			{
				return normField;
			}
			set
			{
				normField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Vector3D up
		{
			get
			{
				return upField;
			}
			set
			{
				upField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_Backdrop Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Backdrop cT_Backdrop = new CT_Backdrop();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "anchor")
				{
					cT_Backdrop.anchor = CT_Point3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "norm")
				{
					cT_Backdrop.norm = CT_Vector3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "up")
				{
					cT_Backdrop.up = CT_Vector3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Backdrop.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Backdrop;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (anchor != null)
			{
				anchor.Write(sw, "anchor");
			}
			if (norm != null)
			{
				norm.Write(sw, "norm");
			}
			if (up != null)
			{
				up.Write(sw, "up");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
