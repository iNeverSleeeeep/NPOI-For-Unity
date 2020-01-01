using NPOI.Util;
using System;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// Represents an Ole10Native record which is wrapped around certain binary
	/// files being embedded in OLE2 documents.
	///
	/// @author Rainer Schwarze
	public class Ole10Native
	{
		private int totalSize;

		private short flags1;

		private string label;

		private string fileName;

		private short flags2;

		private byte[] unknown1;

		private byte[] unknown2;

		private string command;

		private int dataSize;

		private byte[] dataBuffer;

		private short flags3;

		public static string OLE10_NATIVE = "\u0001Ole10Native";

		/// Returns the value of the totalSize field - the total length of the structure
		/// is totalSize + 4 (value of this field + size of this field).
		///
		/// @return the totalSize
		public int TotalSize => totalSize;

		/// <summary>
		/// Creates an instance of this class from an embedded OLE Object. The OLE Object is expected
		/// to include a stream "{01}Ole10Native" which Contains the actual
		/// data relevant for this class.
		/// </summary>
		/// <param name="poifs">poifs POI Filesystem object</param>
		/// <returns>Returns an instance of this class</returns>
		public static Ole10Native CreateFromEmbeddedOleObject(POIFSFileSystem poifs)
		{
			return CreateFromEmbeddedOleObject(poifs.Root);
		}

		/// <summary>
		/// Creates an instance of this class from an embedded OLE Object. The OLE Object is expected
		/// to include a stream "{01}Ole10Native" which contains the actual
		/// data relevant for this class.
		/// </summary>
		/// <param name="directory">directory POI Filesystem object</param>
		/// <returns>Returns an instance of this class</returns>
		public static Ole10Native CreateFromEmbeddedOleObject(DirectoryNode directory)
		{
			bool flag = false;
			try
			{
				directory.GetEntry("\u0001Ole10ItemName");
				flag = true;
			}
			catch (FileNotFoundException)
			{
				flag = false;
			}
			DocumentEntry documentEntry = (DocumentEntry)directory.GetEntry(OLE10_NATIVE);
			byte[] array = new byte[documentEntry.Size];
			directory.CreateDocumentInputStream(documentEntry).Read(array);
			return new Ole10Native(array, 0, flag);
		}

		/// Creates an instance and Fills the fields based on the data in the given buffer.
		///
		/// @param data   The buffer Containing the Ole10Native record
		/// @param offset The start offset of the record in the buffer
		/// @throws Ole10NativeException on invalid or unexcepted data format
		public Ole10Native(byte[] data, int offset)
			: this(data, offset, plain: false)
		{
		}

		/// Creates an instance and Fills the fields based on the data in the given buffer.
		///
		/// @param data   The buffer Containing the Ole10Native record
		/// @param offset The start offset of the record in the buffer
		/// @param plain Specified 'plain' format without filename
		/// @throws Ole10NativeException on invalid or unexcepted data format
		public Ole10Native(byte[] data, int offset, bool plain)
		{
			if (data.Length < offset + 2)
			{
				throw new Ole10NativeException("data is too small");
			}
			totalSize = LittleEndian.GetInt(data, offset);
			int num = offset + 4;
			if (plain)
			{
				dataBuffer = new byte[totalSize - 4];
				Array.Copy(data, 4, dataBuffer, 0, dataBuffer.Length);
				dataSize = totalSize - 4;
				byte[] array = new byte[8];
				Array.Copy(dataBuffer, 0, array, 0, Math.Min(dataBuffer.Length, 8));
				label = "ole-" + HexDump.ToHex(array);
				fileName = label;
				command = label;
			}
			else
			{
				flags1 = LittleEndian.GetShort(data, num);
				num += 2;
				int stringLength = GetStringLength(data, num);
				label = StringUtil.GetFromCompressedUnicode(data, num, stringLength - 1);
				num += stringLength;
				stringLength = GetStringLength(data, num);
				fileName = StringUtil.GetFromCompressedUnicode(data, num, stringLength - 1);
				num += stringLength;
				flags2 = LittleEndian.GetShort(data, num);
				num += 2;
				stringLength = LittleEndian.GetUByte(data, num);
				unknown1 = new byte[stringLength];
				num += stringLength;
				stringLength = 3;
				unknown2 = new byte[stringLength];
				num += stringLength;
				stringLength = GetStringLength(data, num);
				command = StringUtil.GetFromCompressedUnicode(data, num, stringLength - 1);
				num += stringLength;
				if (totalSize + 4 - num <= 4)
				{
					throw new Ole10NativeException("Invalid Ole10Native");
				}
				dataSize = LittleEndian.GetInt(data, num);
				num += 4;
				if (dataSize > totalSize || dataSize < 0)
				{
					throw new Ole10NativeException("Invalid Ole10Native");
				}
				dataBuffer = new byte[dataSize];
				Array.Copy(data, num, dataBuffer, 0, dataSize);
				num += dataSize;
				if (unknown1.Length > 0)
				{
					flags3 = LittleEndian.GetShort(data, num);
					num += 2;
				}
				else
				{
					flags3 = 0;
				}
			}
		}

		private static int GetStringLength(byte[] data, int ofs)
		{
			int i;
			for (i = 0; i + ofs < data.Length && data[ofs + i] != 0; i++)
			{
			}
			return i + 1;
		}

		/// Returns flags1 - currently unknown - usually 0x0002.
		///
		/// @return the flags1
		public short GetFlags1()
		{
			return flags1;
		}

		/// Returns the label field - usually the name of the file (without directory) but
		/// probably may be any name specified during packaging/embedding the data.
		///
		/// @return the label
		public string GetLabel()
		{
			return label;
		}

		/// Returns the fileName field - usually the name of the file being embedded
		/// including the full path.
		///
		/// @return the fileName
		public string GetFileName()
		{
			return fileName;
		}

		/// Returns flags2 - currently unknown - mostly 0x0000.
		///
		/// @return the flags2
		public short GetFlags2()
		{
			return flags2;
		}

		/// Returns unknown1 field - currently unknown.
		///
		/// @return the unknown1
		public byte[] GetUnknown1()
		{
			return unknown1;
		}

		/// Returns the unknown2 field - currently being a byte[3] - mostly {0, 0, 0}.
		///
		/// @return the unknown2
		public byte[] GetUnknown2()
		{
			return unknown2;
		}

		/// Returns the command field - usually the name of the file being embedded
		/// including the full path, may be a command specified during embedding the file.
		///
		/// @return the command
		public string GetCommand()
		{
			return command;
		}

		/// Returns the size of the embedded file. If the size is 0 (zero), no data has been
		/// embedded. To be sure, that no data has been embedded, check whether
		/// {@link #getDataBuffer()} returns <code>null</code>.
		///
		/// @return the dataSize
		public int GetDataSize()
		{
			return dataSize;
		}

		/// Returns the buffer Containing the embedded file's data, or <code>null</code>
		/// if no data was embedded. Note that an embedding may provide information about
		/// the data, but the actual data is not included. (So label, filename etc. are
		/// available, but this method returns <code>null</code>.)
		///
		/// @return the dataBuffer
		public byte[] GetDataBuffer()
		{
			return dataBuffer;
		}

		/// Returns the flags3 - currently unknown.
		///
		/// @return the flags3
		public short GetFlags3()
		{
			return flags3;
		}
	}
}
