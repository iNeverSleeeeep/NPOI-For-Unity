using NPOI.HPSF.Wellknown;
using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// Convenience class representing a Summary Information stream in a
	/// Microsoft Office document.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @see DocumentSummaryInformation
	/// @since 2002-02-09
	/// </summary>
	[Serializable]
	public class SummaryInformation : SpecialPropertySet
	{
		/// The document name a summary information stream usually has in a POIFS
		/// filesystem.
		public const string DEFAULT_STREAM_NAME = "\u0005SummaryInformation";

		public override PropertyIDMap PropertySetIDMap => PropertyIDMap.SummaryInformationProperties;

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
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
		/// Gets or sets the subject.
		/// </summary>
		/// <value>The subject.</value>
		public string Subject
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
		/// Gets or sets the author.
		/// </summary>
		/// <value>The author.</value>
		public string Author
		{
			get
			{
				return (string)GetProperty(4);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(4, value);
			}
		}

		/// <summary>
		/// Gets or sets the keywords.
		/// </summary>
		/// <value>The keywords.</value>
		public string Keywords
		{
			get
			{
				return (string)GetProperty(5);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(5, value);
			}
		}

		/// <summary>
		/// Gets or sets the comments.
		/// </summary>
		/// <value>The comments.</value>
		public string Comments
		{
			get
			{
				return (string)GetProperty(6);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(6, value);
			}
		}

		/// <summary>
		/// Gets or sets the template.
		/// </summary>
		/// <value>The template.</value>
		public string Template
		{
			get
			{
				return (string)GetProperty(7);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(7, value);
			}
		}

		/// <summary>
		/// Gets or sets the last author.
		/// </summary>
		/// <value>The last author.</value>
		public string LastAuthor
		{
			get
			{
				return (string)GetProperty(8);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(8, value);
			}
		}

		/// <summary>
		/// Gets or sets the rev number.
		/// </summary>
		/// <value>The rev number.</value>
		public string RevNumber
		{
			get
			{
				return (string)GetProperty(9);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(9, value);
			}
		}

		/// <summary>
		/// Returns the Total time spent in editing the document (or 0).
		/// </summary>
		/// <value>The Total time spent in editing the document or 0 if the {@link
		/// SummaryInformation} does not contain this information.</value>
		public long EditTime
		{
			get
			{
				if (GetProperty(10) == null)
				{
					return 0L;
				}
				DateTime dateTime = (DateTime)GetProperty(10);
				return Util.DateToFileTime(dateTime);
			}
			set
			{
				DateTime dateTime = Util.FiletimeToDate(value);
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(10, 64L, dateTime);
			}
		}

		/// <summary>
		/// Gets or sets the last printed time
		/// </summary>
		/// <value>The last printed time</value>
		/// Returns the last printed time (or <c>null</c>).
		public DateTime? LastPrinted
		{
			get
			{
				return (DateTime?)GetProperty(11);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(11, 64L, value);
			}
		}

		/// <summary>
		/// Gets or sets the create date time.
		/// </summary>
		/// <value>The create date time.</value>
		public DateTime? CreateDateTime
		{
			get
			{
				return (DateTime?)GetProperty(12);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(12, 64L, value);
			}
		}

		/// <summary>
		/// Gets or sets the last save date time.
		/// </summary>
		/// <value>The last save date time.</value>
		public DateTime? LastSaveDateTime
		{
			get
			{
				return (DateTime?)GetProperty(13);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(13, 64L, value);
			}
		}

		/// <summary>
		/// Gets or sets the page count or 0 if the {@link SummaryInformation} does
		/// not contain a page count.
		/// </summary>
		/// <value>The page count or 0 if the {@link SummaryInformation} does not
		/// contain a page count.</value>
		public int PageCount
		{
			get
			{
				return GetPropertyIntValue(14);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(14, value);
			}
		}

		/// <summary>
		/// Gets or sets the word count or 0 if the {@link SummaryInformation} does
		/// not contain a word count.
		/// </summary>
		/// <value>The word count.</value>
		public int WordCount
		{
			get
			{
				return GetPropertyIntValue(15);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(15, value);
			}
		}

		/// <summary>
		/// Gets or sets the character count or 0 if the {@link SummaryInformation}
		/// does not contain a char count.
		/// </summary>
		/// <value>The character count.</value>
		public int CharCount
		{
			get
			{
				return GetPropertyIntValue(16);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(16, value);
			}
		}

		/// <summary>
		/// Gets or sets the thumbnail (or <c>null</c>) <strong>when this
		/// method is implemented. Please note that the return type is likely To
		/// Change!</strong>
		/// <strong>Hint To developers:</strong> Drew Varner &lt;Drew.Varner
		/// -at- sc.edu&gt; said that this is an image in WMF or Clipboard (BMP?)
		/// format. However, we won't do any conversion into any image type but
		/// instead just return a byte array.
		/// </summary>
		/// <value>The thumbnail.</value>
		public byte[] Thumbnail
		{
			get
			{
				return (byte[])GetProperty(17);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(17, 30L, value);
			}
		}

		/// <summary>
		/// Gets or sets the name of the application.
		/// </summary>
		/// <value>The name of the application.</value>
		public string ApplicationName
		{
			get
			{
				return (string)GetProperty(18);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(18, value);
			}
		}

		/// <summary>
		/// Gets or sets a security code which is one of the following values:
		/// <ul>
		/// 	<li>0 if the {@link SummaryInformation} does not contain a
		/// security field or if there is no security on the document. Use
		/// {@link PropertySet#wasNull()} To distinguish between the two
		/// cases!</li>
		/// 	<li>1 if the document is password protected</li>
		/// 	<li>2 if the document is Read-only recommended</li>
		/// 	<li>4 if the document is Read-only enforced</li>
		/// 	<li>8 if the document is locked for annotations</li>
		/// </ul>
		/// </summary>
		/// <value>The security code</value>
		public int Security
		{
			get
			{
				return GetPropertyIntValue(19);
			}
			set
			{
				MutableSection mutableSection = (MutableSection)FirstSection;
				mutableSection.SetProperty(19, value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.SummaryInformation" /> class.
		/// </summary>
		/// <param name="ps">A property Set which should be Created from a summary
		/// information stream.</param>
		public SummaryInformation(PropertySet ps)
			: base(ps)
		{
			if (!IsSummaryInformation)
			{
				throw new UnexpectedPropertySetTypeException("Not a " + GetType().Name);
			}
		}

		/// <summary>
		/// Removes the title.
		/// </summary>
		public void RemoveTitle()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(2L);
		}

		/// <summary>
		/// Removes the subject.
		/// </summary>
		public void RemoveSubject()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(3L);
		}

		/// <summary>
		/// Removes the author.
		/// </summary>
		public void RemoveAuthor()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(4L);
		}

		/// <summary>
		/// Removes the keywords.
		/// </summary>
		public void RemoveKeywords()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(5L);
		}

		/// <summary>
		/// Removes the comments.
		/// </summary>
		public void RemoveComments()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(6L);
		}

		/// <summary>
		/// Removes the template.
		/// </summary>
		public void RemoveTemplate()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(7L);
		}

		/// <summary>
		/// Removes the last author.
		/// </summary>
		public void RemoveLastAuthor()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(8L);
		}

		/// <summary>
		/// Removes the rev number.
		/// </summary>
		public void RemoveRevNumber()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(9L);
		}

		/// <summary>
		/// Removes the edit time.
		/// </summary>
		public void RemoveEditTime()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(10L);
		}

		/// <summary>
		/// Removes the last printed.
		/// </summary>
		public void RemoveLastPrinted()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(11L);
		}

		/// <summary>
		/// Removes the create date time.
		/// </summary>
		public void RemoveCreateDateTime()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(12L);
		}

		/// <summary>
		/// Removes the last save date time.
		/// </summary>
		public void RemoveLastSaveDateTime()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(13L);
		}

		/// <summary>
		/// Removes the page count.
		/// </summary>
		public void RemovePageCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(14L);
		}

		/// <summary>
		/// Removes the word count.
		/// </summary>
		public void RemoveWordCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(15L);
		}

		/// <summary>
		/// Removes the char count.
		/// </summary>
		public void RemoveCharCount()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(16L);
		}

		/// <summary>
		/// Removes the thumbnail.
		/// </summary>
		public void RemoveThumbnail()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(17L);
		}

		/// <summary>
		/// Removes the name of the application.
		/// </summary>
		public void RemoveApplicationName()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(18L);
		}

		/// <summary>
		/// Removes the security code.
		/// </summary>
		public void RemoveSecurity()
		{
			MutableSection mutableSection = (MutableSection)FirstSection;
			mutableSection.RemoveProperty(19L);
		}
	}
}
