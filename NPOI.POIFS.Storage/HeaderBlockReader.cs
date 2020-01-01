using NPOI.POIFS.Common;
using NPOI.POIFS.FileSystem;
using NPOI.Util;
using System;
using System.IO;

namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// The block containing the archive header
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class HeaderBlockReader
	{
		/// What big block Size the file uses. Most files
		///  use 512 bytes, but a few use 4096
		private POIFSBigBlockSize bigBlockSize;

		private int _bat_count;

		private int _property_start;

		private int _sbat_start;

		/// Number of small block allocation table blocks (int)
		/// (Number of MiniFAT Sectors in Microsoft parlance)
		private int _sbat_count;

		private int _xbat_start;

		private int _xbat_count;

		private byte[] _data;

		/// <summary>
		/// Get start of Property Table
		/// </summary>
		/// <value>the index of the first block of the Property Table</value>
		public int PropertyStart => _property_start;

		/// <summary>
		/// Gets start of small block allocation table
		/// </summary>
		/// <value>The SBAT start.</value>
		public int SBATStart => _sbat_start;

		/// <summary>
		/// Gets number of BAT blocks
		/// </summary>
		/// <value>The BAT count.</value>
		public int BATCount => _bat_count;

		/// <summary>
		/// Gets the BAT array.
		/// </summary>
		/// <value>The BAT array.</value>
		public int[] BATArray
		{
			get
			{
				int[] array = new int[109];
				int num = 76;
				for (int i = 0; i < 109; i++)
				{
					array[i] = LittleEndian.GetInt(_data, num);
					num += 4;
				}
				return array;
			}
		}

		/// <summary>
		/// Gets the XBAT count.
		/// </summary>
		/// <value>The XBAT count.</value>
		/// @return XBAT count
		public int XBATCount => _xbat_count;

		/// <summary>
		/// Gets the index of the XBAT.
		/// </summary>
		/// <value>The index of the XBAT.</value>
		public int XBATIndex => _xbat_start;

		/// <summary>
		/// Gets The Big Block Size, normally 512 bytes, sometimes 4096 bytes
		/// </summary>
		/// <value>The size of the big block.</value>
		/// @return 
		public POIFSBigBlockSize BigBlockSize => bigBlockSize;

		/// <summary>
		/// create a new HeaderBlockReader from an Stream
		/// </summary>
		/// <param name="stream">the source Stream</param>
		public HeaderBlockReader(Stream stream)
		{
			stream.Position = 0L;
			_data = ReadFirst512(stream);
			long @long = LittleEndian.GetLong(_data, 0);
			if (@long != -2226271756974174256L)
			{
				byte[] oOXML_FILE_HEADER = POIFSConstants.OOXML_FILE_HEADER;
				if (_data[0] == oOXML_FILE_HEADER[0] && _data[1] == oOXML_FILE_HEADER[1] && _data[2] == oOXML_FILE_HEADER[2] && _data[3] == oOXML_FILE_HEADER[3])
				{
					throw new OfficeXmlFileException("The supplied data appears to be in the Office 2007+ XML. POI only supports OLE2 Office documents");
				}
				if ((@long & -31525197391593473L) == 4503608217567241L)
				{
					throw new ArgumentException("The supplied data appears to be in BIFF2 format.  POI only supports BIFF8 format");
				}
				throw new IOException("Invalid header signature; Read " + LongToHex(@long) + ", expected " + LongToHex(-2226271756974174256L));
			}
			if (_data[30] == 12)
			{
				bigBlockSize = POIFSConstants.LARGER_BIG_BLOCK_SIZE_DETAILS;
			}
			else
			{
				if (_data[30] != 9)
				{
					throw new IOException("Unsupported blocksize  (2^" + _data[30] + "). Expected 2^9 or 2^12.");
				}
				bigBlockSize = POIFSConstants.SMALLER_BIG_BLOCK_SIZE_DETAILS;
			}
			_bat_count = new IntegerField(44, _data).Value;
			_property_start = new IntegerField(48, _data).Value;
			_sbat_start = new IntegerField(60, _data).Value;
			_sbat_count = new IntegerField(64, _data).Value;
			_xbat_start = new IntegerField(68, _data).Value;
			_xbat_count = new IntegerField(72, _data).Value;
			if (bigBlockSize.GetBigBlockSize() != 512)
			{
				int num = bigBlockSize.GetBigBlockSize() - 512;
				byte[] b = new byte[num];
				IOUtils.ReadFully(stream, b);
			}
		}

		private byte[] ReadFirst512(Stream stream)
		{
			byte[] array = new byte[512];
			int num = IOUtils.ReadFully(stream, array);
			if (num != 512)
			{
				AlertShortRead(num, 512);
			}
			return array;
		}

		private static string LongToHex(long value)
		{
			return new string(HexDump.LongToHex(value));
		}

		/// <summary>
		/// Alerts the short read.
		/// </summary>
		/// <param name="read">The read.</param>
		/// <param name="expectedReadSize">expected size to read</param>
		private void AlertShortRead(int read, int expectedReadSize)
		{
			if (read < 0)
			{
				read = 0;
			}
			string text = " byte" + ((read == 1) ? "" : "s");
			throw new IOException("Unable to Read entire header; " + read + text + " Read; expected " + expectedReadSize + " bytes");
		}
	}
}
