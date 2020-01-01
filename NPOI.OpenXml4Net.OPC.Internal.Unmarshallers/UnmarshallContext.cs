using ICSharpCode.SharpZipLib.Zip;

namespace NPOI.OpenXml4Net.OPC.Internal.Unmarshallers
{
	/// Context needed for the unmarshall process of a part. This class is immutable.
	///
	/// @author Julien Chable
	/// @version 1.0
	public class UnmarshallContext
	{
		private OPCPackage _package;

		private PackagePartName partName;

		private ZipEntry zipEntry;

		/// @return the container
		internal OPCPackage Package
		{
			get
			{
				return _package;
			}
			set
			{
				_package = value;
			}
		}

		/// @return the partName
		internal PackagePartName PartName
		{
			get
			{
				return partName;
			}
			set
			{
				partName = value;
			}
		}

		/// @return the zipEntry
		internal ZipEntry ZipEntry
		{
			get
			{
				return zipEntry;
			}
			set
			{
				zipEntry = value;
			}
		}

		/// Constructor.
		///
		/// @param targetPackage
		///            Container.
		/// @param partName
		///            Name of the part to unmarshall.
		public UnmarshallContext(OPCPackage targetPackage, PackagePartName partName)
		{
			_package = targetPackage;
			this.partName = partName;
		}
	}
}
