using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace NPOI.SS.Formula.Function
{
	/// Converts the text meta-data file into a <c>FunctionMetadataRegistry</c>
	///
	/// @author Josh Micich
	internal class FunctionMetadataReader
	{
		private const string METADATA_FILE_NAME = "functionMetadata.txt";

		/// plain ASCII text metadata file uses three dots for ellipsis 
		private const string ELLIPSIS = "...";

		private const string TAB_DELIM_PATTERN = "\\t";

		private const string SPACE_DELIM_PATTERN = "\\s";

		private static readonly byte[] EMPTY_BYTE_ARRAY = new byte[0];

		private static readonly string[] DIGIT_ENDING_FUNCTION_NAMES = new string[6]
		{
			"LOG10",
			"ATAN2",
			"DAYS360",
			"SUMXMY2",
			"SUMX2MY2",
			"SUMX2PY2"
		};

		private static List<string> DIGIT_ENDING_FUNCTION_NAMES_Set = new List<string>(DIGIT_ENDING_FUNCTION_NAMES);

		public static FunctionMetadataRegistry CreateRegistry()
		{
			using (StreamReader streamReader = new StreamReader(typeof(FunctionMetadataReader).Assembly.GetManifestResourceStream("functionMetadata.txt")))
			{
				FunctionDataBuilder functionDataBuilder = new FunctionDataBuilder(400);
				try
				{
					while (true)
					{
						string text = streamReader.ReadLine();
						if (text == null)
						{
							break;
						}
						if (text.Length >= 1 && text[0] != '#')
						{
							string text2 = text.Trim();
							if (text2.Length >= 1)
							{
								ProcessLine(functionDataBuilder, text);
							}
						}
					}
				}
				catch (IOException)
				{
					throw;
				}
				return functionDataBuilder.Build();
			}
		}

		private static void ProcessLine(FunctionDataBuilder fdb, string line)
		{
			Regex regex = new Regex("\\t");
			string[] array = regex.Split(line);
			if (array.Length != 8)
			{
				throw new Exception("Bad line format '" + line + "' - expected 8 data fields");
			}
			int functionIndex = ParseInt(array[0]);
			string functionName = array[1];
			int minParams = ParseInt(array[2]);
			int maxParams = ParseInt(array[3]);
			byte returnClassCode = ParseReturnTypeCode(array[4]);
			byte[] parameterClassCodes = ParseOperandTypeCodes(array[5]);
			bool hasFootnote = array[7].Length > 0;
			ValidateFunctionName(functionName);
			fdb.Add(functionIndex, functionName, minParams, maxParams, returnClassCode, parameterClassCodes, hasFootnote);
		}

		private static byte ParseReturnTypeCode(string code)
		{
			if (code.Length == 0)
			{
				return 0;
			}
			return ParseOperandTypeCode(code);
		}

		private static byte[] ParseOperandTypeCodes(string codes)
		{
			if (codes.Length < 1)
			{
				return EMPTY_BYTE_ARRAY;
			}
			if (IsDash(codes))
			{
				return EMPTY_BYTE_ARRAY;
			}
			Regex regex = new Regex("\\s");
			string[] array = regex.Split(codes);
			int num = array.Length;
			if ("...".Equals(array[num - 1]))
			{
				num--;
			}
			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = ParseOperandTypeCode(array[i]);
			}
			return array2;
		}

		private static bool IsDash(string codes)
		{
			if (codes.Length == 1)
			{
				char c = codes[0];
				if (c == '-')
				{
					return true;
				}
			}
			return false;
		}

		private static byte ParseOperandTypeCode(string code)
		{
			if (code.Length == 1)
			{
				switch (code[0])
				{
				case 'V':
					return 32;
				case 'R':
					return 0;
				case 'A':
					return 64;
				default:
					throw new ArgumentException("Unexpected operand type code '" + code + "' (" + (int)code[0] + ")");
				}
			}
			throw new Exception("Bad operand type code format '" + code + "' expected single char");
		}

		/// Makes sure that footnote digits from the original OOO document have not been accidentally 
		/// left behind
		private static void ValidateFunctionName(string functionName)
		{
			int length = functionName.Length;
			int num = length - 1;
			if (char.IsDigit(functionName[num]))
			{
				while (num >= 0 && char.IsDigit(functionName[num]))
				{
					num--;
				}
				if (!DIGIT_ENDING_FUNCTION_NAMES_Set.Contains(functionName))
				{
					throw new Exception("Invalid function name '" + functionName + "' (is footnote number incorrectly Appended)");
				}
			}
		}

		private static int ParseInt(string valStr)
		{
			return int.Parse(valStr, CultureInfo.InvariantCulture);
		}
	}
}
