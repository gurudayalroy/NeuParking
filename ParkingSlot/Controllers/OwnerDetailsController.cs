using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParkingSlot.Controllers
{
    public class OwnerDetailsController : Controller
    {
        
        public ActionResult OwnerInformation()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult OwnerInformation(HttpPostedFileBase photo)
        {
            var imageUrl = photo.FileName;
            var t = Task.Run(() => ImageService.MakeOCRRequest(photo));
            t.Wait();
            var carNo = t.Result;
            TempData["CAR_NO"] = carNo.ToString();
            return RedirectToAction("OwnerDetails");
        }
        // GET: OwnerDetails
        public ActionResult OwnerDetails()
        {
            string vehicleno = string.Empty;
            if (TempData["CAR_NO"] != null)
            {
                vehicleno = Convert.ToString(TempData["CAR_NO"]);
            }
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();           
            return View(dbconn.GetOwnerInfo(vehicleno));
        }
    }
}