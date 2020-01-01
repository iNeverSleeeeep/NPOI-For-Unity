using NPOI.OpenXmlFormats.Dml.Chart;
using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Dml
{
	public class ChartSpaceDocument
	{
		private CT_ChartSpace chartSpace;

		public ChartSpaceDocument()
		{
			chartSpace = new CT_ChartSpace();
		}

		public ChartSpaceDocument(CT_ChartSpace chartspace)
		{
			chartSpace = chartspace;
		}

		public static ChartSpaceDocument Parse(XmlDocument xmldoc, XmlNamespaceManager namespaceMgr)
		{
			CT_ChartSpace chartspace = CT_ChartSpace.Parse(xmldoc.DocumentElement, namespaceMgr);
			return new ChartSpaceDocument(chartspace);
		}

		public CT_ChartSpace GetChartSpace()
		{
			return chartSpace;
		}

		public void SetChartSpace(CT_ChartSpace chartspace)
		{
			chartSpace = chartspace;
		}

		public void Save(Stream stream)
		{
			chartSpace.Write(stream);
		}
	}
}
