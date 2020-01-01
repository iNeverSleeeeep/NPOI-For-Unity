using System;

namespace NPOI
{
	/// Represents a descriptor of a OOXML relation.
	///
	/// @author Yegor Kozlov
	public abstract class POIXMLRelation
	{
		/// Describes the content stored in a part.
		protected string _type;

		/// The kind of connection between a source part and a target part in a namespace.
		protected string _relation;

		/// The path component of a pack URI.
		protected string _defaultName;

		/// Defines what object is used to construct instances of this relationship
		private Type _cls;

		/// Return the content type. Content types define a media type, a subtype, and an
		/// optional set of parameters, as defined in RFC 2616.
		///
		/// @return the content type
		public string ContentType
		{
			get
			{
				return _type;
			}
		}

		/// Return the relationship, the kind of connection between a source part and a target part in a namespace.
		/// Relationships make the connections between parts directly discoverable without looking at the content
		/// in the parts, and without altering the parts themselves.
		///
		/// @return the relationship
		public string Relation
		{
			get
			{
				return _relation;
			}
		}

		/// Return the default part name. Part names are used to refer to a part in the context of a
		/// namespace, typically as part of a URI.
		///
		/// @return the default part name
		public string DefaultFileName
		{
			get
			{
				return _defaultName;
			}
		}

		/// Return type of the obejct used to construct instances of this relationship
		///
		/// @return the class of the object used to construct instances of this relation
		public Type RelationClass
		{
			get
			{
				return _cls;
			}
		}

		/// Instantiates a POIXMLRelation.
		///
		/// @param type content type
		/// @param rel  relationship
		/// @param defaultName default item name
		/// @param cls defines what object is used to construct instances of this relationship
		public POIXMLRelation(string type, string rel, string defaultName, Type cls)
		{
			_type = type;
			_relation = rel;
			_defaultName = defaultName;
			_cls = cls;
		}

		/// Instantiates a POIXMLRelation.
		///
		/// @param type content type
		/// @param rel  relationship
		/// @param defaultName default item name
		public POIXMLRelation(string type, string rel, string defaultName)
			: this(type, rel, defaultName, null)
		{
		}

		/// Returns the filename for the nth one of these,
		///  e.g. /xl/comments4.xml
		public string GetFileName(int index)
		{
			if (_defaultName.IndexOf("#") == -1)
			{
				return DefaultFileName;
			}
			return _defaultName.Replace("#", index.ToString());
		}
	}
}
