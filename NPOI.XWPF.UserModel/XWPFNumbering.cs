using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// @author Philipp Epp
	public class XWPFNumbering : POIXMLDocumentPart
	{
		protected List<XWPFAbstractNum> abstractNums = new List<XWPFAbstractNum>();

		protected List<XWPFNum> nums = new List<XWPFNum>();

		private CT_Numbering ctNumbering;

		private bool isNew;

		/// create a new styles object with an existing document 
		public XWPFNumbering(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			isNew = true;
		}

		/// create a new XWPFNumbering object for use in a new document
		public XWPFNumbering()
		{
			abstractNums = new List<XWPFAbstractNum>();
			nums = new List<XWPFNum>();
			isNew = true;
		}

		/// read numbering form an existing package
		internal override void OnDocumentRead()
		{
			NumberingDocument numberingDocument = null;
			XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(GetPackagePart().GetInputStream());
			try
			{
				numberingDocument = NumberingDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
				ctNumbering = numberingDocument.Numbering;
				foreach (CT_Num num in ctNumbering.GetNumList())
				{
					nums.Add(new XWPFNum(num, this));
				}
				foreach (CT_AbstractNum abstractNum in ctNumbering.GetAbstractNumList())
				{
					abstractNums.Add(new XWPFAbstractNum(abstractNum, this));
				}
				isNew = false;
			}
			catch (Exception ex)
			{
				throw new POIXMLException(ex);
			}
		}

		/// save and Commit numbering
		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			NumberingDocument numberingDocument = new NumberingDocument(ctNumbering);
			numberingDocument.Save(outputStream);
			outputStream.Close();
		}

		/// Sets the ctNumbering
		/// @param numbering
		public void SetNumbering(CT_Numbering numbering)
		{
			ctNumbering = numbering;
		}

		/// Checks whether number with numID exists
		/// @param numID
		/// @return bool		true if num exist, false if num not exist
		public bool NumExist(string numID)
		{
			foreach (XWPFNum num in nums)
			{
				if (num.GetCTNum().numId.Equals(numID))
				{
					return true;
				}
			}
			return false;
		}

		/// add a new number to the numbering document
		/// @param num
		public string AddNum(XWPFNum num)
		{
			ctNumbering.AddNewNum();
			int pos = ctNumbering.GetNumList().Count - 1;
			ctNumbering.SetNumArray(pos, num.GetCTNum());
			nums.Add(num);
			return num.GetCTNum().numId;
		}

		/// Add a new num with an abstractNumID
		/// @return return NumId of the Added num 
		public string AddNum(string abstractNumID)
		{
			CT_Num cT_Num = ctNumbering.AddNewNum();
			cT_Num.AddNewAbstractNumId();
			cT_Num.abstractNumId.val = abstractNumID;
			cT_Num.numId = (nums.Count + 1).ToString();
			XWPFNum item = new XWPFNum(cT_Num, this);
			nums.Add(item);
			return cT_Num.numId;
		}

		/// Add a new num with an abstractNumID and a numID
		/// @param abstractNumID
		/// @param numID
		public void AddNum(string abstractNumID, string numID)
		{
			CT_Num cT_Num = ctNumbering.AddNewNum();
			cT_Num.AddNewAbstractNumId();
			cT_Num.abstractNumId.val = abstractNumID;
			cT_Num.numId = numID;
			XWPFNum item = new XWPFNum(cT_Num, this);
			nums.Add(item);
		}

		/// Get Num by NumID
		/// @param numID
		/// @return abstractNum with NumId if no Num exists with that NumID 
		/// 			null will be returned
		public XWPFNum GetNum(string numID)
		{
			foreach (XWPFNum num in nums)
			{
				if (num.GetCTNum().numId.Equals(numID))
				{
					return num;
				}
			}
			return null;
		}

		/// Get AbstractNum by abstractNumID
		/// @param abstractNumID
		/// @return  abstractNum with abstractNumId if no abstractNum exists with that abstractNumID 
		/// 			null will be returned
		public XWPFAbstractNum GetAbstractNum(string abstractNumID)
		{
			foreach (XWPFAbstractNum abstractNum in abstractNums)
			{
				if (abstractNum.GetAbstractNum().abstractNumId.Equals(abstractNumID))
				{
					return abstractNum;
				}
			}
			return null;
		}

		/// Compare AbstractNum with abstractNums of this numbering document.
		/// If the content of abstractNum Equals with an abstractNum of the List in numbering
		/// the Bigint Value of it will be returned.
		/// If no equal abstractNum is existing null will be returned
		///
		/// @param abstractNum
		/// @return 	Bigint
		public string GetIdOfAbstractNum(XWPFAbstractNum abstractNum)
		{
			CT_AbstractNum ctAbstractNum = abstractNum.GetCTAbstractNum().Copy();
			XWPFAbstractNum xWPFAbstractNum = new XWPFAbstractNum(ctAbstractNum, this);
			for (int i = 0; i < abstractNums.Count; i++)
			{
				xWPFAbstractNum.GetCTAbstractNum().abstractNumId = i.ToString();
				xWPFAbstractNum.SetNumbering(this);
				if (xWPFAbstractNum.GetCTAbstractNum().ValueEquals(abstractNums[i].GetCTAbstractNum()))
				{
					return xWPFAbstractNum.GetCTAbstractNum().abstractNumId;
				}
			}
			return null;
		}

		/// add a new AbstractNum and return its AbstractNumID 
		/// @param abstractNum
		public string AddAbstractNum(XWPFAbstractNum abstractNum)
		{
			int count = abstractNums.Count;
			if (abstractNum.GetAbstractNum() != null)
			{
				ctNumbering.AddNewAbstractNum().Set(abstractNum.GetAbstractNum());
			}
			else
			{
				ctNumbering.AddNewAbstractNum();
				abstractNum.GetAbstractNum().abstractNumId = count.ToString();
				ctNumbering.SetAbstractNumArray(count, abstractNum.GetAbstractNum());
			}
			abstractNums.Add(abstractNum);
			return abstractNum.GetAbstractNum().abstractNumId;
		}

		/// <summary>
		/// Add a new AbstractNum
		/// </summary>
		/// <returns></returns>
		/// @author antony liu
		public string AddAbstractNum()
		{
			CT_AbstractNum ctAbstractNum = ctNumbering.AddNewAbstractNum();
			XWPFAbstractNum xWPFAbstractNum = new XWPFAbstractNum(ctAbstractNum, this);
			xWPFAbstractNum.AbstractNumId = abstractNums.Count.ToString();
			xWPFAbstractNum.MultiLevelType = MultiLevelType.HybridMultilevel;
			xWPFAbstractNum.InitLvl();
			abstractNums.Add(xWPFAbstractNum);
			return xWPFAbstractNum.GetAbstractNum().abstractNumId;
		}

		/// remove an existing abstractNum 
		/// @param abstractNumID
		/// @return true if abstractNum with abstractNumID exists in NumberingArray,
		/// 		   false if abstractNum with abstractNumID not exists
		public bool RemoveAbstractNum(string abstractNumID)
		{
			if (int.Parse(abstractNumID) < abstractNums.Count)
			{
				ctNumbering.RemoveAbstractNum(int.Parse(abstractNumID));
				abstractNums.RemoveAt(int.Parse(abstractNumID));
				return true;
			}
			return false;
		}

		/// return the abstractNumID
		/// If the AbstractNumID not exists
		/// return null
		///  @param 		numID
		///  @return 		abstractNumID
		public string GetAbstractNumID(string numID)
		{
			XWPFNum num = GetNum(numID);
			if (num == null)
			{
				return null;
			}
			if (num.GetCTNum() == null)
			{
				return null;
			}
			if (num.GetCTNum().abstractNumId == null)
			{
				return null;
			}
			return num.GetCTNum().abstractNumId.val;
		}
	}
}
