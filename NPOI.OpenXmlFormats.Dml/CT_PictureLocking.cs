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
	public class CT_PictureLocking
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

		private bool noCropField;

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

		[XmlAttribute]
		[DefaultValue(false)]
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

		[XmlAttribute]
		[DefaultValue(false)]
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

		[XmlAttribute]
		[DefaultValue(false)]
		public bool noCrop
		{
			get
			{
				return noCropField;
			}
			set
			{
				noCropField = value;
			}
		}

		public CT_PictureLocking()
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
			noCropField = false;
		}

		public static CT_PictureLocking Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PictureLocking cT_PictureLocking = new CT_PictureLocking();
			cT_PictureLocking.noGrp = XmlHelper.ReadBool(node.Attributes["noGrp"]);
			cT_PictureLocking.noSelect = XmlHelper.ReadBool(node.Attributes["noSelect"]);
			cT_PictureLocking.noRot = XmlHelper.ReadBool(node.Attributes["noRot"]);
			cT_PictureLocking.noChangeAspect = XmlHelper.ReadBool(node.Attributes["noChangeAspect"]);
			cT_PictureLocking.noMove = XmlHelper.ReadBool(node.Attributes["noMove"]);
			cT_PictureLocking.noResize = XmlHelper.ReadBool(node.Attributes["noResize"]);
			cT_PictureLocking.noEditPoints = XmlHelper.ReadBool(node.Attributes["noEditPoints"]);
			cT_PictureLocking.noAdjustHandles = XmlHelper.ReadBool(node.Attributes["noAdjustHandles"]);
			cT_PictureLocking.noChangeArrowheads = XmlHelper.ReadBool(node.Attributes["noChangeArrowheads"]);
			cT_PictureLocking.noChangeShapeType = XmlHelper.ReadBool(node.Attributes["noChangeShapeType"]);
			cT_PictureLocking.noCrop = XmlHelper.ReadBool(node.Attributes["noCrop"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_PictureLocking.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_PictureLocking;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			if (noGrp)
			{
				XmlHelper.WriteAttribute(sw, "noGrp", noGrp);
			}
			if (noSelect)
			{
				XmlHelper.WriteAttribute(sw, "noSelect", noSelect);
			}
			if (noRot)
			{
				XmlHelper.WriteAttribute(sw, "noRot", noRot);
			}
			if (noChangeAspect)
			{
				XmlHelper.WriteAttribute(sw, "noChangeAspect", noChangeAspect);
			}
			if (noMove)
			{
				XmlHelper.WriteAttribute(sw, "noMove", noMove);
			}
			if (noResize)
			{
				XmlHelper.WriteAttribute(sw, "noResize", noResize);
			}
			if (noEditPoints)
			{
				XmlHelper.WriteAttribute(sw, "noEditPoints", noEditPoints);
			}
			if (noAdjustHandles)
			{
				XmlHelper.WriteAttribute(sw, "noAdjustHandles", noAdjustHandles);
			}
			if (noChangeArrowheads)
			{
				XmlHelper.WriteAttribute(sw, "noChangeArrowheads", noChangeArrowheads);
			}
			if (noChangeShapeType)
			{
				XmlHelper.WriteAttribute(sw, "noChangeShapeType", noChangeShapeType);
			}
			if (noCrop)
			{
				XmlHelper.WriteAttribute(sw, "noCrop", noCrop);
			}
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
