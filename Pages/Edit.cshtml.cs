using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OAuth.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public Recipe Recipe { get; set; }

        private readonly RecipeService _service;
        private readonly IAuthorizationService _authService;

        public EditModel(
            RecipeService service,
            IAuthorizationService authService)
        {
            _service = service;
            _authService = authService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Recipe = _service.GetRecipe(id);
            var authResult = await _authService
                .AuthorizeAsync(User, Recipe, "CanManageRecipe");

            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            return Page();
        }
    }

    public class Recipe
    {
        public string CreatedById { get; set; }
    }

    public class RecipeService
    {
        public Recipe GetRecipe(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class IsRecipeOwnerRequirement
    {
    }

    public class IsRecipeOwnerHandler :
        AuthorizationHandler<IAuthorizationRequirement, Recipe>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IsRecipeOwnerHandler(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IAuthorizationRequirement requirement,
            Recipe resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
            {
                return;
            }
            if (resource.CreatedById == appUser.Id)
            {
                context.Succeed(requirement);
            }
        }
    }
}
