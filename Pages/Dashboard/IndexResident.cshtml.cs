using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;


namespace HomeownersMS.Pages.Dashboard
{
    [Authorize(Roles = "admin,resident")]
    public class IndexResidentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
