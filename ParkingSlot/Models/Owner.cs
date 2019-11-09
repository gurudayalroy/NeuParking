using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ParkingSlot.Models
{
    public class Owner
    {
        [Required]
        [Display(Name = "Owner Name")]
        public string OwnerName{ get; set; }

        [Required]
        [Display(Name = "Owner Email")]
        public string OwnerEmail{ get; set; }

        [Required]
        [Display(Name = "Owner Phone")]
        public string OwnerPhone{ get; set; }

        [Required]
        [Display(Name = "vehicle Number")]
        public string VehicleNumber { get; set; }
    }
}