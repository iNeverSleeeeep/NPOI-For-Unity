using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_OMathArg
	{
		private CT_OMathArgPr argPrField;

		private ArrayList itemsField;

		private List<ItemsChoiceType7> itemsElementNameField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_OMathArgPr argPr
		{
			get
			{
				return argPrField;
			}
			set
			{
				argPrField = value;
			}
		}

		[XmlElement("phant", typeof(CT_Phant), Order = 1)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("rad", typeof(CT_Rad), Order = 1)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("oMath", typeof(CT_OMath), Order = 1)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("r", typeof(CT_R), Order = 1)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("acc", typeof(CT_Acc), Order = 1)]
		[XmlElement("bar", typeof(CT_Bar), Order = 1)]
		[XmlElement("borderBox", typeof(CT_BorderBox), Order = 1)]
		[XmlElement("box", typeof(CT_Box), Order = 1)]
		[XmlElement("d", typeof(CT_D), Order = 1)]
		[XmlElement("eqArr", typeof(CT_EqArr), Order = 1)]
		[XmlElement("f", typeof(CT_F), Order = 1)]
		[XmlElement("func", typeof(CT_Func), Order = 1)]
		[XmlElement("groupChr", typeof(CT_GroupChr), Order = 1)]
		[XmlElement("limLow", typeof(CT_LimLow), Order = 1)]
		[XmlElement("limUpp", typeof(CT_LimUpp), Order = 1)]
		[XmlElement("m", typeof(CT_M), Order = 1)]
		[XmlElement("nary", typeof(CT_Nary), Order = 1)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("del", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("sSup", typeof(CT_SSup), Order = 1)]
		[XmlElement("sSubSup", typeof(CT_SSubSup), Order = 1)]
		[XmlElement("sSub", typeof(CT_SSub), Order = 1)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Order = 1)]
		[XmlElement("permEnd", typeof(CT_Perm), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("permStart", typeof(CT_PermStart), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 1)]
		[XmlElement("sPre", typeof(CT_SPre), Order = 1)]
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

		[XmlElement("ItemsElementName", Order = 2)]
		[XmlIgnore]
		public List<ItemsChoiceType7> ItemsElementName
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

		[XmlElement(Order = 3)]
		public CT_CtrlPr ctrlPr
		{
			get
			{
				return ctrlPrField;
			}
			set
			{
				ctrlPrField = value;
			}
		}

		public CT_OMathArg()
		{
			itemsElementNameField = new List<ItemsChoiceType7>();
			itemsField = new ArrayList();
		}

		public static CT_OMathArg Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OMathArg cT_OMathArg = new CT_OMathArg();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "argPr")
				{
					cT_OMathArg.argPr = CT_OMathArgPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "borderBox")
				{
					cT_OMathArg.Items.Add(CT_BorderBox.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.borderBox);
				}
				else if (childNode.LocalName == "acc")
				{
					cT_OMathArg.Items.Add(CT_Acc.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.acc);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_OMathArg.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_OMathArg.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.proofErr);
				}
				else if (childNode.LocalName == "d")
				{
					cT_OMathArg.Items.Add(CT_D.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.d);
				}
				else if (childNode.LocalName == "eqArr")
				{
					cT_OMathArg.Items.Add(CT_EqArr.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.eqArr);
				}
				else if (childNode.LocalName == "f")
				{
					cT_OMathArg.Items.Add(CT_F.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.f);
				}
				else if (childNode.LocalName == "func")
				{
					cT_OMathArg.Items.Add(CT_Func.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.func);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_OMathArg.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "groupChr")
				{
					cT_OMathArg.Items.Add(CT_GroupChr.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.groupChr);
				}
				else if (childNode.LocalName == "limLow")
				{
					cT_OMathArg.Items.Add(CT_LimLow.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.limLow);
				}
				else if (childNode.LocalName == "limUpp")
				{
					cT_OMathArg.Items.Add(CT_LimUpp.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.limUpp);
				}
				else if (childNode.LocalName == "m")
				{
					cT_OMathArg.Items.Add(CT_M.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.m);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_OMathArg.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_OMathArg.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_OMathArg.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.permStart);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_OMathArg.Items.Add(CT_OMath.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_OMathArg.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.oMathPara);
				}
				else if (childNode.LocalName == "phant")
				{
					cT_OMathArg.Items.Add(CT_Phant.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.phant);
				}
				else if (childNode.LocalName == "bar")
				{
					cT_OMathArg.Items.Add(CT_Bar.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.bar);
				}
				else if (childNode.LocalName == "r")
				{
					cT_OMathArg.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.r);
				}
				else if (childNode.LocalName == "rad")
				{
					cT_OMathArg.Items.Add(CT_Rad.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.rad);
				}
				else if (childNode.LocalName == "sPre")
				{
					cT_OMathArg.Items.Add(CT_SPre.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.sPre);
				}
				else if (childNode.LocalName == "sSub")
				{
					cT_OMathArg.Items.Add(CT_SSub.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.sSub);
				}
				else if (childNode.LocalName == "sSubSup")
				{
					cT_OMathArg.Items.Add(CT_SSubSup.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.sSubSup);
				}
				else if (childNode.LocalName == "sSup")
				{
					cT_OMathArg.Items.Add(CT_SSup.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.sSup);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_OMathArg.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_OMathArg.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_OMathArg.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_OMathArg.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.commentRangeStart);
				}
				else if (childNode.LocalName == "nary")
				{
					cT_OMathArg.Items.Add(CT_Nary.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.nary);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_OMathArg.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_OMathArg.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_OMathArg.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_OMathArg.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "del")
				{
					cT_OMathArg.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.del);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_OMathArg.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.ins);
				}
				else if (childNode.LocalName == "box")
				{
					cT_OMathArg.Items.Add(CT_Box.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.box);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_OMathArg.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_OMathArg.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_OMathArg.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.moveFromRangeStart);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_OMathArg.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_OMathArg.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_OMathArg.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_OMathArg.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_OMathArg.ItemsElementName.Add(ItemsChoiceType7.permEnd);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_OMathArg.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_OMathArg;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (argPr != null)
			{
				argPr.Write(sw, "argPr");
			}
			foreach (object item in Items)
			{
				if (item is CT_BorderBox)
				{
					((CT_BorderBox)item).Write(sw, "borderBox");
				}
				else if (item is CT_Acc)
				{
					((CT_Acc)item).Write(sw, "acc");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlInsRangeStart");
				}
				else if (item is CT_ProofErr)
				{
					((CT_ProofErr)item).Write(sw, "proofErr");
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
				else if (item is CT_Func)
				{
					((CT_Func)item).Write(sw, "func");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveFromRangeEnd");
				}
				else if (item is CT_GroupChr)
				{
					((CT_GroupChr)item).Write(sw, "groupChr");
				}
				else if (item is CT_LimLow)
				{
					((CT_LimLow)item).Write(sw, "limLow");
				}
				else if (item is CT_LimUpp)
				{
					((CT_LimUpp)item).Write(sw, "limUpp");
				}
				else if (item is CT_M)
				{
					((CT_M)item).Write(sw, "m");
				}
				else if (item is CT_TrackChange)
				{
					((CT_TrackChange)item).Write(sw, "customXmlMoveToRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlDelRangeEnd");
				}
				else if (item is CT_PermStart)
				{
					((CT_PermStart)item).Write(sw, "permStart");
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
				else if (item is CT_Bar)
				{
					((CT_Bar)item).Write(sw, "bar");
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
				else if (item is CT_SSubSup)
				{
					((CT_SSubSup)item).Write(sw, "sSubSup");
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
				else if (item is CT_Nary)
				{
					((CT_Nary)item).Write(sw, "nary");
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
					((CT_TrackChange)item).Write(sw, "customXmlMoveFromRangeStart");
				}
				else if (item is CT_Markup)
				{
					((CT_Markup)item).Write(sw, "customXmlMoveToRangeEnd");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "ins");
				}
				else if (item is CT_Box)
				{
					((CT_Box)item).Write(sw, "box");
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
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}", nodeName));
		}
	}
}
