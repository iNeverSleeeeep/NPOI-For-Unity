using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[DesignerCategory("code")]
	public class CT_Fill
	{
		private string idField;

		private ST_FillType typeField;

		private bool typeFieldSpecified;

		private ST_TrueFalse onField;

		private bool onFieldSpecified;

		private string colorField;

		private string opacityField;

		private string color2Field;

		private string srcField;

		private string sizeField;

		private string originField;

		private string positionField;

		private ST_ImageAspect aspectField;

		private bool aspectFieldSpecified;

		private string colorsField;

		private decimal angleField;

		private bool angleFieldSpecified;

		private ST_TrueFalse alignshapeField;

		private bool alignshapeFieldSpecified;

		private string focusField;

		private string focussizeField;

		private string focuspositionField;

		private ST_FillMethod methodField;

		private bool methodFieldSpecified;

		private ST_TrueFalse recolorField;

		private bool recolorFieldSpecified;

		private ST_TrueFalse rotateField;

		private bool rotateFieldSpecified;

		private string id1Field;

		[XmlAttribute]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public ST_FillType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse on
		{
			get
			{
				return onField;
			}
			set
			{
				onField = value;
			}
		}

		[XmlIgnore]
		public bool onSpecified
		{
			get
			{
				return onFieldSpecified;
			}
			set
			{
				onFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string color
		{
			get
			{
				return colorField;
			}
			set
			{
				colorField = value;
			}
		}

		[XmlAttribute]
		public string opacity
		{
			get
			{
				return opacityField;
			}
			set
			{
				opacityField = value;
			}
		}

		[XmlAttribute]
		public string color2
		{
			get
			{
				return color2Field;
			}
			set
			{
				color2Field = value;
			}
		}

		[XmlAttribute]
		public string src
		{
			get
			{
				return srcField;
			}
			set
			{
				srcField = value;
			}
		}

		[XmlAttribute]
		public string size
		{
			get
			{
				return sizeField;
			}
			set
			{
				sizeField = value;
			}
		}

		[XmlAttribute]
		public string origin
		{
			get
			{
				return originField;
			}
			set
			{
				originField = value;
			}
		}

		[XmlAttribute]
		public string position
		{
			get
			{
				return positionField;
			}
			set
			{
				positionField = value;
			}
		}

		[XmlAttribute]
		public ST_ImageAspect aspect
		{
			get
			{
				return aspectField;
			}
			set
			{
				aspectField = value;
			}
		}

		[XmlIgnore]
		public bool aspectSpecified
		{
			get
			{
				return aspectFieldSpecified;
			}
			set
			{
				aspectFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string colors
		{
			get
			{
				return colorsField;
			}
			set
			{
				colorsField = value;
			}
		}

		[XmlAttribute]
		public decimal angle
		{
			get
			{
				return angleField;
			}
			set
			{
				angleField = value;
			}
		}

		[XmlIgnore]
		public bool angleSpecified
		{
			get
			{
				return angleFieldSpecified;
			}
			set
			{
				angleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse alignshape
		{
			get
			{
				return alignshapeField;
			}
			set
			{
				alignshapeField = value;
			}
		}

		[XmlIgnore]
		public bool alignshapeSpecified
		{
			get
			{
				return alignshapeFieldSpecified;
			}
			set
			{
				alignshapeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string focus
		{
			get
			{
				return focusField;
			}
			set
			{
				focusField = value;
			}
		}

		[XmlAttribute]
		public string focussize
		{
			get
			{
				return focussizeField;
			}
			set
			{
				focussizeField = value;
			}
		}

		[XmlAttribute]
		public string focusposition
		{
			get
			{
				return focuspositionField;
			}
			set
			{
				focuspositionField = value;
			}
		}

		[XmlAttribute]
		public ST_FillMethod method
		{
			get
			{
				return methodField;
			}
			set
			{
				methodField = value;
			}
		}

		[XmlIgnore]
		public bool methodSpecified
		{
			get
			{
				return methodFieldSpecified;
			}
			set
			{
				methodFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse recolor
		{
			get
			{
				return recolorField;
			}
			set
			{
				recolorField = value;
			}
		}

		[XmlIgnore]
		public bool recolorSpecified
		{
			get
			{
				return recolorFieldSpecified;
			}
			set
			{
				recolorFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse rotate
		{
			get
			{
				return rotateField;
			}
			set
			{
				rotateField = value;
			}
		}

		[XmlIgnore]
		public bool rotateSpecified
		{
			get
			{
				return rotateFieldSpecified;
			}
			set
			{
				rotateFieldSpecified = value;
			}
		}

		public static CT_Fill Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Fill cT_Fill = new CT_Fill();
			cT_Fill.id = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["id"]);
			if (node.Attributes["type"] != null)
			{
				cT_Fill.type = (ST_FillType)Enum.Parse(typeof(ST_FillType), node.Attributes["type"].Value);
			}
			if (node.Attributes["on"] != null)
			{
				cT_Fill.on = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["on"]);
			}
			cT_Fill.color = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["color"]);
			cT_Fill.opacity = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["opacity"]);
			cT_Fill.color2 = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["color2"]);
			cT_Fill.src = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["src"]);
			cT_Fill.size = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["size"]);
			cT_Fill.origin = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["origin"]);
			cT_Fill.position = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["position"]);
			if (node.Attributes["aspect"] != null)
			{
				cT_Fill.aspect = (ST_ImageAspect)Enum.Parse(typeof(ST_ImageAspect), node.Attributes["aspect"].Value);
			}
			cT_Fill.colors = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["colors"]);
			if (node.Attributes["angle"] != null)
			{
				cT_Fill.angle = NPOI.OpenXml4Net.Util.XmlHelper.ReadDecimal(node.Attributes["angle"]);
			}
			if (node.Attributes["alignshape"] != null)
			{
				cT_Fill.alignshape = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["alignshape"]);
			}
			cT_Fill.focus = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["focus"]);
			cT_Fill.focussize = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["focussize"]);
			cT_Fill.focusposition = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["focusposition"]);
			if (node.Attributes["method"] != null)
			{
				cT_Fill.method = (ST_FillMethod)Enum.Parse(typeof(ST_FillMethod), node.Attributes["method"].Value);
			}
			if (node.Attributes["recolor"] != null)
			{
				cT_Fill.recolor = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["recolor"]);
			}
			if (node.Attributes["rotate"] != null)
			{
				cT_Fill.rotate = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["rotate"]);
			}
			return cT_Fill;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "id", id);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "type", type.ToString());
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "on", on);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "color", color);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "opacity", opacity);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "color2", color2);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "src", src);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "size", size);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "origin", origin);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "position", position);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "aspect", aspect.ToString());
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "colors", colors);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "angle", (double)angle);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "alignshape", alignshape);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "focus", focus);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "focussize", focussize);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "focusposition", focusposition);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "method", method.ToString());
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "recolor", recolor);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "rotate", rotate);
			sw.Write(">");
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
