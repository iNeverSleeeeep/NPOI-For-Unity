using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using System;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// Represents a line shape and Creates all the line specific low level records.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Obsolete]
	public class LineShape : AbstractShape
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
		/// Creates the line shape from the highlevel user shape.  All low level
		/// records are Created at this point.
		/// </summary>
		/// <param name="hssfShape">The user model shape</param>
		/// <param name="shapeId">The identifier to use for this shape.</param>
		public LineShape(HSSFSimpleShape hssfShape, int shapeId)
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
			EscherRecord escherRecord = new EscherClientAnchorRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 322;
			escherSpRecord.ShapeId = shapeId;
			escherSpRecord.Flags = 2560;
			escherOptRecord.RecordId = -4085;
			escherOptRecord.AddEscherProperty(new EscherShapePathProperty(324, 4));
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(511, 1048592));
			AddStandardOptions(hssfShape, escherOptRecord);
			HSSFAnchor anchor = hssfShape.Anchor;
			if (anchor.IsHorizontallyFlipped)
			{
				escherSpRecord.Flags |= 64;
			}
			if (anchor.IsVerticallyFlipped)
			{
				escherSpRecord.Flags |= 128;
			}
			escherRecord = CreateAnchor(anchor);
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(escherRecord);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			return escherContainerRecord;
		}

		/// <summary>
		/// Creates the low level OBJ record for this shape.
		/// </summary>
		/// <param name="hssfShape">The HSSF shape.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		private ObjRecord CreateObjRecord(HSSFShape hssfShape, int shapeId)
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.ObjectType = (CommonObjectType)((HSSFSimpleShape)hssfShape).ShapeType;
			commonObjectDataSubRecord.ObjectId = GetCmoObjectId(shapeId);
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
