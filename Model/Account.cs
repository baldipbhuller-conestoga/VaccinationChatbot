using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Account
    {
        public enum AccountTypes
        {
            Admin = 1,
            User = 2
        }

        private int accountID;
        private AccountTypes accountType;
        private string fName;
        private string mName;
        private string lName;
        private DateTime birthdate;
        private string city;
        private string postalCode;

        public int AccountID { get => accountID; set => accountID = value; }
        public AccountTypes AccountType { get => accountType; set => accountType = value; }
        public string FName { get => fName; set => fName = value; }
        public string MName { get => mName; set => mName = value; }
        public string LName { get => lName; set => lName = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public string City { get => city; set => city = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }

        public Account(AccountTypes accountType, string fName, string mName, string lName, DateTime birthdate, string city, string postalCode)
        {
            this.accountType = accountType;
            this.FName = fName;
            this.MName = mName;
            this.LName = lName;
            this.Birthdate = birthdate;
            this.City = city;
            this.PostalCode = postalCode;
        }

        public Account()
        {

        }
    }
}
