using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel.Charts;
using System;

namespace NPOI.XSSF.UserModel.Charts
{
	/// Package private class with utility methods.
	///
	/// @author Roman Kashitsyn
	internal class XSSFChartUtil
	{
		private XSSFChartUtil()
		{
		}

		/// Builds CTAxDataSource object content from POI ChartDataSource.
		/// @param ctAxDataSource OOXML data source to build
		/// @param dataSource POI data source to use
		public static void BuildAxDataSource<T>(CT_AxDataSource ctAxDataSource, IChartDataSource<T> dataSource)
		{
			if (dataSource.IsNumeric)
			{
				if (dataSource.IsReference)
				{
					BuildNumRef(ctAxDataSource.AddNewNumRef(), dataSource);
				}
				else
				{
					BuildNumLit(ctAxDataSource.AddNewNumLit(), dataSource);
				}
			}
			else if (dataSource.IsReference)
			{
				BuildStrRef(ctAxDataSource.AddNewStrRef(), dataSource);
			}
			else
			{
				BuildStrLit(ctAxDataSource.AddNewStrLit(), dataSource);
			}
		}

		/// Builds CTNumDataSource object content from POI ChartDataSource
		/// @param ctNumDataSource OOXML data source to build
		/// @param dataSource POI data source to use
		public static void BuildNumDataSource<T>(CT_NumDataSource ctNumDataSource, IChartDataSource<T> dataSource)
		{
			if (dataSource.IsReference)
			{
				BuildNumRef(ctNumDataSource.AddNewNumRef(), dataSource);
			}
			else
			{
				BuildNumLit(ctNumDataSource.AddNewNumLit(), dataSource);
			}
		}

		private static void BuildNumRef<T>(CT_NumRef ctNumRef, IChartDataSource<T> dataSource)
		{
			ctNumRef.f = dataSource.FormulaString;
			CT_NumData cache = ctNumRef.AddNewNumCache();
			FillNumCache(cache, dataSource);
		}

		private static void BuildNumLit<T>(CT_NumData ctNumData, IChartDataSource<T> dataSource)
		{
			FillNumCache(ctNumData, dataSource);
		}

		private static void BuildStrRef<T>(CT_StrRef ctStrRef, IChartDataSource<T> dataSource)
		{
			ctStrRef.f = dataSource.FormulaString;
			CT_StrData cache = ctStrRef.AddNewStrCache();
			FillStringCache(cache, dataSource);
		}

		private static void BuildStrLit<T>(CT_StrData ctStrData, IChartDataSource<T> dataSource)
		{
			FillStringCache(ctStrData, dataSource);
		}

		private static void FillStringCache<T>(CT_StrData cache, IChartDataSource<T> dataSource)
		{
			int pointCount = dataSource.PointCount;
			cache.AddNewPtCount().val = (uint)pointCount;
			for (int i = 0; i < pointCount; i++)
			{
				object obj = dataSource.GetPointAt(i);
				if (obj != null)
				{
					CT_StrVal cT_StrVal = cache.AddNewPt();
					cT_StrVal.idx = (uint)i;
					cT_StrVal.v = obj.ToString();
				}
			}
		}

		private static void FillNumCache<T>(CT_NumData cache, IChartDataSource<T> dataSource)
		{
			int pointCount = dataSource.PointCount;
			cache.AddNewPtCount().val = (uint)pointCount;
			for (int i = 0; i < pointCount; i++)
			{
				double d = Convert.ToDouble(dataSource.GetPointAt(i));
				if (!double.IsNaN(d))
				{
					CT_NumVal cT_NumVal = cache.AddNewPt();
					cT_NumVal.idx = (uint)i;
					cT_NumVal.v = d.ToString();
				}
			}
		}
	}
}
