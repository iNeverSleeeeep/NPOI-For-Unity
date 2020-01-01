namespace NPOI.DDF
{
	/// <summary>
	/// Defines the constants for the various possible shape paths.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherShapePathProperty : EscherSimpleProperty
	{
		public const int LINE_OF_STRAIGHT_SEGMENTS = 0;

		public const int CLOSED_POLYGON = 1;

		public const int CURVES = 2;

		public const int CLOSED_CURVES = 3;

		public const int COMPLEX = 4;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.DDF.EscherShapePathProperty" /> class.
		/// </summary>
		/// <param name="propertyNumber">The property number.</param>
		/// <param name="shapePath">The shape path.</param>
		public EscherShapePathProperty(short propertyNumber, int shapePath)
			: base(propertyNumber, isComplex: false, isBlipId: false, shapePath)
		{
		}
	}
}
