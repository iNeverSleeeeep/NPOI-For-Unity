using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_LayoutNode
	{
		private List<object> itemsField;

		private string nameField;

		private string styleLblField;

		private ST_ChildOrderType chOrderField;

		private string moveWithField;

		[XmlElement("ruleLst", typeof(CT_Rules), Order = 0)]
		[XmlElement("constrLst", typeof(CT_Constraints), Order = 0)]
		[XmlElement("shape", typeof(CT_Shape), Order = 0)]
		[XmlElement("presOf", typeof(CT_PresentationOf), Order = 0)]
		[XmlElement("alg", typeof(CT_Algorithm), Order = 0)]
		[XmlElement("choose", typeof(CT_Choose), Order = 0)]
		[XmlElement("varLst", typeof(CT_LayoutVariablePropertySet), Order = 0)]
		[XmlElement("extLst", typeof(CT_OfficeArtExtensionList), Order = 0)]
		[XmlElement("forEach", typeof(CT_ForEach), Order = 0)]
		[XmlElement("layoutNode", typeof(CT_LayoutNode), Order = 0)]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string styleLbl
		{
			get
			{
				return styleLblField;
			}
			set
			{
				styleLblField = value;
			}
		}

		[DefaultValue(ST_ChildOrderType.b)]
		[XmlAttribute]
		public ST_ChildOrderType chOrder
		{
			get
			{
				return chOrderField;
			}
			set
			{
				chOrderField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string moveWith
		{
			get
			{
				return moveWithField;
			}
			set
			{
				moveWithField = value;
			}
		}

		public CT_LayoutNode()
		{
			itemsField = new List<object>();
			nameField = "";
			styleLblField = "";
			chOrderField = ST_ChildOrderType.b;
			moveWithField = "";
		}
	}
}
