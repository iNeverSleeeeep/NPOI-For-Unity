using NPOI.DDF;
using NPOI.HSSF.Record;
using System;

namespace NPOI.HSSF.UserModel
{
	public class HSSFCombobox : HSSFSimpleShape
	{
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

		public HSSFCombobox(EscherContainerRecord spContainer, ObjRecord objRecord)
			: base(spContainer, objRecord)
		{
		}

		public HSSFCombobox(HSSFShape parent, HSSFAnchor anchor)
			: base(parent, anchor)
		{
			base.ShapeType = 201;
			CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)GetObjRecord().SubRecords[0];
			commonObjectDataSubRecord.ObjectType = CommonObjectType.ComboBox;
		}

		protected override TextObjectRecord CreateTextObjRecord()
		{
			return null;
		}

		protected override EscherContainerRecord CreateSpContainer()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 3218;
			escherSpRecord.Flags = 2560;
			escherOptRecord.RecordId = -4085;
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(127, 17039620));
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(191, 524296));
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(511, 524288));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(959, 131072));
			HSSFClientAnchor hSSFClientAnchor = (HSSFClientAnchor)base.Anchor;
			hSSFClientAnchor.AnchorType = 1;
			EscherRecord escherAnchor = hSSFClientAnchor.GetEscherAnchor();
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(escherAnchor);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			return escherContainerRecord;
		}

		protected override ObjRecord CreateObjRecord()
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.ObjectType = CommonObjectType.ComboBox;
			commonObjectDataSubRecord.IsLocked = true;
			commonObjectDataSubRecord.IsPrintable = false;
			commonObjectDataSubRecord.IsAutoFill = true;
			commonObjectDataSubRecord.IsAutoline = false;
			FtCblsSubRecord o = new FtCblsSubRecord();
			LbsDataSubRecord o2 = LbsDataSubRecord.CreateAutoFilterInstance();
			EndSubRecord o3 = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			objRecord.AddSubRecord(o2);
			objRecord.AddSubRecord(o3);
			return objRecord;
		}
	}
}
