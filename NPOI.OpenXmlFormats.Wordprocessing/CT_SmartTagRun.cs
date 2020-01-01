using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SmartTagRun
	{
		private List<CT_Attr> smartTagPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceType25> itemsElementNameField;

		private string uriField;

		private string elementField;

		[XmlArrayItem("attr", IsNullable = false)]
		[XmlArray(Order = 0)]
		public List<CT_Attr> smartTagPr
		{
			get
			{
				return smartTagPrField;
			}
			set
			{
				smartTagPrField = value;
			}
		}

		[XmlElement("permEnd", typeof(CT_Perm), Order = 1)]
		[XmlElement("hyperlink", typeof(CT_Hyperlink1), Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlRun), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("fldSimple", typeof(CT_SimpleField), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 1)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 1)]
		[XmlElement("r", typeof(CT_R), Order = 1)]
		[XmlElement("sdt", typeof(CT_SdtRun), Order = 1)]
		[XmlElement("smartTag", typeof(CT_SmartTagRun), Order = 1)]
		[XmlElement("subDoc", typeof(CT_Rel), Order = 1)]
		public ArrayList Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[XmlIgnore]
		[XmlElement("ItemsElementName", Order = 2)]
		public List<ItemsChoiceType25> ItemsElementName
		{
			get
			{
				return itemsElementNameField;
			}
			set
			{
				itemsElementNameField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string uri
		{
			get
			{
				return uriField;
			}
			set
			{
				uriField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string element
		{
			get
			{
				return elementField;
			}
			set
			{
				elementField = value;
			}
		}

		public CT_SmartTagRun()
		{
			itemsElementNameField = new List<ItemsChoiceType25>();
			itemsField = new ArrayList();
		}

		public static CT_SmartTagRun Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SmartTagRun cT_SmartTagRun = new CT_SmartTagRun();
			cT_SmartTagRun.uri = XmlHelper.ReadString(node.Attributes["w:uri"]);
			cT_SmartTagRun.element = XmlHelper.ReadString(node.Attributes["w:element"]);
			cT_SmartTagRun.smartTagPr = new List<CT_Attr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_SmartTagRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "smartTagPr")
				{
					cT_SmartTagRun.smartTagPr.Add(CT_Attr.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "fldSimple")
				{
					cT_SmartTagRun.Items.Add(CT_SimpleField.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.fldSimple);
				}
				else if (childNode.LocalName == "hyperlink")
				{
					cT_SmartTagRun.Items.Add(CT_Hyperlink1.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.hyperlink);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_SmartTagRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_SmartTagRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_SmartTagRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_SmartTagRun.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_SmartTagRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_SmartTagRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_SmartTagRun.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_SmartTagRun.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_SmartTagRun.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_SmartTagRun.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.proofErr);
				}
				else if (childNode.LocalName == "r")
				{
					cT_SmartTagRun.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.r);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_SmartTagRun.Items.Add(CT_SdtRun.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.sdt);
				}
				else if (childNode.LocalName == "smartTag")
				{
					cT_SmartTagRun.Items.Add(Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.smartTag);
				}
				else if (childNode.LocalName == "subDoc")
				{
					cT_SmartTagRun.Items.Add(CT_Rel.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.subDoc);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_SmartTagRun.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.oMath);
				}
				else if (childNode.LocalName == "del")
				{
					cT_SmartTagRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.del);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_SmartTagRun.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.oMathPara);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_SmartTagRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_SmartTagRun.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_SmartTagRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_SmartTagRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_SmartTagRun.Items.Add(CT_CustomXmlRun.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_SmartTagRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_SmartTagRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_SmartTagRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_SmartTagRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_SmartTagRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_SmartTagRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_SmartTagRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_SmartTagRun.ItemsElementName.Add(ItemsChoiceType25.customXmlMoveToRangeEnd);
				}
			}
			return cT_SmartTagRun;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:uri", uri);
			XmlHelper.WriteAttribute(sw, "w:element", element);
			sw.Write(">");
			if (smartTagPr != null)
			{
				foreach (CT_Attr item in smartTagPr)
				{
					item.Write(sw, "smartTagPr");
				}
			}
			foreach (object item2 in Items)
			{
				if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item2 is CT_SimpleField)
				{
					((CT_SimpleField)item2).Write(sw, "fldSimple");
				}
				else if (item2 is CT_Hyperlink1)
				{
					((CT_Hyperlink1)item2).Write(sw, "hyperlink");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "ins");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "moveFrom");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "moveFromRangeEnd");
				}
				else if (item2 is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item2).Write(sw, "moveFromRangeStart");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "moveTo");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "moveToRangeEnd");
				}
				else if (item2 is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item2).Write(sw, "moveToRangeStart");
				}
				else if (item2 is CT_Perm)
				{
					((CT_Perm)item2).Write(sw, "permEnd");
				}
				else if (item2 is CT_PermStart)
				{
					((CT_PermStart)item2).Write(sw, "permStart");
				}
				else if (item2 is CT_ProofErr)
				{
					((CT_ProofErr)item2).Write(sw, "proofErr");
				}
				else if (item2 is CT_R)
				{
					((CT_R)item2).Write(sw, "r");
				}
				else if (item2 is CT_SdtRun)
				{
					((CT_SdtRun)item2).Write(sw, "sdt");
				}
				else if (item2 is CT_SmartTagRun)
				{
					((CT_SmartTagRun)item2).Write(sw, "smartTag");
				}
				else if (item2 is CT_Rel)
				{
					((CT_Rel)item2).Write(sw, "subDoc");
				}
				else if (item2 is CT_OMath)
				{
					((CT_OMath)item2).Write(sw, "oMath");
				}
				else if (item2 is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item2).Write(sw, "del");
				}
				else if (item2 is CT_OMathPara)
				{
					((CT_OMathPara)item2).Write(sw, "oMathPara");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "bookmarkEnd");
				}
				else if (item2 is CT_Bookmark)
				{
					((CT_Bookmark)item2).Write(sw, "bookmarkStart");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "commentRangeEnd");
				}
				else if (item2 is CT_MarkupRange)
				{
					((CT_MarkupRange)item2).Write(sw, "commentRangeStart");
				}
				else if (item2 is CT_CustomXmlRun)
				{
					((CT_CustomXmlRun)item2).Write(sw, "customXml");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlDelRangeStart");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlInsRangeStart");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item2 is CT_TrackChange)
				{
					((CT_TrackChange)item2).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item2 is CT_Markup)
				{
					((CT_Markup)item2).Write(sw, "customXmlMoveToRangeEnd");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
