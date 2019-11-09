using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingSlot.Controllers
{
    public class CheckinController : ApiController
    {
        // GET: api/Checkin
        public IEnumerable<string> Get()
        {
            return new string[] { "OwnerID", "SlotName" };
        }

        // GET: api/Checkin/5
        public string Get(int OwnerID, string  SlotName)
        {
            DBLayer.DBConnector dbconn = new DBLayer.DBConnector();
            string str = dbconn.Checkin(OwnerID,SlotName);
            return str;
        }
        // POST: api/Checkin
        public void Post([FromBody]string value)
        {

        }
        // PUT: api/Checkin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Checkin/5
        public void Delete(int id)
        {
        }
    }
}
