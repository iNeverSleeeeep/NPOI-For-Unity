using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "calcChain")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CalcChain
	{
		private List<CT_CalcCell> cField = new List<CT_CalcCell>();

		private CT_ExtensionList extLstField;

		[XmlElement("c")]
		public List<CT_CalcCell> c
		{
			get
			{
				return cField;
			}
			set
			{
				cField = value;
			}
		}

		[XmlElement("extLst")]
		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlIgnore]
		public bool extLstSpecified
		{
			get
			{
				return null != extLst;
			}
		}

		public int SizeOfCArray()
		{
			return c.Count;
		}

		public CT_CalcCell GetCArray(int index)
		{
			return c[index];
		}

		public void AddC(CT_CalcCell cell)
		{
			c.Add(cell);
		}

		public void RemoveC(int index)
		{
			c.RemoveAt(index);
		}
	}
}
