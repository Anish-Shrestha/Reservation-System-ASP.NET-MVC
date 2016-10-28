using ReservationSystem.Data.Configuration;
using ReservationSystem.Model.Models;
using System.Data.Entity;

namespace ReservationSystem.Data
{
    public class ReservationEntities : DbContext
    {
        public ReservationEntities() : base("ReservationEntities") { }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationDetail> ReservationDetails { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          modelBuilder.Configurations.Add(new ReservationConfig());
            modelBuilder.Configurations.Add(new ReservationDetailConfig());
        }
    }
}
