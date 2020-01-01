using NPOI.HPSF.Wellknown;
using NPOI.POIFS.FileSystem;
using NPOI.Util;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace NPOI.HPSF.Extractor
{
	/// <summary>
	/// Extracts all of the HPSF properties, both
	/// build in and custom, returning them in
	/// textual form.
	/// </summary>
	public class HPSFPropertiesExtractor : POITextExtractor
	{
		/// <summary>
		/// So we can get at the properties of any
		/// random OLE2 document.
		/// </summary>
		private class PropertiesOnlyDocument : POIDocument
		{
			public PropertiesOnlyDocument(NPOIFSFileSystem fs)
				: base(fs.Root)
			{
			}

			public PropertiesOnlyDocument(POIFSFileSystem fs)
				: base(fs)
			{
			}

			public override void Write(Stream out1)
			{
				throw new InvalidOperationException("Unable to write, only for properties!");
			}
		}

		/// <summary>
		/// Gets the document summary information text.
		/// </summary>
		/// <value>The document summary information text.</value>
		public string DocumentSummaryInformationText
		{
			get
			{
				DocumentSummaryInformation documentSummaryInformation = document.DocumentSummaryInformation;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(GetPropertiesText(documentSummaryInformation));
				CustomProperties customProperties = documentSummaryInformation?.CustomProperties;
				if (customProperties != null)
				{
					IEnumerator enumerator = customProperties.NameSet().GetEnumerator();
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current.ToString();
						string propertyValueText = GetPropertyValueText(customProperties[text]);
						stringBuilder.Append(text + " = " + propertyValueText + "\n");
					}
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>
		/// Gets the summary information text.
		/// </summary>
		/// <value>The summary information text.</value>
		public string SummaryInformationText
		{
			get
			{
				SummaryInformation summaryInformation = document.SummaryInformation;
				return GetPropertiesText(summaryInformation);
			}
		}

		/// <summary>
		/// Return the text of all the properties defined in
		/// the document.
		/// </summary>
		/// <value>All the text from the document.</value>
		public override string Text => SummaryInformationText + DocumentSummaryInformationText;

		/// <summary>
		/// Returns another text extractor, which is able to
		/// output the textual content of the document
		/// metadata / properties, such as author and title.
		/// </summary>
		/// <value>The metadata text extractor.</value>
		public override POITextExtractor MetadataTextExtractor
		{
			get
			{
				throw new InvalidOperationException("You already have the Metadata Text Extractor, not recursing!");
			}
		}

		public HPSFPropertiesExtractor(POITextExtractor mainExtractor)
			: base(mainExtractor)
		{
		}

		public HPSFPropertiesExtractor(POIDocument doc)
			: base(doc)
		{
		}

		public HPSFPropertiesExtractor(POIFSFileSystem fs)
			: base(new PropertiesOnlyDocument(fs))
		{
		}

		public HPSFPropertiesExtractor(NPOIFSFileSystem fs)
			: base(new PropertiesOnlyDocument(fs))
		{
		}

		/// <summary>
		/// Gets the properties text.
		/// </summary>
		/// <param name="ps">The ps.</param>
		/// <returns></returns>
		private static string GetPropertiesText(SpecialPropertySet ps)
		{
			if (ps == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			PropertyIDMap propertySetIDMap = ps.PropertySetIDMap;
			Property[] properties = ps.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				string str = properties[i].ID.ToString(CultureInfo.InvariantCulture);
				object obj = propertySetIDMap.Get(properties[i].ID);
				if (obj != null)
				{
					str = obj.ToString();
				}
				string propertyValueText = GetPropertyValueText(properties[i].Value);
				stringBuilder.Append(str + " = " + propertyValueText + "\n");
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Gets the property value text.
		/// </summary>
		/// <param name="val">The val.</param>
		/// <returns></returns>
		private static string GetPropertyValueText(object val)
		{
			if (val == null)
			{
				return "(not set)";
			}
			if (val is byte[])
			{
				byte[] array = (byte[])val;
				if (array.Length == 0)
				{
					return "";
				}
				if (array.Length == 1)
				{
					return array[0].ToString(CultureInfo.InvariantCulture);
				}
				if (array.Length == 2)
				{
					return LittleEndian.GetUShort(array).ToString(CultureInfo.InvariantCulture);
				}
				if (array.Length == 4)
				{
					return LittleEndian.GetUInt(array).ToString(CultureInfo.InvariantCulture);
				}
				return array.ToString();
			}
			return val.ToString();
		}
	}
}
