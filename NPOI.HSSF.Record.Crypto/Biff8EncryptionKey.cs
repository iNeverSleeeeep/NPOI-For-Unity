using NPOI.Util;
using System;
using System.IO;
using System.Security.Cryptography;

namespace NPOI.HSSF.Record.Crypto
{
	public class Biff8EncryptionKey
	{
		private const int KEY_DIGEST_LENGTH = 5;

		private const int PASSWORD_HASH_NUMBER_OF_BYTES_USED = 5;

		private byte[] _keyDigest;

		/// Stores the BIFF8 encryption/decryption password for the current thread.  This has been done
		/// using a {@link ThreadLocal} in order to avoid further overloading the various public APIs
		/// (e.g. {@link HSSFWorkbook}) that need this functionality.
		[ThreadStatic]
		private static string _userPasswordTLS;

		/// @return the BIFF8 encryption/decryption password for the current thread.
		/// <code>null</code> if it is currently unSet.
		public static string CurrentUserPassword
		{
			get
			{
				return _userPasswordTLS;
			}
			set
			{
				_userPasswordTLS = value;
			}
		}

		/// Create using the default password and a specified docId
		/// @param docId 16 bytes
		public static Biff8EncryptionKey Create(byte[] docId)
		{
			return new Biff8EncryptionKey(CreateKeyDigest("VelvetSweatshop", docId));
		}

		public static Biff8EncryptionKey Create(string password, byte[] docIdData)
		{
			return new Biff8EncryptionKey(CreateKeyDigest(password, docIdData));
		}

		internal Biff8EncryptionKey(byte[] keyDigest)
		{
			if (keyDigest.Length != 5)
			{
				throw new ArgumentException("Expected 5 byte key digest, but got " + HexDump.ToHex(keyDigest));
			}
			_keyDigest = keyDigest;
		}

		internal static byte[] CreateKeyDigest(string password, byte[] docIdData)
		{
			Check16Bytes(docIdData, "docId");
			int num = Math.Min(password.Length, 16);
			byte[] array = new byte[num * 2];
			for (int i = 0; i < num; i++)
			{
				char c = password[i];
				array[i * 2] = (byte)(c & 0xFF);
				array[i * 2 + 1] = (byte)(((uint)c << 8) & 0xFF);
			}
			using (MD5 mD = new MD5CryptoServiceProvider())
			{
				byte[] sourceArray = mD.ComputeHash(array);
				mD.Initialize();
				byte[] array2 = new byte[80 + docIdData.Length * 16];
				int num2 = 0;
				for (int j = 0; j < 16; j++)
				{
					Array.Copy(sourceArray, 0, array2, num2, 5);
					num2 += 5;
					Array.Copy(docIdData, 0, array2, num2, docIdData.Length);
					num2 += docIdData.Length;
				}
				byte[] sourceArray2 = mD.ComputeHash(array2);
				byte[] array3 = new byte[5];
				Array.Copy(sourceArray2, 0, array3, 0, 5);
				mD.Clear();
				return array3;
			}
		}

		/// @return <c>true</c> if the keyDigest is compatible with the specified saltData and saltHash
		public bool Validate(byte[] saltData, byte[] saltHash)
		{
			Check16Bytes(saltData, "saltData");
			Check16Bytes(saltHash, "saltHash");
			RC4 rC = CreateRC4(0);
			byte[] array = new byte[saltData.Length];
			Array.Copy(saltData, array, saltData.Length);
			rC.Encrypt(array);
			byte[] array2 = new byte[saltHash.Length];
			Array.Copy(saltHash, array2, saltHash.Length);
			rC.Encrypt(array2);
			using (MD5 mD = new MD5CryptoServiceProvider())
			{
				byte[] b = mD.ComputeHash(array);
				return Arrays.Equals(array2, b);
			}
		}

		private static byte[] xor(byte[] a, byte[] b)
		{
			byte[] array = new byte[a.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)(a[i] ^ b[i]);
			}
			return array;
		}

		private static void Check16Bytes(byte[] data, string argName)
		{
			if (data.Length != 16)
			{
				throw new ArgumentException("Expected 16 byte " + argName + ", but got " + HexDump.ToHex(data));
			}
		}

		/// The {@link RC4} instance needs to be Changed every 1024 bytes.
		/// @param keyBlockNo used to seed the newly Created {@link RC4}
		internal RC4 CreateRC4(int keyBlockNo)
		{
			using (MD5 mD = new MD5CryptoServiceProvider())
			{
				using (MemoryStream memoryStream = new MemoryStream(4))
				{
					new LittleEndianOutputStream(memoryStream).WriteInt(keyBlockNo);
					byte[] array = memoryStream.ToArray();
					byte[] array2 = new byte[array.Length + _keyDigest.Length];
					Array.Copy(_keyDigest, 0, array2, 0, _keyDigest.Length);
					Array.Copy(array, 0, array2, _keyDigest.Length, array.Length);
					byte[] key = mD.ComputeHash(array2);
					return new RC4(key);
				}
			}
		}
	}
}
