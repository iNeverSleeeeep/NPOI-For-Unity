using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_ColorScheme
	{
		private CT_Color dk1Field;

		private CT_Color lt1Field;

		private CT_Color dk2Field;

		private CT_Color lt2Field;

		private CT_Color accent1Field;

		private CT_Color accent2Field;

		private CT_Color accent3Field;

		private CT_Color accent4Field;

		private CT_Color accent5Field;

		private CT_Color accent6Field;

		private CT_Color hlinkField;

		private CT_Color folHlinkField;

		private CT_OfficeArtExtensionList extLstField;

		private string nameField;

		[XmlElement(Order = 0)]
		public CT_Color dk1
		{
			get
			{
				return dk1Field;
			}
			set
			{
				dk1Field = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Color lt1
		{
			get
			{
				return lt1Field;
			}
			set
			{
				lt1Field = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Color dk2
		{
			get
			{
				return dk2Field;
			}
			set
			{
				dk2Field = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Color lt2
		{
			get
			{
				return lt2Field;
			}
			set
			{
				lt2Field = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Color accent1
		{
			get
			{
				return accent1Field;
			}
			set
			{
				accent1Field = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Color accent2
		{
			get
			{
				return accent2Field;
			}
			set
			{
				accent2Field = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Color accent3
		{
			get
			{
				return accent3Field;
			}
			set
			{
				accent3Field = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_Color accent4
		{
			get
			{
				return accent4Field;
			}
			set
			{
				accent4Field = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_Color accent5
		{
			get
			{
				return accent5Field;
			}
			set
			{
				accent5Field = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Color accent6
		{
			get
			{
				return accent6Field;
			}
			set
			{
				accent6Field = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_Color hlink
		{
			get
			{
				return hlinkField;
			}
			set
			{
				hlinkField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_Color folHlink
		{
			get
			{
				return folHlinkField;
			}
			set
			{
				folHlinkField = value;
			}
		}

		[XmlElement(Order = 12)]
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
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public static CT_ColorScheme Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ColorScheme cT_ColorScheme = new CT_ColorScheme();
			cT_ColorScheme.name = XmlHelper.ReadString(node.Attributes["name"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dk1")
				{
					cT_ColorScheme.dk1 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lt1")
				{
					cT_ColorScheme.lt1 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dk2")
				{
					cT_ColorScheme.dk2 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lt2")
				{
					cT_ColorScheme.lt2 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "accent1")
				{
					cT_ColorScheme.accent1 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "accent2")
				{
					cT_ColorScheme.accent2 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "accent3")
				{
					cT_ColorScheme.accent3 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "accent4")
				{
					cT_ColorScheme.accent4 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "accent5")
				{
					cT_ColorScheme.accent5 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "accent6")
				{
					cT_ColorScheme.accent6 = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hlink")
				{
					cT_ColorScheme.hlink = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "folHlink")
				{
					cT_ColorScheme.folHlink = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ColorScheme.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_ColorScheme;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			sw.Write(">");
			if (dk1 != null)
			{
				dk1.Write(sw, "dk1");
			}
			if (lt1 != null)
			{
				lt1.Write(sw, "lt1");
			}
			if (dk2 != null)
			{
				dk2.Write(sw, "dk2");
			}
			if (lt2 != null)
			{
				lt2.Write(sw, "lt2");
			}
			if (accent1 != null)
			{
				accent1.Write(sw, "accent1");
			}
			if (accent2 != null)
			{
				accent2.Write(sw, "accent2");
			}
			if (accent3 != null)
			{
				accent3.Write(sw, "accent3");
			}
			if (accent4 != null)
			{
				accent4.Write(sw, "accent4");
			}
			if (accent5 != null)
			{
				accent5.Write(sw, "accent5");
			}
			if (accent6 != null)
			{
				accent6.Write(sw, "accent6");
			}
			if (hlink != null)
			{
				hlink.Write(sw, "hlink");
			}
			if (folHlink != null)
			{
				folHlink.Write(sw, "folHlink");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
