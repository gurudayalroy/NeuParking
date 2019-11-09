using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParkingSlot.Controllers
{
    public class VehicleCheckoutController : Controller
    {
        // GET: VehicleCheckout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(HttpPostedFileBase photo)
        {
            var imageUrl = photo.FileName;
            var t = Task.Run(() => ImageService.MakeOCRRequest(photo));
            t.Wait();
            var carNo = t.Result;
            TempData["CAR_NO"] = carNo.ToString();
            //Checkout  API Call with Vehicle No
            return RedirectToAction("ParkingStatus");
        }
        public ActionResult ParkingStatus()
        {
            var latestImage = string.Empty;
            if (TempData["CAR_NO"] != null)
            {
                ViewBag.CAR_NO = Convert.ToString(TempData["CAR_NO"]);
            }
            return View();
        }
    }
}