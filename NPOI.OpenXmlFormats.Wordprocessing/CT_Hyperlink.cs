using NPOI.OpenXmlFormats.Dml;
using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Hyperlink
	{
		private CT_EmbeddedWAVAudioFile sndField;

		private CT_OfficeArtExtensionList extLstField;

		private string idField;

		private string invalidUrlField;

		private string actionField;

		private string tgtFrameField;

		private string tooltipField;

		private bool historyField;

		private bool highlightClickField;

		private bool endSndField;

		[XmlElement(Order = 0)]
		public CT_EmbeddedWAVAudioFile snd
		{
			get
			{
				return sndField;
			}
			set
			{
				sndField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OfficeArtExtensionList extLst
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string invalidUrl
		{
			get
			{
				return invalidUrlField;
			}
			set
			{
				invalidUrlField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string action
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

		[DefaultValue("")]
		[XmlAttribute]
		public string tgtFrame
		{
			get
			{
				return tgtFrameField;
			}
			set
			{
				tgtFrameField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string tooltip
		{
			get
			{
				return tooltipField;
			}
			set
			{
				tooltipField = value;
			}
		}

		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(false)]
		public bool highlightClick
		{
			get
			{
				return highlightClickField;
			}
			set
			{
				highlightClickField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool endSnd
		{
			get
			{
				return endSndField;
			}
			set
			{
				endSndField = value;
			}
		}

		public CT_Hyperlink()
		{
			invalidUrlField = "";
			actionField = "";
			tgtFrameField = "";
			tooltipField = "";
			historyField = true;
			highlightClickField = false;
			endSndField = false;
		}
	}
}
