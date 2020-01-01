namespace NPOI.SS.UserModel
{
	/// @author Dmitriy Kumshayev
	/// @author Yegor Kozlov
	public interface IBorderFormatting
	{
		BorderStyle BorderBottom
		{
			get;
			set;
		}

		BorderStyle BorderDiagonal
		{
			get;
			set;
		}

		BorderStyle BorderLeft
		{
			get;
			set;
		}

		BorderStyle BorderRight
		{
			get;
			set;
		}

		BorderStyle BorderTop
		{
			get;
			set;
		}

		short BottomBorderColor
		{
			get;
			set;
		}

		short DiagonalBorderColor
		{
			get;
			set;
		}

		short LeftBorderColor
		{
			get;
			set;
		}

		short RightBorderColor
		{
			get;
			set;
		}

		short TopBorderColor
		{
			get;
			set;
		}
	}
}
