using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_Users
	{
		private List<CT_SharedUser> userInfoField;

		private uint countField;

		private bool countFieldSpecified;

		public List<CT_SharedUser> userInfo
		{
			get
			{
				return userInfoField;
			}
			set
			{
				userInfoField = value;
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

		public CT_Users()
		{
			userInfoField = new List<CT_SharedUser>();
		}
	}
}
