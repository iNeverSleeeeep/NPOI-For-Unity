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
	[XmlType(TypeName = "CT_Hyperlink", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("CT_Hyperlink", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Hyperlink1
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType12> itemsElementNameField;

		private string tgtFrameField;

		private string tooltipField;

		private string docLocationField;

		private ST_OnOff historyField;

		private bool historyFieldSpecified;

		private string anchorField;

		private string idField;

		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlRun), Order = 0)]
		[XmlElement("subDoc", typeof(CT_Rel), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("smartTag", typeof(CT_SmartTagRun), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtRun), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("r", typeof(CT_R), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("fldSimple", typeof(CT_SimpleField), Order = 0)]
		[XmlElement("hyperlink", typeof(CT_Hyperlink1), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
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
		public List<ItemsChoiceType12> ItemsElementName
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
		public string tgtFrame
		{
			get
			{
				return tgtFrameField;
			}
			set
			{
				tgtFrameField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string tooltip
		{
			get
			{
				return tooltipField;
			}
			set
			{
				tooltipField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string docLocation
		{
			get
			{
				return docLocationField;
			}
			set
			{
				docLocationField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff history
		{
			get
			{
				return historyField;
			}
			set
			{
				historyField = value;
			}
		}

		[XmlIgnore]
		public bool historySpecified
		{
			get
			{
				return historyFieldSpecified;
			}
			set
			{
				historyFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string anchor
		{
			get
			{
				return anchorField;
			}
			set
			{
				anchorField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
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

		public CT_Hyperlink1()
		{
			itemsElementNameField = new List<ItemsChoiceType12>();
			itemsField = new ArrayList();
		}

		public static CT_Hyperlink1 Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Hyperlink1 cT_Hyperlink = new CT_Hyperlink1();
			cT_Hyperlink.tgtFrame = XmlHelper.ReadString(node.Attributes["w:tgtFrame"]);
			cT_Hyperlink.tooltip = XmlHelper.ReadString(node.Attributes["w:tooltip"]);
			cT_Hyperlink.docLocation = XmlHelper.ReadString(node.Attributes["w:docLocation"]);
			if (node.Attributes["w:history"] != null)
			{
				cT_Hyperlink.history = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:history"].Value);
			}
			cT_Hyperlink.anchor = XmlHelper.ReadString(node.Attributes["w:anchor"]);
			cT_Hyperlink.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "bookmarkStart")
				{
					cT_Hyperlink.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.bookmarkStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_Hyperlink.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_Hyperlink.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_Hyperlink.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.del);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_Hyperlink.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.moveToRangeStart);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_Hyperlink.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_Hyperlink.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_Hyperlink.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.bookmarkEnd);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_Hyperlink.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_Hyperlink.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_Hyperlink.Items.Add(CT_CustomXmlRun.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXml);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_Hyperlink.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_Hyperlink.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_Hyperlink.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_Hyperlink.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_Hyperlink.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_Hyperlink.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "hyperlink")
				{
					cT_Hyperlink.Items.Add(Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.hyperlink);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_Hyperlink.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_Hyperlink.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_Hyperlink.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_Hyperlink.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_Hyperlink.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_Hyperlink.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.moveToRangeEnd);
				}
				else if (childNode.LocalName == "fldSimple")
				{
					cT_Hyperlink.Items.Add(CT_SimpleField.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.fldSimple);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_Hyperlink.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_Hyperlink.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_Hyperlink.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.proofErr);
				}
				else if (childNode.LocalName == "r")
				{
					cT_Hyperlink.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.r);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_Hyperlink.Items.Add(CT_SdtRun.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.sdt);
				}
				else if (childNode.LocalName == "smartTag")
				{
					cT_Hyperlink.Items.Add(CT_SmartTagRun.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.smartTag);
				}
				else if (childNode.LocalName == "subDoc")
				{
					cT_Hyperlink.Items.Add(CT_Rel.Parse(childNode, namespaceManager));
					cT_Hyperlink.ItemsElementName.Add(ItemsChoiceType12.subDoc);
				}
			}
			return cT_Hyperlink;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:tgtFrame", tgtFrame);
			XmlHelper.WriteAttribute(sw, "w:tooltip", tooltip);
			XmlHelper.WriteAttribute(sw, "w:docLocation", docLocation);
			XmlHelper.WriteAttribute(sw, "w:history", history.ToString());
			XmlHelper.WriteAttribute(sw, "w:anchor", anchor);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveToRangeStart");
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
				else if (item is CT_CustomXmlRun)
				{
					((CT_CustomXmlRun)item).Write(sw, "customXml");
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
				else if (item is CT_SimpleField)
				{
					((CT_SimpleField)item).Write(sw, "fldSimple");
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
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public IEnumerable<CT_R> GetRList()
		{
			return GetObjectList<CT_R>(ItemsChoiceType12.r);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType12 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType12 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType12 type) where T : class
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

		private T InsertNewObject<T>(ItemsChoiceType12 type, int p) where T : class, new()
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

		private T AddNewObject<T>(ItemsChoiceType12 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType12 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType12 type, int p)
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

		private void RemoveObject(ItemsChoiceType12 type, int p)
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
