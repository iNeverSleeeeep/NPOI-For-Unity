using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_MailMerge
	{
		private CT_MailMergeDocType mainDocumentTypeField;

		private CT_OnOff linkToQueryField;

		private CT_MailMergeDataType dataTypeField;

		private CT_String connectStringField;

		private CT_String queryField;

		private CT_Rel dataSourceField;

		private CT_Rel headerSourceField;

		private CT_OnOff doNotSuppressBlankLinesField;

		private CT_MailMergeDest destinationField;

		private CT_String addressFieldNameField;

		private CT_String mailSubjectField;

		private CT_OnOff mailAsAttachmentField;

		private CT_OnOff viewMergedDataField;

		private CT_DecimalNumber activeRecordField;

		private CT_DecimalNumber checkErrorsField;

		private CT_Odso odsoField;

		[XmlElement(Order = 0)]
		public CT_MailMergeDocType mainDocumentType
		{
			get
			{
				return mainDocumentTypeField;
			}
			set
			{
				mainDocumentTypeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff linkToQuery
		{
			get
			{
				return linkToQueryField;
			}
			set
			{
				linkToQueryField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_MailMergeDataType dataType
		{
			get
			{
				return dataTypeField;
			}
			set
			{
				dataTypeField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_String connectString
		{
			get
			{
				return connectStringField;
			}
			set
			{
				connectStringField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_String query
		{
			get
			{
				return queryField;
			}
			set
			{
				queryField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Rel dataSource
		{
			get
			{
				return dataSourceField;
			}
			set
			{
				dataSourceField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Rel headerSource
		{
			get
			{
				return headerSourceField;
			}
			set
			{
				headerSourceField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff doNotSuppressBlankLines
		{
			get
			{
				return doNotSuppressBlankLinesField;
			}
			set
			{
				doNotSuppressBlankLinesField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_MailMergeDest destination
		{
			get
			{
				return destinationField;
			}
			set
			{
				destinationField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_String addressFieldName
		{
			get
			{
				return addressFieldNameField;
			}
			set
			{
				addressFieldNameField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_String mailSubject
		{
			get
			{
				return mailSubjectField;
			}
			set
			{
				mailSubjectField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_OnOff mailAsAttachment
		{
			get
			{
				return mailAsAttachmentField;
			}
			set
			{
				mailAsAttachmentField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_OnOff viewMergedData
		{
			get
			{
				return viewMergedDataField;
			}
			set
			{
				viewMergedDataField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_DecimalNumber activeRecord
		{
			get
			{
				return activeRecordField;
			}
			set
			{
				activeRecordField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_DecimalNumber checkErrors
		{
			get
			{
				return checkErrorsField;
			}
			set
			{
				checkErrorsField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_Odso odso
		{
			get
			{
				return odsoField;
			}
			set
			{
				odsoField = value;
			}
		}

		public static CT_MailMerge Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MailMerge cT_MailMerge = new CT_MailMerge();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "mainDocumentType")
				{
					cT_MailMerge.mainDocumentType = CT_MailMergeDocType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "linkToQuery")
				{
					cT_MailMerge.linkToQuery = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dataType")
				{
					cT_MailMerge.dataType = CT_MailMergeDataType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "connectString")
				{
					cT_MailMerge.connectString = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "query")
				{
					cT_MailMerge.query = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dataSource")
				{
					cT_MailMerge.dataSource = CT_Rel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "headerSource")
				{
					cT_MailMerge.headerSource = CT_Rel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotSuppressBlankLines")
				{
					cT_MailMerge.doNotSuppressBlankLines = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "destination")
				{
					cT_MailMerge.destination = CT_MailMergeDest.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "addressFieldName")
				{
					cT_MailMerge.addressFieldName = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mailSubject")
				{
					cT_MailMerge.mailSubject = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mailAsAttachment")
				{
					cT_MailMerge.mailAsAttachment = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "viewMergedData")
				{
					cT_MailMerge.viewMergedData = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "activeRecord")
				{
					cT_MailMerge.activeRecord = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "checkErrors")
				{
					cT_MailMerge.checkErrors = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "odso")
				{
					cT_MailMerge.odso = CT_Odso.Parse(childNode, namespaceManager);
				}
			}
			return cT_MailMerge;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (mainDocumentType != null)
			{
				mainDocumentType.Write(sw, "mainDocumentType");
			}
			if (linkToQuery != null)
			{
				linkToQuery.Write(sw, "linkToQuery");
			}
			if (dataType != null)
			{
				dataType.Write(sw, "dataType");
			}
			if (connectString != null)
			{
				connectString.Write(sw, "connectString");
			}
			if (query != null)
			{
				query.Write(sw, "query");
			}
			if (dataSource != null)
			{
				dataSource.Write(sw, "dataSource");
			}
			if (headerSource != null)
			{
				headerSource.Write(sw, "headerSource");
			}
			if (doNotSuppressBlankLines != null)
			{
				doNotSuppressBlankLines.Write(sw, "doNotSuppressBlankLines");
			}
			if (destination != null)
			{
				destination.Write(sw, "destination");
			}
			if (addressFieldName != null)
			{
				addressFieldName.Write(sw, "addressFieldName");
			}
			if (mailSubject != null)
			{
				mailSubject.Write(sw, "mailSubject");
			}
			if (mailAsAttachment != null)
			{
				mailAsAttachment.Write(sw, "mailAsAttachment");
			}
			if (viewMergedData != null)
			{
				viewMergedData.Write(sw, "viewMergedData");
			}
			if (activeRecord != null)
			{
				activeRecord.Write(sw, "activeRecord");
			}
			if (checkErrors != null)
			{
				checkErrors.Write(sw, "checkErrors");
			}
			if (odso != null)
			{
				odso.Write(sw, "odso");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
