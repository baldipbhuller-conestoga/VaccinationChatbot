using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DBAccess
    {
        // Connection string in app.config
        private static SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);

        public static int InsertAccount(Account acc)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT into [Account] VALUES (@AccountType,@Fname,@Mname,@Lname,@Birthdate,@City,@PostalCode); SELECT SCOPE_IDENTITY()", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@AccountType", acc.AccountType.ToString());
                sqlCommand.Parameters.AddWithValue("@Fname", acc.FName);
                sqlCommand.Parameters.AddWithValue("@Mname", acc.MName);
                sqlCommand.Parameters.AddWithValue("@Lname", acc.LName);
                sqlCommand.Parameters.AddWithValue("@Birthdate", acc.Birthdate.ToString("yyyy-MM-dd"));
                sqlCommand.Parameters.AddWithValue("@City", acc.City);
                sqlCommand.Parameters.AddWithValue("@PostalCode", acc.PostalCode);

                sqlConn.Open();

                // returns the id of the data row added if successful, else returns null for failed insert
                object idReturned = sqlCommand.ExecuteScalar();
                int id = 0;

                if (idReturned != null)
                {
                    id = Convert.ToInt32(idReturned);
                }

                sqlConn.Close();

                return id;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public static List<Account> GetAllAccounts()
        {
            try
            {
                Account acc = null;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Account]", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlConn.Open();

                // reads the result
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                List<Account> accountList = new List<Account>();

                // read all rows
                while (sqlDataReader.Read())
                {
                    acc = new Account();
                    acc.AccountID = sqlDataReader.GetInt32(0);
                    acc.AccountType = (Account.AccountTypes)Enum.Parse(typeof(Account.AccountTypes), sqlDataReader[1].ToString());

                    // null checks
                    if (!sqlDataReader.IsDBNull(2))
                    {
                        acc.FName = sqlDataReader[2].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(3))
                    {
                        acc.MName = sqlDataReader[3].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(4))
                    {
                        acc.LName = sqlDataReader[4].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(5))
                    {
                        acc.Birthdate = sqlDataReader.GetDateTime(5);
                    }
                    if (!sqlDataReader.IsDBNull(6))
                    {
                        acc.City = sqlDataReader[6].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(7))
                    {
                        acc.PostalCode = sqlDataReader[7].ToString();
                    }

                    accountList.Add(acc);
                }

                sqlConn.Close();

                return accountList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Clinic> GetAllClinics()
        {
            try
            {
                Clinic cl = null;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Clinic]", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlConn.Open();

                // reads the result
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                List<Clinic> clinicList = new List<Clinic>();

                // read all rows
                while (sqlDataReader.Read())
                {
                    cl = new Clinic();
                    cl.ClinicID = sqlDataReader.GetInt32(0);
                    cl.LocationName = sqlDataReader[1].ToString();
                    cl.PostalCode = sqlDataReader[2].ToString();
                    cl.Capacity = sqlDataReader.GetInt32(3);

                    clinicList.Add(cl);

                }

                sqlConn.Close();

                return clinicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Clinic> GetAllClinicsByPostalCode(string postalCode)
        {
            try
            {
                Clinic cl = null;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Clinic] WHERE PostalCode LIKE @postalCode", sqlConn);
                sqlCommand.CommandType = CommandType.Text;
                string searchTerm = "%" + postalCode.Substring(0,3) + "%";
                sqlCommand.Parameters.AddWithValue("@postalCode", searchTerm);

                sqlConn.Open();

                // reads the result
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                List<Clinic> clinicList = new List<Clinic>();

                // read all rows
                while (sqlDataReader.Read())
                {
                    cl = new Clinic();
                    cl.ClinicID = sqlDataReader.GetInt32(0);
                    cl.LocationName = sqlDataReader[1].ToString();
                    cl.PostalCode = sqlDataReader[2].ToString();
                    cl.Capacity = sqlDataReader.GetInt32(3);

                    clinicList.Add(cl);

                }

                sqlConn.Close();

                return clinicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Booking GetBookingByReferenceNumber(string referenceNumber)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Booking] WHERE ReferenceNumber = @referenceNumber", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@referenceNumber", referenceNumber);

                sqlConn.Open();

                // reads the result
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                Booking bkg = new Booking();

                // read all rows
                while (sqlDataReader.Read())
                {
                    bkg.BookingID = sqlDataReader.GetInt32(0);
                    bkg.AccountID = sqlDataReader.GetInt32(1);
                    bkg.ReferenceNumber = sqlDataReader[2].ToString();
                    bkg.ClinicID = sqlDataReader.GetInt32(3);
                    bkg.DoseType = sqlDataReader.GetInt32(4);
                    TimeSpan AppointmentTime = (TimeSpan)sqlDataReader[6];
                    DateTime date = (DateTime)sqlDataReader[5];
                    bkg.AppoinmentDateTime = date + AppointmentTime;
                }

                sqlConn.Close();

                return bkg;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int InsertClinic(Clinic cl)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT into [Clinic] VALUES (@LocationName,@PostalCode,@Capacity); SELECT SCOPE_IDENTITY()", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@LocationName", cl.LocationName);
                sqlCommand.Parameters.AddWithValue("@PostalCode", cl.PostalCode);
                sqlCommand.Parameters.AddWithValue("@Capacity", cl.Capacity);

                sqlConn.Open();

                // returns the id of the data row added if successful, else returns null for failed insert
                object idReturned = sqlCommand.ExecuteScalar();
                int id = 0;

                if (idReturned != null)
                {
                    id = Convert.ToInt32(idReturned);
                }

                sqlConn.Close();

                return id;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static int DeleteClinic(int id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [Clinic] WHERE ClinicID = @ID", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@ID", id);

                sqlConn.Open();

                // return rows affected
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                sqlConn.Close();

                return rowsAffected;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int DeleteBooking(string referenceNumber)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [Booking] WHERE ReferenceNumber = @referenceNumber", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@referenceNumber", referenceNumber);

                sqlConn.Open();

                // return rows affected
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                sqlConn.Close();

                return rowsAffected;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int InsertBooking(int accID, string referenceNumber, int clinicID, int doseType, string appDate, string appTime)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT into [Booking] VALUES (@AccountID,@ReferenceNumber,@ClinicID,@DoseType,@AppDate,@AppTime); SELECT SCOPE_IDENTITY()", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@AccountID", accID);
                sqlCommand.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                sqlCommand.Parameters.AddWithValue("@ClinicID", clinicID);
                sqlCommand.Parameters.AddWithValue("@DoseType", doseType);
                sqlCommand.Parameters.AddWithValue("@AppDate", appDate);
                sqlCommand.Parameters.AddWithValue("@AppTime", appTime);

                sqlConn.Open();

                // returns the id of the data row added if successful, else returns null for failed insert
                object idReturned = sqlCommand.ExecuteScalar();
                int id = 0;

                if (idReturned != null)
                {
                    id = Convert.ToInt32(idReturned);
                }

                sqlConn.Close();

                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateClinicCapacityDecrease(int id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE [Clinic] SET Capacity = Capacity-1 WHERE ClinicID = @ID", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@ID", id);

                sqlConn.Open();

                sqlCommand.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int UpdateClinic(string locationName, string postalCode, int capacity, int id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE [Clinic] SET LocationName = @LocationName, PostalCode = @PostalCode,Capacity = @Capacity WHERE ClinicID = @ID", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@LocationName", locationName);
                sqlCommand.Parameters.AddWithValue("@PostalCode", postalCode);
                sqlCommand.Parameters.AddWithValue("@Capacity", capacity);
                sqlCommand.Parameters.AddWithValue("@ID", id);

                sqlConn.Open();

                int rowsUpdated = sqlCommand.ExecuteNonQuery();

                sqlConn.Close();

                return rowsUpdated;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
