using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingSlot.Controllers
{
    public class SlotListController : ApiController
    {
        public string Get()
        {
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
            return dbconn.GetAvailableSlotList();
        }
    }
}
