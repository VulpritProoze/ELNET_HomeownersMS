using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Data;
using HomeownersMS.Models;
using Microsoft.AspNetCore.Authorization;


namespace HomeownersMS.Pages_Admin_Admins
{
    [Authorize(Roles = "admin")]
    public class DetailsModel : PageModel
    {
        private readonly HomeownersMS.Data.HomeownersContext _context;

        public DetailsModel(HomeownersMS.Data.HomeownersContext context)
        {
            _context = context;
        }

        public Admin Admin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.UserId == id);
            if (admin == null)
            {
                return NotFound();
            }
            else
            {
                Admin = admin;
            }
            return Page();
        }
    }
}
