using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Fill
	{
		private CT_PatternFill patternFillField;

		private CT_GradientFill gradientFillField;

		[XmlElement]
		public CT_PatternFill patternFill
		{
			get
			{
				return patternFillField;
			}
			set
			{
				patternFillField = value;
			}
		}

		[XmlElement]
		public CT_GradientFill gradientFill
		{
			get
			{
				return gradientFillField;
			}
			set
			{
				gradientFillField = value;
			}
		}

		public CT_PatternFill GetPatternFill()
		{
			return patternFillField;
		}

		public CT_PatternFill AddNewPatternFill()
		{
			patternFillField = new CT_PatternFill();
			return GetPatternFill();
		}

		public bool IsSetPatternFill()
		{
			return patternFillField != null;
		}

		public CT_Fill Copy()
		{
			CT_Fill cT_Fill = new CT_Fill();
			cT_Fill.patternFillField = patternFillField;
			cT_Fill.gradientFillField = gradientFillField;
			return cT_Fill;
		}

		public static CT_Fill Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Fill cT_Fill = new CT_Fill();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "patternFill")
				{
					cT_Fill.patternFill = CT_PatternFill.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gradientFill")
				{
					cT_Fill.gradientFill = CT_GradientFill.Parse(childNode, namespaceManager);
				}
			}
			return cT_Fill;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (patternFill != null)
			{
				patternFill.Write(sw, "patternFill");
			}
			if (gradientFill != null)
			{
				gradientFill.Write(sw, "gradientFill");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public override string ToString()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				Write(streamWriter, "fill");
				streamWriter.Flush();
				memoryStream.Position = 0L;
				StreamReader streamReader = new StreamReader(memoryStream);
				return streamReader.ReadToEnd();
			}
		}
	}
}
