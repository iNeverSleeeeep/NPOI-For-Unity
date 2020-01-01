using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_StyleMatrixReference
	{
		private CT_ScRgbColor scrgbClrField;

		private CT_SRgbColor srgbClrField;

		private CT_HslColor hslClrField;

		private CT_SystemColor sysClrField;

		private CT_SchemeColor schemeClrField;

		private CT_PresetColor prstClrField;

		private uint idxField;

		[XmlElement(Order = 0)]
		public CT_ScRgbColor scrgbClr
		{
			get
			{
				return scrgbClrField;
			}
			set
			{
				scrgbClrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_SRgbColor srgbClr
		{
			get
			{
				return srgbClrField;
			}
			set
			{
				srgbClrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_HslColor hslClr
		{
			get
			{
				return hslClrField;
			}
			set
			{
				hslClrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_SystemColor sysClr
		{
			get
			{
				return sysClrField;
			}
			set
			{
				sysClrField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_SchemeColor schemeClr
		{
			get
			{
				return schemeClrField;
			}
			set
			{
				schemeClrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_PresetColor prstClr
		{
			get
			{
				return prstClrField;
			}
			set
			{
				prstClrField = value;
			}
		}

		[XmlAttribute]
		public uint idx
		{
			get
			{
				return idxField;
			}
			set
			{
				idxField = value;
			}
		}

		public static CT_StyleMatrixReference Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_StyleMatrixReference cT_StyleMatrixReference = new CT_StyleMatrixReference();
			cT_StyleMatrixReference.idx = XmlHelper.ReadUInt(node.Attributes["idx"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "scrgbClr")
				{
					cT_StyleMatrixReference.scrgbClr = CT_ScRgbColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "srgbClr")
				{
					cT_StyleMatrixReference.srgbClr = CT_SRgbColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hslClr")
				{
					cT_StyleMatrixReference.hslClr = CT_HslColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sysClr")
				{
					cT_StyleMatrixReference.sysClr = CT_SystemColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "schemeClr")
				{
					cT_StyleMatrixReference.schemeClr = CT_SchemeColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "prstClr")
				{
					cT_StyleMatrixReference.prstClr = CT_PresetColor.Parse(childNode, namespaceManager);
				}
			}
			return cT_StyleMatrixReference;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "idx", (double)idx, true);
			sw.Write(">");
			if (scrgbClr != null)
			{
				scrgbClr.Write(sw, "scrgbClr");
			}
			if (srgbClr != null)
			{
				srgbClr.Write(sw, "srgbClr");
			}
			if (hslClr != null)
			{
				hslClr.Write(sw, "hslClr");
			}
			if (sysClr != null)
			{
				sysClr.Write(sw, "sysClr");
			}
			if (schemeClr != null)
			{
				schemeClr.Write(sw, "schemeClr");
			}
			if (prstClr != null)
			{
				prstClr.Write(sw, "prstClr");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_SchemeColor AddNewSchemeClr()
		{
			schemeClrField = new CT_SchemeColor();
			return schemeClrField;
		}
	}
}
