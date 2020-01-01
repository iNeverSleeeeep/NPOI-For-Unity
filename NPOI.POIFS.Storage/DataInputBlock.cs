using System;

namespace NPOI.POIFS.Storage
{
	/// Wraps a <c>byte</c> array and provides simple data input access.
	/// Internally, this class maintains a buffer read index, so that for the most part, primitive
	/// data can be read in a data-input-stream-like manner.<p />
	///
	/// Note - the calling class should call the {@link #available()} method to detect end-of-buffer
	/// and Move to the next data block when the current is exhausted.
	/// For optimisation reasons, no error handling is performed in this class.  Thus, mistakes in
	/// calling code ran may raise ugly exceptions here, like {@link ArrayIndexOutOfBoundsException},
	/// etc .<p />
	///
	/// The multi-byte primitive input methods ({@link #readUshortLE()}, {@link #readIntLE()} and
	/// {@link #readLongLE()}) have corresponding 'spanning Read' methods which (when required) perform
	/// a read across the block boundary.  These spanning read methods take the previous
	/// {@link DataInputBlock} as a parameter.
	/// Reads of larger amounts of data (into <c>byte</c> array buffers) must be managed by the caller
	/// since these could conceivably involve more than two blocks.
	///
	/// @author Josh Micich
	public class DataInputBlock
	{
		/// Possibly any size (usually 512K or 64K).  Assumed to be at least 8 bytes for all blocks
		/// before the end of the stream.  The last block in the stream can be any size except zero. 
		private byte[] _buf;

		private int _readIndex;

		private int _maxIndex;

		internal DataInputBlock(byte[] data, int startOffset)
		{
			_buf = data;
			_readIndex = startOffset;
			_maxIndex = _buf.Length;
		}

		public int Available()
		{
			return _maxIndex - _readIndex;
		}

		public int ReadUByte()
		{
			return _buf[_readIndex++] & 0xFF;
		}

		/// Reads a <c>short</c> which was encoded in <em>little endian</em> format.
		public int ReadUshortLE()
		{
			int num = _readIndex;
			int num3 = _buf[num++] & 0xFF;
			int num5 = _buf[num++] & 0xFF;
			_readIndex = num;
			return (num5 << 8) + num3;
		}

		/// Reads a <c>short</c> which spans the end of <c>prevBlock</c> and the start of this block.
		public int ReadUshortLE(DataInputBlock prevBlock)
		{
			int num = prevBlock._buf.Length - 1;
			int num3 = prevBlock._buf[num++] & 0xFF;
			int num4 = _buf[_readIndex++] & 0xFF;
			return (num4 << 8) + num3;
		}

		/// Reads an <c>int</c> which was encoded in <em>little endian</em> format.
		public int ReadIntLE()
		{
			int num = _readIndex;
			int num3 = _buf[num++] & 0xFF;
			int num5 = _buf[num++] & 0xFF;
			int num7 = _buf[num++] & 0xFF;
			int num9 = _buf[num++] & 0xFF;
			_readIndex = num;
			return (num9 << 24) + (num7 << 16) + (num5 << 8) + num3;
		}

		/// Reads an <c>int</c> which spans the end of <c>prevBlock</c> and the start of this block.
		public int ReadIntLE(DataInputBlock prevBlock, int prevBlockAvailable)
		{
			byte[] array = new byte[4];
			ReadSpanning(prevBlock, prevBlockAvailable, array);
			int num = array[0] & 0xFF;
			int num2 = array[1] & 0xFF;
			int num3 = array[2] & 0xFF;
			int num4 = array[3] & 0xFF;
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		/// Reads a <c>long</c> which was encoded in <em>little endian</em> format.
		public long ReadLongLE()
		{
			int num = _readIndex;
			int num3 = _buf[num++] & 0xFF;
			int num5 = _buf[num++] & 0xFF;
			int num7 = _buf[num++] & 0xFF;
			int num9 = _buf[num++] & 0xFF;
			int num11 = _buf[num++] & 0xFF;
			int num13 = _buf[num++] & 0xFF;
			int num15 = _buf[num++] & 0xFF;
			int num17 = _buf[num++] & 0xFF;
			_readIndex = num;
			return ((long)num17 << 56) + ((long)num15 << 48) + ((long)num13 << 40) + ((long)num11 << 32) + ((long)num9 << 24) + (num7 << 16) + (num5 << 8) + num3;
		}

		/// Reads a <c>long</c> which spans the end of <c>prevBlock</c> and the start of this block.
		public long ReadLongLE(DataInputBlock prevBlock, int prevBlockAvailable)
		{
			byte[] array = new byte[8];
			ReadSpanning(prevBlock, prevBlockAvailable, array);
			int num = array[0] & 0xFF;
			int num2 = array[1] & 0xFF;
			int num3 = array[2] & 0xFF;
			int num4 = array[3] & 0xFF;
			int num5 = array[4] & 0xFF;
			int num6 = array[5] & 0xFF;
			int num7 = array[6] & 0xFF;
			int num8 = array[7] & 0xFF;
			return ((long)num8 << 56) + ((long)num7 << 48) + ((long)num6 << 40) + ((long)num5 << 32) + ((long)num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		/// Reads a small amount of data from across the boundary between two blocks.  
		/// The {@link #_readIndex} of this (the second) block is updated accordingly.
		/// Note- this method (and other code) assumes that the second {@link DataInputBlock}
		/// always is big enough to complete the read without being exhausted.
		private void ReadSpanning(DataInputBlock prevBlock, int prevBlockAvailable, byte[] buf)
		{
			Array.Copy(prevBlock._buf, prevBlock._readIndex, buf, 0, prevBlockAvailable);
			int num = buf.Length - prevBlockAvailable;
			Array.Copy(_buf, 0, buf, prevBlockAvailable, num);
			_readIndex = num;
		}

		/// Reads <c>len</c> bytes from this block into the supplied buffer.
		public void ReadFully(byte[] buf, int off, int len)
		{
			Array.Copy(_buf, _readIndex, buf, off, len);
			_readIndex += len;
		}
	}
}
