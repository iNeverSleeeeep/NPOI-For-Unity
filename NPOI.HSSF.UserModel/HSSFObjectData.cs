using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.POIFS.FileSystem;
using NPOI.Util;
using System;
using System.Collections;
using System.IO;

namespace NPOI.HSSF.UserModel
{
	/// Represents binary object (i.e. OLE) data stored in the file.  Eg. A GIF, JPEG etc...
	///
	/// @author Daniel Noll
	public class HSSFObjectData : HSSFPicture
	{
		/// Reference to the filesystem root, required for retrieving the object data.
		private DirectoryEntry _root;

		/// Returns the OLE2 Class Name of the object
		public string OLE2ClassName => FindObjectRecord().OLEClassName;

		public HSSFObjectData(EscherContainerRecord spContainer, ObjRecord objRecord, DirectoryEntry _root)
			: base(spContainer, objRecord)
		{
			this._root = _root;
		}

		/// Gets the object data. Only call for ones that have
		///  data though. See {@link #hasDirectoryEntry()}
		///
		/// @return the object data as an OLE2 directory.
		/// @ if there was an error Reading the data.
		public DirectoryEntry GetDirectory()
		{
			EmbeddedObjectRefSubRecord embeddedObjectRefSubRecord = FindObjectRecord();
			string text = "MBD" + HexDump.ToHex(embeddedObjectRefSubRecord.StreamId.Value);
			Entry entry = _root.GetEntry(text);
			if (entry is DirectoryEntry)
			{
				return (DirectoryEntry)entry;
			}
			throw new IOException("Stream " + text + " was not an OLE2 directory");
		}

		/// Returns the data portion, for an ObjectData
		///  that doesn't have an associated POIFS Directory
		///  Entry
		public byte[] GetObjectData()
		{
			return FindObjectRecord().ObjectData;
		}

		/// Does this ObjectData have an associated POIFS 
		///  Directory Entry?
		/// (Not all do, those that don't have a data portion)
		public bool HasDirectoryEntry()
		{
			EmbeddedObjectRefSubRecord embeddedObjectRefSubRecord = FindObjectRecord();
			int? streamId = embeddedObjectRefSubRecord.StreamId;
			if (streamId.HasValue)
			{
				int? num = streamId;
				if (num.GetValueOrDefault() == 0)
				{
					return !num.HasValue;
				}
				return true;
			}
			return false;
		}

		/// Finds the EmbeddedObjectRefSubRecord, or throws an 
		///  Exception if there wasn't one
		public EmbeddedObjectRefSubRecord FindObjectRecord()
		{
			IEnumerator enumerator = GetObjRecord().SubRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (current is EmbeddedObjectRefSubRecord)
				{
					return (EmbeddedObjectRefSubRecord)current;
				}
			}
			throw new InvalidOperationException("Object data does not contain a reference to an embedded object OLE2 directory");
		}

		protected override EscherContainerRecord CreateSpContainer()
		{
			throw new InvalidOperationException("HSSFObjectData cannot be created from scratch");
		}

		protected override ObjRecord CreateObjRecord()
		{
			throw new InvalidOperationException("HSSFObjectData cannot be created from scratch");
		}

		internal override void AfterRemove(HSSFPatriarch patriarch)
		{
			throw new InvalidOperationException("HSSFObjectData cannot be created from scratch");
		}

		internal override void AfterInsert(HSSFPatriarch patriarch)
		{
			EscherAggregate boundAggregate = patriarch.GetBoundAggregate();
			boundAggregate.AssociateShapeToObjRecord(GetEscherContainer().GetChildById(-4079), GetObjRecord());
			EscherBSERecord bSERecord = ((HSSFWorkbook)patriarch.Sheet.Workbook).Workbook.GetBSERecord(base.PictureIndex);
			bSERecord.Ref++;
		}

		internal override HSSFShape CloneShape()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			byte[] data = GetEscherContainer().Serialize();
			escherContainerRecord.FillFields(data, 0, new DefaultEscherRecordFactory());
			ObjRecord objRecord = (ObjRecord)GetObjRecord().CloneViaReserialise();
			return new HSSFObjectData(escherContainerRecord, objRecord, _root);
		}
	}
}
