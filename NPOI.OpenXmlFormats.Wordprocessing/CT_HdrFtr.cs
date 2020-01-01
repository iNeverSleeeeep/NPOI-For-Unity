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
	public class CT_HdrFtr
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType8> itemsElementNameField;

		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtBlock), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("altChunk", typeof(CT_AltChunk), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("customXml", typeof(CT_CustomXmlBlock), Order = 0)]
		[XmlElement("p", typeof(CT_P), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("tbl", typeof(CT_Tbl), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
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
		public List<ItemsChoiceType8> ItemsElementName
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

		public CT_HdrFtr()
		{
			itemsElementNameField = new List<ItemsChoiceType8>();
			itemsField = new ArrayList();
		}

		public static CT_HdrFtr Parse(CT_HdrFtr ctObj, XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customXmlInsRangeStart")
				{
					ctObj.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					ctObj.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					ctObj.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					ctObj.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					ctObj.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.del);
				}
				else if (childNode.LocalName == "ins")
				{
					ctObj.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					ctObj.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					ctObj.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					ctObj.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					ctObj.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					ctObj.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					ctObj.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.moveToRangeStart);
				}
				else if (childNode.LocalName == "p")
				{
					ctObj.Items.Add(CT_P.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.p);
				}
				else if (childNode.LocalName == "permEnd")
				{
					ctObj.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					ctObj.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					ctObj.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.proofErr);
				}
				else if (childNode.LocalName == "sdt")
				{
					ctObj.Items.Add(CT_SdtBlock.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.sdt);
				}
				else if (childNode.LocalName == "tbl")
				{
					ctObj.Items.Add(CT_Tbl.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.tbl);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					ctObj.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "oMath")
				{
					ctObj.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					ctObj.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.oMathPara);
				}
				else if (childNode.LocalName == "altChunk")
				{
					ctObj.Items.Add(CT_AltChunk.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.altChunk);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					ctObj.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					ctObj.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					ctObj.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					ctObj.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					ctObj.Items.Add(CT_CustomXmlBlock.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					ctObj.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					ctObj.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					ctObj.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					ctObj.ItemsElementName.Add(ItemsChoiceType8.customXmlInsRangeEnd);
				}
			}
			return ctObj;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write(string.Format("<w:{0} ", nodeName));
			sw.Write("xmlns:ve=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
			sw.Write("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" ");
			sw.Write("xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:wp=\"http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing\" ");
			sw.Write("xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" ");
			sw.Write("xmlns:wne=\"http://schemas.microsoft.com/office/word/2006/wordml\"");
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_TrackChange)
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
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
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
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_Tbl GetTblArray(int i)
		{
			return GetObjectArray<CT_Tbl>(i, ItemsChoiceType8.tbl);
		}

		public IList<CT_Tbl> GetTblList()
		{
			return GetObjectList<CT_Tbl>(ItemsChoiceType8.tbl);
		}

		public CT_P AddNewP()
		{
			return AddNewObject<CT_P>(ItemsChoiceType8.p);
		}

		public void SetPArray(int i, CT_P cT_P)
		{
			SetObject(ItemsChoiceType8.p, i, cT_P);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType8 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType8 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType8 type) where T : class
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

		private T AddNewObject<T>(ItemsChoiceType8 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType8 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType8 type, int p)
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

		private void RemoveObject(ItemsChoiceType8 type, int p)
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
