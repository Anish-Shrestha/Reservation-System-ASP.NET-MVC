using ReservationSystem.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ReservationSystem.Web.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Rooms { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public List<ReservationDetailViewModel> ReservationDetailList { get; set; }

        public ReservationViewModel()
        {
            DateCreated = DateTime.Now;
        }

        public bool validate(out string message)
        {
            try
            {
                message = null;
                if (Location == null || Location == "")
                {
                    message = string.Format(ReservationResource.ErrorLocationNull);
                    return false;
                }
                else if (Location.Length > 100 || Location.All(char.IsDigit))
                {
                    message = string.Format(ReservationResource.ErrorLocation, Location);
                    return false;
                }
                else if (CheckIn == new DateTime())
                {

                    message = string.Format(ReservationResource.ErrorCheckInInput);
                    return false;
                }
                else if (CheckOut == new DateTime())
                {
                    message = string.Format(ReservationResource.ErrorCheckOutInput);
                    return false;
                }
                else if (CheckIn > CheckOut)
                {
                    message = string.Format(ReservationResource.ErrorCheckInCheckOut, CheckIn.ToString("MM/dd/yyyy"), CheckOut.ToString("MM/dd/yyyy"));
                    return false;
                }
                else if ((CheckIn - DateTime.Today).TotalDays < 0)
                {
                    message = string.Format(ReservationResource.ErrorCheckInOld, CheckIn.ToString("MM/dd/yyyy"));
                    return false;
                }
                else if ((CheckOut - DateTime.Today).TotalDays < 0)
                {
                    message = string.Format(ReservationResource.ErrorCheckOut, CheckIn.ToString("MM/dd/yyyy"));
                    return false;
                }
                else if (Rooms != ReservationDetailList.Count())
                {
                    message = string.Format(ReservationResource.ErrorRoomNumber);
                    return false;
                }

                message = string.Empty;
                return true;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}