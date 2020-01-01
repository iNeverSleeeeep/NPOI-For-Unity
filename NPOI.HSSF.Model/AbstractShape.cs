using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// An abstract shape Is the lowlevel model for a shape.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Obsolete]
	public abstract class AbstractShape
	{
		/// <summary>
		/// The shape container and it's children that can represent this
		/// shape.
		/// </summary>
		/// <value>The sp container.</value>
		public abstract EscherContainerRecord SpContainer
		{
			get;
		}

		/// <summary>
		/// The object record that Is associated with this shape.
		/// </summary>
		/// <value>The obj record.</value>
		public abstract ObjRecord ObjRecord
		{
			get;
		}

		/// <summary>
		/// Create a new shape object used to Create the escher records.
		/// </summary>
		/// <param name="hssfShape">The simple shape this Is based on.</param>
		/// <param name="shapeId">The shape id.</param>
		/// <returns></returns>
		public static AbstractShape CreateShape(HSSFShape hssfShape, int shapeId)
		{
			AbstractShape abstractShape;
			if (hssfShape is HSSFComment)
			{
				abstractShape = new CommentShape((HSSFComment)hssfShape, shapeId);
			}
			else if (hssfShape is HSSFTextbox)
			{
				abstractShape = new TextboxShape((HSSFTextbox)hssfShape, shapeId);
			}
			else if (hssfShape is HSSFPolygon)
			{
				abstractShape = new PolygonShape((HSSFPolygon)hssfShape, shapeId);
			}
			else
			{
				if (!(hssfShape is HSSFSimpleShape))
				{
					throw new ArgumentException("Unknown shape type");
				}
				HSSFSimpleShape hSSFSimpleShape = (HSSFSimpleShape)hssfShape;
				switch (hSSFSimpleShape.ShapeType)
				{
				case 75:
					abstractShape = new PictureShape(hSSFSimpleShape, shapeId);
					break;
				case 20:
					abstractShape = new LineShape(hSSFSimpleShape, shapeId);
					break;
				case 1:
				case 3:
					abstractShape = new SimpleFilledShape(hSSFSimpleShape, shapeId);
					break;
				case 201:
					abstractShape = new ComboboxShape(hSSFSimpleShape, shapeId);
					break;
				default:
					throw new ArgumentException("Do not know how to handle this type of shape");
				}
			}
			EscherSpRecord escherSpRecord = (EscherSpRecord)abstractShape.SpContainer.GetChildById(-4086);
			if (hssfShape.Parent != null)
			{
				escherSpRecord.Flags |= 2;
			}
			return abstractShape;
		}

		/// <summary>
		/// Creates an escher anchor record from a HSSFAnchor.
		/// </summary>
		/// <param name="userAnchor">The high level anchor to Convert.</param>
		/// <returns>An escher anchor record.</returns>
		protected virtual EscherRecord CreateAnchor(HSSFAnchor userAnchor)
		{
			return ConvertAnchor.CreateAnchor(userAnchor);
		}

		/// <summary>
		/// Add standard properties to the opt record.  These properties effect
		/// all records.
		/// </summary>
		/// <param name="shape">The user model shape.</param>
		/// <param name="opt">The opt record to Add the properties to.</param>
		/// <returns>The number of options Added.</returns>
		protected virtual int AddStandardOptions(HSSFShape shape, EscherOptRecord opt)
		{
			opt.AddEscherProperty(new EscherBoolProperty(191, 524288));
			if (shape.IsNoFill)
			{
				opt.AddEscherProperty(new EscherBoolProperty(447, 1114112));
			}
			else
			{
				opt.AddEscherProperty(new EscherBoolProperty(447, 65536));
			}
			opt.AddEscherProperty(new EscherRGBProperty(385, shape.FillColor));
			opt.AddEscherProperty(new EscherBoolProperty(959, 524288));
			opt.AddEscherProperty(new EscherRGBProperty(448, shape.LineStyleColor));
			int num = 5;
			if (shape.LineWidth != 9525)
			{
				opt.AddEscherProperty(new EscherSimpleProperty(459, shape.LineWidth));
				num++;
			}
			if (shape.LineStyle != 0)
			{
				opt.AddEscherProperty(new EscherSimpleProperty(462, (int)shape.LineStyle));
				opt.AddEscherProperty(new EscherSimpleProperty(471, 0));
				if (shape.LineStyle == LineStyle.None)
				{
					opt.AddEscherProperty(new EscherBoolProperty(511, 524288));
				}
				else
				{
					opt.AddEscherProperty(new EscherBoolProperty(511, 524296));
				}
				num += 3;
			}
			opt.SortProperties();
			return num;
		}

		/// <summary>
		///  Generate id for the CommonObjectDataSubRecord that stands behind this shape
		/// </summary>
		/// <param name="shapeId">shape id as generated by drawing manager</param>
		/// <returns>object id that will be assigned to the Obj record</returns>
		protected virtual int GetCmoObjectId(int shapeId)
		{
			return shapeId - 1024;
		}
	}
}
