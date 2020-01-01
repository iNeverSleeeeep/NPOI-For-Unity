using NPOI.Util;
using System;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// The opt record is used to store property values for a shape.  It is the key to determining
	/// the attributes of a shape.  Properties can be of two types: simple or complex.  Simple types
	/// are fixed Length.  Complex properties are variable Length.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherOptRecord : AbstractEscherOptRecord
	{
		public const short RECORD_ID = -4085;

		public const string RECORD_DESCRIPTION = "msofbtOPT";

		public override short Instance
		{
			get
			{
				Instance = (short)properties.Count;
				return base.Instance;
			}
		}

		/// <summary>
		/// Automatically recalculate the correct option
		/// </summary>
		/// <value></value>
		internal override short Options
		{
			get
			{
				short instance = Instance;
				short version = Version;
				return base.Options;
			}
		}

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Opt";

		public override short Version
		{
			get
			{
				Version = 3;
				return base.Version;
			}
			set
			{
				if (value != 3)
				{
					throw new ArgumentException("msofbtOPT can have only '0x3' version");
				}
				base.Version = value;
			}
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance)));
			foreach (EscherProperty escherProperty in base.EscherProperties)
			{
				stringBuilder.Append(escherProperty.ToXml(tab + "\t"));
			}
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
