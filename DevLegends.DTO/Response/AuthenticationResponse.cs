namespace DevLegends.DTO.Response
{
	public class AuthenticationResponse : BaseResponseTransferObject
	{
		public string? Token { get; set; }

		public AuthenticationResponse(string? token, int statuscode) : base(statuscode)
		{
			Token = token;
		}

		public AuthenticationResponse(int statuscode) : base(statuscode)
		{
		}
	}
}
