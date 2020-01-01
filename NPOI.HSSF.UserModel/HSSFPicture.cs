using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Represents a escher picture.  Eg. A GIF, JPEG etc...
	/// @author Glen Stampoultzis
	/// @author Yegor Kozlov (yegor at apache.org)
	/// </summary>
	public class HSSFPicture : HSSFSimpleShape, IPicture
	{
		/// width of 1px in columns with default width in Units of 1/256 of a Char width
		private const float PX_DEFAULT = 32f;

		/// width of 1px in columns with overridden width in Units of 1/256 of a Char width
		private const float PX_MODIFIED = 36.56f;

		/// Height of 1px of a row
		private const int PX_ROW = 15;

		private static POILogger logger = POILogFactory.GetLogger(typeof(HSSFPicture));

		/// <summary>
		/// Gets or sets the index of the picture.
		/// </summary>
		/// <value>The index of the picture.</value>
		public int PictureIndex
		{
			get
			{
				return ((EscherSimpleProperty)GetOptRecord().Lookup(260))?.PropertyValue ?? (-1);
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(260, isComplex: false, isBlipId: true, value));
			}
		}

		/// Return picture data for this shape
		///
		/// @return picture data for this shape
		public IPictureData PictureData
		{
			get
			{
				InternalWorkbook workbook = (_patriarch.Sheet.Workbook as HSSFWorkbook).Workbook;
				EscherBlipRecord blipRecord = workbook.GetBSERecord(PictureIndex).BlipRecord;
				return new HSSFPictureData(blipRecord);
			}
		}

		/// The color applied to the lines of this shape.
		public string FileName
		{
			get
			{
				EscherComplexProperty escherComplexProperty = (EscherComplexProperty)GetOptRecord().Lookup(261);
				try
				{
					if (escherComplexProperty == null)
					{
						return "";
					}
					return Trim(Encoding.Unicode.GetString(escherComplexProperty.ComplexData));
				}
				catch (Exception)
				{
					return "";
				}
			}
			set
			{
				try
				{
					EscherComplexProperty propertyValue = new EscherComplexProperty(261, isBlipId: true, Encoding.Unicode.GetBytes(value));
					SetPropertyValue(propertyValue);
				}
				catch (Exception)
				{
					logger.Log(7, "Unsupported encoding: UTF-16LE");
				}
			}
		}

		public override int ShapeType
		{
			get
			{
				return base.ShapeType;
			}
			set
			{
				throw new InvalidOperationException("Shape type can not be changed in " + GetType().Name);
			}
		}

		public HSSFPicture(EscherContainerRecord spContainer, ObjRecord objRecord)
			: base(spContainer, objRecord)
		{
		}

		/// <summary>
		/// Constructs a picture object.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="anchor">The anchor.</param>
		public HSSFPicture(HSSFShape parent, HSSFAnchor anchor)
			: base(parent, anchor)
		{
			base.ShapeType = 75;
			CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)GetObjRecord().SubRecords[0];
			commonObjectDataSubRecord.ObjectType = CommonObjectType.Picture;
		}

		protected override EscherContainerRecord CreateSpContainer()
		{
			EscherContainerRecord escherContainerRecord = base.CreateSpContainer();
			EscherOptRecord escherOptRecord = (EscherOptRecord)escherContainerRecord.GetChildById(-4085);
			escherOptRecord.RemoveEscherProperty(462);
			escherOptRecord.RemoveEscherProperty(511);
			escherContainerRecord.RemoveChildRecord(escherContainerRecord.GetChildById(-4083));
			return escherContainerRecord;
		}

		/// <summary>
		/// Reset the image to the original size.
		/// </summary>
		public void Resize(double scale)
		{
			HSSFClientAnchor hSSFClientAnchor = (HSSFClientAnchor)base.Anchor;
			hSSFClientAnchor.AnchorType = 2;
			IClientAnchor preferredSize = GetPreferredSize(scale);
			int row = hSSFClientAnchor.Row1 + (preferredSize.Row2 - preferredSize.Row1);
			int num2 = hSSFClientAnchor.Col2 = hSSFClientAnchor.Col1 + (preferredSize.Col2 - preferredSize.Col1);
			hSSFClientAnchor.Dx1 = 0;
			hSSFClientAnchor.Dx2 = preferredSize.Dx2;
			hSSFClientAnchor.Row2 = row;
			hSSFClientAnchor.Dy1 = 0;
			hSSFClientAnchor.Dy2 = preferredSize.Dy2;
		}

		/// <summary>
		/// Reset the image to the original size.
		/// </summary>
		public void Resize()
		{
			Resize(1.0);
		}

		/// <summary>
		/// Calculate the preferred size for this picture.
		/// </summary>
		/// <param name="scale">the amount by which image dimensions are multiplied relative to the original size.</param>
		/// <returns>HSSFClientAnchor with the preferred size for this image</returns>
		public HSSFClientAnchor GetPreferredSize(double scale)
		{
			HSSFClientAnchor hSSFClientAnchor = (HSSFClientAnchor)base.Anchor;
			Size imageDimension = GetImageDimension();
			double num = (double)imageDimension.Width * scale;
			double num2 = (double)imageDimension.Height * scale;
			float num3 = 0f;
			num3 += GetColumnWidthInPixels(hSSFClientAnchor.Col1) * (1f - (float)hSSFClientAnchor.Dx1 / 1024f);
			short num4 = (short)(hSSFClientAnchor.Col1 + 1);
			int dx = 0;
			while ((double)num3 < num)
			{
				float num5 = num3;
				short num6 = num4;
				num4 = (short)(num6 + 1);
				num3 = num5 + GetColumnWidthInPixels(num6);
			}
			if ((double)num3 > num)
			{
				num4 = (short)(num4 - 1);
				double num7 = (double)GetColumnWidthInPixels(num4);
				double num8 = (double)num3 - num;
				dx = (int)((num7 - num8) / num7 * 1024.0);
			}
			hSSFClientAnchor.Col2 = num4;
			hSSFClientAnchor.Dx2 = dx;
			float num9 = 0f;
			num9 += (1f - (float)hSSFClientAnchor.Dy1 / 256f) * GetRowHeightInPixels(hSSFClientAnchor.Row1);
			int num10 = hSSFClientAnchor.Row1 + 1;
			int dy = 0;
			for (; (double)num9 < num2; num9 += GetRowHeightInPixels(num10++))
			{
			}
			if ((double)num9 > num2)
			{
				num10--;
				double num12 = (double)GetRowHeightInPixels(num10);
				double num13 = (double)num9 - num2;
				dy = (int)((num12 - num13) / num12 * 256.0);
			}
			hSSFClientAnchor.Row2 = num10;
			hSSFClientAnchor.Dy2 = dy;
			return hSSFClientAnchor;
		}

		/// <summary>
		/// Calculate the preferred size for this picture.
		/// </summary>
		/// <returns>HSSFClientAnchor with the preferred size for this image</returns>
		public IClientAnchor GetPreferredSize()
		{
			return GetPreferredSize(1.0);
		}

		/// <summary>
		/// Gets the column width in pixels.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		private float GetColumnWidthInPixels(int column)
		{
			int columnWidth = _patriarch.Sheet.GetColumnWidth(column);
			float pixelWidth = GetPixelWidth(column);
			return (float)columnWidth / pixelWidth;
		}

		/// <summary>
		/// Gets the row height in pixels.
		/// </summary>
		/// <param name="i">The row</param>
		/// <returns></returns>
		private float GetRowHeightInPixels(int i)
		{
			IRow row = _patriarch.Sheet.GetRow(i);
			float num = (row == null) ? ((float)_patriarch.Sheet.DefaultRowHeight) : ((float)row.Height);
			return num / 15f;
		}

		/// <summary>
		/// Gets the width of the pixel.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		private float GetPixelWidth(int column)
		{
			int num = _patriarch.Sheet.DefaultColumnWidth * 256;
			int columnWidth = _patriarch.Sheet.GetColumnWidth(column);
			if (columnWidth != num)
			{
				return 36.56f;
			}
			return 32f;
		}

		/// <summary>
		/// The metadata of PNG and JPEG can contain the width of a pixel in millimeters.
		/// Return the the "effective" dpi calculated as 
		/// <c>25.4/HorizontalPixelSize</c>
		/// and 
		/// <c>25.4/VerticalPixelSize</c>
		/// .  Where 25.4 is the number of mm in inch.
		/// </summary>
		/// <param name="r">The image.</param>
		/// <returns>the resolution</returns>
		protected Size GetResolution(Image r)
		{
			return new Size((int)r.HorizontalResolution, (int)r.VerticalResolution);
		}

		/// <summary>
		/// Return the dimension of this image
		/// </summary>
		/// <returns>image dimension</returns>
		public Size GetImageDimension()
		{
			EscherBSERecord bSERecord = (_patriarch.Sheet.Workbook as HSSFWorkbook).Workbook.GetBSERecord(PictureIndex);
			byte[] pictureData = bSERecord.BlipRecord.PictureData;
			using (MemoryStream stream = new MemoryStream(pictureData))
			{
				using (Image image = Image.FromStream(stream))
				{
					return image.Size;
				}
			}
		}

		internal override void AfterInsert(HSSFPatriarch patriarch)
		{
			EscherAggregate boundAggregate = patriarch.GetBoundAggregate();
			boundAggregate.AssociateShapeToObjRecord(GetEscherContainer().GetChildById(-4079), GetObjRecord());
			EscherBSERecord bSERecord = (patriarch.Sheet.Workbook as HSSFWorkbook).Workbook.GetBSERecord(PictureIndex);
			bSERecord.Ref++;
		}

		private string Trim(string value)
		{
			int num = value.Length;
			int i = 0;
			char[] array;
			for (array = value.ToCharArray(); i < num && array[i] <= ' '; i++)
			{
			}
			while (i < num && array[num - 1] <= ' ')
			{
				num--;
			}
			if (i <= 0 && num >= value.Length)
			{
				return value;
			}
			return value.Substring(i, num - i);
		}

		internal override HSSFShape CloneShape()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			byte[] data = GetEscherContainer().Serialize();
			escherContainerRecord.FillFields(data, 0, new DefaultEscherRecordFactory());
			ObjRecord objRecord = (ObjRecord)GetObjRecord().CloneViaReserialise();
			return new HSSFPicture(escherContainerRecord, objRecord);
		}
	}
}
