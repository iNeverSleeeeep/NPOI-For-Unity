namespace NPOI.DDF
{
	/// <summary>
	/// This class stores the type and description of an escher property.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherPropertyMetaData
	{
		public const byte TYPE_UNKNOWN = 0;

		public const byte TYPE_bool = 1;

		public const byte TYPE_RGB = 2;

		public const byte TYPE_SHAPEPATH = 3;

		public const byte TYPE_SIMPLE = 4;

		public const byte TYPE_ARRAY = 5;

		private string description;

		private byte type;

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description => description;

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public byte Type => type;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.DDF.EscherPropertyMetaData" /> class.
		/// </summary>
		/// <param name="description">The description of the escher property.</param>
		public EscherPropertyMetaData(string description)
		{
			this.description = description;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.DDF.EscherPropertyMetaData" /> class.
		/// </summary>
		/// <param name="description">The description of the escher property.</param>
		/// <param name="type">The type of the property.</param> 
		public EscherPropertyMetaData(string description, byte type)
		{
			this.description = description;
			this.type = type;
		}
	}
}
