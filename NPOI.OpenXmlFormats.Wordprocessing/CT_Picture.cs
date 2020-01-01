using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Picture;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Picture : CT_PictureBase
	{
		private CT_Rel movieField;

		private CT_Control controlField;

		[XmlElement(Order = 0)]
		public CT_Rel movie
		{
			get
			{
				return movieField;
			}
			set
			{
				movieField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Control control
		{
			get
			{
				return controlField;
			}
			set
			{
				controlField = value;
			}
		}

		public CT_PictureNonVisual AddNewNvPicPr()
		{
			throw new NotImplementedException();
		}

		public CT_BlipFillProperties AddNewBlipFill()
		{
			throw new NotImplementedException();
		}

		public CT_ShapeProperties AddNewSpPr()
		{
			throw new NotImplementedException();
		}

		public static CT_Picture Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Picture cT_Picture = new CT_Picture();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "movie")
				{
					cT_Picture.movie = CT_Rel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "control")
				{
					cT_Picture.control = CT_Control.Parse(childNode, namespaceManager);
				}
				else if (childNode.Prefix == "o")
				{
					cT_Picture.ItemsElementName.Add(ItemsChoiceType9.office);
					cT_Picture.Items.Add(childNode);
				}
				else if (childNode.Prefix == "v")
				{
					cT_Picture.ItemsElementName.Add(ItemsChoiceType9.vml);
					cT_Picture.Items.Add(childNode);
				}
			}
			return cT_Picture;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (movie != null)
			{
				movie.Write(sw, "movie");
			}
			if (control != null)
			{
				control.Write(sw, "control");
			}
			foreach (XmlNode item in base.Items)
			{
				sw.Write(item.OuterXml);
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
