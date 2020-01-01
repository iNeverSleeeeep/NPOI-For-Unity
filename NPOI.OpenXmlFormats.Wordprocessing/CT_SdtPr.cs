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
	public class CT_SdtPr
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType17> itemsElementNameField;

		[XmlElement("showingPlcHdr", typeof(CT_OnOff), Order = 0)]
		[XmlElement("group", typeof(CT_Empty), Order = 0)]
		[XmlElement("alias", typeof(CT_String), Order = 0)]
		[XmlElement("bibliography", typeof(CT_Empty), Order = 0)]
		[XmlElement("citation", typeof(CT_Empty), Order = 0)]
		[XmlElement("comboBox", typeof(CT_SdtComboBox), Order = 0)]
		[XmlElement("dataBinding", typeof(CT_DataBinding), Order = 0)]
		[XmlElement("date", typeof(CT_SdtDate), Order = 0)]
		[XmlElement("docPartList", typeof(CT_SdtDocPart), Order = 0)]
		[XmlElement("docPartObj", typeof(CT_SdtDocPart), Order = 0)]
		[XmlElement("dropDownList", typeof(CT_SdtDropDownList), Order = 0)]
		[XmlElement("equation", typeof(CT_Empty), Order = 0)]
		[XmlElement("richText", typeof(CT_Empty), Order = 0)]
		[XmlElement("id", typeof(CT_DecimalNumber), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("picture", typeof(CT_Empty), Order = 0)]
		[XmlElement("placeholder", typeof(CT_Placeholder), Order = 0)]
		[XmlElement("rPr", typeof(CT_RPr), Order = 0)]
		[XmlElement("lock", typeof(CT_Lock), Order = 0)]
		[XmlElement("tag", typeof(CT_String), Order = 0)]
		[XmlElement("temporary", typeof(CT_OnOff), Order = 0)]
		[XmlElement("text", typeof(CT_SdtText), Order = 0)]
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
		public List<ItemsChoiceType17> ItemsElementName
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

		public CT_SdtPr()
		{
			itemsElementNameField = new List<ItemsChoiceType17>();
			itemsField = new ArrayList();
		}

		public static CT_SdtPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtPr cT_SdtPr = new CT_SdtPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "richText")
				{
					cT_SdtPr.Items.Add(new CT_Empty());
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.richText);
				}
				else if (childNode.LocalName == "docPartList")
				{
					cT_SdtPr.Items.Add(CT_SdtDocPart.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.docPartList);
				}
				else if (childNode.LocalName == "docPartObj")
				{
					cT_SdtPr.Items.Add(CT_SdtDocPart.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.docPartObj);
				}
				else if (childNode.LocalName == "dropDownList")
				{
					cT_SdtPr.Items.Add(CT_SdtDropDownList.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.dropDownList);
				}
				else if (childNode.LocalName == "equation")
				{
					cT_SdtPr.Items.Add(new CT_Empty());
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.equation);
				}
				else if (childNode.LocalName == "group")
				{
					cT_SdtPr.Items.Add(new CT_Empty());
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.group);
				}
				else if (childNode.LocalName == "id")
				{
					cT_SdtPr.Items.Add(CT_DecimalNumber.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.id);
				}
				else if (childNode.LocalName == "lock")
				{
					cT_SdtPr.Items.Add(CT_Lock.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.@lock);
				}
				else if (childNode.LocalName == "date")
				{
					cT_SdtPr.Items.Add(CT_SdtDate.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.date);
				}
				else if (childNode.LocalName == "placeholder")
				{
					cT_SdtPr.Items.Add(CT_Placeholder.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.placeholder);
				}
				else if (childNode.LocalName == "rPr")
				{
					cT_SdtPr.Items.Add(CT_RPr.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.rPr);
				}
				else if (childNode.LocalName == "showingPlcHdr")
				{
					cT_SdtPr.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.showingPlcHdr);
				}
				else if (childNode.LocalName == "tag")
				{
					cT_SdtPr.Items.Add(CT_String.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.tag);
				}
				else if (childNode.LocalName == "temporary")
				{
					cT_SdtPr.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.temporary);
				}
				else if (childNode.LocalName == "text")
				{
					cT_SdtPr.Items.Add(CT_SdtText.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.text);
				}
				else if (childNode.LocalName == "picture")
				{
					cT_SdtPr.Items.Add(new CT_Empty());
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.picture);
				}
				else if (childNode.LocalName == "alias")
				{
					cT_SdtPr.Items.Add(CT_String.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.alias);
				}
				else if (childNode.LocalName == "bibliography")
				{
					cT_SdtPr.Items.Add(new CT_Empty());
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.bibliography);
				}
				else if (childNode.LocalName == "citation")
				{
					cT_SdtPr.Items.Add(new CT_Empty());
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.citation);
				}
				else if (childNode.LocalName == "comboBox")
				{
					cT_SdtPr.Items.Add(CT_SdtComboBox.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.comboBox);
				}
				else if (childNode.LocalName == "dataBinding")
				{
					cT_SdtPr.Items.Add(CT_DataBinding.Parse(childNode, namespaceManager));
					cT_SdtPr.ItemsElementName.Add(ItemsChoiceType17.dataBinding);
				}
			}
			return cT_SdtPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			for (int i = 0; i < Items.Count; i++)
			{
				object obj = Items[i];
				ItemsChoiceType17 itemsChoiceType = itemsElementNameField[i];
				if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType17.richText)
				{
					sw.Write("<richText/>");
				}
				else if (obj is CT_SdtDocPart && itemsChoiceType == ItemsChoiceType17.docPartList)
				{
					((CT_SdtDocPart)obj).Write(sw, "docPartList");
				}
				else if (obj is CT_SdtDocPart && itemsChoiceType == ItemsChoiceType17.docPartObj)
				{
					((CT_SdtDocPart)obj).Write(sw, "docPartObj");
				}
				else if (obj is CT_SdtDropDownList)
				{
					((CT_SdtDropDownList)obj).Write(sw, "dropDownList");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType17.equation)
				{
					sw.Write("<equation/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType17.group)
				{
					sw.Write("<group/>");
				}
				else if (obj is CT_DecimalNumber && itemsChoiceType == ItemsChoiceType17.id)
				{
					((CT_DecimalNumber)obj).Write(sw, "id");
				}
				else if (obj is CT_Lock)
				{
					((CT_Lock)obj).Write(sw, "lock");
				}
				else if (obj is CT_SdtDate)
				{
					((CT_SdtDate)obj).Write(sw, "date");
				}
				else if (obj is CT_Placeholder)
				{
					((CT_Placeholder)obj).Write(sw, "placeholder");
				}
				else if (obj is CT_RPr)
				{
					((CT_RPr)obj).Write(sw, "rPr");
				}
				else if (obj is CT_OnOff && itemsChoiceType == ItemsChoiceType17.showingPlcHdr)
				{
					((CT_OnOff)obj).Write(sw, "showingPlcHdr");
				}
				else if (obj is CT_String && itemsChoiceType == ItemsChoiceType17.tag)
				{
					((CT_String)obj).Write(sw, "tag");
				}
				else if (obj is CT_OnOff && itemsChoiceType == ItemsChoiceType17.temporary)
				{
					((CT_OnOff)obj).Write(sw, "temporary");
				}
				else if (obj is CT_SdtText)
				{
					((CT_SdtText)obj).Write(sw, "text");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType17.picture)
				{
					sw.Write("<picture/>");
				}
				else if (obj is CT_String && itemsChoiceType == ItemsChoiceType17.alias)
				{
					((CT_String)obj).Write(sw, "alias");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType17.bibliography)
				{
					sw.Write("<bibliography/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType17.citation)
				{
					sw.Write("<citation/>");
				}
				else if (obj is CT_SdtComboBox)
				{
					((CT_SdtComboBox)obj).Write(sw, "comboBox");
				}
				else if (obj is CT_DataBinding)
				{
					((CT_DataBinding)obj).Write(sw, "dataBinding");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_DecimalNumber AddNewId()
		{
			return AddNewObject<CT_DecimalNumber>(ItemsChoiceType17.id);
		}

		public CT_SdtDocPart AddNewDocPartObj()
		{
			return AddNewObject<CT_SdtDocPart>(ItemsChoiceType17.docPartObj);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType17 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType17 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType17 type) where T : class
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

		private T InsertNewObject<T>(ItemsChoiceType17 type, int p) where T : class, new()
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

		private T AddNewObject<T>(ItemsChoiceType17 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType17 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType17 type, int p)
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

		private void RemoveObject(ItemsChoiceType17 type, int p)
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
