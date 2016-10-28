using log4net;
using ReservationSystem.Model.Models;
using ReservationSystem.Service.Interface;
using ReservationSystem.Web.Resource;
using ReservationSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationSystem.Web.Controllers
{

    public class HomeController : Controller
    {


        #region Declaration

        private static readonly ILog log = LogManager.GetLogger("ReservationSystem");
        public IReservationDetailService _reservationDetailService;
        public IReservationService _reservationService;

        #endregion

        #region Controller

        public HomeController(IReservationDetailService reservationDetailService, IReservationService reservationService)
        {
            _reservationDetailService = reservationDetailService;
            _reservationService = reservationService;

        }

        #endregion

        public ActionResult Index()
        {
            DropdownPopulate(); // Bind dropdown list values
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel svm)
        {
            try
            {
                DropdownPopulate();  // Bind dropdown list values
                var model = BindReservationObject(svm); // Bind SearchViewModel to respective ViewModel
                string errorMessage = string.Empty;
                
                // validate model data
                if (!ModelState.IsValid || !model.validate(out errorMessage))  // If Model state is valid
                {
                    ViewData["hasModelError"] = true;
                    if (!string.IsNullOrEmpty(errorMessage))
                        ModelState.AddModelError(string.Empty, errorMessage);
                    return View(svm);

                }

                // Create reservation
                TempData["SuccessMessage"] = CreateReservation(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message); // logging exception information using Log4Net
                ModelState.AddModelError(string.Empty, string.Format(ReservationResource.FatalError));// Display error message
                return View(svm);
            }
        }


        #region Private Methods 
        
        /// <summary>
        /// Bind dropdown list values for Search-Page
        /// </summary>
        private void DropdownPopulate()
        {
            try
            {
                List<SelectListItem> sli = new List<SelectListItem>();
                for (var i = 1; i < 10; i++)
                {
                    sli.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewData["RoomList"] = sli;
                ViewData["AdultList"] = sli;
                ViewData["ChildrenList"] = sli;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Method to create reservation and reservation detail.
        /// </summary>
        private string CreateReservation(ReservationViewModel rvm)
        {
            try
            {
                // Create reservation information
                var insertedId = _reservationService.CreateReservation(AutoMapper.Mapper.Map<Reservation>(rvm));
                rvm.ReservationDetailList.ForEach(x => x.ReservationId = insertedId);

                // Create reservation detail information
                _reservationDetailService.CreateReservationDetail(rvm.ReservationDetailList
                    .Select(x => AutoMapper.Mapper.Map<ReservationDetail>(x)).ToList());
                return string.Format(ReservationResource.SuccessReservation, rvm.Location);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }
        
        /// <summary>
        /// Map SearchViewModel to Reservation and ReservationDetail ViewModel
        /// </summary>
        private ReservationViewModel BindReservationObject(SearchViewModel svm)
        {
            try
            {
                var rvm = AutoMapper.Mapper.Map<ReservationViewModel>(svm);
                rvm.ReservationDetailList = BindReservationDetail(svm);
                return rvm;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// Map SearchViewModel to ReservationDetail ViewModel
        /// </summary>
        private List<ReservationDetailViewModel> BindReservationDetail(SearchViewModel svm)
        {
            try
            {
                List<ReservationDetailViewModel> lst = new List<ReservationDetailViewModel>();
                for (int i = 0; i < svm.Adult.Count(); i++)
                {
                    ViewData["AdultListStateStore"] += svm.Adult[i] + ",";
                    ViewData["ChildrenListStateStore"] += svm.Children[i] + ",";
                    lst.Add(new ReservationDetailViewModel(svm.Adult[i], svm.Children[i]));
                }
                return lst;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }

        #endregion
    }
}