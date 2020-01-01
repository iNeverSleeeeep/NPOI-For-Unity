using NPOI.SS.Util;

namespace NPOI.SS.UserModel
{
	public interface IDataValidation
	{
		IDataValidationConstraint ValidationConstraint
		{
			get;
		}

		/// <summary>
		/// get or set the error style for error box
		/// </summary>
		int ErrorStyle
		{
			get;
			set;
		}

		/// <summary>
		/// Setting this allows an empty object as a valid value. Retrieve the settings for empty cells allowed.
		/// @return True if this object should treats empty as valid value , false otherwise
		/// </summary>
		/// <value><c>true</c> if this object should treats empty as valid value, <c>false</c>  otherwise</value>
		bool EmptyCellAllowed
		{
			get;
			set;
		}

		/// <summary>
		/// Useful for list validation objects .
		/// Useful only list validation objects . This method always returns false if the object isn't a list validation object
		/// </summary>
		bool SuppressDropDownArrow
		{
			get;
			set;
		}

		/// Sets the behaviour when a cell which belongs to this object is selected
		///
		/// <value><c>true</c> if an prompt box should be displayed , <c>false</c> otherwise</value>
		bool ShowPromptBox
		{
			get;
			set;
		}

		/// Sets the behaviour when an invalid value is entered
		///
		/// <value><c>true</c> if an error box should be displayed , <c>false</c> otherwise</value>
		bool ShowErrorBox
		{
			get;
			set;
		}

		/// @return Prompt box's title or <code>null</code>
		string PromptBoxTitle
		{
			get;
		}

		/// @return Prompt box's text or <code>null</code>
		string PromptBoxText
		{
			get;
		}

		/// @return Error box's title or <code>null</code>
		string ErrorBoxTitle
		{
			get;
		}

		/// @return Error box's text or <code>null</code>
		string ErrorBoxText
		{
			get;
		}

		CellRangeAddressList Regions
		{
			get;
		}

		/// Sets the title and text for the prompt box . Prompt box is displayed when
		/// the user selects a cell which belongs to this validation object . In
		/// order for a prompt box to be displayed you should also use method
		/// SetShowPromptBox( bool show )
		///
		/// @param title The prompt box's title
		/// @param text The prompt box's text
		void CreatePromptBox(string title, string text);

		/// Sets the title and text for the error box . Error box is displayed when
		/// the user enters an invalid value int o a cell which belongs to this
		/// validation object . In order for an error box to be displayed you should
		/// also use method SetShowErrorBox( bool show )
		///
		/// @param title The error box's title
		/// @param text The error box's text
		void CreateErrorBox(string title, string text);
	}
}
