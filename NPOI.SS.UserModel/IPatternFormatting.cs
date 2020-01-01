namespace NPOI.SS.UserModel
{
	/// @author Yegor Kozlov
	public interface IPatternFormatting
	{
		short FillBackgroundColor
		{
			get;
			set;
		}

		short FillForegroundColor
		{
			get;
			set;
		}

		short FillPattern
		{
			get;
			set;
		}
	}
}
