using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	[XmlRoot("wsDr", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing", IsNullable = true)]
	public class CT_Drawing
	{
		private List<IEG_Anchor> cellAnchors = new List<IEG_Anchor>();

		[XmlIgnore]
		public List<IEG_Anchor> CellAnchors
		{
			get
			{
				return cellAnchors;
			}
			set
			{
				cellAnchors = value;
			}
		}

		public CT_TwoCellAnchor AddNewTwoCellAnchor()
		{
			CT_TwoCellAnchor cT_TwoCellAnchor = new CT_TwoCellAnchor();
			cellAnchors.Add(cT_TwoCellAnchor);
			return cT_TwoCellAnchor;
		}

		public int SizeOfTwoCellAnchorArray()
		{
			int num = 0;
			foreach (IEG_Anchor cellAnchor in cellAnchors)
			{
				if (cellAnchor is CT_TwoCellAnchor)
				{
					num++;
				}
			}
			return num;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
				streamWriter.Write("<xdr:wsDr xmlns:xdr=\"http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\">");
				foreach (IEG_Anchor cellAnchor in cellAnchors)
				{
					cellAnchor.Write(streamWriter);
				}
				streamWriter.Write("</xdr:wsDr>");
			}
		}

		public void Set(CT_Drawing ctDrawing)
		{
			cellAnchors.Clear();
			foreach (IEG_Anchor cellAnchor in ctDrawing.cellAnchors)
			{
				cellAnchors.Add(cellAnchor);
			}
		}

		public int SizeOfAbsoluteAnchorArray()
		{
			return 0;
		}

		public int SizeOfOneCellAnchorArray()
		{
			int num = 0;
			foreach (IEG_Anchor cellAnchor in cellAnchors)
			{
				if (cellAnchor is CT_OneCellAnchor)
				{
					num++;
				}
			}
			return num;
		}

		public static CT_Drawing Parse(XmlDocument xmldoc, XmlNamespaceManager namespaceManager)
		{
			XmlNodeList xmlNodeList = xmldoc.SelectNodes("/xdr:wsDr/*", namespaceManager);
			CT_Drawing cT_Drawing = new CT_Drawing();
			foreach (XmlNode item4 in xmlNodeList)
			{
				if (item4.LocalName == "twoCellAnchor")
				{
					CT_TwoCellAnchor item = CT_TwoCellAnchor.Parse(item4, namespaceManager);
					cT_Drawing.cellAnchors.Add(item);
				}
				else if (item4.LocalName == "oneCellAnchor")
				{
					CT_OneCellAnchor item2 = CT_OneCellAnchor.Parse(item4, namespaceManager);
					cT_Drawing.cellAnchors.Add(item2);
				}
				else if (item4.LocalName == "absCellAnchor")
				{
					CT_AbsoluteCellAnchor item3 = CT_AbsoluteCellAnchor.Parse(item4, namespaceManager);
					cT_Drawing.cellAnchors.Add(item3);
				}
			}
			return cT_Drawing;
		}
	}
}
