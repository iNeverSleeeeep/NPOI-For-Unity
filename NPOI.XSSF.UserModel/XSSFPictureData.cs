using NPOI.OpenXml4Net.OPC;
using NPOI.SS.UserModel;
using NPOI.Util;
using System.Collections.Generic;
using System.IO;

namespace NPOI.XSSF.UserModel
{
	/// Raw picture data, normally attached to a SpreadsheetML Drawing.
	/// As a rule, pictures are stored in the /xl/media/ part of a SpreadsheetML package.
	public class XSSFPictureData : POIXMLDocumentPart, IPictureData
	{
		/// Relationships for each known picture type
		internal static Dictionary<int, POIXMLRelation> RELATIONS;

		/// <summary>
		/// Gets the picture data as a byte array.
		/// </summary>
		public byte[] Data
		{
			get
			{
				try
				{
					return IOUtils.ToByteArray(GetPackagePart().GetInputStream());
				}
				catch (IOException ex)
				{
					throw new POIXMLException(ex);
				}
			}
		}

		public string MimeType
		{
			get
			{
				return GetPackagePart().ContentType;
			}
		}

		static XSSFPictureData()
		{
			RELATIONS = new Dictionary<int, POIXMLRelation>(8);
			RELATIONS[2] = XSSFRelation.IMAGE_EMF;
			RELATIONS[3] = XSSFRelation.IMAGE_WMF;
			RELATIONS[4] = XSSFRelation.IMAGE_PICT;
			RELATIONS[5] = XSSFRelation.IMAGE_JPEG;
			RELATIONS[6] = XSSFRelation.IMAGE_PNG;
			RELATIONS[7] = XSSFRelation.IMAGE_DIB;
			RELATIONS[XSSFWorkbook.PICTURE_TYPE_GIF] = XSSFRelation.IMAGE_GIF;
			RELATIONS[XSSFWorkbook.PICTURE_TYPE_TIFF] = XSSFRelation.IMAGE_TIFF;
			RELATIONS[XSSFWorkbook.PICTURE_TYPE_EPS] = XSSFRelation.IMAGE_EPS;
			RELATIONS[XSSFWorkbook.PICTURE_TYPE_BMP] = XSSFRelation.IMAGE_BMP;
			RELATIONS[XSSFWorkbook.PICTURE_TYPE_WPG] = XSSFRelation.IMAGE_WPG;
		}

		/// Create a new XSSFPictureData node
		///
		/// @see NPOI.xssf.usermodel.XSSFWorkbook#AddPicture(byte[], int)
		public XSSFPictureData()
		{
		}

		/// Construct XSSFPictureData from a namespace part
		///
		/// @param part the namespace part holding the Drawing data,
		/// @param rel  the namespace relationship holding this Drawing,
		/// the relationship type must be http://schemas.Openxmlformats.org/officeDocument/2006/relationships/image
		internal XSSFPictureData(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
		}

		/// Suggests a file extension for this image.
		///
		/// @return the file extension.
		public string SuggestFileExtension()
		{
			return GetPackagePart().PartName.Extension;
		}

		/// Return an integer constant that specifies type of this picture
		///
		/// @return an integer constant that specifies type of this picture 
		/// @see NPOI.ss.usermodel.Workbook#PICTURE_TYPE_EMF
		/// @see NPOI.ss.usermodel.Workbook#PICTURE_TYPE_WMF
		/// @see NPOI.ss.usermodel.Workbook#PICTURE_TYPE_PICT
		/// @see NPOI.ss.usermodel.Workbook#PICTURE_TYPE_JPEG
		/// @see NPOI.ss.usermodel.Workbook#PICTURE_TYPE_PNG
		/// @see NPOI.ss.usermodel.Workbook#PICTURE_TYPE_DIB
		public int GetPictureType()
		{
			string contentType = GetPackagePart().ContentType;
			foreach (int key in RELATIONS.Keys)
			{
				if (RELATIONS[key].ContentType.Equals(contentType))
				{
					return key;
				}
			}
			return 0;
		}
	}
}
