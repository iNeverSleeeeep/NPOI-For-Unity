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
	public class CT_CustomXmlCell
	{
		private CT_CustomXmlPr customXmlPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceType27> itemsElementNameField;

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

		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlCell), Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("sdt", typeof(CT_SdtCell), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("tc", typeof(CT_Tc), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
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
		public List<ItemsChoiceType27> ItemsElementName
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

		public CT_CustomXmlCell()
		{
			itemsElementNameField = new List<ItemsChoiceType27>();
			itemsField = new ArrayList();
		}

		public static CT_CustomXmlCell Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomXmlCell cT_CustomXmlCell = new CT_CustomXmlCell();
			cT_CustomXmlCell.uri = XmlHelper.ReadString(node.Attributes["w:uri"]);
			cT_CustomXmlCell.element = XmlHelper.ReadString(node.Attributes["w:element"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_CustomXmlCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlPr")
				{
					cT_CustomXmlCell.customXmlPr = CT_CustomXmlPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_CustomXmlCell.Items.Add(CT_SdtCell.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.sdt);
				}
				else if (childNode.LocalName == "tc")
				{
					cT_CustomXmlCell.Items.Add(CT_Tc.Parse(childNode, namespaceManager, cT_CustomXmlCell));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.tc);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.permEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_CustomXmlCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_CustomXmlCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_CustomXmlCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_CustomXmlCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_CustomXmlCell.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_CustomXmlCell.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_CustomXmlCell.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.moveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_CustomXmlCell.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_CustomXmlCell.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.proofErr);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_CustomXmlCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_CustomXmlCell.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_CustomXmlCell.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_CustomXmlCell.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_CustomXmlCell.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_CustomXmlCell.Items.Add(Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_CustomXmlCell.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_CustomXmlCell.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlCell.ItemsElementName.Add(ItemsChoiceType27.customXmlInsRangeEnd);
				}
			}
			return cT_CustomXmlCell;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:uri", uri);
			XmlHelper.WriteAttribute(sw, "w:element", element);
			sw.Write(">");
			if (customXmlPr != null)
			{
				customXmlPr.Write(sw, "customXmlPr");
			}
			foreach (object item in Items)
			{
				if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_SdtCell)
				{
					((CT_SdtCell)item).Write(sw, "sdt");
				}
				else if (item is CT_Tc)
				{
					((CT_Tc)item).Write(sw, "tc");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
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
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_OMath)
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
				else if (item is CT_CustomXmlCell)
				{
					((CT_CustomXmlCell)item).Write(sw, "customXml");
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
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
