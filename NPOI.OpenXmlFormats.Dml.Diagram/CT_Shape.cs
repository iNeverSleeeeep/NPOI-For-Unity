using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_Shape
	{
		private List<CT_Adj> adjLstField;

		private CT_OfficeArtExtensionList extLstField;

		private double rotField;

		private string typeField;

		private string blipField;

		private int zOrderOffField;

		private bool hideGeomField;

		private bool lkTxEntryField;

		private bool blipPhldrField;

		[XmlArrayItem("adj", IsNullable = false)]
		[XmlArray(Order = 0)]
		public List<CT_Adj> adjLst
		{
			get
			{
				return adjLstField;
			}
			set
			{
				adjLstField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[DefaultValue(0.0)]
		[XmlAttribute]
		public double rot
		{
			get
			{
				return rotField;
			}
			set
			{
				rotField = value;
			}
		}

		[DefaultValue("none")]
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		[DefaultValue("")]
		public string blip
		{
			get
			{
				return blipField;
			}
			set
			{
				blipField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int zOrderOff
		{
			get
			{
				return zOrderOffField;
			}
			set
			{
				zOrderOffField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool hideGeom
		{
			get
			{
				return hideGeomField;
			}
			set
			{
				hideGeomField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool lkTxEntry
		{
			get
			{
				return lkTxEntryField;
			}
			set
			{
				lkTxEntryField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool blipPhldr
		{
			get
			{
				return blipPhldrField;
			}
			set
			{
				blipPhldrField = value;
			}
		}

		public CT_Shape()
		{
			adjLstField = new List<CT_Adj>();
			rotField = 0.0;
			typeField = "none";
			blipField = "";
			zOrderOffField = 0;
			hideGeomField = false;
			lkTxEntryField = false;
			blipPhldrField = false;
		}
	}
}
