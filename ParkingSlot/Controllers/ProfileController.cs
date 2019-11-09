using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSlot.Controllers
{
    public class ProfileController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Profile
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult RegisterOwner()
        {
            return View();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegisterOwner(ParkingSlot.Models.Owner owner)

        {
            if (ModelState.IsValid)
            {
                DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
                dbconn.RegisterUserID(owner.OwnerName, owner.OwnerEmail, owner.OwnerPhone, owner.VehicleNumber);
            }
            TempData["Owner_Name"] = owner.OwnerName;
            TempData["CAR_NO"] = owner.VehicleNumber;
            return RedirectToAction("OwnerStatus");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult OwnerStatus()
        {
            var latestImage = string.Empty;
            if (TempData["CAR_NO"] != null)
            {
                ViewBag.CAR_NO = Convert.ToString(TempData["CAR_NO"]);
            }
            if (TempData["Owner_Name"] != null)
            {
                ViewBag.Owner_Name = Convert.ToString(TempData["Owner_Name"]);
            }
            return View();
        }
    }
}