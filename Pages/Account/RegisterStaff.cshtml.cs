using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HomeownersMS.Data;
using HomeownersMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeownersMS.Pages.Account
{
    public class RegisterStaffModel : PageModel
    {
        private readonly HomeownersContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public RegisterStaffModel(HomeownersContext context)
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
                Privilege = Privileges.staff // Fix: Ensure the correct role is assigned
            };

            var staff = new Staff
            {
                LName = "", 
                FName = "",
                Email = "",
                ContactNo = "",
                Job = "",
                HireDate = System.DateTime.Now,
                User = user
            };

            _context.Users.Add(user);
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
