using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[DesignerCategory("code")]
	public class CT_Formulas
	{
		private List<CT_F> fField;

		[XmlElement("f", Form = XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:vml")]
		public List<CT_F> f
		{
			get
			{
				return fField;
			}
			set
			{
				fField = value;
			}
		}

		[XmlIgnore]
		public bool fSpecified
		{
			get
			{
				return null != fField;
			}
		}

		public CT_F AddNewF()
		{
			if (fField == null)
			{
				fField = new List<CT_F>();
			}
			fField.Add(new CT_F());
			return fField[fField.Count - 1];
		}

		public static CT_Formulas Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Formulas cT_Formulas = new CT_Formulas();
			cT_Formulas.f = new List<CT_F>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "f")
				{
					cT_Formulas.f.Add(CT_F.Parse(childNode, namespaceManager));
				}
			}
			return cT_Formulas;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			sw.Write(">");
			if (f != null)
			{
				foreach (CT_F item in f)
				{
					item.Write(sw, "f");
				}
			}
			sw.Write(string.Format("</v:{0}>", nodeName));
		}
	}
}
