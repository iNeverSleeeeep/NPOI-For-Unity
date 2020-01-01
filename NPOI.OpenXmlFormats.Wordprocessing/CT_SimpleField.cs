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
	public class CT_SimpleField
	{
		private CT_Text fldDataField;

		private ArrayList itemsField;

		private List<ItemsChoiceType13> itemsElementNameField;

		private string instrField;

		private ST_OnOff fldLockField;

		private bool fldLockFieldSpecified;

		private ST_OnOff dirtyField;

		private bool dirtyFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_Text fldData
		{
			get
			{
				return fldDataField;
			}
			set
			{
				fldDataField = value;
			}
		}

		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("subDoc", typeof(CT_Rel), Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("fldSimple", typeof(CT_SimpleField), Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 1)]
		[XmlElement("sdt", typeof(CT_SdtRun), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("hyperlink", typeof(CT_Hyperlink1), Order = 1)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("smartTag", typeof(CT_SmartTagRun), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlRun), Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 1)]
		[XmlElement("r", typeof(CT_R), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 1)]
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
		public List<ItemsChoiceType13> ItemsElementName
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
		public string instr
		{
			get
			{
				return instrField;
			}
			set
			{
				instrField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff fldLock
		{
			get
			{
				return fldLockField;
			}
			set
			{
				fldLockField = value;
			}
		}

		[XmlIgnore]
		public bool fldLockSpecified
		{
			get
			{
				return fldLockFieldSpecified;
			}
			set
			{
				fldLockFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff dirty
		{
			get
			{
				return dirtyField;
			}
			set
			{
				dirtyField = value;
			}
		}

		[XmlIgnore]
		public bool dirtySpecified
		{
			get
			{
				return dirtyFieldSpecified;
			}
			set
			{
				dirtyFieldSpecified = value;
			}
		}

		public CT_SimpleField()
		{
			itemsElementNameField = new List<ItemsChoiceType13>();
			itemsField = new ArrayList();
		}

		public static CT_SimpleField Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SimpleField cT_SimpleField = new CT_SimpleField();
			cT_SimpleField.instr = XmlHelper.ReadString(node.Attributes["w:instr"]);
			if (node.Attributes["w:fldLock"] != null)
			{
				cT_SimpleField.fldLock = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:fldLock"].Value);
			}
			if (node.Attributes["w:dirty"] != null)
			{
				cT_SimpleField.dirty = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:dirty"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "subDoc")
				{
					cT_SimpleField.Items.Add(CT_Rel.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.subDoc);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_SimpleField.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_SimpleField.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.moveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_SimpleField.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_SimpleField.Items.Add(CT_SdtRun.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.sdt);
				}
				else if (childNode.LocalName == "smartTag")
				{
					cT_SimpleField.Items.Add(CT_SmartTagRun.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.smartTag);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_SimpleField.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_SimpleField.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "del")
				{
					cT_SimpleField.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.del);
				}
				else if (childNode.LocalName == "fldSimple")
				{
					cT_SimpleField.Items.Add(Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.fldSimple);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_SimpleField.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_SimpleField.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_SimpleField.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_SimpleField.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.moveToRangeStart);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_SimpleField.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_SimpleField.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_SimpleField.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_SimpleField.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.proofErr);
				}
				else if (childNode.LocalName == "r")
				{
					cT_SimpleField.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.r);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_SimpleField.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.ins);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_SimpleField.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "hyperlink")
				{
					cT_SimpleField.Items.Add(CT_Hyperlink1.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.hyperlink);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_SimpleField.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_SimpleField.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_SimpleField.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_SimpleField.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_SimpleField.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_SimpleField.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_SimpleField.Items.Add(CT_CustomXmlRun.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_SimpleField.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_SimpleField.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_SimpleField.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SimpleField.ItemsElementName.Add(ItemsChoiceType13.customXmlInsRangeEnd);
				}
			}
			return cT_SimpleField;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:instr", instr);
			XmlHelper.WriteAttribute(sw, "w:fldLock", fldLock.ToString());
			XmlHelper.WriteAttribute(sw, "w:dirty", dirty.ToString());
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_Rel)
				{
					((CT_Rel)item).Write(sw, "subDoc");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_SdtRun)
				{
					((CT_SdtRun)item).Write(sw, "sdt");
				}
				else if (item is CT_SmartTagRun)
				{
					((CT_SmartTagRun)item).Write(sw, "smartTag");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_SimpleField)
				{
					((CT_SimpleField)item).Write(sw, "fldSimple");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
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
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
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
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_Hyperlink1)
				{
					((CT_Hyperlink1)item).Write(sw, "hyperlink");
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
				else if (item is CT_CustomXmlRun)
				{
					((CT_CustomXmlRun)item).Write(sw, "customXml");
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

		public IEnumerable<CT_R> GetRList()
		{
			return GetObjectList<CT_R>(ItemsChoiceType13.r);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType13 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType13 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType13 type) where T : class
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

		private T InsertNewObject<T>(ItemsChoiceType13 type, int p) where T : class, new()
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

		private T AddNewObject<T>(ItemsChoiceType13 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType13 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType13 type, int p)
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

		private void RemoveObject(ItemsChoiceType13 type, int p)
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
