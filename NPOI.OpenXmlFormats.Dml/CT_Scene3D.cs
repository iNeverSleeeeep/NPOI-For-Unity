using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_Scene3D
	{
		private CT_Camera cameraField;

		private CT_LightRig lightRigField;

		private CT_Backdrop backdropField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_Camera camera
		{
			get
			{
				return cameraField;
			}
			set
			{
				cameraField = value;
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
		public CT_Backdrop backdrop
		{
			get
			{
				return backdropField;
			}
			set
			{
				backdropField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_Scene3D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Scene3D cT_Scene3D = new CT_Scene3D();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "camera")
				{
					cT_Scene3D.camera = CT_Camera.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lightRig")
				{
					cT_Scene3D.lightRig = CT_LightRig.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "backdrop")
				{
					cT_Scene3D.backdrop = CT_Backdrop.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Scene3D.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Scene3D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (camera != null)
			{
				camera.Write(sw, "camera");
			}
			if (lightRig != null)
			{
				lightRig.Write(sw, "lightRig");
			}
			if (backdrop != null)
			{
				backdrop.Write(sw, "backdrop");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
