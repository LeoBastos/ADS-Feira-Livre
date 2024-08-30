//using ads.feira.api.Models.Accounts;
//using ads.feira.application.Interfaces.Accounts;
//using Amazon;
//using Amazon.CognitoIdentityProvider;
//using Amazon.CognitoIdentityProvider.Model;
//using Microsoft.AspNetCore.Mvc;

//namespace ads.feira.api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly IConfiguration config;
//        private RegionEndpoint region;
//        private string? awsClientId;
//        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
//        private readonly ILogger _logger;
//        private readonly ICognitoUserService _userService;



//        public AuthController(IConfiguration config, ICognitoUserService userService)
//        {
//            this.config = config;
//            region = RegionEndpoint.GetBySystemName(config["AWS:Region"]);
//            awsClientId = config["AWS:ClientId"];
//            _userService = userService;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
//        {
//            var provider = new AmazonCognitoIdentityProviderClient(region);
//            var request = new SignUpRequest
//            {
//                ClientId = awsClientId,
//                Username = model.Email,
//                Password = model.Password,

//                Other atributes
//                UserAttributes ={
//                     new AttributeType{
//                         Name="name",
//                     }
//                }
//            };


//            Tenho que buscar o usuário registrado
//            E inserir no banco local

//            var localUser = new CognitoUserDTO
//            {
//                //CognitoId = cognitoResponse.UserId,
//                Email = model.Email,
//                Name = model.Name,
//            };

//            await _userService.Create(localUser);

//            try
//            {
//                var response = await provider.SignUpAsync(request);

//                return Ok(response);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Registration failed: {ex.Message}");
//            }
//        }

//        [HttpPost("confirm")]
//        public async Task<IActionResult> Confirm([FromBody] ConfirmViewModel model)
//        {
//            var provider = new AmazonCognitoIdentityProviderClient(region);
//            var confirmRequest = new ConfirmSignUpRequest
//            {
//                ClientId = awsClientId,
//                Username = model.Username,
//                ConfirmationCode = model.ConfirmationCode
//            };

//            try
//            {
//                var confirmResponse = await provider.ConfirmSignUpAsync(confirmRequest);
//                return Ok(confirmResponse);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Confirmation failed: {ex.Message}");

//            }
//        }


//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
//        {
//            var provider = new AmazonCognitoIdentityProviderClient(region);
//            var request = new InitiateAuthRequest
//            {
//                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
//                ClientId = awsClientId,
//                AuthParameters = new Dictionary<string, string>
//                 {
//                     {"USERNAME", model.Username},
//                     {"PASSWORD", model.Password}
//                 }
//            };

//            try
//            {
//                var response = await provider.InitiateAuthAsync(request);

//                return Ok(response);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Login failed: {ex.Message}");
//            }
//        }

//    }
//}
