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
	public class CT_CustomXmlRun
	{
		private CT_CustomXmlPr customXmlPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceType24> itemsElementNameField;

		private string uriField;

		private string elementField;

		[XmlElement(Order = 0)]
		public CT_CustomXmlPr customXmlPr
		{
			get
			{
				return customXmlPrField;
			}
			set
			{
				customXmlPrField = value;
			}
		}

		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("smartTag", typeof(CT_SmartTagRun), Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("subDoc", typeof(CT_Rel), Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 1)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("customXml", typeof(CT_CustomXmlRun), Order = 1)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("sdt", typeof(CT_SdtRun), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("fldSimple", typeof(CT_SimpleField), Order = 1)]
		[XmlElement("hyperlink", typeof(CT_Hyperlink1), Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 1)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 1)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 1)]
		[XmlElement("r", typeof(CT_R), Order = 1)]
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
		public List<ItemsChoiceType24> ItemsElementName
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

		public CT_CustomXmlRun()
		{
			itemsElementNameField = new List<ItemsChoiceType24>();
			itemsField = new ArrayList();
		}

		public static CT_CustomXmlRun Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomXmlRun cT_CustomXmlRun = new CT_CustomXmlRun();
			cT_CustomXmlRun.uri = XmlHelper.ReadString(node.Attributes["w:uri"]);
			cT_CustomXmlRun.element = XmlHelper.ReadString(node.Attributes["w:element"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "bookmarkStart")
				{
					cT_CustomXmlRun.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.bookmarkStart);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_CustomXmlRun.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_CustomXmlRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.moveTo);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_CustomXmlRun.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.oMath);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_CustomXmlRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_CustomXmlRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_CustomXmlRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_CustomXmlRun.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_CustomXmlRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.del);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.commentRangeEnd);
				}
				else if (childNode.LocalName == "fldSimple")
				{
					cT_CustomXmlRun.Items.Add(CT_SimpleField.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.fldSimple);
				}
				else if (childNode.LocalName == "hyperlink")
				{
					cT_CustomXmlRun.Items.Add(CT_Hyperlink1.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.hyperlink);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_CustomXmlRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_CustomXmlRun.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_CustomXmlRun.Items.Add(Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.customXml);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_CustomXmlRun.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_CustomXmlRun.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.permStart);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_CustomXmlRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.bookmarkEnd);
				}
				else if (childNode.LocalName == "r")
				{
					cT_CustomXmlRun.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.r);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_CustomXmlRun.Items.Add(CT_SdtRun.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.sdt);
				}
				else if (childNode.LocalName == "smartTag")
				{
					cT_CustomXmlRun.Items.Add(CT_SmartTagRun.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.smartTag);
				}
				else if (childNode.LocalName == "subDoc")
				{
					cT_CustomXmlRun.Items.Add(CT_Rel.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.subDoc);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_CustomXmlRun.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.proofErr);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_CustomXmlRun.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.commentRangeStart);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_CustomXmlRun.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_CustomXmlRun.ItemsElementName.Add(ItemsChoiceType24.oMathPara);
				}
			}
			return cT_CustomXmlRun;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:uri", uri);
			XmlHelper.WriteAttribute(sw, "w:element", element);
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlDelRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlInsRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_SimpleField)
				{
					((CT_SimpleField)item).Write(sw, "fldSimple");
				}
				else if (item is CT_Hyperlink1)
				{
					((CT_Hyperlink1)item).Write(sw, "hyperlink");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveFrom");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveFromRangeEnd");
				}
				else if (item is CT_CustomXmlRun)
				{
					((CT_CustomXmlRun)item).Write(sw, "customXml");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "moveToRangeEnd");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveToRangeStart");
				}
				else if (item is CT_Perm)
				{
					((CT_Perm)item).Write(sw, "permEnd");
				}
				else if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_R)
				{
					((CT_R)item).Write(sw, "r");
				}
				else if (item is CT_SdtRun)
				{
					((CT_SdtRun)item).Write(sw, "sdt");
				}
				else if (item is CT_SmartTagRun)
				{
					((CT_SmartTagRun)item).Write(sw, "smartTag");
				}
				else if (item is CT_Rel)
				{
					((CT_Rel)item).Write(sw, "subDoc");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
