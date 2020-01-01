using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.Util;
using System;

namespace NPOI.HSSF.Model
{
	[Obsolete]
	public class PolygonShape : AbstractShape
	{
		public const short OBJECT_TYPE_MICROSOFT_OFFICE_DRAWING = 30;

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
		/// Creates the low evel records for an polygon.
		/// </summary>
		/// <param name="hssfShape">The highlevel shape.</param>
		/// <param name="shapeId">The shape id to use for this shape.</param>
		public PolygonShape(HSSFPolygon hssfShape, int shapeId)
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
		private EscherContainerRecord CreateSpContainer(HSSFPolygon hssfShape, int shapeId)
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 2;
			escherSpRecord.ShapeId = shapeId;
			if (hssfShape.Parent == null)
			{
				escherSpRecord.Flags = 2560;
			}
			else
			{
				escherSpRecord.Flags = 2562;
			}
			escherOptRecord.RecordId = -4085;
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(4, isComplex: false, isBlipId: false, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(322, isComplex: false, isBlipId: false, hssfShape.DrawAreaWidth));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(323, isComplex: false, isBlipId: false, hssfShape.DrawAreaHeight));
			escherOptRecord.AddEscherProperty(new EscherShapePathProperty(324, 4));
			EscherArrayProperty escherArrayProperty = new EscherArrayProperty(325, isBlipId: false, new byte[0]);
			escherArrayProperty.NumberOfElementsInArray = hssfShape.XPoints.Length + 1;
			escherArrayProperty.NumberOfElementsInMemory = hssfShape.XPoints.Length + 1;
			escherArrayProperty.SizeOfElements = -16;
			for (int i = 0; i < hssfShape.XPoints.Length; i++)
			{
				byte[] array = new byte[4];
				LittleEndian.PutShort(array, 0, (short)hssfShape.XPoints[i]);
				LittleEndian.PutShort(array, 2, (short)hssfShape.YPoints[i]);
				escherArrayProperty.SetElement(i, array);
			}
			int index = hssfShape.XPoints.Length;
			byte[] array2 = new byte[4];
			LittleEndian.PutShort(array2, 0, (short)hssfShape.XPoints[0]);
			LittleEndian.PutShort(array2, 2, (short)hssfShape.YPoints[0]);
			escherArrayProperty.SetElement(index, array2);
			escherOptRecord.AddEscherProperty(escherArrayProperty);
			EscherArrayProperty escherArrayProperty2 = new EscherArrayProperty(326, isBlipId: false, null);
			escherArrayProperty2.SizeOfElements = 2;
			escherArrayProperty2.NumberOfElementsInArray = hssfShape.XPoints.Length * 2 + 4;
			escherArrayProperty2.NumberOfElementsInMemory = hssfShape.XPoints.Length * 2 + 4;
			escherArrayProperty2.SetElement(0, new byte[2]
			{
				0,
				64
			});
			escherArrayProperty2.SetElement(1, new byte[2]
			{
				0,
				172
			});
			for (int j = 0; j < hssfShape.XPoints.Length; j++)
			{
				escherArrayProperty2.SetElement(2 + j * 2, new byte[2]
				{
					1,
					0
				});
				escherArrayProperty2.SetElement(3 + j * 2, new byte[2]
				{
					0,
					172
				});
			}
			escherArrayProperty2.SetElement(escherArrayProperty2.NumberOfElementsInArray - 2, new byte[2]
			{
				1,
				96
			});
			escherArrayProperty2.SetElement(escherArrayProperty2.NumberOfElementsInArray - 1, new byte[2]
			{
				0,
				128
			});
			escherOptRecord.AddEscherProperty(escherArrayProperty2);
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(383, isComplex: false, isBlipId: false, 65537));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(464, isComplex: false, isBlipId: false, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(465, isComplex: false, isBlipId: false, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(471, isComplex: false, isBlipId: false, 0));
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
			commonObjectDataSubRecord.ObjectType = CommonObjectType.MicrosoftOfficeDrawing;
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
