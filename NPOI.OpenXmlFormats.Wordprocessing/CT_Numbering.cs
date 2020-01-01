using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("numbering", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_Numbering
	{
		private List<CT_NumPicBullet> numPicBulletField;

		private List<CT_AbstractNum> abstractNumField;

		private List<CT_Num> numField;

		private CT_DecimalNumber numIdMacAtCleanupField;

		[XmlElement("numPicBullet", Order = 0)]
		public List<CT_NumPicBullet> numPicBullet
		{
			get
			{
				return numPicBulletField;
			}
			set
			{
				numPicBulletField = value;
			}
		}

		[XmlElement("abstractNum", Order = 1)]
		public List<CT_AbstractNum> abstractNum
		{
			get
			{
				return abstractNumField;
			}
			set
			{
				abstractNumField = value;
			}
		}

		[XmlElement("num", Order = 2)]
		public List<CT_Num> num
		{
			get
			{
				return numField;
			}
			set
			{
				numField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_DecimalNumber numIdMacAtCleanup
		{
			get
			{
				return numIdMacAtCleanupField;
			}
			set
			{
				numIdMacAtCleanupField = value;
			}
		}

		public static CT_Numbering Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Numbering cT_Numbering = new CT_Numbering();
			cT_Numbering.numPicBullet = new List<CT_NumPicBullet>();
			cT_Numbering.abstractNum = new List<CT_AbstractNum>();
			cT_Numbering.num = new List<CT_Num>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "numIdMacAtCleanup")
				{
					cT_Numbering.numIdMacAtCleanup = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numPicBullet")
				{
					cT_Numbering.numPicBullet.Add(CT_NumPicBullet.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "abstractNum")
				{
					cT_Numbering.abstractNum.Add(CT_AbstractNum.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "num")
				{
					cT_Numbering.num.Add(CT_Num.Parse(childNode, namespaceManager));
				}
			}
			return cT_Numbering;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<w:numbering xmlns:ve=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
			sw.Write("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" ");
			sw.Write("xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:wp=\"http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing\" ");
			sw.Write("xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" ");
			sw.Write("xmlns:wne=\"http://schemas.microsoft.com/office/word/2006/wordml\">");
			if (numIdMacAtCleanup != null)
			{
				numIdMacAtCleanup.Write(sw, "numIdMacAtCleanup");
			}
			if (numPicBullet != null)
			{
				foreach (CT_NumPicBullet item in numPicBullet)
				{
					item.Write(sw, "numPicBullet");
				}
			}
			if (abstractNum != null)
			{
				foreach (CT_AbstractNum item2 in abstractNum)
				{
					item2.Write(sw, "abstractNum");
				}
			}
			if (num != null)
			{
				foreach (CT_Num item3 in num)
				{
					item3.Write(sw, "num");
				}
			}
			sw.Write("</w:numbering>");
		}

		public IList<CT_Num> GetNumList()
		{
			return numField;
		}

		public IList<CT_AbstractNum> GetAbstractNumList()
		{
			return abstractNumField;
		}

		public CT_Num AddNewNum()
		{
			CT_Num cT_Num = new CT_Num();
			if (numField == null)
			{
				numField = new List<CT_Num>();
			}
			numField.Add(cT_Num);
			return cT_Num;
		}

		public void SetNumArray(int pos, CT_Num ct_Num)
		{
			if (numField == null)
			{
				numField = new List<CT_Num>();
			}
			if (pos < 0 || pos >= numField.Count)
			{
				numField.Add(ct_Num);
			}
			numField[pos] = ct_Num;
		}

		public CT_AbstractNum AddNewAbstractNum()
		{
			CT_AbstractNum cT_AbstractNum = new CT_AbstractNum();
			if (abstractNumField == null)
			{
				abstractNumField = new List<CT_AbstractNum>();
			}
			abstractNumField.Add(cT_AbstractNum);
			return cT_AbstractNum;
		}

		public void SetAbstractNumArray(int pos, CT_AbstractNum cT_AbstractNum)
		{
			if (abstractNumField == null)
			{
				abstractNumField = new List<CT_AbstractNum>();
			}
			if (pos < 0 || pos >= abstractNumField.Count)
			{
				abstractNumField.Add(cT_AbstractNum);
			}
			abstractNumField[pos] = cT_AbstractNum;
		}

		public void RemoveAbstractNum(int p)
		{
			if (abstractNumField != null)
			{
				abstractNumField.RemoveAt(p);
			}
		}
	}
}
