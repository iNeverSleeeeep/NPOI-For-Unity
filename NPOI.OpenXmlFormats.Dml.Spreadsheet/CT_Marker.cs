using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_Marker
	{
		private int _col;

		private long _colOff;

		private int _row;

		private long _rowOff;

		public int col
		{
			get
			{
				return _col;
			}
			set
			{
				_col = value;
			}
		}

		public long colOff
		{
			get
			{
				return _colOff;
			}
			set
			{
				_colOff = value;
			}
		}

		public int row
		{
			get
			{
				return _row;
			}
			set
			{
				_row = value;
			}
		}

		public long rowOff
		{
			get
			{
				return _rowOff;
			}
			set
			{
				_rowOff = value;
			}
		}

		public static CT_Marker Parse(XmlNode node, XmlNamespaceManager nameSpaceManager)
		{
			CT_Marker cT_Marker = new CT_Marker();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "col")
				{
					cT_Marker.col = int.Parse(childNode.InnerText);
				}
				else if (childNode.LocalName == "colOff")
				{
					cT_Marker.colOff = long.Parse(childNode.InnerText);
				}
				else if (childNode.LocalName == "row")
				{
					cT_Marker.row = int.Parse(childNode.InnerText);
				}
				else if (childNode.LocalName == "rowOff")
				{
					cT_Marker.rowOff = long.Parse(childNode.InnerText);
				}
			}
			return cT_Marker;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				stringWriter.Write("<xdr:col>");
				stringWriter.Write(col.ToString());
				stringWriter.Write("</xdr:col>");
				stringWriter.Write("<xdr:colOff>");
				stringWriter.Write(colOff.ToString());
				stringWriter.Write("</xdr:colOff>");
				stringWriter.Write("<xdr:row>");
				stringWriter.Write(row.ToString());
				stringWriter.Write("</xdr:row>");
				stringWriter.Write("<xdr:rowOff>");
				stringWriter.Write(rowOff.ToString());
				stringWriter.Write("</xdr:rowOff>");
			}
			return stringBuilder.ToString();
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}>", nodeName));
			sw.Write("<xdr:col>");
			sw.Write(col.ToString());
			sw.Write("</xdr:col>");
			sw.Write("<xdr:colOff>");
			sw.Write(colOff.ToString());
			sw.Write("</xdr:colOff>");
			sw.Write("<xdr:row>");
			sw.Write(row.ToString());
			sw.Write("</xdr:row>");
			sw.Write("<xdr:rowOff>");
			sw.Write(rowOff.ToString());
			sw.Write("</xdr:rowOff>");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
