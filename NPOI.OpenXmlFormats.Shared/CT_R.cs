using NPOI.OpenXmlFormats.Dml.WordProcessing;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_R
	{
		private CT_RPR rPrField;

		private CT_RPr rPr1Field;

		private ArrayList itemsField;

		private List<ItemsChoiceType6> itemsElementNameField;

		[XmlElement(Order = 0)]
		public CT_RPR rPr
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

		[XmlElement("rPr", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		public CT_RPr rPr1
		{
			get
			{
				return rPr1Field;
			}
			set
			{
				rPr1Field = value;
			}
		}

		[XmlElement("dayShort", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("t", typeof(CT_Text1), Order = 2)]
		[XmlElement("commentReference", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("continuationSeparator", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("dayLong", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("delInstrText", typeof(CT_Text), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("endnoteRef", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("endnoteReference", typeof(CT_FtnEdnRef), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("fldChar", typeof(CT_FldChar), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("footnoteRef", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("footnoteReference", typeof(CT_FtnEdnRef), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("instrText", typeof(CT_Text), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("delText", typeof(CT_Text), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("drawing", typeof(CT_Drawing), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("lastRenderedPageBreak", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("monthLong", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("monthShort", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("noBreakHyphen", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("object", typeof(CT_Object), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("pgNum", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("pict", typeof(CT_Picture), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("ptab", typeof(CT_PTab), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("ruby", typeof(CT_Ruby), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("separator", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("softHyphen", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("cr", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("t", typeof(CT_Text), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("tab", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("yearLong", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("yearShort", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("sym", typeof(CT_Sym), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("annotationRef", typeof(CT_Empty), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
		[XmlElement("br", typeof(CT_Br), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 2)]
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

		[XmlElement("ItemsElementName", Order = 3)]
		[XmlIgnore]
		public List<ItemsChoiceType6> ItemsElementName
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

		public CT_R()
		{
			itemsElementNameField = new List<ItemsChoiceType6>();
			itemsField = new ArrayList();
		}

		public static CT_R Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_R cT_R = new CT_R();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "endnoteReference")
				{
					cT_R.Items.Add(CT_FtnEdnRef.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.endnoteReference);
				}
				else if (childNode.LocalName == "drawing")
				{
					cT_R.Items.Add(CT_Drawing.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.drawing);
				}
				else if (childNode.LocalName == "tab")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.tab);
				}
				else if (childNode.LocalName == "dayLong")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.dayLong);
				}
				else if (childNode.LocalName == "t")
				{
					cT_R.Items.Add(CT_Text1.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.t);
				}
				else if (childNode.LocalName == "dayShort")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.dayShort);
				}
				else if (childNode.LocalName == "annotationRef")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.annotationRef);
				}
				else if (childNode.LocalName == "endnoteRef")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.endnoteRef);
				}
				else if (childNode.LocalName == "fldChar")
				{
					cT_R.Items.Add(CT_FldChar.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.fldChar);
				}
				else if (childNode.LocalName == "lastRenderedPageBreak")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.lastRenderedPageBreak);
				}
				else if (childNode.LocalName == "footnoteReference")
				{
					cT_R.Items.Add(CT_FtnEdnRef.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.footnoteReference);
				}
				else if (childNode.LocalName == "delInstrText")
				{
					cT_R.Items.Add(CT_Text.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.delInstrText);
				}
				else if (childNode.LocalName == "delText")
				{
					cT_R.Items.Add(CT_Text.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.delText);
				}
				else if (childNode.LocalName == "commentReference")
				{
					cT_R.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.commentReference);
				}
				else if (childNode.LocalName == "monthLong")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.monthLong);
				}
				else if (childNode.LocalName == "monthShort")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.monthShort);
				}
				else if (childNode.LocalName == "continuationSeparator")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.continuationSeparator);
				}
				else if (childNode.LocalName == "cr")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.cr);
				}
				else if (childNode.LocalName == "noBreakHyphen")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.noBreakHyphen);
				}
				else if (childNode.LocalName == "object")
				{
					cT_R.Items.Add(CT_Object.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.@object);
				}
				else if (childNode.LocalName == "br")
				{
					cT_R.Items.Add(CT_Br.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.br);
				}
				else if (childNode.LocalName == "pgNum")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.pgNum);
				}
				else if (childNode.LocalName == "pict")
				{
					cT_R.Items.Add(CT_Picture.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.pict);
				}
				else if (childNode.LocalName == "ptab")
				{
					cT_R.Items.Add(CT_PTab.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.ptab);
				}
				else if (childNode.LocalName == "ruby")
				{
					cT_R.Items.Add(CT_Ruby.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.ruby);
				}
				else if (childNode.LocalName == "separator")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.separator);
				}
				else if (childNode.LocalName == "softHyphen")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.softHyphen);
				}
				else if (childNode.LocalName == "sym")
				{
					cT_R.Items.Add(CT_Sym.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.sym);
				}
				else if (childNode.LocalName == "t")
				{
					cT_R.Items.Add(CT_Text1.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.t);
				}
				else if (childNode.LocalName == "yearLong")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.yearLong);
				}
				else if (childNode.LocalName == "yearShort")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.yearShort);
				}
				else if (childNode.LocalName == "instrText")
				{
					cT_R.Items.Add(CT_Text.Parse(childNode, namespaceManager));
					cT_R.ItemsElementName.Add(ItemsChoiceType6.instrText);
				}
				else if (childNode.LocalName == "footnoteRef")
				{
					cT_R.Items.Add(new CT_Empty());
					cT_R.ItemsElementName.Add(ItemsChoiceType6.footnoteRef);
				}
			}
			return cT_R;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			for (int i = 0; i < Items.Count; i++)
			{
				object obj = Items[i];
				ItemsChoiceType6 itemsChoiceType = ItemsElementName[i];
				if (obj is CT_FtnEdnRef && itemsChoiceType == ItemsChoiceType6.endnoteReference)
				{
					((CT_FtnEdnRef)obj).Write(sw, "endnoteReference");
				}
				else if (obj is CT_Drawing)
				{
					((CT_Drawing)obj).Write(sw, "drawing");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.tab)
				{
					sw.Write("<tab/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.dayLong)
				{
					sw.Write("<dayLong/>");
				}
				else if (obj is CT_Text1 && itemsChoiceType == ItemsChoiceType6.t)
				{
					((CT_Text1)obj).Write(sw, "t");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.dayShort)
				{
					sw.Write("<dayShort/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.annotationRef)
				{
					sw.Write("<annotationRef/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.endnoteRef)
				{
					sw.Write("<endnoteRef/>");
				}
				else if (obj is CT_FldChar)
				{
					((CT_FldChar)obj).Write(sw, "fldChar");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.lastRenderedPageBreak)
				{
					sw.Write("<lastRenderedPageBreak/>");
				}
				else if (obj is CT_FtnEdnRef && itemsChoiceType == ItemsChoiceType6.footnoteReference)
				{
					((CT_FtnEdnRef)obj).Write(sw, "footnoteReference");
				}
				else if (obj is CT_Text && itemsChoiceType == ItemsChoiceType6.delInstrText)
				{
					((CT_Text)obj).Write(sw, "delInstrText");
				}
				else if (obj is CT_Text && itemsChoiceType == ItemsChoiceType6.delText)
				{
					((CT_Text)obj).Write(sw, "delText");
				}
				else if (obj is CT_Markup && itemsChoiceType == ItemsChoiceType6.commentReference)
				{
					((CT_Markup)obj).Write(sw, "commentReference");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.monthLong)
				{
					sw.Write("<monthLong/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.monthShort)
				{
					sw.Write("<monthShort/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.continuationSeparator)
				{
					sw.Write("<continuationSeparator/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.cr)
				{
					sw.Write("<cr/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.noBreakHyphen)
				{
					sw.Write("<noBreakHyphen/>");
				}
				else if (obj is CT_Object)
				{
					((CT_Object)obj).Write(sw, "object");
				}
				else if (obj is CT_Br)
				{
					((CT_Br)obj).Write(sw, "br");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.pgNum)
				{
					sw.Write("<pgNum/>");
				}
				else if (obj is CT_Picture)
				{
					((CT_Picture)obj).Write(sw, "pict");
				}
				else if (obj is CT_PTab)
				{
					((CT_PTab)obj).Write(sw, "ptab");
				}
				else if (obj is CT_Ruby)
				{
					((CT_Ruby)obj).Write(sw, "ruby");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.separator)
				{
					sw.Write("<separator/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.softHyphen)
				{
					sw.Write("<softHyphen/>");
				}
				else if (obj is CT_Sym)
				{
					((CT_Sym)obj).Write(sw, "sym");
				}
				else if (obj is CT_Text && itemsChoiceType == ItemsChoiceType6.t)
				{
					((CT_Text)obj).Write(sw, "t");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.yearLong)
				{
					sw.Write("<yearLong/>");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.yearShort)
				{
					sw.Write("<yearShort/>");
				}
				else if (obj is CT_Text && itemsChoiceType == ItemsChoiceType6.instrText)
				{
					((CT_Text)obj).Write(sw, "instrText");
				}
				else if (obj is CT_Empty && itemsChoiceType == ItemsChoiceType6.footnoteRef)
				{
					sw.Write("<footnoteRef/>");
				}
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
