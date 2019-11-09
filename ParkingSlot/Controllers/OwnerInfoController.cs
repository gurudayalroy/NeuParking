using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ParkingSlot.Controllers
{
    public class OwnerInfoController : ApiController
    {
        public List<DBLayer.OwnerInfo> Get(string vehicleno)
        {
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
            return dbconn.GetOwnerInfo(vehicleno);
        }
    }
}
