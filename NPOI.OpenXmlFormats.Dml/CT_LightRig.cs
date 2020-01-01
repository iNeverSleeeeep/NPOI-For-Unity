using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_LightRig
	{
		private CT_SphereCoords rotField;

		private ST_LightRigType rigField;

		private ST_LightRigDirection dirField;

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
		public ST_LightRigType rig
		{
			get
			{
				return rigField;
			}
			set
			{
				rigField = value;
			}
		}

		[XmlAttribute]
		public ST_LightRigDirection dir
		{
			get
			{
				return dirField;
			}
			set
			{
				dirField = value;
			}
		}

		public static CT_LightRig Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LightRig cT_LightRig = new CT_LightRig();
			if (node.Attributes["rig"] != null)
			{
				cT_LightRig.rig = (ST_LightRigType)Enum.Parse(typeof(ST_LightRigType), node.Attributes["rig"].Value);
			}
			if (node.Attributes["dir"] != null)
			{
				cT_LightRig.dir = (ST_LightRigDirection)Enum.Parse(typeof(ST_LightRigDirection), node.Attributes["dir"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rot")
				{
					cT_LightRig.rot = CT_SphereCoords.Parse(childNode, namespaceManager);
				}
			}
			return cT_LightRig;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "rig", rig.ToString());
			XmlHelper.WriteAttribute(sw, "dir", dir.ToString());
			sw.Write(">");
			if (rot != null)
			{
				rot.Write(sw, "rot");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
