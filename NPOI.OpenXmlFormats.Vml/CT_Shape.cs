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
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[XmlRoot("shape", Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	public class CT_Shape
	{
		private string typeField;

		private string adjField;

		private string styleField;

		private CT_Path pathField;

		private string equationxmlField;

		private string idField;

		private string fillcolorField;

		private ST_InsetMode insetmodeField;

		private ST_TrueFalse strokedField;

		private string wrapcoordsField;

		private string spidField;

		private CT_Wrap wrapField;

		private CT_Fill fillField;

		private CT_Formulas formulasField;

		private CT_Handles handlesField;

		private CT_ImageData imagedataField;

		private CT_Stroke strokeField;

		private CT_Shadow shadowField;

		private CT_Textbox textboxField;

		private CT_TextPath textpathField;

		private CT_Empty iscommentField;

		private CT_Lock lockField;

		private CT_Border bordertopField;

		private CT_Border borderrightField;

		private CT_Border borderleftField;

		private CT_Border borderbottomField;

		private CT_AnchorLock anchorlockField;

		private CT_Rel textdataField;

		private List<CT_ClientData> clientDataField;

		[XmlAttribute]
		public string wrapcoords
		{
			get
			{
				return wrapcoordsField;
			}
			set
			{
				wrapcoordsField = value;
			}
		}

		[DefaultValue(ST_TrueFalse.t)]
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

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		public string spid
		{
			get
			{
				return spidField;
			}
			set
			{
				spidField = value;
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

		[XmlAttribute]
		public string fillcolor
		{
			get
			{
				return fillcolorField;
			}
			set
			{
				fillcolorField = value;
			}
		}

		[XmlAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
		[DefaultValue(ST_InsetMode.auto)]
		public ST_InsetMode insetmode
		{
			get
			{
				return insetmodeField;
			}
			set
			{
				insetmodeField = value;
			}
		}

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
		public CT_Rel textdata
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

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:word")]
		public CT_AnchorLock anchorlock
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

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:word")]
		public CT_Border borderright
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

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:word")]
		public CT_Border borderleft
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

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:word")]
		public CT_Border borderbottom
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

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:word")]
		public CT_Border bordertop
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

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
		public CT_Empty iscomment
		{
			get
			{
				return iscommentField;
			}
			set
			{
				iscommentField = value;
			}
		}

		[XmlElement]
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

		[XmlElement(Namespace = "urn:schemas-microsoft-com:office:word")]
		public CT_Wrap wrap
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

		[XmlElement]
		public CT_Textbox textbox
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

		[XmlElement]
		public CT_Fill fill
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

		[XmlElement]
		public CT_Formulas formulas
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

		[XmlElement]
		public CT_Handles handles
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

		[XmlElement]
		public CT_ImageData imagedata
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

		[XmlElement(ElementName = "lock", Namespace = "urn:schemas-microsoft-com:office:office")]
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

		[XmlElement]
		public CT_Shadow shadow
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

		[XmlElement("ClientData", Namespace = "urn:schemas-microsoft-com:office:excel")]
		public List<CT_ClientData> ClientData
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

		[XmlAttribute]
		public string type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
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

		[XmlElement]
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

		[XmlAttribute]
		public string equationxml
		{
			get
			{
				return equationxmlField;
			}
			set
			{
				equationxmlField = value;
			}
		}

		[XmlAttribute]
		public string style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlElement]
		public CT_TextPath textpath
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

		public static CT_Shape Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shape cT_Shape = new CT_Shape();
			cT_Shape.wrapcoords = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["wrapcoords"]);
			if (node.Attributes["stroked"] != null)
			{
				cT_Shape.stroked = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalse2(node.Attributes["stroked"]);
			}
			cT_Shape.spid = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["o:spid"]);
			cT_Shape.id = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["id"]);
			cT_Shape.fillcolor = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["fillcolor"]);
			if (node.Attributes["o:insetmode"] != null)
			{
				cT_Shape.insetmode = (ST_InsetMode)Enum.Parse(typeof(ST_InsetMode), node.Attributes["o:insetmode"].Value);
			}
			cT_Shape.type = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["type"]);
			cT_Shape.adj = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["adj"]);
			cT_Shape.equationxml = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["equationxml"]);
			cT_Shape.style = NPOI.OpenXml4Net.Util.XmlHelper.ReadString(node.Attributes["style"]);
			cT_Shape.ClientData = new List<CT_ClientData>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "textdata")
				{
					cT_Shape.textdata = CT_Rel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "anchorlock")
				{
					cT_Shape.anchorlock = new CT_AnchorLock();
				}
				else if (childNode.LocalName == "borderright")
				{
					cT_Shape.borderright = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "borderleft")
				{
					cT_Shape.borderleft = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "borderbottom")
				{
					cT_Shape.borderbottom = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bordertop")
				{
					cT_Shape.bordertop = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "iscomment")
				{
					cT_Shape.iscomment = new CT_Empty();
				}
				else if (childNode.LocalName == "stroke")
				{
					cT_Shape.stroke = CT_Stroke.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wrap")
				{
					cT_Shape.wrap = CT_Wrap.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textbox")
				{
					cT_Shape.textbox = CT_Textbox.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fill")
				{
					cT_Shape.fill = CT_Fill.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "formulas")
				{
					cT_Shape.formulas = CT_Formulas.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "handles")
				{
					cT_Shape.handles = CT_Handles.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "imagedata")
				{
					cT_Shape.imagedata = CT_ImageData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lock")
				{
					cT_Shape.@lock = CT_Lock.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shadow")
				{
					cT_Shape.shadow = CT_Shadow.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "path")
				{
					cT_Shape.path = CT_Path.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textpath")
				{
					cT_Shape.textpath = CT_TextPath.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ClientData")
				{
					cT_Shape.ClientData.Add(CT_ClientData.Parse(childNode, namespaceManager));
				}
			}
			return cT_Shape;
		}

		public void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<v:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "wrapcoords", wrapcoords);
			NPOI.OpenXmlFormats.Util.XmlHelper.WriteAttribute(sw, "stroked", stroked);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:spid", spid);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "id", id);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "fillcolor", fillcolor);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "o:insetmode", insetmode.ToString());
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "type", type);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "adj", adj);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "equationxml", equationxml);
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "style", style);
			sw.Write(">");
			if (iscomment != null)
			{
				sw.Write("<iscomment/>");
			}
			if (stroke != null)
			{
				stroke.Write(sw, "stroke");
			}
			if (wrap != null)
			{
				wrap.Write(sw, "wrap");
			}
			if (fill != null)
			{
				fill.Write(sw, "fill");
			}
			if (formulas != null)
			{
				formulas.Write(sw, "formulas");
			}
			if (handles != null)
			{
				handles.Write(sw, "handles");
			}
			if (imagedata != null)
			{
				imagedata.Write(sw, "imagedata");
			}
			if (@lock != null)
			{
				@lock.Write(sw, "lock");
			}
			if (shadow != null)
			{
				shadow.Write(sw, "shadow");
			}
			if (path != null)
			{
				path.Write(sw, "path");
			}
			if (textpath != null)
			{
				textpath.Write(sw, "textpath");
			}
			if (textbox != null)
			{
				textbox.Write(sw, "textbox");
			}
			if (textdata != null)
			{
				textdata.Write(sw, "textdata");
			}
			if (anchorlock != null)
			{
				sw.Write("<w:anchorlock/>");
			}
			if (borderright != null)
			{
				borderright.Write(sw, "borderright");
			}
			if (borderleft != null)
			{
				borderleft.Write(sw, "borderleft");
			}
			if (borderbottom != null)
			{
				borderbottom.Write(sw, "borderbottom");
			}
			if (bordertop != null)
			{
				bordertop.Write(sw, "bordertop");
			}
			if (ClientData != null)
			{
				foreach (CT_ClientData clientDatum in ClientData)
				{
					clientDatum.Write(sw, "ClientData");
				}
			}
			sw.Write(string.Format("</v:{0}>", nodeName));
		}

		public CT_Textbox AddNewTextbox()
		{
			textboxField = new CT_Textbox();
			return textboxField;
		}

		public CT_Fill AddNewFill()
		{
			fillField = new CT_Fill();
			return fillField;
		}

		public CT_Shadow AddNewShadow()
		{
			shadowField = new CT_Shadow();
			return shadowField;
		}

		public CT_Path AddNewPath()
		{
			pathField = new CT_Path();
			return pathField;
		}

		public CT_ClientData GetClientDataArray(int index)
		{
			if (clientDataField == null)
			{
				return null;
			}
			return clientDataField[index];
		}

		public int sizeOfClientDataArray()
		{
			if (clientDataField == null)
			{
				return 0;
			}
			return clientDataField.Count;
		}

		public CT_ClientData AddNewClientData()
		{
			CT_ClientData cT_ClientData = new CT_ClientData();
			if (clientDataField == null)
			{
				clientDataField = new List<CT_ClientData>();
			}
			clientDataField.Add(cT_ClientData);
			return cT_ClientData;
		}

		public CT_TextPath AddNewTextpath()
		{
			textpathField = new CT_TextPath();
			return textpathField;
		}
	}
}
