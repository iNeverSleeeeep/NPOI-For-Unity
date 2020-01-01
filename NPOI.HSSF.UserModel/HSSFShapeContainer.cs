using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// An interface that indicates whether a class can contain children.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public interface HSSFShapeContainer : IEnumerable<HSSFShape>, IEnumerable
	{
		/// <summary>
		/// Gets Any children contained by this shape.
		/// </summary>
		/// <value>The children.</value>
		IList<HSSFShape> Children
		{
			get;
		}

		/// <summary>
		/// Get the top left x coordinate of this group.
		/// </summary>
		int X1
		{
			get;
		}

		/// <summary>
		/// Get the top left y coordinate of this group.
		/// </summary>
		int Y1
		{
			get;
		}

		/// <summary>
		/// Get the bottom right x coordinate of this group.
		/// </summary>
		int X2
		{
			get;
		}

		/// <summary>
		/// Get the bottom right y coordinate of this group.
		/// </summary>
		int Y2
		{
			get;
		}

		/// <summary>
		/// dd shape to the list of child records
		/// </summary>
		/// <param name="shape">shape</param>
		void AddShape(HSSFShape shape);

		/// <summary>
		/// set coordinates of this group relative to the parent
		/// </summary>
		/// <param name="x1">x1</param>
		/// <param name="y1">y1</param>
		/// <param name="x2">x2</param>
		/// <param name="y2">y2</param>
		void SetCoordinates(int x1, int y1, int x2, int y2);

		void Clear();

		/// remove first level shapes
		/// @param shape to be removed
		/// @return true if shape is removed else return false
		bool RemoveShape(HSSFShape shape);
	}
}
