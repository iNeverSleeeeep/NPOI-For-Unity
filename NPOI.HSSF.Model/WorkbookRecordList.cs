using NPOI.HSSF.Record;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// List for records in Workbook
	/// </summary>
	public class WorkbookRecordList
	{
		private List<NPOI.HSSF.Record.Record> records = new List<NPOI.HSSF.Record.Record>();

		private int protpos;

		private int bspos;

		private int tabpos;

		private int fontpos;

		private int xfpos;

		private int backuppos;

		private int namepos;

		private int supbookpos;

		private int externsheetPos;

		private int palettepos = -1;

		/// <summary>
		/// Gets or sets the records.
		/// </summary>
		/// <value>The records.</value>
		public List<NPOI.HSSF.Record.Record> Records
		{
			get
			{
				return records;
			}
			set
			{
				records = value;
			}
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count => records.Count;

		/// <summary>
		/// Gets the <see cref="T:NPOI.HSSF.Record.Record" /> at the specified index.
		/// </summary>
		/// <value></value>
		public NPOI.HSSF.Record.Record this[int index]
		{
			get
			{
				return records[index];
			}
		}

		/// <summary>
		/// Gets or sets the protpos.
		/// </summary>
		/// <value>The protpos.</value>
		public int Protpos
		{
			get
			{
				return protpos;
			}
			set
			{
				protpos = value;
			}
		}

		/// <summary>
		/// Gets or sets the bspos.
		/// </summary>
		/// <value>The bspos.</value>
		public int Bspos
		{
			get
			{
				return bspos;
			}
			set
			{
				bspos = value;
			}
		}

		/// <summary>
		/// Gets or sets the tabpos.
		/// </summary>
		/// <value>The tabpos.</value>
		public int Tabpos
		{
			get
			{
				return tabpos;
			}
			set
			{
				tabpos = value;
			}
		}

		/// <summary>
		/// Gets or sets the fontpos.
		/// </summary>
		/// <value>The fontpos.</value>
		public int Fontpos
		{
			get
			{
				return fontpos;
			}
			set
			{
				fontpos = value;
			}
		}

		/// <summary>
		/// Gets or sets the xfpos.
		/// </summary>
		/// <value>The xfpos.</value>
		public int Xfpos
		{
			get
			{
				return xfpos;
			}
			set
			{
				xfpos = value;
			}
		}

		/// <summary>
		/// Gets or sets the backuppos.
		/// </summary>
		/// <value>The backuppos.</value>
		public int Backuppos
		{
			get
			{
				return backuppos;
			}
			set
			{
				backuppos = value;
			}
		}

		/// <summary>
		/// Gets or sets the palettepos.
		/// </summary>
		/// <value>The palettepos.</value>
		public int Palettepos
		{
			get
			{
				return palettepos;
			}
			set
			{
				palettepos = value;
			}
		}

		/// <summary>
		/// Gets or sets the namepos.
		/// </summary>
		/// <value>The namepos.</value>
		public int Namepos
		{
			get
			{
				return namepos;
			}
			set
			{
				namepos = value;
			}
		}

		/// <summary>
		/// Gets or sets the supbookpos.
		/// </summary>
		/// <value>The supbookpos.</value>
		public int Supbookpos
		{
			get
			{
				return supbookpos;
			}
			set
			{
				supbookpos = value;
			}
		}

		/// <summary>
		/// Gets or sets the externsheet pos.
		/// </summary>
		/// <value>The externsheet pos.</value>
		public int ExternsheetPos
		{
			get
			{
				return externsheetPos;
			}
			set
			{
				externsheetPos = value;
			}
		}

		/// <summary>
		/// Adds the specified pos.
		/// </summary>
		/// <param name="pos">The pos.</param>
		/// <param name="r">The r.</param>
		public void Add(int pos, NPOI.HSSF.Record.Record r)
		{
			records.Insert(pos, r);
			if (Protpos >= pos)
			{
				Protpos = protpos + 1;
			}
			if (Bspos >= pos)
			{
				Bspos = bspos + 1;
			}
			if (Tabpos >= pos)
			{
				Tabpos = tabpos + 1;
			}
			if (Fontpos >= pos)
			{
				Fontpos = fontpos + 1;
			}
			if (Xfpos >= pos)
			{
				Xfpos = xfpos + 1;
			}
			if (Backuppos >= pos)
			{
				Backuppos = backuppos + 1;
			}
			if (Namepos >= pos)
			{
				Namepos = namepos + 1;
			}
			if (Supbookpos >= pos)
			{
				Supbookpos = supbookpos + 1;
			}
			if (Palettepos != -1 && Palettepos >= pos)
			{
				Palettepos = palettepos + 1;
			}
			if (ExternsheetPos >= pos)
			{
				ExternsheetPos++;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return records.GetEnumerator();
		}

		/// <summary>
		/// Removes the specified record.
		/// </summary>
		/// <param name="record">The record.</param>
		public void Remove(NPOI.HSSF.Record.Record record)
		{
			int pos = records.IndexOf(record);
			Remove(pos);
		}

		/// <summary>
		/// Removes the specified position.
		/// </summary>
		/// <param name="pos">The position.</param>
		public void Remove(int pos)
		{
			records.RemoveAt(pos);
			if (Protpos >= pos)
			{
				Protpos = protpos - 1;
			}
			if (Bspos >= pos)
			{
				Bspos = bspos - 1;
			}
			if (Tabpos >= pos)
			{
				Tabpos = tabpos - 1;
			}
			if (Fontpos >= pos)
			{
				Fontpos = fontpos - 1;
			}
			if (Xfpos >= pos)
			{
				Xfpos = xfpos - 1;
			}
			if (Backuppos >= pos)
			{
				Backuppos = backuppos - 1;
			}
			if (Namepos >= pos)
			{
				Namepos--;
			}
			if (Supbookpos >= pos)
			{
				Supbookpos--;
			}
			if (Palettepos != -1 && Palettepos >= pos)
			{
				Palettepos = palettepos - 1;
			}
			if (ExternsheetPos >= pos)
			{
				ExternsheetPos--;
			}
		}
	}
}
