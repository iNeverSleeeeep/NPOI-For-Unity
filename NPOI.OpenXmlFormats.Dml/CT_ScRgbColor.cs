using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_ScRgbColor
	{
		private List<string> itemsValueField;

		private List<EG_ColorTransform> itemsElementNameField;

		private int rField;

		private int gField;

		private int bField;

		[XmlIgnore]
		public List<EG_ColorTransform> ItemsElementName
		{
			get
			{
				return itemsElementNameField;
			}
			set
			{
				itemsElementNameField = value;
			}
		}

		[XmlAttribute]
		public int r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlAttribute]
		public int g
		{
			get
			{
				return gField;
			}
			set
			{
				gField = value;
			}
		}

		[XmlAttribute]
		public int b
		{
			get
			{
				return bField;
			}
			set
			{
				bField = value;
			}
		}

		public CT_ScRgbColor()
		{
			itemsElementNameField = new List<EG_ColorTransform>();
			itemsValueField = new List<string>();
		}

		public static CT_ScRgbColor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ScRgbColor cT_ScRgbColor = new CT_ScRgbColor();
			cT_ScRgbColor.r = XmlHelper.ReadInt(node.Attributes["r"]);
			cT_ScRgbColor.g = XmlHelper.ReadInt(node.Attributes["g"]);
			cT_ScRgbColor.b = XmlHelper.ReadInt(node.Attributes["b"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "alpha")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.alpha);
				}
				else if (childNode.LocalName == "alphaMod")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.alphaMod);
				}
				else if (childNode.LocalName == "alphaOff")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.alphaOff);
				}
				else if (childNode.LocalName == "blue")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.blue);
				}
				else if (childNode.LocalName == "blueMod")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.blueMod);
				}
				else if (childNode.LocalName == "blueOff")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.blueOff);
				}
				else if (childNode.LocalName == "comp")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.comp);
				}
				else if (childNode.LocalName == "gamma")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.gamma);
				}
				else if (childNode.LocalName == "gray")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.gray);
				}
				else if (childNode.LocalName == "green")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.green);
				}
				else if (childNode.LocalName == "greenMod")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.greenMod);
				}
				else if (childNode.LocalName == "greenOff")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.greenOff);
				}
				else if (childNode.LocalName == "hue")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.hue);
				}
				else if (childNode.LocalName == "hueMod")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.hueMod);
				}
				else if (childNode.LocalName == "hueOff")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.hueOff);
				}
				else if (childNode.LocalName == "inv")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.inv);
				}
				else if (childNode.LocalName == "invGamma")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.invGamma);
				}
				else if (childNode.LocalName == "lum")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.lum);
				}
				else if (childNode.LocalName == "lumMod")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.lumMod);
				}
				else if (childNode.LocalName == "lumOff")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.lumOff);
				}
				else if (childNode.LocalName == "red")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.red);
				}
				else if (childNode.LocalName == "redMod")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.redMod);
				}
				else if (childNode.LocalName == "redOff")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.redOff);
				}
				else if (childNode.LocalName == "sat")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.sat);
				}
				else if (childNode.LocalName == "satMod")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.satMod);
				}
				else if (childNode.LocalName == "satOff")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.satOff);
				}
				else if (childNode.LocalName == "shade")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.shade);
				}
				else if (childNode.LocalName == "tint")
				{
					cT_ScRgbColor.ItemsElementName.Add(EG_ColorTransform.tint);
				}
				if (childNode.Attributes["val"] != null)
				{
					cT_ScRgbColor.itemsValueField.Add(childNode.Attributes["val"].Value);
				}
				else
				{
					cT_ScRgbColor.itemsValueField.Add(null);
				}
			}
			return cT_ScRgbColor;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r", r, true);
			XmlHelper.WriteAttribute(sw, "g", g, true);
			XmlHelper.WriteAttribute(sw, "b", b, true);
			sw.Write(">");
			if (ItemsElementName != null)
			{
				for (int i = 0; i < itemsElementNameField.Count; i++)
				{
					EG_ColorTransform eG_ColorTransform = itemsElementNameField[i];
					string text = itemsValueField[i];
					sw.Write(string.Format("<a:{0}", eG_ColorTransform));
					if (text != null)
					{
						sw.Write(" val=\"" + text + "\"");
					}
					sw.Write("/>");
				}
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
