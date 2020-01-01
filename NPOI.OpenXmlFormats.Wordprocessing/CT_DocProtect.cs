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
	public class CT_DocProtect
	{
		private ST_DocProtect editField;

		private bool editFieldSpecified;

		private ST_OnOff formattingField;

		private bool formattingFieldSpecified;

		private ST_OnOff enforcementField;

		private bool enforcementFieldSpecified;

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

		private string hashField;

		private string saltField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_DocProtect edit
		{
			get
			{
				return editField;
			}
			set
			{
				editField = value;
			}
		}

		[XmlIgnore]
		public bool editSpecified
		{
			get
			{
				return editField != ST_DocProtect.none;
			}
			set
			{
				editFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff formatting
		{
			get
			{
				return formattingField;
			}
			set
			{
				formattingField = value;
			}
		}

		[XmlIgnore]
		public bool formattingSpecified
		{
			get
			{
				return formattingFieldSpecified;
			}
			set
			{
				formattingFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff enforcement
		{
			get
			{
				return enforcementField;
			}
			set
			{
				enforcementField = value;
			}
		}

		[XmlIgnore]
		public bool enforcementSpecified
		{
			get
			{
				return editField != ST_DocProtect.none;
			}
			set
			{
				enforcementFieldSpecified = value;
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
		public string hash
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
		public string salt
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

		public static CT_DocProtect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DocProtect cT_DocProtect = new CT_DocProtect();
			if (node.Attributes["w:edit"] != null)
			{
				cT_DocProtect.edit = (ST_DocProtect)Enum.Parse(typeof(ST_DocProtect), node.Attributes["w:edit"].Value);
			}
			if (node.Attributes["w:formatting"] != null)
			{
				cT_DocProtect.formatting = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:formatting"].Value);
			}
			if (node.Attributes["w:enforcement"] != null)
			{
				cT_DocProtect.enforcement = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:enforcement"].Value);
			}
			if (node.Attributes["w:cryptProviderType"] != null)
			{
				cT_DocProtect.cryptProviderType = (ST_CryptProv)Enum.Parse(typeof(ST_CryptProv), node.Attributes["w:cryptProviderType"].Value);
			}
			if (node.Attributes["w:cryptAlgorithmClass"] != null)
			{
				cT_DocProtect.cryptAlgorithmClass = (ST_AlgClass)Enum.Parse(typeof(ST_AlgClass), node.Attributes["w:cryptAlgorithmClass"].Value);
			}
			if (node.Attributes["w:cryptAlgorithmType"] != null)
			{
				cT_DocProtect.cryptAlgorithmType = (ST_AlgType)Enum.Parse(typeof(ST_AlgType), node.Attributes["w:cryptAlgorithmType"].Value);
			}
			cT_DocProtect.cryptAlgorithmSid = XmlHelper.ReadString(node.Attributes["w:cryptAlgorithmSid"]);
			cT_DocProtect.cryptSpinCount = XmlHelper.ReadString(node.Attributes["w:cryptSpinCount"]);
			cT_DocProtect.cryptProvider = XmlHelper.ReadString(node.Attributes["w:cryptProvider"]);
			cT_DocProtect.algIdExt = XmlHelper.ReadBytes(node.Attributes["w:algIdExt"]);
			cT_DocProtect.algIdExtSource = XmlHelper.ReadString(node.Attributes["w:algIdExtSource"]);
			cT_DocProtect.cryptProviderTypeExt = XmlHelper.ReadBytes(node.Attributes["w:cryptProviderTypeExt"]);
			cT_DocProtect.cryptProviderTypeExtSource = XmlHelper.ReadString(node.Attributes["w:cryptProviderTypeExtSource"]);
			cT_DocProtect.hash = XmlHelper.ReadString(node.Attributes["w:hash"]);
			cT_DocProtect.salt = XmlHelper.ReadString(node.Attributes["w:salt"]);
			return cT_DocProtect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:edit", edit.ToString());
			XmlHelper.WriteAttribute(sw, "w:formatting", formatting.ToString());
			XmlHelper.WriteAttribute(sw, "w:enforcement", enforcement.ToString());
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
			sw.Write("/>");
		}
	}
}
