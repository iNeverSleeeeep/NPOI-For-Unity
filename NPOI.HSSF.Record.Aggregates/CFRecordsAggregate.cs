using NPOI.HSSF.Model;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.HSSF.Record.Aggregates
{
	/// <summary>
	///
	/// </summary>
	/// CFRecordsAggregate - aggregates Conditional Formatting records CFHeaderRecord
	/// and number of up to three CFRuleRecord records toGether to simplify
	/// access to them.
	/// @author Dmitriy Kumshayev
	public class CFRecordsAggregate : RecordAggregate
	{
		/// Excel allows up to 3 conditional formating rules 
		private const int MAX_CONDTIONAL_FORMAT_RULES = 3;

		public const short sid = -2008;

		private CFHeaderRecord header;

		/// List of CFRuleRecord objects 
		private List<CFRuleRecord> rules;

		public override short Sid => -2008;

		public CFHeaderRecord Header => header;

		public int NumberOfRules => rules.Count;

		/// @return sum of sizes of all aggregated records
		public override int RecordSize
		{
			get
			{
				int num = 0;
				if (header != null)
				{
					num += header.RecordSize;
				}
				if (rules != null)
				{
					IEnumerator enumerator = rules.GetEnumerator();
					while (enumerator.MoveNext())
					{
						num += ((Record)enumerator.Current).RecordSize;
					}
				}
				return num;
			}
		}

		private CFRecordsAggregate(CFHeaderRecord pHeader, CFRuleRecord[] pRules)
		{
			if (pHeader == null)
			{
				throw new ArgumentException("header must not be null");
			}
			if (pRules == null)
			{
				throw new ArgumentException("rules must not be null");
			}
			if (pRules.Length > 3)
			{
				throw new ArgumentException("No more than " + 3 + " rules may be specified");
			}
			header = pHeader;
			rules = new List<CFRuleRecord>(3);
			for (int i = 0; i < pRules.Length; i++)
			{
				rules.Add(pRules[i]);
			}
		}

		public CFRecordsAggregate(CellRangeAddress[] regions, CFRuleRecord[] rules)
			: this(new CFHeaderRecord(regions, rules.Length), rules)
		{
		}

		/// <summary>
		/// Create CFRecordsAggregate from a list of CF Records
		/// </summary>
		/// <param name="rs">list of Record objects</param>
		public static CFRecordsAggregate CreateCFAggregate(RecordStream rs)
		{
			Record next = rs.GetNext();
			if (next.Sid != 432)
			{
				throw new InvalidOperationException("next record sid was " + next.Sid + " instead of " + (short)432 + " as expected");
			}
			CFHeaderRecord cFHeaderRecord = (CFHeaderRecord)next;
			int numberOfConditionalFormats = cFHeaderRecord.NumberOfConditionalFormats;
			CFRuleRecord[] array = new CFRuleRecord[numberOfConditionalFormats];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (CFRuleRecord)rs.GetNext();
			}
			return new CFRecordsAggregate(cFHeaderRecord, array);
		}

		/// <summary>
		/// Create CFRecordsAggregate from a list of CF Records
		/// </summary>
		/// <param name="recs">list of Record objects</param>
		/// <param name="pOffset">position of CFHeaderRecord object in the list of Record objects</param>
		public static CFRecordsAggregate CreateCFAggregate(IList recs, int pOffset)
		{
			Record record = (Record)recs[pOffset];
			if (record.Sid != 432)
			{
				throw new InvalidOperationException("next record sid was " + record.Sid + " instead of " + (short)432 + " as expected");
			}
			CFHeaderRecord cFHeaderRecord = (CFHeaderRecord)record;
			int numberOfConditionalFormats = cFHeaderRecord.NumberOfConditionalFormats;
			CFRuleRecord[] array = new CFRuleRecord[numberOfConditionalFormats];
			int num = pOffset;
			int i;
			for (i = 0; i < array.Length; i++)
			{
				num++;
				if (num >= recs.Count)
				{
					break;
				}
				record = (Record)recs[num];
				if (!(record is CFRuleRecord))
				{
					break;
				}
				array[i] = (CFRuleRecord)record;
			}
			if (i < numberOfConditionalFormats)
			{
				cFHeaderRecord.NumberOfConditionalFormats = numberOfConditionalFormats;
				CFRuleRecord[] array2 = new CFRuleRecord[i];
				Array.Copy(array, 0, array2, 0, i);
				array = array2;
			}
			return new CFRecordsAggregate(cFHeaderRecord, array);
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(header);
			for (int i = 0; i < rules.Count; i++)
			{
				CFRuleRecord r = rules[i];
				rv.VisitRecord(r);
			}
		}

		/// <summary>
		/// Create a deep Clone of the record
		/// </summary>
		public CFRecordsAggregate CloneCFAggregate()
		{
			CFRuleRecord[] array = new CFRuleRecord[rules.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (CFRuleRecord)GetRule(i).Clone();
			}
			return new CFRecordsAggregate((CFHeaderRecord)header.Clone(), array);
		}

		/// <summary>
		/// called by the class that is responsible for writing this sucker.
		/// Subclasses should implement this so that their data is passed back in a
		/// byte array.
		/// </summary>
		/// <param name="offset">The offset to begin writing at</param>
		/// <param name="data">The data byte array containing instance data</param>
		/// <returns> number of bytes written</returns>
		public override int Serialize(int offset, byte[] data)
		{
			int count = rules.Count;
			header.NumberOfConditionalFormats = count;
			int num = offset + header.Serialize(offset, data);
			for (int i = 0; i < count; i++)
			{
				num += GetRule(i).Serialize(num, data);
			}
			return num - offset;
		}

		private void CheckRuleIndex(int idx)
		{
			if (idx < 0 || idx >= rules.Count)
			{
				throw new ArgumentException("Bad rule record index (" + idx + ") nRules=" + rules.Count);
			}
		}

		public CFRuleRecord GetRule(int idx)
		{
			CheckRuleIndex(idx);
			return rules[idx];
		}

		public void SetRule(int idx, CFRuleRecord r)
		{
			CheckRuleIndex(idx);
			rules[idx] = r;
		}

		/// @return <c>false</c> if this whole {@link CFHeaderRecord} / {@link CFRuleRecord}s should be deleted
		public bool UpdateFormulasAfterCellShift(FormulaShifter shifter, int currentExternSheetIx)
		{
			CellRangeAddress[] cellRanges = header.CellRanges;
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			foreach (CellRangeAddress cellRangeAddress in cellRanges)
			{
				CellRangeAddress cellRangeAddress2 = ShiftRange(shifter, cellRangeAddress, currentExternSheetIx);
				if (cellRangeAddress2 == null)
				{
					flag = true;
				}
				else
				{
					arrayList.Add(cellRangeAddress2);
					if (cellRangeAddress2 != cellRangeAddress)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				int count = arrayList.Count;
				if (count == 0)
				{
					return false;
				}
				CellRangeAddress[] array = new CellRangeAddress[count];
				array = (CellRangeAddress[])arrayList.ToArray(typeof(CellRangeAddress));
				header.CellRanges = array;
			}
			for (int j = 0; j < rules.Count; j++)
			{
				CFRuleRecord cFRuleRecord = rules[j];
				Ptg[] parsedExpression = cFRuleRecord.ParsedExpression1;
				if (parsedExpression != null && shifter.AdjustFormula(parsedExpression, currentExternSheetIx))
				{
					cFRuleRecord.ParsedExpression1 = parsedExpression;
				}
				parsedExpression = cFRuleRecord.ParsedExpression2;
				if (parsedExpression != null && shifter.AdjustFormula(parsedExpression, currentExternSheetIx))
				{
					cFRuleRecord.ParsedExpression2 = parsedExpression;
				}
			}
			return true;
		}

		private static CellRangeAddress ShiftRange(FormulaShifter shifter, CellRangeAddress cra, int currentExternSheetIx)
		{
			AreaPtg areaPtg = new AreaPtg(cra.FirstRow, cra.LastRow, cra.FirstColumn, cra.LastColumn, firstRowRelative: false, lastRowRelative: false, firstColRelative: false, lastColRelative: false);
			Ptg[] array = new Ptg[1]
			{
				areaPtg
			};
			if (!shifter.AdjustFormula(array, currentExternSheetIx))
			{
				return cra;
			}
			Ptg ptg = array[0];
			if (ptg is AreaPtg)
			{
				AreaPtg areaPtg2 = (AreaPtg)ptg;
				return new CellRangeAddress(areaPtg2.FirstRow, areaPtg2.LastRow, areaPtg2.FirstColumn, areaPtg2.LastColumn);
			}
			if (ptg is AreaErrPtg)
			{
				return null;
			}
			throw new InvalidCastException("Unexpected shifted ptg class (" + ptg.GetType().Name + ")");
		}

		public void AddRule(CFRuleRecord r)
		{
			if (rules.Count >= 3)
			{
				throw new InvalidOperationException("Cannot have more than " + 3 + " conditional format rules");
			}
			rules.Add(r);
			header.NumberOfConditionalFormats = rules.Count;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CF]\n");
			if (header != null)
			{
				stringBuilder.Append(header.ToString());
			}
			for (int i = 0; i < rules.Count; i++)
			{
				CFRuleRecord cFRuleRecord = rules[i];
				if (cFRuleRecord != null)
				{
					stringBuilder.Append(cFRuleRecord.ToString());
				}
			}
			stringBuilder.Append("[/CF]\n");
			return stringBuilder.ToString();
		}
	}
}
