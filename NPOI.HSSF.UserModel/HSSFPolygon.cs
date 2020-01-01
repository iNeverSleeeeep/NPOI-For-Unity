using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.Util;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// @author Glen Stampoultzis  (glens at baselinksoftware.com)
	/// </summary>
	public class HSSFPolygon : HSSFSimpleShape
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(HSSFPolygon));

		public new static short OBJECT_TYPE_MICROSOFT_OFFICE_DRAWING = 30;

		/// @return array of x coordinates
		public int[] XPoints
		{
			get
			{
				EscherArrayProperty escherArrayProperty = (EscherArrayProperty)GetOptRecord().Lookup(325);
				if (escherArrayProperty == null)
				{
					return new int[0];
				}
				int[] array = new int[escherArrayProperty.NumberOfElementsInArray - 1];
				for (int i = 0; i < escherArrayProperty.NumberOfElementsInArray - 1; i++)
				{
					byte[] element = escherArrayProperty.GetElement(i);
					short num = (short)(array[i] = LittleEndian.GetShort(element, 0));
				}
				return array;
			}
		}

		/// @return array of y coordinates
		public int[] YPoints
		{
			get
			{
				EscherArrayProperty escherArrayProperty = (EscherArrayProperty)GetOptRecord().Lookup(325);
				if (escherArrayProperty == null)
				{
					return new int[0];
				}
				int[] array = new int[escherArrayProperty.NumberOfElementsInArray - 1];
				for (int i = 0; i < escherArrayProperty.NumberOfElementsInArray - 1; i++)
				{
					byte[] element = escherArrayProperty.GetElement(i);
					short num = (short)(array[i] = LittleEndian.GetShort(element, 2));
				}
				return array;
			}
		}

		/// @return shape width
		public int DrawAreaWidth => ((EscherSimpleProperty)GetOptRecord().Lookup(322))?.PropertyValue ?? 100;

		/// @return shape height
		public int DrawAreaHeight => ((EscherSimpleProperty)GetOptRecord().Lookup(323))?.PropertyValue ?? 100;

		public HSSFPolygon(EscherContainerRecord spContainer, ObjRecord objRecord, TextObjectRecord _textObjectRecord)
			: base(spContainer, objRecord, _textObjectRecord)
		{
		}

		public HSSFPolygon(EscherContainerRecord spContainer, ObjRecord objRecord)
			: base(spContainer, objRecord)
		{
		}

		public HSSFPolygon(HSSFShape parent, HSSFAnchor anchor)
			: base(parent, anchor)
		{
		}

		protected override TextObjectRecord CreateTextObjRecord()
		{
			return null;
		}

		/// Generates the shape records for this shape.
		protected override EscherContainerRecord CreateSpContainer()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 2;
			if (base.Parent == null)
			{
				escherSpRecord.Flags = 2560;
			}
			else
			{
				escherSpRecord.Flags = 2562;
			}
			escherOptRecord.RecordId = -4085;
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(4, isComplex: false, isBlipId: false, 0));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(322, isComplex: false, isBlipId: false, 100));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(323, isComplex: false, isBlipId: false, 100));
			escherOptRecord.SetEscherProperty(new EscherShapePathProperty(324, 4));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(383, isComplex: false, isBlipId: false, 65537));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(464, isComplex: false, isBlipId: false, 0));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(465, isComplex: false, isBlipId: false, 0));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(471, isComplex: false, isBlipId: false, 0));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(462, 0));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(511, 524296));
			escherOptRecord.SetEscherProperty(new EscherSimpleProperty(459, 9525));
			escherOptRecord.SetEscherProperty(new EscherRGBProperty(385, 134217737));
			escherOptRecord.SetEscherProperty(new EscherRGBProperty(448, 134217792));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(447, 1));
			escherOptRecord.SetEscherProperty(new EscherBoolProperty(959, 524288));
			EscherRecord escherAnchor = base.Anchor.GetEscherAnchor();
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(escherAnchor);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			return escherContainerRecord;
		}

		/// Creates the low level OBJ record for this shape.
		protected override ObjRecord CreateObjRecord()
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.ObjectType = CommonObjectType.MicrosoftOfficeDrawing;
			commonObjectDataSubRecord.IsLocked = true;
			commonObjectDataSubRecord.IsPrintable = true;
			commonObjectDataSubRecord.IsAutoFill = true;
			commonObjectDataSubRecord.IsAutoline = true;
			EndSubRecord o = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			return objRecord;
		}

		internal override void AfterRemove(HSSFPatriarch patriarch)
		{
			patriarch.getBoundAggregate().RemoveShapeToObjRecord(GetEscherContainer().GetChildById(-4079));
		}

		/// @param xPoints - array of x coordinates
		/// @param yPoints - array of y coordinates
		public void SetPoints(int[] xPoints, int[] yPoints)
		{
			if (xPoints.Length != yPoints.Length)
			{
				logger.Log(7, "xPoint.Length must be equal to yPoints.Length");
			}
			else
			{
				if (xPoints.Length == 0)
				{
					logger.Log(7, "HSSFPolygon must have at least one point");
				}
				EscherArrayProperty escherArrayProperty = new EscherArrayProperty(325, isBlipId: false, new byte[0]);
				escherArrayProperty.NumberOfElementsInArray = xPoints.Length + 1;
				escherArrayProperty.NumberOfElementsInMemory = xPoints.Length + 1;
				escherArrayProperty.SizeOfElements = -16;
				byte[] array;
				for (int i = 0; i < xPoints.Length; i++)
				{
					array = new byte[4];
					LittleEndian.PutShort(array, 0, (short)xPoints[i]);
					LittleEndian.PutShort(array, 2, (short)yPoints[i]);
					escherArrayProperty.SetElement(i, array);
				}
				int index = xPoints.Length;
				array = new byte[4];
				LittleEndian.PutShort(array, 0, (short)xPoints[0]);
				LittleEndian.PutShort(array, 2, (short)yPoints[0]);
				escherArrayProperty.SetElement(index, array);
				SetPropertyValue(escherArrayProperty);
				EscherArrayProperty escherArrayProperty2 = new EscherArrayProperty(326, isBlipId: false, null);
				escherArrayProperty2.SizeOfElements = 2;
				escherArrayProperty2.NumberOfElementsInArray = xPoints.Length * 2 + 4;
				escherArrayProperty2.NumberOfElementsInMemory = xPoints.Length * 2 + 4;
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
				for (int j = 0; j < xPoints.Length; j++)
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
				SetPropertyValue(escherArrayProperty2);
			}
		}

		/// Defines the width and height of the points in the polygon
		/// @param width
		/// @param height
		public void SetPolygonDrawArea(int width, int height)
		{
			SetPropertyValue(new EscherSimpleProperty(322, width));
			SetPropertyValue(new EscherSimpleProperty(323, height));
		}
	}
}
