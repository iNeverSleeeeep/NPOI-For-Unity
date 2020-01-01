using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TrPr : CT_TrPrBase
	{
		private CT_TrackChange insField;

		private CT_TrackChange delField;

		private CT_TrPrChange trPrChangeField;

		[XmlElement(Order = 0)]
		public CT_TrackChange ins
		{
			get
			{
				return insField;
			}
			set
			{
				insField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TrackChange del
		{
			get
			{
				return delField;
			}
			set
			{
				delField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TrPrChange trPrChange
		{
			get
			{
				return trPrChangeField;
			}
			set
			{
				trPrChangeField = value;
			}
		}

		public new static CT_TrPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TrPr cT_TrPr = new CT_TrPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ins")
				{
					cT_TrPr.ins = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "del")
				{
					cT_TrPr.del = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "trPrChange")
				{
					cT_TrPr.trPrChange = CT_TrPrChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gridBefore")
				{
					cT_TrPr.Items.Add(CT_DecimalNumber.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.gridBefore);
				}
				else if (childNode.LocalName == "cantSplit")
				{
					cT_TrPr.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.cantSplit);
				}
				else if (childNode.LocalName == "cnfStyle")
				{
					cT_TrPr.Items.Add(CT_Cnf.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.cnfStyle);
				}
				else if (childNode.LocalName == "divId")
				{
					cT_TrPr.Items.Add(CT_DecimalNumber.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.divId);
				}
				else if (childNode.LocalName == "gridAfter")
				{
					cT_TrPr.Items.Add(CT_DecimalNumber.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.gridAfter);
				}
				else if (childNode.LocalName == "trHeight")
				{
					cT_TrPr.Items.Add(CT_Height.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.trHeight);
				}
				else if (childNode.LocalName == "hidden")
				{
					cT_TrPr.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.hidden);
				}
				else if (childNode.LocalName == "tblCellSpacing")
				{
					cT_TrPr.Items.Add(CT_TblWidth.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.tblCellSpacing);
				}
				else if (childNode.LocalName == "tblHeader")
				{
					cT_TrPr.Items.Add(CT_OnOff.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.tblHeader);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_TrPr.Items.Add(CT_Jc.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.jc);
				}
				else if (childNode.LocalName == "wAfter")
				{
					cT_TrPr.Items.Add(CT_TblWidth.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.wAfter);
				}
				else if (childNode.LocalName == "wBefore")
				{
					cT_TrPr.Items.Add(CT_TblWidth.Parse(childNode, namespaceManager));
					cT_TrPr.ItemsElementName.Add(ItemsChoiceType2.wBefore);
				}
			}
			return cT_TrPr;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (ins != null)
			{
				ins.Write(sw, "ins");
			}
			if (del != null)
			{
				del.Write(sw, "del");
			}
			if (trPrChange != null)
			{
				trPrChange.Write(sw, "trPrChange");
			}
			for (int i = 0; i < base.Items.Count; i++)
			{
				object obj = base.Items[i];
				if (obj is CT_DecimalNumber && base.ItemsElementName[i] == ItemsChoiceType2.gridBefore)
				{
					((CT_DecimalNumber)obj).Write(sw, "gridBefore");
				}
				else if (obj is CT_OnOff && base.ItemsElementName[i] == ItemsChoiceType2.cantSplit)
				{
					((CT_OnOff)obj).Write(sw, "cantSplit");
				}
				else if (obj is CT_Cnf && base.ItemsElementName[i] == ItemsChoiceType2.cnfStyle)
				{
					((CT_Cnf)obj).Write(sw, "cnfStyle");
				}
				else if (obj is CT_DecimalNumber && base.ItemsElementName[i] == ItemsChoiceType2.divId)
				{
					((CT_DecimalNumber)obj).Write(sw, "divId");
				}
				else if (obj is CT_DecimalNumber && base.ItemsElementName[i] == ItemsChoiceType2.gridAfter)
				{
					((CT_DecimalNumber)obj).Write(sw, "gridAfter");
				}
				else if (obj is CT_Height && base.ItemsElementName[i] == ItemsChoiceType2.trHeight)
				{
					((CT_Height)obj).Write(sw, "trHeight");
				}
				else if (obj is CT_OnOff && base.ItemsElementName[i] == ItemsChoiceType2.hidden)
				{
					((CT_OnOff)obj).Write(sw, "hidden");
				}
				else if (obj is CT_TblWidth && base.ItemsElementName[i] == ItemsChoiceType2.tblCellSpacing)
				{
					((CT_TblWidth)obj).Write(sw, "tblCellSpacing");
				}
				else if (obj is CT_OnOff && base.ItemsElementName[i] == ItemsChoiceType2.tblHeader)
				{
					((CT_OnOff)obj).Write(sw, "tblHeader");
				}
				else if (obj is CT_Jc && base.ItemsElementName[i] == ItemsChoiceType2.jc)
				{
					((CT_Jc)obj).Write(sw, "jc");
				}
				else if (obj is CT_TblWidth && base.ItemsElementName[i] == ItemsChoiceType2.wAfter)
				{
					((CT_TblWidth)obj).Write(sw, "wAfter");
				}
				else if (obj is CT_TblWidth && base.ItemsElementName[i] == ItemsChoiceType2.wBefore)
				{
					((CT_TblWidth)obj).Write(sw, "wBefore");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
