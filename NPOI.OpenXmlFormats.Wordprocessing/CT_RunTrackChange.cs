using NPOI.OpenXmlFormats.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_RunTrackChange : CT_TrackChange
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType6> itemsElementNameField;

		[XmlElement("bar", typeof(CT_Bar), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("sSubSup", typeof(CT_SSubSup), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("acc", typeof(CT_Acc), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("borderBox", typeof(CT_BorderBox), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("box", typeof(CT_Box), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("d", typeof(CT_D), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("eqArr", typeof(CT_EqArr), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("f", typeof(CT_F), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("func", typeof(CT_Func), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("groupChr", typeof(CT_GroupChr), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("limLow", typeof(CT_LimLow), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("limUpp", typeof(CT_LimUpp), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("m", typeof(CT_M), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("nary", typeof(CT_Nary), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("phant", typeof(CT_Phant), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("r", typeof(NPOI.OpenXmlFormats.Shared.CT_R), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("rad", typeof(CT_Rad), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("sPre", typeof(CT_SPre), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("sSub", typeof(CT_SSub), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("sSup", typeof(CT_SSup), Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("customXml", typeof(CT_CustomXmlRun), Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Order = 0)]
		[XmlElement("r", typeof(CT_R), Order = 0)]
		[XmlElement("sdt", typeof(CT_SdtRun), Order = 0)]
		[XmlElement("smartTag", typeof(CT_SmartTagRun), Order = 0)]
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
		[XmlElement("ItemsElementName", Order = 1)]
		public List<ItemsChoiceType6> ItemsElementName
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

		public CT_RunTrackChange()
		{
			itemsElementNameField = new List<ItemsChoiceType6>();
			itemsField = new ArrayList();
		}

		public new static CT_RunTrackChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RunTrackChange cT_RunTrackChange = new CT_RunTrackChange();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "func")
				{
					cT_RunTrackChange.Items.Add(CT_Func.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.func);
				}
				else if (childNode.LocalName == "sSubSup")
				{
					cT_RunTrackChange.Items.Add(CT_SSubSup.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.sSubSup);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_RunTrackChange.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "acc")
				{
					cT_RunTrackChange.Items.Add(CT_Acc.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.acc);
				}
				else if (childNode.LocalName == "bar")
				{
					cT_RunTrackChange.Items.Add(CT_Bar.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.bar);
				}
				else if (childNode.LocalName == "borderBox")
				{
					cT_RunTrackChange.Items.Add(CT_BorderBox.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.borderBox);
				}
				else if (childNode.LocalName == "box")
				{
					cT_RunTrackChange.Items.Add(CT_Box.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.box);
				}
				else if (childNode.LocalName == "d")
				{
					cT_RunTrackChange.Items.Add(CT_D.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.d);
				}
				else if (childNode.LocalName == "eqArr")
				{
					cT_RunTrackChange.Items.Add(CT_EqArr.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.eqArr);
				}
				else if (childNode.LocalName == "f")
				{
					cT_RunTrackChange.Items.Add(CT_F.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.f);
				}
				else if (childNode.LocalName == "groupChr")
				{
					cT_RunTrackChange.Items.Add(CT_GroupChr.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.groupChr);
				}
				else if (childNode.LocalName == "limLow")
				{
					cT_RunTrackChange.Items.Add(CT_LimLow.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.limLow);
				}
				else if (childNode.LocalName == "m")
				{
					cT_RunTrackChange.Items.Add(CT_M.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.m);
				}
				else if (childNode.LocalName == "nary")
				{
					cT_RunTrackChange.Items.Add(CT_Nary.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.nary);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_RunTrackChange.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_RunTrackChange.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.oMathPara);
				}
				else if (childNode.LocalName == "phant")
				{
					cT_RunTrackChange.Items.Add(CT_Phant.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.phant);
				}
				else if (childNode.LocalName == "r")
				{
					cT_RunTrackChange.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.r);
				}
				else if (childNode.LocalName == "rad")
				{
					cT_RunTrackChange.Items.Add(CT_Rad.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.rad);
				}
				else if (childNode.LocalName == "sPre")
				{
					cT_RunTrackChange.Items.Add(CT_SPre.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.sPre);
				}
				else if (childNode.LocalName == "sSub")
				{
					cT_RunTrackChange.Items.Add(CT_SSub.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.sSub);
				}
				else if (childNode.LocalName == "sSup")
				{
					cT_RunTrackChange.Items.Add(CT_SSup.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.sSup);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_RunTrackChange.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_RunTrackChange.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_RunTrackChange.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_RunTrackChange.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.commentRangeStart);
				}
				else if (childNode.LocalName == "customXml")
				{
					cT_RunTrackChange.Items.Add(CT_CustomXmlRun.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXml);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_RunTrackChange.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_RunTrackChange.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_RunTrackChange.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "limUpp")
				{
					cT_RunTrackChange.Items.Add(CT_LimUpp.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.limUpp);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_RunTrackChange.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_RunTrackChange.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_RunTrackChange.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_RunTrackChange.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "del")
				{
					cT_RunTrackChange.Items.Add(Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_RunTrackChange.Items.Add(Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_RunTrackChange.Items.Add(Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_RunTrackChange.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_RunTrackChange.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_RunTrackChange.Items.Add(Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_RunTrackChange.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_RunTrackChange.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_RunTrackChange.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_RunTrackChange.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_RunTrackChange.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.proofErr);
				}
				else if (childNode.LocalName == "r")
				{
					cT_RunTrackChange.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.r);
				}
				else if (childNode.LocalName == "sdt")
				{
					cT_RunTrackChange.Items.Add(CT_SdtRun.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.sdt);
				}
				else if (childNode.LocalName == "smartTag")
				{
					cT_RunTrackChange.Items.Add(CT_SmartTagRun.Parse(childNode, namespaceManager));
					cT_RunTrackChange.ItemsElementName.Add(ItemsChoiceType6.smartTag);
				}
			}
			return cT_RunTrackChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_Func)
				{
					((CT_Func)item).Write(sw, "func");
				}
				else if (item is CT_SSubSup)
				{
					((CT_SSubSup)item).Write(sw, "sSubSup");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_Acc)
				{
					((CT_Acc)item).Write(sw, "acc");
				}
				else if (item is CT_Bar)
				{
					((CT_Bar)item).Write(sw, "bar");
				}
				else if (item is CT_BorderBox)
				{
					((CT_BorderBox)item).Write(sw, "borderBox");
				}
				else if (item is CT_Box)
				{
					((CT_Box)item).Write(sw, "box");
				}
				else if (item is CT_D)
				{
					((CT_D)item).Write(sw, "d");
				}
				else if (item is CT_EqArr)
				{
					((CT_EqArr)item).Write(sw, "eqArr");
				}
				else if (item is CT_F)
				{
					((CT_F)item).Write(sw, "f");
				}
				else if (item is CT_GroupChr)
				{
					((CT_GroupChr)item).Write(sw, "groupChr");
				}
				else if (item is CT_LimLow)
				{
					((CT_LimLow)item).Write(sw, "limLow");
				}
				else if (item is CT_M)
				{
					((CT_M)item).Write(sw, "m");
				}
				else if (item is CT_Nary)
				{
					((CT_Nary)item).Write(sw, "nary");
				}
				else if (item is CT_OMath)
				{
					((CT_OMath)item).Write(sw, "oMath");
				}
				else if (item is CT_OMathPara)
				{
					((CT_OMathPara)item).Write(sw, "oMathPara");
				}
				else if (item is CT_Phant)
				{
					((CT_Phant)item).Write(sw, "phant");
				}
				else if (item is CT_R)
				{
					((CT_R)item).Write(sw, "r");
				}
				else if (item is CT_Rad)
				{
					((CT_Rad)item).Write(sw, "rad");
				}
				else if (item is CT_SPre)
				{
					((CT_SPre)item).Write(sw, "sPre");
				}
				else if (item is CT_SSub)
				{
					((CT_SSub)item).Write(sw, "sSub");
				}
				else if (item is CT_SSup)
				{
					((CT_SSup)item).Write(sw, "sSup");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "bookmarkEnd");
				}
				else if (item is CT_Bookmark)
				{
					((CT_Bookmark)item).Write(sw, "bookmarkStart");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeEnd");
				}
				else if (item is CT_MarkupRange)
				{
					((CT_MarkupRange)item).Write(sw, "commentRangeStart");
				}
				else if (item is CT_CustomXmlRun)
				{
					((CT_CustomXmlRun)item).Write(sw, "customXml");
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
				else if (item is CT_LimUpp)
				{
					((CT_LimUpp)item).Write(sw, "limUpp");
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
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "moveTo");
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
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
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
			}
			sw.Write(string.Format("</w:{0}", nodeName));
		}

		public IEnumerable<CT_R> GetRList()
		{
			return GetObjectList<CT_R>(ItemsChoiceType6.r);
		}

		private List<T> GetObjectList<T>(ItemsChoiceType6 type) where T : class
		{
			lock (this)
			{
				List<T> list = new List<T>();
				for (int i = 0; i < itemsElementNameField.Count; i++)
				{
					if (itemsElementNameField[i] == type)
					{
						list.Add(itemsField[i] as T);
					}
				}
				return list;
			}
		}

		private int SizeOfArray(ItemsChoiceType6 type)
		{
			lock (this)
			{
				int num = 0;
				for (int i = 0; i < itemsElementNameField.Count; i++)
				{
					if (itemsElementNameField[i] == type)
					{
						num++;
					}
				}
				return num;
			}
		}

		private T GetObjectArray<T>(int p, ItemsChoiceType6 type) where T : class
		{
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				if (objectIndex < 0 || objectIndex >= itemsField.Count)
				{
					return null;
				}
				return itemsField[objectIndex] as T;
			}
		}

		private T InsertNewObject<T>(ItemsChoiceType6 type, int p) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				itemsElementNameField.Insert(objectIndex, type);
				itemsField.Insert(objectIndex, val);
				return val;
			}
		}

		private T AddNewObject<T>(ItemsChoiceType6 type) where T : class, new()
		{
			T val = new T();
			lock (this)
			{
				itemsElementNameField.Add(type);
				itemsField.Add(val);
				return val;
			}
		}

		private void SetObject<T>(ItemsChoiceType6 type, int p, T obj) where T : class
		{
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				if (objectIndex >= 0 && objectIndex < itemsField.Count)
				{
					if (!(itemsField[objectIndex] is T))
					{
						throw new Exception(string.Format("object types are difference, itemsField[{0}] is {1}, and parameter obj is {2}", objectIndex, itemsField[objectIndex].GetType().Name, typeof(T).Name));
					}
					itemsField[objectIndex] = obj;
				}
			}
		}

		private int GetObjectIndex(ItemsChoiceType6 type, int p)
		{
			int result = -1;
			int num = 0;
			for (int i = 0; i < itemsElementNameField.Count; i++)
			{
				if (itemsElementNameField[i] == type)
				{
					if (num == p)
					{
						result = i;
						break;
					}
					num++;
				}
			}
			return result;
		}

		private void RemoveObject(ItemsChoiceType6 type, int p)
		{
			lock (this)
			{
				int objectIndex = GetObjectIndex(type, p);
				if (objectIndex >= 0 && objectIndex < itemsField.Count)
				{
					itemsElementNameField.RemoveAt(objectIndex);
					itemsField.RemoveAt(objectIndex);
				}
			}
		}
	}
}
