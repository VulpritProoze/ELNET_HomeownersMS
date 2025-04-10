using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Models;
using HomeownersMS.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HomeownersMS.Pages.Reservation
{
    [Authorize(Roles = "admin,resident")]
    public class ReservationModel : PageModel
    {
        private readonly HomeownersContext _context;

        public ReservationModel(HomeownersContext context)
        {
            _context = context;
        }

        public List<Facility> Facilities { get; set; } = new List<Facility>();
        public List<ReservationLogViewModel> PendingReservations { get; set; } = new();
        public List<ReservationHistoryViewModel> HistoricalReservations { get; set; } = new();

        public class ReservationLogViewModel
        {
            public FacilityRequest? FacilityRequest { get; set; }
            public Event? Event { get; set; }
        }

        public class ReservationHistoryViewModel
        {
            public FacilityRequest? FacilityRequest { get; set; }
            public Event? Event { get; set; }
        }

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                // Get pending reservations (for Log)
                var pendingRequests = await _context.FacilityRequests
                    .Include(fr => fr.Facility)
                    .Where(fr => fr.ResidentId == userId && fr.Status == RequestStatus.Pending)
                    .OrderByDescending(fr => fr.RequestDate)
                    .ToListAsync();

                var pendingRequestIds = pendingRequests.Select(fr => fr.FacilityRequestId).ToList();
                var pendingEvents = await _context.Events
                    .Where(e => pendingRequestIds.Contains(e.FacilityRequestId ?? 0))
                    .ToListAsync();

                PendingReservations = pendingRequests.Select(fr => new ReservationLogViewModel
                {
                    FacilityRequest = fr,
                    Event = pendingEvents.FirstOrDefault(e => e.FacilityRequestId == fr.FacilityRequestId)
                }).ToList();

                // Get historical reservations (for History)
                var historicalRequests = await _context.FacilityRequests
                    .Include(fr => fr.Facility)
                    .Where(fr => fr.ResidentId == userId &&
                        (fr.Status == RequestStatus.Approved ||
                         fr.Status == RequestStatus.Declined ||
                         fr.Status == RequestStatus.Cancelled))
                    .OrderByDescending(fr => fr.RequestDate)
                    .ToListAsync();

                var historicalRequestIds = historicalRequests.Select(fr => fr.FacilityRequestId).ToList();
                var historicalEvents = await _context.Events
                    .Where(e => historicalRequestIds.Contains(e.FacilityRequestId ?? 0))
                    .ToListAsync();

                HistoricalReservations = historicalRequests.Select(fr => new ReservationHistoryViewModel
                {
                    FacilityRequest = fr,
                    Event = historicalEvents.FirstOrDefault(e => e.FacilityRequestId == fr.FacilityRequestId)
                }).ToList();
            }

            Facilities = await _context.Facilities.ToListAsync();
        }
    }
}