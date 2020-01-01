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
	public class CT_Tc
	{
		private CT_TcPr tcPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceTableCellType> itemsElementNameField;

		private object parent;

		[XmlElement(Order = 0)]
		public CT_TcPr tcPr
		{
			get
			{
				return tcPrField;
			}
			set
			{
				tcPrField = value;
			}
		}

		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("altChunk", typeof(CT_AltChunk), Order = 1)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlBlock), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
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
		public List<ItemsChoiceTableCellType> ItemsElementName
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

		[XmlIgnore]
		public object Parent
		{
			get
			{
				return parent;
			}
		}

		public CT_Tc()
		{
			itemsElementNameField = new List<ItemsChoiceTableCellType>();
			itemsField = new ArrayList();
		}

		public static CT_Tc Parse(XmlNode node, XmlNamespaceManager namespaceManager, object parent)
		{
			if (node == null)
			{
				return null;
			}
			CT_Tc cT_Tc = new CT_Tc();
			if (parent != null)
			{
				cT_Tc.parent = parent;
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "commentRangeEnd")
				{
					cT_Tc.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.commentRangeEnd);
				}
				else if (childNode.LocalName == "tcPr")
				{
					cT_Tc.tcPr = CT_TcPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_Tc.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_Tc.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.oMathPara);
				}
				else if (childNode.LocalName == "altChunk")
				{
					cT_Tc.Items.Add(CT_AltChunk.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.altChunk);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_Tc.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_Tc.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.bookmarkStart);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_Tc.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.moveFromRangeStart);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_Tc.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_Tc.Items.Add(CT_CustomXmlBlock.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_Tc.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_Tc.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_Tc.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_Tc.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_Tc.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_Tc.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_Tc.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_Tc.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_Tc.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_Tc.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_Tc.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_Tc.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_Tc.Items.Add(CT_SdtBlock.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.sdt);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_Tc.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_Tc.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_Tc.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.moveToRangeStart);
				}
				else if (childNode.LocalName == "p")
				{
					cT_Tc.Items.Add(CT_P.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.p);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_Tc.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.permEnd);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_Tc.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.proofErr);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_Tc.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.permStart);
				}
				else if (childNode.LocalName == "tbl")
				{
					cT_Tc.Items.Add(CT_Tbl.Parse(childNode, namespaceManager));
					cT_Tc.ItemsElementName.Add(ItemsChoiceTableCellType.tbl);
				}
			}
			return cT_Tc;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (tcPr != null)
			{
				tcPr.Write(sw, "tcPr");
			}
			foreach (object item in Items)
			{
				if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_AltChunk)
				{
					((CT_AltChunk)item).Write(sw, "altChunk");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
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
				else if (item is CT_SdtBlock)
				{
					((CT_SdtBlock)item).Write(sw, "sdt");
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
				else if (item is CT_P)
				{
					((CT_P)item).Write(sw, "p");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
				}
				else if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
				}
				else if (item is CT_Tbl)
				{
					((CT_Tbl)item).Write(sw, "tbl");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		private List<T> GetObjectList<T>(ItemsChoiceTableCellType type) where T : class
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

		private int SizeOfArray(ItemsChoiceTableCellType type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceTableCellType type) where T : class
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

		private int GetObjectIndex(ItemsChoiceTableCellType type, int p)
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

		private void RemoveObject(ItemsChoiceTableCellType type, int p)
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

		private void SetObject<T>(ItemsChoiceTableCellType type, int p, T obj) where T : class
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

		private T AddNewObject<T>(ItemsChoiceTableCellType type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		public CT_P AddNewP()
		{
			return AddNewObject<CT_P>(ItemsChoiceTableCellType.p);
		}

		public IList<CT_P> GetPList()
		{
			return GetObjectList<CT_P>(ItemsChoiceTableCellType.p);
		}

		public int SizeOfPArray()
		{
			return SizeOfArray(ItemsChoiceTableCellType.p);
		}

		public void SetPArray(int p, CT_P cT_P)
		{
			SetObject(ItemsChoiceTableCellType.p, p, cT_P);
		}

		public void RemoveP(int pos)
		{
			RemoveObject(ItemsChoiceTableCellType.p, pos);
		}

		public CT_P GetPArray(int p)
		{
			return GetObjectArray<CT_P>(p, ItemsChoiceTableCellType.p);
		}

		public IList<CT_Tbl> GetTblList()
		{
			return GetObjectList<CT_Tbl>(ItemsChoiceTableCellType.tbl);
		}

		public CT_Tbl GetTblArray(int p)
		{
			return GetObjectArray<CT_Tbl>(p, ItemsChoiceTableCellType.tbl);
		}

		public CT_TcPr AddNewTcPr()
		{
			tcPrField = new CT_TcPr();
			return tcPrField;
		}

		public bool IsSetTcPr()
		{
			return tcPrField != null;
		}
	}
}
