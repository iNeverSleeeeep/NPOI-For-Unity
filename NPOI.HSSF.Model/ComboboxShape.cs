using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using System;

namespace NPOI.HSSF.Model
{
	[Obsolete]
	public class ComboboxShape : AbstractShape
	{
		private EscherContainerRecord spContainer;

		private ObjRecord objRecord;

		public override EscherContainerRecord SpContainer => spContainer;

		public override ObjRecord ObjRecord => objRecord;

		/// Creates the low evel records for a combobox.
		///
		/// @param hssfShape The highlevel shape.
		/// @param shapeId   The shape id to use for this shape.
		public ComboboxShape(HSSFSimpleShape hssfShape, int shapeId)
		{
			spContainer = CreateSpContainer(hssfShape, shapeId);
		}

		/// Generates the escher shape records for this shape.
		private EscherContainerRecord CreateSpContainer(HSSFSimpleShape shape, int shapeId)
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 3218;
			escherSpRecord.ShapeId = shapeId;
			escherSpRecord.Flags = 2560;
			escherOptRecord.RecordId = -4085;
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(127, 17039620));
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(191, 524296));
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(511, 524288));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(959, 131072));
			HSSFClientAnchor hSSFClientAnchor = (HSSFClientAnchor)shape.Anchor;
			hSSFClientAnchor.AnchorType = 1;
			EscherRecord record = CreateAnchor(hSSFClientAnchor);
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(record);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			return escherContainerRecord;
		}
	}
}
