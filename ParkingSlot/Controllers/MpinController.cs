using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingSlot.Controllers
{
    public class MpinController : ApiController
    {
        public int Get(string Name, string Email, string Phone, string VehicleNo)
        {
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
            return dbconn.RegisterUserID(Name, Email, Phone, VehicleNo);

        }
    }
}
