using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_UndoInfo
	{
		private uint indexField;

		private ST_FormulaExpression expField;

		private bool ref3DField;

		private bool arrayField;

		private bool vField;

		private bool nfField;

		private bool csField;

		private string drField;

		private string dnField;

		private string rField;

		private uint sIdField;

		private bool sIdFieldSpecified;

		public uint index
		{
			get
			{
				return indexField;
			}
			set
			{
				indexField = value;
			}
		}

		public ST_FormulaExpression exp
		{
			get
			{
				return expField;
			}
			set
			{
				expField = value;
			}
		}

		[DefaultValue(false)]
		public bool ref3D
		{
			get
			{
				return ref3DField;
			}
			set
			{
				ref3DField = value;
			}
		}

		[DefaultValue(false)]
		public bool array
		{
			get
			{
				return arrayField;
			}
			set
			{
				arrayField = value;
			}
		}

		[DefaultValue(false)]
		public bool v
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

		[DefaultValue(false)]
		public bool nf
		{
			get
			{
				return nfField;
			}
			set
			{
				nfField = value;
			}
		}

		[DefaultValue(false)]
		public bool cs
		{
			get
			{
				return csField;
			}
			set
			{
				csField = value;
			}
		}

		public string dr
		{
			get
			{
				return drField;
			}
			set
			{
				drField = value;
			}
		}

		public string dn
		{
			get
			{
				return dnField;
			}
			set
			{
				dnField = value;
			}
		}

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

		[XmlIgnore]
		public bool sIdSpecified
		{
			get
			{
				return sIdFieldSpecified;
			}
			set
			{
				sIdFieldSpecified = value;
			}
		}

		public CT_UndoInfo()
		{
			ref3DField = false;
			arrayField = false;
			vField = false;
			nfField = false;
			csField = false;
		}
	}
}
