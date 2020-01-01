using NPOI.POIFS.FileSystem;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace NPOI.POIFS.Dev
{
	/// <summary>
	/// This class contains methods used to inspect POIFSViewable objects
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class POIFSViewEngine
	{
		/// <summary>
		/// Inspect an object that may be viewable, and drill down if told to
		/// </summary>
		/// <param name="viewable">the object to be viewed</param>
		/// <param name="drilldown">if <c>true</c> and the object implements POIFSViewable, inspect the objects' contents</param>
		/// <param name="indentLevel">how far in to indent each string</param>
		/// <param name="indentString">string to use for indenting</param>
		/// <returns>a List of Strings holding the content</returns>
		public static IList InspectViewable(object viewable, bool drilldown, int indentLevel, string indentString)
		{
			IList list = new ArrayList();
			if (viewable is DictionaryEntry)
			{
				ProcessViewable(((DictionaryEntry)viewable).Value, drilldown, indentLevel, indentString, list);
			}
			else if (viewable is POIFSViewable)
			{
				ProcessViewable(viewable, drilldown, indentLevel, indentString, list);
			}
			else
			{
				list.Add(Indent(indentLevel, indentString, viewable.ToString()));
			}
			return list;
		}

		internal static void ProcessViewable(object viewable, bool drilldown, int indentLevel, string indentString, IList objects)
		{
			POIFSViewable pOIFSViewable = (POIFSViewable)viewable;
			objects.Add(Indent(indentLevel, indentString, pOIFSViewable.ShortDescription));
			if (drilldown)
			{
				if (pOIFSViewable is POIFSDocument)
				{
					((ArrayList)objects).AddRange(InspectViewable("POIFSDocument content is too long so ignored", drilldown, indentLevel + 1, indentString));
				}
				else if (pOIFSViewable.PreferArray)
				{
					Array viewableArray = pOIFSViewable.ViewableArray;
					for (int i = 0; i < viewableArray.Length; i++)
					{
						((ArrayList)objects).AddRange(InspectViewable(viewableArray.GetValue(i), drilldown, indentLevel + 1, indentString));
					}
				}
				else
				{
					IEnumerator viewableIterator = pOIFSViewable.ViewableIterator;
					while (viewableIterator.MoveNext())
					{
						((ArrayList)objects).AddRange(InspectViewable(viewableIterator.Current, drilldown, indentLevel + 1, indentString));
					}
				}
			}
		}

		/// <summary>
		/// Indents the specified indent level.
		/// </summary>
		/// <param name="indentLevel">how far in to indent each string</param>
		/// <param name="indentString">string to use for indenting</param>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		private static string Indent(int indentLevel, string indentString, string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < indentLevel; i++)
			{
				stringBuilder2.Append(indentString);
			}
			using (StringReader stringReader = new StringReader(data))
			{
				for (string text = stringReader.ReadLine(); text != null; text = stringReader.ReadLine())
				{
					stringBuilder.Append(stringBuilder2).Append(text).Append(Environment.NewLine);
				}
				return stringBuilder.ToString();
			}
		}
	}
}
