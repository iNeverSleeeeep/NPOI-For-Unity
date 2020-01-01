using NPOI.HSSF.Record.Chart;
using NPOI.HSSF.Record.Crypto;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.HSSF.Record
{
	/// A stream based way to get at complete records, with
	/// as low a memory footprint as possible.
	/// This handles Reading from a RecordInputStream, turning
	/// the data into full records, processing continue records
	/// etc.
	/// Most users should use {@link HSSFEventFactory} /
	/// {@link HSSFListener} and have new records pushed to
	/// them, but this does allow for a "pull" style of coding.
	public class RecordFactoryInputStream
	{
		/// Keeps track of the sizes of the Initial records up to and including {@link FilePassRecord}
		/// Needed for protected files because each byte is encrypted with respect to its absolute
		/// position from the start of the stream.
		private class StreamEncryptionInfo
		{
			private int _InitialRecordsSize;

			private FilePassRecord _filePassRec;

			private Record _lastRecord;

			private bool _hasBOFRecord;

			public bool HasEncryption => _filePassRec != null;

			/// @return last record scanned while looking for encryption info.
			/// This will typically be the first or second record Read. Possibly <code>null</code>
			/// if stream was empty
			public Record LastRecord => _lastRecord;

			/// <c>false</c> in some test cases
			public bool HasBOFRecord => _hasBOFRecord;

			public StreamEncryptionInfo(RecordInputStream rs, List<Record> outputRecs)
			{
				rs.NextRecord();
				int num = 4 + rs.Remaining;
				Record record = RecordFactory.CreateSingleRecord(rs);
				outputRecs.Add(record);
				FilePassRecord filePassRec = null;
				if (record is BOFRecord)
				{
					_hasBOFRecord = true;
					if (rs.HasNextRecord)
					{
						rs.NextRecord();
						record = RecordFactory.CreateSingleRecord(rs);
						num += record.RecordSize;
						outputRecs.Add(record);
						if (record is WriteProtectRecord && rs.HasNextRecord)
						{
							rs.NextRecord();
							record = RecordFactory.CreateSingleRecord(rs);
							num += record.RecordSize;
							outputRecs.Add(record);
						}
						if (record is FilePassRecord)
						{
							filePassRec = (FilePassRecord)record;
							outputRecs.RemoveAt(outputRecs.Count - 1);
							record = outputRecs[0];
						}
						else if (record is EOFRecord)
						{
							throw new InvalidOperationException("Nothing between BOF and EOF");
						}
					}
				}
				else
				{
					_hasBOFRecord = false;
				}
				_InitialRecordsSize = num;
				_filePassRec = filePassRec;
				_lastRecord = record;
			}

			public RecordInputStream CreateDecryptingStream(Stream original)
			{
				FilePassRecord filePassRec = _filePassRec;
				string currentUserPassword = Biff8EncryptionKey.CurrentUserPassword;
				Biff8EncryptionKey biff8EncryptionKey = (currentUserPassword != null) ? Biff8EncryptionKey.Create(currentUserPassword, filePassRec.DocId) : Biff8EncryptionKey.Create(filePassRec.DocId);
				if (!biff8EncryptionKey.Validate(filePassRec.SaltData, filePassRec.SaltHash))
				{
					throw new EncryptedDocumentException(((currentUserPassword == null) ? "Default" : "Supplied") + " password is invalid for docId/saltData/saltHash");
				}
				return new RecordInputStream(original, biff8EncryptionKey, _InitialRecordsSize);
			}
		}

		private RecordInputStream _recStream;

		private bool _shouldIncludeContinueRecords;

		/// Temporarily stores a group of {@link Record}s, for future return by {@link #nextRecord()}.
		/// This is used at the start of the workbook stream, and also when the most recently read
		/// underlying record is a {@link MulRKRecord}
		private Record[] _unreadRecordBuffer;

		/// used to help iterating over the unread records
		private int _unreadRecordIndex = -1;

		/// The most recent record that we gave to the user
		private Record _lastRecord;

		/// The most recent DrawingRecord seen
		private DrawingRecord _lastDrawingRecord = new DrawingRecord();

		private int _bofDepth;

		private bool _lastRecordWasEOFLevelZero;

		/// @param shouldIncludeContinueRecords caller can pass <c>false</c> if loose
		/// {@link ContinueRecord}s should be skipped (this is sometimes useful in event based
		/// processing).
		public RecordFactoryInputStream(Stream in1, bool shouldIncludeContinueRecords)
		{
			RecordInputStream recordInputStream = new RecordInputStream(in1);
			List<Record> list = new List<Record>();
			StreamEncryptionInfo streamEncryptionInfo = new StreamEncryptionInfo(recordInputStream, list);
			if (streamEncryptionInfo.HasEncryption)
			{
				recordInputStream = streamEncryptionInfo.CreateDecryptingStream(in1);
			}
			if (list.Count != 0)
			{
				_unreadRecordBuffer = new Record[list.Count];
				_unreadRecordBuffer = list.ToArray();
				_unreadRecordIndex = 0;
			}
			_recStream = recordInputStream;
			_shouldIncludeContinueRecords = shouldIncludeContinueRecords;
			_lastRecord = streamEncryptionInfo.LastRecord;
			_bofDepth = (streamEncryptionInfo.HasBOFRecord ? 1 : 0);
			_lastRecordWasEOFLevelZero = false;
		}

		/// Returns the next (complete) record from the
		/// stream, or null if there are no more.
		public Record NextRecord()
		{
			Record nextUnreadRecord = GetNextUnreadRecord();
			if (nextUnreadRecord != null)
			{
				return nextUnreadRecord;
			}
			do
			{
				if (!_recStream.HasNextRecord)
				{
					return null;
				}
				_recStream.NextRecord();
				if (_lastRecordWasEOFLevelZero && _recStream.Sid != 2057)
				{
					return null;
				}
				nextUnreadRecord = ReadNextRecord();
			}
			while (nextUnreadRecord == null);
			return nextUnreadRecord;
		}

		/// @return the next {@link Record} from the multiple record group as expanded from
		/// a recently read {@link MulRKRecord}. <code>null</code> if not present.
		private Record GetNextUnreadRecord()
		{
			if (_unreadRecordBuffer != null)
			{
				int unreadRecordIndex = _unreadRecordIndex;
				if (unreadRecordIndex < _unreadRecordBuffer.Length)
				{
					Record result = _unreadRecordBuffer[unreadRecordIndex];
					_unreadRecordIndex = unreadRecordIndex + 1;
					return result;
				}
				_unreadRecordIndex = -1;
				_unreadRecordBuffer = null;
			}
			return null;
		}

		/// @return the next available record, or <code>null</code> if
		/// this pass didn't return a record that's
		/// suitable for returning (eg was a continue record).
		private Record ReadNextRecord()
		{
			Record record = RecordFactory.CreateSingleRecord(_recStream);
			_lastRecordWasEOFLevelZero = false;
			if (record is BOFRecord)
			{
				_bofDepth++;
				return record;
			}
			if (record is EOFRecord)
			{
				_bofDepth--;
				if (_bofDepth < 1)
				{
					_lastRecordWasEOFLevelZero = true;
				}
				return record;
			}
			if (record is DBCellRecord)
			{
				return null;
			}
			if (record is RKRecord)
			{
				return RecordFactory.ConvertToNumberRecord((RKRecord)record);
			}
			if (record is MulRKRecord)
			{
				Record[] array = _unreadRecordBuffer = RecordFactory.ConvertRKRecords((MulRKRecord)record);
				_unreadRecordIndex = 1;
				return array[0];
			}
			if (record.Sid == 235 && _lastRecord is DrawingGroupRecord)
			{
				DrawingGroupRecord drawingGroupRecord = (DrawingGroupRecord)_lastRecord;
				drawingGroupRecord.Join((AbstractEscherHolderRecord)record);
				return null;
			}
			if (record.Sid == 60)
			{
				ContinueRecord continueRecord = (ContinueRecord)record;
				if (_lastRecord is ObjRecord || _lastRecord is TextObjectRecord)
				{
					_lastDrawingRecord.ProcessContinueRecord(continueRecord.Data);
					if (_shouldIncludeContinueRecords)
					{
						return record;
					}
					return null;
				}
				if (_lastRecord is DrawingGroupRecord)
				{
					((DrawingGroupRecord)_lastRecord).ProcessContinueRecord(continueRecord.Data);
					return null;
				}
				if (_lastRecord is DrawingRecord)
				{
					return continueRecord;
				}
				if (_lastRecord is CrtMlFrtRecord)
				{
					return record;
				}
				if (_lastRecord is UnknownRecord)
				{
					return record;
				}
				if (_lastRecord is EOFRecord)
				{
					return record;
				}
				throw new RecordFormatException("Unhandled Continue Record");
			}
			_lastRecord = record;
			if (record is DrawingRecord)
			{
				_lastDrawingRecord = (DrawingRecord)record;
			}
			return record;
		}
	}
}
