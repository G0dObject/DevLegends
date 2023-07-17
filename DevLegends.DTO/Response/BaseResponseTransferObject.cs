namespace DevLegends.DTO.Response
{
	public class BaseResponseTransferObject
	{
		public virtual int StatusCode { get; set; }

		public BaseResponseTransferObject(int statusCode)
		{
			StatusCode = statusCode;
		}
	}
}
