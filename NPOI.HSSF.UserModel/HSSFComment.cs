using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Represents a cell comment - a sticky note associated with a cell.
	/// @author Yegor Kozlov
	/// </summary>
	[Serializable]
	public class HSSFComment : HSSFTextbox, IComment
	{
		private const int FILL_TYPE_SOLID = 0;

		private const int FILL_TYPE_PICTURE = 3;

		private NoteRecord _note;

		public override int ShapeId
		{
			get
			{
				return base.ShapeId;
			}
			set
			{
				base.ShapeId = value;
				CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)GetObjRecord().SubRecords[0];
				commonObjectDataSubRecord.ObjectId = (short)(value % 1024);
				_note.ShapeId = value % 1024;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:NPOI.HSSF.UserModel.HSSFComment" /> is visible.
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		/// Sets whether this comment Is visible.
		/// @return 
		/// <c>true</c>
		///  if the comment Is visible, 
		/// <c>false</c>
		///  otherwise
		public bool Visible
		{
			get
			{
				return _note.Flags == 2;
			}
			set
			{
				if (_note != null)
				{
					_note.Flags = (short)(value ? 2 : 0);
				}
			}
		}

		/// <summary>
		/// Gets or sets the row of the cell that Contains the comment
		/// </summary>
		/// <value>the 0-based row of the cell that Contains the comment</value>
		public int Row
		{
			get
			{
				return _note.Row;
			}
			set
			{
				if (_note != null)
				{
					_note.Row = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the column of the cell that Contains the comment
		/// </summary>
		/// <value>the 0-based column of the cell that Contains the comment</value>
		public int Column
		{
			get
			{
				return _note.Column;
			}
			set
			{
				if (_note != null)
				{
					_note.Column = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the name of the original comment author
		/// </summary>
		/// <value>the name of the original author of the comment</value>
		public string Author
		{
			get
			{
				return _note.Author;
			}
			set
			{
				if (_note != null)
				{
					_note.Author = value;
				}
			}
		}

		/// <summary>
		/// Gets the note record.
		/// </summary>
		/// <value>the underlying Note record.</value>
		internal NoteRecord NoteRecord => _note;

		public override int ShapeType
		{
			get
			{
				return base.ShapeType;
			}
			set
			{
				throw new InvalidOperationException("Shape type can not be changed in " + GetType().Name);
			}
		}

		public HSSFComment(EscherContainerRecord spContainer, ObjRecord objRecord, TextObjectRecord textObjectRecord, NoteRecord _note)
			: base(spContainer, objRecord, textObjectRecord)
		{
			this._note = _note;
		}

		/// <summary>
		/// Construct a new comment with the given parent and anchor.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="anchor">defines position of this anchor in the sheet</param>
		public HSSFComment(HSSFShape parent, HSSFAnchor anchor)
			: base(parent, anchor)
		{
			_note = CreateNoteRecord();
			base.FillColor = 134217808;
			Visible = false;
			Author = "";
			CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)GetObjRecord().SubRecords[0];
			commonObjectDataSubRecord.ObjectType = CommonObjectType.Comment;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFComment" /> class.
		/// </summary>
		/// <param name="note">The note.</param>
		/// <param name="txo">The txo.</param>
		public HSSFComment(NoteRecord note, TextObjectRecord txo)
			: this(null, new HSSFClientAnchor())
		{
			_note = note;
		}

		internal override void AfterInsert(HSSFPatriarch patriarch)
		{
			base.AfterInsert(patriarch);
			patriarch.getBoundAggregate().AddTailRecord(NoteRecord);
		}

		protected override EscherContainerRecord CreateSpContainer()
		{
			EscherContainerRecord escherContainerRecord = base.CreateSpContainer();
			EscherOptRecord escherOptRecord = (EscherOptRecord)escherContainerRecord.GetChildById(-4085);
			escherOptRecord.RemoveEscherProperty(129);
			escherOptRecord.RemoveEscherProperty(131);
			escherOptRecord.RemoveEscherProperty(130);
			escherOptRecord.RemoveEscherProperty(132);
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(959, isComplex: false, isBlipId: false, 655362));
			return escherContainerRecord;
		}

		protected override ObjRecord CreateObjRecord()
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.ObjectType = (CommonObjectType)202;
			commonObjectDataSubRecord.IsLocked = true;
			commonObjectDataSubRecord.IsPrintable = true;
			commonObjectDataSubRecord.IsAutoFill = false;
			commonObjectDataSubRecord.IsAutoline = true;
			NoteStructureSubRecord o = new NoteStructureSubRecord();
			EndSubRecord o2 = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			objRecord.AddSubRecord(o2);
			return objRecord;
		}

		private NoteRecord CreateNoteRecord()
		{
			NoteRecord noteRecord = new NoteRecord();
			noteRecord.Flags = 0;
			noteRecord.Author = "";
			return noteRecord;
		}

		internal override void AfterRemove(HSSFPatriarch patriarch)
		{
			base.AfterRemove(patriarch);
			patriarch.getBoundAggregate().RemoveTailRecord(NoteRecord);
		}

		internal override HSSFShape CloneShape()
		{
			TextObjectRecord textObjectRecord = (TextObjectRecord)GetTextObjectRecord().CloneViaReserialise();
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			byte[] data = GetEscherContainer().Serialize();
			escherContainerRecord.FillFields(data, 0, new DefaultEscherRecordFactory());
			ObjRecord objRecord = (ObjRecord)GetObjRecord().CloneViaReserialise();
			NoteRecord note = (NoteRecord)NoteRecord.CloneViaReserialise();
			return new HSSFComment(escherContainerRecord, objRecord, textObjectRecord, note);
		}

		public void SetBackgroundImage(int pictureIndex)
		{
			SetPropertyValue(new EscherSimpleProperty(390, isComplex: false, isBlipId: true, pictureIndex));
			SetPropertyValue(new EscherSimpleProperty(384, isComplex: false, isBlipId: false, 3));
			EscherBSERecord bSERecord = ((HSSFWorkbook)base.Patriarch.Sheet.Workbook).Workbook.GetBSERecord(pictureIndex);
			bSERecord.Ref++;
		}

		public void ResetBackgroundImage()
		{
			EscherSimpleProperty escherSimpleProperty = (EscherSimpleProperty)GetOptRecord().Lookup(390);
			if (escherSimpleProperty != null)
			{
				EscherBSERecord bSERecord = ((HSSFWorkbook)base.Patriarch.Sheet.Workbook).Workbook.GetBSERecord(escherSimpleProperty.PropertyValue);
				bSERecord.Ref--;
				GetOptRecord().RemoveEscherProperty(390);
			}
			SetPropertyValue(new EscherSimpleProperty(384, isComplex: false, isBlipId: false, 0));
		}

		public int GetBackgroundImageId()
		{
			return ((EscherSimpleProperty)GetOptRecord().Lookup(390))?.PropertyValue ?? 0;
		}
	}
}
