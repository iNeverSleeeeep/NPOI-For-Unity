using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using System;

namespace NPOI.XSSF.UserModel.Charts
{
	public class AbstractXSSFChartSerie : IChartSerie
	{
		private string titleValue;

		private CellReference titleRef;

		private TitleType titleType;

		protected bool IsTitleSet
		{
			get
			{
				return true;
			}
		}

		public void SetTitle(string title)
		{
			titleType = TitleType.String;
			titleValue = title;
		}

		public void SetTitle(CellReference titleReference)
		{
			titleType = TitleType.CellReference;
			titleRef = titleReference;
		}

		public string GetTitleString()
		{
			if (titleType == TitleType.String)
			{
				return titleValue;
			}
			throw new InvalidOperationException("Title type is not String.");
		}

		public CellReference GetTitleCellReference()
		{
			if (titleType == TitleType.CellReference)
			{
				return titleRef;
			}
			throw new InvalidOperationException("Title type is not CellReference.");
		}

		public TitleType GetTitleType()
		{
			return titleType;
		}

		protected CT_SerTx GetCTSerTx()
		{
			CT_SerTx cT_SerTx = new CT_SerTx();
			switch (titleType)
			{
			case TitleType.CellReference:
				cT_SerTx.AddNewStrRef().f = titleRef.FormatAsString();
				return cT_SerTx;
			case TitleType.String:
				cT_SerTx.v = titleValue;
				return cT_SerTx;
			default:
				throw new InvalidOperationException("Unkown title type: " + titleType);
			}
		}
	}
}
