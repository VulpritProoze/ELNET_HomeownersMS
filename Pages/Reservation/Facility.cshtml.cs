using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeownersMS.Models;
using HomeownersMS.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HomeownersMS.Pages.Reservation
{
    [Authorize(Roles="admin,resident")]
    public class FacilityModel : PageModel 
    {
        private readonly HomeownersContext _context;

        public FacilityModel(HomeownersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Facility? Facility { get; set; }
        
        [BindProperty]
        public List<FacilityReview> FacilityReviews { get; set; } = new();
        
        [BindProperty]
        public FacilityReview NewReview { get; set; } = new();
        
        public bool CanReview { get; set; }
        public bool HasReviewed { get; set; }
        public FacilityReview? UserReview { get; set; }

        public async Task OnGetAsync(int? facilityId)
        {
            if (facilityId == null)
            {
                Facility = null;
                FacilityReviews = new List<FacilityReview>();
                return;
            }

            Facility = await _context.Facilities
                .FirstOrDefaultAsync(f => f.FacilityId == facilityId);

            FacilityReviews = await _context.FacilityReviews
                .Include(fr => fr.Resident)
                .Where(fr => fr.FacilityId == facilityId)
                .ToListAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && int.TryParse(userId, out int residentId))
            {
                // Check if user has reserved this facility before
                // AnyAsync returns a bool
                CanReview = await _context.FacilityRequests
                    .AnyAsync(fr => fr.ResidentId == residentId && 
                                  fr.FacilityId == facilityId && 
                                  fr.Status == RequestStatus.Approved);

                // Check if user already reviewed
                UserReview = await _context.FacilityReviews
                    .FirstOrDefaultAsync(fr => fr.FacilityId == facilityId && 
                                             fr.ResidentId == residentId);
                HasReviewed = UserReview != null;
            }
        }

        public async Task<IActionResult> OnPostAddReviewAsync(int facilityId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage(new { facilityId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && int.TryParse(userId, out int residentId))
            {
                // Verify user can review
                var canReview = await _context.FacilityRequests
                    .AnyAsync(fr => fr.ResidentId == residentId && 
                                  fr.FacilityId == facilityId && 
                                  fr.Status == RequestStatus.Approved);

                if (!canReview) return Forbid();

                // Check if already reviewed
                var existingReview = await _context.FacilityReviews
                    .FirstOrDefaultAsync(fr => fr.FacilityId == facilityId && 
                                              fr.ResidentId == residentId);

                if (existingReview != null) return Forbid();

                NewReview.FacilityId = facilityId;
                NewReview.ResidentId = residentId;
                NewReview.ReviewDate = DateTime.Now;

                _context.FacilityReviews.Add(NewReview);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Reservation/Facility", new { facilityId });
        }

        public async Task<IActionResult> OnPostDeleteReviewAsync(int reviewId, int facilityId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && int.TryParse(userId, out int residentId))
            {
                var review = await _context.FacilityReviews
                    .FirstOrDefaultAsync(fr => fr.ReviewId == reviewId && 
                                             fr.ResidentId == residentId);

                if (review != null)
                {
                    _context.FacilityReviews.Remove(review);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("/Reservation/Facility", new { facilityId });
        }
    }
}