using NPOI.Util;
using System;
using System.IO;

namespace NPOI.HPSF
{
	/// <summary>
	/// Class for writing little-endian data and more.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2003-02-20 
	/// </summary>
	public class TypeWriter
	{
		/// <summary>
		/// Writes a two-byte value (short) To an output stream.
		/// </summary>
		/// <param name="out1">The stream To Write To..</param>
		/// <param name="n">The number of bytes that have been written.</param>
		/// <returns></returns>
		public static int WriteToStream(Stream out1, short n)
		{
			LittleEndian.PutShort(out1, n);
			return 2;
		}

		/// Writes a four-byte value To an output stream.
		///
		/// @param out The stream To Write To.
		/// @param n The value To Write.
		/// @exception IOException if an I/O error occurs
		/// @return The number of bytes written To the output stream. 
		public static int WriteToStream(Stream out1, int n)
		{
			LittleEndian.PutInt(n, out1);
			return 4;
		}

		/// Writes a four-byte value To an output stream.
		///
		/// @param out The stream To Write To.
		/// @param n The value To Write.
		/// @exception IOException if an I/O error occurs
		/// @return The number of bytes written To the output stream. 
		[Obsolete]
		public static int WriteToStream(Stream out1, uint n)
		{
			int num = 4;
			byte[] array = new byte[num];
			LittleEndian.PutUInt(array, 0, n);
			out1.Write(array, 0, num);
			return num;
		}

		/// Writes a eight-byte value To an output stream.
		///
		/// @param out The stream To Write To.
		/// @param n The value To Write.
		/// @exception IOException if an I/O error occurs
		/// @return The number of bytes written To the output stream. 
		public static int WriteToStream(Stream out1, long n)
		{
			LittleEndian.PutLong(n, out1);
			return 8;
		}

		/// Writes an unsigned two-byte value To an output stream.
		///
		/// @param out The stream To Write To
		/// @param n The value To Write
		/// @exception IOException if an I/O error occurs
		public static void WriteUShortToStream(Stream out1, int n)
		{
			if ((n & -65536) != 0)
			{
				throw new IllegalPropertySetDataException("Value " + n + " cannot be represented by 2 bytes.");
			}
			LittleEndian.PutUShort(n, out1);
		}

		/// Writes an unsigned four-byte value To an output stream.
		///
		/// @param out The stream To Write To.
		/// @param n The value To Write.
		/// @return The number of bytes that have been written To the output stream.
		/// @exception IOException if an I/O error occurs
		public static int WriteUIntToStream(Stream out1, uint n)
		{
			ulong num = (ulong)(n & -4294967296L);
			if (num != 0 && num != 18446744069414584320uL)
			{
				throw new IllegalPropertySetDataException("Value " + n + " cannot be represented by 4 bytes.");
			}
			LittleEndian.PutUInt(n, out1);
			return 4;
		}

		/// Writes a 16-byte {@link ClassID} To an output stream.
		///
		/// @param out The stream To Write To
		/// @param n The value To Write
		/// @return The number of bytes written
		/// @exception IOException if an I/O error occurs
		public static int WriteToStream(Stream out1, ClassID n)
		{
			byte[] array = new byte[16];
			n.Write(array, 0);
			out1.Write(array, 0, array.Length);
			return array.Length;
		}

		/// Writes an array of {@link Property} instances To an output stream
		/// according To the Horrible Property  Format.
		///
		/// @param out The stream To Write To
		/// @param properties The array To Write To the stream
		/// @param codepage The codepage number To use for writing strings
		/// @exception IOException if an I/O error occurs
		/// @throws UnsupportedVariantTypeException if HPSF does not support some
		///         variant type.
		public static void WriteToStream(Stream out1, Property[] properties, int codepage)
		{
			if (properties != null)
			{
				foreach (Property property in properties)
				{
					WriteUIntToStream(out1, (uint)property.ID);
					WriteUIntToStream(out1, (uint)property.Count);
				}
				foreach (Property property2 in properties)
				{
					long type = property2.Type;
					WriteUIntToStream(out1, (uint)type);
					VariantSupport.Write(out1, (int)type, property2.Value, codepage);
				}
			}
		}

		/// Writes a double value value To an output stream.
		///
		/// @param out The stream To Write To.
		/// @param n The value To Write.
		/// @exception IOException if an I/O error occurs
		/// @return The number of bytes written To the output stream. 
		public static int WriteToStream(Stream out1, double n)
		{
			LittleEndian.PutDouble(n, out1);
			return 8;
		}
	}
}
