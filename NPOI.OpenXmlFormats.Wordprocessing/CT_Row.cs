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
	public class CT_Row
	{
		private CT_TblPrEx tblPrExField;

		private CT_TrPr trPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceTableRowType> itemsElementNameField;

		private byte[] rsidRPrField;

		private byte[] rsidRField;

		private byte[] rsidDelField;

		private byte[] rsidTrField;

		private object parent;

		private CT_Tbl _table;

		[XmlIgnore]
		public object Parent
		{
			get
			{
				return parent;
			}
		}

		[XmlElement(Order = 0)]
		public CT_TblPrEx tblPrEx
		{
			get
			{
				return tblPrExField;
			}
			set
			{
				tblPrExField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TrPr trPr
		{
			get
			{
				return trPrField;
			}
			set
			{
				trPrField = value;
			}
		}

		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 2)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 2)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 2)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 2)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 2)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 2)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 2)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 2)]
		[XmlElement("customXml", typeof(CT_CustomXmlCell), Order = 2)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 2)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 2)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 2)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 2)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 2)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 2)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 2)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 2)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 2)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 2)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 2)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 2)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 2)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 2)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 2)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 2)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 2)]
		[XmlElement("sdt", typeof(CT_SdtCell), Order = 2)]
		[XmlElement("tc", typeof(CT_Tc), Order = 2)]
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

		[XmlElement("ItemsElementName", Order = 3)]
		[XmlIgnore]
		public List<ItemsChoiceTableRowType> ItemsElementName
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] rsidRPr
		{
			get
			{
				return rsidRPrField;
			}
			set
			{
				rsidRPrField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] rsidR
		{
			get
			{
				return rsidRField;
			}
			set
			{
				rsidRField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] rsidDel
		{
			get
			{
				return rsidDelField;
			}
			set
			{
				rsidDelField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] rsidTr
		{
			get
			{
				return rsidTrField;
			}
			set
			{
				rsidTrField = value;
			}
		}

		[XmlIgnore]
		public CT_Tbl Table
		{
			get
			{
				return _table;
			}
			set
			{
				_table = value;
			}
		}

		public CT_Row()
		{
			itemsElementNameField = new List<ItemsChoiceTableRowType>();
			itemsField = new ArrayList();
		}

		public static CT_Row Parse(XmlNode node, XmlNamespaceManager namespaceManager, object parent)
		{
			if (node == null)
			{
				return null;
			}
			CT_Row cT_Row = new CT_Row();
			if (parent != null)
			{
				cT_Row.parent = parent;
			}
			cT_Row.rsidRPr = XmlHelper.ReadBytes(node.Attributes["w:rsidRPr"]);
			cT_Row.rsidR = XmlHelper.ReadBytes(node.Attributes["w:rsidR"]);
			cT_Row.rsidDel = XmlHelper.ReadBytes(node.Attributes["w:rsidDel"]);
			cT_Row.rsidTr = XmlHelper.ReadBytes(node.Attributes["w:rsidTr"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblPrEx")
				{
					cT_Row.tblPrEx = CT_TblPrEx.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "trPr")
				{
					cT_Row.trPr = CT_TrPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_Row.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_Row.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_Row.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.commentRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_Row.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_Row.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_Row.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_Row.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_Row.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.del);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_Row.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.moveTo);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_Row.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_Row.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_Row.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.bookmarkEnd);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_Row.Items.Add(CT_CustomXmlCell.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_Row.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_Row.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_Row.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_Row.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_Row.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_Row.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_Row.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.moveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_Row.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_Row.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_Row.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_Row.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_Row.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_Row.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.proofErr);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_Row.Items.Add(CT_SdtCell.Parse(childNode, namespaceManager));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.sdt);
				}
				else if (childNode.LocalName == "tc")
				{
					cT_Row.Items.Add(CT_Tc.Parse(childNode, namespaceManager, cT_Row));
					cT_Row.ItemsElementName.Add(ItemsChoiceTableRowType.tc);
				}
			}
			return cT_Row;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:rsidRPr", rsidRPr);
			XmlHelper.WriteAttribute(sw, "w:rsidR", rsidR);
			XmlHelper.WriteAttribute(sw, "w:rsidDel", rsidDel);
			XmlHelper.WriteAttribute(sw, "w:rsidTr", rsidTr);
			sw.Write(">");
			if (tblPrEx != null)
			{
				tblPrEx.Write(sw, "tblPrEx");
			}
			if (trPr != null)
			{
				trPr.Write(sw, "trPr");
			}
			foreach (object item in Items)
			{
				if (item is CT_Bookmark)
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
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
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
					((CT_RunTrackChange)item).Write(sw, "moveTo");
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
				else if (item is CT_CustomXmlCell)
				{
					((CT_CustomXmlCell)item).Write(sw, "customXml");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
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
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
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
				else if (item is CT_SdtCell)
				{
					((CT_SdtCell)item).Write(sw, "sdt");
				}
				else if (item is CT_Tc)
				{
					((CT_Tc)item).Write(sw, "tc");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public void RemoveTc(int pos)
		{
			RemoveObject(ItemsChoiceTableRowType.tc, pos);
		}

		public IList<CT_Tc> GetTcList()
		{
			return GetObjectList<CT_Tc>(ItemsChoiceTableRowType.tc);
		}

		public bool IsSetTrPr()
		{
			if (trPrField == null)
			{
				return false;
			}
			return trPrField.Items.Count > 0;
		}

		public CT_TrPr AddNewTrPr()
		{
			if (trPrField == null)
			{
				trPrField = new CT_TrPr();
			}
			return trPrField;
		}

		public CT_Tc AddNewTc()
		{
			return AddNewObject<CT_Tc>(ItemsChoiceTableRowType.tc);
		}

		public int SizeOfTcArray()
		{
			return SizeOfArray(ItemsChoiceTableRowType.tc);
		}

		public CT_Tc GetTcArray(int p)
		{
			return GetObjectArray<CT_Tc>(p, ItemsChoiceTableRowType.tc);
		}

		private List<T> GetObjectList<T>(ItemsChoiceTableRowType type) where T : class
		{
			lock (this)
			{
				List<T> list = new List<T>();
				for (int i = 0; i < itemsElementNameField.Count; i++)
				{
					if (itemsElementNameField[i] == type)
					{
						list.Add(itemsField[i] as T);
					}
				}
				return list;
			}
		}

		private int SizeOfArray(ItemsChoiceTableRowType type)
		{
			lock (this)
			{
				int num = 0;
				for (int i = 0; i < itemsElementNameField.Count; i++)
				{
					if (itemsElementNameField[i] == type)
					{
						num++;
					}
				}
				return num;
			}
		}

		private T GetObjectArray<T>(int p, ItemsChoiceTableRowType type) where T : class
		{
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				if (objectIndex < 0 || objectIndex >= itemsField.Count)
				{
					return null;
				}
				return itemsField[objectIndex] as T;
			}
		}

		private T InsertNewObject<T>(ItemsChoiceTableRowType type, int p) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				itemsElementNameField.Insert(objectIndex, type);
				itemsField.Insert(objectIndex, val);
				return val;
			}
		}

		private T AddNewObject<T>(ItemsChoiceTableRowType type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceTableRowType type, int p, T obj) where T : class
		{
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				if (objectIndex >= 0 && objectIndex < itemsField.Count)
				{
					if (!(itemsField[objectIndex] is T))
					{
						throw new Exception(string.Format("object types are difference, itemsField[{0}] is {1}, and parameter obj is {2}", objectIndex, itemsField[objectIndex].GetType().Name, typeof(T).Name));
					}
					itemsField[objectIndex] = obj;
				}
			}
		}

		private int GetObjectIndex(ItemsChoiceTableRowType type, int p)
		{
			int result = -1;
			int num = 0;
			for (int i = 0; i < itemsElementNameField.Count; i++)
			{
				if (itemsElementNameField[i] == type)
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

		private void RemoveObject(ItemsChoiceTableRowType type, int p)
		{
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				if (objectIndex >= 0 && objectIndex < itemsField.Count)
				{
					itemsElementNameField.RemoveAt(objectIndex);
					itemsField.RemoveAt(objectIndex);
				}
			}
		}
	}
}
