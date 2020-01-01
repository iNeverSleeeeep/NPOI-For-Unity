using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TransformEffect
	{
		private int sxField;

		private int syField;

		private int kxField;

		private int kyField;

		private long txField;

		private long tyField;

		[DefaultValue(100000)]
		[XmlAttribute]
		public int sx
		{
			get
			{
				return sxField;
			}
			set
			{
				sxField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(100000)]
		public int sy
		{
			get
			{
				return syField;
			}
			set
			{
				syField = value;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
		public int kx
		{
			get
			{
				return kxField;
			}
			set
			{
				kxField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int ky
		{
			get
			{
				return kyField;
			}
			set
			{
				kyField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
		public long tx
		{
			get
			{
				return txField;
			}
			set
			{
				txField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
		public long ty
		{
			get
			{
				return tyField;
			}
			set
			{
				tyField = value;
			}
		}

		public CT_TransformEffect()
		{
			sxField = 100000;
			syField = 100000;
			kxField = 0;
			kyField = 0;
			txField = 0L;
			tyField = 0L;
		}
	}
}
