using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_R
	{
		private CT_RPr rPrField;

		private ArrayList itemsField;

		private List<RunItemsChoiceType> itemsElementNameField;

		private byte[] rsidRPrField;

		private byte[] rsidDelField;

		private byte[] rsidRField;

		[XmlElement(Order = 0, IsNullable = true)]
		public CT_RPr rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		[XmlElement("object", typeof(CT_Object), Order = 1)]
		[XmlElement("delText", typeof(CT_Text), Order = 1)]
		[XmlElement("drawing", typeof(CT_Drawing), Order = 1)]
		[XmlElement("endnoteRef", typeof(CT_Empty), Order = 1)]
		[XmlElement("endnoteReference", typeof(CT_FtnEdnRef), Order = 1)]
		[XmlElement("fldChar", typeof(CT_FldChar), Order = 1)]
		[XmlElement("footnoteRef", typeof(CT_Empty), Order = 1)]
		[XmlElement("footnoteReference", typeof(CT_FtnEdnRef), Order = 1)]
		[XmlElement("delInstrText", typeof(CT_Text), Order = 1)]
		[XmlElement("lastRenderedPageBreak", typeof(CT_Empty), Order = 1)]
		[XmlElement("monthLong", typeof(CT_Empty), Order = 1)]
		[XmlElement("monthShort", typeof(CT_Empty), Order = 1)]
		[XmlElement("noBreakHyphen", typeof(CT_Empty), Order = 1)]
		[XmlElement("annotationRef", typeof(CT_Empty), Order = 1)]
		[XmlElement("pgNum", typeof(CT_Empty), Order = 1)]
		[XmlElement("pict", typeof(CT_Picture), Order = 1)]
		[XmlElement("ptab", typeof(CT_PTab), Order = 1)]
		[XmlElement("ruby", typeof(CT_Ruby), Order = 1)]
		[XmlElement("separator", typeof(CT_Empty), Order = 1)]
		[XmlElement("softHyphen", typeof(CT_Empty), Order = 1)]
		[XmlElement("sym", typeof(CT_Sym), Order = 1)]
		[XmlElement("t", typeof(CT_Text), Order = 1)]
		[XmlElement("tab", typeof(CT_Empty), Order = 1)]
		[XmlElement("yearLong", typeof(CT_Empty), Order = 1)]
		[XmlElement("yearShort", typeof(CT_Empty), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("instrText", typeof(CT_Text), Order = 1)]
		[XmlElement("br", typeof(CT_Br), Order = 1)]
		[XmlElement("commentReference", typeof(CT_Markup), Order = 1)]
		[XmlElement("continuationSeparator", typeof(CT_Empty), Order = 1)]
		[XmlElement("cr", typeof(CT_Empty), Order = 1)]
		[XmlElement("dayLong", typeof(CT_Empty), Order = 1)]
		[XmlElement("dayShort", typeof(CT_Empty), Order = 1)]
		public ArrayList Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = new ArrayList();
			}
		}

		[XmlElement("ItemsElementName", Order = 2)]
		[XmlIgnore]
		public List<RunItemsChoiceType> ItemsElementName
		{
			get
			{
				return itemsElementNameField;
			}
			set
			{
				itemsElementNameField = new List<RunItemsChoiceType>();
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

		public CT_R()
		{
			itemsElementNameField = new List<RunItemsChoiceType>();
			itemsField = new ArrayList();
		}

		public CT_Text AddNewT()
		{
			return AddNewObject<CT_Text>(RunItemsChoiceType.t);
		}

		public CT_RPr AddNewRPr()
		{
			if (rPrField == null)
			{
				rPrField = new CT_RPr();
			}
			return rPrField;
		}

		public CT_Empty AddNewTab()
		{
			return AddNewObject<CT_Empty>(RunItemsChoiceType.tab);
		}

		public CT_FldChar AddNewFldChar()
		{
			return AddNewObject<CT_FldChar>(RunItemsChoiceType.fldChar);
		}

		public CT_Text AddNewInstrText()
		{
			return AddNewObject<CT_Text>(RunItemsChoiceType.instrText);
		}

		public CT_Empty AddNewCr()
		{
			return AddNewObject<CT_Empty>(RunItemsChoiceType.cr);
		}

		public CT_Br AddNewBr()
		{
			return AddNewObject<CT_Br>(RunItemsChoiceType.br);
		}

		public bool IsSetRPr()
		{
			return rPrField != null;
		}

		public int SizeOfTArray()
		{
			return SizeOfArray(RunItemsChoiceType.t);
		}

		public CT_Text GetTArray(int pos)
		{
			return GetObjectArray<CT_Text>(pos, RunItemsChoiceType.t);
		}

		public List<CT_Text> GetTList()
		{
			return GetObjectList<CT_Text>(RunItemsChoiceType.t);
		}

		public CT_Drawing AddNewDrawing()
		{
			return AddNewObject<CT_Drawing>(RunItemsChoiceType.drawing);
		}

		public IList<CT_Drawing> GetDrawingList()
		{
			return GetObjectList<CT_Drawing>(RunItemsChoiceType.drawing);
		}

		public IList<CT_Picture> GetPictList()
		{
			return GetObjectList<CT_Picture>(RunItemsChoiceType.pict);
		}

		public CT_Picture AddNewPict()
		{
			return AddNewObject<CT_Picture>(RunItemsChoiceType.pict);
		}

		private List<T> GetObjectList<T>(RunItemsChoiceType type) where T : class
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

		private int SizeOfArray(RunItemsChoiceType type)
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

		private T GetObjectArray<T>(int p, RunItemsChoiceType type) where T : class
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

		private T InsertNewObject<T>(RunItemsChoiceType type, int p) where T : class, new()
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

		private T AddNewObject<T>(RunItemsChoiceType type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(RunItemsChoiceType type, int p, T obj) where T : class
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

		private int GetObjectIndex(RunItemsChoiceType type, int p)
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

		private void RemoveObject(RunItemsChoiceType type, int p)
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

		public CT_Drawing GetDrawingArray(int p)
		{
			return GetObjectArray<CT_Drawing>(p, RunItemsChoiceType.drawing);
		}

		public int SizeOfCrArray()
		{
			return SizeOfArray(RunItemsChoiceType.cr);
		}

		public IList<CT_Empty> GetCrList()
		{
			return GetObjectList<CT_Empty>(RunItemsChoiceType.cr);
		}

		public int SizeOfBrArray()
		{
			return SizeOfArray(RunItemsChoiceType.br);
		}

		public static CT_R Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_R cT_R = new CT_R();
			cT_R.rsidRPr = XmlHelper.ReadBytes(node.Attributes["w:rsidRPr"]);
			cT_R.rsidDel = XmlHelper.ReadBytes(node.Attributes["w:rsidDel"]);
			cT_R.rsidR = XmlHelper.ReadBytes(node.Attributes["w:rsidR"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_R.rPr = CT_RPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "instrText")
				{
					cT_R.Items.Add(CT_Text.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.instrText);
				}
				else if (childNode.LocalName == "object")
				{
					cT_R.Items.Add(CT_Object.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.@object);
				}
				else if (childNode.LocalName == "pgNum")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.pgNum);
				}
				else if (childNode.LocalName == "annotationRef")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.annotationRef);
				}
				else if (childNode.LocalName == "br")
				{
					cT_R.Items.Add(CT_Br.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.br);
				}
				else if (childNode.LocalName == "commentReference")
				{
					cT_R.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.commentReference);
				}
				else if (childNode.LocalName == "continuationSeparator")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.continuationSeparator);
				}
				else if (childNode.LocalName == "cr")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.cr);
				}
				else if (childNode.LocalName == "dayLong")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.dayLong);
				}
				else if (childNode.LocalName == "dayShort")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.dayShort);
				}
				else if (childNode.LocalName == "delInstrText")
				{
					cT_R.Items.Add(CT_Text.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.delInstrText);
				}
				else if (childNode.LocalName == "delText")
				{
					cT_R.Items.Add(CT_Text.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.delText);
				}
				else if (childNode.LocalName == "drawing")
				{
					cT_R.Items.Add(CT_Drawing.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.drawing);
				}
				else if (childNode.LocalName == "endnoteRef")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.endnoteRef);
				}
				else if (childNode.LocalName == "endnoteReference")
				{
					cT_R.Items.Add(CT_FtnEdnRef.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.endnoteReference);
				}
				else if (childNode.LocalName == "fldChar")
				{
					cT_R.Items.Add(CT_FldChar.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.fldChar);
				}
				else if (childNode.LocalName == "footnoteRef")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.footnoteRef);
				}
				else if (childNode.LocalName == "footnoteReference")
				{
					cT_R.Items.Add(CT_FtnEdnRef.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.footnoteReference);
				}
				else if (childNode.LocalName == "lastRenderedPageBreak")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.lastRenderedPageBreak);
				}
				else if (childNode.LocalName == "monthLong")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.monthLong);
				}
				else if (childNode.LocalName == "monthShort")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.monthShort);
				}
				else if (childNode.LocalName == "noBreakHyphen")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.noBreakHyphen);
				}
				else if (childNode.LocalName == "yearLong")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.yearLong);
				}
				else if (childNode.LocalName == "pict")
				{
					cT_R.Items.Add(CT_Picture.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.pict);
				}
				else if (childNode.LocalName == "ptab")
				{
					cT_R.Items.Add(CT_PTab.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.ptab);
				}
				else if (childNode.LocalName == "ruby")
				{
					cT_R.Items.Add(CT_Ruby.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.ruby);
				}
				else if (childNode.LocalName == "separator")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.separator);
				}
				else if (childNode.LocalName == "softHyphen")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.softHyphen);
				}
				else if (childNode.LocalName == "sym")
				{
					cT_R.Items.Add(CT_Sym.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.sym);
				}
				else if (childNode.LocalName == "t")
				{
					cT_R.Items.Add(CT_Text.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(RunItemsChoiceType.t);
				}
				else if (childNode.LocalName == "tab")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.tab);
				}
				else if (childNode.LocalName == "yearShort")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(RunItemsChoiceType.yearShort);
				}
			}
			return cT_R;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:rsidR", rsidR);
			XmlHelper.WriteAttribute(sw, "w:rsidRPr", rsidRPr);
			XmlHelper.WriteAttribute(sw, "w:rsidDel", rsidDel);
			sw.Write(">");
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			int num = 0;
			foreach (object item in Items)
			{
				if (item is CT_Text && ItemsElementName[num] == RunItemsChoiceType.instrText)
				{
					((CT_Text)item).Write(sw, "instrText");
				}
				else if (item is CT_Object)
				{
					((CT_Object)item).Write(sw, "object");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.pgNum)
				{
					sw.Write("<w:pgNum/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.annotationRef)
				{
					sw.Write("<w:annotationRef/>");
				}
				else if (item is CT_Br)
				{
					((CT_Br)item).Write(sw, "br");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "w:commentReference");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.continuationSeparator)
				{
					sw.Write("<w:continuationSeparator/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.cr)
				{
					sw.Write("<w:cr/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.dayLong)
				{
					sw.Write("<w:dayLong/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.dayShort)
				{
					sw.Write("<w:dayShort/>");
				}
				else if (item is CT_Text && ItemsElementName[num] == RunItemsChoiceType.delInstrText)
				{
					((CT_Text)item).Write(sw, "delInstrText");
				}
				else if (item is CT_Text && ItemsElementName[num] == RunItemsChoiceType.delText)
				{
					((CT_Text)item).Write(sw, "delText");
				}
				else if (item is CT_Drawing)
				{
					((CT_Drawing)item).Write(sw, "drawing");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.endnoteRef)
				{
					sw.Write("<w:endnoteRef/>");
				}
				else if (item is CT_FtnEdnRef)
				{
					((CT_FtnEdnRef)item).Write(sw, "endnoteReference");
				}
				else if (item is CT_FldChar)
				{
					((CT_FldChar)item).Write(sw, "fldChar");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.footnoteRef)
				{
					sw.Write("<w:footnoteRef/>");
				}
				else if (item is CT_FtnEdnRef)
				{
					((CT_FtnEdnRef)item).Write(sw, "footnoteReference");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.lastRenderedPageBreak)
				{
					sw.Write("<w:lastRenderedPageBreak/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.monthLong)
				{
					sw.Write("<w:monthLong/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.monthShort)
				{
					sw.Write("<w:monthShort/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.noBreakHyphen)
				{
					sw.Write("<w:noBreakHyphen/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.yearLong)
				{
					sw.Write("<w:yearLong/>");
				}
				else if (item is CT_Picture)
				{
					((CT_Picture)item).Write(sw, "pict");
				}
				else if (item is CT_PTab)
				{
					((CT_PTab)item).Write(sw, "ptab");
				}
				else if (item is CT_Ruby)
				{
					((CT_Ruby)item).Write(sw, "ruby");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.separator)
				{
					sw.Write("<w:separator/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.softHyphen)
				{
					sw.Write("<w:softHyphen/>");
				}
				else if (item is CT_Sym)
				{
					((CT_Sym)item).Write(sw, "sym");
				}
				else if (item is CT_Text && ItemsElementName[num] == RunItemsChoiceType.t)
				{
					((CT_Text)item).Write(sw, "t");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.tab)
				{
					sw.Write("<w:tab/>");
				}
				else if (item is CT_Empty && ItemsElementName[num] == RunItemsChoiceType.yearShort)
				{
					sw.Write("<w:yearShort/>");
				}
				num++;
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
