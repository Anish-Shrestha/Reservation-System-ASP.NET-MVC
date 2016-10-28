using System;

namespace ReservationSystem.Model.Models
{
    public class ReservationDetail
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int Adult { get; set; }
        public int Children { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
