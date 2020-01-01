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
	public class CT_SdtContentBlock
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType19> itemsElementNameField;

		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
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
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("p", typeof(CT_P), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtBlock), Order = 0)]
		[XmlElement("tbl", typeof(CT_Tbl), Order = 0)]
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
		public List<ItemsChoiceType19> ItemsElementName
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

		public CT_SdtContentBlock()
		{
			itemsElementNameField = new List<ItemsChoiceType19>();
			itemsField = new ArrayList();
		}

		public static CT_SdtContentBlock Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtContentBlock cT_SdtContentBlock = new CT_SdtContentBlock();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "bookmarkStart")
				{
					cT_SdtContentBlock.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.bookmarkStart);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_SdtContentBlock.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_SdtContentBlock.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.bookmarkEnd);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_SdtContentBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_SdtContentBlock.Items.Add(CT_CustomXmlBlock.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_SdtContentBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_SdtContentBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_SdtContentBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_SdtContentBlock.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_SdtContentBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_SdtContentBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_SdtContentBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_SdtContentBlock.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_SdtContentBlock.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_SdtContentBlock.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.moveToRangeStart);
				}
				else if (childNode.LocalName == "p")
				{
					cT_SdtContentBlock.Items.Add(CT_P.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.p);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_SdtContentBlock.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_SdtContentBlock.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_SdtContentBlock.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.proofErr);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_SdtContentBlock.Items.Add(CT_SdtBlock.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.sdt);
				}
				else if (childNode.LocalName == "tbl")
				{
					cT_SdtContentBlock.Items.Add(CT_Tbl.Parse(childNode, namespaceManager));
					cT_SdtContentBlock.ItemsElementName.Add(ItemsChoiceType19.tbl);
				}
			}
			return cT_SdtContentBlock;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
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
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
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
				else if (item is CT_P)
				{
					((CT_P)item).Write(sw, "p");
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
				else if (item is CT_SdtBlock)
				{
					((CT_SdtBlock)item).Write(sw, "sdt");
				}
				else if (item is CT_Tbl)
				{
					((CT_Tbl)item).Write(sw, "tbl");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_P AddNewP()
		{
			return AddNewObject<CT_P>(ItemsChoiceType19.p);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType19 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType19 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType19 type) where T : class
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

		private T InsertNewObject<T>(ItemsChoiceType19 type, int p) where T : class, new()
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

		private T AddNewObject<T>(ItemsChoiceType19 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType19 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType19 type, int p)
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

		private void RemoveObject(ItemsChoiceType19 type, int p)
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
