using NPOI.OpenXmlFormats.Shared;
using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("txbxContent", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_TxbxContent
	{
		private object[] itemsField;

		private ItemsChoiceType[] itemsElementNameField;

		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("altChunk", typeof(CT_AltChunk), Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlBlock), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("p", typeof(CT_P), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtBlock), Order = 0)]
		[XmlElement("tbl", typeof(CT_Tbl), Order = 0)]
		public object[] Items
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

		[XmlElement("ItemsElementName", Order = 1)]
		[XmlIgnore]
		public ItemsChoiceType[] ItemsElementName
		{
			get
			{
				return itemsElementNameField;
			}
			set
			{
				itemsElementNameField = value;
			}
		}

		public CT_TxbxContent()
		{
			itemsElementNameField = new ItemsChoiceType[0];
			itemsField = new object[0];
		}
	}
}
