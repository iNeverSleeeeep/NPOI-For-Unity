using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_CustomXmlBlock
	{
		private CT_CustomXmlPr customXmlPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceType26> itemsElementNameField;

		private string uriField;

		private string elementField;

		[XmlElement(Order = 0)]
		public CT_CustomXmlPr customXmlPr
		{
			get
			{
				return customXmlPrField;
			}
			set
			{
				customXmlPrField = value;
			}
		}

		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("p", typeof(CT_P), Order = 1)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 1)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 1)]
		[XmlElement("sdt", typeof(CT_SdtBlock), Order = 1)]
		[XmlElement("tbl", typeof(CT_Tbl), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlBlock), Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
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

		[XmlElement("ItemsElementName", Order = 2)]
		[XmlIgnore]
		public List<ItemsChoiceType26> ItemsElementName
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string uri
		{
			get
			{
				return uriField;
			}
			set
			{
				uriField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string element
		{
			get
			{
				return elementField;
			}
			set
			{
				elementField = value;
			}
		}

		public CT_CustomXmlBlock()
		{
			itemsElementNameField = new List<ItemsChoiceType26>();
			itemsField = new ArrayList();
		}

		public static CT_CustomXmlBlock Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomXmlBlock cT_CustomXmlBlock = new CT_CustomXmlBlock();
			cT_CustomXmlBlock.uri = XmlHelper.ReadString(node.Attributes["w:uri"]);
			cT_CustomXmlBlock.element = XmlHelper.ReadString(node.Attributes["w:element"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "del")
				{
					cT_CustomXmlBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.del);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_CustomXmlBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.moveFrom);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_CustomXmlBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.moveTo);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_CustomXmlBlock.Items.Add(Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_CustomXmlBlock.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.oMath);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_CustomXmlBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.ins);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.moveFromRangeStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.commentRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.moveToRangeStart);
				}
				else if (childNode.LocalName == "p")
				{
					cT_CustomXmlBlock.Items.Add(CT_P.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.p);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.permEnd);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_CustomXmlBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.bookmarkEnd);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_CustomXmlBlock.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.proofErr);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_CustomXmlBlock.Items.Add(CT_SdtBlock.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.sdt);
				}
				else if (childNode.LocalName == "tbl")
				{
					cT_CustomXmlBlock.Items.Add(CT_Tbl.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.tbl);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_CustomXmlBlock.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.permStart);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_CustomXmlBlock.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_CustomXmlBlock.ItemsElementName.Add(ItemsChoiceType26.oMathPara);
				}
			}
			return cT_CustomXmlBlock;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:uri", uri);
			XmlHelper.WriteAttribute(sw, "w:element", element);
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_CustomXmlBlock)
				{
					((CT_CustomXmlBlock)item).Write(sw, "customXml");
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
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveToRangeEnd");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveToRangeStart");
				}
				else if (item is CT_P)
				{
					((CT_P)item).Write(sw, "p");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
				}
				else if (item is CT_SdtBlock)
				{
					((CT_SdtBlock)item).Write(sw, "sdt");
				}
				else if (item is CT_Tbl)
				{
					((CT_Tbl)item).Write(sw, "tbl");
				}
				else if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
