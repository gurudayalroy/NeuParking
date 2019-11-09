using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParkingSlot.Controllers
{
    public class VehicleCheckinController : Controller
    {
        // GET: VehicleCheckin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkin()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public ActionResult Checkin(HttpPostedFileBase photo, string SlotName)
        {
            var imageUrl = photo.FileName;
            var t = Task.Run(() => ImageService.MakeOCRRequest(photo));

            t.Wait();
            var carNo = t.Result;
            TempData["CAR_NO"] = carNo.ToString();
            //var SlotName = "12";
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
            string str = dbconn.Checkin(carNo.ToString(), SlotName);
            if (str == "")
                TempData["SlotAssigned"] = SlotName;
            else
                TempData["Message"] = str;



            return RedirectToAction("ParkingStatus");
        }

        /// <summary>
        /// ParkingStatus
        /// </summary>
        /// <returns></returns>
        public ActionResult ParkingStatus()
        {
            var latestImage = string.Empty;
            if (TempData["CAR_NO"] != null && TempData["SlotAssigned"] != null)
            {
                ViewBag.CAR_NO = Convert.ToString(TempData["CAR_NO"]);
                ViewBag.SlotNo = Convert.ToString(TempData["SlotAssigned"]);
            }
            if(TempData["Message"] != null)
            {
                ViewBag.Message = Convert.ToString(TempData["Message"]);
            }
            return View();
        }
    }
}