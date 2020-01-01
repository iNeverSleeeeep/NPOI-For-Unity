using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace NPOI.HSSF.Util
{
	/// Intends to provide support for the very evil index to triplet Issue and
	/// will likely replace the color constants interface for HSSF 2.0.
	/// This class Contains static inner class members for representing colors.
	/// Each color has an index (for the standard palette in Excel (tm) ),
	/// native (RGB) triplet and string triplet.  The string triplet Is as the
	/// color would be represented by Gnumeric.  Having (string) this here Is a bit of a
	/// collusion of function between HSSF and the HSSFSerializer but I think its
	/// a reasonable one in this case.
	///
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author  Brian Sanders (bsanders at risklabs dot com) - full default color palette
	public class HSSFColor : IColor
	{
		/// Class BLACK
		public class Black : HSSFColor
		{
			public const short Index = 8;

			public const string HexString = "0:0:0";

			public static readonly byte[] Triplet;

			public override short Indexed => 8;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:0:0";
			}

			static Black()
			{
				byte[] array = Triplet = new byte[3];
			}
		}

		/// Class BROWN
		public class Brown : HSSFColor
		{
			public const short Index = 60;

			public const string HexString = "9999:3333:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				153,
				51,
				0
			};

			public override short Indexed => 60;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "9999:3333:0";
			}
		}

		/// Class OLIVE_GREEN
		public class OliveGreen : HSSFColor
		{
			public const short Index = 59;

			public const string HexString = "3333:3333:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				51,
				51,
				0
			};

			public override short Indexed => 59;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "3333:3333:0";
			}
		}

		/// Class DARK_GREEN
		public class DarkGreen : HSSFColor
		{
			public const short Index = 58;

			public const string HexString = "0:3333:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				51,
				0
			};

			public override short Indexed => 58;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:3333:0";
			}
		}

		/// Class DARK_TEAL
		public class DarkTeal : HSSFColor
		{
			public const short Index = 56;

			public const string HexString = "0:3333:6666";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				51,
				102
			};

			public override short Indexed => 56;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:3333:6666";
			}
		}

		/// Class DARK_BLUE
		public class DarkBlue : HSSFColor
		{
			public const short Index = 18;

			public const short Index2 = 32;

			public const string HexString = "0:0:8080";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				0,
				128
			};

			public override short Indexed => 18;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:0:8080";
			}
		}

		/// Class INDIGO
		public class Indigo : HSSFColor
		{
			public const short Index = 62;

			public const string HexString = "3333:3333:9999";

			public static readonly byte[] Triplet = new byte[3]
			{
				51,
				51,
				153
			};

			public override short Indexed => 62;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "3333:3333:9999";
			}
		}

		/// Class GREY_80_PERCENT
		public class Grey80Percent : HSSFColor
		{
			public const short Index = 63;

			public const string HexString = "3333:3333:3333";

			public static readonly byte[] Triplet = new byte[3]
			{
				51,
				51,
				51
			};

			public override short Indexed => 63;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "3333:3333:3333";
			}
		}

		/// Class DARK_RED
		public class DarkRed : HSSFColor
		{
			public const short Index = 16;

			public const short Index2 = 37;

			public const string HexString = "8080:0:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				128,
				0,
				0
			};

			public override short Indexed => 16;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "8080:0:0";
			}
		}

		/// Class ORANGE
		public class Orange : HSSFColor
		{
			public const short Index = 53;

			public const string HexString = "FFFF:6666:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				102,
				0
			};

			public override short Indexed => 53;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:6666:0";
			}
		}

		/// Class DARK_YELLOW
		public class DarkYellow : HSSFColor
		{
			public const short Index = 19;

			public const string HexString = "8080:8080:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				128,
				128,
				0
			};

			public override short Indexed => 19;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "8080:8080:0";
			}
		}

		/// Class GREEN
		public class Green : HSSFColor
		{
			public const short Index = 17;

			public const string HexString = "0:8080:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				128,
				0
			};

			public override short Indexed => 17;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:8080:0";
			}
		}

		/// Class TEAL
		public class Teal : HSSFColor
		{
			public const short Index = 21;

			public const short Index2 = 38;

			public const string HexString = "0:8080:8080";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				128,
				128
			};

			public override short Indexed => 21;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:8080:8080";
			}
		}

		/// Class BLUE
		public class Blue : HSSFColor
		{
			public const short Index = 12;

			public const short Index2 = 39;

			public const string HexString = "0:0:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				0,
				byte.MaxValue
			};

			public override short Indexed => 12;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:0:FFFF";
			}
		}

		/// Class BLUE_GREY
		public class BlueGrey : HSSFColor
		{
			public const short Index = 54;

			public const string HexString = "6666:6666:9999";

			public static readonly byte[] Triplet = new byte[3]
			{
				102,
				102,
				153
			};

			public override short Indexed => 54;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "6666:6666:9999";
			}
		}

		/// Class GREY_50_PERCENT
		public class Grey50Percent : HSSFColor
		{
			public const short Index = 23;

			public const string HexString = "8080:8080:8080";

			public static readonly byte[] Triplet = new byte[3]
			{
				128,
				128,
				128
			};

			public override short Indexed => 23;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "8080:8080:8080";
			}
		}

		/// Class RED
		public class Red : HSSFColor
		{
			public const short Index = 10;

			public const string HexString = "FFFF:0:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				0,
				0
			};

			public override short Indexed => 10;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:0:0";
			}
		}

		/// Class LIGHT_ORANGE
		public class LightOrange : HSSFColor
		{
			public const short Index = 52;

			public const string HexString = "FFFF:9999:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				153,
				0
			};

			public override short Indexed => 52;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:9999:0";
			}
		}

		/// Class LIME
		public class Lime : HSSFColor
		{
			public const short Index = 50;

			public const string HexString = "9999:CCCC:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				153,
				204,
				0
			};

			public override short Indexed => 50;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "9999:CCCC:0";
			}
		}

		/// Class SEA_GREEN
		public class SeaGreen : HSSFColor
		{
			public const short Index = 57;

			public const string HexString = "3333:9999:6666";

			public static readonly byte[] Triplet = new byte[3]
			{
				51,
				153,
				102
			};

			public override short Indexed => 57;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "3333:9999:6666";
			}
		}

		/// Class AQUA
		public class Aqua : HSSFColor
		{
			public const short Index = 49;

			public const string HexString = "3333:CCCC:CCCC";

			public static readonly byte[] Triplet = new byte[3]
			{
				51,
				204,
				204
			};

			public override short Indexed => 49;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "3333:CCCC:CCCC";
			}
		}

		public class LightBlue : HSSFColor
		{
			public const short Index = 48;

			public const string HexString = "3333:6666:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				51,
				102,
				byte.MaxValue
			};

			public override short Indexed => 48;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "3333:6666:FFFF";
			}
		}

		public class Violet : HSSFColor
		{
			public const short Index = 20;

			public const short Index2 = 36;

			public const string HexString = "8080:0:8080";

			public static readonly byte[] Triplet = new byte[3]
			{
				128,
				0,
				128
			};

			public override short Indexed => 20;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "8080:0:8080";
			}
		}

		/// Class GREY_40_PERCENT
		public class Grey40Percent : HSSFColor
		{
			public const short Index = 55;

			public const string HexString = "9696:9696:9696";

			public static readonly byte[] Triplet = new byte[3]
			{
				150,
				150,
				150
			};

			public override short Indexed => 55;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "9696:9696:9696";
			}
		}

		public class Pink : HSSFColor
		{
			public const short Index = 14;

			public const short Index2 = 33;

			public const string HexString = "FFFF:0:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				0,
				byte.MaxValue
			};

			public override short Indexed => 14;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:0:FFFF";
			}
		}

		public class Gold : HSSFColor
		{
			public const short Index = 51;

			public const string HexString = "FFFF:CCCC:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				204,
				0
			};

			public override short Indexed => 51;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:CCCC:0";
			}
		}

		public class Yellow : HSSFColor
		{
			public const short Index = 13;

			public const short Index2 = 34;

			public const string HexString = "FFFF:FFFF:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				byte.MaxValue,
				0
			};

			public override short Indexed => 13;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:FFFF:0";
			}
		}

		public class BrightGreen : HSSFColor
		{
			public const short Index = 11;

			public const short Index2 = 35;

			public const string HexString = "0:FFFF:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				byte.MaxValue,
				0
			};

			public override short Indexed => 11;

			public override string GetHexString()
			{
				return "0:FFFF:0";
			}

			public override byte[] GetTriplet()
			{
				return Triplet;
			}
		}

		/// Class TURQUOISE
		public class Turquoise : HSSFColor
		{
			public const short Index = 15;

			public const short Index2 = 35;

			public const string HexString = "0:FFFF:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				byte.MaxValue,
				byte.MaxValue
			};

			public override short Indexed => 15;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:FFFF:FFFF";
			}
		}

		/// Class SKY_BLUE
		public class SkyBlue : HSSFColor
		{
			public const short Index = 40;

			public const string HexString = "0:CCCC:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				204,
				byte.MaxValue
			};

			public override short Indexed => 40;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:CCCC:FFFF";
			}
		}

		/// Class PLUM
		public class Plum : HSSFColor
		{
			public const short Index = 61;

			public const short Index2 = 25;

			public const string HexString = "9999:3333:6666";

			public static readonly byte[] Triplet = new byte[3]
			{
				153,
				51,
				102
			};

			public override short Indexed => 61;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "9999:3333:6666";
			}
		}

		/// Class GREY_25_PERCENT
		public class Grey25Percent : HSSFColor
		{
			public const short Index = 22;

			public const string HexString = "C0C0:C0C0:C0C0";

			public static readonly byte[] Triplet = new byte[3]
			{
				192,
				192,
				192
			};

			public override short Indexed => 22;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "C0C0:C0C0:C0C0";
			}
		}

		/// Class ROSE
		public class Rose : HSSFColor
		{
			public const short Index = 45;

			public const string HexString = "FFFF:9999:CCCC";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				153,
				204
			};

			public override short Indexed => 45;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:9999:CCCC";
			}
		}

		/// Class TAN
		public class Tan : HSSFColor
		{
			public const short Index = 47;

			public const string HexString = "FFFF:CCCC:9999";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				204,
				153
			};

			public override short Indexed => 47;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:CCCC:9999";
			}
		}

		/// Class LIGHT_YELLOW
		public class LightYellow : HSSFColor
		{
			public const short Index = 43;

			public const string HexString = "FFFF:FFFF:9999";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				byte.MaxValue,
				153
			};

			public override short Indexed => 43;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:FFFF:9999";
			}
		}

		/// Class LIGHT_GREEN
		public class LightGreen : HSSFColor
		{
			public const short Index = 42;

			public const string HexString = "CCCC:FFFF:CCCC";

			public static readonly byte[] Triplet = new byte[3]
			{
				204,
				byte.MaxValue,
				204
			};

			public override short Indexed => 42;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "CCCC:FFFF:CCCC";
			}
		}

		/// Class LIGHT_TURQUOISE
		public class LightTurquoise : HSSFColor
		{
			public const short Index = 41;

			public const short Index2 = 27;

			public const string HexString = "CCCC:FFFF:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				204,
				byte.MaxValue,
				byte.MaxValue
			};

			public override short Indexed => 41;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "CCCC:FFFF:FFFF";
			}
		}

		/// Class PALE_BLUE
		public class PaleBlue : HSSFColor
		{
			public const short Index = 44;

			public const string HexString = "9999:CCCC:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				153,
				204,
				byte.MaxValue
			};

			public override short Indexed => 44;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "9999:CCCC:FFFF";
			}
		}

		/// Class LAVENDER
		public class Lavender : HSSFColor
		{
			public const short Index = 46;

			public const string HexString = "CCCC:9999:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				204,
				153,
				byte.MaxValue
			};

			public override short Indexed => 46;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "CCCC:9999:FFFF";
			}
		}

		/// Class WHITE
		public class White : HSSFColor
		{
			public const short Index = 9;

			public const string HexString = "FFFF:FFFF:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue
			};

			public override short Indexed => 9;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:FFFF:FFFF";
			}
		}

		/// Class CORNFLOWER_BLUE
		public class CornflowerBlue : HSSFColor
		{
			public const short Index = 24;

			public const string HexString = "9999:9999:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				153,
				153,
				byte.MaxValue
			};

			public override short Indexed => 24;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "9999:9999:FFFF";
			}
		}

		/// Class LEMON_CHIFFON
		public class LemonChiffon : HSSFColor
		{
			public const short Index = 26;

			public const string HexString = "FFFF:FFFF:CCCC";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				byte.MaxValue,
				204
			};

			public override short Indexed => 26;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:FFFF:CCCC";
			}
		}

		/// Class MAROON
		public class Maroon : HSSFColor
		{
			public const short Index = 25;

			public const string HexString = "8000:0:0";

			public static readonly byte[] Triplet = new byte[3]
			{
				127,
				0,
				0
			};

			public override short Indexed => 25;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "8000:0:0";
			}
		}

		/// Class ORCHID
		public class Orchid : HSSFColor
		{
			public const short Index = 28;

			public const string HexString = "6666:0:6666";

			public static readonly byte[] Triplet = new byte[3]
			{
				102,
				0,
				102
			};

			public override short Indexed => 28;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "6666:0:6666";
			}
		}

		/// Class CORAL
		public class Coral : HSSFColor
		{
			public const short Index = 29;

			public const string HexString = "FFFF:8080:8080";

			public static readonly byte[] Triplet = new byte[3]
			{
				byte.MaxValue,
				128,
				128
			};

			public override short Indexed => 29;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "FFFF:8080:8080";
			}
		}

		/// Class ROYAL_BLUE
		public class RoyalBlue : HSSFColor
		{
			public const short Index = 30;

			public const string HexString = "0:6666:CCCC";

			public static readonly byte[] Triplet = new byte[3]
			{
				0,
				102,
				204
			};

			public override short Indexed => 30;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "0:6666:CCCC";
			}
		}

		/// Class LIGHT_CORNFLOWER_BLUE
		public class LightCornflowerBlue : HSSFColor
		{
			public const short Index = 31;

			public const string HexString = "CCCC:CCCC:FFFF";

			public static readonly byte[] Triplet = new byte[3]
			{
				204,
				204,
				byte.MaxValue
			};

			public override short Indexed => 31;

			public override byte[] GetTriplet()
			{
				return Triplet;
			}

			public override string GetHexString()
			{
				return "CCCC:CCCC:FFFF";
			}
		}

		/// Special Default/Normal/Automatic color.
		/// <i>Note:</i> This class Is NOT in the default HashTables returned by HSSFColor.
		/// The index Is a special case which Is interpreted in the various SetXXXColor calls.
		///
		/// @author Jason
		public class Automatic : HSSFColor
		{
			public const short Index = 64;

			private static HSSFColor instance = new Automatic();

			public override byte[] GetTriplet()
			{
				return Black.Triplet;
			}

			public override string GetHexString()
			{
				return "0:0:0";
			}

			public static HSSFColor GetInstance()
			{
				return instance;
			}
		}

		public const short COLOR_NORMAL = short.MaxValue;

		private static Hashtable indexHash;

		/// @return index to the standard palette
		public virtual short Indexed => 8;

		public byte[] RGB => GetTriplet();

		/// this function returns all colors in a hastable.  Its not implemented as a
		/// static member/staticly initialized because that would be dirty in a
		/// server environment as it Is intended.  This means you'll eat the time
		/// it takes to Create it once per request but you will not hold onto it
		/// if you have none of those requests.
		///
		/// @return a hashtable containing all colors keyed by <c>int</c> excel-style palette indexes
		public static Hashtable GetIndexHash()
		{
			if (indexHash == null)
			{
				indexHash = CreateColorsByIndexMap();
			}
			return indexHash;
		}

		/// This function returns all the Colours, stored in a Hashtable that
		///  can be edited. No caching is performed. If you don't need to edit
		///  the table, then call {@link #getIndexHash()} which returns a
		///  statically cached imuatable map of colours.
		public static Hashtable GetMutableIndexHash()
		{
			return CreateColorsByIndexMap();
		}

		private static Hashtable CreateColorsByIndexMap()
		{
			HSSFColor[] allColors = GetAllColors();
			Hashtable hashtable = new Hashtable(allColors.Length * 3 / 2);
			foreach (HSSFColor hSSFColor in allColors)
			{
				int indexed = hSSFColor.Indexed;
				if (hashtable.ContainsKey(indexed))
				{
					HSSFColor hSSFColor2 = (HSSFColor)hashtable[indexed];
					throw new InvalidDataException("Dup color index (" + indexed + ") for colors (" + hSSFColor2.GetType().Name + "),(" + hSSFColor.GetType().Name + ")");
				}
				hashtable[indexed] = hSSFColor;
			}
			foreach (HSSFColor hSSFColor3 in allColors)
			{
				int index = GetIndex2(hSSFColor3);
				if (index != -1)
				{
					hashtable[index] = hSSFColor3;
				}
			}
			return hashtable;
		}

		private static int GetIndex2(HSSFColor color)
		{
			FieldInfo field = color.GetType().GetField("Index2", BindingFlags.Static | BindingFlags.Public);
			if (field == null)
			{
				return -1;
			}
			short value = (short)field.GetValue(color);
			return Convert.ToInt32(value);
		}

		internal static HSSFColor[] GetAllColors()
		{
			return new HSSFColor[47]
			{
				new Black(),
				new Brown(),
				new OliveGreen(),
				new DarkGreen(),
				new DarkTeal(),
				new DarkBlue(),
				new Indigo(),
				new Grey80Percent(),
				new Orange(),
				new DarkYellow(),
				new Green(),
				new Teal(),
				new Blue(),
				new BlueGrey(),
				new Grey50Percent(),
				new Red(),
				new LightOrange(),
				new Lime(),
				new SeaGreen(),
				new Aqua(),
				new LightBlue(),
				new Violet(),
				new Grey40Percent(),
				new Pink(),
				new Gold(),
				new Yellow(),
				new BrightGreen(),
				new Turquoise(),
				new DarkRed(),
				new SkyBlue(),
				new Plum(),
				new Grey25Percent(),
				new Rose(),
				new LightYellow(),
				new LightGreen(),
				new LightTurquoise(),
				new PaleBlue(),
				new Lavender(),
				new White(),
				new CornflowerBlue(),
				new LemonChiffon(),
				new Maroon(),
				new Orchid(),
				new Coral(),
				new RoyalBlue(),
				new LightCornflowerBlue(),
				new Tan()
			};
		}

		/// <summary>
		/// this function returns all colors in a hastable.  Its not implemented as a
		/// static member/staticly initialized because that would be dirty in a
		/// server environment as it Is intended.  This means you'll eat the time
		/// it takes to Create it once per request but you will not hold onto it
		/// if you have none of those requests.
		/// </summary>
		/// <returns>a hashtable containing all colors keyed by String gnumeric-like triplets</returns>
		public static Hashtable GetTripletHash()
		{
			return CreateColorsByHexStringMap();
		}

		private static Hashtable CreateColorsByHexStringMap()
		{
			HSSFColor[] allColors = GetAllColors();
			Hashtable hashtable = new Hashtable(allColors.Length * 3 / 2);
			foreach (HSSFColor hSSFColor in allColors)
			{
				string hexString = hSSFColor.GetHexString();
				if (hashtable.ContainsKey(hexString))
				{
					throw new InvalidDataException("Dup color hexString (" + hexString + ") for color (" + hSSFColor.GetType().Name + ")");
				}
				hashtable[hexString] = hSSFColor;
			}
			return hashtable;
		}

		/// @return  triplet representation like that in Excel
		[Obsolete]
		public virtual byte[] GetTriplet()
		{
			return Black.Triplet;
		}

		/// @return a hex string exactly like a gnumeric triplet
		public virtual string GetHexString()
		{
			return "0:0:0";
		}
	}
}
