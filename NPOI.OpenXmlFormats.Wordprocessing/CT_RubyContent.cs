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
	public class CT_RubyContent
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType16> itemsElementNameField;

		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("r", typeof(CT_R), Order = 0)]
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
		public List<ItemsChoiceType16> ItemsElementName
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

		public CT_RubyContent()
		{
			itemsElementNameField = new List<ItemsChoiceType16>();
			itemsField = new ArrayList();
		}

		public static CT_RubyContent Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RubyContent cT_RubyContent = new CT_RubyContent();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "oMath")
				{
					cT_RubyContent.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.oMath);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_RubyContent.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_RubyContent.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.proofErr);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_RubyContent.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.permEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_RubyContent.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.moveToRangeStart);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_RubyContent.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_RubyContent.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.moveTo);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_RubyContent.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_RubyContent.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_RubyContent.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.moveFrom);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_RubyContent.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_RubyContent.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_RubyContent.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_RubyContent.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_RubyContent.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_RubyContent.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_RubyContent.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_RubyContent.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.ins);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_RubyContent.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_RubyContent.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.commentRangeStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_RubyContent.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.commentRangeEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_RubyContent.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.bookmarkStart);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_RubyContent.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.bookmarkEnd);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_RubyContent.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.oMathPara);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_RubyContent.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "r")
				{
					cT_RubyContent.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_RubyContent.ItemsElementName.Add(ItemsChoiceType16.r);
				}
			}
			return cT_RubyContent;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			int num = 0;
			foreach (object item in Items)
			{
				if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == ItemsChoiceType16.moveToRangeStart)
				{
					((CT_MoveBookmark)item).Write(sw, "moveToRangeStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType16.moveToRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveToRangeEnd");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType16.moveTo)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType16.moveFrom)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == ItemsChoiceType16.moveFromRangeStart)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType16.moveFromRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType16.customXmlInsRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType16.customXmlInsRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType16.customXmlMoveFromRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType16.customXmlMoveFromRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType16.customXmlMoveToRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType16.customXmlMoveToRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType16.del)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType16.ins)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType16.customXmlDelRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType16.customXmlDelRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType16.commentRangeStart)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType16.commentRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType16.bookmarkEnd)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_R)
				{
					((CT_R)item).Write(sw, "r");
				}
				num++;
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
