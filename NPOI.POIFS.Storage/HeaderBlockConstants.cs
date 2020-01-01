namespace NPOI.POIFS.Storage
{
	/// <summary>
	/// Constants used in reading/writing the Header block
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class HeaderBlockConstants
	{
		public const long _signature = -2226271756974174256L;

		public const int _bat_array_offset = 76;

		public const int _max_bats_in_header = 109;

		public const int _signature_offset = 0;

		public const int _bat_count_offset = 44;

		public const int _property_start_offset = 48;

		public const int _sbat_start_offset = 60;

		public const int _sbat_block_count_offset = 64;

		public const int _xbat_start_offset = 68;

		public const int _xbat_count_offset = 72;
	}
}
