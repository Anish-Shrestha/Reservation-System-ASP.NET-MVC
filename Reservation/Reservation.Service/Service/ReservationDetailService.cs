using ReservationSystem.Data.Repositories.IRepositories;
using ReservationSystem.Model.Models;
using ReservationSystem.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Service
{
    public class ReservationDetailService : IReservationDetailService
    {
        private readonly IReservationDetailRepository _reservationDetailRepository;


        public ReservationDetailService(IReservationDetailRepository reservationDetailRepository)
        {
            try
            {
                _reservationDetailRepository = reservationDetailRepository;
            }
            catch { throw; }
        }

        public void CreateReservationDetail(IEnumerable<ReservationDetail> rdml)
        {
            try
            {
                foreach (var item in rdml.ToList())
                {
                    _reservationDetailRepository.Add(item);
                }
                _reservationDetailRepository.Commit();
            }
            catch { throw; }
        }

        public void CreateReservationDetail(ReservationDetail reservationDetail)
        {
            try
            {
                _reservationDetailRepository.Add(reservationDetail);
            }
            catch { throw; }
        }

        public void SaveReservationDetail()
        {
            try
            {
                _reservationDetailRepository.Commit();
            }
            catch { throw; }
        }
    }
}
