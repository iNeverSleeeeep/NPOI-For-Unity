using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Picture;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.XWPF.UserModel
{
	/// XWPFrun.object defines a region of text with a common Set of properties
	///
	/// @author Yegor Kozlov
	/// @author Gregg Morris (gregg dot morris at gmail dot com) - added getColor(), setColor()
	public class XWPFRun
	{
		private CT_R run;

		private string pictureText;

		private XWPFParagraph paragraph;

		private List<XWPFPicture> pictures;

		/// Get the currenty referenced paragraph object
		/// @return current paragraph
		public XWPFParagraph Paragraph
		{
			get
			{
				return paragraph;
			}
		}

		/// @return The {@link XWPFDocument} instance, this run.belongs to, or
		///         <code>null</code> if parent structure (paragraph &gt; document) is not properly Set.
		public XWPFDocument Document
		{
			get
			{
				if (paragraph != null)
				{
					return paragraph.Document;
				}
				return null;
			}
		}

		/// Whether the bold property shall be applied to all non-complex script
		/// characters in the contents of this run.when displayed in a document
		///
		/// @return <code>true</code> if the bold property is applied
		public bool IsBold
		{
			get
			{
				CT_RPr rPr = run.rPr;
				if (rPr == null || !rPr.IsSetB())
				{
					return false;
				}
				return IsCTOnOff(rPr.b);
			}
		}

		/// Returns text embedded in pictures
		public string PictureText
		{
			get
			{
				return pictureText;
			}
		}

		/// Whether the italic property should be applied to all non-complex script
		/// characters in the contents of this run.when displayed in a document.
		///
		/// @return <code>true</code> if the italic property is applied
		public bool IsItalic
		{
			get
			{
				CT_RPr rPr = run.rPr;
				if (rPr == null || !rPr.IsSetI())
				{
					return false;
				}
				return IsCTOnOff(rPr.i);
			}
			set
			{
				CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
				CT_OnOff cT_OnOff = cT_RPr.IsSetI() ? cT_RPr.i : cT_RPr.AddNewI();
				cT_OnOff.val = value;
			}
		}

		/// Specifies that the contents of this run.should be displayed along with an
		/// underline appearing directly below the character heigh
		///
		/// @return the Underline pattern Applyed to this run
		/// @see UnderlinePatterns
		public UnderlinePatterns Underline
		{
			get
			{
				CT_RPr rPr = run.rPr;
				if (rPr == null || !rPr.IsSetU())
				{
					return UnderlinePatterns.None;
				}
				return EnumConverter.ValueOf<UnderlinePatterns, ST_Underline>(rPr.u.val);
			}
		}

		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < run.Items.Count; i++)
				{
					object obj = run.Items[i];
					if (obj is CT_Text && run.ItemsElementName[i] != RunItemsChoiceType.instrText)
					{
						stringBuilder.Append(((CT_Text)obj).Value);
					}
					if (obj is CT_PTab)
					{
						stringBuilder.Append("\t");
					}
					if (obj is CT_Br)
					{
						stringBuilder.Append("\n");
					}
					if (obj is CT_Empty)
					{
						if (run.ItemsElementName[i] == RunItemsChoiceType.tab)
						{
							stringBuilder.Append("\t");
						}
						if (run.ItemsElementName[i] == RunItemsChoiceType.br)
						{
							stringBuilder.Append("\n");
						}
						if (run.ItemsElementName[i] == RunItemsChoiceType.cr)
						{
							stringBuilder.Append("\n");
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		/// Specifies that the contents of this run.shall be displayed with a single
		/// horizontal line through the center of the line.
		///
		/// @return <code>true</code> if the strike property is applied
		public bool IsStrike
		{
			get
			{
				CT_RPr rPr = run.rPr;
				if (rPr == null || !rPr.IsSetStrike())
				{
					return false;
				}
				return IsCTOnOff(rPr.strike);
			}
		}

		/// Specifies the alignment which shall be applied to the contents of this
		/// run.in relation to the default appearance of the run.s text.
		/// This allows the text to be repositioned as subscript or superscript without
		/// altering the font size of the run.properties.
		///
		/// @return VerticalAlign
		/// @see VerticalAlign all possible value that could be Applyed to this run
		public VerticalAlign Subscript
		{
			get
			{
				CT_RPr rPr = run.rPr;
				if (rPr == null || !rPr.IsSetVertAlign())
				{
					return VerticalAlign.BASELINE;
				}
				return EnumConverter.ValueOf<VerticalAlign, ST_VerticalAlignRun>(rPr.vertAlign.val);
			}
			set
			{
				CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
				CT_VerticalAlignRun cT_VerticalAlignRun = cT_RPr.IsSetVertAlign() ? cT_RPr.vertAlign : cT_RPr.AddNewVertAlign();
				cT_VerticalAlignRun.val = EnumConverter.ValueOf<ST_VerticalAlignRun, VerticalAlign>(value);
			}
		}

		/// Specifies the fonts which shall be used to display the text contents of
		/// this run. Specifies a font which shall be used to format all characters
		/// in the ASCII range (0 - 127) within the parent run
		///
		/// @return a string representing the font family
		public string FontFamily
		{
			get
			{
				CT_RPr rPr = run.rPr;
				if (rPr == null || !rPr.IsSetRFonts())
				{
					return null;
				}
				return rPr.rFonts.ascii;
			}
			set
			{
				CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
				CT_Fonts cT_Fonts = cT_RPr.IsSetRFonts() ? cT_RPr.rFonts : cT_RPr.AddNewRFonts();
				cT_Fonts.ascii = value;
			}
		}

		/// Specifies the font size which shall be applied to all non complex script
		/// characters in the contents of this run.when displayed.
		///
		/// @return value representing the font size
		public int FontSize
		{
			get
			{
				CT_RPr rPr = run.rPr;
				if (rPr == null || !rPr.IsSetSz())
				{
					return -1;
				}
				return (int)rPr.sz.val / 2;
			}
			set
			{
				CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
				CT_HpsMeasure cT_HpsMeasure = cT_RPr.IsSetSz() ? cT_RPr.sz : cT_RPr.AddNewSz();
				cT_HpsMeasure.val = (ulong)((long)value * 2L);
			}
		}

		/// @param r the CT_R bean which holds the run.attributes
		/// @param p the parent paragraph
		public XWPFRun(CT_R r, XWPFParagraph p)
		{
			run = r;
			paragraph = p;
			IList<CT_Drawing> drawingList = r.GetDrawingList();
			foreach (CT_Drawing item2 in drawingList)
			{
				List<CT_Anchor> anchorList = item2.GetAnchorList();
				foreach (CT_Anchor item3 in anchorList)
				{
					if (item3.docPr != null)
					{
						Document.DrawingIdManager.Reserve(item3.docPr.id);
					}
				}
				List<CT_Inline> inlineList = item2.GetInlineList();
				foreach (CT_Inline item4 in inlineList)
				{
					if (item4.docPr != null)
					{
						Document.DrawingIdManager.Reserve(item4.docPr.id);
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			List<object> list = new List<object>();
			foreach (NPOI.OpenXmlFormats.Wordprocessing.CT_Picture pict in r.GetPictList())
			{
				list.Add(pict);
			}
			foreach (CT_Drawing item5 in drawingList)
			{
				list.Add(item5);
			}
			pictureText = stringBuilder.ToString();
			pictures = new List<XWPFPicture>();
			foreach (object item6 in list)
			{
				foreach (NPOI.OpenXmlFormats.Dml.Picture.CT_Picture cTPicture in GetCTPictures(item6))
				{
					XWPFPicture item = new XWPFPicture(cTPicture, this);
					pictures.Add(item);
				}
			}
		}

		private List<NPOI.OpenXmlFormats.Dml.Picture.CT_Picture> GetCTPictures(object o)
		{
			List<NPOI.OpenXmlFormats.Dml.Picture.CT_Picture> result = new List<NPOI.OpenXmlFormats.Dml.Picture.CT_Picture>();
			if (o is CT_Drawing)
			{
				CT_Drawing cT_Drawing = o as CT_Drawing;
				if (cT_Drawing.inline != null)
				{
					foreach (CT_Inline item in cT_Drawing.inline)
					{
						GetPictures(item.graphic.graphicData, result);
					}
					return result;
				}
			}
			else if (o is CT_GraphicalObjectData)
			{
				GetPictures(o as CT_GraphicalObjectData, result);
			}
			return result;
		}

		private void GetPictures(CT_GraphicalObjectData god, List<NPOI.OpenXmlFormats.Dml.Picture.CT_Picture> pictures)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(NPOI.OpenXmlFormats.Dml.Picture.CT_Picture));
			foreach (string item2 in god.Any)
			{
				StringReader input = new StringReader(item2);
				NPOI.OpenXmlFormats.Dml.Picture.CT_Picture item = xmlSerializer.Deserialize(XmlReader.Create(input)) as NPOI.OpenXmlFormats.Dml.Picture.CT_Picture;
				pictures.Add(item);
			}
		}

		/// Get the currently used CT_R object
		/// @return CT_R object
		public CT_R GetCTR()
		{
			return run;
		}

		/// For isBold, isItalic etc
		private bool IsCTOnOff(CT_OnOff onoff)
		{
			if (!onoff.IsSetVal())
			{
				return true;
			}
			return onoff.val;
		}

		/// Whether the bold property shall be applied to all non-complex script
		/// characters in the contents of this run.when displayed in a document. 
		/// <p>
		/// This formatting property is a toggle property, which specifies that its
		/// behavior differs between its use within a style defInition and its use as
		/// direct formatting. When used as part of a style defInition, Setting this
		/// property shall toggle the current state of that property as specified up
		/// to this point in the hierarchy (i.e. applied to not applied, and vice
		/// versa). Setting it to <code>false</code> (or an equivalent) shall
		/// result in the current Setting remaining unChanged. However, when used as
		/// direct formatting, Setting this property to true or false shall Set the
		/// absolute state of the resulting property.
		/// </p>
		/// <p>
		/// If this element is not present, the default value is to leave the
		/// formatting applied at previous level in the style hierarchy. If this
		/// element is never applied in the style hierarchy, then bold shall not be
		/// applied to non-complex script characters.
		/// </p>
		///
		/// @param value <code>true</code> if the bold property is applied to
		///              this run
		public void SetBold(bool value)
		{
			CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
			CT_OnOff cT_OnOff = cT_RPr.IsSetB() ? cT_RPr.b : cT_RPr.AddNewB();
			cT_OnOff.val = value;
		}

		/// Get text color. The returned value is a string in the hex form "RRGGBB".
		public string GetColor()
		{
			string result = null;
			if (run.IsSetRPr())
			{
				CT_RPr rPr = run.rPr;
				if (rPr.IsSetColor())
				{
					NPOI.OpenXmlFormats.Wordprocessing.CT_Color color = rPr.color;
					result = color.val;
				}
			}
			return result;
		}

		/// Set text color.
		/// @param rgbStr - the desired color, in the hex form "RRGGBB".
		public void SetColor(string rgbStr)
		{
			CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
			NPOI.OpenXmlFormats.Wordprocessing.CT_Color cT_Color = cT_RPr.IsSetColor() ? cT_RPr.color : cT_RPr.AddNewColor();
			cT_Color.val = rgbStr;
		}

		/// Return the string content of this text run
		///
		/// @return the text of this text run.or <code>null</code> if not Set
		public string GetText(int pos)
		{
			if (run.SizeOfTArray() != 0)
			{
				return run.GetTArray(pos).Value;
			}
			return null;
		}

		public void ReplaceText(string oldText, string newText)
		{
			string text = Text.Replace(oldText, newText);
			SetText(text);
		}

		/// Sets the text of this text run
		///
		/// @param value the literal text which shall be displayed in the document
		public void SetText(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			run.Items.Clear();
			run.ItemsElementName.Clear();
			char[] array = value.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '\n')
				{
					ArrayList items = run.Items;
					CT_Text cT_Text = new CT_Text();
					cT_Text.Value = stringBuilder.ToString();
					items.Add(cT_Text);
					run.ItemsElementName.Add(RunItemsChoiceType.instrText);
					stringBuilder = stringBuilder.Remove(0, stringBuilder.Length);
					run.Items.Add(new CT_Br());
					run.ItemsElementName.Add(RunItemsChoiceType.br);
				}
				else if (array[i] == '\t')
				{
					ArrayList items2 = run.Items;
					CT_Text cT_Text2 = new CT_Text();
					cT_Text2.Value = stringBuilder.ToString();
					items2.Add(cT_Text2);
					run.ItemsElementName.Add(RunItemsChoiceType.instrText);
					stringBuilder = stringBuilder.Remove(0, stringBuilder.Length);
					run.Items.Add(new CT_PTab());
					run.ItemsElementName.Add(RunItemsChoiceType.ptab);
				}
				else
				{
					stringBuilder.Append(array[i]);
				}
			}
			if (stringBuilder.Length > 0)
			{
				ArrayList items3 = run.Items;
				CT_Text cT_Text3 = new CT_Text();
				cT_Text3.Value = stringBuilder.ToString();
				items3.Add(cT_Text3);
				run.ItemsElementName.Add(RunItemsChoiceType.instrText);
			}
		}

		public void AppendText(string value)
		{
			SetText(value, run.GetTList().Count);
		}

		/// Sets the text of this text run.in the 
		///
		/// @param value the literal text which shall be displayed in the document
		/// @param pos - position in the text array (NB: 0 based)
		public void SetText(string value, int pos)
		{
			int num = run.SizeOfTArray();
			if (pos > num)
			{
				throw new IndexOutOfRangeException("Value too large for the parameter position in XWPFrun.Text=(String value,int pos)");
			}
			CT_Text cT_Text = (pos < num && pos >= 0) ? run.GetTArray(pos) : run.AddNewT();
			cT_Text.Value = value;
			preserveSpaces(cT_Text);
		}

		internal void InsertText(CT_Text text, int textIndex)
		{
			run.GetTList().Insert(textIndex, text);
		}

		/// <summary>
		/// insert text at start index in the run
		/// </summary>
		/// <param name="text">insert text</param>
		/// <param name="startIndex">start index of the insertion in the run text</param>
		public void InsertText(string text, int startIndex)
		{
			List<CT_Text> tList = run.GetTList();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (true)
			{
				if (num3 >= tList.Count)
				{
					return;
				}
				num2 = num;
				num += tList[num3].Value.Length;
				if (num > startIndex)
				{
					break;
				}
				num3++;
			}
			tList[num3].Value = tList[num3].Value.Insert(startIndex - num2, text);
		}

		/// Specifies that the contents of this run.should be displayed along with an
		/// underline appearing directly below the character heigh
		/// If this element is not present, the default value is to leave the
		/// formatting applied at previous level in the style hierarchy. If this
		/// element is never applied in the style hierarchy, then an underline shall
		/// not be applied to the contents of this run.
		///
		/// @param value -
		///              underline type
		/// @see UnderlinePatterns : all possible patterns that could be applied
		public void SetUnderline(UnderlinePatterns value)
		{
			CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
			CT_Underline cT_Underline = (cT_RPr.u == null) ? cT_RPr.AddNewU() : cT_RPr.u;
			cT_Underline.val = EnumConverter.ValueOf<ST_Underline, UnderlinePatterns>(value);
		}

		public void SetStrike(bool value)
		{
			CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
			CT_OnOff cT_OnOff = cT_RPr.IsSetStrike() ? cT_RPr.strike : cT_RPr.AddNewStrike();
			cT_OnOff.val = value;
		}

		/// This element specifies the amount by which text shall be raised or
		/// lowered for this run.in relation to the default baseline of the
		/// surrounding non-positioned text. This allows the text to be repositioned
		/// without altering the font size of the contents.
		///
		/// @return a big integer representing the amount of text shall be "moved"
		public int GetTextPosition()
		{
			CT_RPr rPr = run.rPr;
			if (rPr == null || !rPr.IsSetPosition())
			{
				return -1;
			}
			return int.Parse(rPr.position.val);
		}

		/// This element specifies the amount by which text shall be raised or
		/// lowered for this run.in relation to the default baseline of the
		/// surrounding non-positioned text. This allows the text to be repositioned
		/// without altering the font size of the contents.
		///
		/// If the val attribute is positive, then the parent run.shall be raised
		/// above the baseline of the surrounding text by the specified number of
		/// half-points. If the val attribute is negative, then the parent run.shall
		/// be lowered below the baseline of the surrounding text by the specified
		/// number of half-points.
		///         * 
		/// If this element is not present, the default value is to leave the
		/// formatting applied at previous level in the style hierarchy. If this
		/// element is never applied in the style hierarchy, then the text shall not
		/// be raised or lowered relative to the default baseline location for the
		/// contents of this run.
		public void SetTextPosition(int val)
		{
			CT_RPr cT_RPr = run.IsSetRPr() ? run.rPr : run.AddNewRPr();
			CT_SignedHpsMeasure cT_SignedHpsMeasure = cT_RPr.IsSetPosition() ? cT_RPr.position : cT_RPr.AddNewPosition();
			cT_SignedHpsMeasure.val = val.ToString();
		}

		public void RemoveBreak()
		{
		}

		/// Specifies that a break shall be placed at the current location in the run
		/// content. 
		/// A break is a special character which is used to override the
		/// normal line breaking that would be performed based on the normal layout
		/// of the document's contents. 
		/// @see #AddCarriageReturn() 
		public void AddBreak()
		{
			run.AddNewBr();
		}

		/// Specifies that a break shall be placed at the current location in the run
		/// content.
		/// A break is a special character which is used to override the
		/// normal line breaking that would be performed based on the normal layout
		/// of the document's contents.
		/// <p>
		/// The behavior of this break character (the
		/// location where text shall be restarted After this break) shall be
		/// determined by its type values.
		/// </p>
		/// @see BreakType
		public void AddBreak(BreakType type)
		{
			CT_Br cT_Br = run.AddNewBr();
			cT_Br.type = EnumConverter.ValueOf<ST_BrType, BreakType>(type);
		}

		/// Specifies that a break shall be placed at the current location in the run
		/// content. A break is a special character which is used to override the
		/// normal line breaking that would be performed based on the normal layout
		/// of the document's contents.
		/// <p>
		/// The behavior of this break character (the
		/// location where text shall be restarted After this break) shall be
		/// determined by its type (in this case is BreakType.TEXT_WRAPPING as default) and clear attribute values.
		/// </p>
		/// @see BreakClear
		public void AddBreak(BreakClear Clear)
		{
			CT_Br cT_Br = run.AddNewBr();
			cT_Br.type = EnumConverter.ValueOf<ST_BrType, BreakType>(BreakType.TEXTWRAPPING);
			cT_Br.clear = EnumConverter.ValueOf<ST_BrClear, BreakClear>(Clear);
		}

		/// Specifies that a carriage return shall be placed at the
		/// current location in the run.content.
		/// A carriage return is used to end the current line of text in
		/// WordProcess.
		/// The behavior of a carriage return in run.content shall be
		/// identical to a break character with null type and clear attributes, which
		/// shall end the current line and find the next available line on which to
		/// continue.
		/// The carriage return character forced the following text to be
		/// restarted on the next available line in the document.
		public void AddCarriageReturn()
		{
			run.AddNewCr();
		}

		public void RemoveCarriageReturn(int i)
		{
			throw new NotImplementedException();
		}

		/// Adds a picture to the run. This method handles
		///  attaching the picture data to the overall file.
		///
		/// @see NPOI.XWPF.UserModel.Document#PICTURE_TYPE_EMF
		/// @see NPOI.XWPF.UserModel.Document#PICTURE_TYPE_WMF
		/// @see NPOI.XWPF.UserModel.Document#PICTURE_TYPE_PICT
		/// @see NPOI.XWPF.UserModel.Document#PICTURE_TYPE_JPEG
		/// @see NPOI.XWPF.UserModel.Document#PICTURE_TYPE_PNG
		/// @see NPOI.XWPF.UserModel.Document#PICTURE_TYPE_DIB
		///
		/// @param pictureData The raw picture data
		/// @param pictureType The type of the picture, eg {@link Document#PICTURE_TYPE_JPEG}
		/// @param width width in EMUs. To convert to / from points use {@link org.apache.poi.util.Units}
		/// @param height height in EMUs. To convert to / from points use {@link org.apache.poi.util.Units}
		/// @throws NPOI.Openxml4j.exceptions.InvalidFormatException 
		/// @throws IOException 
		public XWPFPicture AddPicture(Stream pictureData, int pictureType, string filename, int width, int height)
		{
			XWPFDocument document = paragraph.Document;
			string id = document.AddPictureData(pictureData, pictureType);
			XWPFPictureData xWPFPictureData = (XWPFPictureData)document.GetRelationById(id);
			CT_Drawing cT_Drawing = run.AddNewDrawing();
			CT_Inline cT_Inline = cT_Drawing.AddNewInline();
			new XmlDocument();
			cT_Inline.graphic = new CT_GraphicalObject();
			cT_Inline.graphic.graphicData = new CT_GraphicalObjectData();
			cT_Inline.graphic.graphicData.uri = "http://schemas.openxmlformats.org/drawingml/2006/picture";
			cT_Inline.distT = 0u;
			cT_Inline.distR = 0u;
			cT_Inline.distB = 0u;
			cT_Inline.distL = 0u;
			NPOI.OpenXmlFormats.Dml.WordProcessing.CT_NonVisualDrawingProps cT_NonVisualDrawingProps = cT_Inline.AddNewDocPr();
			long num = Paragraph.Document.DrawingIdManager.ReserveNew();
			cT_NonVisualDrawingProps.id = (uint)num;
			cT_NonVisualDrawingProps.name = "Drawing " + num;
			cT_NonVisualDrawingProps.descr = filename;
			NPOI.OpenXmlFormats.Dml.WordProcessing.CT_PositiveSize2D cT_PositiveSize2D = cT_Inline.AddNewExtent();
			cT_PositiveSize2D.cx = width;
			cT_PositiveSize2D.cy = height;
			NPOI.OpenXmlFormats.Dml.Picture.CT_Picture cT_Picture = new NPOI.OpenXmlFormats.Dml.Picture.CT_Picture();
			CT_PictureNonVisual cT_PictureNonVisual = cT_Picture.AddNewNvPicPr();
			NPOI.OpenXmlFormats.Dml.CT_NonVisualDrawingProps cT_NonVisualDrawingProps2 = cT_PictureNonVisual.AddNewCNvPr();
			cT_NonVisualDrawingProps2.id = 0u;
			cT_NonVisualDrawingProps2.name = "Picture " + num;
			cT_NonVisualDrawingProps2.descr = filename;
			CT_NonVisualPictureProperties cT_NonVisualPictureProperties = cT_PictureNonVisual.AddNewCNvPicPr();
			cT_NonVisualPictureProperties.AddNewPicLocks().noChangeAspect = true;
			CT_BlipFillProperties cT_BlipFillProperties = cT_Picture.AddNewBlipFill();
			CT_Blip cT_Blip = cT_BlipFillProperties.AddNewBlip();
			cT_Blip.embed = xWPFPictureData.GetPackageRelationship().Id;
			cT_BlipFillProperties.AddNewStretch().AddNewFillRect();
			CT_ShapeProperties cT_ShapeProperties = cT_Picture.AddNewSpPr();
			CT_Transform2D cT_Transform2D = cT_ShapeProperties.AddNewXfrm();
			CT_Point2D cT_Point2D = cT_Transform2D.AddNewOff();
			cT_Point2D.x = 0L;
			cT_Point2D.y = 0L;
			NPOI.OpenXmlFormats.Dml.CT_PositiveSize2D cT_PositiveSize2D2 = cT_Transform2D.AddNewExt();
			cT_PositiveSize2D2.cx = width;
			cT_PositiveSize2D2.cy = height;
			CT_PresetGeometry2D cT_PresetGeometry2D = cT_ShapeProperties.AddNewPrstGeom();
			cT_PresetGeometry2D.prst = ST_ShapeType.rect;
			cT_PresetGeometry2D.AddNewAvLst();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				cT_Picture.Write(streamWriter, "pic:pic");
				streamWriter.Flush();
				memoryStream.Position = 0L;
				StreamReader streamReader = new StreamReader(memoryStream);
				string el = streamReader.ReadToEnd();
				cT_Inline.graphic.graphicData.AddPicElement(el);
			}
			XWPFPicture xWPFPicture = new XWPFPicture(cT_Picture, this);
			pictures.Add(xWPFPicture);
			return xWPFPicture;
		}

		/// Returns the embedded pictures of the run. These
		///  are pictures which reference an external, 
		///  embedded picture image such as a .png or .jpg
		public List<XWPFPicture> GetEmbeddedPictures()
		{
			return pictures;
		}

		/// Add the xml:spaces="preserve" attribute if the string has leading or trailing white spaces
		///
		/// @param xs    the string to check
		private static void preserveSpaces(CT_Text xs)
		{
			string value = xs.Value;
			if (value != null && (value.StartsWith(" ") || value.EndsWith(" ")))
			{
				xs.space = "preserve";
			}
		}

		/// Returns the string version of the text, with tabs and
		///  carriage returns in place of their xml equivalents.
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Text);
			if (pictureText != null && pictureText.Length > 0)
			{
				stringBuilder.Append("\n").Append(pictureText);
			}
			return stringBuilder.ToString();
		}
	}
}
