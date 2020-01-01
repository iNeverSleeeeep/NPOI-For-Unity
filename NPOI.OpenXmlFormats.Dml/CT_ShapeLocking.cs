using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_ShapeLocking
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

		private bool noTextEditField;

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

		[XmlAttribute]
		[DefaultValue(false)]
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

		[DefaultValue(false)]
		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(false)]
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

		[DefaultValue(false)]
		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(false)]
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool noTextEdit
		{
			get
			{
				return noTextEditField;
			}
			set
			{
				noTextEditField = value;
			}
		}

		public static CT_ShapeLocking Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ShapeLocking cT_ShapeLocking = new CT_ShapeLocking();
			cT_ShapeLocking.noGrp = XmlHelper.ReadBool(node.Attributes["noGrp"]);
			cT_ShapeLocking.noSelect = XmlHelper.ReadBool(node.Attributes["noSelect"]);
			cT_ShapeLocking.noRot = XmlHelper.ReadBool(node.Attributes["noRot"]);
			cT_ShapeLocking.noChangeAspect = XmlHelper.ReadBool(node.Attributes["noChangeAspect"]);
			cT_ShapeLocking.noMove = XmlHelper.ReadBool(node.Attributes["noMove"]);
			cT_ShapeLocking.noResize = XmlHelper.ReadBool(node.Attributes["noResize"]);
			cT_ShapeLocking.noEditPoints = XmlHelper.ReadBool(node.Attributes["noEditPoints"]);
			cT_ShapeLocking.noAdjustHandles = XmlHelper.ReadBool(node.Attributes["noAdjustHandles"]);
			cT_ShapeLocking.noChangeArrowheads = XmlHelper.ReadBool(node.Attributes["noChangeArrowheads"]);
			cT_ShapeLocking.noChangeShapeType = XmlHelper.ReadBool(node.Attributes["noChangeShapeType"]);
			cT_ShapeLocking.noTextEdit = XmlHelper.ReadBool(node.Attributes["noTextEdit"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_ShapeLocking.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_ShapeLocking;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "noGrp", noGrp, false);
			XmlHelper.WriteAttribute(sw, "noSelect", noSelect, false);
			XmlHelper.WriteAttribute(sw, "noRot", noRot, false);
			XmlHelper.WriteAttribute(sw, "noChangeAspect", noChangeAspect, false);
			XmlHelper.WriteAttribute(sw, "noMove", noMove, false);
			XmlHelper.WriteAttribute(sw, "noResize", noResize, false);
			XmlHelper.WriteAttribute(sw, "noEditPoints", noEditPoints, false);
			XmlHelper.WriteAttribute(sw, "noAdjustHandles", noAdjustHandles, false);
			XmlHelper.WriteAttribute(sw, "noChangeArrowheads", noChangeArrowheads, false);
			XmlHelper.WriteAttribute(sw, "noChangeShapeType", noChangeShapeType, false);
			XmlHelper.WriteAttribute(sw, "noTextEdit", noTextEdit, false);
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_ShapeLocking()
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
			noTextEditField = false;
		}
	}
}
