using System.Drawing;

namespace NPOI.SS.Format
{
	/// This object Contains the result of Applying a cell format or cell format part
	/// to a value.
	///
	/// @author Ken Arnold, Industrious Media LLC
	/// @see CellFormatPart#Apply(Object)
	/// @see CellFormat#Apply(Object)
	public class CellFormatResult
	{
		private bool _applies;

		private string _text;

		private Color _textcolor;

		/// This is <tt>true</tt> if no condition was given that applied to the
		/// value, or if the condition is satisfied.  If a condition is relevant, and
		/// when applied the value fails the test, this is <tt>false</tt>.
		public bool Applies
		{
			get
			{
				return _applies;
			}
			set
			{
				_applies = value;
			}
		}

		/// The resulting text.  This will never be <tt>null</tt>. 
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}

		/// The color the format Sets, or <tt>null</tt> if the format Sets no color.
		/// This will always be <tt>null</tt> if {@link #applies} is <tt>false</tt>.
		public Color TextColor
		{
			get
			{
				return _textcolor;
			}
			set
			{
				_textcolor = value;
			}
		}

		/// Creates a new format result object.
		///
		/// @param applies   The value for {@link #applies}.
		/// @param text      The value for {@link #text}.
		/// @param textColor The value for {@link #textColor}.
		public CellFormatResult(bool applies, string text, Color textColor)
		{
			Applies = applies;
			Text = text;
			TextColor = (applies ? textColor : Color.Empty);
		}
	}
}
