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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_OMath
	{
		private ArrayList itemsField;

		private List<ItemsChoiceType8> itemsElementNameField;

		[XmlElement("customXmlDelRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("customXmlMoveFromRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("customXmlMoveFromRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("customXmlMoveToRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("moveToRangeEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("sPre", typeof(CT_SPre), Order = 0)]
		[XmlElement("sSub", typeof(CT_SSub), Order = 0)]
		[XmlElement("permEnd", typeof(CT_Perm), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("customXmlInsRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("acc", typeof(CT_Acc), Order = 0)]
		[XmlElement("bookmarkEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("borderBox", typeof(CT_BorderBox), Order = 0)]
		[XmlElement("sSubSup", typeof(CT_SSubSup), Order = 0)]
		[XmlElement("sSup", typeof(CT_SSup), Order = 0)]
		[XmlElement("oMath", typeof(CT_OMath), Order = 0)]
		[XmlElement("oMathPara", typeof(CT_OMathPara), Order = 0)]
		[XmlElement("phant", typeof(CT_Phant), Order = 0)]
		[XmlElement("r", typeof(CT_R), Order = 0)]
		[XmlElement("customXmlMoveToRangeStart", typeof(CT_TrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("del", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("ins", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("moveFrom", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("moveFromRangeEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("box", typeof(CT_Box), Order = 0)]
		[XmlElement("rad", typeof(CT_Rad), Order = 0)]
		[XmlElement("moveFromRangeStart", typeof(CT_MoveBookmark), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("moveTo", typeof(CT_RunTrackChange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("moveToRangeStart", typeof(CT_MoveBookmark), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("permStart", typeof(CT_PermStart), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("proofErr", typeof(CT_ProofErr), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("m", typeof(CT_M), Order = 0)]
		[XmlElement("bar", typeof(CT_Bar), Order = 0)]
		[XmlElement("d", typeof(CT_D), Order = 0)]
		[XmlElement("eqArr", typeof(CT_EqArr), Order = 0)]
		[XmlElement("customXmlInsRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("limLow", typeof(CT_LimLow), Order = 0)]
		[XmlElement("f", typeof(CT_F), Order = 0)]
		[XmlElement("func", typeof(CT_Func), Order = 0)]
		[XmlElement("commentRangeEnd", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("bookmarkStart", typeof(CT_Bookmark), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("groupChr", typeof(CT_GroupChr), Order = 0)]
		[XmlElement("limUpp", typeof(CT_LimUpp), Order = 0)]
		[XmlElement("commentRangeStart", typeof(CT_MarkupRange), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("customXmlDelRangeEnd", typeof(CT_Markup), Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", Order = 0)]
		[XmlElement("nary", typeof(CT_Nary), Order = 0)]
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
		public List<ItemsChoiceType8> ItemsElementName
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

		public CT_OMath()
		{
			itemsElementNameField = new List<ItemsChoiceType8>();
			itemsField = new ArrayList();
		}

		public static CT_OMath Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OMath cT_OMath = new CT_OMath();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "del")
				{
					cT_OMath.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.del);
				}
				else if (childNode.LocalName == "moveFromRangeStart")
				{
					cT_OMath.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.moveFromRangeStart);
				}
				else if (childNode.LocalName == "acc")
				{
					cT_OMath.Items.Add(CT_Acc.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.acc);
				}
				else if (childNode.LocalName == "bar")
				{
					cT_OMath.Items.Add(CT_Bar.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.bar);
				}
				else if (childNode.LocalName == "borderBox")
				{
					cT_OMath.Items.Add(CT_BorderBox.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.borderBox);
				}
				else if (childNode.LocalName == "box")
				{
					cT_OMath.Items.Add(CT_Box.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.box);
				}
				else if (childNode.LocalName == "d")
				{
					cT_OMath.Items.Add(CT_D.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.d);
				}
				else if (childNode.LocalName == "eqArr")
				{
					cT_OMath.Items.Add(CT_EqArr.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.eqArr);
				}
				else if (childNode.LocalName == "f")
				{
					cT_OMath.Items.Add(CT_F.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.f);
				}
				else if (childNode.LocalName == "func")
				{
					cT_OMath.Items.Add(CT_Func.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.func);
				}
				else if (childNode.LocalName == "groupChr")
				{
					cT_OMath.Items.Add(CT_GroupChr.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.groupChr);
				}
				else if (childNode.LocalName == "limLow")
				{
					cT_OMath.Items.Add(CT_LimLow.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.limLow);
				}
				else if (childNode.LocalName == "limUpp")
				{
					cT_OMath.Items.Add(CT_LimUpp.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.limUpp);
				}
				else if (childNode.LocalName == "m")
				{
					cT_OMath.Items.Add(CT_M.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.m);
				}
				else if (childNode.LocalName == "nary")
				{
					cT_OMath.Items.Add(CT_Nary.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.nary);
				}
				else if (childNode.LocalName == "oMath")
				{
					cT_OMath.Items.Add(Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.oMath);
				}
				else if (childNode.LocalName == "oMathPara")
				{
					cT_OMath.Items.Add(CT_OMathPara.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.oMathPara);
				}
				else if (childNode.LocalName == "phant")
				{
					cT_OMath.Items.Add(CT_Phant.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.phant);
				}
				else if (childNode.LocalName == "r")
				{
					cT_OMath.Items.Add(CT_R.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.r);
				}
				else if (childNode.LocalName == "rad")
				{
					cT_OMath.Items.Add(CT_Rad.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.rad);
				}
				else if (childNode.LocalName == "sPre")
				{
					cT_OMath.Items.Add(CT_SPre.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.sPre);
				}
				else if (childNode.LocalName == "sSub")
				{
					cT_OMath.Items.Add(CT_SSub.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.sSub);
				}
				else if (childNode.LocalName == "sSubSup")
				{
					cT_OMath.Items.Add(CT_SSubSup.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.sSubSup);
				}
				else if (childNode.LocalName == "sSup")
				{
					cT_OMath.Items.Add(CT_SSup.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.sSup);
				}
				else if (childNode.LocalName == "bookmarkEnd")
				{
					cT_OMath.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.bookmarkEnd);
				}
				else if (childNode.LocalName == "bookmarkStart")
				{
					cT_OMath.Items.Add(CT_Bookmark.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.bookmarkStart);
				}
				else if (childNode.LocalName == "commentRangeEnd")
				{
					cT_OMath.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.commentRangeEnd);
				}
				else if (childNode.LocalName == "commentRangeStart")
				{
					cT_OMath.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.commentRangeStart);
				}
				else if (childNode.LocalName == "customXmlDelRangeEnd")
				{
					cT_OMath.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlDelRangeEnd);
				}
				else if (childNode.LocalName == "customXmlDelRangeStart")
				{
					cT_OMath.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlDelRangeStart);
				}
				else if (childNode.LocalName == "customXmlInsRangeEnd")
				{
					cT_OMath.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlInsRangeEnd);
				}
				else if (childNode.LocalName == "customXmlInsRangeStart")
				{
					cT_OMath.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlInsRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeEnd")
				{
					cT_OMath.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveFromRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveFromRangeStart")
				{
					cT_OMath.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveFromRangeStart);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeEnd")
				{
					cT_OMath.Items.Add(CT_Markup.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveToRangeEnd);
				}
				else if (childNode.LocalName == "customXmlMoveToRangeStart")
				{
					cT_OMath.Items.Add(CT_TrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.customXmlMoveToRangeStart);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_OMath.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.ins);
				}
				else if (childNode.LocalName == "moveFrom")
				{
					cT_OMath.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.moveFrom);
				}
				else if (childNode.LocalName == "moveFromRangeEnd")
				{
					cT_OMath.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.moveFromRangeEnd);
				}
				else if (childNode.LocalName == "moveTo")
				{
					cT_OMath.Items.Add(CT_RunTrackChange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.moveTo);
				}
				else if (childNode.LocalName == "moveToRangeEnd")
				{
					cT_OMath.Items.Add(CT_MarkupRange.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.moveToRangeEnd);
				}
				else if (childNode.LocalName == "moveToRangeStart")
				{
					cT_OMath.Items.Add(CT_MoveBookmark.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.moveToRangeStart);
				}
				else if (childNode.LocalName == "permEnd")
				{
					cT_OMath.Items.Add(CT_Perm.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.permEnd);
				}
				else if (childNode.LocalName == "permStart")
				{
					cT_OMath.Items.Add(CT_PermStart.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.permStart);
				}
				else if (childNode.LocalName == "proofErr")
				{
					cT_OMath.Items.Add(CT_ProofErr.Parse(childNode, namespaceManager));
					cT_OMath.ItemsElementName.Add(ItemsChoiceType8.proofErr);
				}
			}
			return cT_OMath;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			foreach (object item in Items)
			{
				if (item is CT_RunTrackChange)
				{
					((CT_RunTrackChange)item).Write(sw, "del");
				}
				else if (item is CT_MoveBookmark)
				{
					((CT_MoveBookmark)item).Write(sw, "moveFromRangeStart");
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
				else if (item is CT_Func)
				{
					((CT_Func)item).Write(sw, "func");
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
			}
			sw.Write(string.Format("</m:{0}", nodeName));
		}
	}
}
