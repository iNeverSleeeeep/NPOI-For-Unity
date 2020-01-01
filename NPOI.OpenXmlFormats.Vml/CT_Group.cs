using NPOI.OpenXmlFormats.Vml.Presentation;
using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using NPOI.OpenXmlFormats.Vml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_Group
	{
		private List<object> itemsField = new List<object>();

		private List<ItemsChoiceType6> itemsElementNameField = new List<ItemsChoiceType6>();

		private ST_TrueFalse filledField;

		private bool filledFieldSpecified;

		private string fillcolorField;

		private ST_EditAs editasField;

		private bool editasFieldSpecified;

		[XmlElement("ClientData", typeof(CT_ClientData), Namespace = "urn:schemas-microsoft-com:office:excel")]
		[XmlElement("anchorlock", typeof(CT_AnchorLock), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("image", typeof(CT_Image))]
		[XmlElement("path", typeof(CT_Path))]
		[XmlElement("textdata", typeof(CT_Rel), Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
		[XmlElement("borderbottom", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("borderleft", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("borderright", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("bordertop", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("wrap", typeof(CT_Wrap), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("arc", typeof(CT_Arc))]
		[XmlElement("curve", typeof(CT_Curve))]
		[XmlElement("fill", typeof(CT_Fill))]
		[XmlElement("formulas", typeof(CT_Formulas))]
		[XmlElement("group", typeof(CT_Group))]
		[XmlElement("handles", typeof(CT_Handles))]
		[XmlElement("imagedata", typeof(CT_ImageData))]
		[XmlElement("line", typeof(CT_Line))]
		[XmlElement("oval", typeof(CT_Oval))]
		[XmlElement("textbox", typeof(CT_Textbox))]
		[XmlElement("polyline", typeof(CT_PolyLine))]
		[XmlElement("rect", typeof(CT_Rect))]
		[XmlElement("roundrect", typeof(CT_RoundRect))]
		[XmlElement("shadow", typeof(CT_Shadow))]
		[XmlElement("shape", typeof(CT_Shape))]
		[XmlElement("shapetype", typeof(CT_Shapetype))]
		[XmlElement("stroke", typeof(CT_Stroke))]
		[XmlElement("textpath", typeof(CT_TextPath))]
		[XmlChoiceIdentifier("ItemsElementName")]
		public object[] Items
		{
			get
			{
				if (itemsField == null)
				{
					return null;
				}
				return itemsField.ToArray();
			}
			set
			{
				if (value == null)
				{
					itemsField = new List<object>();
				}
				else
				{
					itemsField = new List<object>(value);
				}
			}
		}

		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType6[] ItemsElementName
		{
			get
			{
				return itemsElementNameField.ToArray();
			}
			set
			{
				if (value == null)
				{
					itemsElementNameField = new List<ItemsChoiceType6>();
				}
				else
				{
					itemsElementNameField = new List<ItemsChoiceType6>(value);
				}
			}
		}

		[XmlAttribute]
		public ST_TrueFalse filled
		{
			get
			{
				return filledField;
			}
			set
			{
				filledField = value;
			}
		}

		[XmlIgnore]
		public bool filledSpecified
		{
			get
			{
				return filledFieldSpecified;
			}
			set
			{
				filledFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string fillcolor
		{
			get
			{
				return fillcolorField;
			}
			set
			{
				fillcolorField = value;
			}
		}

		[XmlAttribute]
		public ST_EditAs editas
		{
			get
			{
				return editasField;
			}
			set
			{
				editasField = value;
			}
		}

		[XmlIgnore]
		public bool editasSpecified
		{
			get
			{
				return editasFieldSpecified;
			}
			set
			{
				editasFieldSpecified = value;
			}
		}

		public CT_Shapetype AddNewShapetype()
		{
			return AddNewObject<CT_Shapetype>(ItemsChoiceType6.shapetype);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType6 type) where T : class
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

		private int SizeOfObjectArray(ItemsChoiceType6 type)
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

		private T GetObjectArray<T>(int p, ItemsChoiceType6 type) where T : class
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

		private T InsertNewObject<T>(ItemsChoiceType6 type, int p) where T : class, new()
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

		private T AddNewObject<T>(ItemsChoiceType6 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObjectArray<T>(ItemsChoiceType6 type, int p, T obj) where T : class
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

		private int GetObjectIndex(ItemsChoiceType6 type, int p)
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

		private void RemoveObject(ItemsChoiceType6 type, int p)
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

		public CT_Shape AddNewShape()
		{
			return AddNewObject<CT_Shape>(ItemsChoiceType6.shape);
		}
	}
}
