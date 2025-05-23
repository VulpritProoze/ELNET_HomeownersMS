﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Data;
using HomeownersMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace HomeownersMS.Pages.Admin.Users
{
    [Authorize(Roles = "admin")]
    public class DetailsModel : PageModel
    {
        private readonly HomeownersMS.Data.HomeownersContext _context;

        public DetailsModel(HomeownersMS.Data.HomeownersContext context)
        {
            _context = context;
        }

        public User UserList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userIdentity = HttpContext.User.Identity;

            if (userIdentity == null || !userIdentity.IsAuthenticated || !HttpContext.User.IsInRole("admin"))
            {
                return RedirectToPage("/Account/AccessDenied");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Resident)
                .Include(u => u.Staff)
                .Include(u => u.Admin)
                .FirstOrDefaultAsync(m => m.UserId == id);
                
            if (user == null)
            {
                return NotFound();
            }
            
            UserList = user;
            return Page();
        }
    }
}
