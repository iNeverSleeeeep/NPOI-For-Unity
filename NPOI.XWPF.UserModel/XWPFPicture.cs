using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Picture;

namespace NPOI.XWPF.UserModel
{
	/// @author Philipp Epp
	public class XWPFPicture
	{
		private CT_Picture ctPic;

		private string description;

		private XWPFRun run;

		public XWPFPicture(CT_Picture ctPic, XWPFRun Run)
		{
			run = Run;
			this.ctPic = ctPic;
			description = ctPic.nvPicPr.cNvPr.descr;
		}

		/// Link Picture with PictureData
		/// @param rel
		public void SetPictureReference(PackageRelationship rel)
		{
			ctPic.blipFill.blip.embed = rel.Id;
		}

		/// Return the underlying CTPicture bean that holds all properties for this picture
		///
		/// @return the underlying CTPicture bean
		public CT_Picture GetCTPicture()
		{
			return ctPic;
		}

		/// Get the PictureData of the Picture, if present.
		/// Note - not all kinds of picture have data
		public XWPFPictureData GetPictureData()
		{
			CT_BlipFillProperties blipFill = ctPic.blipFill;
			if (blipFill == null || !blipFill.IsSetBlip())
			{
				return null;
			}
			string embed = blipFill.blip.embed;
			POIXMLDocumentPart part = run.Paragraph.GetPart();
			if (part != null)
			{
				POIXMLDocumentPart relationById = part.GetRelationById(embed);
				if (relationById is XWPFPictureData)
				{
					return (XWPFPictureData)relationById;
				}
			}
			return null;
		}

		public string GetDescription()
		{
			return description;
		}
	}
}
