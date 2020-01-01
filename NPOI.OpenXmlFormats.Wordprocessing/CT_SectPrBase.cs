using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SectPrBase
	{
		private CT_FtnProps footnotePrField;

		private CT_EdnProps endnotePrField;

		private CT_SectType typeField;

		private CT_PageSz pgSzField;

		private CT_PageMar pgMarField;

		private CT_PaperSource paperSrcField;

		private CT_PageBorders pgBordersField;

		private CT_LineNumber lnNumTypeField;

		private CT_PageNumber pgNumTypeField;

		private CT_Columns colsField;

		private CT_OnOff formProtField;

		private CT_VerticalJc vAlignField;

		private CT_OnOff noEndnoteField;

		private CT_OnOff titlePgField;

		private CT_TextDirection textDirectionField;

		private CT_OnOff bidiField;

		private CT_OnOff rtlGutterField;

		private CT_DocGrid docGridField;

		private CT_Rel printerSettingsField;

		private byte[] rsidRPrField;

		private byte[] rsidDelField;

		private byte[] rsidRField;

		private byte[] rsidSectField;

		[XmlElement(Order = 0)]
		public CT_FtnProps footnotePr
		{
			get
			{
				return footnotePrField;
			}
			set
			{
				footnotePrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_EdnProps endnotePr
		{
			get
			{
				return endnotePrField;
			}
			set
			{
				endnotePrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_SectType type
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

		[XmlElement(Order = 3)]
		public CT_PageSz pgSz
		{
			get
			{
				return pgSzField;
			}
			set
			{
				pgSzField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_PageMar pgMar
		{
			get
			{
				return pgMarField;
			}
			set
			{
				pgMarField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_PaperSource paperSrc
		{
			get
			{
				return paperSrcField;
			}
			set
			{
				paperSrcField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_PageBorders pgBorders
		{
			get
			{
				return pgBordersField;
			}
			set
			{
				pgBordersField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_LineNumber lnNumType
		{
			get
			{
				return lnNumTypeField;
			}
			set
			{
				lnNumTypeField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_PageNumber pgNumType
		{
			get
			{
				return pgNumTypeField;
			}
			set
			{
				pgNumTypeField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Columns cols
		{
			get
			{
				return colsField;
			}
			set
			{
				colsField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_OnOff formProt
		{
			get
			{
				return formProtField;
			}
			set
			{
				formProtField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_VerticalJc vAlign
		{
			get
			{
				return vAlignField;
			}
			set
			{
				vAlignField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_OnOff noEndnote
		{
			get
			{
				return noEndnoteField;
			}
			set
			{
				noEndnoteField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_OnOff titlePg
		{
			get
			{
				return titlePgField;
			}
			set
			{
				titlePgField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_TextDirection textDirection
		{
			get
			{
				return textDirectionField;
			}
			set
			{
				textDirectionField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_OnOff bidi
		{
			get
			{
				return bidiField;
			}
			set
			{
				bidiField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_OnOff rtlGutter
		{
			get
			{
				return rtlGutterField;
			}
			set
			{
				rtlGutterField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_DocGrid docGrid
		{
			get
			{
				return docGridField;
			}
			set
			{
				docGridField = value;
			}
		}

		[XmlElement(Order = 18)]
		public CT_Rel printerSettings
		{
			get
			{
				return printerSettingsField;
			}
			set
			{
				printerSettingsField = value;
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] rsidSect
		{
			get
			{
				return rsidSectField;
			}
			set
			{
				rsidSectField = value;
			}
		}

		public static CT_SectPrBase Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SectPrBase cT_SectPrBase = new CT_SectPrBase();
			cT_SectPrBase.rsidRPr = XmlHelper.ReadBytes(node.Attributes["w:rsidRPr"]);
			cT_SectPrBase.rsidDel = XmlHelper.ReadBytes(node.Attributes["w:rsidDel"]);
			cT_SectPrBase.rsidR = XmlHelper.ReadBytes(node.Attributes["w:rsidR"]);
			cT_SectPrBase.rsidSect = XmlHelper.ReadBytes(node.Attributes["w:rsidSect"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "footnotePr")
				{
					cT_SectPrBase.footnotePr = CT_FtnProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "endnotePr")
				{
					cT_SectPrBase.endnotePr = CT_EdnProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "type")
				{
					cT_SectPrBase.type = CT_SectType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgSz")
				{
					cT_SectPrBase.pgSz = CT_PageSz.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgMar")
				{
					cT_SectPrBase.pgMar = CT_PageMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "paperSrc")
				{
					cT_SectPrBase.paperSrc = CT_PaperSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgBorders")
				{
					cT_SectPrBase.pgBorders = CT_PageBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lnNumType")
				{
					cT_SectPrBase.lnNumType = CT_LineNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgNumType")
				{
					cT_SectPrBase.pgNumType = CT_PageNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cols")
				{
					cT_SectPrBase.cols = CT_Columns.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "formProt")
				{
					cT_SectPrBase.formProt = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vAlign")
				{
					cT_SectPrBase.vAlign = CT_VerticalJc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noEndnote")
				{
					cT_SectPrBase.noEndnote = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "titlePg")
				{
					cT_SectPrBase.titlePg = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textDirection")
				{
					cT_SectPrBase.textDirection = CT_TextDirection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bidi")
				{
					cT_SectPrBase.bidi = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rtlGutter")
				{
					cT_SectPrBase.rtlGutter = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "docGrid")
				{
					cT_SectPrBase.docGrid = CT_DocGrid.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printerSettings")
				{
					cT_SectPrBase.printerSettings = CT_Rel.Parse(childNode, namespaceManager);
				}
			}
			return cT_SectPrBase;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:rsidRPr", rsidRPr);
			XmlHelper.WriteAttribute(sw, "w:rsidDel", rsidDel);
			XmlHelper.WriteAttribute(sw, "w:rsidR", rsidR);
			XmlHelper.WriteAttribute(sw, "w:rsidSect", rsidSect);
			sw.Write(">");
			if (footnotePr != null)
			{
				footnotePr.Write(sw, "w:footnotePr");
			}
			if (endnotePr != null)
			{
				endnotePr.Write(sw, "w:endnotePr");
			}
			if (type != null)
			{
				type.Write(sw, "w:type");
			}
			if (pgSz != null)
			{
				pgSz.Write(sw, "w:pgSz");
			}
			if (pgMar != null)
			{
				pgMar.Write(sw, "w:pgMar");
			}
			if (paperSrc != null)
			{
				paperSrc.Write(sw, "w:paperSrc");
			}
			if (pgBorders != null)
			{
				pgBorders.Write(sw, "w:pgBorders");
			}
			if (lnNumType != null)
			{
				lnNumType.Write(sw, "w:lnNumType");
			}
			if (pgNumType != null)
			{
				pgNumType.Write(sw, "w:pgNumType");
			}
			if (cols != null)
			{
				cols.Write(sw, "w:cols");
			}
			if (formProt != null)
			{
				formProt.Write(sw, "w:formProt");
			}
			if (vAlign != null)
			{
				vAlign.Write(sw, "w:vAlign");
			}
			if (noEndnote != null)
			{
				noEndnote.Write(sw, "w:noEndnote");
			}
			if (titlePg != null)
			{
				titlePg.Write(sw, "w:titlePg");
			}
			if (textDirection != null)
			{
				textDirection.Write(sw, "w:textDirection");
			}
			if (bidi != null)
			{
				bidi.Write(sw, "w:bidi");
			}
			if (rtlGutter != null)
			{
				rtlGutter.Write(sw, "w:rtlGutter");
			}
			if (docGrid != null)
			{
				docGrid.Write(sw, "w:docGrid");
			}
			if (printerSettings != null)
			{
				printerSettings.Write(sw, "w:printerSettings");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
