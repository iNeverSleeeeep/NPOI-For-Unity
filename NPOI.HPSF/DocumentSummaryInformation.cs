using NPOI.HPSF.Wellknown;
using System;
using System.Collections;

namespace NPOI.HPSF
{
	/// <summary>
	/// Convenience class representing a DocumentSummary Information stream in a
	/// Microsoft Office document.
	/// @author Rainer Klute 
	/// klute@rainer-klute.de
	/// @author Drew Varner (Drew.Varner cloSeto sc.edu)
	/// @author robert_flaherty@hyperion.com
	/// @since 2002-02-09
	/// </summary>
	[Serializable]
	public class DocumentSummaryInformation : SpecialPropertySet
	{
		/// The document name a document summary information stream
		/// usually has in a POIFS filesystem.
		public const string DEFAULT_STREAM_NAME = "\u0005DocumentSummaryInformation";

		public override PropertyIDMap PropertySetIDMap => PropertyIDMap.DocumentSummaryInformationProperties;

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>The category value</value>
		public string Category
		{
			get
			{
				return (string)GetProperty(2);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(2, value);
			}
		}

		/// <summary>
		/// Gets or sets the presentation format (or null).
		/// </summary>
		/// <value>The presentation format value</value>
		public string PresentationFormat
		{
			get
			{
				return (string)GetProperty(3);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(3, value);
			}
		}

		/// <summary>
		/// Gets or sets the byte count or 0 if the {@link
		/// DocumentSummaryInformation} does not contain a byte count.
		/// </summary>
		/// <value>The byteCount value</value>
		public int ByteCount
		{
			get
			{
				return GetPropertyIntValue(4);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(4, value);
			}
		}

		/// <summary>
		/// Gets or sets the line count or 0 if the {@link
		/// DocumentSummaryInformation} does not contain a line count.
		/// </summary>
		/// <value>The line count value.</value>
		public int LineCount
		{
			get
			{
				return GetPropertyIntValue(5);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(5, value);
			}
		}

		/// <summary>
		/// Gets or sets the par count or 0 if the {@link
		/// DocumentSummaryInformation} does not contain a par count.
		/// </summary>
		/// <value>The par count value</value>
		public int ParCount
		{
			get
			{
				return GetPropertyIntValue(6);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(6, value);
			}
		}

		/// <summary>
		/// Gets or sets the slide count or 0 if the {@link
		/// DocumentSummaryInformation} does not contain a slide count.
		/// </summary>
		/// <value>The slide count value</value>
		public int SlideCount
		{
			get
			{
				return GetPropertyIntValue(7);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(7, value);
			}
		}

		/// <summary>
		/// Gets or sets the note count or 0 if the {@link
		/// DocumentSummaryInformation} does not contain a note count
		/// </summary>
		/// <value>The note count value</value>
		public int NoteCount
		{
			get
			{
				return GetPropertyIntValue(8);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(8, value);
			}
		}

		/// <summary>
		/// Gets or sets the hidden count or 0 if the {@link
		/// DocumentSummaryInformation} does not contain a hidden
		/// count.
		/// </summary>
		/// <value>The hidden count value.</value>
		public int HiddenCount
		{
			get
			{
				return GetPropertyIntValue(9);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)Sections[0];
				mutableSection.SetProperty(9, value);
			}
		}

		/// <summary>
		/// Returns the mmclip count or 0 if the {@link
		/// DocumentSummaryInformation} does not contain a mmclip
		/// count.
		/// </summary>
		/// <value>The mmclip count value.</value>
		public int MMClipCount
		{
			get
			{
				return GetPropertyIntValue(10);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(10, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:NPOI.HPSF.DocumentSummaryInformation" /> is scale.
		/// </summary>
		/// <value><c>true</c> if cropping is desired; otherwise, <c>false</c>.</value>
		public bool Scale
		{
			get
			{
				return GetPropertyBooleanValue(11);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(11, value);
			}
		}

		/// <summary>
		/// Gets or sets the heading pair (or null)
		/// </summary>
		/// <value>The heading pair value.</value>
		public byte[] HeadingPair
		{
			get
			{
				return (byte[])GetProperty(12);
			}
			set
			{
				throw new NotImplementedException("Writing byte arrays ");
			}
		}

		/// <summary>
		/// Gets or sets the doc parts.
		/// </summary>
		/// <value>The doc parts value</value>
		public byte[] Docparts
		{
			get
			{
				return (byte[])GetProperty(13);
			}
			set
			{
				throw new NotImplementedException("Writing byte arrays");
			}
		}

		/// <summary>
		/// Gets or sets the manager (or <c>null</c>).
		/// </summary>
		/// <value>The manager value</value>
		public string Manager
		{
			get
			{
				return (string)GetProperty(14);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(14, value);
			}
		}

		/// <summary>
		/// Gets or sets the company (or <c>null</c>).
		/// </summary>
		/// <value>The company value</value>
		public string Company
		{
			get
			{
				return (string)GetProperty(15);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(15, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [links dirty].
		/// </summary>
		/// <value><c>true</c> if the custom links are dirty.; otherwise, <c>false</c>.</value>
		public bool LinksDirty
		{
			get
			{
				return GetPropertyBooleanValue(16);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(16, value);
			}
		}

		/// <summary>
		/// Gets or sets the custom properties.
		/// </summary>
		/// <value>The custom properties.</value>
		public CustomProperties CustomProperties
		{
			get
			{
				CustomProperties customProperties = null;
				if (SectionCount >= 2)
				{
					customProperties = new CustomProperties();
					Section section = Sections[1];
					IDictionary dictionary = section.Dictionary;
					Property[] properties = section.Properties;
					int num = 0;
					foreach (Property property in properties)
					{
						long iD = property.ID;
						if (iD != 0 && iD != 1)
						{
							num++;
							CustomProperty customProperty = new CustomProperty(property, (string)dictionary[iD]);
							customProperties.Put(customProperty.Name, customProperty);
						}
					}
					if (customProperties.Count != num)
					{
						customProperties.IsPure = false;
					}
				}
				return customProperties;
			}
			set
			{
				EnsureSection2();
				MutableSection mutableSection = (MutableSection)Sections[1];
				IDictionary dictionary = value.Dictionary;
				mutableSection.Clear();
				int num = value.Codepage;
				if (num < 0)
				{
					num = mutableSection.Codepage;
				}
				if (num < 0)
				{
					num = 1200;
				}
				value.Codepage = num;
				mutableSection.Codepage = num;
				mutableSection.Dictionary = dictionary;
				IEnumerator enumerator = value.Values.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Property property = (Property)enumerator.Current;
					mutableSection.SetProperty(property);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.DocumentSummaryInformation" /> class.
		/// </summary>
		/// <param name="ps">A property Set which should be Created from a
		/// document summary information stream.</param>
		public DocumentSummaryInformation(PropertySet ps)
			: base(ps)
		{
			if (!IsDocumentSummaryInformation)
			{
				throw new UnexpectedPropertySetTypeException("Not a " + GetType().Name);
			}
		}

		/// <summary>
		/// Removes the category.
		/// </summary>
		public void RemoveCategory()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(2L);
		}

		/// <summary>
		/// Removes the presentation format.
		/// </summary>
		public void RemovePresentationFormat()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(3L);
		}

		/// <summary>
		/// Removes the byte count.
		/// </summary>
		public void RemoveByteCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(4L);
		}

		/// <summary>
		/// Removes the line count.
		/// </summary>
		public void RemoveLineCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(5L);
		}

		/// <summary>
		/// Removes the par count.
		/// </summary>
		public void RemoveParCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(6L);
		}

		/// <summary>
		/// Removes the slide count.
		/// </summary>
		public void RemoveSlideCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(7L);
		}

		/// <summary>
		/// Removes the note count.
		/// </summary>
		public void RemoveNoteCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(8L);
		}

		/// <summary>
		/// Removes the hidden count.
		/// </summary>
		public void RemoveHiddenCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(9L);
		}

		/// <summary>
		/// Removes the MMClip count.
		/// </summary>
		public void RemoveMMClipCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(10L);
		}

		/// <summary>
		/// Removes the scale.
		/// </summary>
		public void RemoveScale()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(11L);
		}

		/// <summary>
		/// Removes the heading pair.
		/// </summary>
		public void RemoveHeadingPair()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(12L);
		}

		/// <summary>
		/// Removes the doc parts.
		/// </summary>
		public void RemoveDocparts()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(13L);
		}

		/// <summary>
		/// Removes the manager.
		/// </summary>
		public void RemoveManager()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(14L);
		}

		/// <summary>
		/// Removes the company.
		/// </summary>
		public void RemoveCompany()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(15L);
		}

		/// <summary>
		/// Removes the links dirty.
		/// </summary>
		public void RemoveLinksDirty()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(16L);
		}

		/// <summary>
		/// Creates section 2 if it is not alReady present.
		/// </summary>
		private void EnsureSection2()
		{
			if (SectionCount < 2)
			{
				MutableSection mutableSection = new MutableSection();
				mutableSection.SetFormatID(SectionIDMap.DOCUMENT_SUMMARY_INFORMATION_ID2);
				AddSection(mutableSection);
			}
		}

		/// <summary>
		/// Removes the custom properties.
		/// </summary>
		public void RemoveCustomProperties()
		{
			if (SectionCount >= 2)
			{
				Sections.RemoveAt(1);
				return;
			}
			throw new HPSFRuntimeException("Illegal internal format of Document SummaryInformation stream: second section is missing.");
		}
	}
}
