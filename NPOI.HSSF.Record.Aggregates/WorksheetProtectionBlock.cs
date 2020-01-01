using NPOI.HSSF.Model;
using NPOI.Util;

namespace NPOI.HSSF.Record.Aggregates
{
	/// Groups the sheet protection records for a worksheet.
	/// <p />
	///
	/// See OOO excelfileformat.pdf sec 4.18.2 'Sheet Protection in a Workbook
	/// (BIFF5-BIFF8)'
	///
	/// @author Josh Micich
	public class WorksheetProtectionBlock : RecordAggregate
	{
		private ProtectRecord _protectRecord;

		private ObjectProtectRecord _objectProtectRecord;

		private ScenarioProtectRecord _scenarioProtectRecord;

		private PasswordRecord _passwordRecord;

		/// <summary>
		/// the ProtectRecord. If one is not contained in the sheet, then one is created.
		/// </summary>
		private ProtectRecord Protect
		{
			get
			{
				if (_protectRecord == null)
				{
					_protectRecord = new ProtectRecord(isProtected: false);
				}
				return _protectRecord;
			}
		}

		/// <summary>
		/// the PasswordRecord. If one is not Contained in the sheet, then one is Created.
		/// </summary>
		public PasswordRecord Password
		{
			get
			{
				if (_passwordRecord == null)
				{
					_passwordRecord = CreatePassword();
				}
				return _passwordRecord;
			}
		}

		public bool IsSheetProtected
		{
			get
			{
				if (_protectRecord != null)
				{
					return _protectRecord.Protect;
				}
				return false;
			}
		}

		public bool IsObjectProtected
		{
			get
			{
				if (_objectProtectRecord != null)
				{
					return _objectProtectRecord.Protect;
				}
				return false;
			}
		}

		public bool IsScenarioProtected
		{
			get
			{
				if (_scenarioProtectRecord != null)
				{
					return _scenarioProtectRecord.Protect;
				}
				return false;
			}
		}

		public int PasswordHash
		{
			get
			{
				if (_passwordRecord == null)
				{
					return 0;
				}
				return _passwordRecord.Password;
			}
		}

		/// @return <c>true</c> if the specified Record sid is one belonging to
		///         the 'Page Settings Block'.
		public static bool IsComponentRecord(int sid)
		{
			switch (sid)
			{
			case 18:
			case 19:
			case 99:
			case 221:
				return true;
			default:
				return false;
			}
		}

		private bool ReadARecord(RecordStream rs)
		{
			switch (rs.PeekNextSid())
			{
			case 18:
				CheckNotPresent(_protectRecord);
				_protectRecord = (rs.GetNext() as ProtectRecord);
				break;
			case 99:
				CheckNotPresent(_objectProtectRecord);
				_objectProtectRecord = (rs.GetNext() as ObjectProtectRecord);
				break;
			case 221:
				CheckNotPresent(_scenarioProtectRecord);
				_scenarioProtectRecord = (rs.GetNext() as ScenarioProtectRecord);
				break;
			case 19:
				CheckNotPresent(_passwordRecord);
				_passwordRecord = (rs.GetNext() as PasswordRecord);
				break;
			default:
				return false;
			}
			return true;
		}

		private void CheckNotPresent(Record rec)
		{
			if (rec != null)
			{
				throw new RecordFormatException("Duplicate WorksheetProtectionBlock record (sid=0x" + StringUtil.ToHexString(rec.Sid) + ")");
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			VisitIfPresent(_protectRecord, rv);
			VisitIfPresent(_objectProtectRecord, rv);
			VisitIfPresent(_scenarioProtectRecord, rv);
			VisitIfPresent(_passwordRecord, rv);
		}

		private static void VisitIfPresent(Record r, RecordVisitor rv)
		{
			if (r != null)
			{
				rv.VisitRecord(r);
			}
		}

		public PasswordRecord GetPasswordRecord()
		{
			return _passwordRecord;
		}

		public ScenarioProtectRecord GetHCenter()
		{
			return _scenarioProtectRecord;
		}

		/// This method Reads {@link WorksheetProtectionBlock} records from the supplied RecordStream
		/// until the first non-WorksheetProtectionBlock record is encountered. As each record is Read,
		/// it is incorporated into this WorksheetProtectionBlock.
		/// <p />
		/// As per the OOO documentation, the protection block records can be expected to be written
		/// toGether (with no intervening records), but earlier versions of POI (prior to Jun 2009)
		/// didn't do this.  Workbooks with sheet protection Created by those earlier POI versions
		/// seemed to be valid (Excel opens them OK). So PO allows continues to support Reading of files
		/// with non continuous worksheet protection blocks.
		///
		/// <p />
		/// <b>Note</b> - when POI Writes out this WorksheetProtectionBlock, the records will always be
		/// written in one consolidated block (in the standard ordering) regardless of how scattered the
		/// records were when they were originally Read.
		public void AddRecords(RecordStream rs)
		{
			while (ReadARecord(rs))
			{
			}
		}

		/// <summary>
		/// protect a spreadsheet with a password (not encrypted, just sets protect flags and the password.)
		/// </summary>
		/// <param name="password">password to set;Pass <code>null</code> to remove all protection</param>
		/// <param name="shouldProtectObjects">shouldProtectObjects are protected</param>
		/// <param name="shouldProtectScenarios">shouldProtectScenarios are protected</param>
		public void ProtectSheet(string password, bool shouldProtectObjects, bool shouldProtectScenarios)
		{
			if (password == null)
			{
				_passwordRecord = null;
				_protectRecord = null;
				_objectProtectRecord = null;
				_scenarioProtectRecord = null;
			}
			else
			{
				ProtectRecord protect = Protect;
				PasswordRecord password2 = Password;
				protect.Protect = true;
				password2.Password = PasswordRecord.HashPassword(password);
				if (_objectProtectRecord == null && shouldProtectObjects)
				{
					ObjectProtectRecord objectProtectRecord = CreateObjectProtect();
					objectProtectRecord.Protect = true;
					_objectProtectRecord = objectProtectRecord;
				}
				if (_scenarioProtectRecord == null && shouldProtectScenarios)
				{
					ScenarioProtectRecord scenarioProtectRecord = CreateScenarioProtect();
					scenarioProtectRecord.Protect = true;
					_scenarioProtectRecord = scenarioProtectRecord;
				}
			}
		}

		/// <summary>
		/// Creates an ObjectProtect record with protect set to false.
		/// </summary>
		/// <returns></returns>
		private static ObjectProtectRecord CreateObjectProtect()
		{
			ObjectProtectRecord objectProtectRecord = new ObjectProtectRecord();
			objectProtectRecord.Protect = false;
			return objectProtectRecord;
		}

		/// <summary>
		/// Creates a ScenarioProtect record with protect set to false.
		/// </summary>
		/// <returns></returns>
		private static ScenarioProtectRecord CreateScenarioProtect()
		{
			ScenarioProtectRecord scenarioProtectRecord = new ScenarioProtectRecord();
			scenarioProtectRecord.Protect = false;
			return scenarioProtectRecord;
		}

		/// <summary>
		///             Creates a Password record with password set to 0x0000.
		/// </summary>
		/// <returns></returns>
		private static PasswordRecord CreatePassword()
		{
			return new PasswordRecord(0);
		}
	}
}
