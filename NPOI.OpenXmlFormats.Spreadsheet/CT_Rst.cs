using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Rst
	{
		private string tField;

		private List<CT_RElt> rField;

		private List<CT_PhoneticRun> rPhField;

		private CT_PhoneticPr phoneticPrField;

		private string xmltext;

		[XmlElement("t", DataType = "string")]
		public string t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlElement("r")]
		public List<CT_RElt> r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlIgnore]
		public string XmlText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					if (rField != null && rField.Count > 0)
					{
						foreach (CT_RElt item in rField)
						{
							stringWriter.Write("<r>");
							if (item.rPr != null)
							{
								stringWriter.Write("<rPr>");
								if (item.rPr.b != null && item.rPr.b.val)
								{
									stringWriter.Write("<b/>");
								}
								if (item.rPr.i != null && item.rPr.i.val)
								{
									stringWriter.Write("<i/>");
								}
								if (item.rPr.u != null)
								{
									stringWriter.Write("<u val=\"" + item.rPr.u.val + "\"/>");
								}
								if (item.rPr.color != null && item.rPr.color.theme != 0)
								{
									stringWriter.Write("<color theme=\"" + item.rPr.color.theme + "\"/>");
								}
								if (item.rPr.rFont != null)
								{
									stringWriter.Write("<rFont val=\"" + item.rPr.rFont.val + "\"/>");
								}
								if (item.rPr.family != null)
								{
									stringWriter.Write("<family val=\"" + item.rPr.family.val + "\"/>");
								}
								if (item.rPr.charset != null)
								{
									stringWriter.Write("<charset val=\"" + item.rPr.charset.val + "\"/>");
								}
								if (item.rPr.scheme != null)
								{
									stringWriter.Write("<scheme val=\"" + item.rPr.scheme.val + "\"/>");
								}
								if (item.rPr.sz != null)
								{
									stringWriter.Write("<sz val=\"" + item.rPr.sz.val + "\"/>");
								}
								if (item.rPr.vertAlign != null)
								{
									stringWriter.Write("<vertAlign val=\"" + item.rPr.vertAlign.val + "\"/>");
								}
								stringWriter.Write("</rPr>");
							}
							if (item.t != null)
							{
								stringWriter.Write("<t xml:space=\"preserve\">");
								stringWriter.Write(XmlHelper.EncodeXml(item.t));
								stringWriter.Write("</t>");
							}
							stringWriter.Write("</r>");
						}
					}
					if (t != null)
					{
						stringWriter.Write("<t>");
						stringWriter.Write(XmlHelper.EncodeXml(t));
						stringWriter.Write("</t>");
					}
					xmltext = stringBuilder.ToString();
				}
				return xmltext;
			}
			set
			{
				xmltext = value;
			}
		}

		[XmlElement("rPh")]
		public List<CT_PhoneticRun> rPh
		{
			get
			{
				return rPhField;
			}
			set
			{
				rPhField = value;
			}
		}

		[XmlElement("phoneticPr")]
		public CT_PhoneticPr phoneticPr
		{
			get
			{
				return phoneticPrField;
			}
			set
			{
				phoneticPrField = value;
			}
		}

		public void Set(CT_Rst o)
		{
			tField = o.tField;
			rField = o.rField;
			rPhField = o.rPhField;
			phoneticPrField = o.phoneticPrField;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}>", nodeName));
			if (t != null)
			{
				sw.Write(string.Format("<t xml:space=\"preserve\">{0}</t>", XmlHelper.ExcelEncodeString(XmlHelper.EncodeXml(t))));
			}
			if (r != null)
			{
				foreach (CT_RElt item in r)
				{
					item.Write(sw, "r");
				}
			}
			if (rPh != null)
			{
				foreach (CT_PhoneticRun item2 in rPh)
				{
					item2.Write(sw, "rPh");
				}
			}
			if (phoneticPr != null)
			{
				phoneticPr.Write(sw, "phoneticPr");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public bool IsSetT()
		{
			return tField != null;
		}

		public void unsetT()
		{
			tField = null;
		}

		public CT_RElt AddNewR()
		{
			if (rField == null)
			{
				rField = new List<CT_RElt>();
			}
			CT_RElt cT_RElt = new CT_RElt();
			rField.Add(cT_RElt);
			return cT_RElt;
		}

		public int sizeOfRArray()
		{
			if (rField != null)
			{
				return r.Count;
			}
			return 0;
		}

		public CT_RElt GetRArray(int index)
		{
			if (rField != null)
			{
				return rField[index];
			}
			return null;
		}

		public static CT_Rst Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_Rst cT_Rst = new CT_Rst();
			cT_Rst.r = new List<CT_RElt>();
			cT_Rst.rPh = new List<CT_PhoneticRun>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "phoneticPr")
				{
					cT_Rst.phoneticPr = CT_PhoneticPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "r")
				{
					cT_Rst.r.Add(CT_RElt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "rPh")
				{
					cT_Rst.rPh.Add(CT_PhoneticRun.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "t")
				{
					cT_Rst.t = childNode.InnerText.Replace("\r", "");
				}
			}
			return cT_Rst;
		}
	}
}
