using System;

namespace NPOI.HSSF.Record.Crypto
{
	/// Used for both encrypting and decrypting BIFF8 streams. The internal
	/// {@link RC4} instance is renewed (re-keyed) every 1024 bytes.
	///
	/// @author Josh Micich
	public class Biff8RC4
	{
		private const int RC4_REKEYING_INTERVAL = 1024;

		private RC4 _rc4;

		/// This field is used to keep track of when to change the {@link RC4}
		/// instance. The change occurs every 1024 bytes. Every byte passed over is
		/// counted.
		private int _streamPos;

		private int _nextRC4BlockStart;

		private int _currentKeyIndex;

		private bool _shouldSkipEncryptionOnCurrentRecord;

		private Biff8EncryptionKey _key;

		public Biff8RC4(int InitialOffset, Biff8EncryptionKey key)
		{
			if (InitialOffset >= 1024)
			{
				throw new Exception("InitialOffset (" + InitialOffset + ")>" + 1024 + " not supported yet");
			}
			_key = key;
			_streamPos = 0;
			RekeyForNextBlock();
			_streamPos = InitialOffset;
			for (int num = InitialOffset; num > 0; num--)
			{
				_rc4.Output();
			}
			_shouldSkipEncryptionOnCurrentRecord = false;
		}

		private void RekeyForNextBlock()
		{
			_currentKeyIndex = _streamPos / 1024;
			_rc4 = _key.CreateRC4(_currentKeyIndex);
			_nextRC4BlockStart = (_currentKeyIndex + 1) * 1024;
		}

		private int GetNextRC4Byte()
		{
			if (_streamPos >= _nextRC4BlockStart)
			{
				RekeyForNextBlock();
			}
			byte b = _rc4.Output();
			_streamPos++;
			if (_shouldSkipEncryptionOnCurrentRecord)
			{
				return 0;
			}
			return b & 0xFF;
		}

		public void StartRecord(int currentSid)
		{
			_shouldSkipEncryptionOnCurrentRecord = IsNeverEncryptedRecord(currentSid);
		}

		/// TODO: Additionally, the lbPlyPos (position_of_BOF) field of the BoundSheet8 record MUST NOT be encrypted.
		///
		/// @return <c>true</c> if record type specified by <c>sid</c> is never encrypted
		private static bool IsNeverEncryptedRecord(int sid)
		{
			if (sid == 47 || sid == 225 || sid == 2057)
			{
				return true;
			}
			return false;
		}

		/// Used when BIFF header fields (sid, size) are being Read. The internal
		/// {@link RC4} instance must step even when unencrypted bytes are read
		public void SkipTwoBytes()
		{
			GetNextRC4Byte();
			GetNextRC4Byte();
		}

		public void Xor(byte[] buf, int pOffSet, int pLen)
		{
			int num = _nextRC4BlockStart - _streamPos;
			if (pLen <= num)
			{
				_rc4.Encrypt(buf, pOffSet, pLen);
				_streamPos += pLen;
			}
			else
			{
				int num2 = pOffSet;
				int num3 = pLen;
				if (num3 > num)
				{
					if (num > 0)
					{
						_rc4.Encrypt(buf, num2, num);
						_streamPos += num;
						num2 += num;
						num3 -= num;
					}
					RekeyForNextBlock();
				}
				while (num3 > 1024)
				{
					_rc4.Encrypt(buf, num2, 1024);
					_streamPos += 1024;
					num2 += 1024;
					num3 -= 1024;
					RekeyForNextBlock();
				}
				_rc4.Encrypt(buf, num2, num3);
				_streamPos += num3;
			}
		}

		public int XorByte(int rawVal)
		{
			int nextRC4Byte = GetNextRC4Byte();
			return (byte)(rawVal ^ nextRC4Byte);
		}

		public int Xorshort(int rawVal)
		{
			int nextRC4Byte = GetNextRC4Byte();
			int nextRC4Byte2 = GetNextRC4Byte();
			int num = (nextRC4Byte2 << 8) + nextRC4Byte;
			return rawVal ^ num;
		}

		public int XorInt(int rawVal)
		{
			int nextRC4Byte = GetNextRC4Byte();
			int nextRC4Byte2 = GetNextRC4Byte();
			int nextRC4Byte3 = GetNextRC4Byte();
			int nextRC4Byte4 = GetNextRC4Byte();
			int num = (nextRC4Byte4 << 24) + (nextRC4Byte3 << 16) + (nextRC4Byte2 << 8) + nextRC4Byte;
			return rawVal ^ num;
		}

		public long XorLong(long rawVal)
		{
			int nextRC4Byte = GetNextRC4Byte();
			int nextRC4Byte2 = GetNextRC4Byte();
			int nextRC4Byte3 = GetNextRC4Byte();
			int nextRC4Byte4 = GetNextRC4Byte();
			int nextRC4Byte5 = GetNextRC4Byte();
			int nextRC4Byte6 = GetNextRC4Byte();
			int nextRC4Byte7 = GetNextRC4Byte();
			int nextRC4Byte8 = GetNextRC4Byte();
			long num = ((long)nextRC4Byte8 << 56) + ((long)nextRC4Byte7 << 48) + ((long)nextRC4Byte6 << 40) + ((long)nextRC4Byte5 << 32) + ((long)nextRC4Byte4 << 24) + (nextRC4Byte3 << 16) + (nextRC4Byte2 << 8) + nextRC4Byte;
			return rawVal ^ num;
		}
	}
}
