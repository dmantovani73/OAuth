using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth.Security;

namespace OAuth.Pages
{
    [Authorize(Policy = PolicyName.IsAdmin)]
    public class ProtectedModel : PageModel
    {
        public void OnGet()
        { }
    }
}
