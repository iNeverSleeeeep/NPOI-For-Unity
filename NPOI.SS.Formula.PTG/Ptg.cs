using NPOI.Util;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NPOI.SS.Formula.PTG
{
	/// <c>Ptg</c> represents a syntactic token in a formula.  'PTG' is an acronym for 
	/// '<b>p</b>arse <b>t</b>hin<b>g</b>'.  Originally, the name referred to the single 
	/// byte identifier at the start of the token, but in POI, <c>Ptg</c> encapsulates
	/// the whole formula token (initial byte + value data).
	///
	///
	/// <c>Ptg</c>s are logically arranged in a tree representing the structure of the
	/// Parsed formula.  However, in BIFF files <c>Ptg</c>s are written/Read in 
	/// <em>Reverse-Polish Notation</em> order. The RPN ordering also simplifies formula
	/// evaluation logic, so POI mostly accesses <c>Ptg</c>s in the same way.
	///
	/// @author  andy
	/// @author avik
	/// @author Jason Height (jheight at chariot dot net dot au)
	[Serializable]
	public abstract class Ptg : ICloneable
	{
		public const byte CLASS_REF = 0;

		public const byte CLASS_VALUE = 32;

		public const byte CLASS_ARRAY = 64;

		public static Ptg[] EMPTY_PTG_ARRAY = new Ptg[0];

		private byte ptgClass;

		/// @return the encoded Length of this Ptg, including the initial Ptg type identifier byte. 
		public abstract int Size
		{
			get;
		}

		/// @return <c>false</c> if this token is classified as 'reference', 'value', or 'array'
		public abstract bool IsBaseToken
		{
			get;
		}

		/// @return the 'operand class' (REF/VALUE/ARRAY) for this Ptg
		public byte PtgClass
		{
			get
			{
				return ptgClass;
			}
			set
			{
				if (IsBaseToken)
				{
					throw new Exception("SetClass should not be called on a base token");
				}
				ptgClass = value;
			}
		}

		public abstract byte DefaultOperandClass
		{
			get;
		}

		/// Debug / diagnostic method to get this token's 'operand class' type.
		/// @return 'R' for 'reference', 'V' for 'value', 'A' for 'array' and '.' for base tokens
		public char RVAType
		{
			get
			{
				if (!IsBaseToken)
				{
					switch (ptgClass)
					{
					case 0:
						return 'R';
					case 32:
						return 'V';
					case 64:
						return 'A';
					default:
						throw new InvalidOperationException("Unknown operand class (" + ptgClass + ")");
					}
				}
				return '.';
			}
		}

		/// Reads <c>size</c> bytes of the input stream, to Create an array of <c>Ptg</c>s.
		/// Extra data (beyond <c>size</c>) may be Read if and <c>ArrayPtg</c>s are present.
		public static Ptg[] ReadTokens(int size, ILittleEndianInput in1)
		{
			ArrayList arrayList = new ArrayList(4 + size / 2);
			int num = 0;
			bool flag = false;
			while (num < size)
			{
				Ptg ptg = CreatePtg(in1);
				if (ptg is ArrayPtg.Initial)
				{
					flag = true;
				}
				num += ptg.Size;
				arrayList.Add(ptg);
			}
			if (num != size)
			{
				throw new Exception("Ptg array size mismatch");
			}
			if (flag)
			{
				Ptg[] array = ToPtgArray(arrayList);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] is ArrayPtg.Initial)
					{
						array[i] = ((ArrayPtg.Initial)array[i]).FinishReading(in1);
					}
				}
				return array;
			}
			return ToPtgArray(arrayList);
		}

		public static Ptg CreatePtg(ILittleEndianInput in1)
		{
			byte b = (byte)in1.ReadByte();
			if (b < 32)
			{
				return CreateBasePtg(b, in1);
			}
			Ptg ptg = CreateClassifiedPtg(b, in1);
			if (b >= 96)
			{
				ptg.PtgClass = 64;
			}
			else if (b >= 64)
			{
				ptg.PtgClass = 32;
			}
			else
			{
				ptg.PtgClass = 0;
			}
			return ptg;
		}

		private static Ptg CreateClassifiedPtg(byte id, ILittleEndianInput in1)
		{
			switch ((id & 0x1F) | 0x20)
			{
			case 32:
				return new ArrayPtg.Initial(in1);
			case 33:
				return FuncPtg.Create(in1);
			case 34:
				return FuncVarPtg.Create(in1);
			case 35:
				return new NamePtg(in1);
			case 36:
				return new RefPtg(in1);
			case 37:
				return new AreaPtg(in1);
			case 38:
				return new MemAreaPtg(in1);
			case 39:
				return new MemErrPtg(in1);
			case 41:
				return new MemFuncPtg(in1);
			case 42:
				return new RefErrorPtg(in1);
			case 43:
				return new AreaErrPtg(in1);
			case 44:
				return new RefNPtg(in1);
			case 45:
				return new AreaNPtg(in1);
			case 57:
				return new NameXPtg(in1);
			case 58:
				return new Ref3DPtg(in1);
			case 59:
				return new Area3DPtg(in1);
			case 60:
				return new DeletedRef3DPtg(in1);
			case 61:
				return new DeletedArea3DPtg(in1);
			default:
				throw new NotSupportedException(" Unknown Ptg in Formula: 0x" + StringUtil.ToHexString(id) + " (" + (int)id + ")");
			}
		}

		private static Ptg CreateBasePtg(byte id, ILittleEndianInput in1)
		{
			switch (id)
			{
			case 0:
				return new UnknownPtg();
			case 1:
				return new ExpPtg(in1);
			case 2:
				return new TblPtg(in1);
			case 3:
				return AddPtg.instance;
			case 4:
				return SubtractPtg.instance;
			case 5:
				return MultiplyPtg.instance;
			case 6:
				return DividePtg.instance;
			case 7:
				return PowerPtg.instance;
			case 8:
				return ConcatPtg.instance;
			case 9:
				return LessThanPtg.instance;
			case 10:
				return LessEqualPtg.instance;
			case 11:
				return EqualPtg.instance;
			case 12:
				return GreaterEqualPtg.instance;
			case 13:
				return GreaterThanPtg.instance;
			case 14:
				return NotEqualPtg.instance;
			case 15:
				return IntersectionPtg.instance;
			case 16:
				return UnionPtg.instance;
			case 17:
				return RangePtg.instance;
			case 18:
				return UnaryPlusPtg.instance;
			case 19:
				return UnaryMinusPtg.instance;
			case 20:
				return PercentPtg.instance;
			case 21:
				return ParenthesisPtg.instance;
			case 22:
				return MissingArgPtg.instance;
			case 23:
				return new StringPtg(in1);
			case 25:
				return new AttrPtg(in1);
			case 28:
				return new ErrPtg(in1);
			case 29:
				return new BoolPtg(in1);
			case 30:
				return new IntPtg(in1);
			case 31:
				return new NumberPtg(in1);
			default:
				throw new Exception("Unexpected base token id (" + id + ")");
			}
		}

		private static Ptg[] ToPtgArray(ArrayList l)
		{
			if (l.Count == 0)
			{
				return EMPTY_PTG_ARRAY;
			}
			return (Ptg[])l.ToArray(typeof(Ptg));
		}

		/// @return a distinct copy of this <c>Ptg</c> if the class is mutable, or the same instance
		/// if the class is immutable.
		public virtual object Clone()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, this);
				memoryStream.Position = 0L;
				return binaryFormatter.Deserialize(memoryStream);
			}
		}

		/// This method will return the same result as {@link #getEncodedSizeWithoutArrayData(Ptg[])}
		/// if there are no array tokens present.
		/// @return the full size taken to encode the specified <c>Ptg</c>s
		public static int GetEncodedSize(Ptg[] ptgs)
		{
			int num = 0;
			for (int i = 0; i < ptgs.Length; i++)
			{
				num += ptgs[i].Size;
			}
			return num;
		}

		/// Used to calculate value that should be encoded at the start of the encoded Ptg token array;
		/// @return the size of the encoded Ptg tokens not including any trailing array data.
		public static int GetEncodedSizeWithoutArrayData(Ptg[] ptgs)
		{
			int num = 0;
			foreach (Ptg ptg in ptgs)
			{
				num = ((!(ptg is ArrayPtg)) ? (num + ptg.Size) : (num + 8));
			}
			return num;
		}

		/// Writes the ptgs to the data buffer, starting at the specified offset.  
		///
		/// <br />
		/// The 2 byte encode Length field is <b>not</b> written by this method.
		/// @return number of bytes written
		public static int SerializePtgs(Ptg[] ptgs, byte[] array, int offset)
		{
			int num = ptgs.Length;
			LittleEndianByteArrayOutputStream littleEndianByteArrayOutputStream = new LittleEndianByteArrayOutputStream(array, offset);
			ArrayList arrayList = null;
			for (int i = 0; i < num; i++)
			{
				Ptg ptg = ptgs[i];
				ptg.Write(littleEndianByteArrayOutputStream);
				if (ptg is ArrayPtg)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList(5);
					}
					arrayList.Add(ptg);
				}
			}
			if (arrayList != null)
			{
				for (int j = 0; j < arrayList.Count; j++)
				{
					ArrayPtg arrayPtg = (ArrayPtg)arrayList[j];
					arrayPtg.WriteTokenValueBytes(littleEndianByteArrayOutputStream);
				}
			}
			return littleEndianByteArrayOutputStream.WriteIndex - offset;
		}

		/// Write this Ptg to a byte array
		public abstract void Write(ILittleEndianOutput out1);

		/// return a string representation of this token alone
		public abstract string ToFormulaString();

		/// Overridden toString method to Ensure object hash is not printed.
		///  This helps Get rid of gratuitous diffs when comparing two dumps
		///  Subclasses may output more relevant information by overriding this method
		public override string ToString()
		{
			return GetType().ToString();
		}

		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		public static bool DoesFormulaReferToDeletedCell(Ptg[] ptgs)
		{
			for (int i = 0; i < ptgs.Length; i++)
			{
				if (IsDeletedCellRef(ptgs[i]))
				{
					return true;
				}
			}
			return false;
		}

		private static bool IsDeletedCellRef(Ptg ptg)
		{
			if (ptg == ErrPtg.REF_INVALID)
			{
				return true;
			}
			if (ptg is DeletedArea3DPtg)
			{
				return true;
			}
			if (ptg is DeletedRef3DPtg)
			{
				return true;
			}
			if (ptg is AreaErrPtg)
			{
				return true;
			}
			if (ptg is RefErrorPtg)
			{
				return true;
			}
			return false;
		}
	}
}
