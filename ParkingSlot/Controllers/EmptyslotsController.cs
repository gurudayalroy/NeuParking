using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingSlot.DBLayer;
namespace ParkingSlot.Controllers
{
    public class EmptyslotsController : Controller
    {
        // GET: Emptyslots
        public ActionResult EmptySlots()
        {
            DBLayer.DBConnector conn = new DBLayer.DBConnector();
            //ViewData["parkinginfo"] = conn.GetVacantSlots();
            return View(conn.GetVacantSlots());
        }
    }
}