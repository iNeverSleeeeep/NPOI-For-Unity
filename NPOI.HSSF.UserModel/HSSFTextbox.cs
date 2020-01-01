using NPOI.DDF;
using NPOI.HSSF.Record;
using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// A textbox Is a shape that may hold a rich text string.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Serializable]
	public class HSSFTextbox : HSSFSimpleShape
	{
		public const short OBJECT_TYPE_TEXT = 6;

		private HSSFRichTextString str = new HSSFRichTextString("");

		/// <summary>
		/// Gets or sets the left margin within the textbox.
		/// </summary>
		/// <value>The margin left.</value>
		public int MarginLeft
		{
			get
			{
				return ((EscherSimpleProperty)GetOptRecord().Lookup(129))?.PropertyValue ?? 0;
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(129, value));
			}
		}

		/// <summary>
		/// Gets or sets the right margin within the textbox.
		/// </summary>
		/// <value>The margin right.</value>
		public int MarginRight
		{
			get
			{
				return ((EscherSimpleProperty)GetOptRecord().Lookup(131))?.PropertyValue ?? 0;
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(131, value));
			}
		}

		/// <summary>
		/// Gets or sets the top margin within the textbox
		/// </summary>
		/// <value>The top margin.</value>
		public int MarginTop
		{
			get
			{
				return ((EscherSimpleProperty)GetOptRecord().Lookup(130))?.PropertyValue ?? 0;
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(130, value));
			}
		}

		/// <summary>
		/// Gets or sets the bottom margin within the textbox.
		/// </summary>
		/// <value>The margin bottom.</value>
		public int MarginBottom
		{
			get
			{
				return ((EscherSimpleProperty)GetOptRecord().Lookup(132))?.PropertyValue ?? 0;
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(132, value));
			}
		}

		/// <summary>
		/// Gets or sets the horizontal alignment.
		/// </summary>
		/// <value>The horizontal alignment.</value>
		public HorizontalTextAlignment HorizontalAlignment
		{
			get
			{
				return GetTextObjectRecord().HorizontalTextAlignment;
			}
			set
			{
				GetTextObjectRecord().HorizontalTextAlignment = value;
			}
		}

		/// <summary>
		/// Gets or sets the vertical alignment.
		/// </summary>
		/// <value>The vertical alignment.</value>
		public VerticalTextAlignment VerticalAlignment
		{
			get
			{
				return GetTextObjectRecord().VerticalTextAlignment;
			}
			set
			{
				GetTextObjectRecord().VerticalTextAlignment = value;
			}
		}

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

		public HSSFTextbox(EscherContainerRecord spContainer, ObjRecord objRecord, TextObjectRecord textObjectRecord)
			: base(spContainer, objRecord, textObjectRecord)
		{
		}

		/// <summary>
		/// Construct a new textbox with the given parent and anchor.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="anchor">One of HSSFClientAnchor or HSSFChildAnchor</param>
		public HSSFTextbox(HSSFShape parent, HSSFAnchor anchor)
			: base(parent, anchor)
		{
			HorizontalAlignment = HorizontalTextAlignment.Left;
			VerticalAlignment = VerticalTextAlignment.Top;
			String = new HSSFRichTextString("");
		}

		protected override ObjRecord CreateObjRecord()
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.ObjectType = CommonObjectType.Text;
			commonObjectDataSubRecord.IsLocked = true;
			commonObjectDataSubRecord.IsPrintable = true;
			commonObjectDataSubRecord.IsAutoFill = true;
			commonObjectDataSubRecord.IsAutoline = true;
			EndSubRecord o = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			return objRecord;
		}

		protected override EscherContainerRecord CreateSpContainer()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			EscherTextboxRecord escherTextboxRecord = new EscherTextboxRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 3234;
			escherSpRecord.Flags = 2560;
			escherOptRecord.RecordId = -4085;
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(128, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(133, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(135, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(959, 524288));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(129, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(131, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(130, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(132, 0));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(462, 0));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(511, 524296));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(459, 9525));
			escherOptRecord.SetEscherProperty(new EscherRGBProperty(385, 134217737));
			escherOptRecord.SetEscherProperty(new EscherRGBProperty(448, 134217792));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(447, 65536));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(959, 524288));
			EscherRecord escherAnchor = base.Anchor.GetEscherAnchor();
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherTextboxRecord.RecordId = -4083;
			escherTextboxRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(escherAnchor);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			escherContainerRecord.AddChildRecord(escherTextboxRecord);
			return escherContainerRecord;
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

		internal override HSSFShape CloneShape()
		{
			TextObjectRecord textObjectRecord = (GetTextObjectRecord() == null) ? null : ((TextObjectRecord)GetTextObjectRecord().CloneViaReserialise());
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			byte[] data = GetEscherContainer().Serialize();
			escherContainerRecord.FillFields(data, 0, new DefaultEscherRecordFactory());
			ObjRecord objRecord = (ObjRecord)GetObjRecord().CloneViaReserialise();
			return new HSSFTextbox(escherContainerRecord, objRecord, textObjectRecord);
		}

		internal override void AfterRemove(HSSFPatriarch patriarch)
		{
			patriarch.getBoundAggregate().RemoveShapeToObjRecord(GetEscherContainer().GetChildById(-4079));
			patriarch.getBoundAggregate().RemoveShapeToObjRecord(GetEscherContainer().GetChildById(-4083));
		}
	}
}
