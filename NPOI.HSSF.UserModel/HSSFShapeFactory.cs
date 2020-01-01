using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.POIFS.FileSystem;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	/// @author Evgeniy Berlog
	/// date: 05.06.12
	public class HSSFShapeFactory
	{
		private const short OBJECT_TYPE_LINE = 1;

		private const short OBJECT_TYPE_RECTANGLE = 2;

		private const short OBJECT_TYPE_OVAL = 3;

		private const short OBJECT_TYPE_ARC = 4;

		private const short OBJECT_TYPE_PICTURE = 8;

		/// build shape tree from escher container
		/// @param container root escher container from which escher records must be taken
		/// @param agg - EscherAggregate
		/// @param out - shape container to which shapes must be added
		/// @param root - node to create HSSFObjectData shapes
		public static void CreateShapeTree(EscherContainerRecord container, EscherAggregate agg, HSSFShapeContainer out1, DirectoryNode root)
		{
			if (container.RecordId == -4093)
			{
				ObjRecord objRecord = null;
				EscherClientDataRecord escherClientDataRecord = (EscherClientDataRecord)((EscherContainerRecord)container.GetChild(0)).GetChildById(-4079);
				if (escherClientDataRecord != null)
				{
					objRecord = (ObjRecord)agg.GetShapeToObjMapping()[escherClientDataRecord];
				}
				HSSFShapeGroup hSSFShapeGroup = new HSSFShapeGroup(container, objRecord);
				IList<EscherContainerRecord> childContainers = container.ChildContainers;
				for (int i = 0; i < childContainers.Count; i++)
				{
					EscherContainerRecord container2 = childContainers[i];
					if (i != 0)
					{
						CreateShapeTree(container2, agg, hSSFShapeGroup, root);
					}
				}
				out1.AddShape(hSSFShapeGroup);
			}
			else if (container.RecordId == -4092)
			{
				Dictionary<EscherRecord, NPOI.HSSF.Record.Record> shapeToObjMapping = agg.GetShapeToObjMapping();
				ObjRecord objRecord2 = null;
				TextObjectRecord textObjectRecord = null;
				foreach (EscherRecord childRecord in container.ChildRecords)
				{
					switch (childRecord.RecordId)
					{
					case -4079:
						objRecord2 = (ObjRecord)shapeToObjMapping[childRecord];
						break;
					case -4083:
						textObjectRecord = (TextObjectRecord)shapeToObjMapping[childRecord];
						break;
					}
				}
				if (IsEmbeddedObject(objRecord2))
				{
					HSSFObjectData shape = new HSSFObjectData(container, objRecord2, root);
					out1.AddShape(shape);
				}
				else
				{
					CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)objRecord2.SubRecords[0];
					HSSFShape shape2;
					switch (commonObjectDataSubRecord.ObjectType)
					{
					case CommonObjectType.Picture:
						shape2 = new HSSFPicture(container, objRecord2);
						break;
					case CommonObjectType.Rectangle:
						shape2 = new HSSFSimpleShape(container, objRecord2, textObjectRecord);
						break;
					case CommonObjectType.Line:
						shape2 = new HSSFSimpleShape(container, objRecord2);
						break;
					case CommonObjectType.ComboBox:
						shape2 = new HSSFCombobox(container, objRecord2);
						break;
					case CommonObjectType.MicrosoftOfficeDrawing:
					{
						EscherOptRecord escherOptRecord = (EscherOptRecord)container.GetChildById(-4085);
						EscherProperty escherProperty = escherOptRecord.Lookup(325);
						shape2 = ((escherProperty == null) ? new HSSFSimpleShape(container, objRecord2, textObjectRecord) : new HSSFPolygon(container, objRecord2, textObjectRecord));
						break;
					}
					case CommonObjectType.Text:
						shape2 = new HSSFTextbox(container, objRecord2, textObjectRecord);
						break;
					case CommonObjectType.Comment:
						shape2 = new HSSFComment(container, objRecord2, textObjectRecord, agg.GetNoteRecordByObj(objRecord2));
						break;
					default:
						shape2 = new HSSFSimpleShape(container, objRecord2, textObjectRecord);
						break;
					}
					out1.AddShape(shape2);
				}
			}
		}

		private static bool IsEmbeddedObject(ObjRecord obj)
		{
			foreach (SubRecord subRecord in obj.SubRecords)
			{
				if (subRecord is EmbeddedObjectRefSubRecord)
				{
					return true;
				}
			}
			return false;
		}
	}
}
