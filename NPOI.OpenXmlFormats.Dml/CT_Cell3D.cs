using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Cell3D
	{
		private CT_Bevel bevelField;

		private CT_LightRig lightRigField;

		private CT_OfficeArtExtensionList extLstField;

		private ST_PresetMaterialType prstMaterialField;

		[XmlElement(Order = 0)]
		public CT_Bevel bevel
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

		[XmlElement(Order = 1)]
		public CT_LightRig lightRig
		{
			get
			{
				return lightRigField;
			}
			set
			{
				lightRigField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		[DefaultValue(ST_PresetMaterialType.plastic)]
		[XmlAttribute]
		public ST_PresetMaterialType prstMaterial
		{
			get
			{
				return prstMaterialField;
			}
			set
			{
				prstMaterialField = value;
			}
		}

		public CT_Cell3D()
		{
			extLstField = new CT_OfficeArtExtensionList();
			lightRigField = new CT_LightRig();
			bevelField = new CT_Bevel();
			prstMaterialField = ST_PresetMaterialType.plastic;
		}
	}
}
