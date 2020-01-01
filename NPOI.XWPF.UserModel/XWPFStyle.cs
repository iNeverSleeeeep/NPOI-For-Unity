using NPOI.OpenXmlFormats.Wordprocessing;

namespace NPOI.XWPF.UserModel
{
	/// @author Philipp Epp
	public class XWPFStyle
	{
		private CT_Style ctStyle;

		protected XWPFStyles styles;

		/// Get StyleID of the style
		/// @return styleID		StyleID of the style
		public string StyleId
		{
			get
			{
				return ctStyle.styleId;
			}
			set
			{
				ctStyle.styleId = value;
			}
		}

		/// Get Type of the Style
		/// @return	ctType 
		public ST_StyleType StyleType
		{
			get
			{
				return ctStyle.type;
			}
			set
			{
				ctStyle.type = value;
			}
		}

		public string BasisStyleID
		{
			get
			{
				if (ctStyle.basedOn != null)
				{
					return ctStyle.basedOn.val;
				}
				return null;
			}
		}

		/// Get StyleID of the linked Style
		public string LinkStyleID
		{
			get
			{
				if (ctStyle.link != null)
				{
					return ctStyle.link.val;
				}
				return null;
			}
		}

		/// Get StyleID of the next style
		public string NextStyleID
		{
			get
			{
				if (ctStyle.next != null)
				{
					return ctStyle.next.val;
				}
				return null;
			}
		}

		public string Name
		{
			get
			{
				if (ctStyle.IsSetName())
				{
					return ctStyle.name.val;
				}
				return null;
			}
		}

		/// constructor
		/// @param style
		public XWPFStyle(CT_Style style)
			: this(style, null)
		{
		}

		/// constructor
		/// @param style
		/// @param styles
		public XWPFStyle(CT_Style style, XWPFStyles styles)
		{
			ctStyle = style;
			this.styles = styles;
		}

		/// Set style
		/// @param style		
		public void SetStyle(CT_Style style)
		{
			ctStyle = style;
		}

		/// Get ctStyle
		/// @return	ctStyle
		public CT_Style GetCTStyle()
		{
			return ctStyle;
		}

		/// Get styles
		/// @return styles		the styles to which this style belongs
		public XWPFStyles GetStyles()
		{
			return styles;
		}

		/// Compares the names of the Styles 
		/// @param compStyle
		public bool HasSameName(XWPFStyle compStyle)
		{
			CT_Style cTStyle = compStyle.GetCTStyle();
			string val = cTStyle.name.val;
			return val.Equals(ctStyle.name.val);
		}
	}
}
