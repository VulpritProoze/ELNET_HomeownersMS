using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Models;
using HomeownersMS.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace HomeownersMS.Pages.Reservation
{
    [Authorize(Roles = "admin,resident")]
    public class ReserveModel : PageModel
    {
        private readonly HomeownersContext _context;

        public ReserveModel(HomeownersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FacilityRequest FacilityRequest { get; set; } = new FacilityRequest();

        [BindProperty]
        public Event Event { get; set; } = new Event();

        [BindProperty]
        public List<string> SelectedServices { get; set; } = new List<string>();

        public Facility Facility { get; set; } = new Facility();

        public async Task<IActionResult> OnGetAsync(int facilityId)
        {
            Facility = await _context.Facilities.FindAsync(facilityId) ?? throw new Exception("Facility not found");

            // Get user ID from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                var resident = await _context.Residents.FirstOrDefaultAsync(r => r.UserId == userId);
                if (resident != null)
                {
                    FacilityRequest = new FacilityRequest
                    {
                        FacilityId = facilityId,
                        ResidentId = resident.UserId,
                        FullName = $"{resident.FName} {resident.LName}",
                        EmailAddress = resident.Email,
                        PhoneNumber = resident.ContactNo
                    };
                    return Page();
                }
            }

            // Default case
            FacilityRequest = new FacilityRequest
            {
                FacilityId = facilityId
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the user is an admin or staff
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                // Check if the user is an admin
                var isAdmin = await _context.Admins.AnyAsync(a => a.UserId == userId);
                if (isAdmin)
                {
                    ModelState.AddModelError(string.Empty, "Admins are not allowed to make reservations.");
                    return Page();
                }

                // Check if the user is a staff member
                var isStaff = await _context.Staffs.AnyAsync(s => s.UserId == userId);
                if (isStaff)
                {
                    ModelState.AddModelError(string.Empty, "Staff members are not allowed to make reservations.");
                    return Page();
                }
            }

            if (!ModelState.IsValid)
            {
                Facility = await _context.Facilities.FindAsync(FacilityRequest.FacilityId)
                           ?? throw new Exception("Facility not found");
                return Page();
            }

            // Log all fields in FacilityRequest
            Console.WriteLine("FacilityRequest:");
            Console.WriteLine($"  FacilityId: {FacilityRequest.FacilityId}");
            Console.WriteLine($"  ResidentId: {FacilityRequest.ResidentId}");
            Console.WriteLine($"  FullName: {FacilityRequest.FullName}");
            Console.WriteLine($"  EmailAddress: {FacilityRequest.EmailAddress}");
            Console.WriteLine($"  PhoneNumber: {FacilityRequest.PhoneNumber}");
            Console.WriteLine($"  RequestDate: {FacilityRequest.RequestDate}");
            Console.WriteLine($"  Status: {FacilityRequest.Status}");

            // Add selected services to the Event
            foreach (var service in SelectedServices)
            {
                switch (service)
                {
                    case "AirConditioning":
                        Event.AdditionalServices.Add(service, PredefinedServices.AirConditioning);
                        break;
                    case "TableAndChairs":
                        Event.AdditionalServices.Add(service, PredefinedServices.TableAndChairs);
                        break;
                    case "SoundSystem":
                        Event.AdditionalServices.Add(service, PredefinedServices.SoundSystem);
                        break;
                    case "ProjectorAndScreen":
                        Event.AdditionalServices.Add(service, PredefinedServices.ProjectorAndScreen);
                        break;
                    case "Decorations":
                        Event.AdditionalServices.Add(service, PredefinedServices.Decorations);
                        break;
                }
            }

            // Save FacilityRequest first to get its ID
            _context.FacilityRequests.Add(FacilityRequest);
            await _context.SaveChangesAsync();

            // Now create and save Event with the FacilityRequestId
            Event.FacilityRequestId = FacilityRequest.FacilityRequestId;
            Event.CreatedBy = FacilityRequest.ResidentId;

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Reservation/Reservation");
        }
    }
}