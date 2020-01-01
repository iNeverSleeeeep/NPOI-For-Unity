using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// High level representation of the style of a cell in a sheet of a workbook.
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// </summary>
	public class HSSFCellStyle : ICellStyle
	{
		private ExtendedFormatRecord format;

		private short index;

		private InternalWorkbook workbook;

		/// <summary>
		/// Get the index within the HSSFWorkbook (sequence within the collection of ExtnededFormat objects)
		/// </summary>
		/// <value>Unique index number of the Underlying record this style represents (probably you don't care
		/// Unless you're comparing which one is which)</value>
		public short Index => index;

		/// <summary>
		/// Gets the parent style.
		/// </summary>
		/// <value>the parent style for this cell style.
		/// In most cases this will be null, but in a few
		/// cases there'll be a fully defined parent.</value>
		public HSSFCellStyle ParentStyle
		{
			get
			{
				short parentIndex = format.ParentIndex;
				if (parentIndex == 0 || parentIndex == 4095)
				{
					return null;
				}
				return new HSSFCellStyle(parentIndex, workbook.GetExFormatAt(parentIndex), workbook);
			}
		}

		/// <summary>
		/// Get the index of the format
		/// </summary>
		/// <value>The data format.</value>
		public short DataFormat
		{
			get
			{
				return format.FormatIndex;
			}
			set
			{
				format.FormatIndex = value;
			}
		}

		/// <summary>
		/// Gets the index of the font for this style.
		/// </summary>
		/// <value>The index of the font.</value>
		public short FontIndex => format.FontIndex;

		/// <summary>
		/// Get whether the cell's using this style are to be hidden
		/// </summary>
		/// <value>whether the cell using this style should be hidden</value>
		public bool IsHidden
		{
			get
			{
				return format.IsHidden;
			}
			set
			{
				format.IsIndentNotParentCellOptions = true;
				format.IsHidden = value;
			}
		}

		/// <summary>
		/// Get whether the cell's using this style are to be locked
		/// </summary>
		/// <value>whether the cell using this style should be locked</value>
		public bool IsLocked
		{
			get
			{
				return format.IsLocked;
			}
			set
			{
				format.IsIndentNotParentCellOptions = true;
				format.IsLocked = value;
			}
		}

		/// <summary>
		/// Get the type of horizontal alignment for the cell
		/// </summary>
		/// <value> the type of alignment</value>
		public HorizontalAlignment Alignment
		{
			get
			{
				return (HorizontalAlignment)format.Alignment;
			}
			set
			{
				format.IsIndentNotParentAlignment = true;
				format.Alignment = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the text should be wrapped
		/// </summary>
		/// <value><c>true</c> if [wrap text]; otherwise, <c>false</c>.</value>
		public bool WrapText
		{
			get
			{
				return format.WrapText;
			}
			set
			{
				format.IsIndentNotParentAlignment = true;
				format.WrapText = value;
			}
		}

		/// <summary>
		/// Gets or sets the vertical alignment for the cell.
		/// </summary>
		/// <value>the type of alignment</value>
		public VerticalAlignment VerticalAlignment
		{
			get
			{
				return (VerticalAlignment)format.VerticalAlignment;
			}
			set
			{
				format.VerticalAlignment = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the degree of rotation for the text in the cell
		/// </summary>
		/// <value>The rotation degrees (between -90 and 90 degrees).</value>
		public short Rotation
		{
			get
			{
				short num = format.Rotation;
				if (num == 255)
				{
					return num;
				}
				if (num > 90)
				{
					num = (short)(90 - num);
				}
				return num;
			}
			set
			{
				short num = value;
				if (num != 255)
				{
					if (value < 0 && value >= -90)
					{
						num = (short)(90 - value);
					}
					else if (value < -90 || value > 90)
					{
						throw new ArgumentException("The rotation must be between -90 and 90 degrees, or 0xff");
					}
				}
				format.Rotation = num;
			}
		}

		/// <summary>
		/// Gets or sets the number of spaces to indent the text in the cell
		/// </summary>
		/// <value>number of spaces</value>
		public short Indention
		{
			get
			{
				return format.Indent;
			}
			set
			{
				format.Indent = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of border to use for the left border of the cell
		/// </summary>
		/// <value>The border type.</value>
		public BorderStyle BorderLeft
		{
			get
			{
				return (BorderStyle)format.BorderLeft;
			}
			set
			{
				format.IsIndentNotParentBorder = true;
				format.BorderLeft = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the type of border to use for the right border of the cell
		/// </summary>
		/// <value>The border type.</value>
		public BorderStyle BorderRight
		{
			get
			{
				return (BorderStyle)format.BorderRight;
			}
			set
			{
				format.IsIndentNotParentBorder = true;
				format.BorderRight = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the type of border to use for the top border of the cell
		/// </summary>
		/// <value>The border type.</value>
		public BorderStyle BorderTop
		{
			get
			{
				return (BorderStyle)format.BorderTop;
			}
			set
			{
				format.IsIndentNotParentBorder = true;
				format.BorderTop = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the type of border to use for the bottom border of the cell
		/// </summary>
		/// <value>The border type.</value>
		public BorderStyle BorderBottom
		{
			get
			{
				return (BorderStyle)format.BorderBottom;
			}
			set
			{
				format.IsIndentNotParentBorder = true;
				format.BorderBottom = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the color to use for the left border
		/// </summary>
		/// <value>The index of the color definition</value>
		public short LeftBorderColor
		{
			get
			{
				return format.LeftBorderPaletteIdx;
			}
			set
			{
				format.LeftBorderPaletteIdx = value;
			}
		}

		/// <summary>
		/// Gets or sets the color to use for the left border.
		/// </summary>
		/// <value>The index of the color definition</value>
		public short RightBorderColor
		{
			get
			{
				return format.RightBorderPaletteIdx;
			}
			set
			{
				format.RightBorderPaletteIdx = value;
			}
		}

		/// <summary>
		/// Gets or sets the color to use for the top border
		/// </summary>
		/// <value>The index of the color definition.</value>
		public short TopBorderColor
		{
			get
			{
				return format.TopBorderPaletteIdx;
			}
			set
			{
				format.TopBorderPaletteIdx = value;
			}
		}

		/// <summary>
		/// Gets or sets the color to use for the left border
		/// </summary>
		/// <value>The index of the color definition.</value>
		public short BottomBorderColor
		{
			get
			{
				return format.BottomBorderPaletteIdx;
			}
			set
			{
				format.BottomBorderPaletteIdx = value;
			}
		}

		/// <summary>
		/// Gets or sets the color to use for the diagional border
		/// </summary>
		/// <value>The index of the color definition.</value>
		public short BorderDiagonalColor
		{
			get
			{
				return format.AdtlDiagBorderPaletteIdx;
			}
			set
			{
				format.AdtlDiagBorderPaletteIdx = value;
			}
		}

		/// <summary>
		/// Gets or sets the line type  to use for the diagional border
		/// </summary>
		/// <value>The line type.</value>
		public BorderStyle BorderDiagonalLineStyle
		{
			get
			{
				return (BorderStyle)format.AdtlDiagLineStyle;
			}
			set
			{
				format.AdtlDiagLineStyle = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the type of diagional border
		/// </summary>.
		/// <value>The border diagional type.</value>
		public BorderDiagonal BorderDiagonal
		{
			get
			{
				return (BorderDiagonal)format.Diagonal;
			}
			set
			{
				format.Diagonal = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets whether the cell is shrink-to-fit
		/// </summary>
		public bool ShrinkToFit
		{
			get
			{
				return format.ShrinkToFit;
			}
			set
			{
				format.ShrinkToFit = value;
			}
		}

		/// <summary>
		/// Gets or sets the fill pattern. - Set to 1 to Fill with foreground color
		/// </summary>
		/// <value>The fill pattern.</value>
		public FillPattern FillPattern
		{
			get
			{
				return (FillPattern)format.AdtlFillPattern;
			}
			set
			{
				format.AdtlFillPattern = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the fill background.
		/// </summary>
		/// <value>The color of the fill background.</value>
		/// Set the background Fill color.
		/// <example>
		/// cs.SetFillPattern(HSSFCellStyle.FINE_DOTS );
		/// cs.SetFillBackgroundColor(new HSSFColor.RED().Index);
		/// optionally a Foreground and background Fill can be applied:
		/// Note: Ensure Foreground color is Set prior to background
		/// cs.SetFillPattern(HSSFCellStyle.FINE_DOTS );
		/// cs.SetFillForegroundColor(new HSSFColor.BLUE().Index);
		/// cs.SetFillBackgroundColor(new HSSFColor.RED().Index);
		/// or, for the special case of SOLID_Fill:
		/// cs.SetFillPattern(HSSFCellStyle.SOLID_FOREGROUND );
		/// cs.SetFillForegroundColor(new HSSFColor.RED().Index);
		/// It is necessary to Set the Fill style in order
		/// for the color to be shown in the cell.
		/// </example>
		public short FillBackgroundColor
		{
			get
			{
				short fillBackground = format.FillBackground;
				if (fillBackground == 65)
				{
					return 64;
				}
				return fillBackground;
			}
			set
			{
				format.FillBackground = value;
				CheckDefaultBackgroundFills();
			}
		}

		public IColor FillBackgroundColorColor
		{
			get
			{
				HSSFPalette hSSFPalette = new HSSFPalette(workbook.CustomPalette);
				return hSSFPalette.GetColor(FillBackgroundColor);
			}
		}

		/// <summary>
		/// Gets or sets the foreground Fill color
		/// </summary>
		/// <value>Fill color.</value>
		/// @see org.apache.poi.hssf.usermodel.HSSFPalette#GetColor(short)
		public short FillForegroundColor
		{
			get
			{
				return format.FillForeground;
			}
			set
			{
				format.FillForeground = value;
				CheckDefaultBackgroundFills();
			}
		}

		public IColor FillForegroundColorColor
		{
			get
			{
				HSSFPalette hSSFPalette = new HSSFPalette(workbook.CustomPalette);
				return hSSFPalette.GetColor(FillForegroundColor);
			}
		}

		/// Gets the name of the user defined style.
		/// Returns null for built in styles, and
		///  styles where no name has been defined
		public string UserStyleName
		{
			get
			{
				StyleRecord styleRecord = workbook.GetStyleRecord(index);
				if (styleRecord == null)
				{
					return null;
				}
				if (styleRecord.IsBuiltin)
				{
					return null;
				}
				return styleRecord.Name;
			}
			set
			{
				StyleRecord styleRecord = workbook.GetStyleRecord(index);
				if (styleRecord == null)
				{
					styleRecord = workbook.CreateStyleRecord(index);
				}
				if (styleRecord.IsBuiltin && index <= 20)
				{
					throw new ArgumentException("Unable to set user specified style names for built in styles!");
				}
				styleRecord.Name = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFCellStyle" /> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="rec">The record.</param>
		/// <param name="workbook">The workbook.</param>
		public HSSFCellStyle(short index, ExtendedFormatRecord rec, HSSFWorkbook workbook)
			: this(index, rec, workbook.Workbook)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFCellStyle" /> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="rec">The record.</param>
		/// <param name="workbook">The workbook.</param>
		public HSSFCellStyle(short index, ExtendedFormatRecord rec, InternalWorkbook workbook)
		{
			this.workbook = workbook;
			this.index = index;
			format = rec;
		}

		/// <summary>
		/// Get the contents of the format string, by looking up
		/// the DataFormat against the bound workbook
		/// </summary>
		/// <returns></returns>
		public string GetDataFormatString()
		{
			HSSFDataFormat hSSFDataFormat = new HSSFDataFormat(workbook);
			return hSSFDataFormat.GetFormat(DataFormat);
		}

		/// <summary>
		/// Get the contents of the format string, by looking up
		/// the DataFormat against the supplied workbook
		/// </summary>
		/// <param name="workbook">The workbook.</param>
		/// <returns></returns>
		public string GetDataFormatString(InternalWorkbook workbook)
		{
			HSSFDataFormat hSSFDataFormat = new HSSFDataFormat(workbook);
			return hSSFDataFormat.GetFormat(DataFormat);
		}

		/// <summary>
		/// Set the font for this style
		/// </summary>
		/// <param name="font">a font object Created or retreived from the HSSFWorkbook object</param>
		public void SetFont(IFont font)
		{
			format.IsIndentNotParentFont = true;
			short fontIndex = font.Index;
			format.FontIndex = fontIndex;
		}

		/// <summary>
		/// Gets the font for this style
		/// </summary>
		/// <param name="parentWorkbook">The parent workbook that this style belongs to.</param>
		/// <returns></returns>
		public IFont GetFont(IWorkbook parentWorkbook)
		{
			return ((HSSFWorkbook)parentWorkbook).GetFontAt(FontIndex);
		}

		/// <summary>
		/// Verifies that this style belongs to the supplied Workbook.
		/// Will throw an exception if it belongs to a different one.
		/// This is normally called when trying to assign a style to a
		/// cell, to ensure the cell and the style are from the same
		/// workbook (if they're not, it won't work)
		/// </summary>
		/// <param name="wb">The workbook.</param>
		public void VerifyBelongsToWorkbook(HSSFWorkbook wb)
		{
			if (wb.Workbook != workbook)
			{
				throw new ArgumentException("This Style does not belong to the supplied Workbook. Are you trying to assign a style from one workbook to the cell of a differnt workbook?");
			}
		}

		/// <summary>
		/// Checks if the background and foreground Fills are Set correctly when one
		/// or the other is Set to the default color.
		/// Works like the logic table below:
		/// BACKGROUND   FOREGROUND
		/// NONE         AUTOMATIC
		/// 0x41         0x40
		/// NONE         RED/ANYTHING
		/// 0x40         0xSOMETHING
		/// </summary>
		private void CheckDefaultBackgroundFills()
		{
			if (format.FillForeground == 64)
			{
				if (format.FillBackground != 65)
				{
					FillBackgroundColor = 65;
				}
			}
			else if (format.FillBackground == 65 && format.FillForeground != 64)
			{
				FillBackgroundColor = 64;
			}
		}

		/// Clones all the style information from another
		///  HSSFCellStyle, onto this one. This
		///  HSSFCellStyle will then have all the same
		///  properties as the source, but the two may
		///  be edited independently.
		/// Any stylings on this HSSFCellStyle will be lost!
		///
		/// The source HSSFCellStyle could be from another
		///  HSSFWorkbook if you like. This allows you to
		///  copy styles from one HSSFWorkbook to another.
		public void CloneStyleFrom(ICellStyle source)
		{
			if (source is HSSFCellStyle)
			{
				CloneStyleFrom((HSSFCellStyle)source);
				return;
			}
			throw new ArgumentException("Can only clone from one HSSFCellStyle to another, not between HSSFCellStyle and XSSFCellStyle");
		}

		/// <summary>
		/// Clones all the style information from another
		/// HSSFCellStyle, onto this one. This
		/// HSSFCellStyle will then have all the same
		/// properties as the source, but the two may
		/// be edited independently.
		/// Any stylings on this HSSFCellStyle will be lost!
		/// The source HSSFCellStyle could be from another
		/// HSSFWorkbook if you like. This allows you to
		/// copy styles from one HSSFWorkbook to another.
		/// </summary>
		/// <param name="source">The source.</param>
		public void CloneStyleFrom(HSSFCellStyle source)
		{
			format.CloneStyleFrom(source.format);
			if (workbook != source.workbook)
			{
				short num2 = DataFormat = (short)workbook.CreateFormat(source.GetDataFormatString());
				FontRecord fontRecord = workbook.CreateNewFont();
				fontRecord.CloneStyleFrom(source.workbook.GetFontRecordAt(source.FontIndex));
				HSSFFont font = new HSSFFont((short)workbook.GetFontIndex(fontRecord), fontRecord);
				SetFont(font);
			}
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		public override int GetHashCode()
		{
			int num = 31;
			int num2 = 1;
			num2 = num * num2 + ((format != null) ? format.GetHashCode() : 0);
			return num * num2 + index;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj" /> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (obj is HSSFCellStyle)
			{
				HSSFCellStyle hSSFCellStyle = (HSSFCellStyle)obj;
				if (format == null)
				{
					if (hSSFCellStyle.format != null)
					{
						return false;
					}
				}
				else if (!format.Equals(hSSFCellStyle.format))
				{
					return false;
				}
				if (index != hSSFCellStyle.index)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
