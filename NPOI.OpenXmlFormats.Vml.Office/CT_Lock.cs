using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Lock
	{
		private ST_Ext extField;

		private ST_TrueFalse positionField;

		private bool positionFieldSpecified;

		private ST_TrueFalse selectionField;

		private bool selectionFieldSpecified;

		private ST_TrueFalse groupingField;

		private bool groupingFieldSpecified;

		private ST_TrueFalse ungroupingField;

		private bool ungroupingFieldSpecified;

		private ST_TrueFalse rotationField;

		private bool rotationFieldSpecified;

		private ST_TrueFalse croppingField;

		private bool croppingFieldSpecified;

		private ST_TrueFalse verticiesField;

		private bool verticiesFieldSpecified;

		private ST_TrueFalse adjusthandlesField;

		private bool adjusthandlesFieldSpecified;

		private ST_TrueFalse textField;

		private bool textFieldSpecified;

		private ST_TrueFalse aspectratioField;

		private bool aspectratioFieldSpecified;

		private ST_TrueFalse shapetypeField;

		private bool shapetypeFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:vml")]
		public ST_Ext ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}

		[XmlIgnore]
		public bool extSpecified
		{
			get
			{
				return ST_Ext.NONE != extField;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse position
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

		[XmlIgnore]
		public bool positionSpecified
		{
			get
			{
				return positionFieldSpecified;
			}
			set
			{
				positionFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse selection
		{
			get
			{
				return selectionField;
			}
			set
			{
				selectionField = value;
			}
		}

		[XmlIgnore]
		public bool selectionSpecified
		{
			get
			{
				return selectionFieldSpecified;
			}
			set
			{
				selectionFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse grouping
		{
			get
			{
				return groupingField;
			}
			set
			{
				groupingField = value;
			}
		}

		[XmlIgnore]
		public bool groupingSpecified
		{
			get
			{
				return groupingFieldSpecified;
			}
			set
			{
				groupingFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse ungrouping
		{
			get
			{
				return ungroupingField;
			}
			set
			{
				ungroupingField = value;
			}
		}

		[XmlIgnore]
		public bool ungroupingSpecified
		{
			get
			{
				return ungroupingFieldSpecified;
			}
			set
			{
				ungroupingFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse rotation
		{
			get
			{
				return rotationField;
			}
			set
			{
				rotationField = value;
			}
		}

		[XmlIgnore]
		public bool rotationSpecified
		{
			get
			{
				return rotationFieldSpecified;
			}
			set
			{
				rotationFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse cropping
		{
			get
			{
				return croppingField;
			}
			set
			{
				croppingField = value;
			}
		}

		[XmlIgnore]
		public bool croppingSpecified
		{
			get
			{
				return croppingFieldSpecified;
			}
			set
			{
				croppingFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse verticies
		{
			get
			{
				return verticiesField;
			}
			set
			{
				verticiesField = value;
			}
		}

		[XmlIgnore]
		public bool verticiesSpecified
		{
			get
			{
				return verticiesFieldSpecified;
			}
			set
			{
				verticiesFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse adjusthandles
		{
			get
			{
				return adjusthandlesField;
			}
			set
			{
				adjusthandlesField = value;
			}
		}

		[XmlIgnore]
		public bool adjusthandlesSpecified
		{
			get
			{
				return adjusthandlesFieldSpecified;
			}
			set
			{
				adjusthandlesFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse text
		{
			get
			{
				return textField;
			}
			set
			{
				textField = value;
			}
		}

		[XmlIgnore]
		public bool textSpecified
		{
			get
			{
				return textFieldSpecified;
			}
			set
			{
				textFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse aspectratio
		{
			get
			{
				return aspectratioField;
			}
			set
			{
				aspectratioField = value;
			}
		}

		[XmlIgnore]
		public bool aspectratioSpecified
		{
			get
			{
				return aspectratioFieldSpecified;
			}
			set
			{
				aspectratioFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse shapetype
		{
			get
			{
				return shapetypeField;
			}
			set
			{
				shapetypeField = value;
			}
		}

		[XmlIgnore]
		public bool shapetypeSpecified
		{
			get
			{
				return shapetypeFieldSpecified;
			}
			set
			{
				shapetypeFieldSpecified = value;
			}
		}

		public static CT_Lock Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Lock cT_Lock = new CT_Lock();
			if (node.Attributes["v:ext"] != null)
			{
				cT_Lock.ext = (ST_Ext)Enum.Parse(typeof(ST_Ext), node.Attributes["v:ext"].Value);
			}
			if (node.Attributes["position"] != null)
			{
				cT_Lock.position = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["position"]);
			}
			if (node.Attributes["selection"] != null)
			{
				cT_Lock.selection = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["selection"]);
			}
			if (node.Attributes["grouping"] != null)
			{
				cT_Lock.grouping = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["grouping"]);
			}
			if (node.Attributes["ungrouping"] != null)
			{
				cT_Lock.ungrouping = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["ungrouping"]);
			}
			if (node.Attributes["rotation"] != null)
			{
				cT_Lock.rotation = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["rotation"]);
			}
			if (node.Attributes["cropping"] != null)
			{
				cT_Lock.cropping = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["cropping"]);
			}
			if (node.Attributes["verticies"] != null)
			{
				cT_Lock.verticies = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["verticies"]);
			}
			if (node.Attributes["adjusthandles"] != null)
			{
				cT_Lock.adjusthandles = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["adjusthandles"]);
			}
			if (node.Attributes["text"] != null)
			{
				cT_Lock.text = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["text"]);
			}
			if (node.Attributes["aspectratio"] != null)
			{
				cT_Lock.aspectratio = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["aspectratio"]);
			}
			if (node.Attributes["shapetype"] != null)
			{
				cT_Lock.shapetype = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse(node.Attributes["shapetype"]);
			}
			return cT_Lock;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<o:{0}", nodeName));
			if (ext != 0)
			{
				NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "v:ext", ext.ToString());
			}
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "position", position);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "selection", selection);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "grouping", grouping);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "ungrouping", ungrouping);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "rotation", rotation);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "cropping", cropping);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "verticies", verticies);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "adjusthandles", adjusthandles);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "text", text);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "aspectratio", aspectratio);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "shapetype", shapetype);
			sw.Write(">");
			sw.Write(string.Format("</o:{0}>", nodeName));
		}
	}
}
