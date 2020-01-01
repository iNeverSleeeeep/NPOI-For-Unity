using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.XWPF.UserModel
{
	/// Sketch of XWPF paragraph class
	public class XWPFParagraph : IBodyElement
	{
		private CT_P paragraph;

		protected IBody part;

		/// For access to the document's hyperlink, comments, tables etc 
		protected XWPFDocument document;

		protected List<XWPFRun> runs;

		private StringBuilder footnoteText = new StringBuilder();

		public IList<XWPFRun> Runs
		{
			get
			{
				return runs.AsReadOnly();
			}
		}

		public bool IsEmpty
		{
			get
			{
				return paragraph.Items.Count == 0;
			}
		}

		public XWPFDocument Document
		{
			get
			{
				return document;
			}
		}

		/// Return the textual content of the paragraph, including text from pictures
		/// in it.
		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (XWPFRun run in runs)
				{
					stringBuilder.Append(run.ToString());
				}
				stringBuilder.Append(footnoteText);
				return stringBuilder.ToString();
			}
		}

		/// Return styleID of the paragraph if style exist for this paragraph
		/// if not, null will be returned     
		/// @return		styleID as String
		public string StyleID
		{
			get
			{
				if (paragraph.pPr != null && paragraph.pPr.pStyle != null && paragraph.pPr.pStyle.val != null)
				{
					return paragraph.pPr.pStyle.val;
				}
				return null;
			}
		}

		/// Returns the text of the paragraph, but not of any objects in the
		/// paragraph
		public string ParagraphText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (XWPFRun run in runs)
				{
					stringBuilder.Append(run.ToString());
				}
				return stringBuilder.ToString();
			}
		}

		/// Returns any text from any suitable pictures in the paragraph
		public string PictureText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (XWPFRun run in runs)
				{
					stringBuilder.Append(run.PictureText);
				}
				return stringBuilder.ToString();
			}
		}

		/// Returns the footnote text of the paragraph
		///
		/// @return  the footnote text or empty string if the paragraph does not have footnotes
		public string FootnoteText
		{
			get
			{
				return footnoteText.ToString();
			}
		}

		/// Returns the paragraph alignment which shall be applied to text in this
		/// paragraph.
		/// <p>
		/// If this element is not Set on a given paragraph, its value is determined
		/// by the Setting previously Set at any level of the style hierarchy (i.e.
		/// that previous Setting remains unChanged). If this Setting is never
		/// specified in the style hierarchy, then no alignment is applied to the
		/// paragraph.
		/// </p>
		///
		/// @return the paragraph alignment of this paragraph.
		public ParagraphAlignment Alignment
		{
			get
			{
				CT_PPr cTPPr = GetCTPPr();
				if (cTPPr != null && cTPPr.IsSetJc())
				{
					return EnumConverter.ValueOf<ParagraphAlignment, ST_Jc>(cTPPr.jc.val);
				}
				return ParagraphAlignment.LEFT;
			}
			set
			{
				CT_PPr cTPPr = GetCTPPr();
				CT_Jc cT_Jc = cTPPr.IsSetJc() ? cTPPr.jc : cTPPr.AddNewJc();
				cT_Jc.val = EnumConverter.ValueOf<ST_Jc, ParagraphAlignment>(value);
			}
		}

		/// Returns the text vertical alignment which shall be applied to text in
		/// this paragraph.
		/// <p>
		/// If the line height (before any Added spacing) is larger than one or more
		/// characters on the line, all characters will be aligned to each other as
		/// specified by this element.
		/// </p>
		/// <p>
		/// If this element is omitted on a given paragraph, its value is determined
		/// by the Setting previously Set at any level of the style hierarchy (i.e.
		/// that previous Setting remains unChanged). If this Setting is never
		/// specified in the style hierarchy, then the vertical alignment of all
		/// characters on the line shall be automatically determined by the consumer.
		/// </p>
		///
		/// @return the vertical alignment of this paragraph.
		public TextAlignment VerticalAlignment
		{
			get
			{
				CT_PPr cTPPr = GetCTPPr();
				if (cTPPr != null && cTPPr.IsSetTextAlignment())
				{
					return EnumConverter.ValueOf<TextAlignment, ST_TextAlignment>(cTPPr.textAlignment.val);
				}
				return TextAlignment.AUTO;
			}
			set
			{
				CT_PPr cTPPr = GetCTPPr();
				CT_TextAlignment cT_TextAlignment = cTPPr.IsSetTextAlignment() ? cTPPr.textAlignment : cTPPr.AddNewTextAlignment();
				cT_TextAlignment.val = EnumConverter.ValueOf<ST_TextAlignment, TextAlignment>(value);
			}
		}

		/// <summary>
		/// the top border for the paragraph
		/// </summary>
		public Borders BorderTop
		{
			get
			{
				CT_PBdr cTPBrd = GetCTPBrd(false);
				CT_Border cT_Border = null;
				if (cTPBrd != null)
				{
					cT_Border = cTPBrd.top;
				}
				ST_Border val = (cT_Border == null) ? ST_Border.none : cT_Border.val;
				return EnumConverter.ValueOf<Borders, ST_Border>(val);
			}
			set
			{
				CT_PBdr cTPBrd = GetCTPBrd(true);
				CT_Border cT_Border = (cTPBrd != null && cTPBrd.IsSetTop()) ? cTPBrd.top : cTPBrd.AddNewTop();
				if (value == Borders.NONE)
				{
					cTPBrd.UnsetTop();
				}
				else
				{
					cT_Border.val = EnumConverter.ValueOf<ST_Border, Borders>(value);
				}
			}
		}

		/// <summary>
		///             Specifies the border which shall be displayed below a Set of
		/// paragraphs which have the same Set of paragraph border Settings.
		/// </summary>
		/// <returns>the bottom border for the paragraph</returns>
		public Borders BorderBottom
		{
			get
			{
				CT_PBdr cTPBrd = GetCTPBrd(false);
				CT_Border cT_Border = null;
				if (cTPBrd != null)
				{
					cT_Border = cTPBrd.bottom;
				}
				ST_Border val = (cT_Border == null) ? ST_Border.none : cT_Border.val;
				return EnumConverter.ValueOf<Borders, ST_Border>(val);
			}
			set
			{
				CT_PBdr cTPBrd = GetCTPBrd(true);
				CT_Border cT_Border = cTPBrd.IsSetBottom() ? cTPBrd.bottom : cTPBrd.AddNewBottom();
				if (value == Borders.NONE)
				{
					cTPBrd.UnsetBottom();
				}
				else
				{
					cT_Border.val = EnumConverter.ValueOf<ST_Border, Borders>(value);
				}
			}
		}

		/// <summary>
		/// Specifies the border which shall be displayed on the left side of the
		/// page around the specified paragraph.
		/// </summary>
		/// <returns>the left border for the paragraph</returns>
		public Borders BorderLeft
		{
			get
			{
				CT_PBdr cTPBrd = GetCTPBrd(false);
				CT_Border cT_Border = null;
				if (cTPBrd != null)
				{
					cT_Border = cTPBrd.left;
				}
				ST_Border val = (cT_Border == null) ? ST_Border.none : cT_Border.val;
				return EnumConverter.ValueOf<Borders, ST_Border>(val);
			}
			set
			{
				CT_PBdr cTPBrd = GetCTPBrd(true);
				CT_Border cT_Border = cTPBrd.IsSetLeft() ? cTPBrd.left : cTPBrd.AddNewLeft();
				if (value == Borders.NONE)
				{
					cTPBrd.UnsetLeft();
				}
				else
				{
					cT_Border.val = EnumConverter.ValueOf<ST_Border, Borders>(value);
				}
			}
		}

		/// Specifies the border which shall be displayed on the right side of the
		/// page around the specified paragraph.
		///
		/// @return ParagraphBorder - the right border for the paragraph
		/// @see #setBorderRight(Borders)
		/// @see Borders for a list of all possible borders
		public Borders BorderRight
		{
			get
			{
				CT_PBdr cTPBrd = GetCTPBrd(false);
				CT_Border cT_Border = null;
				if (cTPBrd != null)
				{
					cT_Border = cTPBrd.right;
				}
				ST_Border val = (cT_Border == null) ? ST_Border.none : cT_Border.val;
				return EnumConverter.ValueOf<Borders, ST_Border>(val);
			}
			set
			{
				CT_PBdr cTPBrd = GetCTPBrd(true);
				CT_Border cT_Border = cTPBrd.IsSetRight() ? cTPBrd.right : cTPBrd.AddNewRight();
				if (value == Borders.NONE)
				{
					cTPBrd.UnsetRight();
				}
				else
				{
					cT_Border.val = EnumConverter.ValueOf<ST_Border, Borders>(value);
				}
			}
		}

		public ST_Shd FillPattern
		{
			get
			{
				if (!GetCTPPr().IsSetShd())
				{
					return ST_Shd.nil;
				}
				return GetCTPPr().shd.val;
			}
			set
			{
				CT_Shd cT_Shd = null;
				cT_Shd = (GetCTPPr().IsSetShd() ? GetCTPPr().shd : GetCTPPr().AddNewShd());
				cT_Shd.val = value;
			}
		}

		public string FillBackgroundColor
		{
			get
			{
				if (!GetCTPPr().IsSetShd())
				{
					return null;
				}
				return GetCTPPr().shd.fill;
			}
			set
			{
				CT_Shd cT_Shd = null;
				cT_Shd = (GetCTPPr().IsSetShd() ? GetCTPPr().shd : GetCTPPr().AddNewShd());
				cT_Shd.color = "auto";
				cT_Shd.fill = value;
			}
		}

		/// Specifies the border which shall be displayed between each paragraph in a
		/// Set of paragraphs which have the same Set of paragraph border Settings.
		///
		/// @return ParagraphBorder - the between border for the paragraph
		/// @see #setBorderBetween(Borders)
		/// @see Borders for a list of all possible borders
		public Borders BorderBetween
		{
			get
			{
				CT_PBdr cTPBrd = GetCTPBrd(false);
				CT_Border cT_Border = null;
				if (cTPBrd != null)
				{
					cT_Border = cTPBrd.between;
				}
				ST_Border val = (cT_Border == null) ? ST_Border.none : cT_Border.val;
				return EnumConverter.ValueOf<Borders, ST_Border>(val);
			}
			set
			{
				CT_PBdr cTPBrd = GetCTPBrd(true);
				CT_Border cT_Border = cTPBrd.IsSetBetween() ? cTPBrd.between : cTPBrd.AddNewBetween();
				if (value == Borders.NONE)
				{
					cTPBrd.UnsetBetween();
				}
				else
				{
					cT_Border.val = EnumConverter.ValueOf<ST_Border, Borders>(value);
				}
			}
		}

		/// Specifies that when rendering this document in a paginated
		/// view, the contents of this paragraph are rendered on the start of a new
		/// page in the document.
		/// <p>
		/// If this element is omitted on a given paragraph,
		/// its value is determined by the Setting previously Set at any level of the
		/// style hierarchy (i.e. that previous Setting remains unChanged). If this
		/// Setting is never specified in the style hierarchy, then this property
		/// shall not be applied. Since the paragraph is specified to start on a new
		/// page, it begins page two even though it could have fit on page one.
		/// </p>
		///
		/// @return bool - if page break is Set
		public bool IsPageBreak
		{
			get
			{
				CT_PPr cTPPr = GetCTPPr();
				CT_OnOff cT_OnOff = cTPPr.IsSetPageBreakBefore() ? cTPPr.pageBreakBefore : null;
				if (cT_OnOff != null && cT_OnOff.val)
				{
					return true;
				}
				return false;
			}
			set
			{
				CT_PPr cTPPr = GetCTPPr();
				CT_OnOff cT_OnOff = cTPPr.IsSetPageBreakBefore() ? cTPPr.pageBreakBefore : cTPPr.AddNewPageBreakBefore();
				cT_OnOff.val = value;
			}
		}

		/// Specifies the spacing that should be Added After the last line in this
		/// paragraph in the document in absolute units.
		///
		/// @return int - value representing the spacing After the paragraph
		public int SpacingAfter
		{
			get
			{
				CT_Spacing cTSpacing = GetCTSpacing(false);
				if (cTSpacing == null || !cTSpacing.IsSetAfter())
				{
					return -1;
				}
				return (int)cTSpacing.after;
			}
			set
			{
				CT_Spacing cTSpacing = GetCTSpacing(true);
				if (cTSpacing != null)
				{
					cTSpacing.after = (ulong)value;
				}
			}
		}

		/// Specifies the spacing that should be Added After the last line in this
		/// paragraph in the document in absolute units.
		///
		/// @return bigint - value representing the spacing After the paragraph
		/// @see #setSpacingAfterLines(int)
		public int SpacingAfterLines
		{
			get
			{
				CT_Spacing cTSpacing = GetCTSpacing(false);
				if (cTSpacing == null || !cTSpacing.IsSetAfterLines())
				{
					return -1;
				}
				return int.Parse(cTSpacing.afterLines);
			}
			set
			{
				CT_Spacing cTSpacing = GetCTSpacing(true);
				cTSpacing.afterLines = value.ToString();
			}
		}

		/// Specifies the spacing that should be Added above the first line in this
		/// paragraph in the document in absolute units.
		///
		/// @return the spacing that should be Added above the first line
		/// @see #setSpacingBefore(int)
		public int SpacingBefore
		{
			get
			{
				CT_Spacing cTSpacing = GetCTSpacing(false);
				if (cTSpacing == null || !cTSpacing.IsSetBefore())
				{
					return -1;
				}
				return (int)cTSpacing.before;
			}
			set
			{
				CT_Spacing cTSpacing = GetCTSpacing(true);
				cTSpacing.before = (ulong)value;
			}
		}

		/// Specifies the spacing that should be Added before the first line in this paragraph in the
		/// document in line units.
		/// The value of this attribute is specified in one hundredths of a line.
		///
		/// @return the spacing that should be Added before the first line in this paragraph
		/// @see #setSpacingBeforeLines(int)
		public int SpacingBeforeLines
		{
			get
			{
				CT_Spacing cTSpacing = GetCTSpacing(false);
				if (cTSpacing == null || !cTSpacing.IsSetBeforeLines())
				{
					return -1;
				}
				return int.Parse(cTSpacing.beforeLines);
			}
			set
			{
				CT_Spacing cTSpacing = GetCTSpacing(true);
				cTSpacing.beforeLines = value.ToString();
			}
		}

		/// Specifies how the spacing between lines is calculated as stored in the
		/// line attribute. If this attribute is omitted, then it shall be assumed to
		/// be of a value auto if a line attribute value is present.
		///
		/// @return rule
		/// @see LineSpacingRule
		/// @see #setSpacingLineRule(LineSpacingRule)
		public LineSpacingRule SpacingLineRule
		{
			get
			{
				CT_Spacing cTSpacing = GetCTSpacing(false);
				if (cTSpacing == null || !cTSpacing.IsSetLineRule())
				{
					return LineSpacingRule.AUTO;
				}
				return EnumConverter.ValueOf<LineSpacingRule, ST_LineSpacingRule>(cTSpacing.lineRule);
			}
			set
			{
				CT_Spacing cTSpacing = GetCTSpacing(true);
				cTSpacing.lineRule = EnumConverter.ValueOf<ST_LineSpacingRule, LineSpacingRule>(value);
			}
		}

		/// Specifies the indentation which shall be placed between the left text
		/// margin for this paragraph and the left edge of that paragraph's content
		/// in a left to right paragraph, and the right text margin and the right
		/// edge of that paragraph's text in a right to left paragraph
		/// <p>
		/// If this attribute is omitted, its value shall be assumed to be zero.
		/// Negative values are defined such that the text is Moved past the text margin,
		/// positive values Move the text inside the text margin.
		/// </p>
		///
		/// @return indentation or null if indentation is not Set
		public int IndentationLeft
		{
			get
			{
				CT_Ind cTInd = GetCTInd(false);
				if (cTInd == null || !cTInd.IsSetLeft())
				{
					return -1;
				}
				return int.Parse(cTInd.left);
			}
			set
			{
				CT_Ind cTInd = GetCTInd(true);
				cTInd.left = value.ToString();
			}
		}

		/// Specifies the indentation which shall be placed between the right text
		/// margin for this paragraph and the right edge of that paragraph's content
		/// in a left to right paragraph, and the right text margin and the right
		/// edge of that paragraph's text in a right to left paragraph
		/// <p>
		/// If this attribute is omitted, its value shall be assumed to be zero.
		/// Negative values are defined such that the text is Moved past the text margin,
		/// positive values Move the text inside the text margin.
		/// </p>
		///
		/// @return indentation or null if indentation is not Set
		public int IndentationRight
		{
			get
			{
				CT_Ind cTInd = GetCTInd(false);
				if (cTInd == null || !cTInd.IsSetRight())
				{
					return -1;
				}
				return int.Parse(cTInd.right);
			}
			set
			{
				CT_Ind cTInd = GetCTInd(true);
				cTInd.right = value.ToString();
			}
		}

		/// Specifies the indentation which shall be Removed from the first line of
		/// the parent paragraph, by moving the indentation on the first line back
		/// towards the beginning of the direction of text flow.
		/// This indentation is
		/// specified relative to the paragraph indentation which is specified for
		/// all other lines in the parent paragraph.
		/// The firstLine and hanging
		/// attributes are mutually exclusive, if both are specified, then the
		/// firstLine value is ignored.
		///
		/// @return indentation or null if indentation is not Set
		public int IndentationHanging
		{
			get
			{
				CT_Ind cTInd = GetCTInd(false);
				if (cTInd == null || !cTInd.IsSetHanging())
				{
					return -1;
				}
				return (int)cTInd.hanging;
			}
			set
			{
				CT_Ind cTInd = GetCTInd(true);
				cTInd.hanging = (ulong)value;
			}
		}

		/// Specifies the Additional indentation which shall be applied to the first
		/// line of the parent paragraph. This Additional indentation is specified
		/// relative to the paragraph indentation which is specified for all other
		/// lines in the parent paragraph.
		/// The firstLine and hanging attributes are
		/// mutually exclusive, if both are specified, then the firstLine value is
		/// ignored.
		/// If the firstLineChars attribute is also specified, then this
		/// value is ignored.
		/// If this attribute is omitted, then its value shall be
		/// assumed to be zero (if needed).
		///
		/// @return indentation or null if indentation is not Set
		public int IndentationFirstLine
		{
			get
			{
				CT_Ind cTInd = GetCTInd(false);
				if (cTInd == null || !cTInd.IsSetFirstLine())
				{
					return -1;
				}
				return (int)cTInd.firstLine;
			}
			set
			{
				CT_Ind cTInd = GetCTInd(true);
				cTInd.firstLine = value;
			}
		}

		/// This element specifies whether a consumer shall break Latin text which
		/// exceeds the text extents of a line by breaking the word across two lines
		/// (breaking on the character level) or by moving the word to the following
		/// line (breaking on the word level).
		///
		/// @return bool
		public bool IsWordWrap
		{
			get
			{
				CT_OnOff cT_OnOff = GetCTPPr().IsSetWordWrap() ? GetCTPPr().wordWrap : null;
				if (cT_OnOff != null)
				{
					return cT_OnOff.val;
				}
				return false;
			}
			set
			{
				CT_OnOff cT_OnOff = GetCTPPr().IsSetWordWrap() ? GetCTPPr().wordWrap : GetCTPPr().AddNewWordWrap();
				if (value)
				{
					cT_OnOff.val = true;
				}
				else
				{
					cT_OnOff.UnSetVal();
				}
			}
		}

		/// @return  the style of the paragraph
		public string Style
		{
			get
			{
				CT_PPr cTPPr = GetCTPPr();
				CT_String cT_String = cTPPr.IsSetPStyle() ? cTPPr.pStyle : null;
				if (cT_String == null)
				{
					return null;
				}
				return cT_String.val;
			}
			set
			{
				CT_PPr cTPPr = GetCTPPr();
				CT_String cT_String = (cTPPr.pStyle != null) ? cTPPr.pStyle : cTPPr.AddNewPStyle();
				cT_String.val = value;
			}
		}

		/// returns the type of the BodyElement Paragraph
		/// @see NPOI.XWPF.UserModel.IBodyElement#getElementType()
		public BodyElementType ElementType
		{
			get
			{
				return BodyElementType.PARAGRAPH;
			}
		}

		public IBody Body
		{
			get
			{
				return part;
			}
		}

		/// returns the partType of the bodyPart which owns the bodyElement
		///
		/// @see NPOI.XWPF.UserModel.IBody#getPartType()
		public BodyType PartType
		{
			get
			{
				return part.PartType;
			}
		}

		public XWPFParagraph(CT_P prgrph, IBody part)
		{
			paragraph = prgrph;
			this.part = part;
			document = part.GetXWPFDocument();
			if (document == null)
			{
				throw new NullReferenceException();
			}
			runs = new List<XWPFRun>();
			BuildRunsInOrderFromXml(paragraph.Items);
			foreach (XWPFRun run in runs)
			{
				CT_R cTR = run.GetCTR();
				if (document != null)
				{
					for (int i = 0; i < cTR.Items.Count; i++)
					{
						object obj = cTR.Items[i];
						if (obj is CT_FtnEdnRef)
						{
							CT_FtnEdnRef cT_FtnEdnRef = (CT_FtnEdnRef)obj;
							footnoteText.Append("[").Append(cT_FtnEdnRef.id).Append(": ");
							XWPFFootnote xWPFFootnote = null;
							if (cTR.ItemsElementName.Count > i && cTR.ItemsElementName[i] == RunItemsChoiceType.endnoteReference)
							{
								xWPFFootnote = document.GetEndnoteByID(int.Parse(cT_FtnEdnRef.id));
								if (xWPFFootnote == null)
								{
									xWPFFootnote = document.GetFootnoteByID(int.Parse(cT_FtnEdnRef.id));
								}
							}
							else
							{
								xWPFFootnote = document.GetFootnoteByID(int.Parse(cT_FtnEdnRef.id));
								if (xWPFFootnote == null)
								{
									xWPFFootnote = document.GetEndnoteByID(int.Parse(cT_FtnEdnRef.id));
								}
							}
							if (xWPFFootnote != null)
							{
								bool flag = true;
								foreach (XWPFParagraph paragraph2 in xWPFFootnote.Paragraphs)
								{
									if (!flag)
									{
										footnoteText.Append("\n");
										flag = false;
									}
									footnoteText.Append(paragraph2.Text);
								}
							}
							footnoteText.Append("]");
						}
					}
				}
			}
		}

		/// Identifies (in order) the parts of the paragraph /
		///  sub-paragraph that correspond to character text
		///  runs, and builds the appropriate runs for these.
		private void BuildRunsInOrderFromXml(ArrayList items)
		{
			foreach (object item in items)
			{
				if (item is CT_R)
				{
					runs.Add(new XWPFRun((CT_R)item, this));
				}
				if (item is CT_Hyperlink1)
				{
					CT_Hyperlink1 cT_Hyperlink = (CT_Hyperlink1)item;
					foreach (CT_R r in cT_Hyperlink.GetRList())
					{
						runs.Add(new XWPFHyperlinkRun(cT_Hyperlink, r, this));
					}
				}
				if (item is CT_SdtRun)
				{
					CT_SdtContentRun sdtContent = ((CT_SdtRun)item).sdtContent;
					foreach (CT_R r2 in sdtContent.GetRList())
					{
						runs.Add(new XWPFRun(r2, this));
					}
				}
				if (item is CT_RunTrackChange)
				{
					foreach (CT_R r3 in ((CT_RunTrackChange)item).GetRList())
					{
						runs.Add(new XWPFRun(r3, this));
					}
				}
				if (item is CT_SimpleField)
				{
					foreach (CT_R r4 in ((CT_SimpleField)item).GetRList())
					{
						runs.Add(new XWPFRun(r4, this));
					}
				}
				if (item is CT_SmartTagRun)
				{
					BuildRunsInOrderFromXml((item as CT_SmartTagRun).Items);
				}
			}
		}

		internal CT_P GetCTP()
		{
			return paragraph;
		}

		/// If style exist for this paragraph
		/// NumId of the paragraph will be returned.
		/// If style not exist null will be returned     
		/// @return	NumID as Bigint
		public string GetNumID()
		{
			if (paragraph.pPr != null && paragraph.pPr.numPr != null && paragraph.pPr.numPr.numId != null)
			{
				return paragraph.pPr.numPr.numId.val;
			}
			return null;
		}

		/// SetNumID of Paragraph
		/// @param numPos
		public void SetNumID(string numId)
		{
			if (paragraph.pPr == null)
			{
				paragraph.AddNewPPr();
			}
			if (paragraph.pPr.numPr == null)
			{
				paragraph.pPr.AddNewNumPr();
			}
			if (paragraph.pPr.numPr.numId == null)
			{
				paragraph.pPr.numPr.AddNewNumId();
			}
			paragraph.pPr.numPr.ilvl = new CT_DecimalNumber();
			paragraph.pPr.numPr.ilvl.val = "0";
			paragraph.pPr.numPr.numId.val = numId;
		}

		/// <summary>
		/// Set NumID and level of Paragraph
		/// </summary>
		/// <param name="numId"></param>
		/// <param name="ilvl"></param>
		public void SetNumID(string numId, string ilvl)
		{
			if (paragraph.pPr == null)
			{
				paragraph.AddNewPPr();
			}
			if (paragraph.pPr.numPr == null)
			{
				paragraph.pPr.AddNewNumPr();
			}
			if (paragraph.pPr.numPr.numId == null)
			{
				paragraph.pPr.numPr.AddNewNumId();
			}
			paragraph.pPr.numPr.ilvl = new CT_DecimalNumber();
			paragraph.pPr.numPr.ilvl.val = ilvl;
			paragraph.pPr.numPr.numId.val = numId;
		}

		/// <summary>
		/// Appends a new run to this paragraph
		/// </summary>
		/// <returns>a new text run</returns>
		public XWPFRun CreateRun()
		{
			XWPFRun xWPFRun = new XWPFRun(paragraph.AddNewR(), this);
			runs.Add(xWPFRun);
			return xWPFRun;
		}

		/// Get a <b>copy</b> of the currently used CTPBrd, if none is used, return
		/// a new instance.
		private CT_PBdr GetCTPBrd(bool create)
		{
			CT_PPr cTPPr = GetCTPPr();
			CT_PBdr cT_PBdr = cTPPr.IsSetPBdr() ? cTPPr.pBdr : null;
			if (create && cT_PBdr == null)
			{
				cT_PBdr = cTPPr.AddNewPBdr();
			}
			return cT_PBdr;
		}

		/// Get a <b>copy</b> of the currently used CTSpacing, if none is used,
		/// return a new instance.
		private CT_Spacing GetCTSpacing(bool create)
		{
			CT_PPr cTPPr = GetCTPPr();
			CT_Spacing cT_Spacing = (cTPPr.spacing == null) ? null : cTPPr.spacing;
			if (create && cT_Spacing == null)
			{
				cT_Spacing = cTPPr.AddNewSpacing();
			}
			return cT_Spacing;
		}

		/// Get a <b>copy</b> of the currently used CTPInd, if none is used, return
		/// a new instance.
		private CT_Ind GetCTInd(bool create)
		{
			CT_PPr cTPPr = GetCTPPr();
			CT_Ind cT_Ind = (cTPPr.ind == null) ? null : cTPPr.ind;
			if (create && cT_Ind == null)
			{
				cT_Ind = cTPPr.AddNewInd();
			}
			return cT_Ind;
		}

		/// Get a <b>copy</b> of the currently used CTPPr, if none is used, return
		/// a new instance.
		internal CT_PPr GetCTPPr()
		{
			return (paragraph.pPr == null) ? paragraph.AddNewPPr() : paragraph.pPr;
		}

		/// add a new run at the end of the position of 
		/// the content of parameter run
		/// @param run
		protected void AddRun(CT_R Run)
		{
			int count = paragraph.GetRList().Count;
			paragraph.AddNewR();
			paragraph.SetRArray(count, Run);
		}

		/// <summary>
		/// Replace text inside each run (cross run is not supported yet)
		/// </summary>
		/// <param name="oldText">target text</param>
		/// <param name="newText">replacement text</param>
		public void ReplaceText(string oldText, string newText)
		{
			for (int i = 0; i < runs.Count; i++)
			{
				runs[i].ReplaceText(oldText, newText);
			}
		}

		/// <summary>
		/// this methods parse the paragraph and search for the string searched. 
		/// If it finds the string, it will return true and the position of the String will be saved in the parameter startPos.
		/// </summary>
		/// <param name="searched"></param>
		/// <param name="startPos"></param>
		/// <returns></returns>
		public TextSegement SearchText(string searched, PositionInParagraph startPos)
		{
			int run = startPos.Run;
			int text = startPos.Text;
			int @char = startPos.Char;
			int beginRun = 0;
			int num = 0;
			bool flag = false;
			for (int i = run; i < paragraph.GetRList().Count; i++)
			{
				int beginText = 0;
				int beginChar = 0;
				int num2 = 0;
				int num3 = 0;
				CT_R cT_R = paragraph.GetRList()[i];
				foreach (object item in cT_R.Items)
				{
					if (item is CT_Text)
					{
						if (num2 >= text)
						{
							string value = ((CT_Text)item).Value;
							for (num3 = ((i == run) ? @char : 0); num3 < value.Length; num3++)
							{
								if (value[num3] == searched[0] && num == 0)
								{
									beginText = num2;
									beginChar = num3;
									beginRun = i;
									flag = true;
								}
								if (value[num3] == searched[num])
								{
									if (num + 1 < searched.Length)
									{
										num++;
									}
									else if (flag)
									{
										TextSegement textSegement = new TextSegement();
										textSegement.BeginRun = beginRun;
										textSegement.BeginText = beginText;
										textSegement.BeginChar = beginChar;
										textSegement.EndRun = i;
										textSegement.EndText = num2;
										textSegement.EndChar = num3;
										return textSegement;
									}
								}
								else
								{
									num = 0;
								}
							}
						}
						num2++;
					}
					else if (!(item is CT_ProofErr) && !(item is CT_RPr))
					{
						num = 0;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// insert a new Run in RunArray
		/// </summary>
		/// <param name="pos"></param>
		/// <returns>the inserted run</returns>
		public XWPFRun InsertNewRun(int pos)
		{
			if (pos >= 0 && pos <= paragraph.SizeOfRArray())
			{
				CT_R r = paragraph.InsertNewR(pos);
				XWPFRun xWPFRun = new XWPFRun(r, this);
				runs.Insert(pos, xWPFRun);
				return xWPFRun;
			}
			return null;
		}

		/// Get a Text
		/// @param segment
		public string GetText(TextSegement segment)
		{
			int beginRun = segment.BeginRun;
			int beginText = segment.BeginText;
			int beginChar = segment.BeginChar;
			int endRun = segment.EndRun;
			int endText = segment.EndText;
			int endChar = segment.EndChar;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = beginRun; i <= endRun; i++)
			{
				int num = 0;
				int num2 = paragraph.GetRList()[i].GetTList().Count - 1;
				if (i == beginRun)
				{
					num = beginText;
				}
				if (i == endRun)
				{
					num2 = endText;
				}
				for (int j = num; j <= num2; j++)
				{
					string value = paragraph.GetRList()[i].GetTArray(j).Value;
					int num3 = 0;
					int num4 = value.Length - 1;
					if (j == beginText && i == beginRun)
					{
						num3 = beginChar;
					}
					if (j == endText && i == endRun)
					{
						num4 = endChar;
					}
					stringBuilder.Append(value.Substring(num3, num4 - num3 + 1));
				}
			}
			return stringBuilder.ToString();
		}

		/// Removes a Run at the position pos in the paragraph
		/// @param pos
		/// @return true if the run was Removed
		public bool RemoveRun(int pos)
		{
			if (pos >= 0 && pos < paragraph.SizeOfRArray())
			{
				GetCTP().RemoveR(pos);
				runs.RemoveAt(pos);
				return true;
			}
			return false;
		}

		/// returns the part of the bodyElement
		/// @see NPOI.XWPF.UserModel.IBody#getPart()
		public POIXMLDocumentPart GetPart()
		{
			if (part != null)
			{
				return part.GetPart();
			}
			return null;
		}

		/// Adds a new Run to the Paragraph
		///
		/// @param r
		public void AddRun(XWPFRun r)
		{
			if (!runs.Contains(r))
			{
				runs.Add(r);
			}
		}

		/// return the XWPFRun-Element which owns the CTR Run-Element
		///
		/// @param r
		public XWPFRun GetRun(CT_R r)
		{
			for (int i = 0; i < Runs.Count; i++)
			{
				if (Runs[i].GetCTR() == r)
				{
					return Runs[i];
				}
			}
			return null;
		}
	}
}
