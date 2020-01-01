using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// This is the abstract base class for all escher properties.
	/// @see EscherOptRecord
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public abstract class EscherProperty
	{
		protected short id;

		/// <summary>
		/// Gets the id.
		/// </summary>
		/// <value>The id.</value>
		public virtual short Id => id;

		/// <summary>
		/// Gets the property number.
		/// </summary>
		/// <value>The property number.</value>
		public virtual short PropertyNumber => (short)(id & 0x3FFF);

		/// <summary>
		/// Gets a value indicating whether this instance is complex.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is complex; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsComplex => (id & -32768) != 0;

		/// <summary>
		/// Gets a value indicating whether this instance is blip id.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is blip id; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsBlipId => (id & 0x4000) != 0;

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public virtual string Name => EscherProperties.GetPropertyName(PropertyNumber);

		/// <summary>
		/// Most properties are just 6 bytes in Length.  Override this if we're
		/// dealing with complex properties.
		/// </summary>
		/// <value>The size of the property.</value>
		public virtual int PropertySize => 6;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.DDF.EscherProperty" /> class.
		/// </summary>
		/// <param name="id">The id is distinct from the actual property number.  The id includes the property number the blip id
		/// flag and an indicator whether the property is complex or not.</param>
		public EscherProperty(short id)
		{
			this.id = id;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.DDF.EscherProperty" /> class.The three parameters are combined to form a property
		/// id.
		/// </summary>
		/// <param name="propertyNumber">The property number.</param>
		/// <param name="isComplex">if set to <c>true</c> [is complex].</param>
		/// <param name="isBlipId">if set to <c>true</c> [is blip id].</param> 
		public EscherProperty(short propertyNumber, bool isComplex, bool isBlipId)
		{
			id = (short)(propertyNumber + (isComplex ? (-32768) : 0) + (isBlipId ? 16384 : 0));
		}

		public virtual string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append("<").Append(GetType().Name)
				.Append(" id=\"")
				.Append(Id)
				.Append("\" name=\"")
				.Append(Name)
				.Append("\" blipId=\"")
				.Append(IsBlipId)
				.Append("\"/>\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Escher properties consist of a simple fixed Length part and a complex variable Length part.
		/// The fixed Length part is Serialized first.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="pos">The pos.</param>
		/// <returns></returns>
		public abstract int SerializeSimplePart(byte[] data, int pos);

		/// <summary>
		/// Escher properties consist of a simple fixed Length part and a complex variable Length part.
		/// The fixed Length part is Serialized first.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="pos">The pos.</param>
		/// <returns></returns>
		public abstract int SerializeComplexPart(byte[] data, int pos);
	}
}
