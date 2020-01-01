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
	public class HeaderBlock : HeaderBlockConstants
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(HeaderBlock));

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

		private static byte _default_value = byte.MaxValue;

		/// <summary>
		/// Get start of Property Table
		/// </summary>
		/// <value>the index of the first block of the Property Table</value>
		public int PropertyStart
		{
			get
			{
				return _property_start;
			}
			set
			{
				_property_start = value;
			}
		}

		/// <summary>
		/// Gets start of small block allocation table
		/// </summary>
		/// <value>The SBAT start.</value>
		public int SBATStart
		{
			get
			{
				return _sbat_start;
			}
			set
			{
				_sbat_start = value;
			}
		}

		/// <summary>
		/// Gets number of BAT blocks
		/// </summary>
		/// <value>The BAT count.</value>
		public int SBATCount
		{
			get
			{
				return _sbat_count;
			}
			set
			{
				_sbat_count = value;
			}
		}

		public int SBATBlockCount
		{
			get
			{
				return _sbat_count;
			}
			set
			{
				_sbat_count = value;
			}
		}

		public int BATCount
		{
			get
			{
				return _bat_count;
			}
			set
			{
				_bat_count = value;
			}
		}

		/// <summary>
		/// Gets the BAT array.
		/// </summary>
		/// <value>The BAT array.</value>
		public int[] BATArray
		{
			get
			{
				int[] array = new int[Math.Min(_bat_count, 109)];
				int num = 76;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = LittleEndian.GetInt(_data, num);
					num += 4;
				}
				return array;
			}
			set
			{
				int num = Math.Min(value.Length, 109);
				int num2 = 109 - num;
				int num3 = 76;
				for (int i = 0; i < num; i++)
				{
					LittleEndian.PutInt(_data, num3, value[i]);
					num3 += 4;
				}
				for (int j = 0; j < num2; j++)
				{
					LittleEndian.PutInt(_data, num3, -1);
					num3 += 4;
				}
			}
		}

		/// <summary>
		/// Gets the XBAT count.
		/// </summary>
		/// <value>The XBAT count.</value>
		/// @return XBAT count
		public int XBATCount
		{
			get
			{
				return _xbat_count;
			}
			set
			{
				_xbat_count = value;
			}
		}

		/// <summary>
		/// Gets the index of the XBAT.
		/// </summary>
		/// <value>The index of the XBAT.</value>
		public int XBATIndex
		{
			get
			{
				return _xbat_start;
			}
			set
			{
				_xbat_count = value;
			}
		}

		public int XBATStart
		{
			set
			{
				_xbat_start = value;
			}
		}

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
		public HeaderBlock(Stream stream)
		{
			try
			{
				stream.Position = 0L;
				PrivateHeaderBlock(ReadFirst512(stream));
				if (bigBlockSize.GetBigBlockSize() != 512)
				{
					int num = bigBlockSize.GetBigBlockSize() - 512;
					byte[] b = new byte[num];
					IOUtils.ReadFully(stream, b);
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		public HeaderBlock(ByteBuffer buffer)
			: this(IOUtils.ToByteArray(buffer, 512))
		{
		}

		public HeaderBlock(byte[] buffer)
		{
			try
			{
				PrivateHeaderBlock(buffer);
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		public void PrivateHeaderBlock(byte[] data)
		{
			_data = data;
			long @long = LittleEndian.GetLong(_data, 0);
			if (@long != -2226271756974174256L)
			{
				byte[] oOXML_FILE_HEADER = POIFSConstants.OOXML_FILE_HEADER;
				if (_data[0] == oOXML_FILE_HEADER[0] && _data[1] == oOXML_FILE_HEADER[1] && _data[2] == oOXML_FILE_HEADER[2] && _data[3] == oOXML_FILE_HEADER[3])
				{
					throw new OfficeXmlFileException("The supplied data appears to be in the Office 2007+ XML. You are calling the part of POI that deals with OLE2 Office Documents. You need to call a different part of POI to process this data (eg XSSF instead of HSSF)");
				}
				if ((@long & -31525197391593473L) == 4503608217567241L)
				{
					throw new ArgumentException("The supplied data appears to be in BIFF2 format.  POI only supports BIFF8 format");
				}
				throw new IOException("Invalid header signature; read " + LongToHex(@long) + ", expected " + LongToHex(-2226271756974174256L));
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
		}

		public HeaderBlock(POIFSBigBlockSize bigBlockSize)
		{
			this.bigBlockSize = bigBlockSize;
			_data = new byte[512];
			for (int i = 0; i < _data.Length; i++)
			{
				_data[i] = _default_value;
			}
			new LongField(0, -2226271756974174256L, _data);
			new IntegerField(8, 0, _data);
			new IntegerField(12, 0, _data);
			new IntegerField(16, 0, _data);
			new IntegerField(20, 0, _data);
			new ShortField(24, 59, ref _data);
			new ShortField(26, 3, ref _data);
			new ShortField(28, -2, ref _data);
			new ShortField(30, bigBlockSize.GetHeaderValue(), ref _data);
			new IntegerField(32, 6, _data);
			new IntegerField(36, 0, _data);
			new IntegerField(40, 0, _data);
			new IntegerField(52, 0, _data);
			new IntegerField(56, 4096, _data);
			_bat_count = 0;
			_sbat_count = 0;
			_xbat_count = 0;
			_property_start = -2;
			_sbat_start = -2;
			_xbat_start = -2;
		}

		private static byte[] ReadFirst512(Stream stream)
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
		/// <param name="expectedReadSize">The expected size.</param>
		private static IOException AlertShortRead(int read, int expectedReadSize)
		{
			if (read < 0)
			{
				read = 0;
			}
			string text = " byte" + ((read == 1) ? "" : "s");
			return new IOException("Unable to Read entire header; " + read + text + " Read; expected " + expectedReadSize + " bytes");
		}

		public void WriteData(Stream stream)
		{
			try
			{
				new IntegerField(44, _bat_count, _data);
				new IntegerField(48, _property_start, _data);
				new IntegerField(60, _sbat_start, _data);
				new IntegerField(64, _sbat_count, _data);
				new IntegerField(68, _xbat_start, _data);
				new IntegerField(72, _xbat_count, _data);
				stream.Write(_data, 0, 512);
				for (int i = 512; i < bigBlockSize.GetBigBlockSize(); i++)
				{
					stream.WriteByte(0);
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}
	}
}
