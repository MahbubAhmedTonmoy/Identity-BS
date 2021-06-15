using Core;
using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using UAM;
using UAM.DTO;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Helpers;
using WebAPI.Infrastructure;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IEmailSender _emailSender;
        private readonly AppDbContext appDbContext;
        public readonly ITwilioRestClient _client;
        private readonly EmailTemplate emailTemplate;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager, IConfiguration config, ILogger<AuthController> logger,
             IJwtGenerator jwtGenerator, IEmailSender emailSender, AppDbContext appDbContext,
             ITwilioRestClient client, EmailTemplate emailTemplate)
        {
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = config;
            _logger = logger;
            _emailSender = emailSender;
            this.appDbContext = appDbContext;
            _client = client;
            this.emailTemplate = emailTemplate;
        }

        [HttpPost("Sms Send")]
        public IActionResult SendSms(SmsMessage model)
        {
            var message = MessageResource.Create(
                to: new PhoneNumber(model.To),
                from: new PhoneNumber(model.From),
                body: model.Message,
                client: _client); // pass in the custom client
            return Ok("Success");
        }
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(UserRegistrationDTO userRegistrationDto)
        {
            //role create
            if (!await _roleManager.RoleExistsAsync(Role.SuperAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role.SuperAdmin));
            }
            if (!await _roleManager.RoleExistsAsync(Role.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role.User));
            }

            //create claim

            //create user
            var userForCreate = new AppUser
            {
                UserName = userRegistrationDto.UserName,
                Name = userRegistrationDto.UserName,
                Email = userRegistrationDto.Email,
                TwoFactorEnabled = userRegistrationDto.TwoFactorEnabled
            };
            if (await _userManager.FindByEmailAsync(userRegistrationDto.Email) != null)
            {
                return BadRequest("this email already used");
            }
            if (userRegistrationDto.Role.Contains("Admin") || userRegistrationDto.Role.Contains("User"))
            {
                var createdUser = await _userManager.CreateAsync(userForCreate, userRegistrationDto.Password);

                // role assign
                if (createdUser.Succeeded)
                {
                    if (userRegistrationDto.Role.Contains("Admin"))
                    {
                        await _userManager.AddToRoleAsync(userForCreate, Role.SuperAdmin);
                    }
                    if (userRegistrationDto.Role.Contains("User"))
                    {
                        await _userManager.AddToRoleAsync(userForCreate, Role.User);
                        //  await _userManager.AddClaimAsync(userForCreate, new Claim("Create Role", "Create Role"));
                    }
                    else
                    {
                        return BadRequest("role is not matched");
                    }

                    // verify email address
                    var user = await _userManager.FindByEmailAsync(userRegistrationDto.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = user.Email }, Request.Scheme);
                    var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
                    await _emailSender.SendEmailAsync(message);

                    return Ok(new { createdUser, token });
                }
                else
                {
                    return StatusCode(400, createdUser.Errors);
                }
            }

            return StatusCode(500, "Internal server error");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded) return Ok();
            else return BadRequest();
        }
        [HttpGet("ResendConfirmEmail")]
        public async Task<IActionResult> ResendConfirmEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Error");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "resend Confirmation email link", confirmationLink, null);
            await _emailSender.SendEmailAsync(message);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userForLoginDTO)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(userForLoginDTO.Email);
                if (userExist == null)
                {
                    return BadRequest($"this {userForLoginDTO.Email} is not resisrered");
                }
                var result = await _signInManager.CheckPasswordSignInAsync(userExist, userForLoginDTO.Password, false);

                if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(userExist);
                    //string[] roleAssigned = role.ToArray();
                    var token = _jwtGenerator.CreateToken(userExist, role.ToList());
                    userExist.RefreshToken = token.RefreshToken;
                    appDbContext.AppUsers.Update(userExist);
                    appDbContext.SaveChanges();
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace} {ex.Message}");
                throw;
            }
            return null;// StatusCode(500, "Internal server error");
        }
       
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var principal = _jwtGenerator.GetPrincipalFromExpiredToken(refreshTokenDTO.Token);
                // email pawa jay na for loop diye
                string email = null;
                var tempClaimPrincipal = principal.Claims.ToList();
                email = tempClaimPrincipal[2].Value;
                //-----
                var userExist = await _userManager.FindByEmailAsync(email);
                if (userExist == null || userExist.RefreshToken != refreshTokenDTO.RefreshToken)
                {
                    return BadRequest($"this {email} is not resisrered");
                }
                var role = await _userManager.GetRolesAsync(userExist);
                //string[] roleAssigned = role.ToArray();
                return Ok(_jwtGenerator.CreateToken(userExist, role.ToList()));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("not valid input");
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return BadRequest($"{user} not found");
            var emailContent = this.emailTemplate.FindEmailTemplate("Reset password");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Auth", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, emailContent.TemplateSubject, callback, null);
            await _emailSender.SendEmailAsync(message);

            return Ok(token);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("not valid input");
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null) return BadRequest($"{user} not found");
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("LoginTwoStep VerificationCode")]
        public async Task<IActionResult> LoginTwoStep(string email, bool rememberMe, string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest($"this {email} is not resisrered");
            }

            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
            {
                return BadRequest();
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var message = new Message(new string[] { email }, "Authentication token", token, null);
            await _emailSender.SendEmailAsync(message);

            return Ok(token);
        }


        //not tested not implemented
        [HttpPost("TwoStepLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginTwoStep(TwoStepVerificationModel twoStepModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _signInManager.TwoFactorSignInAsync("Email", twoStepModel.TwoFactorCode, twoStepModel.RememberMe, rememberClient: false);
            if (result.Succeeded)
            {
                return Ok(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                return BadRequest("The account is locked out");
            }
            else
            {
                return BadRequest("Invalid Login Attempt");
            }
        }
    }
}
