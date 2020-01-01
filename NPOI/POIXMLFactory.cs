using NPOI.OpenXml4Net.OPC;

namespace NPOI
{
	/// Defines a factory API that enables sub-classes to create instances of <code>POIXMLDocumentPart</code>
	///
	/// @author Yegor Kozlov
	public abstract class POIXMLFactory
	{
		/// Create a POIXMLDocumentPart from existing namespace part and relation. This method is called
		/// from {@link POIXMLDocument#load(POIXMLFactory)} when parsing a document
		///
		/// @param parent parent part
		/// @param rel   the namespace part relationship
		/// @param part  the PackagePart representing the Created instance
		/// @return A new instance of a POIXMLDocumentPart.
		public abstract POIXMLDocumentPart CreateDocumentPart(POIXMLDocumentPart parent, PackageRelationship rel, PackagePart part);

		/// Create a new POIXMLDocumentPart using the supplied descriptor. This method is used when Adding new parts
		/// to a document, for example, when Adding a sheet to a workbook, slide to a presentation, etc.
		///
		/// @param descriptor  describes the object to create
		/// @return A new instance of a POIXMLDocumentPart.
		public abstract POIXMLDocumentPart CreateDocumentPart(POIXMLRelation descriptor);
	}
}
