using System.Collections.Generic;
using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionHeaders
	{
		private List<CT_RevisionHeader> headerField;

		private string guidField;

		private string lastGuidField;

		private bool sharedField;

		private bool diskRevisionsField;

		private bool historyField;

		private bool trackRevisionsField;

		private bool exclusiveField;

		private uint revisionIdField;

		private int versionField;

		private bool keepChangeHistoryField;

		private bool protectedField;

		private uint preserveHistoryField;

		public List<CT_RevisionHeader> header
		{
			get
			{
				return headerField;
			}
			set
			{
				headerField = value;
			}
		}

		public string guid
		{
			get
			{
				return guidField;
			}
			set
			{
				guidField = value;
			}
		}

		public string lastGuid
		{
			get
			{
				return lastGuidField;
			}
			set
			{
				lastGuidField = value;
			}
		}

		[DefaultValue(true)]
		public bool shared
		{
			get
			{
				return sharedField;
			}
			set
			{
				sharedField = value;
			}
		}

		[DefaultValue(false)]
		public bool diskRevisions
		{
			get
			{
				return diskRevisionsField;
			}
			set
			{
				diskRevisionsField = value;
			}
		}

		[DefaultValue(true)]
		public bool history
		{
			get
			{
				return historyField;
			}
			set
			{
				historyField = value;
			}
		}

		[DefaultValue(true)]
		public bool trackRevisions
		{
			get
			{
				return trackRevisionsField;
			}
			set
			{
				trackRevisionsField = value;
			}
		}

		[DefaultValue(false)]
		public bool exclusive
		{
			get
			{
				return exclusiveField;
			}
			set
			{
				exclusiveField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		public uint revisionId
		{
			get
			{
				return revisionIdField;
			}
			set
			{
				revisionIdField = value;
			}
		}

		[DefaultValue(1)]
		public int version
		{
			get
			{
				return versionField;
			}
			set
			{
				versionField = value;
			}
		}

		[DefaultValue(true)]
		public bool keepChangeHistory
		{
			get
			{
				return keepChangeHistoryField;
			}
			set
			{
				keepChangeHistoryField = value;
			}
		}

		[DefaultValue(false)]
		public bool @protected
		{
			get
			{
				return protectedField;
			}
			set
			{
				protectedField = value;
			}
		}

		[DefaultValue(typeof(uint), "30")]
		public uint preserveHistory
		{
			get
			{
				return preserveHistoryField;
			}
			set
			{
				preserveHistoryField = value;
			}
		}

		public CT_RevisionHeaders()
		{
			headerField = new List<CT_RevisionHeader>();
			sharedField = true;
			diskRevisionsField = false;
			historyField = true;
			trackRevisionsField = true;
			exclusiveField = false;
			revisionIdField = 0u;
			versionField = 1;
			keepChangeHistoryField = true;
			protectedField = false;
			preserveHistoryField = 30u;
		}
	}
}
