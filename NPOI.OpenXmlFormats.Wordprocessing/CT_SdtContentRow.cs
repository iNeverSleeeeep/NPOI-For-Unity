using NPOI.OpenXmlFormats.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_SdtContentRow
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType22> itemsElementNameField;

		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlRow), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtRow), Order = 0)]
		[XmlElement("tr", typeof(CT_Row), Order = 0)]
		public ArrayList Items
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

		[XmlIgnore]
		[XmlElement("ItemsElementName", Order = 1)]
		public List<ItemsChoiceType22> ItemsElementName
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

		public CT_SdtContentRow()
		{
			itemsElementNameField = new List<ItemsChoiceType22>();
			itemsField = new ArrayList();
		}

		public static CT_SdtContentRow Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtContentRow cT_SdtContentRow = new CT_SdtContentRow();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "oMath")
				{
					cT_SdtContentRow.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_SdtContentRow.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_SdtContentRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_SdtContentRow.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_SdtContentRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_SdtContentRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_SdtContentRow.Items.Add(CT_CustomXmlRow.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_SdtContentRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_SdtContentRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_SdtContentRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_SdtContentRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_SdtContentRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_SdtContentRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_SdtContentRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_SdtContentRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_SdtContentRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_SdtContentRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_SdtContentRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_SdtContentRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_SdtContentRow.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_SdtContentRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_SdtContentRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_SdtContentRow.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_SdtContentRow.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_SdtContentRow.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_SdtContentRow.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.proofErr);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_SdtContentRow.Items.Add(CT_SdtRow.Parse(childNode, namespaceManager));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.sdt);
				}
				else if (childNode.LocalName == "tr")
				{
					cT_SdtContentRow.Items.Add(CT_Row.Parse(childNode, namespaceManager, cT_SdtContentRow));
					cT_SdtContentRow.ItemsElementName.Add(ItemsChoiceType22.tr);
				}
			}
			return cT_SdtContentRow;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_CustomXmlRow)
				{
					((CT_CustomXmlRow)item).Write(sw, "customXml");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveToRangeEnd");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveToRangeStart");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
				}
				else if (item is CT_SdtRow)
				{
					((CT_SdtRow)item).Write(sw, "sdt");
				}
				else if (item is CT_Row)
				{
					((CT_Row)item).Write(sw, "tr");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
