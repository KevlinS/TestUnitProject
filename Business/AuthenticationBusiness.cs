namespace GestionBibliothequeAPI.Business
{
    public class AuthenticationBusiness
    {
        private readonly JwtConfig _jwtConfig;

        public AuthenticationBusiness(IOptionsMonitor<JwtConfig> option)
        {
            _jwtConfig = option.CurrentValue;
        }
        public static object GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("super_secret123456789123456789");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
