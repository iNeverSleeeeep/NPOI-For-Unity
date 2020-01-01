using System;
using System.Globalization;
using System.Text;

namespace NPOI.HSSF.Util
{
	/// * Title:        Range Address 
	/// * Description:  provides connectivity utilities for ranges
	/// *
	/// *
	/// * REFERENCE:  
	/// * @author IgOr KaTz &amp; EuGeNe BuMaGiN (Tal Moshaiov) (VistaPortal LDT.)
	///                 @version 1.0
	public class RangeAddress
	{
		private const int WRONG_POS = -1;

		private const int MAX_HEIGHT = 66666;

		private string m_sheetName;

		private string m_cellFrom;

		private string m_cellTo;

		/// @return String <b>note: </b> All absolute references are Removed
		public string Address
		{
			get
			{
				string text = "";
				if (m_sheetName != null)
				{
					text = text + m_sheetName + "!";
				}
				if (m_cellFrom != null)
				{
					text += m_cellFrom;
					if (m_cellTo != null)
					{
						text = text + ":" + m_cellTo;
					}
				}
				return text;
			}
		}

		public string SheetName => m_sheetName;

		public string Range
		{
			get
			{
				string text = "";
				if (m_cellFrom != null)
				{
					text += m_cellFrom;
					if (m_cellTo != null)
					{
						text = text + ":" + m_cellTo;
					}
				}
				return text;
			}
		}

		public string FromCell => m_cellFrom;

		public string ToCell => m_cellTo;

		public int Width
		{
			get
			{
				if (m_cellFrom != null && m_cellTo != null)
				{
					int xPosition = GetXPosition(m_cellTo);
					int xPosition2 = GetXPosition(m_cellFrom);
					if (xPosition == -1 || xPosition2 == -1)
					{
						return 0;
					}
					return xPosition - xPosition2 + 1;
				}
				return 0;
			}
		}

		public int Height
		{
			get
			{
				if (m_cellFrom != null && m_cellTo != null)
				{
					int yPosition = GetYPosition(m_cellTo);
					int yPosition2 = GetYPosition(m_cellFrom);
					if (yPosition == -1 || yPosition2 == -1)
					{
						return 0;
					}
					return yPosition - yPosition2 + 1;
				}
				return 0;
			}
		}

		public bool HasSheetName
		{
			get
			{
				if (m_sheetName == null)
				{
					return false;
				}
				return true;
			}
		}

		public bool HasRange
		{
			get
			{
				if (m_cellFrom != null && m_cellTo != null)
				{
					return !m_cellFrom.Equals(m_cellTo);
				}
				return false;
			}
		}

		public bool HasCell
		{
			get
			{
				if (m_cellFrom == null)
				{
					return false;
				}
				return true;
			}
		}

		/// Accepts an external reference from excel.
		///
		/// i.e. Sheet1!$A$4:$B$9
		/// @param _url
		public RangeAddress(string _url)
		{
			init(_url);
		}

		public RangeAddress(int _startCol, int _startRow, int _endCol, int _endRow)
		{
			init(NumTo26Sys(_startCol) + _startRow + ":" + NumTo26Sys(_endCol) + _endRow);
		}

		public bool IsCellOk(string _cell)
		{
			if (_cell != null)
			{
				if (GetYPosition(_cell) != -1 && GetXPosition(_cell) != -1)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public bool IsSheetNameOk()
		{
			return IsSheetNameOk(m_sheetName);
		}

		private static bool intern_isSheetNameOk(string _sheetName, bool _canBeWaitSpace)
		{
			foreach (char c in _sheetName)
			{
				if (!char.IsLetterOrDigit(c) && c != '_' && (!_canBeWaitSpace || c != ' '))
				{
					return false;
				}
			}
			return true;
		}

		public static bool IsSheetNameOk(string _sheetName)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(_sheetName))
			{
				return intern_isSheetNameOk(_sheetName, _canBeWaitSpace: true);
			}
			return true;
		}

		public void SetSize(int _width, int _height)
		{
			if (m_cellFrom == null)
			{
				m_cellFrom = "a1";
			}
			int xPosition = GetXPosition(m_cellFrom);
			int yPosition = GetYPosition(m_cellFrom);
			m_cellTo = NumTo26Sys(xPosition + _width - 1);
			m_cellTo += (yPosition + _height - 1).ToString(CultureInfo.InvariantCulture);
		}

		private void init(string _url)
		{
			_url = RemoveString(_url, "$");
			_url = RemoveString(_url, "'");
			string[] array = ParseURL(_url);
			m_sheetName = array[0];
			m_cellFrom = array[1];
			m_cellTo = array[2];
			if (m_cellTo == null)
			{
				m_cellTo = m_cellFrom;
			}
			m_cellTo = RemoveString(m_cellTo, ".");
		}

		private string[] ParseURL(string _url)
		{
			string[] array = new string[3];
			int num = _url.IndexOf(':');
			if (num >= 0)
			{
				string text = _url.Substring(0, num);
				string text2 = _url.Substring(num + 1);
				num = text.IndexOf('!');
				if (num >= 0)
				{
					array[0] = text.Substring(0, num);
					array[1] = text.Substring(num + 1);
				}
				else
				{
					array[1] = text;
				}
				num = text2.IndexOf('!');
				if (num >= 0)
				{
					array[2] = text2.Substring(num + 1);
				}
				else
				{
					array[2] = text2;
				}
			}
			else
			{
				num = _url.IndexOf('!');
				if (num >= 0)
				{
					array[0] = _url.Substring(0, num);
					array[1] = _url.Substring(num + 1);
				}
				else
				{
					array[1] = _url;
				}
			}
			return array;
		}

		public int GetYPosition(string _subrange)
		{
			int result = -1;
			_subrange = _subrange.Trim();
			if (_subrange.Length != 0)
			{
				string digitPart = GetDigitPart(_subrange);
				try
				{
					result = int.Parse(digitPart, CultureInfo.InvariantCulture);
					if (result > 66666)
					{
						return -1;
					}
					return result;
				}
				catch (Exception)
				{
					return -1;
				}
			}
			return result;
		}

		private static bool IsLetter(string _str)
		{
			bool result = true;
			if (!string.IsNullOrEmpty(_str))
			{
				foreach (char c in _str)
				{
					if (!char.IsLetter(c))
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		public int GetXPosition(string _subrange)
		{
			int result = -1;
			string text = Filter(_subrange);
			text = GetCharPart(_subrange);
			if (IsLetter(text) && (text.Length == 2 || text.Length == 1))
			{
				result = Get26Sys(text);
			}
			return result;
		}

		public string GetDigitPart(string _value)
		{
			string result = "";
			int firstDigitPosition = GetFirstDigitPosition(_value);
			if (firstDigitPosition >= 0)
			{
				result = _value.Substring(firstDigitPosition);
			}
			return result;
		}

		public string GetCharPart(string _value)
		{
			string result = "";
			int firstDigitPosition = GetFirstDigitPosition(_value);
			if (firstDigitPosition >= 0)
			{
				result = _value.Substring(0, firstDigitPosition);
			}
			return result;
		}

		private string Filter(string _range)
		{
			string text = "";
			foreach (char c in _range)
			{
				if (c != '$')
				{
					text += c;
				}
			}
			return text;
		}

		private int GetFirstDigitPosition(string _value)
		{
			int result = -1;
			if (_value != null && _value.Trim().Length == 0)
			{
				return result;
			}
			_value = _value.Trim();
			int length = _value.Length;
			for (int i = 0; i < length; i++)
			{
				if (char.IsDigit(_value[i]))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		public int Get26Sys(string _s)
		{
			int num = 0;
			int num2 = 1;
			if (!string.IsNullOrEmpty(_s))
			{
				for (int num3 = _s.Length - 1; num3 >= 0; num3--)
				{
					char c = _s[num3];
					int num4 = (int)(char.GetNumericValue(c) - char.GetNumericValue('A') + 1.0);
					num += num4 * num2;
					num2 *= 26;
				}
				return num;
			}
			return -1;
		}

		public string NumTo26Sys(int _num)
		{
			string text = "";
			do
			{
				_num--;
				int num = _num % 26;
				int num2 = 65 + num;
				_num /= 26;
				text = (char)num2 + text;
			}
			while (_num > 0);
			return text;
		}

		public string ReplaceString(string _source, string _oldPattern, string _newPattern)
		{
			StringBuilder stringBuilder = new StringBuilder(_source);
			stringBuilder = stringBuilder.Replace(_oldPattern, _newPattern);
			return stringBuilder.ToString();
		}

		public string RemoveString(string _source, string _match)
		{
			return ReplaceString(_source, _match, "");
		}
	}
}
