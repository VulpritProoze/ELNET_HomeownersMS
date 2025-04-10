using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Models;
using HomeownersMS.Data;

namespace HomeownersMS.Pages.Admin.Reservations
{
    [Authorize(Roles = "admin")]
    public class ManageReservationsModel : PageModel
    {
        private readonly HomeownersContext _context;

        public ManageReservationsModel(HomeownersContext context)
        {
            _context = context;
        }

        public List<ReservationViewModel> Reservations { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Get all facility requests with their facilities and residents
            var facilityRequests = await _context.FacilityRequests
                .Include(fr => fr.Facility)
                .Include(fr => fr.Resident)
                .OrderBy(fr => fr.RequestDate)
                .ToListAsync();

            // Get all events with additional services for these facility requests
            var facilityRequestIds = facilityRequests.Select(fr => fr.FacilityRequestId).ToList();
            var events = await _context.Events
                .Where(e => facilityRequestIds.Contains(e.FacilityRequestId ?? 0))
                .ToListAsync();

            // Combine them for the view
            Reservations = facilityRequests.Select(fr => new ReservationViewModel
            {
                FacilityRequest = fr,
                Event = events.FirstOrDefault(e => e.FacilityRequestId == fr.FacilityRequestId),
                Facility = fr.Facility,
                Resident = fr.Resident
            }).ToList();
        }

        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var request = await _context.FacilityRequests.FindAsync(id);
            if (request != null)
            {
                request.Status = RequestStatus.Approved;
                request.ApprovalDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeclineAsync(int id)
        {
            var request = await _context.FacilityRequests.FindAsync(id);
            if (request != null)
            {
                request.Status = RequestStatus.Declined;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }

    public class ReservationViewModel
    {
        public FacilityRequest? FacilityRequest { get; set; }
        public Event? Event { get; set; }
        public Facility? Facility { get; set; }
        public Resident? Resident { get; set; }
    }
}