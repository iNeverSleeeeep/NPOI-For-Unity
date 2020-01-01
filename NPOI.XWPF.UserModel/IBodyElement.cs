namespace NPOI.XWPF.UserModel
{
	/// 9 Jan 2010
	/// @author Philipp Epp
	public interface IBodyElement
	{
		IBody Body
		{
			get;
		}

		BodyType PartType
		{
			get;
		}

		BodyElementType ElementType
		{
			get;
		}

		POIXMLDocumentPart GetPart();
	}
}
