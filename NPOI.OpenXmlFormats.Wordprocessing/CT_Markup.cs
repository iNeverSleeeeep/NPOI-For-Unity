using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlInclude(typeof(CT_Bookmark))]
	[XmlInclude(typeof(CT_TcPrChange))]
	[XmlInclude(typeof(CT_TblPrChange))]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlInclude(typeof(CT_MarkupRange))]
	[XmlInclude(typeof(CT_BookmarkRange))]
	[XmlInclude(typeof(CT_SectPrChange))]
	[XmlInclude(typeof(CT_MoveBookmark))]
	[XmlInclude(typeof(CT_TrackChange))]
	[XmlInclude(typeof(CT_RunTrackChange))]
	[XmlInclude(typeof(CT_RPrChange))]
	[XmlInclude(typeof(CT_ParaRPrChange))]
	[XmlInclude(typeof(CT_PPrChange))]
	[XmlInclude(typeof(CT_TrackChangeNumbering))]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlInclude(typeof(CT_TrPrChange))]
	[XmlInclude(typeof(CT_TblPrExChange))]
	[XmlInclude(typeof(CT_TblGridChange))]
	[XmlInclude(typeof(CT_Comment))]
	[XmlInclude(typeof(CT_TrackChangeRange))]
	[XmlInclude(typeof(CT_CellMergeTrackChange))]
	public class CT_Markup
	{
		private string idField;

		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		public static CT_Markup Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Markup cT_Markup = new CT_Markup();
			cT_Markup.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			return cT_Markup;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:id", id);
			sw.Write("/>");
		}
	}
}
