using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_CellMergeTrackChange : CT_TrackChange
	{
		private ST_AnnotationVMerge vMergeField;

		private bool vMergeFieldSpecified;

		private ST_AnnotationVMerge vMergeOrigField;

		private bool vMergeOrigFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_AnnotationVMerge vMerge
		{
			get
			{
				return vMergeField;
			}
			set
			{
				vMergeField = value;
			}
		}

		[XmlIgnore]
		public bool vMergeSpecified
		{
			get
			{
				return vMergeFieldSpecified;
			}
			set
			{
				vMergeFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_AnnotationVMerge vMergeOrig
		{
			get
			{
				return vMergeOrigField;
			}
			set
			{
				vMergeOrigField = value;
			}
		}

		[XmlIgnore]
		public bool vMergeOrigSpecified
		{
			get
			{
				return vMergeOrigFieldSpecified;
			}
			set
			{
				vMergeOrigFieldSpecified = value;
			}
		}

		public new static CT_CellMergeTrackChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellMergeTrackChange cT_CellMergeTrackChange = new CT_CellMergeTrackChange();
			if (node.Attributes["w:vMerge"] != null)
			{
				cT_CellMergeTrackChange.vMerge = (ST_AnnotationVMerge)Enum.Parse(typeof(ST_AnnotationVMerge), node.Attributes["w:vMerge"].Value);
			}
			if (node.Attributes["w:vMergeOrig"] != null)
			{
				cT_CellMergeTrackChange.vMergeOrig = (ST_AnnotationVMerge)Enum.Parse(typeof(ST_AnnotationVMerge), node.Attributes["w:vMergeOrig"].Value);
			}
			cT_CellMergeTrackChange.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_CellMergeTrackChange.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_CellMergeTrackChange.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			return cT_CellMergeTrackChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:vMerge", vMerge.ToString());
			XmlHelper.WriteAttribute(sw, "w:vMergeOrig", vMergeOrig.ToString());
			XmlHelper.WriteAttribute(sw, "w:author", base.author);
			XmlHelper.WriteAttribute(sw, "w:date", base.date);
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
