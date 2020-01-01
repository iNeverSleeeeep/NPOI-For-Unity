using System.Collections.Generic;
using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionRowColumn
	{
		private List<object> itemsField;

		private uint rIdField;

		private bool uaField;

		private bool raField;

		private uint sIdField;

		private bool eolField;

		private string refField;

		private ST_rwColActionType actionField;

		private bool edgeField;

		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
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
		public bool eol
		{
			get
			{
				return eolField;
			}
			set
			{
				eolField = value;
			}
		}

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

		public ST_rwColActionType action
		{
			get
			{
				return actionField;
			}
			set
			{
				actionField = value;
			}
		}

		[DefaultValue(false)]
		public bool edge
		{
			get
			{
				return edgeField;
			}
			set
			{
				edgeField = value;
			}
		}

		public CT_RevisionRowColumn()
		{
			itemsField = new List<object>();
			uaField = false;
			raField = false;
			eolField = false;
			edgeField = false;
		}
	}
}
