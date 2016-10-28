using ReservationSystem.Service.Interface;
using ReservationSystem.Model.Models;
using ReservationSystem.Data.Repositories.IRepositories;

namespace ReservationSystem.Service
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository _reservationRepository;


        public ReservationService(IReservationRepository reservationRepository)
        {
            try
            {
                _reservationRepository = reservationRepository;
            }
            catch { throw; }
        }
        public int CreateReservation(Reservation reservation)
        {
            try
            {
                _reservationRepository.Add(reservation);
                SaveReservation();
                return reservation.Id;
            }
            catch { throw; }
        }

        public void SaveReservation()
        {
            try
            {
                _reservationRepository.Commit();
            }
            catch { throw; }
        }
    }
}
