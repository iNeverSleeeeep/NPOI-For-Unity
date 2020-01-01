using NPOI.OpenXml4Net.OPC.Internal.Marshallers;
using System.IO;

namespace NPOI.OpenXml4Net.OPC.Internal
{
	public class MemoryPackagePart : PackagePart
	{
		/// Storage for the part data.
		internal MemoryStream data;

		/// Constructor.
		///
		/// @param pack
		///            The owner package.
		/// @param partName
		///            The part name.
		/// @param contentType
		///            The content type.
		/// @throws InvalidFormatException
		///             If the specified URI is not OPC compliant.
		public MemoryPackagePart(OPCPackage pack, PackagePartName partName, string contentType)
			: base(pack, partName, contentType)
		{
		}

		/// Constructor.
		///
		/// @param pack
		///            The owner package.
		/// @param partName
		///            The part name.
		/// @param contentType
		///            The content type.
		/// @param loadRelationships
		///            Specify if the relationships will be loaded.
		/// @throws InvalidFormatException
		///             If the specified URI is not OPC compliant.
		public MemoryPackagePart(OPCPackage pack, PackagePartName partName, string contentType, bool loadRelationships)
			: base(pack, partName, new ContentType(contentType), loadRelationships)
		{
		}

		protected override Stream GetInputStreamImpl()
		{
			if (data == null)
			{
				return new MemoryStream();
			}
			MemoryStream memoryStream = new MemoryStream((int)data.Length);
			data.Position = 0L;
			StreamHelper.CopyStream(data, memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		protected override Stream GetOutputStreamImpl()
		{
			return new MemoryPackagePartOutputStream(this);
		}

		public void Clear()
		{
			data = null;
		}

		public override bool Save(Stream os)
		{
			return new ZipPartMarshaller().Marshall(this, os);
		}

		public override bool Load(Stream ios)
		{
			StreamHelper.CopyStream(ios, data);
			return true;
		}

		public override void Close()
		{
		}

		public override void Flush()
		{
		}
	}
}
