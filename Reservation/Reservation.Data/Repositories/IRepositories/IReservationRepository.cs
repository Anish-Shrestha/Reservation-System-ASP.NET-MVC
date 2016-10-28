using ReservationSystem.Data.Infrastructure;
using ReservationSystem.Model.Models;
namespace ReservationSystem.Data.Repositories.IRepositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
      //  Reservation GetReservationByLocation(string location);
    }
}
