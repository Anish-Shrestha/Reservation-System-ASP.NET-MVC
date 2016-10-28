using ReservationSystem.Web.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace ReservationSystem.Web.ViewModels
{
    public class SearchViewModel
    {
        [Required, Display(Name = "Location")]
        public string Location { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Rooms { get; set; }
        public List<int> Adult { get; set; }
        public List<int> Children { get; set; }

        [ScaffoldColumn(false)]
        public string AdultList { get; set; }

        [ScaffoldColumn(false)]
        public string ChildrenList { get; set; }
    }
}