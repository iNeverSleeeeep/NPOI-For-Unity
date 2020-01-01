using NPOI.OpenXml4Net.Util;
using System;
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
	public class CT_SectPr
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

		private CT_SectPrChange sectPrChangeField;

		private byte[] rsidRPrField;

		private byte[] rsidDelField;

		private byte[] rsidRField;

		private byte[] rsidSectField;

		private List<CT_HdrFtrRef> footerReferenceField;

		private List<CT_HdrFtrRef> headerReferenceField;

		public List<CT_HdrFtrRef> footerReference
		{
			get
			{
				return footerReferenceField;
			}
			set
			{
				footerReferenceField = value;
			}
		}

		public List<CT_HdrFtrRef> headerReference
		{
			get
			{
				return headerReferenceField;
			}
			set
			{
				headerReferenceField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 5)]
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

		[XmlElement(Order = 6)]
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

		[XmlElement(Order = 7)]
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

		[XmlElement(Order = 8)]
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

		[XmlElement(Order = 9)]
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

		[XmlElement(Order = 10)]
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

		[XmlElement(Order = 11)]
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

		[XmlElement(Order = 12)]
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

		[XmlElement(Order = 13)]
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

		[XmlElement(Order = 14)]
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

		[XmlElement(Order = 15)]
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

		[XmlElement(Order = 16)]
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

		[XmlElement(Order = 17)]
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

		[XmlElement(Order = 18)]
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

		[XmlElement(Order = 19)]
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

		[XmlElement(Order = 20)]
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

		[XmlElement(Order = 21)]
		public CT_SectPrChange sectPrChange
		{
			get
			{
				return sectPrChangeField;
			}
			set
			{
				sectPrChangeField = value;
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

		public CT_SectPr()
		{
			docGridField = new CT_DocGrid();
			docGrid.type = ST_DocGrid.lines;
			docGrid.typeSpecified = true;
			docGrid.linePitch = "312";
			colsField = new CT_Columns();
			cols.space = 425uL;
			cols.spaceSpecified = true;
			pgMarField = new CT_PageMar();
			pgMar.top = "1440";
			pgMar.right = 1800uL;
			pgMar.bottom = "1440";
			pgMar.left = 1800uL;
			pgMar.header = 851uL;
			pgMar.footer = 992uL;
			pgMar.gutter = 0uL;
			pgSzField = new CT_PageSz();
			pgSz.w = 11906uL;
			pgSz.wSpecified = true;
			pgSz.h = 16838uL;
			pgSz.hSpecified = true;
		}

		public CT_HdrFtrRef AddNewHeaderReference()
		{
			CT_HdrFtrRef cT_HdrFtrRef = new CT_HdrFtrRef();
			if (headerReferenceField == null)
			{
				headerReferenceField = new List<CT_HdrFtrRef>();
			}
			headerReferenceField.Add(cT_HdrFtrRef);
			return cT_HdrFtrRef;
		}

		public CT_HdrFtrRef AddNewFooterReference()
		{
			CT_HdrFtrRef cT_HdrFtrRef = new CT_HdrFtrRef();
			if (footerReferenceField == null)
			{
				footerReferenceField = new List<CT_HdrFtrRef>();
			}
			footerReferenceField.Add(cT_HdrFtrRef);
			return cT_HdrFtrRef;
		}

		public int SizeOfHeaderReferenceArray()
		{
			return headerReferenceField.Count;
		}

		public CT_HdrFtrRef GetHeaderReferenceArray(int i)
		{
			return headerReferenceField[i];
		}

		public int SizeOfFooterReferenceArray()
		{
			return footerReferenceField.Count;
		}

		public CT_HdrFtrRef GetFooterReferenceArray(int i)
		{
			return footerReferenceField[i];
		}

		public static CT_SectPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SectPr cT_SectPr = new CT_SectPr();
			cT_SectPr.rsidRPr = XmlHelper.ReadBytes(node.Attributes["w:rsidRPr"]);
			cT_SectPr.rsidDel = XmlHelper.ReadBytes(node.Attributes["w:rsidDel"]);
			cT_SectPr.rsidR = XmlHelper.ReadBytes(node.Attributes["w:rsidR"]);
			cT_SectPr.rsidSect = XmlHelper.ReadBytes(node.Attributes["w:rsidSect"]);
			cT_SectPr.footerReference = new List<CT_HdrFtrRef>();
			cT_SectPr.headerReference = new List<CT_HdrFtrRef>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "footnotePr")
				{
					cT_SectPr.footnotePr = CT_FtnProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "endnotePr")
				{
					cT_SectPr.endnotePr = CT_EdnProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "type")
				{
					cT_SectPr.type = CT_SectType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgSz")
				{
					cT_SectPr.pgSz = CT_PageSz.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgMar")
				{
					cT_SectPr.pgMar = CT_PageMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "paperSrc")
				{
					cT_SectPr.paperSrc = CT_PaperSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgBorders")
				{
					cT_SectPr.pgBorders = CT_PageBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lnNumType")
				{
					cT_SectPr.lnNumType = CT_LineNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pgNumType")
				{
					cT_SectPr.pgNumType = CT_PageNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cols")
				{
					cT_SectPr.cols = CT_Columns.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "formProt")
				{
					cT_SectPr.formProt = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vAlign")
				{
					cT_SectPr.vAlign = CT_VerticalJc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noEndnote")
				{
					cT_SectPr.noEndnote = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "titlePg")
				{
					cT_SectPr.titlePg = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textDirection")
				{
					cT_SectPr.textDirection = CT_TextDirection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bidi")
				{
					cT_SectPr.bidi = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rtlGutter")
				{
					cT_SectPr.rtlGutter = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "docGrid")
				{
					cT_SectPr.docGrid = CT_DocGrid.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printerSettings")
				{
					cT_SectPr.printerSettings = CT_Rel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sectPrChange")
				{
					cT_SectPr.sectPrChange = CT_SectPrChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "footerReference")
				{
					cT_SectPr.footerReference.Add(CT_HdrFtrRef.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "headerReference")
				{
					cT_SectPr.headerReference.Add(CT_HdrFtrRef.Parse(childNode, namespaceManager));
				}
			}
			return cT_SectPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:rsidR", rsidR);
			XmlHelper.WriteAttribute(sw, "w:rsidRPr", rsidRPr);
			XmlHelper.WriteAttribute(sw, "w:rsidSect", rsidSect);
			XmlHelper.WriteAttribute(sw, "w:rsidDel", rsidDel);
			sw.Write(">");
			if (headerReference != null)
			{
				foreach (CT_HdrFtrRef item in headerReference)
				{
					item.Write(sw, "headerReference");
				}
			}
			if (footerReference != null)
			{
				foreach (CT_HdrFtrRef item2 in footerReference)
				{
					item2.Write(sw, "footerReference");
				}
			}
			if (footnotePr != null)
			{
				footnotePr.Write(sw, "footnotePr");
			}
			if (endnotePr != null)
			{
				endnotePr.Write(sw, "endnotePr");
			}
			if (type != null)
			{
				type.Write(sw, "type");
			}
			if (pgSz != null)
			{
				pgSz.Write(sw, "pgSz");
			}
			if (pgMar != null)
			{
				pgMar.Write(sw, "pgMar");
			}
			if (paperSrc != null)
			{
				paperSrc.Write(sw, "paperSrc");
			}
			if (pgBorders != null)
			{
				pgBorders.Write(sw, "pgBorders");
			}
			if (lnNumType != null)
			{
				lnNumType.Write(sw, "lnNumType");
			}
			if (pgNumType != null)
			{
				pgNumType.Write(sw, "pgNumType");
			}
			if (cols != null)
			{
				cols.Write(sw, "cols");
			}
			if (formProt != null)
			{
				formProt.Write(sw, "formProt");
			}
			if (vAlign != null)
			{
				vAlign.Write(sw, "vAlign");
			}
			if (noEndnote != null)
			{
				noEndnote.Write(sw, "noEndnote");
			}
			if (titlePg != null)
			{
				titlePg.Write(sw, "titlePg");
			}
			if (textDirection != null)
			{
				textDirection.Write(sw, "textDirection");
			}
			if (bidi != null)
			{
				bidi.Write(sw, "bidi");
			}
			if (rtlGutter != null)
			{
				rtlGutter.Write(sw, "rtlGutter");
			}
			if (docGrid != null)
			{
				docGrid.Write(sw, "docGrid");
			}
			if (printerSettings != null)
			{
				printerSettings.Write(sw, "printerSettings");
			}
			if (sectPrChange != null)
			{
				sectPrChange.Write(sw, "sectPrChange");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
