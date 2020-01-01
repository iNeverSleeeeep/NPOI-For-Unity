using System;
using System.Text;

namespace NPOI.Util
{
	/// <summary>
	/// Represents a class ID (16 bytes). Unlike other little-endian
	/// type the {@link ClassID} is not just 16 bytes stored in the wrong
	/// order. Instead, it is a double word (4 bytes) followed by two
	/// words (2 bytes each) followed by 8 bytes.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">klute@rainer-klute.de</a>
	/// @version $Id: ClassID.java 489730 2006-12-22 19:18:16Z bayard $
	/// @since 2002-02-09
	/// </summary>
	public class ClassID
	{
		/// The number of bytes occupied by this object in the byte
		/// stream. 
		public const int LENGTH = 16;

		/// The bytes making out the class ID in correct order,
		/// i.e. big-endian.
		protected byte[] bytes;

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>The number of bytes occupied by this object in the byte stream.</value>
		public int Length => 16;

		/// <summary>
		/// Gets or sets the bytes making out the class ID. They are returned in correct order, i.e. big-endian.
		/// </summary>
		/// <value>the bytes making out the class ID..</value>
		public byte[] Bytes
		{
			get
			{
				return bytes;
			}
			set
			{
				for (int i = 0; i < bytes.Length; i++)
				{
					bytes[i] = value[i];
				}
			}
		}

		/// <summary>
		/// Creates a <see cref="T:NPOI.Util.ClassID" /> and Reads its value from a byte array.
		/// </summary>
		/// <param name="src">The byte array to Read from.</param>
		/// <param name="offset">The offset of the first byte to Read.</param>
		public ClassID(byte[] src, int offset)
		{
			Read(src, offset);
		}

		/// <summary>
		/// Creates a <see cref="T:NPOI.Util.ClassID" /> and initializes its value with 0x00 bytes.
		/// </summary>
		public ClassID()
		{
			bytes = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				bytes[i] = 0;
			}
		}

		/// <summary>
		/// Reads the class ID's value from a byte array by turning little-endian into big-endian.
		/// </summary>
		/// <param name="src">The byte array to Read from</param>
		/// <param name="offset">The offset within the </param>
		/// <returns>A byte array containing the class ID.</returns>
		public byte[] Read(byte[] src, int offset)
		{
			bytes = new byte[16];
			bytes[0] = src[3 + offset];
			bytes[1] = src[2 + offset];
			bytes[2] = src[1 + offset];
			bytes[3] = src[offset];
			bytes[4] = src[5 + offset];
			bytes[5] = src[4 + offset];
			bytes[6] = src[7 + offset];
			bytes[7] = src[6 + offset];
			for (int i = 8; i < 16; i++)
			{
				bytes[i] = src[i + offset];
			}
			return bytes;
		}

		/// <summary>
		/// Writes the class ID to a byte array in the little-endian format.
		/// </summary>
		/// <param name="dst">The byte array to Write to.</param>
		/// <param name="offset">The offset within the </param>
		public void Write(byte[] dst, int offset)
		{
			if (dst.Length < 16)
			{
				throw new ArrayTypeMismatchException("Destination byte[] must have room for at least 16 bytes, but has a length of only " + dst.Length + ".");
			}
			dst[offset] = bytes[3];
			dst[1 + offset] = bytes[2];
			dst[2 + offset] = bytes[1];
			dst[3 + offset] = bytes[0];
			dst[4 + offset] = bytes[5];
			dst[5 + offset] = bytes[4];
			dst[6 + offset] = bytes[7];
			dst[7 + offset] = bytes[6];
			for (int i = 8; i < 16; i++)
			{
				dst[i + offset] = bytes[i];
			}
		}

		/// <summary>
		/// Checks whether this ClassID is equal to another
		/// object.
		/// </summary>
		/// <param name="o">the object to compare this PropertySet with</param>
		/// <returns>true if the objects are equal, else
		/// false</returns>
		public override bool Equals(object o)
		{
			if (o == null || !(o is ClassID))
			{
				return false;
			}
			ClassID classID = (ClassID)o;
			if (bytes.Length != classID.bytes.Length)
			{
				return false;
			}
			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] != classID.bytes[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		public override int GetHashCode()
		{
			return Encoding.UTF8.GetString(bytes).GetHashCode();
		}

		/// <summary>
		/// Returns a human-Readable representation of the Class ID in standard
		/// format <c>"{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}"</c>.
		/// </summary>
		/// <returns>
		/// A String representation of the Class ID represented by this object..
		/// </returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(38);
			stringBuilder.Append('{');
			for (int i = 0; i < 16; i++)
			{
				stringBuilder.Append(HexDump.ToHex(bytes[i]));
				if (i == 3 || i == 5 || i == 7 || i == 9)
				{
					stringBuilder.Append('-');
				}
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}
	}
}
