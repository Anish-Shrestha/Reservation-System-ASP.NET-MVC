using ReservationSystem.Data.Infrastructure;
using ReservationSystem.Data.Repositories.IRepositories;
using ReservationSystem.Model.Models;

namespace ReservationSystem.Data.Repositories
{
    public class ReservationDetailRepository : RepositoryBase<ReservationDetail>, IReservationDetailRepository
    {       
        public ReservationDetailRepository() 
            : base() { }
    }
}
