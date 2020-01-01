using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using NPOI.OpenXmlFormats.Vml.Office;
using NPOI.OpenXmlFormats.Vml.Presentation;
using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using NPOI.OpenXmlFormats.Vml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlRoot("shapetype", Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_Shapetype
	{
		private CT_Path pathField;

		private List<CT_Formulas> formulasField = new List<CT_Formulas>();

		private List<CT_Handles> handlesField = new List<CT_Handles>();

		private List<CT_Fill> fillField = new List<CT_Fill>();

		private CT_Stroke strokeField;

		private List<CT_Shadow> shadowField;

		private List<CT_Textbox> textboxField;

		private List<CT_TextPath> textpathField = new List<CT_TextPath>();

		private List<CT_ImageData> imagedataField;

		private List<CT_Wrap> wrapField;

		private List<CT_AnchorLock> anchorlockField;

		private CT_Lock lockField;

		private List<CT_Border> bordertopField;

		private List<CT_Border> borderbottomField;

		private List<CT_Border> borderleftField;

		private List<CT_Border> borderrightField;

		private List<CT_ClientData> clientDataField;

		private List<CT_Rel> textdataField;

		private string adjField;

		private string idField;

		private ST_TrueFalse filledField;

		private ST_TrueFalse strokedField;

		private ST_TrueFalse preferrelativeField;

		private string styleField;

		private float sptField;

		private string coordsizeField;

		private string path1Field;

		[XmlAttribute]
		public ST_TrueFalse stroked
		{
			get
			{
				return strokedField;
			}
			set
			{
				strokedField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse filled
		{
			get
			{
				return filledField;
			}
			set
			{
				filledField = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public ST_TrueFalse preferrelative
		{
			get
			{
				return preferrelativeField;
			}
			set
			{
				preferrelativeField = value;
			}
		}

		[XmlAttribute]
		public string coordsize
		{
			get
			{
				return coordsizeField;
			}
			set
			{
				coordsizeField = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public float spt
		{
			get
			{
				return sptField;
			}
			set
			{
				sptField = value;
			}
		}

		[XmlAttribute]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlElement("stroke")]
		public CT_Stroke stroke
		{
			get
			{
				return strokeField;
			}
			set
			{
				strokeField = value;
			}
		}

		[XmlElement("formulas")]
		public List<CT_Formulas> formulas
		{
			get
			{
				return formulasField;
			}
			set
			{
				formulasField = value;
			}
		}

		[XmlElement("handles")]
		public List<CT_Handles> handles
		{
			get
			{
				return handlesField;
			}
			set
			{
				handlesField = value;
			}
		}

		[XmlElement("fill")]
		public List<CT_Fill> fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlElement("shadow")]
		public List<CT_Shadow> shadow
		{
			get
			{
				return shadowField;
			}
			set
			{
				shadowField = value;
			}
		}

		[XmlElement("textbox")]
		public List<CT_Textbox> textbox
		{
			get
			{
				return textboxField;
			}
			set
			{
				textboxField = value;
			}
		}

		[XmlElement("textpath")]
		public List<CT_TextPath> textpath
		{
			get
			{
				return textpathField;
			}
			set
			{
				textpathField = value;
			}
		}

		[XmlElement("imagedata")]
		public List<CT_ImageData> imagedata
		{
			get
			{
				return imagedataField;
			}
			set
			{
				imagedataField = value;
			}
		}

		[XmlElement("wrap", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Wrap> wrap
		{
			get
			{
				return wrapField;
			}
			set
			{
				wrapField = value;
			}
		}

		[XmlElement("anchorlock", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_AnchorLock> anchorlock
		{
			get
			{
				return anchorlockField;
			}
			set
			{
				anchorlockField = value;
			}
		}

		[XmlElement("path")]
		public CT_Path path
		{
			get
			{
				return pathField;
			}
			set
			{
				pathField = value;
			}
		}

		[XmlElement("lock", Namespace = "urn:schemas-microsoft-com:office:office")]
		public CT_Lock @lock
		{
			get
			{
				return lockField;
			}
			set
			{
				lockField = value;
			}
		}

		[XmlElement("bordertop", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> bordertop
		{
			get
			{
				return bordertopField;
			}
			set
			{
				bordertopField = value;
			}
		}

		[XmlElement("borderbottom", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> borderbottom
		{
			get
			{
				return borderbottomField;
			}
			set
			{
				borderbottomField = value;
			}
		}

		[XmlElement("borderleft", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> borderleft
		{
			get
			{
				return borderleftField;
			}
			set
			{
				borderleftField = value;
			}
		}

		[XmlElement("borderright", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> borderright
		{
			get
			{
				return borderrightField;
			}
			set
			{
				borderrightField = value;
			}
		}

		[XmlElement("clientdata", Namespace = "urn:schemas-microsoft-com:office:excel")]
		public List<CT_ClientData> Clientdata
		{
			get
			{
				return clientDataField;
			}
			set
			{
				clientDataField = value;
			}
		}

		[XmlElement("textdata", Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
		public List<CT_Rel> textdata
		{
			get
			{
				return textdataField;
			}
			set
			{
				textdataField = value;
			}
		}

		[XmlAttribute]
		public string adj
		{
			get
			{
				return adjField;
			}
			set
			{
				adjField = value;
			}
		}

		[XmlAttribute("path")]
		public string path2
		{
			get
			{
				return path1Field;
			}
			set
			{
				path1Field = value;
			}
		}

		public static CT_Shapetype Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shapetype cT_Shapetype = new CT_Shapetype();
			if (node.Attributes["stroked"] != null)
			{
				cT_Shapetype.stroked = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["stroked"]);
			}
			if (node.Attributes["filled"] != null)
			{
				cT_Shapetype.filled = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["filled"]);
			}
			if (node.Attributes["o:preferrelative"] != null)
			{
				cT_Shapetype.preferrelative = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["o:preferrelative"]);
			}
			cT_Shapetype.coordsize = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["coordsize"]);
			if (node.Attributes["o:spt"] != null)
			{
				cT_Shapetype.id = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["id"]);
			}
			cT_Shapetype.adj = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["adj"]);
			cT_Shapetype.path2 = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["path"]);
			cT_Shapetype.formulas = new List<CT_Formulas>();
			cT_Shapetype.handles = new List<CT_Handles>();
			cT_Shapetype.fill = new List<CT_Fill>();
			cT_Shapetype.shadow = new List<CT_Shadow>();
			cT_Shapetype.textbox = new List<CT_Textbox>();
			cT_Shapetype.textpath = new List<CT_TextPath>();
			cT_Shapetype.imagedata = new List<CT_ImageData>();
			cT_Shapetype.wrap = new List<CT_Wrap>();
			cT_Shapetype.anchorlock = new List<CT_AnchorLock>();
			cT_Shapetype.bordertop = new List<CT_Border>();
			cT_Shapetype.borderbottom = new List<CT_Border>();
			cT_Shapetype.borderleft = new List<CT_Border>();
			cT_Shapetype.borderright = new List<CT_Border>();
			cT_Shapetype.Clientdata = new List<CT_ClientData>();
			cT_Shapetype.textdata = new List<CT_Rel>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "stroke")
				{
					cT_Shapetype.stroke = CT_Stroke.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "path")
				{
					cT_Shapetype.path = CT_Path.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lock")
				{
					cT_Shapetype.@lock = CT_Lock.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "formulas")
				{
					cT_Shapetype.formulas.Add(CT_Formulas.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "handles")
				{
					cT_Shapetype.handles.Add(CT_Handles.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "fill")
				{
					cT_Shapetype.fill.Add(CT_Fill.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "shadow")
				{
					cT_Shapetype.shadow.Add(CT_Shadow.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "textbox")
				{
					cT_Shapetype.textbox.Add(CT_Textbox.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "textpath")
				{
					cT_Shapetype.textpath.Add(CT_TextPath.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "imagedata")
				{
					cT_Shapetype.imagedata.Add(CT_ImageData.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "wrap")
				{
					cT_Shapetype.wrap.Add(CT_Wrap.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "anchorlock")
				{
					cT_Shapetype.anchorlock.Add(new CT_AnchorLock());
				}
				else if (childNode.LocalName == "bordertop")
				{
					cT_Shapetype.bordertop.Add(CT_Border.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "borderbottom")
				{
					cT_Shapetype.borderbottom.Add(CT_Border.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "borderleft")
				{
					cT_Shapetype.borderleft.Add(CT_Border.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "borderright")
				{
					cT_Shapetype.borderright.Add(CT_Border.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "ClientData")
				{
					cT_Shapetype.Clientdata.Add(CT_ClientData.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "textdata")
				{
					cT_Shapetype.textdata.Add(CT_Rel.Parse(childNode, namespaceManager));
				}
			}
			return cT_Shapetype;
		}

		public void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "stroked", stroked);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "filled", filled);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "o:preferrelative", preferrelative);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "coordsize", coordsize);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:spt", (double)spt);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "id", id);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "adj", adj);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "path", path2);
			sw.Write(">");
			if (stroke != null)
			{
				stroke.Write(sw, "stroke");
			}
			if (formulas != null)
			{
				foreach (CT_Formulas formula in formulas)
				{
					formula.Write(sw, "formulas");
				}
			}
			if (handles != null)
			{
				foreach (CT_Handles handle in handles)
				{
					handle.Write(sw, "handles");
				}
			}
			if (fill != null)
			{
				foreach (CT_Fill item in fill)
				{
					item.Write(sw, "fill");
				}
			}
			if (shadow != null)
			{
				foreach (CT_Shadow item2 in shadow)
				{
					item2.Write(sw, "shadow");
				}
			}
			if (path != null)
			{
				path.Write(sw, "path");
			}
			if (@lock != null)
			{
				@lock.Write(sw, "lock");
			}
			if (textbox != null)
			{
				foreach (CT_Textbox item3 in textbox)
				{
					item3.Write(sw, "textbox");
				}
			}
			if (textpath != null)
			{
				foreach (CT_TextPath item4 in textpath)
				{
					item4.Write(sw, "textpath");
				}
			}
			if (imagedata != null)
			{
				foreach (CT_ImageData imagedatum in imagedata)
				{
					imagedatum.Write(sw, "imagedata");
				}
			}
			if (wrap != null)
			{
				foreach (CT_Wrap item5 in wrap)
				{
					item5.Write(sw, "wrap");
				}
			}
			if (anchorlock != null)
			{
				foreach (CT_AnchorLock item6 in anchorlock)
				{
					CT_AnchorLock cT_AnchorLock = item6;
					sw.Write("<anchorlock/>");
				}
			}
			if (bordertop != null)
			{
				foreach (CT_Border item7 in bordertop)
				{
					item7.Write(sw, "bordertop");
				}
			}
			if (borderbottom != null)
			{
				foreach (CT_Border item8 in borderbottom)
				{
					item8.Write(sw, "borderbottom");
				}
			}
			if (borderleft != null)
			{
				foreach (CT_Border item9 in borderleft)
				{
					item9.Write(sw, "borderleft");
				}
			}
			if (borderright != null)
			{
				foreach (CT_Border item10 in borderright)
				{
					item10.Write(sw, "borderright");
				}
			}
			if (Clientdata != null)
			{
				foreach (CT_ClientData clientdatum in Clientdata)
				{
					clientdatum.Write(sw, "ClientData");
				}
			}
			if (textdata != null)
			{
				foreach (CT_Rel textdatum in textdata)
				{
					textdatum.Write(sw, "textdata");
				}
			}
			sw.Write(string.Format("</v:{0}>", nodeName));
		}

		public CT_Stroke AddNewStroke()
		{
			strokeField = new CT_Stroke();
			return strokeField;
		}

		public CT_Path AddNewPath()
		{
			pathField = new CT_Path();
			return pathField;
		}

		public CT_Formulas AddNewFormulas()
		{
			if (formulasField == null)
			{
				formulasField = new List<CT_Formulas>();
			}
			CT_Formulas cT_Formulas = new CT_Formulas();
			formulasField.Add(cT_Formulas);
			return cT_Formulas;
		}

		public CT_TextPath AddNewTextpath()
		{
			if (textpathField == null)
			{
				textpathField = new List<CT_TextPath>();
			}
			CT_TextPath cT_TextPath = new CT_TextPath();
			textpathField.Add(cT_TextPath);
			return cT_TextPath;
		}

		public CT_Handles AddNewHandles()
		{
			if (handlesField == null)
			{
				handlesField = new List<CT_Handles>();
			}
			CT_Handles cT_Handles = new CT_Handles();
			handlesField.Add(cT_Handles);
			return cT_Handles;
		}

		public CT_Lock AddNewLock()
		{
			return new CT_Lock();
		}
	}
}
