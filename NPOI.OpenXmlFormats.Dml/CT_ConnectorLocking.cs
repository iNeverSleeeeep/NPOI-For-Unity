using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_ConnectorLocking
	{
		private CT_OfficeArtExtensionList extLstField;

		private bool noGrpField;

		private bool noSelectField;

		private bool noRotField;

		private bool noChangeAspectField;

		private bool noMoveField;

		private bool noResizeField;

		private bool noEditPointsField;

		private bool noAdjustHandlesField;

		private bool noChangeArrowheadsField;

		private bool noChangeShapeTypeField;

		[XmlElement(Order = 0)]
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
		[DefaultValue(false)]
		public bool noGrp
		{
			get
			{
				return noGrpField;
			}
			set
			{
				noGrpField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool noSelect
		{
			get
			{
				return noSelectField;
			}
			set
			{
				noSelectField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool noRot
		{
			get
			{
				return noRotField;
			}
			set
			{
				noRotField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool noChangeAspect
		{
			get
			{
				return noChangeAspectField;
			}
			set
			{
				noChangeAspectField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool noMove
		{
			get
			{
				return noMoveField;
			}
			set
			{
				noMoveField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool noResize
		{
			get
			{
				return noResizeField;
			}
			set
			{
				noResizeField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool noEditPoints
		{
			get
			{
				return noEditPointsField;
			}
			set
			{
				noEditPointsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool noAdjustHandles
		{
			get
			{
				return noAdjustHandlesField;
			}
			set
			{
				noAdjustHandlesField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool noChangeArrowheads
		{
			get
			{
				return noChangeArrowheadsField;
			}
			set
			{
				noChangeArrowheadsField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool noChangeShapeType
		{
			get
			{
				return noChangeShapeTypeField;
			}
			set
			{
				noChangeShapeTypeField = value;
			}
		}

		public static CT_ConnectorLocking Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ConnectorLocking cT_ConnectorLocking = new CT_ConnectorLocking();
			cT_ConnectorLocking.noGrp = XmlHelper.ReadBool(node.Attributes["noGrp"]);
			cT_ConnectorLocking.noSelect = XmlHelper.ReadBool(node.Attributes["noSelect"]);
			cT_ConnectorLocking.noRot = XmlHelper.ReadBool(node.Attributes["noRot"]);
			cT_ConnectorLocking.noChangeAspect = XmlHelper.ReadBool(node.Attributes["noChangeAspect"]);
			cT_ConnectorLocking.noMove = XmlHelper.ReadBool(node.Attributes["noMove"]);
			cT_ConnectorLocking.noResize = XmlHelper.ReadBool(node.Attributes["noResize"]);
			cT_ConnectorLocking.noEditPoints = XmlHelper.ReadBool(node.Attributes["noEditPoints"]);
			cT_ConnectorLocking.noAdjustHandles = XmlHelper.ReadBool(node.Attributes["noAdjustHandles"]);
			cT_ConnectorLocking.noChangeArrowheads = XmlHelper.ReadBool(node.Attributes["noChangeArrowheads"]);
			cT_ConnectorLocking.noChangeShapeType = XmlHelper.ReadBool(node.Attributes["noChangeShapeType"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_ConnectorLocking.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_ConnectorLocking;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "noGrp", noGrp);
			XmlHelper.WriteAttribute(sw, "noSelect", noSelect);
			XmlHelper.WriteAttribute(sw, "noRot", noRot);
			XmlHelper.WriteAttribute(sw, "noChangeAspect", noChangeAspect);
			XmlHelper.WriteAttribute(sw, "noMove", noMove);
			XmlHelper.WriteAttribute(sw, "noResize", noResize);
			XmlHelper.WriteAttribute(sw, "noEditPoints", noEditPoints);
			XmlHelper.WriteAttribute(sw, "noAdjustHandles", noAdjustHandles);
			XmlHelper.WriteAttribute(sw, "noChangeArrowheads", noChangeArrowheads);
			XmlHelper.WriteAttribute(sw, "noChangeShapeType", noChangeShapeType);
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_ConnectorLocking()
		{
			noGrpField = false;
			noSelectField = false;
			noRotField = false;
			noChangeAspectField = false;
			noMoveField = false;
			noResizeField = false;
			noEditPointsField = false;
			noAdjustHandlesField = false;
			noChangeArrowheadsField = false;
			noChangeShapeTypeField = false;
		}
	}
}
