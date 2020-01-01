using NPOI.DDF;
using System;

namespace NPOI.HSSF.UserModel
{
	public class HSSFChildAnchor : HSSFAnchor
	{
		private EscherChildAnchorRecord _escherChildAnchor;

		public override bool IsHorizontallyFlipped => _isHorizontallyFlipped;

		public override bool IsVerticallyFlipped => _isVerticallyFlipped;

		public override int Dx1
		{
			get
			{
				return _escherChildAnchor.Dx1;
			}
			set
			{
				_escherChildAnchor.Dx1 = (short)value;
			}
		}

		public override int Dx2
		{
			get
			{
				return _escherChildAnchor.Dx2;
			}
			set
			{
				_escherChildAnchor.Dx2 = (short)value;
			}
		}

		public override int Dy1
		{
			get
			{
				return _escherChildAnchor.Dy1;
			}
			set
			{
				_escherChildAnchor.Dy1 = (short)value;
			}
		}

		public override int Dy2
		{
			get
			{
				return _escherChildAnchor.Dy2;
			}
			set
			{
				_escherChildAnchor.Dy2 = (short)value;
			}
		}

		/// create anchor from existing file
		/// @param escherChildAnchorRecord
		public HSSFChildAnchor(EscherChildAnchorRecord escherChildAnchorRecord)
		{
			_escherChildAnchor = escherChildAnchorRecord;
		}

		public HSSFChildAnchor()
		{
			_escherChildAnchor = new EscherChildAnchorRecord();
		}

		/// create anchor from scratch
		/// @param dx1 x coordinate of the left up corner
		/// @param dy1 y coordinate of the left up corner
		/// @param dx2 x coordinate of the right down corner
		/// @param dy2 y coordinate of the right down corner
		public HSSFChildAnchor(int dx1, int dy1, int dx2, int dy2)
			: base(Math.Min(dx1, dx2), Math.Min(dy1, dy2), Math.Max(dx1, dx2), Math.Max(dy1, dy2))
		{
			if (dx1 > dx2)
			{
				_isHorizontallyFlipped = true;
			}
			if (dy1 > dy2)
			{
				_isVerticallyFlipped = true;
			}
		}

		/// @param dx1 x coordinate of the left up corner
		/// @param dy1 y coordinate of the left up corner
		/// @param dx2 x coordinate of the right down corner
		/// @param dy2 y coordinate of the right down corner
		public void SetAnchor(int dx1, int dy1, int dx2, int dy2)
		{
			Dx1 = Math.Min(dx1, dx2);
			Dy1 = Math.Min(dy1, dy2);
			Dx2 = Math.Max(dx1, dx2);
			Dy2 = Math.Max(dy1, dy2);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj == this)
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			HSSFChildAnchor hSSFChildAnchor = (HSSFChildAnchor)obj;
			if (hSSFChildAnchor.Dx1 == Dx1 && hSSFChildAnchor.Dx2 == Dx2 && hSSFChildAnchor.Dy1 == Dy1)
			{
				return hSSFChildAnchor.Dy2 == Dy2;
			}
			return false;
		}

		internal override EscherRecord GetEscherAnchor()
		{
			return _escherChildAnchor;
		}

		protected override void CreateEscherAnchor()
		{
			_escherChildAnchor = new EscherChildAnchorRecord();
		}
	}
}
