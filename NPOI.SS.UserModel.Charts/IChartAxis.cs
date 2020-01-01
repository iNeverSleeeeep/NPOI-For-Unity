namespace NPOI.SS.UserModel.Charts
{
	/// High level representation of chart axis.
	///
	/// @author Roman Kashitsyn
	public interface IChartAxis
	{
		/// @return axis id
		long GetId();

		/// @return axis position
		AxisPosition GetPosition();

		/// @param position new axis position
		void SetPosition(AxisPosition position);

		/// @return axis number format
		string GetNumberFormat();

		/// @param format axis number format
		void SetNumberFormat(string format);

		/// @return true if log base is defined, false otherwise
		bool IsSetLogBase();

		/// @param logBase a number between 2 and 1000 (inclusive)
		/// @throws ArgumentException if log base not within allowed range
		void SetLogBase(double logBase);

		/// @return axis log base or 0.0 if not Set
		double GetLogBase();

		/// @return true if minimum value is defined, false otherwise
		bool IsSetMinimum();

		/// @return axis minimum or 0.0 if not Set
		double GetMinimum();

		/// @param min axis minimum
		void SetMinimum(double min);

		/// @return true if maximum value is defined, false otherwise
		bool IsSetMaximum();

		/// @return axis maximum or 0.0 if not Set
		double GetMaximum();

		/// @param max axis maximum
		void SetMaximum(double max);

		/// @return axis orientation
		AxisOrientation GetOrientation();

		/// @param orientation axis orientation
		void SetOrientation(AxisOrientation orientation);

		/// @param crosses axis cross type
		void SetCrosses(AxisCrosses crosses);

		/// @return axis cross type
		AxisCrosses GetCrosses();

		/// Declare this axis cross another axis.
		/// @param axis that this axis should cross
		void CrossAxis(IChartAxis axis);
	}
}
