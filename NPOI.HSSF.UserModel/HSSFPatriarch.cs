using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// The patriarch is the toplevel container for shapes in a sheet.  It does
	/// little other than act as a container for other shapes and Groups.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class HSSFPatriarch : HSSFShapeContainer, IEnumerable<HSSFShape>, IEnumerable, IDrawing
	{
		private static POILogger log = POILogFactory.GetLogger(typeof(HSSFPatriarch));

		private List<HSSFShape> _shapes = new List<HSSFShape>();

		private HSSFSheet _sheet;

		private EscherSpgrRecord _spgrRecord;

		private EscherContainerRecord _mainSpgrContainer;

		/// The EscherAggregate we have been bound to.
		/// (This will handle writing us out into records,
		///  and building up our shapes from the records)
		private EscherAggregate _boundAggregate;

		/// <summary>
		/// Returns a list of all shapes contained by the patriarch.
		/// </summary>
		/// <value>The children.</value>
		public IList<HSSFShape> Children => _shapes;

		/// <summary>
		/// Total count of all children and their children's children.
		/// </summary>
		/// <value>The count of all children.</value>
		public int CountOfAllChildren
		{
			get
			{
				int num = _shapes.Count;
				IEnumerator enumerator = _shapes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					HSSFShape hSSFShape = (HSSFShape)enumerator.Current;
					num += hSSFShape.CountOfAllChildren;
				}
				return num;
			}
		}

		/// <summary>
		/// The top left x coordinate of this Group.
		/// </summary>
		/// <value>The x1.</value>
		public int X1 => _spgrRecord.RectX1;

		/// <summary>
		/// The top left y coordinate of this Group.
		/// </summary>
		/// <value>The y1.</value>
		public int Y1 => _spgrRecord.RectY1;

		/// <summary>
		/// The bottom right x coordinate of this Group.
		/// </summary>
		/// <value>The x2.</value>
		public int X2 => _spgrRecord.RectX2;

		/// <summary>
		/// The bottom right y coordinate of this Group.
		/// </summary>
		/// <value>The y2.</value>
		public int Y2 => _spgrRecord.RectY2;

		protected internal HSSFSheet Sheet => _sheet;

		/// <summary>
		/// Creates the patriarch.
		/// </summary>
		/// <param name="sheet">the sheet this patriarch is stored in.</param>
		/// <param name="boundAggregate">The bound aggregate.</param>
		public HSSFPatriarch(HSSFSheet sheet, EscherAggregate boundAggregate)
		{
			_boundAggregate = boundAggregate;
			_sheet = sheet;
			_mainSpgrContainer = _boundAggregate.GetEscherContainer().ChildContainers[0];
			EscherContainerRecord escherContainerRecord = (EscherContainerRecord)_boundAggregate.GetEscherContainer().ChildContainers[0].GetChild(0);
			_spgrRecord = (EscherSpgrRecord)escherContainerRecord.GetChildById(-4087);
			BuildShapeTree();
		}

		public static HSSFPatriarch CreatePatriarch(HSSFPatriarch patriarch, HSSFSheet sheet)
		{
			HSSFPatriarch hSSFPatriarch = new HSSFPatriarch(sheet, new EscherAggregate(createDefaultTree: true));
			hSSFPatriarch.AfterCreate();
			foreach (HSSFShape child in patriarch.Children)
			{
				HSSFShape shape = (!(child is HSSFShapeGroup)) ? child.CloneShape() : ((HSSFShapeGroup)child).CloneShape(hSSFPatriarch);
				hSSFPatriarch.OnCreate(shape);
				hSSFPatriarch.AddShape(shape);
			}
			return hSSFPatriarch;
		}

		/// check if any shapes contain wrong data
		/// At now(13.08.2010) check if patriarch contains 2 or more comments with same coordinates
		protected internal void PreSerialize()
		{
			Dictionary<int, NoteRecord> tailRecords = _boundAggregate.TailRecords;
			Hashtable hashtable = new Hashtable(tailRecords.Count);
			foreach (NoteRecord value in tailRecords.Values)
			{
				string text = new CellReference(value.Row, value.Column).FormatAsString();
				if (hashtable.Contains(text))
				{
					throw new InvalidOperationException("found multiple cell comments for cell " + text);
				}
				hashtable.Add(text, null);
			}
		}

		/// @param shape to be removed
		/// @return true of shape is removed
		public bool RemoveShape(HSSFShape shape)
		{
			bool flag = _mainSpgrContainer.RemoveChildRecord(shape.GetEscherContainer());
			if (flag)
			{
				shape.AfterRemove(this);
				_shapes.Remove(shape);
			}
			return flag;
		}

		internal void AfterCreate()
		{
			DrawingManager2 drawingManager = ((HSSFWorkbook)_sheet.Workbook).Workbook.DrawingManager;
			short dgId = drawingManager.FindNewDrawingGroupId();
			_boundAggregate.SetDgId(dgId);
			_boundAggregate.SetMainSpRecordId(NewShapeId());
			drawingManager.IncrementDrawingsSaved();
		}

		/// <summary>
		/// Creates a new Group record stored Under this patriarch.
		/// </summary>
		/// <param name="anchor">the client anchor describes how this Group is attached
		/// to the sheet.</param>
		/// <returns>the newly created Group.</returns>
		public HSSFShapeGroup CreateGroup(HSSFClientAnchor anchor)
		{
			HSSFShapeGroup hSSFShapeGroup = new HSSFShapeGroup(null, anchor);
			AddShape(hSSFShapeGroup);
			OnCreate(hSSFShapeGroup);
			return hSSFShapeGroup;
		}

		/// <summary>
		/// Creates a simple shape.  This includes such shapes as lines, rectangles,
		/// and ovals.
		/// </summary>
		/// <param name="anchor">the client anchor describes how this Group is attached
		/// to the sheet.</param>
		/// <returns>the newly created shape.</returns>
		public HSSFSimpleShape CreateSimpleShape(HSSFClientAnchor anchor)
		{
			HSSFSimpleShape hSSFSimpleShape = new HSSFSimpleShape(null, anchor);
			AddShape(hSSFSimpleShape);
			OnCreate(hSSFSimpleShape);
			return hSSFSimpleShape;
		}

		/// <summary>
		/// Creates a picture.
		/// </summary>
		/// <param name="anchor">the client anchor describes how this Group is attached
		/// to the sheet.</param>
		/// <param name="pictureIndex">Index of the picture.</param>
		/// <returns>the newly created shape.</returns>
		public IPicture CreatePicture(HSSFClientAnchor anchor, int pictureIndex)
		{
			HSSFPicture hSSFPicture = new HSSFPicture(null, anchor);
			hSSFPicture.PictureIndex = pictureIndex;
			AddShape(hSSFPicture);
			OnCreate(hSSFPicture);
			return hSSFPicture;
		}

		/// <summary>
		/// CreatePicture
		/// </summary>
		/// <param name="anchor">the client anchor describes how this picture is attached to the sheet.</param>
		/// <param name="pictureIndex">the index of the picture in the workbook collection of pictures.</param>
		/// <returns>return newly created shape</returns>
		public IPicture CreatePicture(IClientAnchor anchor, int pictureIndex)
		{
			return CreatePicture((HSSFClientAnchor)anchor, pictureIndex);
		}

		/// <summary>
		/// Creates a polygon
		/// </summary>
		/// <param name="anchor">the client anchor describes how this Group is attached
		/// to the sheet.</param>
		/// <returns>the newly Created shape.</returns>
		public HSSFPolygon CreatePolygon(IClientAnchor anchor)
		{
			HSSFPolygon hSSFPolygon = new HSSFPolygon(null, (HSSFAnchor)anchor);
			AddShape(hSSFPolygon);
			OnCreate(hSSFPolygon);
			return hSSFPolygon;
		}

		/// <summary>
		/// Constructs a textbox Under the patriarch.
		/// </summary>
		/// <param name="anchor">the client anchor describes how this Group is attached
		/// to the sheet.</param>
		/// <returns>the newly Created textbox.</returns>
		public HSSFSimpleShape CreateTextbox(IClientAnchor anchor)
		{
			HSSFTextbox hSSFTextbox = new HSSFTextbox(null, (HSSFAnchor)anchor);
			AddShape(hSSFTextbox);
			OnCreate(hSSFTextbox);
			return hSSFTextbox;
		}

		/// Constructs a cell comment.
		///
		/// @param anchor    the client anchor describes how this comment is attached
		///                  to the sheet.
		/// @return      the newly created comment.
		public HSSFComment CreateComment(HSSFAnchor anchor)
		{
			HSSFComment hSSFComment = new HSSFComment(null, anchor);
			AddShape(hSSFComment);
			OnCreate(hSSFComment);
			return hSSFComment;
		}

		/// YK: used to create autofilters
		///
		/// @see org.apache.poi.hssf.usermodel.HSSFSheet#setAutoFilter(int, int, int, int)
		public HSSFSimpleShape CreateComboBox(HSSFAnchor anchor)
		{
			HSSFCombobox hSSFCombobox = new HSSFCombobox(null, anchor);
			AddShape(hSSFCombobox);
			OnCreate(hSSFCombobox);
			return hSSFCombobox;
		}

		/// <summary>
		/// Constructs a cell comment.
		/// </summary>
		/// <param name="anchor">the client anchor describes how this comment is attached
		/// to the sheet.</param>
		/// <returns>the newly created comment.</returns>
		public IComment CreateCellComment(IClientAnchor anchor)
		{
			return CreateComment((HSSFAnchor)anchor);
		}

		private void SetFlipFlags(HSSFShape shape)
		{
			EscherSpRecord escherSpRecord = (EscherSpRecord)shape.GetEscherContainer().GetChildById(-4086);
			if (shape.Anchor.IsHorizontallyFlipped)
			{
				escherSpRecord.Flags |= 64;
			}
			if (shape.Anchor.IsVerticallyFlipped)
			{
				escherSpRecord.Flags |= 128;
			}
		}

		/// add a shape to this drawing
		public void AddShape(HSSFShape shape)
		{
			shape.Patriarch = this;
			_shapes.Add(shape);
		}

		private void OnCreate(HSSFShape shape)
		{
			EscherContainerRecord escherContainerRecord = _boundAggregate.GetEscherContainer().ChildContainers[0];
			EscherContainerRecord escherContainer = shape.GetEscherContainer();
			int num2 = shape.ShapeId = NewShapeId();
			escherContainerRecord.AddChildRecord(escherContainer);
			shape.AfterInsert(this);
			SetFlipFlags(shape);
		}

		/// <summary>
		/// Sets the coordinate space of this Group.  All children are contrained
		/// to these coordinates.
		/// </summary>
		/// <param name="x1">The x1.</param>
		/// <param name="y1">The y1.</param>
		/// <param name="x2">The x2.</param>
		/// <param name="y2">The y2.</param>
		public void SetCoordinates(int x1, int y1, int x2, int y2)
		{
			_spgrRecord.RectY1 = y1;
			_spgrRecord.RectY2 = y2;
			_spgrRecord.RectX1 = x1;
			_spgrRecord.RectX2 = x2;
		}

		public void Clear()
		{
			List<HSSFShape> list = new List<HSSFShape>(_shapes);
			foreach (HSSFShape item in list)
			{
				RemoveShape(item);
			}
		}

		internal int NewShapeId()
		{
			DrawingManager2 drawingManager = ((HSSFWorkbook)_sheet.Workbook).Workbook.DrawingManager;
			EscherDgRecord escherDgRecord = (EscherDgRecord)_boundAggregate.GetEscherContainer().GetChildById(-4088);
			short drawingGroupId = escherDgRecord.DrawingGroupId;
			return drawingManager.AllocateShapeId(drawingGroupId, escherDgRecord);
		}

		/// <summary>
		/// Does this HSSFPatriarch contain a chart?
		/// (Technically a reference to a chart, since they
		/// Get stored in a different block of records)
		/// FIXME - detect chart in all cases (only seems
		/// to work on some charts so far)
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if this instance contains chart; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsChart()
		{
			EscherOptRecord escherOptRecord = (EscherOptRecord)_boundAggregate.FindFirstWithId(-4085);
			if (escherOptRecord == null)
			{
				return false;
			}
			IEnumerator enumerator = escherOptRecord.EscherProperties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherProperty escherProperty = (EscherProperty)enumerator.Current;
				if (escherProperty.PropertyNumber == 896 && escherProperty.IsComplex)
				{
					EscherComplexProperty escherComplexProperty = (EscherComplexProperty)escherProperty;
					string fromUnicodeLE = StringUtil.GetFromUnicodeLE(escherComplexProperty.ComplexData);
					if (fromUnicodeLE.Equals("Chart 1\0"))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Returns the aggregate escher record we're bound to
		/// </summary>
		/// <returns></returns>
		internal EscherAggregate GetBoundAggregate()
		{
			return _boundAggregate;
		}

		internal EscherAggregate getBoundAggregate()
		{
			return _boundAggregate;
		}

		/// Creates a new client anchor and sets the top-left and bottom-right
		/// coordinates of the anchor.
		///
		/// @param dx1  the x coordinate in EMU within the first cell.
		/// @param dy1  the y coordinate in EMU within the first cell.
		/// @param dx2  the x coordinate in EMU within the second cell.
		/// @param dy2  the y coordinate in EMU within the second cell.
		/// @param col1 the column (0 based) of the first cell.
		/// @param row1 the row (0 based) of the first cell.
		/// @param col2 the column (0 based) of the second cell.
		/// @param row2 the row (0 based) of the second cell.
		/// @return the newly created client anchor
		public IClientAnchor CreateAnchor(int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2)
		{
			return new HSSFClientAnchor(dx1, dy1, dx2, dy2, (short)col1, row1, (short)col2, row2);
		}

		public IChart CreateChart(IClientAnchor anchor)
		{
			throw new RuntimeException("NotImplemented");
		}

		/// create shape tree from existing escher records tree
		public void BuildShapeTree()
		{
			EscherContainerRecord escherContainer = _boundAggregate.GetEscherContainer();
			if (escherContainer != null)
			{
				EscherContainerRecord escherContainerRecord = escherContainer.ChildContainers[0];
				IList<EscherContainerRecord> childContainers = escherContainerRecord.ChildContainers;
				for (int i = 0; i < childContainers.Count; i++)
				{
					EscherContainerRecord container = childContainers[i];
					if (i != 0)
					{
						HSSFShapeFactory.CreateShapeTree(container, _boundAggregate, this, ((HSSFWorkbook)_sheet.Workbook).RootDirectory);
					}
				}
			}
		}

		public List<HSSFShape> GetShapes()
		{
			return _shapes;
		}

		public IEnumerator<HSSFShape> GetEnumerator()
		{
			return _shapes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _shapes.GetEnumerator();
		}
	}
}
