using ICSharpCode.SharpZipLib.Zip;
using NPOI.OpenXml4Net.OPC.Internal.Marshallers;
using System;
using System.IO;

namespace NPOI.OpenXml4Net.OPC
{
	/// Zip implementation of a PackagePart.
	///
	/// @author Julien Chable
	/// @version 1.0
	/// @see PackagePart
	public class ZipPackagePart : PackagePart
	{
		/// The zip entry corresponding to this part.
		private ZipEntry zipEntry;

		/// Get the zip entry of this part.
		///
		/// @return The zip entry in the zip structure coresponding to this part.
		public ZipEntry ZipArchive
		{
			get
			{
				return zipEntry;
			}
		}

		/// Constructor.
		///
		/// @param container
		///            The container package.
		/// @param partName
		///            Part name.
		/// @param contentType
		///            Content type.
		/// @throws InvalidFormatException
		///             Throws if the content of this part invalid.
		public ZipPackagePart(OPCPackage container, PackagePartName partName, string contentType)
			: base(container, partName, contentType)
		{
		}

		/// Constructor.
		///
		/// @param container
		///            The container package.
		/// @param zipEntry
		///            The zip entry corresponding to this part.
		/// @param partName
		///            The part name.
		/// @param contentType
		///            Content type.
		/// @throws InvalidFormatException
		///             Throws if the content of this part is invalid.
		public ZipPackagePart(OPCPackage container, ZipEntry zipEntry, PackagePartName partName, string contentType)
			: base(container, partName, contentType)
		{
			this.zipEntry = zipEntry;
		}

		/// Implementation of the getInputStream() which return the inputStream of
		/// this part zip entry.
		///
		/// @return Input stream of this part zip entry.
		protected override Stream GetInputStreamImpl()
		{
			return ((ZipPackage)container).ZipArchive.GetInputStream(zipEntry);
		}

		protected override Stream GetOutputStreamImpl()
		{
			return null;
		}

		public override bool Save(Stream os)
		{
			return new ZipPartMarshaller().Marshall(this, os);
		}

		public override bool Load(Stream ios)
		{
			throw new InvalidOperationException("Method not implemented !");
		}

		public override void Close()
		{
			throw new InvalidOperationException("Method not implemented !");
		}

		public override void Flush()
		{
			throw new InvalidOperationException("Method not implemented !");
		}
	}
}
