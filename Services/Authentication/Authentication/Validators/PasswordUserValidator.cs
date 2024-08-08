using Authentication.Entities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using static IdentityModel.OidcConstants;

namespace Authentication.Validators
{
    public class PasswordUserValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public PasswordUserValidator(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(context.UserName);
                var claims = await _userManager.GetClaimsAsync(user);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(context.UserName, context.Password, isPersistent: true, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        // context set to success
                        context.Result = new GrantValidationResult(
                            subject: user.Id.ToString(),
                            authenticationMethod: AuthenticationMethods.Password,
                            claims: claims
                        );

                    }
                }
                else
                {
                    // context set to Failure        
                    context.Result = new GrantValidationResult(
                            TokenRequestErrors.UnauthorizedClient, "Invalid Crdentials");
                }
                return;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
