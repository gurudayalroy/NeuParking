using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingSlot.Controllers
{
    public class ValidateCheckinController : ApiController
    {
        public int Get(int OwnerID)
        {
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
            return dbconn.ValidateCheckin(OwnerID);
        }
    }
}
