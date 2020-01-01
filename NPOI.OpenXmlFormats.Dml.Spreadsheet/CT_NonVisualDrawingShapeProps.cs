using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_NonVisualDrawingShapeProps
	{
		private CT_ShapeLocking spLocksField;

		private CT_OfficeArtExtensionList extLstField;

		private bool txBoxField;

		[XmlElement(Order = 0)]
		public CT_ShapeLocking spLocks
		{
			get
			{
				return spLocksField;
			}
			set
			{
				spLocksField = value;
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

		[XmlAttribute]
		[DefaultValue(false)]
		public bool txBox
		{
			get
			{
				return txBoxField;
			}
			set
			{
				txBoxField = value;
			}
		}

		public static CT_NonVisualDrawingShapeProps Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NonVisualDrawingShapeProps cT_NonVisualDrawingShapeProps = new CT_NonVisualDrawingShapeProps();
			cT_NonVisualDrawingShapeProps.txBox = XmlHelper.ReadBool(node.Attributes["txBox"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "spLocks")
				{
					cT_NonVisualDrawingShapeProps.spLocks = CT_ShapeLocking.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_NonVisualDrawingShapeProps.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_NonVisualDrawingShapeProps;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "txBox", txBox, false);
			sw.Write(">");
			if (spLocks != null)
			{
				spLocks.Write(sw, "spLocks");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}
	}
}
