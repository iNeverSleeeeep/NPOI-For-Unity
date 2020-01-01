using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// Represents a cell comment.
	/// This class Converts highlevel model data from HSSFComment
	/// to low-level records.
	/// @author Yegor Kozlov
	/// </summary>
	[Obsolete]
	public class CommentShape : TextboxShape
	{
		private NoteRecord note;

		/// <summary>
		/// Gets the NoteRecord holding the comment attributes
		/// </summary>
		/// <value>The NoteRecord</value>
		public NoteRecord NoteRecord => note;

		/// <summary>
		/// Creates the low-level records for a comment.
		/// </summary>
		/// <param name="hssfShape">The highlevel shape.</param>
		/// <param name="shapeId">The shape id to use for this shape.</param>
		public CommentShape(HSSFComment hssfShape, int shapeId)
			: base(hssfShape, shapeId)
		{
			note = CreateNoteRecord(hssfShape, shapeId);
			ObjRecord objRecord = ObjRecord;
			List<SubRecord> subRecords = objRecord.SubRecords;
			int num = 0;
			for (int i = 0; i < subRecords.Count; i++)
			{
				object obj = subRecords[i];
				if (obj is CommonObjectDataSubRecord)
				{
					CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)obj;
					commonObjectDataSubRecord.IsAutoFill = false;
					num = i;
				}
			}
			NoteStructureSubRecord element = new NoteStructureSubRecord();
			objRecord.AddSubRecord(num + 1, element);
		}

		/// <summary>
		/// Creates the low level NoteRecord
		/// which holds the comment attributes.
		/// </summary>
		/// <param name="shape">The shape.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		private NoteRecord CreateNoteRecord(HSSFComment shape, int shapeId)
		{
			NoteRecord noteRecord = new NoteRecord();
			noteRecord.Column = shape.Column;
			noteRecord.Row = shape.Row;
			noteRecord.Flags = (short)(shape.Visible ? 2 : 0);
			noteRecord.ShapeId = shapeId;
			noteRecord.Author = ((shape.Author == null) ? "" : shape.Author);
			return noteRecord;
		}

		/// <summary>
		/// Sets standard escher options for a comment.
		/// This method is responsible for Setting default background,
		/// shading and other comment properties.
		/// </summary>
		/// <param name="shape">The highlevel shape.</param>
		/// <param name="opt">The escher records holding the proerties</param>
		/// <returns>The number of escher options added</returns>
		protected override int AddStandardOptions(HSSFShape shape, EscherOptRecord opt)
		{
			base.AddStandardOptions(shape, opt);
			for (int i = 0; i < opt.EscherProperties.Count; i++)
			{
				EscherProperty escherProperty = opt.EscherProperties[i];
				switch (escherProperty.Id)
				{
				case 129:
				case 130:
				case 131:
				case 132:
				case 387:
				case 448:
				case 959:
					opt.EscherProperties.Remove(escherProperty);
					i--;
					break;
				}
			}
			HSSFComment hSSFComment = (HSSFComment)shape;
			opt.AddEscherProperty(new EscherSimpleProperty(959, hSSFComment.Visible ? 655360 : 655362));
			opt.AddEscherProperty(new EscherSimpleProperty(575, 196611));
			opt.AddEscherProperty(new EscherSimpleProperty(513, 0));
			opt.SortProperties();
			return opt.EscherProperties.Count;
		}

		protected override int GetCmoObjectId(int shapeId)
		{
			return shapeId;
		}
	}
}
