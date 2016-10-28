using ReservationSystem.Model.Models;

namespace ReservationSystem.Service.Interface
{
    public interface IReservationService
    {
        int CreateReservation(Reservation reservation);
        void SaveReservation();
    }
}
