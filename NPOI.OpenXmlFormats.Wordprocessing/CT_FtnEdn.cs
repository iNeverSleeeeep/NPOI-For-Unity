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
	[XmlRoot("endnote", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_FtnEdn
	{
		private ArrayList itemsField = new ArrayList();

		private List<ItemsChoiceType7> itemsElementNameField = new List<ItemsChoiceType7>();

		private ST_FtnEdn typeField;

		private bool typeFieldSpecified;

		private string idField = string.Empty;

		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("tbl", typeof(CT_Tbl), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtBlock), Order = 0)]
		[XmlElement("p", typeof(CT_P), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("altChunk", typeof(CT_AltChunk), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlBlock), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
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
		public List<ItemsChoiceType7> ItemsElementName
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
		public ST_FtnEdn type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		public static CT_FtnEdn Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FtnEdn cT_FtnEdn = new CT_FtnEdn();
			if (node.Attributes["w:type"] != null)
			{
				cT_FtnEdn.type = (ST_FtnEdn)Enum.Parse(typeof(ST_FtnEdn), node.Attributes["w:type"].Value);
			}
			cT_FtnEdn.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_FtnEdn.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_FtnEdn.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.moveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_FtnEdn.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_FtnEdn.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.ins);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_FtnEdn.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_FtnEdn.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_FtnEdn.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_FtnEdn.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_FtnEdn.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.moveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_FtnEdn.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_FtnEdn.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_FtnEdn.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_FtnEdn.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.del);
				}
				else if (childNode.LocalName == "p")
				{
					cT_FtnEdn.Items.Add(CT_P.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.p);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_FtnEdn.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_FtnEdn.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_FtnEdn.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.proofErr);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_FtnEdn.Items.Add(CT_SdtBlock.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.sdt);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_FtnEdn.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_FtnEdn.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "tbl")
				{
					cT_FtnEdn.Items.Add(CT_Tbl.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.tbl);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_FtnEdn.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.moveFromRangeStart);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_FtnEdn.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_FtnEdn.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.oMathPara);
				}
				else if (childNode.LocalName == "altChunk")
				{
					cT_FtnEdn.Items.Add(CT_AltChunk.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.altChunk);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_FtnEdn.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_FtnEdn.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_FtnEdn.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_FtnEdn.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_FtnEdn.Items.Add(CT_CustomXmlBlock.Parse(childNode, namespaceManager));
					cT_FtnEdn.ItemsElementName.Add(ItemsChoiceType7.customXml);
				}
			}
			return cT_FtnEdn;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			XmlHelper.WriteAttribute(sw, "w:id", id);
			sw.Write(">");
			int num = 0;
			foreach (object item in Items)
			{
				if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType7.customXmlDelRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType7.moveToRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveToRangeEnd");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType7.customXmlMoveFromRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType7.ins)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType7.customXmlDelRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType7.customXmlInsRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType7.customXmlInsRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType7.moveTo)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == ItemsChoiceType7.moveToRangeStart)
				{
					((CT_MoveBookmark)item).Write(sw, "moveToRangeStart");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType7.customXmlMoveFromRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_Markup && itemsElementNameField[num] == ItemsChoiceType7.customXmlMoveToRangeEnd)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_TrackChange && itemsElementNameField[num] == ItemsChoiceType7.customXmlMoveToRangeStart)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType7.del)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
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
				else if (item is CT_RunTrackChange && itemsElementNameField[num] == ItemsChoiceType7.moveFrom)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType7.moveFromRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_Tbl)
				{
					((CT_Tbl)item).Write(sw, "tbl");
				}
				else if (item is CT_MoveBookmark && itemsElementNameField[num] == ItemsChoiceType7.moveFromRangeStart)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
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
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType7.bookmarkEnd)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_Bookmark && itemsElementNameField[num] == ItemsChoiceType7.bookmarkStart)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType7.commentRangeEnd)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_MarkupRange && itemsElementNameField[num] == ItemsChoiceType7.commentRangeStart)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_CustomXmlBlock)
				{
					((CT_CustomXmlBlock)item).Write(sw, "customXml");
				}
				num++;
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public void Set(CT_FtnEdn note)
		{
			idField = note.idField;
			itemsElementNameField = note.itemsElementNameField;
			itemsField = note.itemsField;
			typeField = note.typeField;
		}

		private List<T> GetObjectList<T>(ItemsChoiceType7 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType7 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType7 type) where T : class
		{
			lock (this)
			{
				int num = 0;
				for (int i = 0; i < itemsElementNameField.Count; i++)
				{
					if (itemsElementNameField[i] == type)
					{
						if (num == p)
						{
							return itemsField[i] as T;
						}
						num++;
					}
				}
				return null;
			}
		}

		private T AddNewObject<T>(ItemsChoiceType7 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private T AddNewObject<T>(ItemsChoiceType7 type, T t) where T : class, new()
		{
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(t);
				return t;
			}
		}

		public IList<CT_P> GetPList()
		{
			return GetObjectList<CT_P>(ItemsChoiceType7.p);
		}

		public IList<CT_Tbl> GetTblList()
		{
			return GetObjectList<CT_Tbl>(ItemsChoiceType7.tbl);
		}

		public CT_Tbl GetTblArray(int i)
		{
			return GetObjectArray<CT_Tbl>(i, ItemsChoiceType7.tbl);
		}

		public CT_Tbl AddNewTbl()
		{
			return AddNewObject<CT_Tbl>(ItemsChoiceType7.tbl);
		}

		public CT_P AddNewP()
		{
			return AddNewObject<CT_P>(ItemsChoiceType7.p);
		}

		public CT_P AddNewP(CT_P paragraph)
		{
			return AddNewObject(ItemsChoiceType7.p, paragraph);
		}
	}
}
