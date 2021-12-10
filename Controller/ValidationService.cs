using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Controller
{
    public class ValidationService
    {
        private static string regexForName = "^[a-zA-Z ]*$";
        public static bool validateAccountInput(string message, int infoCounter)
        {
            bool isValid = false;

            if(infoCounter < 2)
            {
                isValid = Regex.Match(message, regexForName).Success;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
