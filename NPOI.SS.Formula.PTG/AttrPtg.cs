using NPOI.Util;
using System;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// "Special Attributes"
	/// This seems to be a Misc Stuff and Junk record.  One function it serves Is
	/// in SUM functions (i.e. SUM(A1:A3) causes an area PTG then an ATTR with the SUM option Set)
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class AttrPtg : ControlPtg
	{
		public enum SpaceType
		{
			/// 00H = Spaces before the next token (not allowed before tParen token) 
			SpaceBefore,
			/// 01H = Carriage returns before the next token (not allowed before tParen token) 
			CrBefore,
			/// 02H = Spaces before opening parenthesis (only allowed before tParen token) 
			SpaceBeforeOpenParen,
			/// 03H = Carriage returns before opening parenthesis (only allowed before tParen token) 
			CrBeforeOpenParen,
			/// 04H = Spaces before closing parenthesis (only allowed before tParen, tFunc, and tFuncVar tokens) 
			SpaceBeforeCloseParen,
			/// 05H = Carriage returns before closing parenthesis (only allowed before tParen, tFunc, and tFuncVar tokens) 
			CrBeforeCloseParen,
			/// 06H = Spaces following the equality sign (only in macro sheets) 
			SpaceAfterEquality
		}

		public const byte sid = 25;

		private const int SIZE = 4;

		private byte field_1_options;

		private short field_2_data;

		/// only used for tAttrChoose: table of offsets to starts of args 
		private int[] _jumpTable;

		/// only used for tAttrChoose: offset to the tFuncVar for CHOOSE() 
		private int _chooseFuncOffset;

		private static BitField semiVolatile = BitFieldFactory.GetInstance(1);

		private static BitField optiIf = BitFieldFactory.GetInstance(2);

		private static BitField optiChoose = BitFieldFactory.GetInstance(4);

		private static BitField optiSkip = BitFieldFactory.GetInstance(8);

		private static BitField optiSum = BitFieldFactory.GetInstance(16);

		private static BitField baxcel = BitFieldFactory.GetInstance(32);

		private static BitField space = BitFieldFactory.GetInstance(64);

		public static readonly AttrPtg SUM = new AttrPtg(16, 0, null, -1);

		public bool IsSemiVolatile => semiVolatile.IsSet(field_1_options);

		public bool IsOptimizedIf
		{
			get
			{
				return optiIf.IsSet(field_1_options);
			}
			set
			{
				field_1_options = optiIf.SetByteBoolean(field_1_options, value);
			}
		}

		public bool IsOptimizedChoose => optiChoose.IsSet(field_1_options);

		public bool IsSum
		{
			get
			{
				return optiSum.IsSet(field_1_options);
			}
			set
			{
				field_1_options = optiSum.SetByteBoolean(field_1_options, value);
			}
		}

		public bool IsBaxcel => baxcel.IsSet(field_1_options);

		public bool IsSpace => space.IsSet(field_1_options);

		public bool IsSkip => optiSkip.IsSet(field_1_options);

		public short Data
		{
			get
			{
				return field_2_data;
			}
			set
			{
				field_2_data = value;
			}
		}

		public int[] JumpTable => (int[])_jumpTable.Clone();

		public int ChooseFuncOffset
		{
			get
			{
				if (_jumpTable == null)
				{
					throw new InvalidOperationException("Not tAttrChoose");
				}
				return _chooseFuncOffset;
			}
		}

		public override int Size
		{
			get
			{
				if (_jumpTable != null)
				{
					return 4 + (_jumpTable.Length + 1) * 2;
				}
				return 4;
			}
		}

		public int NumberOfOperands => 1;

		public int Type => -1;

		public AttrPtg()
		{
			_jumpTable = null;
			_chooseFuncOffset = -1;
		}

		public AttrPtg(ILittleEndianInput in1)
		{
			field_1_options = (byte)in1.ReadByte();
			field_2_data = in1.ReadShort();
			if (IsOptimizedChoose)
			{
				int num = field_2_data;
				int[] array = new int[num];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = in1.ReadUShort();
				}
				_jumpTable = array;
				_chooseFuncOffset = in1.ReadUShort();
			}
			else
			{
				_jumpTable = null;
				_chooseFuncOffset = -1;
			}
		}

		private AttrPtg(int options, int data, int[] jt, int chooseFuncOffset)
		{
			field_1_options = (byte)options;
			field_2_data = (short)data;
			_jumpTable = jt;
			_chooseFuncOffset = chooseFuncOffset;
		}

		/// <summary>
		/// Creates the space.
		/// </summary>
		/// <param name="type">a constant from SpaceType</param>
		/// <param name="count">The count.</param>
		public static AttrPtg CreateSpace(SpaceType type, int count)
		{
			int data = (int)(type & (SpaceType)255) | ((count << 8) & 0xFFFF);
			return new AttrPtg(space.Set(0), data, null, -1);
		}

		/// <summary>
		/// Creates if.
		/// </summary>
		/// <param name="dist">distance (in bytes) to start of either
		/// tFuncVar(IF) token (when false parameter is not present).</param>
		public static AttrPtg CreateIf(int dist)
		{
			return new AttrPtg(optiIf.Set(0), dist, null, -1);
		}

		/// <summary>
		/// Creates the skip.
		/// </summary>
		/// <param name="dist">distance (in bytes) to position behind tFuncVar(IF) token (minus 1).</param>
		public static AttrPtg CreateSkip(int dist)
		{
			return new AttrPtg(optiSkip.Set(0), dist, null, -1);
		}

		public static AttrPtg GetSumSingle()
		{
			return new AttrPtg(optiSum.Set(0), 0, null, -1);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			if (IsSemiVolatile)
			{
				stringBuilder.Append("volatile ");
			}
			if (IsSpace)
			{
				stringBuilder.Append("space count=").Append((field_2_data >> 8) & 0xFF);
				stringBuilder.Append(" type=").Append(field_2_data & 0xFF).Append(" ");
			}
			if (IsOptimizedIf)
			{
				stringBuilder.Append("if dist=").Append(Data);
			}
			else if (IsOptimizedChoose)
			{
				stringBuilder.Append("choose nCases=").Append(Data);
			}
			else if (IsSkip)
			{
				stringBuilder.Append("skip dist=").Append(Data);
			}
			else if (IsSum)
			{
				stringBuilder.Append("sum ");
			}
			else if (IsBaxcel)
			{
				stringBuilder.Append("assign ");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(25 + base.PtgClass);
			out1.WriteByte(field_1_options);
			out1.WriteShort(field_2_data);
			int[] jumpTable = _jumpTable;
			if (jumpTable != null)
			{
				for (int i = 0; i < jumpTable.Length; i++)
				{
					out1.WriteShort(jumpTable[i]);
				}
				out1.WriteShort(_chooseFuncOffset);
			}
		}

		public string ToFormulaString(string[] operands)
		{
			if (space.IsSet(field_1_options))
			{
				return operands[0];
			}
			if (optiIf.IsSet(field_1_options))
			{
				return ToFormulaString() + "(" + operands[0] + ")";
			}
			if (optiSkip.IsSet(field_1_options))
			{
				return ToFormulaString() + operands[0];
			}
			return ToFormulaString() + "(" + operands[0] + ")";
		}

		public override string ToFormulaString()
		{
			if (semiVolatile.IsSet(field_1_options))
			{
				return "ATTR(semiVolatile)";
			}
			if (optiIf.IsSet(field_1_options))
			{
				return "IF";
			}
			if (optiChoose.IsSet(field_1_options))
			{
				return "CHOOSE";
			}
			if (optiSkip.IsSet(field_1_options))
			{
				return "";
			}
			if (optiSum.IsSet(field_1_options))
			{
				return "SUM";
			}
			if (baxcel.IsSet(field_1_options))
			{
				return "ATTR(baxcel)";
			}
			if (space.IsSet(field_1_options))
			{
				return "";
			}
			return "UNKNOWN ATTRIBUTE";
		}

		public override object Clone()
		{
			return new AttrPtg(jt: (_jumpTable != null) ? ((int[])_jumpTable.Clone()) : null, options: field_1_options, data: field_2_data, chooseFuncOffset: _chooseFuncOffset);
		}
	}
}
