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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SdtContentCell
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType23> itemsElementNameField;

		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlCell), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtCell), Order = 0)]
		[XmlElement("tc", typeof(CT_Tc), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
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

		[XmlElement("ItemsElementName", Order = 1)]
		[XmlIgnore]
		public List<ItemsChoiceType23> ItemsElementName
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

		public CT_SdtContentCell()
		{
			itemsElementNameField = new List<ItemsChoiceType23>();
			itemsField = new ArrayList();
		}

		public static CT_SdtContentCell Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtContentCell cT_SdtContentCell = new CT_SdtContentCell();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "permStart")
				{
					cT_SdtContentCell.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.permStart);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_SdtContentCell.Items.Add(CT_SdtCell.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.sdt);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_SdtContentCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_SdtContentCell.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.proofErr);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_SdtContentCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_SdtContentCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_SdtContentCell.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_SdtContentCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_SdtContentCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_SdtContentCell.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.moveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_SdtContentCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "tc")
				{
					cT_SdtContentCell.Items.Add(CT_Tc.Parse(childNode, namespaceManager, cT_SdtContentCell));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.tc);
				}
				else if (childNode.LocalName == "del")
				{
					cT_SdtContentCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.del);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_SdtContentCell.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.permEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_SdtContentCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.commentRangeStart);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_SdtContentCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.moveFrom);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_SdtContentCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_SdtContentCell.Items.Add(CT_CustomXmlCell.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXml);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_SdtContentCell.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.oMath);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_SdtContentCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_SdtContentCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_SdtContentCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.ins);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_SdtContentCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_SdtContentCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_SdtContentCell.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_SdtContentCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_SdtContentCell.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_SdtContentCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentCell.ItemsElementName.Add(ItemsChoiceType23.commentRangeEnd);
				}
			}
			return cT_SdtContentCell;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
				}
				else if (item is CT_SdtCell)
				{
					((CT_SdtCell)item).Write(sw, "sdt");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
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
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_Tc)
				{
					((CT_Tc)item).Write(sw, "tc");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_CustomXmlCell)
				{
					((CT_CustomXmlCell)item).Write(sw, "customXml");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
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
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
