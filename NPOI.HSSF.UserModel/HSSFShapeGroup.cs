using NPOI.DDF;
using NPOI.HSSF.Record;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// A shape Group may contain other shapes.  It was no actual form on the
	/// sheet.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class HSSFShapeGroup : HSSFShape, HSSFShapeContainer, IEnumerable<HSSFShape>, IEnumerable
	{
		private List<HSSFShape> shapes = new List<HSSFShape>();

		private EscherSpgrRecord _spgrRecord;

		/// <summary>
		/// Return all children contained by this shape.
		/// </summary>
		/// <value></value>
		public IList<HSSFShape> Children => shapes;

		/// <summary>
		/// Gets The top left x coordinate of this Group.
		/// </summary>
		/// <value>The x1.</value>
		public int X1 => _spgrRecord.RectX1;

		/// <summary>
		/// Gets The top left y coordinate of this Group.
		/// </summary>
		/// <value>The y1.</value>
		public int Y1 => _spgrRecord.RectY1;

		/// <summary>
		/// Gets The bottom right x coordinate of this Group.
		/// </summary>
		/// <value>The x2.</value>
		public int X2 => _spgrRecord.RectX2;

		/// <summary>
		/// Gets the bottom right y coordinate of this Group.
		/// </summary>
		/// <value>The y2.</value>
		public int Y2 => _spgrRecord.RectY2;

		/// <summary>
		/// Count of all children and their childrens children.
		/// </summary>
		/// <value></value>
		public override int CountOfAllChildren
		{
			get
			{
				int num = shapes.Count;
				IEnumerator enumerator = shapes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					HSSFShape hSSFShape = (HSSFShape)enumerator.Current;
					num += hSSFShape.CountOfAllChildren;
				}
				return num;
			}
		}

		public override int ShapeId
		{
			get
			{
				EscherContainerRecord escherContainerRecord = (EscherContainerRecord)GetEscherContainer().GetChildById(-4092);
				return ((EscherSpRecord)escherContainerRecord.GetChildById(-4086)).ShapeId;
			}
			set
			{
				EscherContainerRecord escherContainerRecord = (EscherContainerRecord)GetEscherContainer().GetChildById(-4092);
				EscherSpRecord escherSpRecord = (EscherSpRecord)escherContainerRecord.GetChildById(-4086);
				escherSpRecord.ShapeId = value;
				CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)GetObjRecord().SubRecords[0];
				commonObjectDataSubRecord.ObjectId = (short)(value % 1024);
			}
		}

		public HSSFShapeGroup(EscherContainerRecord spgrContainer, ObjRecord objRecord)
			: base(spgrContainer, objRecord)
		{
			EscherContainerRecord escherContainerRecord = spgrContainer.ChildContainers[0];
			_spgrRecord = (EscherSpgrRecord)escherContainerRecord.GetChild(0);
			foreach (EscherRecord childRecord in escherContainerRecord.ChildRecords)
			{
				switch (childRecord.RecordId)
				{
				case -4080:
					anchor = new HSSFClientAnchor((EscherClientAnchorRecord)childRecord);
					break;
				case -4081:
					anchor = new HSSFChildAnchor((EscherChildAnchorRecord)childRecord);
					break;
				}
			}
		}

		public HSSFShapeGroup(HSSFShape parent, HSSFAnchor anchor)
			: base(parent, anchor)
		{
			_spgrRecord = (EscherSpgrRecord)((EscherContainerRecord)GetEscherContainer().GetChild(0)).GetChildById(-4087);
		}

		protected override EscherContainerRecord CreateSpContainer()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherContainerRecord escherContainerRecord2 = new EscherContainerRecord();
			EscherSpgrRecord escherSpgrRecord = new EscherSpgrRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherContainerRecord.RecordId = -4093;
			escherContainerRecord.Options = 15;
			escherContainerRecord2.RecordId = -4092;
			escherContainerRecord2.Options = 15;
			escherSpgrRecord.RecordId = -4087;
			escherSpgrRecord.Options = 1;
			escherSpgrRecord.RectX1 = 0;
			escherSpgrRecord.RectY1 = 0;
			escherSpgrRecord.RectX2 = 1023;
			escherSpgrRecord.RectY2 = 255;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 2;
			if (base.Anchor is HSSFClientAnchor)
			{
				escherSpRecord.Flags = 513;
			}
			else
			{
				escherSpRecord.Flags = 515;
			}
			escherOptRecord.RecordId = -4085;
			escherOptRecord.Options = 35;
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(127, 262148));
			escherOptRecord.AddEscherProperty(new EscherBoolProperty(959, 524288));
			EscherRecord escherAnchor = base.Anchor.GetEscherAnchor();
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherContainerRecord.AddChildRecord(escherContainerRecord2);
			escherContainerRecord2.AddChildRecord(escherSpgrRecord);
			escherContainerRecord2.AddChildRecord(escherSpRecord);
			escherContainerRecord2.AddChildRecord(escherOptRecord);
			escherContainerRecord2.AddChildRecord(escherAnchor);
			escherContainerRecord2.AddChildRecord(escherClientDataRecord);
			return escherContainerRecord;
		}

		protected override ObjRecord CreateObjRecord()
		{
			ObjRecord objRecord = new ObjRecord();
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.ObjectType = CommonObjectType.Group;
			commonObjectDataSubRecord.IsLocked = true;
			commonObjectDataSubRecord.IsPrintable = true;
			commonObjectDataSubRecord.IsAutoFill = true;
			commonObjectDataSubRecord.IsAutoline = true;
			GroupMarkerSubRecord o = new GroupMarkerSubRecord();
			EndSubRecord o2 = new EndSubRecord();
			objRecord.AddSubRecord(commonObjectDataSubRecord);
			objRecord.AddSubRecord(o);
			objRecord.AddSubRecord(o2);
			return objRecord;
		}

		internal override void AfterRemove(HSSFPatriarch patriarch)
		{
			patriarch.GetBoundAggregate().RemoveShapeToObjRecord(GetEscherContainer().ChildContainers[0].GetChildById(-4079));
			for (int i = 0; i < shapes.Count; i++)
			{
				HSSFShape hSSFShape = shapes[i];
				RemoveShape(hSSFShape);
				hSSFShape.AfterRemove(base.Patriarch);
			}
			shapes.Clear();
		}

		private void OnCreate(HSSFShape shape)
		{
			if (base.Patriarch != null)
			{
				EscherContainerRecord escherContainer = shape.GetEscherContainer();
				int num2 = shape.ShapeId = base.Patriarch.NewShapeId();
				GetEscherContainer().AddChildRecord(escherContainer);
				shape.AfterInsert(base.Patriarch);
				EscherSpRecord escherSpRecord = (!(shape is HSSFShapeGroup)) ? ((EscherSpRecord)shape.GetEscherContainer().GetChildById(-4086)) : ((EscherSpRecord)shape.GetEscherContainer().ChildContainers[0].GetChildById(-4086));
				escherSpRecord.Flags |= 2;
			}
		}

		/// <summary>
		/// Create another Group Under this Group.
		/// </summary>
		/// <param name="anchor">the position of the new Group.</param>
		/// <returns>the Group</returns>
		public HSSFShapeGroup CreateGroup(HSSFChildAnchor anchor)
		{
			HSSFShapeGroup hSSFShapeGroup = new HSSFShapeGroup(this, anchor);
			hSSFShapeGroup.Parent = this;
			hSSFShapeGroup.Anchor = anchor;
			shapes.Add(hSSFShapeGroup);
			OnCreate(hSSFShapeGroup);
			return hSSFShapeGroup;
		}

		public void AddShape(HSSFShape shape)
		{
			shape.Patriarch = base.Patriarch;
			shape.Parent = this;
			shapes.Add(shape);
		}

		/// <summary>
		/// Create a new simple shape Under this Group.
		/// </summary>
		/// <param name="anchor">the position of the shape.</param>
		/// <returns>the shape</returns>
		public HSSFSimpleShape CreateShape(HSSFChildAnchor anchor)
		{
			HSSFSimpleShape hSSFSimpleShape = new HSSFSimpleShape(this, anchor);
			hSSFSimpleShape.Parent = this;
			hSSFSimpleShape.Anchor = anchor;
			shapes.Add(hSSFSimpleShape);
			OnCreate(hSSFSimpleShape);
			EscherSpRecord escherSpRecord = (EscherSpRecord)hSSFSimpleShape.GetEscherContainer().GetChildById(-4086);
			if (hSSFSimpleShape.Anchor.IsHorizontallyFlipped)
			{
				escherSpRecord.Flags |= 64;
			}
			if (hSSFSimpleShape.Anchor.IsVerticallyFlipped)
			{
				escherSpRecord.Flags |= 128;
			}
			return hSSFSimpleShape;
		}

		/// <summary>
		/// Create a new textbox Under this Group.
		/// </summary>
		/// <param name="anchor">the position of the shape.</param>
		/// <returns>the textbox</returns>
		public HSSFTextbox CreateTextbox(HSSFChildAnchor anchor)
		{
			HSSFTextbox hSSFTextbox = new HSSFTextbox(this, anchor);
			hSSFTextbox.Parent = this;
			hSSFTextbox.Anchor = anchor;
			shapes.Add(hSSFTextbox);
			OnCreate(hSSFTextbox);
			return hSSFTextbox;
		}

		/// <summary>
		/// Creates a polygon
		/// </summary>
		/// <param name="anchor">the client anchor describes how this Group Is attached
		/// to the sheet.</param>
		/// <returns>the newly Created shape.</returns>
		public HSSFPolygon CreatePolygon(HSSFChildAnchor anchor)
		{
			HSSFPolygon hSSFPolygon = new HSSFPolygon(this, anchor);
			hSSFPolygon.Parent = this;
			hSSFPolygon.Anchor = anchor;
			shapes.Add(hSSFPolygon);
			OnCreate(hSSFPolygon);
			return hSSFPolygon;
		}

		/// <summary>
		/// Creates a picture.
		/// </summary>
		/// <param name="anchor">the client anchor describes how this Group Is attached
		/// to the sheet.</param>
		/// <param name="pictureIndex">Index of the picture.</param>
		/// <returns>the newly Created shape.</returns>
		public HSSFPicture CreatePicture(HSSFChildAnchor anchor, int pictureIndex)
		{
			HSSFPicture hSSFPicture = new HSSFPicture(this, anchor);
			hSSFPicture.Parent = this;
			hSSFPicture.Anchor = anchor;
			hSSFPicture.PictureIndex = pictureIndex;
			shapes.Add(hSSFPicture);
			OnCreate(hSSFPicture);
			EscherSpRecord escherSpRecord = (EscherSpRecord)hSSFPicture.GetEscherContainer().GetChildById(-4086);
			if (hSSFPicture.Anchor.IsHorizontallyFlipped)
			{
				escherSpRecord.Flags |= 64;
			}
			if (hSSFPicture.Anchor.IsVerticallyFlipped)
			{
				escherSpRecord.Flags |= 128;
			}
			return hSSFPicture;
		}

		/// <summary>
		/// Sets the coordinate space of this Group.  All children are constrained
		/// to these coordinates.
		/// </summary>
		/// <param name="x1">The x1.</param>
		/// <param name="y1">The y1.</param>
		/// <param name="x2">The x2.</param>
		/// <param name="y2">The y2.</param>
		public void SetCoordinates(int x1, int y1, int x2, int y2)
		{
			_spgrRecord.RectX1 = x1;
			_spgrRecord.RectX2 = x2;
			_spgrRecord.RectY1 = y1;
			_spgrRecord.RectY2 = y2;
		}

		public void Clear()
		{
			List<HSSFShape> list = new List<HSSFShape>(shapes);
			foreach (HSSFShape item in list)
			{
				RemoveShape(item);
			}
		}

		internal override void AfterInsert(HSSFPatriarch patriarch)
		{
			EscherAggregate boundAggregate = patriarch.GetBoundAggregate();
			EscherContainerRecord escherContainerRecord = (EscherContainerRecord)GetEscherContainer().GetChildById(-4092);
			boundAggregate.AssociateShapeToObjRecord(escherContainerRecord.GetChildById(-4079), GetObjRecord());
		}

		internal override HSSFShape CloneShape()
		{
			throw new NotImplementedException("Use method cloneShape(HSSFPatriarch patriarch)");
		}

		internal HSSFShape CloneShape(HSSFPatriarch patriarch)
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			escherContainerRecord.RecordId = -4093;
			escherContainerRecord.Options = 15;
			EscherContainerRecord escherContainerRecord2 = new EscherContainerRecord();
			EscherContainerRecord escherContainerRecord3 = (EscherContainerRecord)GetEscherContainer().GetChildById(-4092);
			byte[] data = escherContainerRecord3.Serialize();
			escherContainerRecord2.FillFields(data, 0, new DefaultEscherRecordFactory());
			escherContainerRecord.AddChildRecord(escherContainerRecord2);
			ObjRecord objRecord = null;
			if (GetObjRecord() != null)
			{
				objRecord = (ObjRecord)GetObjRecord().CloneViaReserialise();
			}
			HSSFShapeGroup hSSFShapeGroup = new HSSFShapeGroup(escherContainerRecord, objRecord);
			hSSFShapeGroup.Patriarch = patriarch;
			foreach (HSSFShape child in Children)
			{
				HSSFShape shape = (!(child is HSSFShapeGroup)) ? child.CloneShape() : ((HSSFShapeGroup)child).CloneShape(patriarch);
				hSSFShapeGroup.AddShape(shape);
				hSSFShapeGroup.OnCreate(shape);
			}
			return hSSFShapeGroup;
		}

		public bool RemoveShape(HSSFShape shape)
		{
			bool flag = GetEscherContainer().RemoveChildRecord(shape.GetEscherContainer());
			if (flag)
			{
				shape.AfterRemove(base.Patriarch);
				shapes.Remove(shape);
			}
			return flag;
		}

		public IEnumerator<HSSFShape> GetEnumerator()
		{
			return shapes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return shapes.GetEnumerator();
		}
	}
}
