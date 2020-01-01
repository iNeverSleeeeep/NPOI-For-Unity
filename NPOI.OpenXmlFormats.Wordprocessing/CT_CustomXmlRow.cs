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
	public class CT_CustomXmlRow
	{
		private CT_CustomXmlPr customXmlPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceType21> itemsElementNameField;

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

		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlRow), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("sdt", typeof(CT_SdtRow), Order = 1)]
		[XmlElement("tr", typeof(CT_Row), Order = 1)]
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
		[XmlElement("ItemsElementName", Order = 2)]
		public List<ItemsChoiceType21> ItemsElementName
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

		public CT_CustomXmlRow()
		{
			itemsElementNameField = new List<ItemsChoiceType21>();
			itemsField = new ArrayList();
		}

		public static CT_CustomXmlRow Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomXmlRow cT_CustomXmlRow = new CT_CustomXmlRow();
			cT_CustomXmlRow.uri = XmlHelper.ReadString(node.Attributes["w:uri"]);
			cT_CustomXmlRow.element = XmlHelper.ReadString(node.Attributes["w:element"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_CustomXmlRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlPr")
				{
					cT_CustomXmlRow.customXmlPr = CT_CustomXmlPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_CustomXmlRow.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.oMathPara);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_CustomXmlRow.Items.Add(CT_SdtRow.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.sdt);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_CustomXmlRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_CustomXmlRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_CustomXmlRow.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.oMath);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_CustomXmlRow.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_CustomXmlRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_CustomXmlRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_CustomXmlRow.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_CustomXmlRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_CustomXmlRow.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_CustomXmlRow.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_CustomXmlRow.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.proofErr);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "tr")
				{
					cT_CustomXmlRow.Items.Add(CT_Row.Parse(childNode, namespaceManager, cT_CustomXmlRow));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.tr);
				}
				else if (childNode.LocalName == "del")
				{
					cT_CustomXmlRow.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.del);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_CustomXmlRow.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_CustomXmlRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_CustomXmlRow.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_CustomXmlRow.Items.Add(Parse(childNode, namespaceManager));
					cT_CustomXmlRow.ItemsElementName.Add(ItemsChoiceType21.customXml);
				}
			}
			return cT_CustomXmlRow;
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
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_SdtRow)
				{
					((CT_SdtRow)item).Write(sw, "sdt");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
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
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_Row)
				{
					((CT_Row)item).Write(sw, "tr");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
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
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
