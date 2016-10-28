using System.Data.Entity.ModelConfiguration;
using ReservationSystem.Model.Models;

namespace ReservationSystem.Data.Configuration
{
    public class ReservationConfig : EntityTypeConfiguration<Reservation>
    {
        public ReservationConfig()
        {
            ToTable("Reservations");
            Property(c => c.CheckIn).IsRequired();
            Property(c => c.CheckOut).IsRequired();
            Property(c => c.Location).IsRequired().HasMaxLength(100); 
            Property(c => c.DateCreated).IsRequired();
        }
    }
}
