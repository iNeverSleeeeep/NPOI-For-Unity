using NPOI.Util.Collections;
using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace NPOI.HSSF.UserModel
{
	/// Allows the user to lookup the font metrics for a particular font without
	/// actually having the font on the system.  The font details are Loaded
	/// as a resource from the POI jar file (or classpath) and should be contained
	/// in path "/font_metrics.properties".  The font widths are for a 10 point
	/// version of the font.  Use a multiplier for other sizes.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	internal class StaticFontMetrics
	{
		private const string FONT_METRICS_PROPERTIES_FILE_NAME = "font_metrics.properties";

		/// The font metrics property file we're using 
		private static Properties fontMetricsProps;

		/// Our cache of font details we've alReady looked up 
		private static Hashtable fontDetailsMap = new Hashtable();

		/// Retrieves the fake font details for a given font.
		/// @param font  the font to lookup.
		/// @return  the fake font.
		public static FontDetails GetFontDetails(Font font)
		{
			if (fontMetricsProps == null)
			{
				Stream stream = null;
				try
				{
					fontMetricsProps = new Properties();
					string text = null;
					try
					{
						text = ConfigurationManager.AppSettings["font.metrics.filename"];
					}
					catch (Exception)
					{
					}
					if (text != null)
					{
						if (!File.Exists(text))
						{
							throw new FileNotFoundException("font_metrics.properties not found at path " + Path.GetFullPath(text));
						}
						stream = typeof(StaticFontMetrics).Assembly.GetManifestResourceStream("font_metrics.properties");
					}
					else
					{
						stream = typeof(StaticFontMetrics).Assembly.GetManifestResourceStream("font_metrics.properties");
						if (stream == null)
						{
							throw new FileNotFoundException("font_metrics.properties not found in classpath");
						}
					}
					fontMetricsProps.Load(stream);
				}
				catch (IOException ex2)
				{
					throw new Exception("Could not Load font metrics: " + ex2.Message);
				}
				finally
				{
					if (stream != null)
					{
						try
						{
							stream.Close();
						}
						catch (IOException)
						{
						}
					}
				}
			}
			string text2 = font.FontFamily.Name;
			string text3 = "";
			if (font.Bold)
			{
				text3 += "bold";
			}
			if (font.Italic)
			{
				text3 += "italic";
			}
			if (fontMetricsProps[FontDetails.BuildFontHeightProperty(text2)] == null && fontMetricsProps[FontDetails.BuildFontHeightProperty(text2 + "." + text3)] != null)
			{
				text2 = text2 + "." + text3;
			}
			if (fontDetailsMap[text2] == null)
			{
				FontDetails fontDetails = FontDetails.Create(text2, fontMetricsProps);
				fontDetailsMap[text2] = fontDetails;
				return fontDetails;
			}
			return (FontDetails)fontDetailsMap[text2];
		}
	}
}
