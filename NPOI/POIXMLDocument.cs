using NPOI.OpenXml4Net;
using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC;
using NPOI.POIFS.Common;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NPOI
{
	public abstract class POIXMLDocument : POIXMLDocumentPart
	{
		public static string DOCUMENT_CREATOR = "NPOI";

		public static string OLE_OBJECT_REL_TYPE = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject";

		public static string PACK_OBJECT_REL_TYPE = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package";

		/// The OPC Package 
		private OPCPackage pkg;

		/// The properties of the OPC namespace, opened as needed
		private POIXMLProperties properties;

		public OPCPackage Package
		{
			get
			{
				return pkg;
			}
		}

		protected PackagePart CorePart
		{
			get
			{
				return GetPackagePart();
			}
		}

		protected POIXMLDocument(OPCPackage pkg)
			: base(pkg)
		{
			this.pkg = pkg;
		}

		/// Wrapper to open a namespace, returning an IOException
		///  in the event of a problem.
		/// Works around shortcomings in java's this() constructor calls
		public static OPCPackage OpenPackage(string path)
		{
			try
			{
				return OPCPackage.Open(path);
			}
			catch (InvalidFormatException ex)
			{
				throw new IOException(ex.ToString());
			}
		}

		/// Retrieves all the PackageParts which are defined as
		///  relationships of the base document with the
		///  specified content type.
		protected PackagePart[] GetRelatedByType(string contentType)
		{
			PackageRelationshipCollection relationshipsByType = GetPackagePart().GetRelationshipsByType(contentType);
			PackagePart[] array = new PackagePart[relationshipsByType.Size];
			int num = 0;
			foreach (PackageRelationship item in relationshipsByType)
			{
				array[num] = GetPackagePart().GetRelatedPart(item);
				num++;
			}
			return array;
		}

		/// Checks that the supplied Stream (which MUST
		///  support mark and reSet, or be a PushbackStream)
		///  has a OOXML (zip) header at the start of it.
		/// If your Stream does not support mark / reSet,
		///  then wrap it in a PushBackStream, then be
		///  sure to always use that, and not the original!
		/// @param inp An Stream which supports either mark/reSet, or is a PushbackStream
		public static bool HasOOXMLHeader(Stream inp)
		{
			byte[] array = new byte[4];
			IOUtils.ReadFully(inp, array);
			if (inp is PushbackStream)
			{
				PushbackStream pushbackStream = (PushbackStream)inp;
				pushbackStream.Position -= 4L;
			}
			else
			{
				inp.Position = 0L;
			}
			if (array[0] == POIFSConstants.OOXML_FILE_HEADER[0] && array[1] == POIFSConstants.OOXML_FILE_HEADER[1] && array[2] == POIFSConstants.OOXML_FILE_HEADER[2])
			{
				return array[3] == POIFSConstants.OOXML_FILE_HEADER[3];
			}
			return false;
		}

		/// Get the document properties. This gives you access to the
		///  core ooxml properties, and the extended ooxml properties.
		public POIXMLProperties GetProperties()
		{
			if (properties == null)
			{
				try
				{
					properties = new POIXMLProperties(pkg);
				}
				catch (Exception ex)
				{
					throw new POIXMLException(ex);
				}
			}
			return properties;
		}

		/// Get the document's embedded files.
		public abstract List<PackagePart> GetAllEmbedds();

		protected void Load(POIXMLFactory factory)
		{
			Dictionary<PackagePart, POIXMLDocumentPart> dictionary = new Dictionary<PackagePart, POIXMLDocumentPart>();
			try
			{
				Read(factory, dictionary);
			}
			catch (OpenXml4NetException ex)
			{
				throw new POIXMLException(ex);
			}
			OnDocumentRead();
			dictionary.Clear();
		}

		/// Write out this document to an Outputstream.
		///
		/// @param stream - the java Stream you wish to write the file to
		///
		/// @exception IOException if anything can't be written.
		public void Write(Stream stream)
		{
			if (!GetProperties().CustomProperties.Contains("Generator"))
			{
				GetProperties().CustomProperties.AddProperty("Generator", "NPOI");
			}
			if (!GetProperties().CustomProperties.Contains("Generator Version"))
			{
				GetProperties().CustomProperties.AddProperty("Generator Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
			}
			List<PackagePart> list = new List<PackagePart>();
			OnSave(list);
			list.Clear();
			GetProperties().Commit();
			Package.Save(stream);
		}
	}
}
