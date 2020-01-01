using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using System;

namespace NPOI.HSSF.Model
{
	[Obsolete]
	public class SimpleFilledShape : AbstractShape
	{
		private EscherContainerRecord spContainer;

		private ObjRecord objRecord;

		/// <summary>
		/// The shape container and it's children that can represent this
		/// shape.
		/// </summary>
		/// <value></value>
		public override EscherContainerRecord SpContainer => spContainer;

		/// <summary>
		/// The object record that is associated with this shape.
		/// </summary>
		/// <value></value>
		public override ObjRecord ObjRecord => objRecord;

		/// <summary>
		/// Creates the low evel records for an oval.
		/// </summary>
		/// <param name="hssfShape">The highlevel shape.</param>
		/// <param name="shapeId">The shape id to use for this shape.</param>
		public SimpleFilledShape(HSSFSimpleShape hssfShape, int shapeId)
		{
			spContainer = CreateSpContainer(hssfShape, shapeId);
			objRecord = CreateObjRecord(hssfShape, shapeId);
		}

		/// <summary>
		/// Creates the lowerlevel escher records for this shape.
		/// </summary>
		/// <param name="hssfShape">The HSSF shape.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		private EscherContainerRecord CreateSpContainer(HSSFSimpleShape hssfShape, int shapeId)
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			short num = objTypeToShapeType(hssfShape.ShapeType);
			escherSpRecord.Options = (short)((num << 4) | 2);
			escherSpRecord.ShapeId = shapeId;
			escherSpRecord.Flags = 2560;
			escherOptRecord.RecordId = -4085;
			AddStandardOptions(hssfShape, escherOptRecord);
			EscherRecord record = CreateAnchor(hssfShape.Anchor);
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(record);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			return escherContainerRecord;
		}

		private short objTypeToShapeType(int objType)
		{
			switch (objType)
			{
			case 3:
				return 3;
			case 1:
				return 1;
			default:
				throw new ArgumentException("Unable to handle an object of this type");
			}
		}

		/// <summary>
		/// Creates the lowerlevel OBJ records for this shape.
		/// </summary>
		/// <param name="hssfShape">The HSSF shape.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		private ObjRecord CreateObjRecord(HSSFShape hssfShape, int shapeId)
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.ObjectType = (CommonObjectType)((HSSFSimpleShape)hssfShape).ShapeType;
			commonObjectDataSubRecord.ObjectId = shapeId;
			commonObjectDataSubRecord.IsLocked = true;
			commonObjectDataSubRecord.IsPrintable = true;
			commonObjectDataSubRecord.IsAutoFill = true;
			commonObjectDataSubRecord.IsAutoline = true;
			EndSubRecord o = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			return objRecord;
		}
	}
}
