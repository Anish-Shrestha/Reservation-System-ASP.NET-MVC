using ReservationSystem.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace ReservationSystem.Data.Configuration
{
    class ReservationDetailConfig: EntityTypeConfiguration<ReservationDetail>
    {
        public ReservationDetailConfig()
        {
            ToTable("ReservationDetails");
            Property(c => c.ReservationId).IsRequired();
            Property(c => c.DateCreated).IsRequired();
            Property(c => c.Adult).IsRequired();
            Property(c => c.Children).IsRequired();
            
        }
    }
}
