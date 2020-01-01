namespace NPOI.HPSF
{
	/// <summary>
	/// This class represents custum properties in the document summary
	/// information stream. The difference To normal properties is that custom
	/// properties have an optional name. If the name is not <c>null</c> it
	/// will be maintained in the section's dictionary.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2006-02-09
	/// </summary>
	public class CustomProperty : MutableProperty
	{
		private string name;

		/// <summary>
		/// Gets or sets the property's name.
		/// </summary>
		/// <value>the property's name.</value>
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.CustomProperty" /> class.
		/// </summary>
		public CustomProperty()
		{
			name = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.CustomProperty" /> class.
		/// </summary>
		/// <param name="property">the property To copy</param>
		public CustomProperty(Property property)
			: this(property, "")
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.CustomProperty" /> class.
		/// </summary>
		/// <param name="property">This property's attributes are copied To the new custom
		/// property.</param>
		/// <param name="name">The new custom property's name.</param>
		public CustomProperty(Property property, string name)
			: base(property)
		{
			this.name = name;
		}

		/// <summary>
		/// Compares two custom properties for equality. The method returns
		/// <c>true</c> if all attributes of the two custom properties are
		/// equal.
		/// </summary>
		/// <param name="o">The custom property To Compare with.</param>
		/// <returns><c>true</c>
		///  if both custom properties are equal, else
		/// <c>false</c></returns>
		public bool EqualsContents(object o)
		{
			CustomProperty customProperty = (CustomProperty)o;
			string text = customProperty.Name;
			string text2 = Name;
			bool flag = true;
			flag = (text?.Equals(text2) ?? (text2 == null));
			if (flag && customProperty.ID == ID && customProperty.Type == Type)
			{
				return customProperty.Value.Equals(Value);
			}
			return false;
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		/// @see Object#GetHashCode()
		public override int GetHashCode()
		{
			return (int)ID;
		}
	}
}
