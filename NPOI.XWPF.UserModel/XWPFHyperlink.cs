namespace NPOI.XWPF.UserModel
{
	/// * Sketch of XWPF hyperlink class
	/// * 
	///                 * @author Yury Batrakov (batrakov at gmail.com)
	/// * 
	public class XWPFHyperlink
	{
		private string id;

		private string url;

		public string Id
		{
			get
			{
				return id;
			}
		}

		public string URL
		{
			get
			{
				return url;
			}
		}

		public XWPFHyperlink(string id, string url)
		{
			this.id = id;
			this.url = url;
		}
	}
}
