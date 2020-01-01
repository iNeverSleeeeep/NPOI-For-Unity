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
	[XmlInclude(typeof(CT_TrPr))]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TrPrBase
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType2> itemsElementNameField;

		[XmlElement("jc", typeof(CT_Jc), Order = 0)]
		[XmlElement("cantSplit", typeof(CT_OnOff), Order = 0)]
		[XmlElement("cnfStyle", typeof(CT_Cnf), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("gridAfter", typeof(CT_DecimalNumber), Order = 0)]
		[XmlElement("gridBefore", typeof(CT_DecimalNumber), Order = 0)]
		[XmlElement("hidden", typeof(CT_OnOff), Order = 0)]
		[XmlElement("divId", typeof(CT_DecimalNumber), Order = 0)]
		[XmlElement("tblCellSpacing", typeof(CT_TblWidth), Order = 0)]
		[XmlElement("tblHeader", typeof(CT_OnOff), Order = 0)]
		[XmlElement("trHeight", typeof(CT_Height), Order = 0)]
		[XmlElement("wAfter", typeof(CT_TblWidth), Order = 0)]
		[XmlElement("wBefore", typeof(CT_TblWidth), Order = 0)]
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
		public List<ItemsChoiceType2> ItemsElementName
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

		public CT_TrPrBase()
		{
			itemsElementNameField = new List<ItemsChoiceType2>();
			itemsField = new ArrayList();
		}

		public static CT_TrPrBase Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TrPrBase cT_TrPrBase = new CT_TrPrBase();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "gridBefore")
				{
					cT_TrPrBase.Items.Add(CT_DecimalNumber.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.gridBefore);
				}
				else if (childNode.LocalName == "cantSplit")
				{
					cT_TrPrBase.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.cantSplit);
				}
				else if (childNode.LocalName == "cnfStyle")
				{
					cT_TrPrBase.Items.Add(CT_Cnf.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.cnfStyle);
				}
				else if (childNode.LocalName == "divId")
				{
					cT_TrPrBase.Items.Add(CT_DecimalNumber.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.divId);
				}
				else if (childNode.LocalName == "gridAfter")
				{
					cT_TrPrBase.Items.Add(CT_DecimalNumber.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.gridAfter);
				}
				else if (childNode.LocalName == "trHeight")
				{
					cT_TrPrBase.Items.Add(CT_Height.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.trHeight);
				}
				else if (childNode.LocalName == "hidden")
				{
					cT_TrPrBase.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.hidden);
				}
				else if (childNode.LocalName == "tblCellSpacing")
				{
					cT_TrPrBase.Items.Add(CT_TblWidth.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.tblCellSpacing);
				}
				else if (childNode.LocalName == "tblHeader")
				{
					cT_TrPrBase.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.tblHeader);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_TrPrBase.Items.Add(CT_Jc.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.jc);
				}
				else if (childNode.LocalName == "wAfter")
				{
					cT_TrPrBase.Items.Add(CT_TblWidth.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.wAfter);
				}
				else if (childNode.LocalName == "wBefore")
				{
					cT_TrPrBase.Items.Add(CT_TblWidth.Parse(childNode, namespaceManager));
					cT_TrPrBase.ItemsElementName.Add(ItemsChoiceType2.wBefore);
				}
			}
			return cT_TrPrBase;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			for (int i = 0; i < Items.Count; i++)
			{
				object obj = Items[i];
				if (obj is CT_DecimalNumber && ItemsElementName[i] == ItemsChoiceType2.gridBefore)
				{
					((CT_DecimalNumber)obj).Write(sw, "gridBefore");
				}
				else if (obj is CT_OnOff && ItemsElementName[i] == ItemsChoiceType2.cantSplit)
				{
					((CT_OnOff)obj).Write(sw, "cantSplit");
				}
				else if (obj is CT_Cnf && ItemsElementName[i] == ItemsChoiceType2.cnfStyle)
				{
					((CT_Cnf)obj).Write(sw, "cnfStyle");
				}
				else if (obj is CT_DecimalNumber && ItemsElementName[i] == ItemsChoiceType2.divId)
				{
					((CT_DecimalNumber)obj).Write(sw, "divId");
				}
				else if (obj is CT_DecimalNumber && ItemsElementName[i] == ItemsChoiceType2.gridAfter)
				{
					((CT_DecimalNumber)obj).Write(sw, "gridAfter");
				}
				else if (obj is CT_Height && ItemsElementName[i] == ItemsChoiceType2.trHeight)
				{
					((CT_Height)obj).Write(sw, "trHeight");
				}
				else if (obj is CT_OnOff && ItemsElementName[i] == ItemsChoiceType2.hidden)
				{
					((CT_OnOff)obj).Write(sw, "hidden");
				}
				else if (obj is CT_TblWidth && ItemsElementName[i] == ItemsChoiceType2.tblCellSpacing)
				{
					((CT_TblWidth)obj).Write(sw, "tblCellSpacing");
				}
				else if (obj is CT_OnOff && ItemsElementName[i] == ItemsChoiceType2.tblHeader)
				{
					((CT_OnOff)obj).Write(sw, "tblHeader");
				}
				else if (obj is CT_Jc && ItemsElementName[i] == ItemsChoiceType2.jc)
				{
					((CT_Jc)obj).Write(sw, "jc");
				}
				else if (obj is CT_TblWidth && ItemsElementName[i] == ItemsChoiceType2.wAfter)
				{
					((CT_TblWidth)obj).Write(sw, "wAfter");
				}
				else if (obj is CT_TblWidth && ItemsElementName[i] == ItemsChoiceType2.wBefore)
				{
					((CT_TblWidth)obj).Write(sw, "wBefore");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public int SizeOfTrHeightArray()
		{
			return SizeOfArray(ItemsChoiceType2.trHeight);
		}

		public CT_Height GetTrHeightArray(int p)
		{
			return GetObjectArray<CT_Height>(p, ItemsChoiceType2.trHeight);
		}

		public CT_Height AddNewTrHeight()
		{
			return AddNewObject<CT_Height>(ItemsChoiceType2.trHeight);
		}

		public CT_OnOff AddNewCantSplit()
		{
			return AddNewObject<CT_OnOff>(ItemsChoiceType2.cantSplit);
		}

		public List<CT_OnOff> GetCantSplitList()
		{
			return GetObjectList<CT_OnOff>(ItemsChoiceType2.cantSplit);
		}

		public CT_OnOff AddNewTblHeader()
		{
			return AddNewObject<CT_OnOff>(ItemsChoiceType2.tblHeader);
		}

		public List<CT_OnOff> GetTblHeaderList()
		{
			return GetObjectList<CT_OnOff>(ItemsChoiceType2.tblHeader);
		}

		public int SizeOfTblHeaderArray()
		{
			return SizeOfArray(ItemsChoiceType2.tblHeader);
		}

		public int SizeOfCantSplitArray()
		{
			return SizeOfArray(ItemsChoiceType2.cantSplit);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType2 type) where T : class
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

		private int SizeOfArray(ItemsChoiceType2 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType2 type) where T : class
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

		private T InsertNewObject<T>(ItemsChoiceType2 type, int p) where T : class, new()
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

		private T AddNewObject<T>(ItemsChoiceType2 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType2 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType2 type, int p)
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

		private void RemoveObject(ItemsChoiceType2 type, int p)
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
