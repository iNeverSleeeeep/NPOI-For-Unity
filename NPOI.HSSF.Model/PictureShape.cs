using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// Represents a picture shape and Creates all specific low level records.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class PictureShape : AbstractShape
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
		/// <param name="hssfShape">The user model shape.</param>
		/// <param name="shapeId">The identifier to use for this shape.</param>
		public PictureShape(HSSFSimpleShape hssfShape, int shapeId)
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
			HSSFPicture hSSFPicture = (HSSFPicture)hssfShape;
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 1202;
			escherSpRecord.ShapeId = shapeId;
			escherSpRecord.Flags = 2560;
			escherOptRecord.RecordId = -4085;
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(260, isComplex: false, isBlipId: true, hSSFPicture.PictureIndex));
			AddStandardOptions(hSSFPicture, escherOptRecord);
			HSSFAnchor anchor = hSSFPicture.Anchor;
			if (anchor.IsHorizontallyFlipped)
			{
				escherSpRecord.Flags |= 64;
			}
			if (anchor.IsVerticallyFlipped)
			{
				escherSpRecord.Flags |= 128;
			}
			EscherRecord record = CreateAnchor(anchor);
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(record);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			return escherContainerRecord;
		}

		/// <summary>
		/// Creates the low level OBJ record for this shape.
		/// </summary>
		/// <param name="hssfShape">The HSSFShape.</param>
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
			commonObjectDataSubRecord.Reserved2 = 0;
			EndSubRecord o = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			return objRecord;
		}
	}
}
