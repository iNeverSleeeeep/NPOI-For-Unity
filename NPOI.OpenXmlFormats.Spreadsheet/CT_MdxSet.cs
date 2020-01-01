using System.Collections.Generic;
using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_MdxSet
	{
		private List<CT_MetadataStringIndex> nField;

		private uint nsField;

		private uint cField;

		private ST_MdxSetOrder oField;

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

		public uint ns
		{
			get
			{
				return nsField;
			}
			set
			{
				nsField = value;
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

		[DefaultValue(ST_MdxSetOrder.u)]
		public ST_MdxSetOrder o
		{
			get
			{
				return oField;
			}
			set
			{
				oField = value;
			}
		}

		public CT_MdxSet()
		{
			nField = new List<CT_MetadataStringIndex>();
			cField = 0u;
			oField = ST_MdxSetOrder.u;
		}
	}
}
