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
	public class CT_Body
	{
		private ArrayList itemsField;

		private List<DocumentBodyItemChoiceType> itemsElementNameField;

		private CT_SectPr sectPrField;

		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("ins", Type = typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlBlock), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("del", Type = typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtBlock), Order = 0)]
		[XmlElement("tbl", typeof(CT_Tbl), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("p", typeof(CT_P), Order = 0)]
		[XmlElement("altChunk", typeof(CT_AltChunk), Order = 0)]
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
		public List<DocumentBodyItemChoiceType> ItemsElementName
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
		public CT_SectPr sectPr
		{
			get
			{
				return sectPrField;
			}
			set
			{
				sectPrField = value;
			}
		}

		public CT_Body()
		{
			itemsElementNameField = new List<DocumentBodyItemChoiceType>();
			itemsField = new ArrayList();
		}

		public static CT_Body Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Body cT_Body = new CT_Body();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "moveTo")
				{
					cT_Body.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.moveTo);
				}
				else if (childNode.LocalName == "sectPr")
				{
					cT_Body.sectPr = CT_SectPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_Body.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.oMathPara);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_Body.Items.Add(CT_CustomXmlBlock.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXml);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_Body.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.oMath);
				}
				else if (childNode.LocalName == "altChunk")
				{
					cT_Body.Items.Add(CT_AltChunk.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.altChunk);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_Body.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_Body.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_Body.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_Body.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.commentRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_Body.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_Body.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_Body.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_Body.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_Body.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_Body.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_Body.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_Body.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_Body.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_Body.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_Body.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_Body.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_Body.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_Body.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_Body.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.moveToRangeStart);
				}
				else if (childNode.LocalName == "p")
				{
					cT_Body.Items.Add(CT_P.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.p);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_Body.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_Body.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_Body.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.proofErr);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_Body.Items.Add(CT_SdtBlock.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.sdt);
				}
				else if (childNode.LocalName == "tbl")
				{
					cT_Body.Items.Add(CT_Tbl.Parse(childNode, namespaceManager));
					cT_Body.ItemsElementName.Add(DocumentBodyItemChoiceType.tbl);
				}
			}
			return cT_Body;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			int num = 0;
			foreach (object item in Items)
			{
				if (item is CT_RunTrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.moveTo)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_CustomXmlBlock)
				{
					((CT_CustomXmlBlock)item).Write(sw, "customXml");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_AltChunk)
				{
					((CT_AltChunk)item).Write(sw, "altChunk");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == DocumentBodyItemChoiceType.bookmarkEnd)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_Bookmark && itemsElementNameField[num] == DocumentBodyItemChoiceType.bookmarkStart)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == DocumentBodyItemChoiceType.commentRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == DocumentBodyItemChoiceType.commentRangeStart)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlDelRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlDelRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlInsRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlInsRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlMoveFromRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlMoveFromRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlMoveToRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.customXmlMoveToRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.del)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.ins)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == DocumentBodyItemChoiceType.moveFrom)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == DocumentBodyItemChoiceType.moveFromRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == DocumentBodyItemChoiceType.moveFromRangeStart)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == DocumentBodyItemChoiceType.moveToRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveToRangeEnd");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == DocumentBodyItemChoiceType.moveToRangeStart)
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
				num++;
			}
			if (sectPr != null)
			{
				sectPr.Write(sw, "sectPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_P AddNewP()
		{
			CT_P cT_P = new CT_P();
			lock (this)
			{
				itemsField.Add(cT_P);
				itemsElementNameField.Add(DocumentBodyItemChoiceType.p);
				return cT_P;
			}
		}

		public CT_Tbl AddNewTbl()
		{
			CT_Tbl cT_Tbl = new CT_Tbl();
			lock (this)
			{
				itemsField.Add(cT_Tbl);
				itemsElementNameField.Add(DocumentBodyItemChoiceType.tbl);
				return cT_Tbl;
			}
		}

		public int sizeOfTblArray()
		{
			return SizeOfArray(DocumentBodyItemChoiceType.tbl);
		}

		public List<CT_Tbl> getTblArray()
		{
			return GetObjectList<CT_Tbl>(DocumentBodyItemChoiceType.tbl);
		}

		public CT_Tbl insertNewTbl(int paramInt)
		{
			throw new NotImplementedException();
		}

		public void removeTbl(int paramInt)
		{
			throw new NotImplementedException();
		}

		public CT_Tbl GetTblArray(int i)
		{
			return GetObjectArray<CT_Tbl>(i, DocumentBodyItemChoiceType.tbl);
		}

		public void SetTblArray(int pos, CT_Tbl cT_Tbl)
		{
			SetObject(DocumentBodyItemChoiceType.tbl, pos, cT_Tbl);
		}

		public CT_Tbl[] GetTblArray()
		{
			return GetObjectList<CT_Tbl>(DocumentBodyItemChoiceType.tbl).ToArray();
		}

		public CT_P GetPArray(int p)
		{
			return GetObjectArray<CT_P>(p, DocumentBodyItemChoiceType.p);
		}

		public void RemoveP(int paraPos)
		{
			RemoveObject(DocumentBodyItemChoiceType.p, paraPos);
		}

		public void RemoveTbl(int tablePos)
		{
			RemoveObject(DocumentBodyItemChoiceType.tbl, tablePos);
		}

		public CT_SdtBlock AddNewSdt()
		{
			return AddNewObject<CT_SdtBlock>(DocumentBodyItemChoiceType.sdt);
		}

		private List<T> GetObjectList<T>(DocumentBodyItemChoiceType type) where T : class
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

		private int SizeOfArray(DocumentBodyItemChoiceType type)
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

		private T GetObjectArray<T>(int p, DocumentBodyItemChoiceType type) where T : class
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

		private T AddNewObject<T>(DocumentBodyItemChoiceType type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(DocumentBodyItemChoiceType type, int p, T obj) where T : class
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

		private int GetObjectIndex(DocumentBodyItemChoiceType type, int p)
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

		private void RemoveObject(DocumentBodyItemChoiceType type, int p)
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

		public void SetPArray(int pos, CT_P cT_P)
		{
			SetObject(DocumentBodyItemChoiceType.p, pos, cT_P);
		}
	}
}
