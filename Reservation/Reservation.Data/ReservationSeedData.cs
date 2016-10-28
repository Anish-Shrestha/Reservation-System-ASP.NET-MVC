using System.Data.Entity;

namespace ReservationSystem.Data
{
    public class ReservationSeedData : DropCreateDatabaseIfModelChanges<ReservationEntities>
    {
        protected override void Seed(ReservationEntities context)
        {
            context.Commit();
        }
    }
}
