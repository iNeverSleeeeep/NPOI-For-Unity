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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_P
	{
		private CT_PPr pPrField;

		private ArrayList itemsField;

		private List<ParagraphItemsChoiceType> itemsElementNameField;

		private byte[] rsidRPrField;

		private byte[] rsidRField;

		private byte[] rsidDelField;

		private byte[] rsidPField;

		private byte[] rsidRDefaultField;

		[XmlElement(Order = 0)]
		public CT_PPr pPr
		{
			get
			{
				return pPrField;
			}
			set
			{
				pPrField = value;
			}
		}

		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("fldSimple", typeof(CT_SimpleField), Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 1)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 1)]
		[XmlElement("r", typeof(CT_R), Order = 1)]
		[XmlElement("sdt", typeof(CT_SdtRun), Order = 1)]
		[XmlElement("smartTag", typeof(CT_SmartTagRun), Order = 1)]
		[XmlElement("subDoc", typeof(CT_Rel), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("hyperlink", typeof(CT_Hyperlink1), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlRun), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
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
		public List<ParagraphItemsChoiceType> ItemsElementName
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
		public byte[] rsidP
		{
			get
			{
				return rsidPField;
			}
			set
			{
				rsidPField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] rsidRDefault
		{
			get
			{
				return rsidRDefaultField;
			}
			set
			{
				rsidRDefaultField = value;
			}
		}

		public CT_P()
		{
			itemsElementNameField = new List<ParagraphItemsChoiceType>();
			itemsField = new ArrayList();
			pPrField = new CT_PPr();
		}

		public static CT_P Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_P cT_P = new CT_P();
			cT_P.rsidRPr = XmlHelper.ReadBytes(node.Attributes["w:rsidRPr"]);
			cT_P.rsidR = XmlHelper.ReadBytes(node.Attributes["w:rsidR"]);
			cT_P.rsidDel = XmlHelper.ReadBytes(node.Attributes["w:rsidDel"]);
			cT_P.rsidP = XmlHelper.ReadBytes(node.Attributes["w:rsidP"]);
			cT_P.rsidRDefault = XmlHelper.ReadBytes(node.Attributes["w:rsidRDefault"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pPr")
				{
					cT_P.pPr = CT_PPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_P.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.bookmarkEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_P.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_P.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.moveTo);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_P.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.oMathPara);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_P.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.oMath);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_P.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_P.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_P.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_P.Items.Add(CT_CustomXmlRun.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_P.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_P.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_P.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_P.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_P.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_P.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_P.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_P.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_P.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.del);
				}
				else if (childNode.LocalName == "fldSimple")
				{
					cT_P.Items.Add(CT_SimpleField.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.fldSimple);
				}
				else if (childNode.LocalName == "hyperlink")
				{
					cT_P.Items.Add(CT_Hyperlink1.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.hyperlink);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_P.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_P.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_P.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_P.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_P.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_P.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_P.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_P.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.proofErr);
				}
				else if (childNode.LocalName == "r")
				{
					cT_P.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.r);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_P.Items.Add(CT_SdtRun.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.sdt);
				}
				else if (childNode.LocalName == "smartTag")
				{
					cT_P.Items.Add(CT_SmartTagRun.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.smartTag);
				}
				else if (childNode.LocalName == "subDoc")
				{
					cT_P.Items.Add(CT_Rel.Parse(childNode, namespaceManager));
					cT_P.ItemsElementName.Add(ParagraphItemsChoiceType.subDoc);
				}
			}
			return cT_P;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:rsidR", rsidR);
			XmlHelper.WriteAttribute(sw, "w:rsidRPr", rsidRPr);
			XmlHelper.WriteAttribute(sw, "w:rsidRDefault", rsidRDefault);
			XmlHelper.WriteAttribute(sw, "w:rsidP", rsidP);
			XmlHelper.WriteAttribute(sw, "w:rsidDel", rsidDel);
			sw.Write(">");
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			int num = 0;
			foreach (object item in Items)
			{
				if (item is CT_MarkupRange && itemsElementNameField[num] == ParagraphItemsChoiceType.bookmarkEnd)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == ParagraphItemsChoiceType.moveFromRangeStart)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.moveTo)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_Bookmark && itemsElementNameField[num] == ParagraphItemsChoiceType.bookmarkStart)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ParagraphItemsChoiceType.commentRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ParagraphItemsChoiceType.commentRangeStart)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_CustomXmlRun)
				{
					((CT_CustomXmlRun)item).Write(sw, "customXml");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlDelRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlDelRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlInsRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlInsRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlMoveFromRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlMoveFromRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlMoveToRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.customXmlMoveToRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.del)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_SimpleField)
				{
					((CT_SimpleField)item).Write(sw, "fldSimple");
				}
				else if (item is CT_Hyperlink1)
				{
					((CT_Hyperlink1)item).Write(sw, "hyperlink");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.ins)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ParagraphItemsChoiceType.moveFrom)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ParagraphItemsChoiceType.moveFromRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ParagraphItemsChoiceType.moveToRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveToRangeEnd");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == ParagraphItemsChoiceType.moveToRangeStart)
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
				else if (item is CT_R)
				{
					((CT_R)item).Write(sw, "r");
				}
				else if (item is CT_SdtRun)
				{
					((CT_SdtRun)item).Write(sw, "sdt");
				}
				else if (item is CT_SmartTagRun)
				{
					((CT_SmartTagRun)item).Write(sw, "smartTag");
				}
				else if (item is CT_Rel)
				{
					((CT_Rel)item).Write(sw, "subDoc");
				}
				num++;
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_R AddNewR()
		{
			CT_R cT_R = new CT_R();
			lock (this)
			{
				itemsField.Add(cT_R);
				itemsElementNameField.Add(ParagraphItemsChoiceType.r);
				return cT_R;
			}
		}

		public CT_PPr AddNewPPr()
		{
			if (pPrField == null)
			{
				pPrField = new CT_PPr();
			}
			return pPrField;
		}

		public void SetRArray(int pos, CT_R Run)
		{
			SetObject(ParagraphItemsChoiceType.r, pos, Run);
		}

		public CT_R InsertNewR(int pos)
		{
			return InsertNewObject<CT_R>(ParagraphItemsChoiceType.r, pos);
		}

		public int SizeOfRArray()
		{
			return SizeOfArray(ParagraphItemsChoiceType.r);
		}

		public void RemoveR(int pos)
		{
			RemoveObject(ParagraphItemsChoiceType.r, pos);
		}

		public IEnumerable<CT_MarkupRange> GetCommentRangeStartList()
		{
			return GetObjectList<CT_MarkupRange>(ParagraphItemsChoiceType.commentRangeStart);
		}

		public IEnumerable<CT_Hyperlink1> GetHyperlinkList()
		{
			return GetObjectList<CT_Hyperlink1>(ParagraphItemsChoiceType.hyperlink);
		}

		private List<T> GetObjectList<T>(ParagraphItemsChoiceType type) where T : class
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

		private int SizeOfArray(ParagraphItemsChoiceType type)
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

		private T GetObjectArray<T>(int p, ParagraphItemsChoiceType type) where T : class
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

		private T InsertNewObject<T>(ParagraphItemsChoiceType type, int p) where T : class, new()
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

		private T AddNewObject<T>(ParagraphItemsChoiceType type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ParagraphItemsChoiceType type, int p, T obj) where T : class
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

		private int GetObjectIndex(ParagraphItemsChoiceType type, int p)
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

		private void RemoveObject(ParagraphItemsChoiceType type, int p)
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

		public IList<CT_R> GetRList()
		{
			return GetObjectList<CT_R>(ParagraphItemsChoiceType.r);
		}

		public int SizeOfBookmarkStartArray()
		{
			return SizeOfArray(ParagraphItemsChoiceType.bookmarkStart);
		}

		public int SizeOfBookmarkEndArray()
		{
			return SizeOfArray(ParagraphItemsChoiceType.bookmarkEnd);
		}

		public CT_Bookmark GetBookmarkStartArray(int p)
		{
			return GetObjectArray<CT_Bookmark>(p, ParagraphItemsChoiceType.bookmarkStart);
		}

		public IEnumerable<CT_Bookmark> GetBookmarkStartList()
		{
			return GetObjectList<CT_Bookmark>(ParagraphItemsChoiceType.bookmarkStart);
		}
	}
}
