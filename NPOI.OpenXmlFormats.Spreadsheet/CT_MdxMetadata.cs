using System.Collections.Generic;
using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_MdxMetadata
	{
		private List<CT_Mdx> mdxField;

		private uint countField;

		public List<CT_Mdx> mdx
		{
			get
			{
				return mdxField;
			}
			set
			{
				mdxField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		public CT_MdxMetadata()
		{
			mdxField = new List<CT_Mdx>();
			countField = 0u;
		}
	}
}
