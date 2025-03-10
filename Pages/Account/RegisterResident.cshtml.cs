using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HomeownersMS.Data;
using HomeownersMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeownersMS.Pages.Account
{
    public class RegisterResidentModel : PageModel
    {
        private readonly HomeownersContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public RegisterResidentModel(HomeownersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserRegistrationModel UserInput { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Fix: Use _context.Users instead of _context.User
            if (await _context.Users.AnyAsync(u => u.Username == UserInput.Username))
            {
                ModelState.AddModelError("UserInput.Username", "Username is already taken.");
                return Page();
            }

            var user = new User
            {
                Username = UserInput.Username,
                PasswordHash = _passwordHasher.HashPassword(new User(), UserInput.Password), // Fix: Pass a User instance
                Privilege = Privileges.resident // Fix: Ensure the correct role is assigned
            };

            var resident = new Resident
            {
                LName = "", 
                FName = "",
                Email = "",
                ContactNo = "",
                Address = "",
                MoveInDate = System.DateTime.Now,
                User = user
            };

            _context.Users.Add(user);
            _context.Residents.Add(resident);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
