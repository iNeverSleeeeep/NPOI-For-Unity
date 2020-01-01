using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        WSBool Record.
	/// Description:  stores workbook Settings  (aka its a big "everything we didn't
	///               put somewhere else")
	/// REFERENCE:  PG 425 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Glen Stampoultzis (gstamp@iprimus.com.au)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class WSBoolRecord : StandardRecord
	{
		public const short sid = 129;

		private byte field_1_wsbool;

		private byte field_2_wsbool;

		private static BitField autobreaks = BitFieldFactory.GetInstance(1);

		private static BitField dialog = BitFieldFactory.GetInstance(16);

		private static BitField applystyles = BitFieldFactory.GetInstance(32);

		private static BitField rowsumsbelow = BitFieldFactory.GetInstance(64);

		private static BitField rowsumsright = BitFieldFactory.GetInstance(128);

		private static BitField fittopage = BitFieldFactory.GetInstance(1);

		private static BitField Displayguts = BitFieldFactory.GetInstance(6);

		private static BitField alternateexpression = BitFieldFactory.GetInstance(64);

		private static BitField alternateformula = BitFieldFactory.GetInstance(128);

		/// Get first byte (see bit Getters)
		public byte WSBool1
		{
			get
			{
				return field_1_wsbool;
			}
			set
			{
				field_1_wsbool = value;
			}
		}

		/// <summary>
		/// Whether to show automatic page breaks or not
		/// </summary>
		public bool Autobreaks
		{
			get
			{
				return autobreaks.IsSet(field_1_wsbool);
			}
			set
			{
				field_1_wsbool = autobreaks.SetByteBoolean(field_1_wsbool, value);
			}
		}

		/// <summary>
		/// Whether sheet is a dialog sheet or not
		/// </summary>
		public bool Dialog
		{
			get
			{
				return dialog.IsSet(field_1_wsbool);
			}
			set
			{
				field_1_wsbool = dialog.SetByteBoolean(field_1_wsbool, value);
			}
		}

		/// <summary>
		/// Get if row summaries appear below detail in the outline
		/// </summary>
		public bool RowSumsBelow
		{
			get
			{
				return rowsumsbelow.IsSet(field_1_wsbool);
			}
			set
			{
				field_1_wsbool = rowsumsbelow.SetByteBoolean(field_1_wsbool, value);
			}
		}

		/// <summary>
		/// Get if col summaries appear right of the detail in the outline
		/// </summary>
		public bool RowSumsRight
		{
			get
			{
				return rowsumsright.IsSet(field_1_wsbool);
			}
			set
			{
				field_1_wsbool = rowsumsright.SetByteBoolean(field_1_wsbool, value);
			}
		}

		/// <summary>
		/// Get the second byte (see bit Getters)
		/// </summary>
		public byte WSBool2
		{
			get
			{
				return field_2_wsbool;
			}
			set
			{
				field_2_wsbool = value;
			}
		}

		/// <summary>
		/// fit to page option is on
		/// </summary>
		public bool FitToPage
		{
			get
			{
				return fittopage.IsSet(field_2_wsbool);
			}
			set
			{
				field_2_wsbool = fittopage.SetByteBoolean(field_2_wsbool, value);
			}
		}

		/// <summary>
		/// Whether to display the guts or not
		/// </summary>
		public bool DisplayGuts
		{
			get
			{
				return Displayguts.IsSet(field_2_wsbool);
			}
			set
			{
				field_2_wsbool = Displayguts.SetByteBoolean(field_2_wsbool, value);
			}
		}

		/// <summary>
		/// whether alternate expression evaluation is on
		/// </summary>
		public bool AlternateExpression
		{
			get
			{
				return alternateexpression.IsSet(field_2_wsbool);
			}
			set
			{
				field_2_wsbool = alternateexpression.SetByteBoolean(field_2_wsbool, value);
			}
		}

		/// <summary>
		/// whether alternative formula entry is on
		/// </summary>
		public bool AlternateFormula
		{
			get
			{
				return alternateformula.IsSet(field_2_wsbool);
			}
			set
			{
				field_2_wsbool = alternateformula.SetByteBoolean(field_2_wsbool, value);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 129;

		public WSBoolRecord()
		{
		}

		/// Constructs a WSBool record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public WSBoolRecord(RecordInputStream in1)
		{
			byte[] array = in1.ReadRemainder();
			field_1_wsbool = array[0];
			field_2_wsbool = array[1];
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WSBOOL]\n");
			stringBuilder.Append("    .wsbool1        = ").Append(StringUtil.ToHexString(WSBool1)).Append("\n");
			stringBuilder.Append("        .autobreaks = ").Append(Autobreaks).Append("\n");
			stringBuilder.Append("        .dialog     = ").Append(Dialog).Append("\n");
			stringBuilder.Append("        .rowsumsbelw= ").Append(RowSumsBelow).Append("\n");
			stringBuilder.Append("        .rowsumsrigt= ").Append(RowSumsRight).Append("\n");
			stringBuilder.Append("    .wsbool2        = ").Append(StringUtil.ToHexString(WSBool2)).Append("\n");
			stringBuilder.Append("        .fittopage  = ").Append(FitToPage).Append("\n");
			stringBuilder.Append("        .Displayguts= ").Append(DisplayGuts).Append("\n");
			stringBuilder.Append("        .alternateex= ").Append(AlternateExpression).Append("\n");
			stringBuilder.Append("        .alternatefo= ").Append(AlternateFormula).Append("\n");
			stringBuilder.Append("[/WSBOOL]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(WSBool1);
			out1.WriteByte(WSBool2);
		}

		public override object Clone()
		{
			WSBoolRecord wSBoolRecord = new WSBoolRecord();
			wSBoolRecord.field_1_wsbool = field_1_wsbool;
			wSBoolRecord.field_2_wsbool = field_2_wsbool;
			return wSBoolRecord;
		}
	}
}
