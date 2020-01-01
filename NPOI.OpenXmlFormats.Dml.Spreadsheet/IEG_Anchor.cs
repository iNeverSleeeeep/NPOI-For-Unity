using System.IO;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	public interface IEG_Anchor
	{
		CT_Shape sp
		{
			get;
			set;
		}

		CT_Connector connector
		{
			get;
			set;
		}

		CT_GraphicalObjectFrame graphicFrame
		{
			get;
			set;
		}

		CT_Picture picture
		{
			get;
			set;
		}

		CT_GroupShape groupShape
		{
			get;
			set;
		}

		CT_AnchorClientData clientData
		{
			get;
			set;
		}

		void Write(StreamWriter sw);
	}
}
