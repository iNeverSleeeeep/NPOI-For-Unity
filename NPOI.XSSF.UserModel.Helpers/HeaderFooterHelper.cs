using System.Text;

namespace NPOI.XSSF.UserModel.Helpers
{
	public class HeaderFooterHelper
	{
		private static string HeaderFooterEntity_L = "&L";

		private static string HeaderFooterEntity_C = "&C";

		private static string HeaderFooterEntity_R = "&R";

		public static string HeaderFooterEntity_File = "&F";

		public static string HeaderFooterEntity_Date = "&D";

		public static string HeaderFooterEntity_Time = "&T";

		public string GetLeftSection(string str)
		{
			return GetParts(str)[0];
		}

		public string GetCenterSection(string str)
		{
			return GetParts(str)[1];
		}

		public string GetRightSection(string str)
		{
			return GetParts(str)[2];
		}

		public string SetLeftSection(string str, string newLeft)
		{
			string[] parts = GetParts(str);
			parts[0] = newLeft;
			return JoinParts(parts);
		}

		public string SetCenterSection(string str, string newCenter)
		{
			string[] parts = GetParts(str);
			parts[1] = newCenter;
			return JoinParts(parts);
		}

		public string SetRightSection(string str, string newRight)
		{
			string[] parts = GetParts(str);
			parts[2] = newRight;
			return JoinParts(parts);
		}

		/// Split into left, center, right
		private string[] GetParts(string str)
		{
			string[] array = new string[3]
			{
				"",
				"",
				""
			};
			if (str == null)
			{
				return array;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while ((num = str.IndexOf(HeaderFooterEntity_L)) > -2 && (num2 = str.IndexOf(HeaderFooterEntity_C)) > -2 && (num3 = str.IndexOf(HeaderFooterEntity_R)) > -2 && (num > -1 || num2 > -1 || num3 > -1))
			{
				if (num3 > num2 && num3 > num)
				{
					array[2] = str.Substring(num3 + HeaderFooterEntity_R.Length);
					str = str.Substring(0, num3);
				}
				else if (num2 > num3 && num2 > num)
				{
					array[1] = str.Substring(num2 + HeaderFooterEntity_C.Length);
					str = str.Substring(0, num2);
				}
				else
				{
					array[0] = str.Substring(num + HeaderFooterEntity_L.Length);
					str = str.Substring(0, num);
				}
			}
			return array;
		}

		private string JoinParts(string[] parts)
		{
			return JoinParts(parts[0], parts[1], parts[2]);
		}

		private string JoinParts(string l, string c, string r)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (c.Length > 0)
			{
				stringBuilder.Append(HeaderFooterEntity_C);
				stringBuilder.Append(c);
			}
			if (l.Length > 0)
			{
				stringBuilder.Append(HeaderFooterEntity_L);
				stringBuilder.Append(l);
			}
			if (r.Length > 0)
			{
				stringBuilder.Append(HeaderFooterEntity_R);
				stringBuilder.Append(r);
			}
			return stringBuilder.ToString();
		}
	}
}
