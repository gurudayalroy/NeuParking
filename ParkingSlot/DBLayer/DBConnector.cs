using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ParkingSlot.DBLayer
{
    public class DBConnector
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;
        public string Checkin(string vehicleNo, string ParkingID)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string message = string.Empty;
                //SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_checkin_vehicle";
                cmd.Connection = conn;
                cmd.Parameters.Add("@VehicleNo", SqlDbType.NVarChar).Value = vehicleNo;
                cmd.Parameters.Add("@Parking", SqlDbType.NVarChar).Value = ParkingID;
                conn.Open();
                message = cmd.ExecuteScalar().ToString();
                return message;
            }

        }
        public string CheckOut(int OwnerID)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string message = string.Empty;
                //SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_checkout_vehicle";
                cmd.Connection = conn;
                cmd.Parameters.Add("@OwnerID", SqlDbType.Int).Value = OwnerID;
                //cmd.Parameters.Add("@ParkingID", SqlDbType.Int).Value = ParkingID;
                conn.Open();
                cmd.ExecuteNonQuery();
                return "You have successfully checked out";
            }

        }
        public List<OwnerInfo> GetOwnerInfo(string vehicleNumber)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string message = string.Empty;
                //SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Get_Owner_Info";
                cmd.Connection = conn;
                cmd.Parameters.Add("@VehicleNo", SqlDbType.NVarChar).Value = vehicleNumber;
                //cmd.Parameters.Add("@ParkingID", SqlDbType.Int).Value = ParkingID;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<OwnerInfo> ownerInfo = new List<DBLayer.OwnerInfo>();
                OwnerInfo info = null;
                while (reader.Read())
                {
                    info = new OwnerInfo();
                    info.OwnerName = reader["OwnerName"].ToString();
                    info.ContactType = reader["ContactType"].ToString();
                    info.ContactDetails = reader["ContactDetails"].ToString();
                    ownerInfo.Add(info);
                }
                return ownerInfo;
            }

        }
        public int RegisterUserID(string email, Int32 empid)
        {
            
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                
                //SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetOwnerID";
                cmd.Connection = conn;
                cmd.Parameters.Add("@emailid", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add("@empid", SqlDbType.BigInt).Value = empid;
                //cmd.Parameters.Add("@ParkingID", SqlDbType.Int).Value = ParkingID;
                conn.Open();
                var data = cmd.ExecuteScalar();
                if(data == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(data.ToString());
                }
                

            }
        }
        public List<VacantParking> GetVacantSlots()
        {
            List<VacantParking> vParkings = new List<VacantParking>();
            VacantParking vParking = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {

                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GetVacantSlots";
                cmd.Connection = conn;               
                //cmd.Parameters.Add("@ParkingID", SqlDbType.Int).Value = ParkingID;
                conn.Open();
                SqlDataReader data = cmd.ExecuteReader();
                while(data.Read())
                {
                    vParking = new VacantParking();
                    vParking.parkingSlot = data["ParkingSlot"].ToString();
                    vParking.ParkingAvailable = data["ParkingAvailable"].ToString();
                    vParking.ParkedVehicle = data["ParkedVehicle"].ToString();
                    vParking.BlockedBy = data["BlockingVehicle"].ToString();
                    vParkings.Add(vParking);
                }

                return vParkings;
            }
        }
        /// <summary>
        /// This method will be used by the android app for list population
        /// </summary>
        /// <returns>string</returns>
        public string GetAvailableSlotList()
        {
            string availableSlots = string.Empty;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                
                //SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetSlots_Mobile";
                cmd.Connection = conn;               
                //cmd.Parameters.Add("@ParkingID", SqlDbType.Int).Value = ParkingID;
                conn.Open();
                SqlDataReader data = cmd.ExecuteReader();
                while(data.Read())
                {
                    availableSlots = availableSlots + "," + data["ParkingSlot"].ToString();
                }
                availableSlots = availableSlots.Substring(1);
                return availableSlots;
            }
        }
        public int ValidateCheckin(int OwnerID)
        {
            string availableSlots = string.Empty;
            using (SqlConnection conn = new SqlConnection(connStr))
            {

                //SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ValidateSlots_Mobile";
                cmd.Connection = conn;
                cmd.Parameters.Add("@OwnerID", SqlDbType.Int).Value = OwnerID;
                conn.Open();
                return int.Parse(cmd.ExecuteScalar().ToString());                
               
            }
        }
    }
}