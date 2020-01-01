using System;
using System.IO;

namespace NPOI.HSSF.Record
{
	/// Title: Record
	/// Description: All HSSF Records inherit from this class.  It
	///              populates the fields common to all records (id, size and data).
	///              Subclasses should be sure to validate the id,
	/// Company:
	/// @author Andrew C. Oliver
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	[Serializable]
	public abstract class Record : RecordBase
	{
		/// return the non static version of the id for this record.
		public abstract short Sid
		{
			get;
		}

		/// instantiates a blank record strictly for ID matching
		public Record()
		{
		}

		/// called by the class that is responsible for writing this sucker.
		/// Subclasses should implement this so that their data is passed back in a
		/// byte array.
		///
		/// @return byte array containing instance data
		public byte[] Serialize()
		{
			byte[] array = new byte[RecordSize];
			Serialize(0, array);
			return array;
		}

		public virtual object Clone()
		{
			throw new Exception("The class " + GetType().Name + " needs to define a Clone method");
		}

		public Record CloneViaReserialise()
		{
			byte[] buffer = Serialize();
			using (MemoryStream @in = new MemoryStream(buffer))
			{
				RecordInputStream recordInputStream = new RecordInputStream(@in);
				recordInputStream.NextRecord();
				Record[] array = RecordFactory.CreateRecord(recordInputStream);
				if (array.Length != 1)
				{
					throw new InvalidOperationException("Re-serialised a record to clone it, but got " + array.Length + " records back!");
				}
				return array[0];
			}
		}
	}
}
