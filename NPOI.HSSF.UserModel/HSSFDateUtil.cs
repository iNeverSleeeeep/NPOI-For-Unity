using NPOI.SS.UserModel;
using System;

namespace NPOI.HSSF.UserModel
{
	/// Contains methods for dealing with Excel dates.
	///
	/// @author  Michael Harhen
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @author  Dan Sherman (dsherman at isisph.com)
	/// @author  Hack Kampbjorn (hak at 2mba.dk)
	/// @author  Alex Jacoby (ajacoby at gmail.com)
	/// @author  Pavel Krupets (pkrupets at palmtreebusiness dot com)
	public class HSSFDateUtil : DateUtil
	{
		protected new static int AbsoluteDay(DateTime cal, bool use1904windowing)
		{
			return DateUtil.AbsoluteDay(cal, use1904windowing);
		}
	}
}
