using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_HslColor
	{
		private List<string> itemsValueField;

		private List<EG_ColorTransform> itemsElementNameField;

		private int hueField;

		private int satField;

		private int lumField;

		[XmlElement("ItemsElementName", Order = 1)]
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
		public int hue
		{
			get
			{
				return hueField;
			}
			set
			{
				hueField = value;
			}
		}

		[XmlAttribute]
		public int sat
		{
			get
			{
				return satField;
			}
			set
			{
				satField = value;
			}
		}

		[XmlAttribute]
		public int lum
		{
			get
			{
				return lumField;
			}
			set
			{
				lumField = value;
			}
		}

		public CT_HslColor()
		{
			itemsElementNameField = new List<EG_ColorTransform>();
			itemsValueField = new List<string>();
		}

		public static CT_HslColor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_HslColor cT_HslColor = new CT_HslColor();
			cT_HslColor.hue = XmlHelper.ReadInt(node.Attributes["hue"]);
			cT_HslColor.sat = XmlHelper.ReadInt(node.Attributes["sat"]);
			cT_HslColor.lum = XmlHelper.ReadInt(node.Attributes["lum"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "alpha")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.alpha);
				}
				else if (childNode.LocalName == "alphaMod")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.alphaMod);
				}
				else if (childNode.LocalName == "alphaOff")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.alphaOff);
				}
				else if (childNode.LocalName == "blue")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.blue);
				}
				else if (childNode.LocalName == "blueMod")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.blueMod);
				}
				else if (childNode.LocalName == "blueOff")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.blueOff);
				}
				else if (childNode.LocalName == "comp")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.comp);
				}
				else if (childNode.LocalName == "gamma")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.gamma);
				}
				else if (childNode.LocalName == "gray")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.gray);
				}
				else if (childNode.LocalName == "green")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.green);
				}
				else if (childNode.LocalName == "greenMod")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.greenMod);
				}
				else if (childNode.LocalName == "greenOff")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.greenOff);
				}
				else if (childNode.LocalName == "hue")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.hue);
				}
				else if (childNode.LocalName == "hueMod")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.hueMod);
				}
				else if (childNode.LocalName == "hueOff")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.hueOff);
				}
				else if (childNode.LocalName == "inv")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.inv);
				}
				else if (childNode.LocalName == "invGamma")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.invGamma);
				}
				else if (childNode.LocalName == "lum")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.lum);
				}
				else if (childNode.LocalName == "lumMod")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.lumMod);
				}
				else if (childNode.LocalName == "lumOff")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.lumOff);
				}
				else if (childNode.LocalName == "red")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.red);
				}
				else if (childNode.LocalName == "redMod")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.redMod);
				}
				else if (childNode.LocalName == "redOff")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.redOff);
				}
				else if (childNode.LocalName == "sat")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.sat);
				}
				else if (childNode.LocalName == "satMod")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.satMod);
				}
				else if (childNode.LocalName == "satOff")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.satOff);
				}
				else if (childNode.LocalName == "shade")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.shade);
				}
				else if (childNode.LocalName == "tint")
				{
					cT_HslColor.ItemsElementName.Add(EG_ColorTransform.tint);
				}
				if (childNode.Attributes["val"] != null)
				{
					cT_HslColor.itemsValueField.Add(childNode.Attributes["val"].Value);
				}
				else
				{
					cT_HslColor.itemsValueField.Add(null);
				}
			}
			return cT_HslColor;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "hue", hue);
			XmlHelper.WriteAttribute(sw, "sat", sat);
			XmlHelper.WriteAttribute(sw, "lum", lum);
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
