using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_LineProperties
	{
		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_PresetLineDashProperties prstDashField;

		private List<CT_DashStop> custDashField;

		private CT_LineJoinRound roundField;

		private CT_LineJoinBevel bevelField;

		private CT_LineJoinMiterProperties miterField;

		private CT_LineEndProperties headEndField;

		private CT_LineEndProperties tailEndField;

		private CT_OfficeArtExtensionList extLstField;

		private int wField;

		private bool wFieldSpecified;

		private ST_LineCap capField;

		private bool capFieldSpecified;

		private ST_CompoundLine cmpdField;

		private bool cmpdFieldSpecified;

		private ST_PenAlignment algnField;

		private bool algnFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_NoFillProperties noFill
		{
			get
			{
				return noFillField;
			}
			set
			{
				noFillField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_SolidColorFillProperties solidFill
		{
			get
			{
				return solidFillField;
			}
			set
			{
				solidFillField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_GradientFillProperties gradFill
		{
			get
			{
				return gradFillField;
			}
			set
			{
				gradFillField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_PatternFillProperties pattFill
		{
			get
			{
				return pattFillField;
			}
			set
			{
				pattFillField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_PresetLineDashProperties prstDash
		{
			get
			{
				return prstDashField;
			}
			set
			{
				prstDashField = value;
			}
		}

		[XmlElement(Order = 5)]
		public List<CT_DashStop> custDash
		{
			get
			{
				return custDashField;
			}
			set
			{
				custDashField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_LineJoinRound round
		{
			get
			{
				return roundField;
			}
			set
			{
				roundField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_LineJoinBevel bevel
		{
			get
			{
				return bevelField;
			}
			set
			{
				bevelField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_LineJoinMiterProperties miter
		{
			get
			{
				return miterField;
			}
			set
			{
				miterField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_LineEndProperties headEnd
		{
			get
			{
				return headEndField;
			}
			set
			{
				headEndField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_LineEndProperties tailEnd
		{
			get
			{
				return tailEndField;
			}
			set
			{
				tailEndField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_OfficeArtExtensionList extLst
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

		[XmlAttribute]
		public int w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		[XmlIgnore]
		public bool wSpecified
		{
			get
			{
				return wFieldSpecified;
			}
			set
			{
				wFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_LineCap cap
		{
			get
			{
				return capField;
			}
			set
			{
				capField = value;
			}
		}

		[XmlIgnore]
		public bool capSpecified
		{
			get
			{
				return ST_LineCap.NONE != capField;
			}
			set
			{
				capFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_CompoundLine cmpd
		{
			get
			{
				return cmpdField;
			}
			set
			{
				cmpdField = value;
			}
		}

		[XmlIgnore]
		public bool cmpdSpecified
		{
			get
			{
				return ST_CompoundLine.NONE != cmpdField;
			}
			set
			{
				cmpdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_PenAlignment algn
		{
			get
			{
				return algnField;
			}
			set
			{
				algnField = value;
			}
		}

		[XmlIgnore]
		public bool algnSpecified
		{
			get
			{
				return ST_PenAlignment.NONE != algnField;
			}
			set
			{
				algnFieldSpecified = value;
			}
		}

		public static CT_LineProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LineProperties cT_LineProperties = new CT_LineProperties();
			cT_LineProperties.w = XmlHelper.ReadInt(node.Attributes["w"]);
			if (node.Attributes["cap"] != null)
			{
				cT_LineProperties.cap = (ST_LineCap)Enum.Parse(typeof(ST_LineCap), node.Attributes["cap"].Value);
			}
			if (node.Attributes["cmpd"] != null)
			{
				cT_LineProperties.cmpd = (ST_CompoundLine)Enum.Parse(typeof(ST_CompoundLine), node.Attributes["cmpd"].Value);
			}
			if (node.Attributes["algn"] != null)
			{
				cT_LineProperties.algn = (ST_PenAlignment)Enum.Parse(typeof(ST_PenAlignment), node.Attributes["algn"].Value);
			}
			cT_LineProperties.custDash = new List<CT_DashStop>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "noFill")
				{
					cT_LineProperties.noFill = new CT_NoFillProperties();
				}
				else if (childNode.LocalName == "solidFill")
				{
					cT_LineProperties.solidFill = CT_SolidColorFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gradFill")
				{
					cT_LineProperties.gradFill = CT_GradientFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pattFill")
				{
					cT_LineProperties.pattFill = CT_PatternFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "prstDash")
				{
					cT_LineProperties.prstDash = CT_PresetLineDashProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "round")
				{
					cT_LineProperties.round = new CT_LineJoinRound();
				}
				else if (childNode.LocalName == "bevel")
				{
					cT_LineProperties.bevel = new CT_LineJoinBevel();
				}
				else if (childNode.LocalName == "miter")
				{
					cT_LineProperties.miter = CT_LineJoinMiterProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "headEnd")
				{
					cT_LineProperties.headEnd = CT_LineEndProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tailEnd")
				{
					cT_LineProperties.tailEnd = CT_LineEndProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_LineProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "custDash")
				{
					cT_LineProperties.custDash.Add(CT_DashStop.Parse(childNode, namespaceManager));
				}
			}
			return cT_LineProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w", w);
			if (cap != 0)
			{
				XmlHelper.WriteAttribute(sw, "cap", cap.ToString());
			}
			if (cmpd != 0)
			{
				XmlHelper.WriteAttribute(sw, "cmpd", cmpd.ToString());
			}
			if (algn != 0)
			{
				XmlHelper.WriteAttribute(sw, "algn", algn.ToString());
			}
			sw.Write(">");
			if (noFill != null)
			{
				sw.Write("<a:noFill/>");
			}
			if (solidFill != null)
			{
				solidFill.Write(sw, "solidFill");
			}
			if (gradFill != null)
			{
				gradFill.Write(sw, "gradFill");
			}
			if (pattFill != null)
			{
				pattFill.Write(sw, "pattFill");
			}
			if (prstDash != null)
			{
				prstDash.Write(sw, "prstDash");
			}
			if (custDash != null)
			{
				foreach (CT_DashStop item in custDash)
				{
					item.Write(sw, "custDash");
				}
			}
			if (round != null)
			{
				sw.Write("<a:round/>");
			}
			if (bevel != null)
			{
				sw.Write("<a:bevel/>");
			}
			if (miter != null)
			{
				miter.Write(sw, "miter");
			}
			if (headEnd != null)
			{
				headEnd.Write(sw, "headEnd");
			}
			if (tailEnd != null)
			{
				tailEnd.Write(sw, "tailEnd");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_SolidColorFillProperties AddNewSolidFill()
		{
			solidFillField = new CT_SolidColorFillProperties();
			return solidFillField;
		}

		public bool IsSetSolidFill()
		{
			return solidFillField != null;
		}
	}
}
