using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Booking
    {
        private int bookingID;
        private int accountID;
        private string referenceNumber;
        private int clinicID;
        private int doseType;
        private DateTime appoinmentDateTime;

        public Booking()
        {

        }

        public Booking(int accountID, string referenceNumber, int clinicID, int doseType, DateTime appoinmentDateTime)
        {
            this.AccountID = accountID;
            this.ReferenceNumber = referenceNumber;
            this.ClinicID = clinicID;
            this.DoseType = doseType;
            this.AppoinmentDateTime = appoinmentDateTime;
        }

        public int BookingID { get => bookingID; set => bookingID = value; }
        public int AccountID { get => accountID; set => accountID = value; }
        public string ReferenceNumber { get => referenceNumber; set => referenceNumber = value; }
        public int ClinicID { get => clinicID; set => clinicID = value; }
        public int DoseType { get => doseType; set => doseType = value; }
        public DateTime AppoinmentDateTime { get => appoinmentDateTime; set => appoinmentDateTime = value; }
    }
}
