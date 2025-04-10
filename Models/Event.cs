// emptyusing System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HomeownersMS.Models
{
    public enum EventType
    {
        Birthday,
        Wedding,
        Meeting,
        Party,
        Workshop,
        Others
    }

    public class AdditionalServiceDetails
    {
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
    }


    public static class PredefinedServices
    {
        public static readonly AdditionalServiceDetails AirConditioning = new AdditionalServiceDetails
        {
            Name = "Air Conditioning",
            Price = 200,
            Description = "Super cool air conditioning"
        };

        public static readonly AdditionalServiceDetails TableAndChairs = new AdditionalServiceDetails
        {
            Name = "Table and Chairs",
            Price = 100,
            Description = "Additional 25 tables and 125 chairs with setup."
        };

        public static readonly AdditionalServiceDetails SoundSystem = new AdditionalServiceDetails
        {
            Name = "Sound System",
            Price = 150,
            Description = "High-quality microphones, speakers, amplifiers, and basic audio testing."
        };

        public static readonly AdditionalServiceDetails ProjectorAndScreen = new AdditionalServiceDetails
        {
            Name = "Projector and Screen",
            Price = 250,
            Description = "High-quality projector and screen; compatible for HDMI or VGA connections."
        };

        public static readonly AdditionalServiceDetails Decorations = new AdditionalServiceDetails
        {
            Name = "Decorations",
            Price = 300,
            Description = "Basic lightings & minimalist decor setup."
        };

        public static readonly AdditionalServiceDetails CleaningServices = new AdditionalServiceDetails
        {
            Name = "Cleaning Services",
            Price = 300,
            Description = "Offers cleaning services after event"
        };
    }

    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public string? Title { get; set; }

        public EventType EventType { get; set; }

        public DateOnly? EventDate { get; set; }
        public TimeOnly? EventTimeStart { get; set; }

        public TimeOnly? EventTimeEnd { get; set; }

        public int? GuestCapacity { get; set; }

        // Contact info

        public int? ContactNumber { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactName { get; set; }

        public Dictionary<string, AdditionalServiceDetails> AdditionalServices { get; set; } = new();
        /* 
            example of creating new event
                    var newEvent = new Event
                    {
                        Title = "Wedding Celebration",
                        EventType = EventType.wedding,
                        EventDate = new DateOnly(2025, 5, 20),
                        EventTimeStart = new TimeOnly(15, 0),
                        EventTimeEnd = new TimeOnly(20, 0),
                        GuestCapacity = 100,
                        ContactName = "John Doe",
                        ContactNumber = 1234567890,
                        ContactEmail = "john.doe@example.com",
                        AdditionalServices = new Dictionary<string, AdditionalServiceDetails>
                        {
                            { "AirConditioning", PredefinedServices.AirConditioning },
                            { "Decorations", PredefinedServices.Decorations }
                        },
                        CreatedBy = 1
                    };
        */

        [ForeignKey("User")]
        public int? CreatedBy { get; set; }

        [ForeignKey("FacilityRequest")]
        public int? FacilityRequestId { get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User? User { get; set; }

        public virtual FacilityRequest? FacilityRequest { get; set;}
    }
}
