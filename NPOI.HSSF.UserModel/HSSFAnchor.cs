using NPOI.DDF;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// An anchor Is what specifics the position of a shape within a client object
	/// or within another containing shape.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public abstract class HSSFAnchor
	{
		protected bool _isHorizontallyFlipped;

		protected bool _isVerticallyFlipped;

		/// <summary>
		/// Gets or sets the DX1.
		/// </summary>
		/// <value>The DX1.</value>
		public abstract int Dx1
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the dy1.
		/// </summary>
		/// <value>The dy1.</value>
		public abstract int Dy1
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the dy2.
		/// </summary>
		/// <value>The dy2.</value>
		public abstract int Dy2
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the DX2.
		/// </summary>
		/// <value>The DX2.</value>
		public abstract int Dx2
		{
			get;
			set;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is horizontally flipped.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is horizontally flipped; otherwise, <c>false</c>.
		/// </value>
		public abstract bool IsHorizontallyFlipped
		{
			get;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is vertically flipped.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is vertically flipped; otherwise, <c>false</c>.
		/// </value>
		public abstract bool IsVerticallyFlipped
		{
			get;
		}

		public HSSFAnchor()
		{
			CreateEscherAnchor();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFAnchor" /> class.
		/// </summary>
		/// <param name="dx1">The DX1.</param>
		/// <param name="dy1">The dy1.</param>
		/// <param name="dx2">The DX2.</param>
		/// <param name="dy2">The dy2.</param>
		public HSSFAnchor(int dx1, int dy1, int dx2, int dy2)
		{
			CreateEscherAnchor();
			Dx1 = dx1;
			Dy1 = dy1;
			Dx2 = dx2;
			Dy2 = dy2;
		}

		public static HSSFAnchor CreateAnchorFromEscher(EscherContainerRecord container)
		{
			if (container.GetChildById(-4081) != null)
			{
				return new HSSFChildAnchor((EscherChildAnchorRecord)container.GetChildById(-4081));
			}
			if (container.GetChildById(-4080) != null)
			{
				return new HSSFClientAnchor((EscherClientAnchorRecord)container.GetChildById(-4080));
			}
			return null;
		}

		internal abstract EscherRecord GetEscherAnchor();

		protected abstract void CreateEscherAnchor();
	}
}
