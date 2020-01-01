using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using NPOI.Util;

namespace NPOI.SS.Formula
{
	/// Encapsulates an encoded formula token array. 
	///
	/// @author Josh Micich
	public class Formula
	{
		private static readonly Formula EMPTY = new Formula(new byte[0], 0);

		/// immutable 
		private byte[] _byteEncoding;

		private int _encodedTokenLen;

		public Ptg[] Tokens
		{
			get
			{
				ILittleEndianInput @in = new LittleEndianByteArrayInputStream(_byteEncoding);
				return Ptg.ReadTokens(_encodedTokenLen, @in);
			}
		}

		/// @return total formula encoding length.  The formula encoding includes:
		/// <ul>
		/// <li>ushort tokenDataLen</li>
		/// <li>tokenData</li>
		/// <li>arrayConstantData (optional)</li>
		/// </ul>
		/// Note - this value is different to <c>tokenDataLength</c>
		public int EncodedSize => 2 + _byteEncoding.Length;

		/// This method is often used when the formula length does not appear immediately before
		/// the encoded token data.
		///
		/// @return the encoded length of the plain formula tokens.  This does <em>not</em> include
		/// the leading ushort field, nor any trailing array constant data.
		public int EncodedTokenSize => _encodedTokenLen;

		/// Gets the locator for the corresponding {@link SharedFormulaRecord}, {@link ArrayRecord} or
		/// {@link TableRecord} if this formula belongs to such a grouping.  The {@link CellReference}
		/// returned by this method will  match the top left corner of the range of that grouping. 
		/// The return value is usually not the same as the location of the cell containing this formula.
		///
		/// @return the firstRow &amp; firstColumn of an array formula or shared formula that this formula
		/// belongs to.  <code>null</code> if this formula is not part of an array or shared formula.
		public CellReference ExpReference
		{
			get
			{
				byte[] byteEncoding = _byteEncoding;
				if (byteEncoding.Length == 5)
				{
					switch (byteEncoding[0])
					{
					default:
						return null;
					case 1:
					case 2:
					{
						int uShort = LittleEndian.GetUShort(byteEncoding, 1);
						int uShort2 = LittleEndian.GetUShort(byteEncoding, 3);
						return new CellReference(uShort, uShort2);
					}
					}
				}
				return null;
			}
		}

		private Formula(byte[] byteEncoding, int encodedTokenLen)
		{
			_byteEncoding = byteEncoding;
			_encodedTokenLen = encodedTokenLen;
		}

		/// Convenience method for {@link #read(int, LittleEndianInput, int)}
		public static Formula Read(int encodedTokenLen, ILittleEndianInput in1)
		{
			return Read(encodedTokenLen, in1, encodedTokenLen);
		}

		/// When there are no array constants present, <c>encodedTokenLen</c>==<c>totalEncodedLen</c>
		/// @param encodedTokenLen number of bytes in the stream taken by the plain formula tokens
		/// @param totalEncodedLen the total number of bytes in the formula (includes trailing encoding
		/// for array constants, but does not include 2 bytes for initial <c>ushort encodedTokenLen</c> field.
		/// @return A new formula object as read from the stream.  Possibly empty, never <code>null</code>.
		public static Formula Read(int encodedTokenLen, ILittleEndianInput in1, int totalEncodedLen)
		{
			byte[] array = new byte[totalEncodedLen];
			in1.ReadFully(array);
			return new Formula(array, encodedTokenLen);
		}

		/// Writes  The formula encoding is includes:
		/// <ul>
		/// <li>ushort tokenDataLen</li>
		/// <li>tokenData</li>
		/// <li>arrayConstantData (if present)</li>
		/// </ul>
		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_encodedTokenLen);
			out1.Write(_byteEncoding);
		}

		public void SerializeTokens(ILittleEndianOutput out1)
		{
			out1.Write(_byteEncoding, 0, _encodedTokenLen);
		}

		public void SerializeArrayConstantData(ILittleEndianOutput out1)
		{
			int len = _byteEncoding.Length - _encodedTokenLen;
			out1.Write(_byteEncoding, _encodedTokenLen, len);
		}

		/// Creates a {@link Formula} object from a supplied {@link Ptg} array. 
		/// Handles <code>null</code>s OK.
		/// @param ptgs may be <code>null</code>
		/// @return Never <code>null</code> (Possibly empty if the supplied <c>ptgs</c> is <code>null</code>)
		public static Formula Create(Ptg[] ptgs)
		{
			if (ptgs == null || ptgs.Length < 1)
			{
				return EMPTY;
			}
			int encodedSize = Ptg.GetEncodedSize(ptgs);
			byte[] array = new byte[encodedSize];
			Ptg.SerializePtgs(ptgs, array, 0);
			int encodedSizeWithoutArrayData = Ptg.GetEncodedSizeWithoutArrayData(ptgs);
			return new Formula(array, encodedSizeWithoutArrayData);
		}

		/// Gets the {@link Ptg} array from the supplied {@link Formula}. 
		/// Handles <code>null</code>s OK.
		///
		/// @param formula may be <code>null</code>
		/// @return possibly <code>null</code> (if the supplied <c>formula</c> is <code>null</code>)
		public static Ptg[] GetTokens(Formula formula)
		{
			return formula?.Tokens;
		}

		public Formula Copy()
		{
			return this;
		}

		public bool IsSame(Formula other)
		{
			return Arrays.Equals(_byteEncoding, other._byteEncoding);
		}
	}
}
