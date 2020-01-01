using NPOI.OpenXml4Net.OPC.Internal;
using System;

namespace NPOI
{
	/// The core document properties
	public class CoreProperties
	{
		private PackagePropertiesPart part;

		public string Category
		{
			get
			{
				return part.GetCategoryProperty();
			}
			set
			{
				part.SetCategoryProperty(value);
			}
		}

		public string ContentStatus
		{
			get
			{
				return part.GetContentStatusProperty();
			}
			set
			{
				part.SetContentStatusProperty(value);
			}
		}

		public string ContentType
		{
			get
			{
				return part.GetContentTypeProperty();
			}
			set
			{
				part.SetContentTypeProperty(value);
			}
		}

		public DateTime? Created
		{
			get
			{
				return part.GetCreatedProperty();
			}
			set
			{
				part.SetCreatedProperty(value);
			}
		}

		public string Creator
		{
			get
			{
				return part.GetCreatorProperty();
			}
			set
			{
				part.SetCreatorProperty(value);
			}
		}

		public string Description
		{
			get
			{
				return part.GetDescriptionProperty();
			}
			set
			{
				part.SetDescriptionProperty(value);
			}
		}

		public string Identifier
		{
			get
			{
				return part.GetIdentifierProperty();
			}
			set
			{
				part.SetIdentifierProperty(value);
			}
		}

		public string Keywords
		{
			get
			{
				return part.GetKeywordsProperty();
			}
			set
			{
				part.SetKeywordsProperty(value);
			}
		}

		public DateTime? LastPrinted
		{
			get
			{
				return part.GetLastPrintedProperty();
			}
			set
			{
				part.SetLastPrintedProperty(value);
			}
		}

		public DateTime? Modified
		{
			get
			{
				return part.GetModifiedProperty();
			}
			set
			{
				part.SetModifiedProperty(value);
			}
		}

		public string Subject
		{
			get
			{
				return part.GetSubjectProperty();
			}
			set
			{
				part.SetSubjectProperty(value);
			}
		}

		public string Title
		{
			get
			{
				return part.GetTitleProperty();
			}
			set
			{
				part.SetTitleProperty(value);
			}
		}

		public string Revision
		{
			get
			{
				return part.GetRevisionProperty();
			}
			set
			{
				try
				{
					long.Parse(value);
					part.SetRevisionProperty(value);
				}
				catch (FormatException)
				{
				}
			}
		}

		internal CoreProperties(PackagePropertiesPart part)
		{
			this.part = part;
		}

		public void SetCreated(string date)
		{
			part.SetCreatedProperty(date);
		}

		public void SetLastPrinted(string date)
		{
			part.SetLastPrintedProperty(date);
		}

		public void SetModified(string date)
		{
			part.SetModifiedProperty(date);
		}

		public PackagePropertiesPart GetUnderlyingProperties()
		{
			return part;
		}
	}
}
