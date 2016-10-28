using System;

namespace ReservationSystem.Web.ViewModels
{
    public class ReservationDetailViewModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int Adult { get; set; }
        public int Children { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public ReservationDetailViewModel(int adult, int children) {
            DateCreated = DateTime.Now;
            Adult = adult;
            Children = children;
        }
        public ReservationDetailViewModel()
        {
            DateCreated = DateTime.Now;
        }
    }
}