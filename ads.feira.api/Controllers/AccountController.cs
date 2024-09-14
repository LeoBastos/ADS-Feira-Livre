using ads.feira.api.Helpers.EnumHelper;
using ads.feira.api.Helpers.Images;
using ads.feira.api.Models.Accounts;
using ads.feira.application.DTO.Accounts;
using ads.feira.application.Interfaces.Accounts;
using ads.feira.domain.Entity.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ads.feira.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly IAccountServices _accountService;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailSender _emailService;

        public AccountController(
            UserManager<Account> userManager,
            IAccountServices accountService,
            IOptions<JwtSettings> jwtSettings,
            IEmailSender emailService)
        {
            _userManager = userManager;
            _accountService = accountService;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
        }

        /// <summary>
        /// - Busca um usuário por Id.
        /// </summary>
        /// <param name="Id"></param>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountResponseDTO>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found.");
            }

            var rule = user.Email;

            // Only allow users to view their own profile, unless they're an admin
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != rule && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(new AccountResponseDTO
            {
                Email = user.Email,
                Name = user.Name,
                Assets = user.Assets,
                UserType = EnumExtensions.GetDisplayName(user.UserType),
            });
        }

        /// <summary>
        /// - Efetua Login na Plataforma
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid email or password");
            }

            if (!user.EmailConfirmed)
            {
                return Unauthorized("Email not confirmed. Please check your email for the confirmation link.");
            }

            var token = await GenerateJwtToken(user);
            return Ok(new UserToken { Token = token, Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes) });
        }

        /// <summary>
        ///  - Registra um Cliente
        /// </summary>
        [HttpPost("register/customer")]
        [ProducesResponseType(typeof(RegisterViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterCustomer([FromForm] RegisterViewModel model)
        {
            return await RegisterUser(model, UserType.Customer);
        }

        /// <summary>
        ///  - Registra um Lojista
        /// </summary>
        [HttpPost("register/storeowner")]
        [ProducesResponseType(typeof(RegisterViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterStoreOwner([FromForm] RegisterViewModel model)
        {
            return await RegisterUser(model, UserType.StoreOwner);
        }

        private async Task<IActionResult> RegisterUser(RegisterViewModel model, UserType userType)
        {
            if (!ModelState.IsValid || model.TosAccept == false || model.PrivacyAccept == false)
            {
                return BadRequest(ModelState);
            }

            var user = new Account
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Assets = await FilesExtensions.UploadImage(model.Assets) ?? "~/images/noimage.png",
                UserType = userType,
                TosAccept = model.TosAccept,
                PrivacyAccept = model.PrivacyAccept,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userType.ToString());

                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                // Send confirmation email
                await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: {confirmationLink}");

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new { Message = $"User {user.Email} created successfully. Please check your email to confirm your account." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// - Confirmação por Email
        /// </summary>
        [HttpGet("confirm-email")]
        [ProducesResponseType(typeof(ForgotPasswordViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Invalid email confirmation token");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest("Error confirming your email.");
            }

            return Ok("Thank you for confirming your email.");
        }
        
        /// <summary>
        /// - Esqueceu a Senha
        /// </summary>
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ForgotPasswordViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok("If your email is registered and confirmed, you will receive a password reset link shortly.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);

            await _emailService.SendEmailAsync(model.Email, "Reset Password",
                $"Please reset your password by clicking here: {callbackUrl} \n Token: {token}");

            return Ok("If your email is registered and confirmed, you will receive a password reset link shortly.");
        }

        /// <summary>
        ///  - Redefinir Senha
        /// </summary>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ResetPasswordViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok("Password reset successful.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password reset successful.");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        ///  - Atualizar Usuário
        /// </summary>
        /// <param name="Id"></param>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AccountUpdateDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] AccountUpdateDTO model)
        {
            if (id == null)
            {
                return BadRequest("Invalid ID");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found.");
            }

            // Only allow users to update their own profile, unless they're an admin
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            user.Name = model.Name;
            user.Email = model.Email;            
            if (model.Assets != null)
            {
                user.Assets = await FilesExtensions.UploadImage(model.Assets);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Usuário atualizado com sucesso");
        }

        /// <summary>
        ///  - Excluir um Usuário
        /// </summary>
        /// <param name="Id"></param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        private async Task<string> GenerateJwtToken(Account user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)               
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}

