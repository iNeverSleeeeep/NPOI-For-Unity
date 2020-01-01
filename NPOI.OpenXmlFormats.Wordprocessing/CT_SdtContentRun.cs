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
	public class CT_SdtContentRun
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType18> itemsElementNameField;

		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlRun), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("fldSimple", typeof(CT_SimpleField), Order = 0)]
		[XmlElement("hyperlink", typeof(CT_Hyperlink1), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("r", typeof(CT_R), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtRun), Order = 0)]
		[XmlElement("smartTag", typeof(CT_SmartTagRun), Order = 0)]
		[XmlElement("subDoc", typeof(CT_Rel), Order = 0)]
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
		public List<ItemsChoiceType18> ItemsElementName
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

		public CT_SdtContentRun()
		{
			itemsElementNameField = new List<ItemsChoiceType18>();
			itemsField = new ArrayList();
		}

		public static CT_SdtContentRun Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtContentRun cT_SdtContentRun = new CT_SdtContentRun();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "permStart")
				{
					cT_SdtContentRun.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.permStart);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_SdtContentRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_SdtContentRun.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.moveToRangeStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_SdtContentRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_SdtContentRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_SdtContentRun.Items.Add(CT_CustomXmlRun.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_SdtContentRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_SdtContentRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_SdtContentRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_SdtContentRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_SdtContentRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_SdtContentRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_SdtContentRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_SdtContentRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_SdtContentRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.del);
				}
				else if (childNode.LocalName == "fldSimple")
				{
					cT_SdtContentRun.Items.Add(CT_SimpleField.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.fldSimple);
				}
				else if (childNode.LocalName == "hyperlink")
				{
					cT_SdtContentRun.Items.Add(CT_Hyperlink1.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.hyperlink);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_SdtContentRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_SdtContentRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.moveFrom);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_SdtContentRun.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.bookmarkStart);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_SdtContentRun.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_SdtContentRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.moveTo);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_SdtContentRun.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.permEnd);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_SdtContentRun.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.oMath);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_SdtContentRun.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.proofErr);
				}
				else if (childNode.LocalName == "r")
				{
					cT_SdtContentRun.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.r);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_SdtContentRun.Items.Add(CT_SdtRun.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.sdt);
				}
				else if (childNode.LocalName == "smartTag")
				{
					cT_SdtContentRun.Items.Add(CT_SmartTagRun.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.smartTag);
				}
				else if (childNode.LocalName == "subDoc")
				{
					cT_SdtContentRun.Items.Add(CT_Rel.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.subDoc);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_SdtContentRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_SdtContentRun.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_SdtContentRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SdtContentRun.ItemsElementName.Add(ItemsChoiceType18.bookmarkEnd);
				}
			}
			return cT_SdtContentRun;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
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
				else if (item is CT_SimpleField)
				{
					((CT_SimpleField)item).Write(sw, "fldSimple");
				}
				else if (item is CT_Hyperlink1)
				{
					((CT_Hyperlink1)item).Write(sw, "hyperlink");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
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
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public IEnumerable<CT_R> GetRList()
		{
			return GetObjectList<CT_R>(ItemsChoiceType18.r);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType18 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType18 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType18 type) where T : class
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

		private T InsertNewObject<T>(ItemsChoiceType18 type, int p) where T : class, new()
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

		private T AddNewObject<T>(ItemsChoiceType18 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType18 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType18 type, int p)
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

		private void RemoveObject(ItemsChoiceType18 type, int p)
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
