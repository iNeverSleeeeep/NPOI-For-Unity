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
	public class CT_PresetColor
	{
		private List<string> itemsValueField;

		private List<EG_ColorTransform> itemsElementNameField;

		private ST_PresetColorVal valField;

		private bool valFieldSpecified;

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
		public ST_PresetColorVal val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlIgnore]
		public bool valSpecified
		{
			get
			{
				return valFieldSpecified;
			}
			set
			{
				valFieldSpecified = value;
			}
		}

		public CT_PresetColor()
		{
			itemsElementNameField = new List<EG_ColorTransform>();
			itemsValueField = new List<string>();
		}

		public static CT_PresetColor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PresetColor cT_PresetColor = new CT_PresetColor();
			if (node.Attributes["val"] != null)
			{
				cT_PresetColor.val = (ST_PresetColorVal)Enum.Parse(typeof(ST_PresetColorVal), node.Attributes["val"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "alpha")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.alpha);
				}
				else if (childNode.LocalName == "alphaMod")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.alphaMod);
				}
				else if (childNode.LocalName == "alphaOff")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.alphaOff);
				}
				else if (childNode.LocalName == "blue")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.blue);
				}
				else if (childNode.LocalName == "blueMod")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.blueMod);
				}
				else if (childNode.LocalName == "blueOff")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.blueOff);
				}
				else if (childNode.LocalName == "comp")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.comp);
				}
				else if (childNode.LocalName == "gamma")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.gamma);
				}
				else if (childNode.LocalName == "gray")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.gray);
				}
				else if (childNode.LocalName == "green")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.green);
				}
				else if (childNode.LocalName == "greenMod")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.greenMod);
				}
				else if (childNode.LocalName == "greenOff")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.greenOff);
				}
				else if (childNode.LocalName == "hue")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.hue);
				}
				else if (childNode.LocalName == "hueMod")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.hueMod);
				}
				else if (childNode.LocalName == "hueOff")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.hueOff);
				}
				else if (childNode.LocalName == "inv")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.inv);
				}
				else if (childNode.LocalName == "invGamma")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.invGamma);
				}
				else if (childNode.LocalName == "lum")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.lum);
				}
				else if (childNode.LocalName == "lumMod")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.lumMod);
				}
				else if (childNode.LocalName == "lumOff")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.lumOff);
				}
				else if (childNode.LocalName == "red")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.red);
				}
				else if (childNode.LocalName == "redMod")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.redMod);
				}
				else if (childNode.LocalName == "redOff")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.redOff);
				}
				else if (childNode.LocalName == "sat")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.sat);
				}
				else if (childNode.LocalName == "satMod")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.satMod);
				}
				else if (childNode.LocalName == "satOff")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.satOff);
				}
				else if (childNode.LocalName == "shade")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.shade);
				}
				else if (childNode.LocalName == "tint")
				{
					cT_PresetColor.ItemsElementName.Add(EG_ColorTransform.tint);
				}
				if (childNode.Attributes["val"] != null)
				{
					cT_PresetColor.itemsValueField.Add(childNode.Attributes["val"].Value);
				}
				else
				{
					cT_PresetColor.itemsValueField.Add(null);
				}
			}
			return cT_PresetColor;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
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
