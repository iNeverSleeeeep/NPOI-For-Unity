using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// @author Philipp Epp
	public class XWPFStyles : POIXMLDocumentPart
	{
		private List<XWPFStyle> listStyle = new List<XWPFStyle>();

		private CT_Styles ctStyles;

		private XWPFLatentStyles latentStyles;

		/// Construct XWPFStyles from a package part
		///
		/// @param part the package part holding the data of the styles,
		/// @param rel  the package relationship of type "http://schemas.Openxmlformats.org/officeDocument/2006/relationships/styles"
		public XWPFStyles(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
		}

		/// Construct XWPFStyles from scratch for a new document.
		public XWPFStyles()
		{
		}

		/// Read document
		internal override void OnDocumentRead()
		{
			try
			{
				XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(GetPackagePart().GetInputStream());
				StylesDocument stylesDocument = StylesDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
				ctStyles = stylesDocument.Styles;
				latentStyles = new XWPFLatentStyles(ctStyles.latentStyles, this);
			}
			catch (XmlException ex)
			{
				throw new POIXMLException("Unable to read styles", ex);
			}
			foreach (CT_Style style in ctStyles.GetStyleList())
			{
				listStyle.Add(new XWPFStyle(style, this));
			}
		}

		protected override void Commit()
		{
			if (ctStyles == null)
			{
				throw new InvalidOperationException("Unable to write out styles that were never read in!");
			}
			PackagePart packagePart = GetPackagePart();
			using (Stream stream = packagePart.GetOutputStream())
			{
				StylesDocument stylesDocument = new StylesDocument(ctStyles);
				stylesDocument.Save(stream);
			}
		}

		/// Sets the ctStyles
		/// @param styles
		public void SetStyles(CT_Styles styles)
		{
			ctStyles = styles;
		}

		/// Checks whether style with styleID exist
		/// @param styleID		styleID of the Style in the style-Document
		/// @return				true if style exist, false if style not exist
		public bool StyleExist(string styleID)
		{
			foreach (XWPFStyle item in listStyle)
			{
				if (item.StyleId.Equals(styleID))
				{
					return true;
				}
			}
			return false;
		}

		/// add a style to the document
		/// @param style				
		/// @throws IOException		 
		public void AddStyle(XWPFStyle style)
		{
			listStyle.Add(style);
			ctStyles.AddNewStyle();
			int pos = ctStyles.GetStyleList().Count - 1;
			ctStyles.SetStyleArray(pos, style.GetCTStyle());
		}

		/// get style by a styleID 
		///  @param styleID	styleID of the searched style
		///  @return style
		public XWPFStyle GetStyle(string styleID)
		{
			foreach (XWPFStyle item in listStyle)
			{
				if (item.StyleId.Equals(styleID))
				{
					return item;
				}
			}
			return null;
		}

		/// Get the styles which are related to the parameter style and their relatives
		/// this method can be used to copy all styles from one document to another document 
		/// @param style
		/// @return a list of all styles which were used by this method 
		public List<XWPFStyle> GetUsedStyleList(XWPFStyle style)
		{
			List<XWPFStyle> list = new List<XWPFStyle>();
			list.Add(style);
			return GetUsedStyleList(style, list);
		}

		/// Get the styles which are related to parameter style
		/// @param style
		/// @return all Styles of the parameterList
		private List<XWPFStyle> GetUsedStyleList(XWPFStyle style, List<XWPFStyle> usedStyleList)
		{
			string basisStyleID = style.BasisStyleID;
			XWPFStyle style2 = GetStyle(basisStyleID);
			if (style2 != null && !usedStyleList.Contains(style2))
			{
				usedStyleList.Add(style2);
				GetUsedStyleList(style2, usedStyleList);
			}
			string linkStyleID = style.LinkStyleID;
			XWPFStyle style3 = GetStyle(linkStyleID);
			if (style3 != null && !usedStyleList.Contains(style3))
			{
				usedStyleList.Add(style3);
				GetUsedStyleList(style3, usedStyleList);
			}
			string nextStyleID = style.NextStyleID;
			XWPFStyle style4 = GetStyle(nextStyleID);
			if (style4 != null && !usedStyleList.Contains(style4))
			{
				usedStyleList.Add(style3);
				GetUsedStyleList(style3, usedStyleList);
			}
			return usedStyleList;
		}

		/// Sets the default spelling language on ctStyles DocDefaults parameter
		/// @param strSpellingLanguage
		public void SetSpellingLanguage(string strSpellingLanguage)
		{
			CT_DocDefaults cT_DocDefaults = null;
			CT_RPr cT_RPr = null;
			CT_Language cT_Language = null;
			if (ctStyles.IsSetDocDefaults())
			{
				cT_DocDefaults = ctStyles.docDefaults;
				if (cT_DocDefaults.IsSetRPrDefault())
				{
					CT_RPrDefault rPrDefault = cT_DocDefaults.rPrDefault;
					if (rPrDefault.IsSetRPr())
					{
						cT_RPr = rPrDefault.rPr;
						if (cT_RPr.IsSetLang())
						{
							cT_Language = cT_RPr.lang;
						}
					}
				}
			}
			if (cT_DocDefaults == null)
			{
				cT_DocDefaults = ctStyles.AddNewDocDefaults();
			}
			if (cT_RPr == null)
			{
				cT_RPr = cT_DocDefaults.AddNewRPrDefault().AddNewRPr();
			}
			if (cT_Language == null)
			{
				cT_Language = cT_RPr.AddNewLang();
			}
			cT_Language.val = strSpellingLanguage;
			cT_Language.bidi = strSpellingLanguage;
		}

		/// Sets the default East Asia spelling language on ctStyles DocDefaults parameter
		/// @param strEastAsia
		public void SetEastAsia(string strEastAsia)
		{
			CT_DocDefaults cT_DocDefaults = null;
			CT_RPr cT_RPr = null;
			CT_Language cT_Language = null;
			if (ctStyles.IsSetDocDefaults())
			{
				cT_DocDefaults = ctStyles.docDefaults;
				if (cT_DocDefaults.IsSetRPrDefault())
				{
					CT_RPrDefault rPrDefault = cT_DocDefaults.rPrDefault;
					if (rPrDefault.IsSetRPr())
					{
						cT_RPr = rPrDefault.rPr;
						if (cT_RPr.IsSetLang())
						{
							cT_Language = cT_RPr.lang;
						}
					}
				}
			}
			if (cT_DocDefaults == null)
			{
				cT_DocDefaults = ctStyles.AddNewDocDefaults();
			}
			if (cT_RPr == null)
			{
				cT_RPr = cT_DocDefaults.AddNewRPrDefault().AddNewRPr();
			}
			if (cT_Language == null)
			{
				cT_Language = cT_RPr.AddNewLang();
			}
			cT_Language.eastAsia = strEastAsia;
		}

		/// Sets the default font on ctStyles DocDefaults parameter
		/// @param fonts
		public void SetDefaultFonts(CT_Fonts fonts)
		{
			CT_DocDefaults cT_DocDefaults = null;
			CT_RPr cT_RPr = null;
			if (ctStyles.IsSetDocDefaults())
			{
				cT_DocDefaults = ctStyles.docDefaults;
				if (cT_DocDefaults.IsSetRPrDefault())
				{
					CT_RPrDefault rPrDefault = cT_DocDefaults.rPrDefault;
					if (rPrDefault.IsSetRPr())
					{
						cT_RPr = rPrDefault.rPr;
					}
				}
			}
			if (cT_DocDefaults == null)
			{
				cT_DocDefaults = ctStyles.AddNewDocDefaults();
			}
			if (cT_RPr == null)
			{
				cT_RPr = cT_DocDefaults.AddNewRPrDefault().AddNewRPr();
			}
			cT_RPr.rFonts = fonts;
		}

		/// Get latentstyles
		public XWPFLatentStyles GetLatentStyles()
		{
			return latentStyles;
		}

		/// Get the style with the same name
		/// if this style is not existing, return null
		public XWPFStyle GetStyleWithSameName(XWPFStyle style)
		{
			foreach (XWPFStyle item in listStyle)
			{
				if (item.HasSameName(style))
				{
					return item;
				}
			}
			return null;
		}
	}
}
