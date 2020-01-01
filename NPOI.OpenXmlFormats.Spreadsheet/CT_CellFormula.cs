using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CellFormula
	{
		private ST_CellFormulaType tField;

		private bool acaField;

		private string refField;

		private bool dt2DField;

		private bool dtrField;

		private bool del1Field;

		private bool del2Field;

		private string r1Field;

		private string r2Field;

		private bool caField;

		private uint siField;

		private bool siFieldSpecified;

		private bool bxField;

		private string valueField;

		[XmlAttribute]
		[DefaultValue(ST_CellFormulaType.normal)]
		public ST_CellFormulaType t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool aca
		{
			get
			{
				return acaField;
			}
			set
			{
				acaField = value;
			}
		}

		[XmlAttribute]
		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool dt2D
		{
			get
			{
				return dt2DField;
			}
			set
			{
				dt2DField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool dtr
		{
			get
			{
				return dtrField;
			}
			set
			{
				dtrField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool del1
		{
			get
			{
				return del1Field;
			}
			set
			{
				del1Field = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool del2
		{
			get
			{
				return del2Field;
			}
			set
			{
				del2Field = value;
			}
		}

		[XmlAttribute]
		public string r1
		{
			get
			{
				return r1Field;
			}
			set
			{
				r1Field = value;
			}
		}

		[XmlAttribute]
		public string r2
		{
			get
			{
				return r2Field;
			}
			set
			{
				r2Field = value;
			}
		}

		[DefaultValue(false)]
		public bool ca
		{
			get
			{
				return caField;
			}
			set
			{
				caField = value;
			}
		}

		[XmlAttribute]
		public uint si
		{
			get
			{
				return siField;
			}
			set
			{
				siField = value;
			}
		}

		[XmlIgnore]
		public bool siSpecified
		{
			get
			{
				return siFieldSpecified;
			}
			set
			{
				siFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool bx
		{
			get
			{
				return bxField;
			}
			set
			{
				bxField = value;
			}
		}

		[XmlText]
		public string Value
		{
			get
			{
				return valueField;
			}
			set
			{
				valueField = value;
			}
		}

		public static CT_CellFormula Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellFormula cT_CellFormula = new CT_CellFormula();
			if (node.Attributes["t"] != null)
			{
				cT_CellFormula.t = (ST_CellFormulaType)Enum.Parse(typeof(ST_CellFormulaType), node.Attributes["t"].Value);
			}
			else
			{
				cT_CellFormula.t = ST_CellFormulaType.normal;
			}
			cT_CellFormula.aca = XmlHelper.ReadBool(node.Attributes["aca"]);
			cT_CellFormula.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			cT_CellFormula.dt2D = XmlHelper.ReadBool(node.Attributes["dt2D"]);
			cT_CellFormula.dtr = XmlHelper.ReadBool(node.Attributes["dtr"]);
			cT_CellFormula.del1 = XmlHelper.ReadBool(node.Attributes["del1"]);
			cT_CellFormula.del2 = XmlHelper.ReadBool(node.Attributes["del2"]);
			cT_CellFormula.r1 = XmlHelper.ReadString(node.Attributes["r1"]);
			cT_CellFormula.r2 = XmlHelper.ReadString(node.Attributes["r2"]);
			cT_CellFormula.ca = XmlHelper.ReadBool(node.Attributes["ca"]);
			cT_CellFormula.si = XmlHelper.ReadUInt(node.Attributes["si"]);
			cT_CellFormula.bx = XmlHelper.ReadBool(node.Attributes["bx"]);
			cT_CellFormula.Value = node.InnerText.Replace("\r", "");
			return cT_CellFormula;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (t != 0)
			{
				XmlHelper.WriteAttribute(sw, "t", t.ToString());
			}
			XmlHelper.WriteAttribute(sw, "aca", aca, false);
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			XmlHelper.WriteAttribute(sw, "dt2D", dt2D, false);
			XmlHelper.WriteAttribute(sw, "dtr", dtr, false);
			XmlHelper.WriteAttribute(sw, "del1", del1, false);
			XmlHelper.WriteAttribute(sw, "del2", del2, false);
			XmlHelper.WriteAttribute(sw, "r1", r1);
			XmlHelper.WriteAttribute(sw, "r2", r2);
			XmlHelper.WriteAttribute(sw, "ca", ca, false);
			if (t != 0)
			{
				XmlHelper.WriteAttribute(sw, "si", (double)si, true);
			}
			XmlHelper.WriteAttribute(sw, "bx", bx, false);
			if (!string.IsNullOrEmpty(valueField))
			{
				sw.Write(">");
				sw.Write(XmlHelper.EncodeXml(valueField));
				sw.Write(string.Format("</{0}>", nodeName));
			}
			else
			{
				sw.Write("/>");
			}
		}

		public CT_CellFormula()
		{
			tField = ST_CellFormulaType.normal;
			acaField = false;
			dt2DField = false;
			dtrField = false;
			del1Field = false;
			del2Field = false;
			caField = false;
			bxField = false;
		}

		public bool isSetRef()
		{
			return refField != null;
		}

		public CT_CellFormula Copy()
		{
			CT_CellFormula cT_CellFormula = new CT_CellFormula();
			cT_CellFormula.acaField = acaField;
			cT_CellFormula.bxField = bxField;
			cT_CellFormula.caField = caField;
			cT_CellFormula.del1Field = del1Field;
			cT_CellFormula.del2Field = del2Field;
			cT_CellFormula.dt2DField = dt2DField;
			cT_CellFormula.dtrField = dtrField;
			cT_CellFormula.r1Field = r1Field;
			cT_CellFormula.r2Field = r2Field;
			cT_CellFormula.refField = refField;
			cT_CellFormula.siField = siField;
			cT_CellFormula.siFieldSpecified = siFieldSpecified;
			cT_CellFormula.tField = tField;
			cT_CellFormula.valueField = valueField;
			return cT_CellFormula;
		}
	}
}
