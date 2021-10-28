using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Clinic
    {
        private int clinicID;
        private string locationName;
        private string postalCode;
        private int capacity;

        public int ClinicID { get => clinicID; set => clinicID = value; }
        public string LocationName { get => locationName; set => locationName = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public int Capacity { get => capacity; set => capacity = value; }

        public Clinic(string locationName, string postalCode, int capacity)
        {
            this.LocationName = locationName;
            this.PostalCode = postalCode;
            this.Capacity = capacity;
        }

        public Clinic()
        {

        }
    }
}
