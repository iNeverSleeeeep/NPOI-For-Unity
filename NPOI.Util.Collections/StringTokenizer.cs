using System;

namespace NPOI.Util.Collections
{
	public class StringTokenizer
	{
		private int pos;

		private string str;

		private int len;

		private string delim;

		private bool retDelims;

		public StringTokenizer(string str)
			: this(str, " \t\n\r\f", retDelims: false)
		{
		}

		public StringTokenizer(string str, string delim)
			: this(str, delim, retDelims: false)
		{
		}

		public StringTokenizer(string str, string delim, bool retDelims)
		{
			len = str.Length;
			this.str = str;
			this.delim = delim;
			this.retDelims = retDelims;
			pos = 0;
		}

		public bool HasMoreTokens()
		{
			if (!retDelims)
			{
				while (pos < len && delim.IndexOf(str[pos]) >= 0)
				{
					pos++;
				}
			}
			return pos < len;
		}

		public string NextToken(string delim)
		{
			this.delim = delim;
			return NextToken();
		}

		public string NextToken()
		{
			if (pos < len && delim.IndexOf(str[pos]) >= 0)
			{
				if (retDelims)
				{
					return str.Substring(pos++, 1);
				}
				while (++pos < len && delim.IndexOf(str[pos]) >= 0)
				{
				}
			}
			if (pos < len)
			{
				int num = pos;
				while (++pos < len && delim.IndexOf(str[pos]) < 0)
				{
				}
				return str.Substring(num, pos - num);
			}
			throw new IndexOutOfRangeException();
		}

		public int CountTokens()
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			int i = pos;
			while (i < len)
			{
				if (delim.IndexOf(str[i++]) >= 0)
				{
					if (flag)
					{
						num++;
						flag = false;
					}
					num2++;
				}
				else
				{
					flag = true;
					for (; i < len && delim.IndexOf(str[i]) < 0; i++)
					{
					}
				}
			}
			if (flag)
			{
				num++;
			}
			if (!retDelims)
			{
				return num;
			}
			return num + num2;
		}
	}
}
