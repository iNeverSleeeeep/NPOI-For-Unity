using NPOI.HSSF.Util;
using System.Collections.Generic;
using System.Text;

namespace NPOI.SS.UserModel
{
	/// A deprecated indexing scheme for colours that is still required for some records, and for backwards
	///  compatibility with OLE2 formats.
	///
	/// <p>
	/// Each element corresponds to a color index (zero-based). When using the default indexed color palette,
	/// the values are not written out, but instead are implied. When the color palette has been modified from default,
	/// then the entire color palette is used.
	/// </p>
	///
	/// @author Yegor Kozlov
	public class IndexedColors
	{
		public static readonly IndexedColors Black;

		public static readonly IndexedColors White;

		public static readonly IndexedColors Red;

		public static readonly IndexedColors BrightGreen;

		public static readonly IndexedColors Blue;

		public static readonly IndexedColors Yellow;

		public static readonly IndexedColors Pink;

		public static readonly IndexedColors Turquoise;

		public static readonly IndexedColors DarkRed;

		public static readonly IndexedColors Green;

		public static readonly IndexedColors DarkBlue;

		public static readonly IndexedColors DarkYellow;

		public static readonly IndexedColors Violet;

		public static readonly IndexedColors Teal;

		public static readonly IndexedColors Grey25Percent;

		public static readonly IndexedColors Grey50Percent;

		public static readonly IndexedColors CornflowerBlue;

		public static readonly IndexedColors Maroon;

		public static readonly IndexedColors LemonChiffon;

		public static readonly IndexedColors Orchid;

		public static readonly IndexedColors Coral;

		public static readonly IndexedColors RoyalBlue;

		public static readonly IndexedColors LightCornflowerBlue;

		public static readonly IndexedColors SkyBlue;

		public static readonly IndexedColors LightTurquoise;

		public static readonly IndexedColors LightGreen;

		public static readonly IndexedColors LightYellow;

		public static readonly IndexedColors PaleBlue;

		public static readonly IndexedColors Rose;

		public static readonly IndexedColors Lavender;

		public static readonly IndexedColors Tan;

		public static readonly IndexedColors LightBlue;

		public static readonly IndexedColors Aqua;

		public static readonly IndexedColors Lime;

		public static readonly IndexedColors Gold;

		public static readonly IndexedColors LightOrange;

		public static readonly IndexedColors Orange;

		public static readonly IndexedColors BlueGrey;

		public static readonly IndexedColors Grey40Percent;

		public static readonly IndexedColors DarkTeal;

		public static readonly IndexedColors SeaGreen;

		public static readonly IndexedColors DarkGreen;

		public static readonly IndexedColors OliveGreen;

		public static readonly IndexedColors Brown;

		public static readonly IndexedColors Plum;

		public static readonly IndexedColors Indigo;

		public static readonly IndexedColors Grey80Percent;

		public static readonly IndexedColors Automatic;

		private int index;

		private HSSFColor hssfColor;

		private static Dictionary<string, IndexedColors> mappingName;

		private static Dictionary<int, IndexedColors> mappingIndex;

		public byte[] RGB
		{
			get
			{
				return hssfColor.RGB;
			}
		}

		public string HexString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(7);
				stringBuilder.Append('#');
				byte[] rGB = hssfColor.RGB;
				byte[] array = rGB;
				for (int i = 0; i < array.Length; i++)
				{
					byte b = array[i];
					stringBuilder.Append(b.ToString("x2"));
				}
				return stringBuilder.ToString();
			}
		}

		/// Returns index of this color
		///
		/// @return index of this color
		public short Index
		{
			get
			{
				return (short)index;
			}
		}

		private IndexedColors(int idx, HSSFColor color)
		{
			index = idx;
			hssfColor = color;
		}

		static IndexedColors()
		{
			mappingName = null;
			mappingIndex = null;
			Black = new IndexedColors(8, new HSSFColor.Black());
			White = new IndexedColors(9, new HSSFColor.White());
			Red = new IndexedColors(10, new HSSFColor.Red());
			BrightGreen = new IndexedColors(11, new HSSFColor.BrightGreen());
			Blue = new IndexedColors(12, new HSSFColor.Blue());
			Yellow = new IndexedColors(13, new HSSFColor.Yellow());
			Pink = new IndexedColors(14, new HSSFColor.Pink());
			Turquoise = new IndexedColors(15, new HSSFColor.Turquoise());
			DarkRed = new IndexedColors(16, new HSSFColor.DarkRed());
			Green = new IndexedColors(17, new HSSFColor.Green());
			DarkBlue = new IndexedColors(18, new HSSFColor.DarkBlue());
			DarkYellow = new IndexedColors(19, new HSSFColor.DarkYellow());
			Violet = new IndexedColors(20, new HSSFColor.Violet());
			Teal = new IndexedColors(21, new HSSFColor.Teal());
			Grey25Percent = new IndexedColors(22, new HSSFColor.Grey25Percent());
			Grey50Percent = new IndexedColors(23, new HSSFColor.Grey50Percent());
			CornflowerBlue = new IndexedColors(24, new HSSFColor.CornflowerBlue());
			Maroon = new IndexedColors(25, new HSSFColor.Maroon());
			LemonChiffon = new IndexedColors(26, new HSSFColor.LemonChiffon());
			Orchid = new IndexedColors(28, new HSSFColor.Orchid());
			Coral = new IndexedColors(29, new HSSFColor.Coral());
			RoyalBlue = new IndexedColors(30, new HSSFColor.RoyalBlue());
			LightCornflowerBlue = new IndexedColors(31, new HSSFColor.LightCornflowerBlue());
			SkyBlue = new IndexedColors(40, new HSSFColor.SkyBlue());
			LightTurquoise = new IndexedColors(41, new HSSFColor.LightTurquoise());
			LightGreen = new IndexedColors(42, new HSSFColor.LightGreen());
			LightYellow = new IndexedColors(43, new HSSFColor.LightYellow());
			PaleBlue = new IndexedColors(44, new HSSFColor.PaleBlue());
			Rose = new IndexedColors(45, new HSSFColor.Rose());
			Lavender = new IndexedColors(46, new HSSFColor.Lavender());
			Tan = new IndexedColors(47, new HSSFColor.Tan());
			LightBlue = new IndexedColors(48, new HSSFColor.LightBlue());
			Aqua = new IndexedColors(49, new HSSFColor.Aqua());
			Lime = new IndexedColors(50, new HSSFColor.Lime());
			Gold = new IndexedColors(51, new HSSFColor.Gold());
			LightOrange = new IndexedColors(52, new HSSFColor.LightOrange());
			Orange = new IndexedColors(53, new HSSFColor.Orange());
			BlueGrey = new IndexedColors(54, new HSSFColor.BlueGrey());
			Grey40Percent = new IndexedColors(55, new HSSFColor.Grey40Percent());
			DarkTeal = new IndexedColors(56, new HSSFColor.DarkTeal());
			SeaGreen = new IndexedColors(57, new HSSFColor.SeaGreen());
			DarkGreen = new IndexedColors(58, new HSSFColor.DarkGreen());
			OliveGreen = new IndexedColors(59, new HSSFColor.OliveGreen());
			Brown = new IndexedColors(60, new HSSFColor.Brown());
			Plum = new IndexedColors(61, new HSSFColor.Plum());
			Indigo = new IndexedColors(62, new HSSFColor.Indigo());
			Grey80Percent = new IndexedColors(63, new HSSFColor.Grey80Percent());
			Automatic = new IndexedColors(64, new HSSFColor.Automatic());
			mappingName = new Dictionary<string, IndexedColors>();
			mappingName.Add("black", Black);
			mappingName.Add("white", White);
			mappingName.Add("red", Red);
			mappingName.Add("brightgreen", BrightGreen);
			mappingName.Add("blue", Blue);
			mappingName.Add("yellow", Yellow);
			mappingName.Add("pink", Pink);
			mappingName.Add("turquoise", Turquoise);
			mappingName.Add("darkred", DarkRed);
			mappingName.Add("green", Green);
			mappingName.Add("darkblue", DarkBlue);
			mappingName.Add("darkyellow", DarkYellow);
			mappingName.Add("violet", Violet);
			mappingName.Add("teal", Teal);
			mappingName.Add("grey25percent", Grey25Percent);
			mappingName.Add("grey50percent", Grey50Percent);
			mappingName.Add("cornflowerblue", CornflowerBlue);
			mappingName.Add("maroon", Maroon);
			mappingName.Add("lemonchilffon", LemonChiffon);
			mappingName.Add("orchid", Orchid);
			mappingName.Add("coral", Coral);
			mappingName.Add("royalblue", RoyalBlue);
			mappingName.Add("lightcornflowerblue", LightCornflowerBlue);
			mappingName.Add("skyblue", SkyBlue);
			mappingName.Add("lightturquoise", LightTurquoise);
			mappingName.Add("lightgreen", LightGreen);
			mappingName.Add("lightyellow", LightYellow);
			mappingName.Add("paleblue", PaleBlue);
			mappingName.Add("rose", Rose);
			mappingName.Add("lavender", Lavender);
			mappingName.Add("tan", Tan);
			mappingName.Add("lightblue", LightBlue);
			mappingName.Add("aqua", Aqua);
			mappingName.Add("lime", Lime);
			mappingName.Add("gold", Gold);
			mappingName.Add("lightorange", LightOrange);
			mappingName.Add("orange", Orange);
			mappingName.Add("bluegrey", BlueGrey);
			mappingName.Add("grey40percent", Grey40Percent);
			mappingName.Add("darkteal", DarkTeal);
			mappingName.Add("seagreen", SeaGreen);
			mappingName.Add("darkgreen", DarkGreen);
			mappingName.Add("olivergreen", OliveGreen);
			mappingName.Add("brown", Brown);
			mappingName.Add("plum", Plum);
			mappingName.Add("indigo", Indigo);
			mappingName.Add("grey80percent", Grey80Percent);
			mappingName.Add("automatic", Automatic);
			mappingIndex = new Dictionary<int, IndexedColors>();
			mappingIndex.Add(8, Black);
			mappingIndex.Add(9, White);
			mappingIndex.Add(10, Red);
			mappingIndex.Add(11, BrightGreen);
			mappingIndex.Add(12, Blue);
			mappingIndex.Add(13, Yellow);
			mappingIndex.Add(14, Pink);
			mappingIndex.Add(15, Turquoise);
			mappingIndex.Add(16, DarkRed);
			mappingIndex.Add(17, Green);
			mappingIndex.Add(18, DarkBlue);
			mappingIndex.Add(19, DarkYellow);
			mappingIndex.Add(20, Violet);
			mappingIndex.Add(21, Teal);
			mappingIndex.Add(22, Grey25Percent);
			mappingIndex.Add(23, Grey50Percent);
			mappingIndex.Add(24, CornflowerBlue);
			mappingIndex.Add(25, Maroon);
			mappingIndex.Add(26, LemonChiffon);
			mappingIndex.Add(28, Orchid);
			mappingIndex.Add(29, Coral);
			mappingIndex.Add(30, RoyalBlue);
			mappingIndex.Add(31, LightCornflowerBlue);
			mappingIndex.Add(40, SkyBlue);
			mappingIndex.Add(41, LightTurquoise);
			mappingIndex.Add(42, LightGreen);
			mappingIndex.Add(43, LightYellow);
			mappingIndex.Add(44, PaleBlue);
			mappingIndex.Add(45, Rose);
			mappingIndex.Add(46, Lavender);
			mappingIndex.Add(47, Tan);
			mappingIndex.Add(48, LightBlue);
			mappingIndex.Add(49, Aqua);
			mappingIndex.Add(50, Lime);
			mappingIndex.Add(51, Gold);
			mappingIndex.Add(52, LightOrange);
			mappingIndex.Add(53, Orange);
			mappingIndex.Add(54, BlueGrey);
			mappingIndex.Add(55, Grey40Percent);
			mappingIndex.Add(56, DarkTeal);
			mappingIndex.Add(57, SeaGreen);
			mappingIndex.Add(58, DarkGreen);
			mappingIndex.Add(59, OliveGreen);
			mappingIndex.Add(60, Brown);
			mappingIndex.Add(61, Plum);
			mappingIndex.Add(62, Indigo);
			mappingIndex.Add(63, Grey80Percent);
			mappingIndex.Add(64, Automatic);
		}

		public static IndexedColors ValueOf(string colorName)
		{
			if (mappingName.ContainsKey(colorName.ToLower()))
			{
				return mappingName[colorName.ToLower()];
			}
			return null;
		}

		public static IndexedColors ValueOf(int index)
		{
			if (mappingIndex.ContainsKey(index))
			{
				return mappingIndex[index];
			}
			return null;
		}
	}
}
