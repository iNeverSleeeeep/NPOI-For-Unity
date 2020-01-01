using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_MdxTuple
	{
		private List<CT_MetadataStringIndex> nField;

		private uint cField;

		private string ctField;

		private uint siField;

		private bool siFieldSpecified;

		private uint fiField;

		private bool fiFieldSpecified;

		private byte[] bcField;

		private byte[] fcField;

		private bool iField;

		private bool uField;

		private bool stField;

		private bool bField;

		public List<CT_MetadataStringIndex> n
		{
			get
			{
				return nField;
			}
			set
			{
				nField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		public uint c
		{
			get
			{
				return cField;
			}
			set
			{
				cField = value;
			}
		}

		public string ct
		{
			get
			{
				return ctField;
			}
			set
			{
				ctField = value;
			}
		}

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

		public uint fi
		{
			get
			{
				return fiField;
			}
			set
			{
				fiField = value;
			}
		}

		[XmlIgnore]
		public bool fiSpecified
		{
			get
			{
				return fiFieldSpecified;
			}
			set
			{
				fiFieldSpecified = value;
			}
		}

		[XmlAttribute(DataType = "hexBinary")]
		public byte[] bc
		{
			get
			{
				return bcField;
			}
			set
			{
				bcField = value;
			}
		}

		[XmlAttribute(DataType = "hexBinary")]
		public byte[] fc
		{
			get
			{
				return fcField;
			}
			set
			{
				fcField = value;
			}
		}

		[DefaultValue(false)]
		public bool i
		{
			get
			{
				return iField;
			}
			set
			{
				iField = value;
			}
		}

		[DefaultValue(false)]
		public bool u
		{
			get
			{
				return uField;
			}
			set
			{
				uField = value;
			}
		}

		[DefaultValue(false)]
		public bool st
		{
			get
			{
				return stField;
			}
			set
			{
				stField = value;
			}
		}

		[DefaultValue(false)]
		public bool b
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

		public CT_MdxTuple()
		{
			nField = new List<CT_MetadataStringIndex>();
			cField = 0u;
			iField = false;
			uField = false;
			stField = false;
			bField = false;
		}
	}
}
