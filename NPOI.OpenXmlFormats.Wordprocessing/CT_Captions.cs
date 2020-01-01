using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Captions
	{
		private List<CT_Caption> captionField;

		private List<CT_AutoCaption> autoCaptionsField;

		[XmlElement("caption", Order = 0)]
		public List<CT_Caption> caption
		{
			get
			{
				return captionField;
			}
			set
			{
				captionField = value;
			}
		}

		[XmlArrayItem("autoCaption", IsNullable = false)]
		[XmlArray(Order = 1)]
		public List<CT_AutoCaption> autoCaptions
		{
			get
			{
				return autoCaptionsField;
			}
			set
			{
				autoCaptionsField = value;
			}
		}

		public static CT_Captions Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Captions cT_Captions = new CT_Captions();
			cT_Captions.caption = new List<CT_Caption>();
			cT_Captions.autoCaptions = new List<CT_AutoCaption>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "caption")
				{
					cT_Captions.caption.Add(CT_Caption.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "autoCaptions")
				{
					cT_Captions.autoCaptions.Add(CT_AutoCaption.Parse(childNode, namespaceManager));
				}
			}
			return cT_Captions;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (caption != null)
			{
				foreach (CT_Caption item in caption)
				{
					item.Write(sw, "caption");
				}
			}
			if (autoCaptions != null)
			{
				foreach (CT_AutoCaption autoCaption in autoCaptions)
				{
					autoCaption.Write(sw, "autoCaptions");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
