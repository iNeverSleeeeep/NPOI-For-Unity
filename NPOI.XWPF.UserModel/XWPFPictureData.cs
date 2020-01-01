using NPOI.OpenXml4Net.OPC;
using NPOI.Util;
using System.IO;

namespace NPOI.XWPF.UserModel
{
	/// <summary>
	/// Raw picture data, normally attached to a WordProcessingML Drawing. As a rule, pictures are stored in the /word/media/ part of a WordProcessingML package.
	/// </summary>
	/// <remarks>
	/// @author Philipp Epp
	/// </remarks>
	public class XWPFPictureData : POIXMLDocumentPart
	{
		/// Relationships for each known picture type
		internal static POIXMLRelation[] RELATIONS;

		private long? checksum = null;

		/// Gets the picture data as a byte array.
		/// <p>
		/// Note, that this call might be expensive since all the picture data is copied into a temporary byte array.
		/// You can grab the picture data directly from the underlying package part as follows:
		/// <br />
		/// <code>
		/// InputStream is1 = GetPackagePart().InputStream;
		/// </code>
		/// </p>
		/// @return the Picture data.
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

		/// Returns the file name of the image, eg image7.jpg . The original filename
		/// isn't always available, but if it can be found it's likely to be in the
		/// CTDrawing
		public string FileName
		{
			get
			{
				string name = GetPackagePart().PartName.Name;
				if (name == null)
				{
					return null;
				}
				return name.Substring(name.LastIndexOf('/') + 1);
			}
		}

		public long Checksum
		{
			get
			{
				if (!checksum.HasValue)
				{
					Stream stream = null;
					byte[] data;
					try
					{
						stream = GetPackagePart().GetInputStream();
						data = IOUtils.ToByteArray(stream);
					}
					catch (IOException ex)
					{
						throw new POIXMLException(ex);
					}
					finally
					{
						try
						{
							stream.Close();
						}
						catch (IOException ex2)
						{
							throw new POIXMLException(ex2);
						}
					}
					checksum = IOUtils.CalculateChecksum(data);
				}
				return checksum.Value;
			}
		}

		static XWPFPictureData()
		{
			RELATIONS = new POIXMLRelation[13];
			RELATIONS[2] = XWPFRelation.IMAGE_EMF;
			RELATIONS[3] = XWPFRelation.IMAGE_WMF;
			RELATIONS[4] = XWPFRelation.IMAGE_PICT;
			RELATIONS[5] = XWPFRelation.IMAGE_JPEG;
			RELATIONS[6] = XWPFRelation.IMAGE_PNG;
			RELATIONS[7] = XWPFRelation.IMAGE_DIB;
			RELATIONS[8] = XWPFRelation.IMAGE_GIF;
			RELATIONS[9] = XWPFRelation.IMAGE_TIFF;
			RELATIONS[10] = XWPFRelation.IMAGE_EPS;
			RELATIONS[11] = XWPFRelation.IMAGE_BMP;
			RELATIONS[12] = XWPFRelation.IMAGE_WPG;
		}

		/// Create a new XWPFGraphicData node
		protected XWPFPictureData()
		{
		}

		/// Construct XWPFPictureData from a package part
		///
		/// @param part the package part holding the Drawing data,
		/// @param rel  the package relationship holding this Drawing,
		/// the relationship type must be http://schemas.Openxmlformats.org/officeDocument/2006/relationships/image
		public XWPFPictureData(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
		}

		internal override void OnDocumentRead()
		{
			base.OnDocumentRead();
		}

		/// Suggests a file extension for this image.
		/// @return the file extension.
		public string SuggestFileExtension()
		{
			return GetPackagePart().PartName.Extension;
		}

		/// Return an integer constant that specifies type of this picture
		///
		/// @return an integer constant that specifies type of this picture
		/// @see NPOI.XWPF.UserModel.PictureTypeEMF
		/// @see NPOI.XWPF.UserModel.PictureTypeWMF
		/// @see NPOI.XWPF.UserModel.PictureTypePICT
		/// @see NPOI.XWPF.UserModel.PictureTypeJPEG
		/// @see NPOI.XWPF.UserModel.PictureTypePNG
		/// @see NPOI.XWPF.UserModel.PictureTypeDIB
		public int GetPictureType()
		{
			string contentType = GetPackagePart().ContentType;
			for (int i = 0; i < RELATIONS.Length; i++)
			{
				if (RELATIONS[i] != null && RELATIONS[i].ContentType.Equals(contentType))
				{
					return i;
				}
			}
			return 0;
		}

		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (!(obj is XWPFPictureData))
			{
				return false;
			}
			XWPFPictureData xWPFPictureData = (XWPFPictureData)obj;
			PackagePart packagePart = xWPFPictureData.GetPackagePart();
			PackagePart packagePart2 = GetPackagePart();
			if ((packagePart != null && packagePart2 == null) || (packagePart == null && packagePart2 != null))
			{
				return false;
			}
			if (packagePart2 != null)
			{
				OPCPackage package = packagePart.Package;
				OPCPackage package2 = packagePart2.Package;
				if ((package != null && package2 == null) || (package == null && package2 != null))
				{
					return false;
				}
				if (package2 != null && !package2.Equals(package))
				{
					return false;
				}
			}
			long obj2 = xWPFPictureData.Checksum;
			if (!Checksum.Equals(obj2))
			{
				return false;
			}
			return Arrays.Equals(Data, xWPFPictureData.Data);
		}

		public override int GetHashCode()
		{
			return Checksum.GetHashCode();
		}
	}
}
