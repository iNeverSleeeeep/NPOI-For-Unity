using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using System;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// Represents an textbox shape and Converts between the highlevel records
	/// and lowlevel records for an oval.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Obsolete]
	public class TextboxShape : AbstractShape
	{
		private EscherContainerRecord spContainer;

		private TextObjectRecord textObjectRecord;

		private ObjRecord objRecord;

		private EscherTextboxRecord escherTextbox;

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
		/// The TextObject record that is associated with this shape.
		/// </summary>
		/// <value></value>
		public TextObjectRecord TextObjectRecord => textObjectRecord;

		/// <summary>
		/// Gets the EscherTextbox record.
		/// </summary>
		/// <value>The EscherTextbox record.</value>
		public EscherRecord EscherTextbox => escherTextbox;

		/// <summary>
		/// Creates the low evel records for a textbox.
		/// </summary>
		/// <param name="hssfShape">The highlevel shape.</param>
		/// <param name="shapeId">The shape id to use for this shape.</param>
		public TextboxShape(HSSFTextbox hssfShape, int shapeId)
		{
			spContainer = CreateSpContainer(hssfShape, shapeId);
			objRecord = CreateObjRecord(hssfShape, shapeId);
			textObjectRecord = CreateTextObjectRecord(hssfShape, shapeId);
		}

		/// <summary>
		/// Creates the lowerlevel OBJ records for this shape.
		/// </summary>
		/// <param name="hssfShape">The HSSF shape.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		private ObjRecord CreateObjRecord(HSSFTextbox hssfShape, int shapeId)
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

		/// <summary>
		/// Creates the lowerlevel escher records for this shape.
		/// </summary>
		/// <param name="hssfShape">The HSSF shape.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		private EscherContainerRecord CreateSpContainer(HSSFTextbox hssfShape, int shapeId)
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			EscherOptRecord escherOptRecord = new EscherOptRecord();
			EscherRecord escherRecord = new EscherClientAnchorRecord();
			EscherClientDataRecord escherClientDataRecord = new EscherClientDataRecord();
			escherTextbox = new EscherTextboxRecord();
			escherContainerRecord.RecordId = -4092;
			escherContainerRecord.Options = 15;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 3234;
			escherSpRecord.ShapeId = shapeId;
			escherSpRecord.Flags = 2560;
			escherOptRecord.RecordId = -4085;
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(128, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(129, hssfShape.MarginLeft));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(131, hssfShape.MarginRight));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(132, hssfShape.MarginBottom));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(130, hssfShape.MarginTop));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(133, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(135, 0));
			escherOptRecord.AddEscherProperty(new EscherSimpleProperty(959, 524288));
			AddStandardOptions(hssfShape, escherOptRecord);
			HSSFAnchor anchor = hssfShape.Anchor;
			escherRecord = CreateAnchor(anchor);
			escherClientDataRecord.RecordId = -4079;
			escherClientDataRecord.Options = 0;
			escherTextbox.RecordId = -4083;
			escherTextbox.Options = 0;
			escherContainerRecord.AddChildRecord(escherSpRecord);
			escherContainerRecord.AddChildRecord(escherOptRecord);
			escherContainerRecord.AddChildRecord(escherRecord);
			escherContainerRecord.AddChildRecord(escherClientDataRecord);
			escherContainerRecord.AddChildRecord(escherTextbox);
			return escherContainerRecord;
		}

		/// <summary>
		/// Textboxes also have an extra TXO record associated with them that most
		/// other shapes dont have.
		/// </summary>
		/// <param name="hssfShape">The HSSF shape.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		private TextObjectRecord CreateTextObjectRecord(HSSFTextbox hssfShape, int shapeId)
		{
			TextObjectRecord textObjectRecord = new TextObjectRecord();
			textObjectRecord.HorizontalTextAlignment = hssfShape.HorizontalAlignment;
			textObjectRecord.VerticalTextAlignment = hssfShape.VerticalAlignment;
			textObjectRecord.IsTextLocked = true;
			textObjectRecord.TextOrientation = TextOrientation.None;
			int numFormattingRun = hssfShape.String.NumFormattingRuns;
			textObjectRecord.Str = hssfShape.String;
			return textObjectRecord;
		}
	}
}
