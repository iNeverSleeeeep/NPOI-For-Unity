using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_WriteProtection
	{
		private ST_OnOff recommendedField;

		private bool recommendedFieldSpecified;

		private ST_CryptProv cryptProviderTypeField;

		private bool cryptProviderTypeFieldSpecified;

		private ST_AlgClass cryptAlgorithmClassField;

		private bool cryptAlgorithmClassFieldSpecified;

		private ST_AlgType cryptAlgorithmTypeField;

		private bool cryptAlgorithmTypeFieldSpecified;

		private string cryptAlgorithmSidField;

		private string cryptSpinCountField;

		private string cryptProviderField;

		private byte[] algIdExtField;

		private string algIdExtSourceField;

		private byte[] cryptProviderTypeExtField;

		private string cryptProviderTypeExtSourceField;

		private byte[] hashField;

		private byte[] saltField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff recommended
		{
			get
			{
				return recommendedField;
			}
			set
			{
				recommendedField = value;
			}
		}

		[XmlIgnore]
		public bool recommendedSpecified
		{
			get
			{
				return recommendedFieldSpecified;
			}
			set
			{
				recommendedFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_CryptProv cryptProviderType
		{
			get
			{
				return cryptProviderTypeField;
			}
			set
			{
				cryptProviderTypeField = value;
			}
		}

		[XmlIgnore]
		public bool cryptProviderTypeSpecified
		{
			get
			{
				return cryptProviderTypeFieldSpecified;
			}
			set
			{
				cryptProviderTypeFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_AlgClass cryptAlgorithmClass
		{
			get
			{
				return cryptAlgorithmClassField;
			}
			set
			{
				cryptAlgorithmClassField = value;
			}
		}

		[XmlIgnore]
		public bool cryptAlgorithmClassSpecified
		{
			get
			{
				return cryptAlgorithmClassFieldSpecified;
			}
			set
			{
				cryptAlgorithmClassFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_AlgType cryptAlgorithmType
		{
			get
			{
				return cryptAlgorithmTypeField;
			}
			set
			{
				cryptAlgorithmTypeField = value;
			}
		}

		[XmlIgnore]
		public bool cryptAlgorithmTypeSpecified
		{
			get
			{
				return cryptAlgorithmTypeFieldSpecified;
			}
			set
			{
				cryptAlgorithmTypeFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string cryptAlgorithmSid
		{
			get
			{
				return cryptAlgorithmSidField;
			}
			set
			{
				cryptAlgorithmSidField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string cryptSpinCount
		{
			get
			{
				return cryptSpinCountField;
			}
			set
			{
				cryptSpinCountField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string cryptProvider
		{
			get
			{
				return cryptProviderField;
			}
			set
			{
				cryptProviderField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] algIdExt
		{
			get
			{
				return algIdExtField;
			}
			set
			{
				algIdExtField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string algIdExtSource
		{
			get
			{
				return algIdExtSourceField;
			}
			set
			{
				algIdExtSourceField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "hexBinary")]
		public byte[] cryptProviderTypeExt
		{
			get
			{
				return cryptProviderTypeExtField;
			}
			set
			{
				cryptProviderTypeExtField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string cryptProviderTypeExtSource
		{
			get
			{
				return cryptProviderTypeExtSourceField;
			}
			set
			{
				cryptProviderTypeExtSourceField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "base64Binary")]
		public byte[] hash
		{
			get
			{
				return hashField;
			}
			set
			{
				hashField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "base64Binary")]
		public byte[] salt
		{
			get
			{
				return saltField;
			}
			set
			{
				saltField = value;
			}
		}

		public static CT_WriteProtection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WriteProtection cT_WriteProtection = new CT_WriteProtection();
			if (node.Attributes["w:recommended"] != null)
			{
				cT_WriteProtection.recommended = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:recommended"].Value);
			}
			if (node.Attributes["w:cryptProviderType"] != null)
			{
				cT_WriteProtection.cryptProviderType = (ST_CryptProv)Enum.Parse(typeof(ST_CryptProv), node.Attributes["w:cryptProviderType"].Value);
			}
			if (node.Attributes["w:cryptAlgorithmClass"] != null)
			{
				cT_WriteProtection.cryptAlgorithmClass = (ST_AlgClass)Enum.Parse(typeof(ST_AlgClass), node.Attributes["w:cryptAlgorithmClass"].Value);
			}
			if (node.Attributes["w:cryptAlgorithmType"] != null)
			{
				cT_WriteProtection.cryptAlgorithmType = (ST_AlgType)Enum.Parse(typeof(ST_AlgType), node.Attributes["w:cryptAlgorithmType"].Value);
			}
			cT_WriteProtection.cryptAlgorithmSid = XmlHelper.ReadString(node.Attributes["w:cryptAlgorithmSid"]);
			cT_WriteProtection.cryptSpinCount = XmlHelper.ReadString(node.Attributes["w:cryptSpinCount"]);
			cT_WriteProtection.cryptProvider = XmlHelper.ReadString(node.Attributes["w:cryptProvider"]);
			cT_WriteProtection.algIdExt = XmlHelper.ReadBytes(node.Attributes["w:algIdExt"]);
			cT_WriteProtection.algIdExtSource = XmlHelper.ReadString(node.Attributes["w:algIdExtSource"]);
			cT_WriteProtection.cryptProviderTypeExt = XmlHelper.ReadBytes(node.Attributes["w:cryptProviderTypeExt"]);
			cT_WriteProtection.cryptProviderTypeExtSource = XmlHelper.ReadString(node.Attributes["w:cryptProviderTypeExtSource"]);
			cT_WriteProtection.hash = XmlHelper.ReadBytes(node.Attributes["w:hash"]);
			cT_WriteProtection.salt = XmlHelper.ReadBytes(node.Attributes["w:salt"]);
			return cT_WriteProtection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:recommended", recommended.ToString());
			XmlHelper.WriteAttribute(sw, "w:cryptProviderType", cryptProviderType.ToString());
			XmlHelper.WriteAttribute(sw, "w:cryptAlgorithmClass", cryptAlgorithmClass.ToString());
			XmlHelper.WriteAttribute(sw, "w:cryptAlgorithmType", cryptAlgorithmType.ToString());
			XmlHelper.WriteAttribute(sw, "w:cryptAlgorithmSid", cryptAlgorithmSid);
			XmlHelper.WriteAttribute(sw, "w:cryptSpinCount", cryptSpinCount);
			XmlHelper.WriteAttribute(sw, "w:cryptProvider", cryptProvider);
			XmlHelper.WriteAttribute(sw, "w:algIdExt", algIdExt);
			XmlHelper.WriteAttribute(sw, "w:algIdExtSource", algIdExtSource);
			XmlHelper.WriteAttribute(sw, "w:cryptProviderTypeExt", cryptProviderTypeExt);
			XmlHelper.WriteAttribute(sw, "w:cryptProviderTypeExtSource", cryptProviderTypeExtSource);
			XmlHelper.WriteAttribute(sw, "w:hash", hash);
			XmlHelper.WriteAttribute(sw, "w:salt", salt);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
