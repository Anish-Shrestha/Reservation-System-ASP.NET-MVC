using AutoMapper;
using ReservationSystem.Model.Models;
using ReservationSystem.Web.ViewModels;

namespace ReservationSystem.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SearchViewModel, ReservationViewModel>();
                cfg.CreateMap<ReservationViewModel, Reservation>();
                cfg.CreateMap<ReservationDetailViewModel, ReservationDetail>();
            });
        }
    }
}