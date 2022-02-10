namespace GestionBibliothequeAPI.Controllers.v1
{
    public class AccountsController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtConfig _jwtConfig;

        public AccountsController(
            IUnitOfWork unitOfWork, 
            UserManager<IdentityUser> userManager, 
            IOptionsMonitor<JwtConfig> option,
            SignInManager<IdentityUser> signInManager) : base(unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfig = option.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserRegistrationResponse>> Register([FromBody] UserRegistrationRequest registration)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(registration.Email);

                if(userExist != null)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Email already in use"
                        }
                    });
                }

                var newUser = new IdentityUser()
                {
                    Email = registration.Email,
                    UserName = registration.Email,
                    EmailConfirmed = true
                };

                var isCreated = await _userManager.CreateAsync(newUser, registration.Password);
                if (!isCreated.Succeeded)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Success = false,
                        Errors = isCreated.Errors.Select(x => x.Description).ToList()
                    });
                }

                var _user = new User();
                _user.IdentityId = new Guid(newUser.Id);
                _user.Email = registration.Email;
                _user.FirstName = registration.FirstName;
                _user.LastName = registration.LastName;
                _user.CreatedDate = DateTime.Now;
                _user.Phone = "";
                _user.Address = "";
                _user.City = "";
                _user.PostalCode = 0;
                _user.status = 1;

                await _unitOfWork.Users.Add(_user);
                await _unitOfWork.CompleteAsync();

                var token = GenerateToken(newUser);

                return Ok(new UserRegistrationResponse()
                {
                    Success = true,
                    Token = token
                });                             
            }
            else
            {
                return BadRequest(new UserRegistrationResponse
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Invalid payload"
                    }
                });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest login)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(login.Email);

                if(userExist == null)
                {
                    return BadRequest(new UserLoginResponse()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Invalid authentication request"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(userExist, login.Password);

                if (isCorrect)
                {
                    var jwtToken = GenerateToken(userExist);

                    return Ok(new UserLoginResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }else
                {
                    return BadRequest(new UserLoginResponse()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Invalid authentication request"
                        }
                    });
                }
            }else
            {
                return BadRequest(new UserRegistrationResponse
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Invalid payload"
                    }
                });
            }
        }

        private string GenerateToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
