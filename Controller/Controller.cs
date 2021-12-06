using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Controller
    {
        // This is for singleton pattern having only one instance of controller
        private static Controller controller = new Controller();

        private Account loggedinAccount;
        private List<Account> accountList;
        private List<Clinic> clinicList;
        private List<Booking> bookingList;

        // for generating reference number
        private const string alphaNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public List<Account> AccountList { get => accountList; set => accountList = value; }
        public List<Clinic> ClinicList { get => clinicList; set => clinicList = value; }
        public Account LoggedinAccount { get => loggedinAccount; set => loggedinAccount = value; }
        public List<Booking> BookingList { get => bookingList; set => bookingList = value; }

        // Do not allow controller to be created in other classes
        private Controller()
        {

        }

        public static Controller getInstance()
        {
            return controller;
        }

        public int RegisterAccount(string accountType, string fName, string mName, string lName, DateTime dt, string city, string postalCode)
        {
            LoadAccounts();

            Account checkAcc = AccountList.Find(a => a.FName == fName);

            // check if username if already existing
            if (checkAcc != null)
            {
                return -1;
            }

            Account acc = new Account((Account.AccountTypes)Enum.Parse(typeof(Account.AccountTypes), accountType),
                                      fName, mName, lName, dt, city, postalCode);

            int idResult = DBAccess.InsertAccount(acc);

            // returns non-zero ID when successful and the account ID
            return idResult;
        }


        public void LoadAccounts()
        {
            AccountList = DBAccess.GetAllAccounts();
        }

        public void LoadClinics()
        {
            ClinicList = DBAccess.GetAllClinics();
        }
        public string GetBookingInfo(string referenceNumber)
        {
            string bookingInfo = string.Empty;
            Booking? bkg = DBAccess.GetBookingByReferenceNumber(referenceNumber);

            if(!string.IsNullOrEmpty(bkg.ReferenceNumber))
            {
                LoadAccounts();
                LoadClinics();

                Account accFound = AccountList.Find(a => a.AccountID == bkg.AccountID);
                Clinic clinicFound = ClinicList.Find(c => c.ClinicID == bkg.ClinicID);

                bookingInfo = "Booking Found Reference Number: " + bkg.ReferenceNumber + "\n" +
                              "Name: " + accFound.FName + " " + accFound.MName + " " + accFound.LName + "\n" +
                              "Clinic: " + clinicFound.LocationName + "\n" +
                              "Appointment Date and Time: " + bkg.AppoinmentDateTime.ToString("yyyy-MM-dd") + " " + bkg.AppoinmentDateTime.ToString("HH:mm") + "\n";
            }
            else
            {
                bookingInfo = "Booking not found. Please ensure you have the right reference number";
            }

            return bookingInfo;

        }

        public List<string> GetClinicLocationNames()
        {
            List<string> clinicNameList = new List<string>();

            foreach (Clinic cl in ClinicList)
            {
                clinicNameList.Add(cl.LocationName);
            }

            return clinicNameList;
        }

        public List<string> GetClinicNamesByPostalCode(string postalCode)
        {
            List<Clinic> clinicDBList = DBAccess.GetAllClinicsByPostalCode(postalCode);

            List<string> clinicNameList = new List<string>();

            foreach (Clinic cl in clinicDBList)
            {
                clinicNameList.Add(cl.LocationName);
            }

            return clinicNameList;


        }

        public int AddClinic(string locationName, string postalCode, int capacity)
        {
            LoadClinics();

            Clinic checkCl = ClinicList.Find(c => c.LocationName == locationName);

            // check if we already have a same clinic name
            if (checkCl != null)
            {
                return -1;
            }

            Clinic cl = new Clinic(locationName, postalCode, capacity);

            int idResult = DBAccess.InsertClinic(cl);

            return idResult;

        }

        public bool RemoveClinic(int id)
        {
            int resultRows = DBAccess.DeleteClinic(id);

            if (resultRows == 1)
            {
                return true;
            }

            return false;
        }

        public bool RemoveBooking(string referenceNumber)
        {
            int resultRows = DBAccess.DeleteBooking(referenceNumber);

            if (resultRows == 1)
            {
                return true;
            }

            return false;
        }

        public int RetrieveClinicID(string locationName)
        {
            LoadClinics();

            Clinic clinicFound = ClinicList.Find(c => c.LocationName.ToLower() == locationName.ToLower());

            return clinicFound.ClinicID;
        }

        public string BookAppointment(int accountID,int clinicID, int doseType, DateTime selectTime, DateTime selectDate)
        {
            string reference = GenerateReference();

            string date = selectDate.ToString("yyyy-MM-dd");
            string time = selectTime.ToString("HH:mm");


            int idResult = DBAccess.InsertBooking(accountID, reference, clinicID, doseType, date, time);

            // After successful insert, update clinic capacity
            if (idResult >= 1)
            {
                DBAccess.UpdateClinicCapacityDecrease(clinicID);
            }
            else
            {
                reference = string.Empty;
            }

            return reference;

        }

        // This method generate an alphanumber string with 6 as length and used as referenceNumber
        public string GenerateReference()
        {
            int length = 6;
            Random rand = new Random();

            StringBuilder referenceNumber = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                char randChar = alphaNum[rand.Next(0, alphaNum.Length)];
                referenceNumber.Append(randChar);
            }

            return referenceNumber.ToString();
        }

        public int UpdateClinic(string locationName, string postalCode, int capacity, int id)
        {
            int resultRow = DBAccess.UpdateClinic(locationName, postalCode, capacity, id);

            return resultRow;
        }
    }
}
