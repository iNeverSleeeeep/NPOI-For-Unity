using System;
using System.Collections;
using System.IO;
using System.Text;

namespace NPOI.Util.Collections
{
	/// <summary>
	/// This class comes from Java
	/// </summary>
	public class Properties
	{
		private const string whiteSpaceChars = " \t\r\n\f";

		private const string keyValueSeparators = "=: \t\r\n\f";

		private const string strictKeyValueSeparators = "=:";

		private Hashtable _col;

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count => _col.Count;

		/// <summary>
		/// Gets or sets the <see cref="T:System.String" /> with the specified key.
		/// </summary>
		/// <value></value>
		public virtual string this[string key]
		{
			get
			{
				return (string)_col[key];
			}
			set
			{
				_col[key] = value;
			}
		}

		/// <summary>
		/// Gets the keys.
		/// </summary>
		/// <value>The keys.</value>
		public ICollection Keys => _col.Keys;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.Util.Collections.Properties" /> class.
		/// </summary>
		public Properties()
		{
			_col = new Hashtable();
		}

		/// <summary>
		/// Removes the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public string Remove(string key)
		{
			string result = (string)_col[key];
			_col.Remove(key);
			return result;
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return _col.GetEnumerator();
		}

		/// <summary>
		/// Determines whether the specified key contains key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		/// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsKey(string key)
		{
			return _col.ContainsKey(key);
		}

		/// <summary>
		/// Adds the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public virtual void Add(string key, string value)
		{
			_col[key] = value;
		}

		public void AddAll(Properties col)
		{
			foreach (string key in col.Keys)
			{
				_col[key] = col[key];
			}
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear()
		{
			_col.Clear();
		}

		/// <summary>
		/// Loads the specified in stream.
		/// </summary>
		/// <param name="inStream">The in stream.</param>
		public void Load(Stream inStream)
		{
			StreamReader streamReader = new StreamReader(inStream, Encoding.GetEncoding(1252));
			while (true)
			{
				string text = streamReader.ReadLine();
				if (text == null)
				{
					break;
				}
				if (text.Length > 0)
				{
					int length = text.Length;
					int i;
					for (i = 0; i < length && " \t\r\n\f".IndexOf(text[i]) != -1; i++)
					{
					}
					if (i != length)
					{
						char c = text[i];
						if (c != '#' && c != '!')
						{
							while (ContinueLine(text))
							{
								string text2 = streamReader.ReadLine();
								if (text2 == null)
								{
									text2 = "";
								}
								string str = text.Substring(0, length - 1);
								int j;
								for (j = 0; j < text2.Length && " \t\r\n\f".IndexOf(text2[j]) != -1; j++)
								{
								}
								text2 = text2.Substring(j, text2.Length - j);
								text = str + text2;
								length = text.Length;
							}
							int k;
							for (k = i; k < length; k++)
							{
								char c2 = text[k];
								if (c2 == '\\')
								{
									k++;
								}
								else if ("=: \t\r\n\f".IndexOf(c2) != -1)
								{
									break;
								}
							}
							int l;
							for (l = k; l < length && " \t\r\n\f".IndexOf(text[l]) != -1; l++)
							{
							}
							if (l < length && "=:".IndexOf(text[l]) != -1)
							{
								l++;
							}
							for (; l < length && " \t\r\n\f".IndexOf(text[l]) != -1; l++)
							{
							}
							string theString = text.Substring(i, k - i);
							string theString2 = (k < length) ? text.Substring(l, length - l) : "";
							theString = LoadConvert(theString);
							theString2 = LoadConvert(theString2);
							Add(theString, theString2);
						}
					}
				}
			}
		}

		/// <summary>
		/// Loads the convert.
		/// </summary>
		/// <param name="theString">The string.</param>
		/// <returns></returns>
		/// <remarks>
		/// Converts encoded \uxxxx to unicode chars
		/// and changes special saved chars to their original forms
		/// </remarks>
		private string LoadConvert(string theString)
		{
			int length = theString.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			int num = 0;
			while (num < length)
			{
				char c = theString[num++];
				if (c == '\\')
				{
					c = theString[num++];
					switch (c)
					{
					case 'u':
					{
						int num4 = 0;
						for (int i = 0; i < 4; i++)
						{
							c = theString[num++];
							switch (c)
							{
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								num4 = (num4 << 4) + c - 48;
								break;
							case 'a':
							case 'b':
							case 'c':
							case 'd':
							case 'e':
							case 'f':
								num4 = (num4 << 4) + 10 + c - 97;
								break;
							case 'A':
							case 'B':
							case 'C':
							case 'D':
							case 'E':
							case 'F':
								num4 = (num4 << 4) + 10 + c - 65;
								break;
							default:
								throw new ArgumentException("Malformed \\uxxxx encoding.");
							}
						}
						stringBuilder.Append((char)num4);
						break;
					}
					case 't':
						c = '\t';
						goto default;
					case 'r':
						c = '\r';
						goto default;
					case 'n':
						c = '\n';
						goto default;
					case 'f':
						c = '\f';
						goto default;
					default:
						stringBuilder.Append(c);
						break;
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Continues the line.
		/// </summary>
		/// <param name="line">The line.</param>
		/// <returns></returns>
		private bool ContinueLine(string line)
		{
			int num = 0;
			int num2 = line.Length - 1;
			while (num2 >= 0 && line[num2--] == '\\')
			{
				num++;
			}
			return num % 2 == 1;
		}
	}
}
