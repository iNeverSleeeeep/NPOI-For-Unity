using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionCellChange
	{
		private CT_Cell ocField;

		private CT_Cell ncField;

		private CT_Dxf odxfField;

		private CT_Dxf ndxfField;

		private CT_ExtensionList extLstField;

		private uint rIdField;

		private bool uaField;

		private bool raField;

		private uint sIdField;

		private bool odxf1Field;

		private bool xfDxfField;

		private bool sField;

		private bool dxfField;

		private uint numFmtIdField;

		private bool numFmtIdFieldSpecified;

		private bool quotePrefixField;

		private bool oldQuotePrefixField;

		private bool phField;

		private bool oldPhField;

		private bool endOfListFormulaUpdateField;

		public CT_Cell oc
		{
			get
			{
				return ocField;
			}
			set
			{
				ocField = value;
			}
		}

		public CT_Cell nc
		{
			get
			{
				return ncField;
			}
			set
			{
				ncField = value;
			}
		}

		public CT_Dxf odxf
		{
			get
			{
				return odxfField;
			}
			set
			{
				odxfField = value;
			}
		}

		public CT_Dxf ndxf
		{
			get
			{
				return ndxfField;
			}
			set
			{
				ndxfField = value;
			}
		}

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

		public uint rId
		{
			get
			{
				return rIdField;
			}
			set
			{
				rIdField = value;
			}
		}

		[DefaultValue(false)]
		public bool ua
		{
			get
			{
				return uaField;
			}
			set
			{
				uaField = value;
			}
		}

		[DefaultValue(false)]
		public bool ra
		{
			get
			{
				return raField;
			}
			set
			{
				raField = value;
			}
		}

		public uint sId
		{
			get
			{
				return sIdField;
			}
			set
			{
				sIdField = value;
			}
		}

		[DefaultValue(false)]
		public bool odxf1
		{
			get
			{
				return odxf1Field;
			}
			set
			{
				odxf1Field = value;
			}
		}

		[DefaultValue(false)]
		public bool xfDxf
		{
			get
			{
				return xfDxfField;
			}
			set
			{
				xfDxfField = value;
			}
		}

		[DefaultValue(false)]
		public bool s
		{
			get
			{
				return sField;
			}
			set
			{
				sField = value;
			}
		}

		[DefaultValue(false)]
		public bool dxf
		{
			get
			{
				return dxfField;
			}
			set
			{
				dxfField = value;
			}
		}

		public uint numFmtId
		{
			get
			{
				return numFmtIdField;
			}
			set
			{
				numFmtIdField = value;
			}
		}

		[XmlIgnore]
		public bool numFmtIdSpecified
		{
			get
			{
				return numFmtIdFieldSpecified;
			}
			set
			{
				numFmtIdFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		public bool quotePrefix
		{
			get
			{
				return quotePrefixField;
			}
			set
			{
				quotePrefixField = value;
			}
		}

		[DefaultValue(false)]
		public bool oldQuotePrefix
		{
			get
			{
				return oldQuotePrefixField;
			}
			set
			{
				oldQuotePrefixField = value;
			}
		}

		[DefaultValue(false)]
		public bool ph
		{
			get
			{
				return phField;
			}
			set
			{
				phField = value;
			}
		}

		[DefaultValue(false)]
		public bool oldPh
		{
			get
			{
				return oldPhField;
			}
			set
			{
				oldPhField = value;
			}
		}

		[DefaultValue(false)]
		public bool endOfListFormulaUpdate
		{
			get
			{
				return endOfListFormulaUpdateField;
			}
			set
			{
				endOfListFormulaUpdateField = value;
			}
		}

		public CT_RevisionCellChange()
		{
			extLstField = new CT_ExtensionList();
			ndxfField = new CT_Dxf();
			odxfField = new CT_Dxf();
			ncField = new CT_Cell();
			ocField = new CT_Cell();
			uaField = false;
			raField = false;
			odxf1Field = false;
			xfDxfField = false;
			sField = false;
			dxfField = false;
			quotePrefixField = false;
			oldQuotePrefixField = false;
			phField = false;
			oldPhField = false;
			endOfListFormulaUpdateField = false;
		}
	}
}
