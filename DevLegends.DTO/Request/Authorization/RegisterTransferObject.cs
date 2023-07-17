namespace DevLegends.DTO.Request.Authorization
{
    public class RegisterTransferObject
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
