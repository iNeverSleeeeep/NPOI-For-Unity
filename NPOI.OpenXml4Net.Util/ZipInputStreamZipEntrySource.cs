using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.OpenXml4Net.Util
{
	/// Provides a way to get at all the ZipEntries
	///  from a ZipInputStream, as many times as required.
	/// Allows a ZipInputStream to be treated much like
	///  a ZipFile, for a price in terms of memory.
	/// Be sure to call {@link #close()} as soon as you're
	///  done, to free up that memory!
	public class ZipInputStreamZipEntrySource : ZipEntrySource
	{
		/// Why oh why oh why are Iterator and Enumeration
		///  still not compatible?
		internal class EntryEnumerator : IEnumerator
		{
			private List<FakeZipEntry>.Enumerator iterator;

			public object Current
			{
				get
				{
					return iterator.Current;
				}
			}

			internal EntryEnumerator(List<FakeZipEntry> zipEntries)
			{
				iterator = zipEntries.GetEnumerator();
			}

			public bool MoveNext()
			{
				return iterator.MoveNext();
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}
		}

		/// So we can close the real zip entry and still
		///  effectively work with it.
		/// Holds the (decompressed!) data in memory, so
		///  close this as soon as you can! 
		public class FakeZipEntry : ZipEntry
		{
			private byte[] data;

			public FakeZipEntry(ZipEntry entry, ZipInputStream inp)
				: base(entry.Name)
			{
				MemoryStream memoryStream = new MemoryStream();
				long size = entry.Size;
				if (size != -1)
				{
					if (size >= 2147483647)
					{
						throw new IOException("ZIP entry size is too large");
					}
					memoryStream = new MemoryStream((int)size);
				}
				else
				{
					memoryStream = new MemoryStream();
				}
				byte[] array = new byte[4096];
				int num = 0;
				while ((num = inp.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, num);
				}
				data = memoryStream.ToArray();
			}

			public Stream GetInputStream()
			{
				return new MemoryStream(data);
			}
		}

		private List<FakeZipEntry> zipEntries;

		public IEnumerator Entries
		{
			get
			{
				return new EntryEnumerator(zipEntries);
			}
		}

		/// Reads all the entries from the ZipInputStream 
		///  into memory, and closes the source stream.
		/// We'll then eat lots of memory, but be able to
		///  work with the entries at-will.
		public ZipInputStreamZipEntrySource(ZipInputStream inp)
		{
			zipEntries = new List<FakeZipEntry>();
			bool flag = true;
			while (flag)
			{
				ZipEntry nextEntry = inp.GetNextEntry();
				if (nextEntry == null)
				{
					flag = false;
				}
				else
				{
					FakeZipEntry item = new FakeZipEntry(nextEntry, inp);
					zipEntries.Add(item);
				}
			}
			inp.Close();
		}

		public Stream GetInputStream(ZipEntry zipEntry)
		{
			FakeZipEntry fakeZipEntry = (FakeZipEntry)zipEntry;
			return fakeZipEntry.GetInputStream();
		}

		public void Close()
		{
			zipEntries = null;
		}
	}
}
