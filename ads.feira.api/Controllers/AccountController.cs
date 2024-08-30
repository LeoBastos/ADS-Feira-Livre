using ads.feira.api.Helpers;
using ads.feira.api.Models.Accounts;
using ads.feira.api.Models.Products;
using ads.feira.application.DTO.Accounts;
using ads.feira.application.DTO.Categories;
using ads.feira.application.Interfaces.Accounts;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ads.feira.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly RegionEndpoint _region;
        private readonly string _awsClientId;
        private readonly string _userPoolId;
        private readonly ICognitoUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private const string CUSTOMER_GROUP_NAME = "Customers";
        private const string USERS_GROUP_NAME = "Users";

        public AccountController(IConfiguration config, ICognitoUserService userService, ILogger<AccountController> logger, IMapper mapper)
        {
            _config = config;
            _region = RegionEndpoint.GetBySystemName(config["AWS:Region"]);
            _awsClientId = config["AWS:ClientId"];
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _userPoolId = config["AWS:PoolId"];
        }




        /// <summary>
        /// Insere um novo cliente.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var provider = new AmazonCognitoIdentityProviderClient(_region);
            var request = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _awsClientId,
                AuthParameters = new Dictionary<string, string>
                         {
                             {"USERNAME", model.Username},
                             {"PASSWORD", model.Password}
                         }
            };

            try
            {
                var response = await provider.InitiateAuthAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Login failed: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            try
            {
                var provider = new AmazonCognitoIdentityProviderClient(_region);
                var signUpRequest = new SignUpRequest
                {
                    ClientId = _awsClientId,
                    Username = model.Email,
                    Password = model.Password,                  

                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "name", Value = model.Name },
                        new AttributeType { Name = "email", Value = model.Email },                        
                    }
                };

                var cognitoResponse = await provider.SignUpAsync(signUpRequest);

                var cognitoUserId = cognitoResponse.UserSub;

                await AddUserToGroup(provider, cognitoUserId, CUSTOMER_GROUP_NAME);

                // Create local user
                var localUser = new CreateCognitoUserDTO(
                    Guid.Parse(cognitoUserId),
                    model.Email,
                    model.Name,
                    model.Description ?? "",
                    await FilesExtensions.UploadImage(model.Assets) ?? "",
                    model.TosAccept,
                    model.PrivacyAccept,
                    model.Roles = "Customers"                    
                );
                await _userService.Create(localUser);

                return Ok(new { Message = "User registered successfully", UserId = cognitoUserId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed");
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateCognitorUserViewModel model)
        {
            try
            {               
                var provider = new AmazonCognitoIdentityProviderClient(_region);
                var updateRequest = new AdminUpdateUserAttributesRequest
                {
                    UserPoolId = _config["AWS:UserPoolId"],
                    Username = model.Email,
                    UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "name", Value = model.Name },
            }
                };

                await provider.AdminUpdateUserAttributesAsync(updateRequest);

                // Update local user
                var localUser = await _userService.GetById(model.Id);
                if (localUser == null)
                {
                    return NotFound("User not found");
                }
                localUser.Id = model.Id;
                localUser.Email = model.Email;
                localUser.Name = model.Name;
                localUser.Description = model.Description;
                localUser.Assets = model.Assets;
                localUser.TosAccept = model.TosAccept ?? false;
                localUser.PrivacyAccept = model.PrivacyAccept ?? false;
                localUser.Roles = model.Roles;


                //localUser.Update(model.Id, model.Email, model.Name, model.Description, model.Assets, model.TosAccept, model.PrivacyAccept, model.Roles);
                await _userService.Update(localUser);

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User update failed");
                return BadRequest($"Update failed: {ex.Message}");
            }
        }

       
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmRegistration([FromBody] ConfirmViewModel model)
        {
            try
            {
                var provider = new AmazonCognitoIdentityProviderClient(_region);
                var confirmRequest = new ConfirmSignUpRequest
                {
                    ClientId = _awsClientId,
                    Username = model.Email,
                    ConfirmationCode = model.ConfirmationCode
                };

                await provider.ConfirmSignUpAsync(confirmRequest);

                return Ok(new { Message = "User confirmed successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Confirmation failed");
                return BadRequest($"Confirmation failed: {ex.Message}");
            }
        }

        
        [HttpPost("resend-code")]
        public async Task<IActionResult> ResendConfirmationCode([FromBody] ResendConfirmationCodeViewModel model)
        {
            try
            {
                var provider = new AmazonCognitoIdentityProviderClient(_region);
                var resendRequest = new ResendConfirmationCodeRequest
                {
                    ClientId = _awsClientId,
                    Username = model.Email
                };

                await provider.ResendConfirmationCodeAsync(resendRequest);

                return Ok(new { Message = "Confirmation code resent successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Resend confirmation code failed");
                return BadRequest($"Resend confirmation code failed: {ex.Message}");
            }
        }

       
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            try
            {
                var provider = new AmazonCognitoIdentityProviderClient(_region);
                var forgotPasswordRequest = new ForgotPasswordRequest
                {
                    ClientId = _awsClientId,
                    Username = model.Email
                };

                await provider.ForgotPasswordAsync(forgotPasswordRequest);

                return Ok(new { Message = "Password reset code sent successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Forgot password request failed");
                return BadRequest($"Forgot password request failed: {ex.Message}");
            }
        }

        
        [HttpPost("confirm-forgot-password")]
        public async Task<IActionResult> ConfirmForgotPassword([FromBody] ConfirmForgotPasswordViewModel model)
        {
            try
            {
                var provider = new AmazonCognitoIdentityProviderClient(_region);
                var confirmForgotPasswordRequest = new ConfirmForgotPasswordRequest
                {
                    ClientId = _awsClientId,
                    Username = model.Email,
                    Password = model.NewPassword,
                    ConfirmationCode = model.ConfirmationCode
                };

                await provider.ConfirmForgotPasswordAsync(confirmForgotPasswordRequest);

                return Ok(new { Message = "Password reset successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Confirm forgot password failed");
                return BadRequest($"Confirm forgot password failed: {ex.Message}");
            }
        }
        

        [HttpPost("register-owner")]
        public async Task<IActionResult> RegisterInternalUser([FromForm] RegisterInternalUserViewModel model)
        {
            try
            {
                var provider = new AmazonCognitoIdentityProviderClient(_region);
                var signUpRequest = new SignUpRequest
                {
                    ClientId = _awsClientId,
                    Username = model.Email,
                    Password = model.Password,
                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "name", Value = model.Name },
                        new AttributeType { Name = "email", Value = model.Email },                        
                    }
                };

                var cognitoResponse = await provider.SignUpAsync(signUpRequest);
                var cognitoUserId = cognitoResponse.UserSub;

                await AddUserToGroup(provider, cognitoUserId, USERS_GROUP_NAME);

                var localUser = new CreateCognitoUserDTO(
                    Guid.Parse(cognitoUserId),
                    model.Email,
                    model.Name,
                    model.Description ?? "",
                    await FilesExtensions.UploadImage(model.Assets) ?? "",
                    true,
                    true,
                    "Users"
                );

                await _userService.Create(localUser);

                // Automatically confirm the user
                //var confirmRequest = new ConfirmSignUpRequest
                //{
                //    ClientId = _awsClientId,
                //    Username = model.Email,
                //    ConfirmationCode = "Users"
                //};

                //await provider.ConfirmSignUpAsync(confirmRequest);

                return Ok(new { Message = "Internal user registered and confirmed successfully", UserId = cognitoUserId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal user registration failed");
                return BadRequest($"Internal user registration failed: {ex.Message}");
            }
        }


        private async Task AddUserToGroup(AmazonCognitoIdentityProviderClient provider, string username, string groupName)
        {
            var request = new AdminAddUserToGroupRequest
            {
                UserPoolId = _userPoolId,
                Username = username,
                GroupName = groupName
            };

            await provider.AdminAddUserToGroupAsync(request);
        }
    }
}

