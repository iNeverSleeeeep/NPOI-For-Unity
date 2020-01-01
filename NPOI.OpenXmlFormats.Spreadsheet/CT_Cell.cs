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
	public class CT_Cell
	{
		private CT_CellFormula fField;

		private string vField;

		private CT_Rst isField;

		private CT_ExtensionList extLstField;

		private string rField;

		private uint? sField = null;

		private ST_CellType? tField = null;

		private uint? cmField = null;

		private uint? vmField = null;

		private bool? phField = null;

		[XmlElement]
		public CT_CellFormula f
		{
			get
			{
				return fField;
			}
			set
			{
				fField = value;
			}
		}

		[XmlElement]
		public string v
		{
			get
			{
				return vField;
			}
			set
			{
				vField = value;
			}
		}

		[XmlElement("is")]
		public CT_Rst @is
		{
			get
			{
				return isField;
			}
			set
			{
				isField = value;
			}
		}

		[XmlElement]
		public CT_ExtensionList extLst
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
		public string r
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

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint s
		{
			get
			{
				if (sField.HasValue)
				{
					return sField.Value;
				}
				return 0u;
			}
			set
			{
				sField = value;
			}
		}

		[DefaultValue(ST_CellType.n)]
		[XmlAttribute]
		public ST_CellType t
		{
			get
			{
				if (tField.HasValue)
				{
					return tField.Value;
				}
				return ST_CellType.n;
			}
			set
			{
				tField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint cm
		{
			get
			{
				if (cmField.HasValue)
				{
					return cmField.Value;
				}
				return 0u;
			}
			set
			{
				cmField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint vm
		{
			get
			{
				if (vmField.HasValue)
				{
					return vmField.Value;
				}
				return 0u;
			}
			set
			{
				vmField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool ph
		{
			get
			{
				if (phField.HasValue)
				{
					return phField.Value;
				}
				return false;
			}
			set
			{
				phField = value;
			}
		}

		public static CT_Cell Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Cell cT_Cell = new CT_Cell();
			cT_Cell.r = XmlHelper.ReadString(node.Attributes["r"]);
			cT_Cell.s = XmlHelper.ReadUInt(node.Attributes["s"]);
			if (node.Attributes["t"] != null)
			{
				cT_Cell.t = (ST_CellType)Enum.Parse(typeof(ST_CellType), node.Attributes["t"].Value);
			}
			cT_Cell.cm = XmlHelper.ReadUInt(node.Attributes["cm"]);
			cT_Cell.vm = XmlHelper.ReadUInt(node.Attributes["vm"]);
			cT_Cell.ph = XmlHelper.ReadBool(node.Attributes["ph"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "f")
				{
					cT_Cell.f = CT_CellFormula.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "v")
				{
					cT_Cell.v = childNode.InnerText;
				}
				else if (childNode.LocalName == "is")
				{
					cT_Cell.@is = CT_Rst.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Cell.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Cell;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r", r);
			XmlHelper.WriteAttribute(sw, "s", s);
			if (t != ST_CellType.n)
			{
				XmlHelper.WriteAttribute(sw, "t", t.ToString());
			}
			XmlHelper.WriteAttribute(sw, "cm", cm);
			XmlHelper.WriteAttribute(sw, "vm", vm);
			XmlHelper.WriteAttribute(sw, "ph", ph, false);
			if (f == null && v == null && @is == null && extLstField == null)
			{
				sw.Write("/>");
			}
			else
			{
				sw.Write(">");
				if (f != null)
				{
					f.Write(sw, "f");
				}
				if (v != null)
				{
					sw.Write(string.Format("<v>{0}</v>", v));
				}
				if (@is != null)
				{
					@is.Write(sw, "is");
				}
				if (extLst != null)
				{
					extLst.Write(sw, "extLst");
				}
				sw.Write(string.Format("</{0}>", nodeName));
			}
		}

		public void Set(CT_Cell cell)
		{
			fField = cell.fField;
			vField = cell.vField;
			isField = cell.isField;
			extLstField = cell.extLstField;
			rField = cell.rField;
			sField = cell.sField;
			tField = cell.tField;
			cmField = cell.cmField;
			vmField = cell.vmField;
			phField = cell.phField;
		}

		public bool IsSetT()
		{
			return tField != ST_CellType.n;
		}

		public bool IsSetS()
		{
			uint? num = sField;
			if (num.GetValueOrDefault() == 0)
			{
				return !num.HasValue;
			}
			return true;
		}

		public bool IsSetF()
		{
			return fField != null;
		}

		public bool IsSetV()
		{
			return vField != null;
		}

		public bool IsSetIs()
		{
			return isField != null;
		}

		public bool IsSetR()
		{
			return rField != null;
		}

		public void unsetF()
		{
			fField = null;
		}

		public void unsetV()
		{
			vField = null;
		}

		public void unsetS()
		{
			sField = 0u;
		}

		public void unsetT()
		{
			tField = ST_CellType.n;
		}

		public void unsetIs()
		{
			isField = null;
		}

		public void unsetR()
		{
			rField = null;
		}
	}
}
