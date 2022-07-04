namespace ColegioMozart.Domain.Entities
{
    public class AuthenticatedUserResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; }

        public string Role { get; set; }
    }
}
