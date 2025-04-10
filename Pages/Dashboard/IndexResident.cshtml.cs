using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeownersMS.Pages.Dashboard
{
    [Authorize(Roles = "admin,resident")]
    public class IndexResidentModel : PageModel
    {
        private readonly HomeownersMS.Data.HomeownersContext _context;

        public IndexResidentModel(HomeownersMS.Data.HomeownersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Models.Announcement> LatestAnnouncements { get; private set; } = new();
        public List<Models.Event> AllEvents { get; private set; } = new();
        public DateTime? SelectedDate { get; set; }

        public void OnGet(DateTime? date = null)
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Fetch the 3 latest announcements ordered by CreatedAt (newest first)
            LatestAnnouncements = _context
                .Announcements
                .OrderByDescending(a => a.CreatedAt)
                .Take(3)
                .ToList();

            SelectedDate = date;

            // Base query with required includes
            var eventsQuery = _context.Events
                .Include(e => e.FacilityRequest)
                .ThenInclude(fr => fr.Facility)
                .Where(e => e.FacilityRequest.Status == Models.RequestStatus.Approved) // Only approved events
                .Where(e => e.CreatedBy == userId); // Only events created by current user

            // Apply date filter if provided
            if (date.HasValue)
            {
                eventsQuery = eventsQuery.Where(e => e.EventDate == DateOnly.FromDateTime(date.Value));
            }

            AllEvents = eventsQuery
                .OrderBy(e => e.EventDate)
                .ThenBy(e => e.EventTimeStart)
                .ToList();
        }

        public IActionResult OnGetFilterEvents(DateTime? date = null)
        {
            // Get current user ID from claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var eventsQuery = _context.Events
                .Include(e => e.FacilityRequest)
                .ThenInclude(fr => fr.Facility)
                .Where(e => e.FacilityRequest.Status == Models.RequestStatus.Approved)
                .Where(e => e.CreatedBy == userId);

            if (date.HasValue)
            {
                eventsQuery = eventsQuery.Where(e => e.EventDate == DateOnly.FromDateTime(date.Value));
            }

            var events = eventsQuery
                .OrderBy(e => e.EventDate)
                .ThenBy(e => e.EventTimeStart)
                .ToList();

            return Partial("_EventsPartial", events);
        }
    }
}