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
	public class CT_Tbl
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType30> itemsElementNameField;

		private CT_TblPr tblPrField;

		private CT_TblGrid tblGridField;

		private ArrayList items1Field;

		private List<Items1ChoiceType> items1ElementNameField;

		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
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
		public List<ItemsChoiceType30> ItemsElementName
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

		[XmlElement(Order = 2)]
		public CT_TblPr tblPr
		{
			get
			{
				return tblPrField;
			}
			set
			{
				tblPrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_TblGrid tblGrid
		{
			get
			{
				return tblGridField;
			}
			set
			{
				tblGridField = value;
			}
		}

		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 4)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 4)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 4)]
		[XmlChoiceIdentifier("Items1ElementName")]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 4)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 4)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 4)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 4)]
		[XmlElement("customXml", typeof(CT_CustomXmlRow), Order = 4)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 4)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 4)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 4)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 4)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 4)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 4)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 4)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 4)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 4)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 4)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 4)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 4)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 4)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 4)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 4)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 4)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 4)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 4)]
		[XmlElement("sdt", typeof(CT_SdtRow), Order = 4)]
		[XmlElement("tr", typeof(CT_Row), Order = 4)]
		public ArrayList Items1
		{
			get
			{
				return items1Field;
			}
			set
			{
				items1Field = value;
			}
		}

		[XmlIgnore]
		[XmlElement("Items1ElementName", Order = 5)]
		public List<Items1ChoiceType> Items1ElementName
		{
			get
			{
				return items1ElementNameField;
			}
			set
			{
				items1ElementNameField = value;
			}
		}

		public CT_Tbl()
		{
			items1ElementNameField = new List<Items1ChoiceType>();
			items1Field = new ArrayList();
			itemsElementNameField = new List<ItemsChoiceType30>();
			itemsField = new ArrayList();
		}

		public static CT_Tbl Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Tbl cT_Tbl = new CT_Tbl();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_Tbl.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "tblPr")
				{
					cT_Tbl.tblPr = CT_TblPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblGrid")
				{
					cT_Tbl.tblGrid = CT_TblGrid.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_Tbl.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_Tbl.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.moveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_Tbl.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_Tbl.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.moveFromRangeStart);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_Tbl.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_Tbl.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_Tbl.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_Tbl.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.commentRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_Tbl.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_Tbl.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_Tbl.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_Tbl.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_Tbl.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_Tbl.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_Tbl.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.ItemsElementName.Add(ItemsChoiceType30.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_Tbl.Items1.Add(CT_SdtRow.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.sdt);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_Tbl.Items1.Add(CT_CustomXmlRow.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXml);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_Tbl.Items1.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_Tbl.Items1.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_Tbl.Items1.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_Tbl.Items1.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_Tbl.Items1.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_Tbl.Items1.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_Tbl.Items1.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_Tbl.Items1.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_Tbl.Items1.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_Tbl.Items1.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_Tbl.Items1.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.commentRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_Tbl.Items1.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_Tbl.Items1.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_Tbl.Items1.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_Tbl.Items1.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_Tbl.Items1.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_Tbl.Items1.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.proofErr);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_Tbl.Items1.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.oMath);
				}
				else if (childNode.LocalName == "tr")
				{
					cT_Tbl.Items1.Add(CT_Row.Parse(childNode, namespaceManager, cT_Tbl));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.tr);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_Tbl.Items1.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.moveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_Tbl.Items1.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_Tbl.Items1.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_Tbl.Items1.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_Tbl.Items1.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_Tbl.Items1.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_Tbl.Items1.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tbl.Items1ElementName.Add(Items1ChoiceType.commentRangeEnd);
				}
			}
			return cT_Tbl;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
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
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
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
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
			}
			if (tblPr != null)
			{
				tblPr.Write(sw, "tblPr");
			}
			if (tblGrid != null)
			{
				tblGrid.Write(sw, "tblGrid");
			}
			foreach (object item2 in Items1)
			{
				if (item2 is CT_SdtRow)
				{
					((CT_SdtRow)item2).Write(sw, "sdt");
				}
				else if (item2 is CT_CustomXmlRow)
				{
					((CT_CustomXmlRow)item2).Write(sw, "customXml");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlInsRangeStart");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "del");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "ins");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "moveFrom");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "moveFromRangeEnd");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "commentRangeStart");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "moveTo");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "moveToRangeEnd");
				}
				else if (item2 is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item2).Write(sw, "moveToRangeStart");
				}
				else if (item2 is CT_Perm)
				{
					((CT_Perm)item2).Write(sw, "permEnd");
				}
				else if (item2 is CT_PermStart)
				{
					((CT_PermStart)item2).Write(sw, "permStart");
				}
				else if (item2 is CT_ProofErr)
				{
					((CT_ProofErr)item2).Write(sw, "proofErr");
				}
				else if (item2 is CT_OMath)
				{
					((CT_OMath)item2).Write(sw, "oMath");
				}
				else if (item2 is CT_Row)
				{
					((CT_Row)item2).Write(sw, "tr");
				}
				else if (item2 is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item2).Write(sw, "moveFromRangeStart");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlDelRangeStart");
				}
				else if (item2 is CT_OMathPara)
				{
					((CT_OMathPara)item2).Write(sw, "oMathPara");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "bookmarkEnd");
				}
				else if (item2 is CT_Bookmark)
				{
					((CT_Bookmark)item2).Write(sw, "bookmarkStart");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "commentRangeEnd");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public void Set(CT_Tbl table)
		{
			items1ElementNameField = new List<Items1ChoiceType>(table.Items1ElementName);
			items1Field = new ArrayList(table.items1Field);
			itemsElementNameField = new List<ItemsChoiceType30>(table.itemsElementNameField);
			itemsField = new ArrayList(table.itemsField);
			tblGridField = table.tblGridField;
			tblPrField = table.tblPrField;
		}

		public void RemoveTr(int pos)
		{
			RemoveItems1(Items1ChoiceType.tr, pos);
		}

		public CT_Row InsertNewTr(int pos)
		{
			return InsertNewItems1<CT_Row>(Items1ChoiceType.tr, pos);
		}

		public void SetTrArray(int pos, CT_Row cT_Row)
		{
			SetItems1Array(Items1ChoiceType.tr, pos, cT_Row);
		}

		public CT_Row AddNewTr()
		{
			return AddNewItems1<CT_Row>(Items1ChoiceType.tr);
		}

		public CT_TblPr AddNewTblPr()
		{
			if (tblPrField == null)
			{
				tblPrField = new CT_TblPr();
			}
			return tblPrField;
		}

		public int SizeOfTrArray()
		{
			return SizeOfItems1Array(Items1ChoiceType.tr);
		}

		public CT_Row GetTrArray(int p)
		{
			return GetItems1Array<CT_Row>(p, Items1ChoiceType.tr);
		}

		public List<CT_Row> GetTrList()
		{
			return GetItems1List<CT_Row>(Items1ChoiceType.tr);
		}

		private List<T> GetItems1List<T>(Items1ChoiceType type) where T : class
		{
			lock (this)
			{
				List<T> list = new List<T>();
				for (int i = 0; i < items1ElementNameField.Count; i++)
				{
					if (items1ElementNameField[i] == type)
					{
						list.Add(items1Field[i] as T);
					}
				}
				return list;
			}
		}

		private int SizeOfItems1Array(Items1ChoiceType type)
		{
			lock (this)
			{
				int num = 0;
				for (int i = 0; i < items1ElementNameField.Count; i++)
				{
					if (items1ElementNameField[i] == type)
					{
						num++;
					}
				}
				return num;
			}
		}

		private T GetItems1Array<T>(int p, Items1ChoiceType type) where T : class
		{
			lock (this)
			{
				int items1Index = GetItems1Index(type, p);
				if (items1Index < 0 || items1Index >= items1Field.Count)
				{
					return null;
				}
				return items1Field[items1Index] as T;
			}
		}

		private T InsertNewItems1<T>(Items1ChoiceType type, int p) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				int items1Index = GetItems1Index(type, p);
				items1ElementNameField.Insert(items1Index, type);
				items1Field.Insert(items1Index, val);
				return val;
			}
		}

		private T AddNewItems1<T>(Items1ChoiceType type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				items1ElementNameField.Add(type);
				items1Field.Add(val);
				return val;
			}
		}

		private void SetItems1Array<T>(Items1ChoiceType type, int p, T obj) where T : class
		{
			lock (this)
			{
				int items1Index = GetItems1Index(type, p);
				if (items1Index >= 0 && items1Index < items1Field.Count)
				{
					if (!(items1Field[items1Index] is T))
					{
						throw new Exception(string.Format("object types are difference, itemsField[{0}] is {1}, and parameter obj is {2}", items1Index, items1Field[items1Index].GetType().Name, typeof(T).Name));
					}
					items1Field[items1Index] = obj;
				}
			}
		}

		private int GetItems1Index(Items1ChoiceType type, int p)
		{
			int result = -1;
			int num = 0;
			for (int i = 0; i < items1ElementNameField.Count; i++)
			{
				if (items1ElementNameField[i] == type)
				{
					if (num == p)
					{
						result = i;
						break;
					}
					num++;
				}
			}
			return result;
		}

		private void RemoveItems1(Items1ChoiceType type, int p)
		{
			lock (this)
			{
				int items1Index = GetItems1Index(type, p);
				if (items1Index >= 0 && items1Index < items1Field.Count)
				{
					items1ElementNameField.RemoveAt(items1Index);
					items1Field.RemoveAt(items1Index);
				}
			}
		}

		public CT_TblGrid AddNewTblGrid()
		{
			tblGrid = new CT_TblGrid();
			return tblGrid;
		}
	}
}
