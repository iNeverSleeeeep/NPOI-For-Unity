using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Window1 Record
	/// Description:  Stores the attributes of the workbook window.  This Is basically
	///               so the gui knows how big to make the window holding the spReadsheet
	///               document.
	/// REFERENCE:  PG 421 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class WindowOneRecord : StandardRecord
	{
		public const short sid = 61;

		private short field_1_h_hold;

		private short field_2_v_hold;

		private short field_3_width;

		private short field_4_height;

		private short field_5_options;

		private static BitField hidden = BitFieldFactory.GetInstance(1);

		private static BitField iconic = BitFieldFactory.GetInstance(2);

		private static BitField reserved = BitFieldFactory.GetInstance(4);

		private static BitField hscroll = BitFieldFactory.GetInstance(8);

		private static BitField vscroll = BitFieldFactory.GetInstance(16);

		private static BitField tabs = BitFieldFactory.GetInstance(32);

		private int field_6_active_sheet;

		private int field_7_first_visible_tab;

		private short field_8_num_selected_tabs;

		private short field_9_tab_width_ratio;

		/// Get the horizontal position of the window (in 1/20ths of a point)
		/// @return h - horizontal location
		public short HorizontalHold
		{
			get
			{
				return field_1_h_hold;
			}
			set
			{
				field_1_h_hold = value;
			}
		}

		/// Get the vertical position of the window (in 1/20ths of a point)
		/// @return v - vertical location
		public short VerticalHold
		{
			get
			{
				return field_2_v_hold;
			}
			set
			{
				field_2_v_hold = value;
			}
		}

		/// Get the width of the window
		/// @return width
		public short Width
		{
			get
			{
				return field_3_width;
			}
			set
			{
				field_3_width = value;
			}
		}

		/// Get the height of the window
		/// @return height
		public short Height
		{
			get
			{
				return field_4_height;
			}
			set
			{
				field_4_height = value;
			}
		}

		/// Get the options bitmask (see bit Setters)
		///
		/// @return o - the bitmask
		public short Options
		{
			get
			{
				return field_5_options;
			}
			set
			{
				field_5_options = value;
			}
		}

		/// Get whether the window Is hidden or not
		/// @return Ishidden or not
		public bool Hidden
		{
			get
			{
				return hidden.IsSet(field_5_options);
			}
			set
			{
				field_5_options = hidden.SetShortBoolean(field_5_options, value);
			}
		}

		/// Get whether the window has been iconized or not
		/// @return iconize  or not
		public bool Iconic
		{
			get
			{
				return iconic.IsSet(field_5_options);
			}
			set
			{
				field_5_options = iconic.SetShortBoolean(field_5_options, value);
			}
		}

		/// Get whether to Display the horizontal scrollbar or not
		/// @return Display or not
		public bool DisplayHorizontalScrollbar
		{
			get
			{
				return hscroll.IsSet(field_5_options);
			}
			set
			{
				field_5_options = hscroll.SetShortBoolean(field_5_options, value);
			}
		}

		/// Get whether to Display the vertical scrollbar or not
		/// @return Display or not
		public bool DisplayVerticalScrollbar
		{
			get
			{
				return vscroll.IsSet(field_5_options);
			}
			set
			{
				field_5_options = vscroll.SetShortBoolean(field_5_options, value);
			}
		}

		/// Get whether to Display the tabs or not
		/// @return Display or not
		public bool DisplayTabs
		{
			get
			{
				return tabs.IsSet(field_5_options);
			}
			set
			{
				field_5_options = tabs.SetShortBoolean(field_5_options, value);
			}
		}

		/// @return the index of the currently Displayed sheet 
		public int ActiveSheetIndex
		{
			get
			{
				return field_6_active_sheet;
			}
			set
			{
				field_6_active_sheet = value;
			}
		}

		/// deprecated May 2008
		/// @deprecated - Misleading name - use GetActiveSheetIndex() 
		[Obsolete]
		public short SelectedTab
		{
			get
			{
				return (short)ActiveSheetIndex;
			}
			set
			{
				ActiveSheetIndex = value;
			}
		}

		/// @return the first visible sheet in the worksheet tab-bar. 
		/// I.E. the scroll position of the tab-bar.
		public int FirstVisibleTab
		{
			get
			{
				return field_7_first_visible_tab;
			}
			set
			{
				field_7_first_visible_tab = value;
			}
		}

		/// deprecated May 2008
		/// @deprecated - Misleading name - use GetFirstVisibleTab() 
		[Obsolete]
		public short DisplayedTab
		{
			get
			{
				return (short)FirstVisibleTab;
			}
			set
			{
				FirstVisibleTab = value;
			}
		}

		/// Get the number of selected tabs
		/// @return number of tabs
		public short NumSelectedTabs
		{
			get
			{
				return field_8_num_selected_tabs;
			}
			set
			{
				field_8_num_selected_tabs = value;
			}
		}

		/// ratio of the width of the tabs to the horizontal scrollbar
		/// @return ratio
		public short TabWidthRatio
		{
			get
			{
				return field_9_tab_width_ratio;
			}
			set
			{
				field_9_tab_width_ratio = value;
			}
		}

		protected override int DataSize => 18;

		public override short Sid => 61;

		public WindowOneRecord()
		{
		}

		/// Constructs a WindowOne record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public WindowOneRecord(RecordInputStream in1)
		{
			field_1_h_hold = in1.ReadShort();
			field_2_v_hold = in1.ReadShort();
			field_3_width = in1.ReadShort();
			field_4_height = in1.ReadShort();
			field_5_options = in1.ReadShort();
			field_6_active_sheet = in1.ReadShort();
			field_7_first_visible_tab = in1.ReadShort();
			field_8_num_selected_tabs = in1.ReadShort();
			field_9_tab_width_ratio = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WINDOW1]\n");
			stringBuilder.Append("    .h_hold          = ").Append(StringUtil.ToHexString(HorizontalHold)).Append("\n");
			stringBuilder.Append("    .v_hold          = ").Append(StringUtil.ToHexString(VerticalHold)).Append("\n");
			stringBuilder.Append("    .width           = ").Append(StringUtil.ToHexString(Width)).Append("\n");
			stringBuilder.Append("    .height          = ").Append(StringUtil.ToHexString(Height)).Append("\n");
			stringBuilder.Append("    .options         = ").Append(StringUtil.ToHexString(Options)).Append("\n");
			stringBuilder.Append("        .hidden      = ").Append(Hidden).Append("\n");
			stringBuilder.Append("        .iconic      = ").Append(Iconic).Append("\n");
			stringBuilder.Append("        .hscroll     = ").Append(DisplayHorizontalScrollbar).Append("\n");
			stringBuilder.Append("        .vscroll     = ").Append(DisplayVerticalScrollbar).Append("\n");
			stringBuilder.Append("        .tabs        = ").Append(DisplayTabs).Append("\n");
			stringBuilder.Append("    .activeSheet     = ").Append(StringUtil.ToHexString(ActiveSheetIndex)).Append("\n");
			stringBuilder.Append("    .firstVisibleTab    = ").Append(StringUtil.ToHexString(FirstVisibleTab)).Append("\n");
			stringBuilder.Append("    .numselectedtabs = ").Append(StringUtil.ToHexString(NumSelectedTabs)).Append("\n");
			stringBuilder.Append("    .tabwidthratio   = ").Append(StringUtil.ToHexString(TabWidthRatio)).Append("\n");
			stringBuilder.Append("[/WINDOW1]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(HorizontalHold);
			out1.WriteShort(VerticalHold);
			out1.WriteShort(Width);
			out1.WriteShort(Height);
			out1.WriteShort(Options);
			out1.WriteShort(ActiveSheetIndex);
			out1.WriteShort(FirstVisibleTab);
			out1.WriteShort(NumSelectedTabs);
			out1.WriteShort(TabWidthRatio);
		}
	}
}
