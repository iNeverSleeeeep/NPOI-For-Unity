using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Camera
	{
		private CT_SphereCoords rotField;

		private ST_PresetCameraType prstField;

		private int fovField;

		private bool fovFieldSpecified;

		private int zoomField;

		[XmlElement(Order = 0)]
		public CT_SphereCoords rot
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

		[XmlAttribute]
		public ST_PresetCameraType prst
		{
			get
			{
				return prstField;
			}
			set
			{
				prstField = value;
			}
		}

		[XmlAttribute]
		public int fov
		{
			get
			{
				return fovField;
			}
			set
			{
				fovField = value;
			}
		}

		[XmlIgnore]
		public bool fovSpecified
		{
			get
			{
				return fovFieldSpecified;
			}
			set
			{
				fovFieldSpecified = value;
			}
		}

		[DefaultValue(100000)]
		[XmlAttribute]
		public int zoom
		{
			get
			{
				return zoomField;
			}
			set
			{
				zoomField = value;
			}
		}

		public static CT_Camera Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Camera cT_Camera = new CT_Camera();
			if (node.Attributes["prst"] != null)
			{
				cT_Camera.prst = (ST_PresetCameraType)Enum.Parse(typeof(ST_PresetCameraType), node.Attributes["prst"].Value);
			}
			cT_Camera.fov = XmlHelper.ReadInt(node.Attributes["fov"]);
			cT_Camera.zoom = XmlHelper.ReadInt(node.Attributes["zoom"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rot")
				{
					cT_Camera.rot = CT_SphereCoords.Parse(childNode, namespaceManager);
				}
			}
			return cT_Camera;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "prst", prst.ToString());
			XmlHelper.WriteAttribute(sw, "fov", fov);
			XmlHelper.WriteAttribute(sw, "zoom", zoom);
			sw.Write(">");
			if (rot != null)
			{
				rot.Write(sw, "rot");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_Camera()
		{
			rotField = new CT_SphereCoords();
			zoomField = 100000;
		}
	}
}
