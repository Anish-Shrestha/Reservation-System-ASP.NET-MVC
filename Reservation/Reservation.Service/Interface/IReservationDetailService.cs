using ReservationSystem.Model.Models;
using System.Collections.Generic;

namespace ReservationSystem.Service.Interface
{
    public interface IReservationDetailService
    {
        void CreateReservationDetail(ReservationDetail reservationDetail);
        void SaveReservationDetail();
        void CreateReservationDetail(IEnumerable<ReservationDetail> rdml);
    }
}
