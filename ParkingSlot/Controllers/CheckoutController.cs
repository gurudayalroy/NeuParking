using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingSlot.Controllers
{
    public class CheckoutController : ApiController
    {
        public string Get(int OwnerID)
        {
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
            string str = dbconn.CheckOut(OwnerID);
            return str;
        }
    }
}
