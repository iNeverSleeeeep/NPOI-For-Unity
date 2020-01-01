using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_ReviewedRevisions
	{
		private List<CT_Reviewed> reviewedField;

		private uint countField;

		private bool countFieldSpecified;

		public List<CT_Reviewed> reviewed
		{
			get
			{
				return reviewedField;
			}
			set
			{
				reviewedField = value;
			}
		}

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

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		public CT_ReviewedRevisions()
		{
			reviewedField = new List<CT_Reviewed>();
		}
	}
}
