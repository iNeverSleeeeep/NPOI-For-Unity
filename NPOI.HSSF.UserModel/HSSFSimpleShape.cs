using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Represents a simple shape such as a line, rectangle or oval.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Serializable]
	public class HSSFSimpleShape : HSSFShape
	{
		public const short OBJECT_TYPE_LINE = 20;

		public const short OBJECT_TYPE_RECTANGLE = 1;

		public const short OBJECT_TYPE_OVAL = 3;

		public const short OBJECT_TYPE_ARC = 19;

		public const short OBJECT_TYPE_PICTURE = 75;

		public const short OBJECT_TYPE_COMBO_BOX = 201;

		public const short OBJECT_TYPE_COMMENT = 202;

		public const short OBJECT_TYPE_MICROSOFT_OFFICE_DRAWING = 30;

		public const int WRAP_SQUARE = 0;

		public const int WRAP_BY_POINTS = 1;

		public const int WRAP_NONE = 2;

		private TextObjectRecord _textObjectRecord;

		/// <summary>
		/// Gets the shape type.
		/// </summary>
		/// <value>One of the OBJECT_TYPE_* constants.</value>
		/// @see #OBJECT_TYPE_LINE
		/// @see #OBJECT_TYPE_OVAL
		/// @see #OBJECT_TYPE_RECTANGLE
		/// @see #OBJECT_TYPE_PICTURE
		/// @see #OBJECT_TYPE_COMMENT
		public virtual int ShapeType
		{
			get
			{
				EscherSpRecord escherSpRecord = (EscherSpRecord)GetEscherContainer().GetChildById(-4086);
				return escherSpRecord.ShapeType;
			}
			set
			{
				CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)GetObjRecord().SubRecords[0];
				commonObjectDataSubRecord.ObjectType = CommonObjectType.MicrosoftOfficeDrawing;
				EscherSpRecord escherSpRecord = (EscherSpRecord)GetEscherContainer().GetChildById(-4086);
				escherSpRecord.ShapeType = (short)value;
			}
		}

		public int WrapText
		{
			get
			{
				return ((EscherSimpleProperty)GetOptRecord().Lookup(133))?.PropertyValue ?? 0;
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(133, isComplex: false, isBlipId: false, value));
			}
		}

		/// <summary>
		/// Get or set the rich text string used by this object.
		/// </summary>
		public virtual IRichTextString String
		{
			get
			{
				return _textObjectRecord.Str;
			}
			set
			{
				if (ShapeType == 0 || ShapeType == 20)
				{
					throw new InvalidOperationException("Cannot set text for shape type: " + ShapeType);
				}
				HSSFRichTextString hSSFRichTextString = (HSSFRichTextString)value;
				if (hSSFRichTextString.NumFormattingRuns == 0)
				{
					hSSFRichTextString.ApplyFont(0);
				}
				TextObjectRecord orCreateTextObjRecord = GetOrCreateTextObjRecord();
				orCreateTextObjRecord.Str = hSSFRichTextString;
				if (value.String != null)
				{
					SetPropertyValue(new EscherSimpleProperty(128, value.String.GetHashCode()));
				}
			}
		}

		public HSSFSimpleShape(EscherContainerRecord spContainer, ObjRecord objRecord, TextObjectRecord textObjectRecord)
			: base(spContainer, objRecord)
		{
			_textObjectRecord = textObjectRecord;
		}

		public HSSFSimpleShape(EscherContainerRecord spContainer, ObjRecord objRecord)
			: base(spContainer, objRecord)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFSimpleShape" /> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="anchor">The anchor.</param>
		public HSSFSimpleShape(HSSFShape parent, HSSFAnchor anchor)
			: base(parent, anchor)
		{
			_textObjectRecord = CreateTextObjRecord();
		}

		protected internal TextObjectRecord GetTextObjectRecord()
		{
			return _textObjectRecord;
		}

		protected virtual TextObjectRecord CreateTextObjRecord()
		{
			TextObjectRecord textObjectRecord = new TextObjectRecord();
			textObjectRecord.HorizontalTextAlignment = HorizontalTextAlignment.Center;
			textObjectRecord.VerticalTextAlignment = VerticalTextAlignment.Center;
			textObjectRecord.IsTextLocked = true;
			textObjectRecord.TextOrientation = TextOrientation.None;
			textObjectRecord.Str = new HSSFRichTextString("");
			return textObjectRecord;
		}

		internal override HSSFShape CloneShape()
		{
			TextObjectRecord textObjectRecord = null;
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			byte[] data = GetEscherContainer().Serialize();
			escherContainerRecord.FillFields(data, 0, new DefaultEscherRecordFactory());
			ObjRecord objRecord = (ObjRecord)GetObjRecord().CloneViaReserialise();
			if (GetTextObjectRecord() != null && String != null && String.String != null)
			{
				textObjectRecord = (TextObjectRecord)GetTextObjectRecord().CloneViaReserialise();
			}
			return new HSSFSimpleShape(escherContainerRecord, objRecord, textObjectRecord);
		}

		internal override void AfterInsert(HSSFPatriarch patriarch)
		{
			EscherAggregate boundAggregate = patriarch.getBoundAggregate();
			boundAggregate.AssociateShapeToObjRecord(GetEscherContainer().GetChildById(-4079), GetObjRecord());
			if (GetTextObjectRecord() != null)
			{
				boundAggregate.AssociateShapeToObjRecord(GetEscherContainer().GetChildById(-4083), GetTextObjectRecord());
			}
		}

		internal override void AfterRemove(HSSFPatriarch patriarch)
		{
			patriarch.getBoundAggregate().RemoveShapeToObjRecord(GetEscherContainer().GetChildById(-4079));
			if (GetEscherContainer().GetChildById(-4083) != null)
			{
				patriarch.getBoundAggregate().RemoveShapeToObjRecord(GetEscherContainer().GetChildById(-4083));
			}
		}

		protected override EscherContainerRecord CreateSpContainer()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Flags = 2560;
			escherSpRecord.Version = 2;
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(462, 0));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(511, 524296));
			escherOptRecord.SetEscherProperty(new EscherRGBProperty(385, 134217737));
			escherOptRecord.SetEscherProperty(new EscherRGBProperty(448, 134217792));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(447, 65536));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(511, 524296));
			escherOptRecord.SetEscherProperty(new EscherShapePathProperty(324, 4));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(959, 524288));
			escherOptRecord.RecordId = -4085;
			EscherTextboxRecord escherTextboxRecord = new EscherTextboxRecord();
			escherTextboxRecord.RecordId = -4083;
			escherTextboxRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(base.Anchor.GetEscherAnchor());
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			escherContainerRecord.AddChildRecord(escherTextboxRecord);
			return escherContainerRecord;
		}

		protected override ObjRecord CreateObjRecord()
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.IsLocked = true;
			commonObjectDataSubRecord.IsPrintable = true;
			commonObjectDataSubRecord.IsAutoFill = true;
			commonObjectDataSubRecord.IsAutoline = true;
			EndSubRecord o = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			return objRecord;
		}

		private TextObjectRecord GetOrCreateTextObjRecord()
		{
			if (GetTextObjectRecord() == null)
			{
				_textObjectRecord = CreateTextObjRecord();
			}
			EscherTextboxRecord escherTextboxRecord = (EscherTextboxRecord)GetEscherContainer().GetChildById(-4083);
			if (escherTextboxRecord == null)
			{
				escherTextboxRecord = new EscherTextboxRecord();
				escherTextboxRecord.RecordId = -4083;
				escherTextboxRecord.Options = 0;
				GetEscherContainer().AddChildRecord(escherTextboxRecord);
				base.Patriarch.getBoundAggregate().AssociateShapeToObjRecord(escherTextboxRecord, _textObjectRecord);
			}
			return _textObjectRecord;
		}
	}
}
