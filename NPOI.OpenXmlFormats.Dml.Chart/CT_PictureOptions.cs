using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_PictureOptions
	{
		private CT_Boolean applyToFrontField;

		private CT_Boolean applyToSidesField;

		private CT_Boolean applyToEndField;

		private CT_PictureFormat pictureFormatField;

		private CT_PictureStackUnit pictureStackUnitField;

		[XmlElement(Order = 0)]
		public CT_Boolean applyToFront
		{
			get
			{
				return applyToFrontField;
			}
			set
			{
				applyToFrontField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Boolean applyToSides
		{
			get
			{
				return applyToSidesField;
			}
			set
			{
				applyToSidesField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Boolean applyToEnd
		{
			get
			{
				return applyToEndField;
			}
			set
			{
				applyToEndField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_PictureFormat pictureFormat
		{
			get
			{
				return pictureFormatField;
			}
			set
			{
				pictureFormatField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_PictureStackUnit pictureStackUnit
		{
			get
			{
				return pictureStackUnitField;
			}
			set
			{
				pictureStackUnitField = value;
			}
		}

		public static CT_PictureOptions Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PictureOptions cT_PictureOptions = new CT_PictureOptions();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "applyToFront")
				{
					cT_PictureOptions.applyToFront = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "applyToSides")
				{
					cT_PictureOptions.applyToSides = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "applyToEnd")
				{
					cT_PictureOptions.applyToEnd = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pictureFormat")
				{
					cT_PictureOptions.pictureFormat = CT_PictureFormat.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pictureStackUnit")
				{
					cT_PictureOptions.pictureStackUnit = CT_PictureStackUnit.Parse(childNode, namespaceManager);
				}
			}
			return cT_PictureOptions;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (applyToFront != null)
			{
				applyToFront.Write(sw, "applyToFront");
			}
			if (applyToSides != null)
			{
				applyToSides.Write(sw, "applyToSides");
			}
			if (applyToEnd != null)
			{
				applyToEnd.Write(sw, "applyToEnd");
			}
			if (pictureFormat != null)
			{
				pictureFormat.Write(sw, "pictureFormat");
			}
			if (pictureStackUnit != null)
			{
				pictureStackUnit.Write(sw, "pictureStackUnit");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
