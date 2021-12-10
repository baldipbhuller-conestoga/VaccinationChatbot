using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Controller.Session;

namespace Controller
{
    public class MessageHandler
    {
        const string helpMessage = "The following are the list of commands: \n" +
                                 "book - for booking an vaccine appointment \n" +
                                 "show - for displaying nearby clinics \n" +
                                 "cancel - for cancelling an appointment \n" +
                                 "get - for retreiving booking information";

        Controller controller;

        public MessageHandler()
        {
            controller = Controller.getInstance();
        }

        public bool isMessageHelp(string sInMessage)
        {
            if (sInMessage.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public string EvaluateMessage(string sInMessage, ref State state)
        {
            string replyMessage = string.Empty;

            if (sInMessage.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                replyMessage = helpMessage;
            }
            else if (sInMessage.Equals("book", StringComparison.OrdinalIgnoreCase))
            {
                replyMessage = "Please provide your first name";
                state = State.BEGINACCOUNT;
            }
            else if (sInMessage.Equals("show", StringComparison.OrdinalIgnoreCase))
            {
                replyMessage = "Please provide the postal code";
                state = State.SHOW;
            }
            else if (sInMessage.Equals("cancel", StringComparison.OrdinalIgnoreCase))
            {
                replyMessage = "Please provide the reference number";
                state = State.CANCEL;
            }
            else if (sInMessage.Equals("get", StringComparison.OrdinalIgnoreCase))
            {
                replyMessage = "Please provide the reference number";
                state = State.GET;
            }
            else
            {
                replyMessage = "Please input a valid command or type help";
            }

            return replyMessage;
        }

        public string GatherInfo(int infoCounter)
        {
            string replyMessage = string.Empty;
            List<string> infoToAsk = new List<string>() { "Please provide your middle name",
                                                          "Please provide your last name",
                                                          "Please provide your birthday in format(mm/dd/yyyy).",
                                                          "Please provide your city",
                                                          "Please provide your postal code"};

            if (infoCounter < infoToAsk.Count)
            {
                replyMessage = infoToAsk[infoCounter];
            }
            else
            {   //change state
                replyMessage = "Please wait";
            }

            return replyMessage;
        }

        public string GatherBookingInfo(int infoCounter, string postalCode)
        {
            string replyMessage = string.Empty;
            List<string> infoToAsk = new List<string>() { "Please provide your dose number 1 or 2?",
                                                          "Please choose a clinic (input the exact name)",
                                                          "Please provide your appointment date and time (format as mm/dd/yyyy HH:mm 24-hour format)"};

            if (infoCounter < infoToAsk.Count)
            {
                replyMessage = infoToAsk[infoCounter];
                if (infoCounter == 1)
                {
                    //ConfigurationManager.RefreshSection("connectionStrings");
                    List<string> clinicNames = controller.GetClinicNamesByPostalCode(postalCode);

                    foreach(string name in clinicNames)
                    {
                        replyMessage += "\n" + name;
                    }
                }   
            }
            else
            {   //change state
                replyMessage = "Please wait";
            }

            return replyMessage;
        }

        public string ConfirmCancel(string referenceNumber)
        {
            string replyMessage = "Are you sure you want to cancel booking " + referenceNumber + " ? (Y/N)";

            return replyMessage;
        }

        public string FinalizeCancel(string referenceNumber, string choice)
        {
            string replyMessage = string.Empty;

            if (choice.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                if (controller.RemoveBooking(referenceNumber))
                {
                    replyMessage = "Booking successfully cancelled";
                }
                else
                {
                    replyMessage = "Error in booking cancellation reference number may not be existing";
                }
            }
            else if (choice.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                replyMessage = "Cancel booking ignored";
            }

            return replyMessage;
        }

        public int GetClinicID(string locationName)
        {
            return controller.RetrieveClinicID(locationName);
        }

        public string RegisterAccountAndBook(Account account, Booking booking)
        {
            int resultAccountID = controller.RegisterAccount(Account.AccountTypes.User.ToString(), account.FName, account.MName, account.LName, account.Birthdate, account.City, account.PostalCode);

            string referenceNumber = controller.BookAppointment(resultAccountID,booking.ClinicID,booking.DoseType,booking.AppoinmentDateTime,booking.AppoinmentDateTime);

            string replyMessage = string.Empty;

            if (!string.IsNullOrEmpty(referenceNumber))
            {
                replyMessage = "Your booking is confirmed with reference number: " + referenceNumber;
            }
            else
            {
                replyMessage = "Booking error. Please restart the process.";
            }

            return replyMessage;
        }

        public string ShowNearbyClinic(string postalCode)
        {
            string replyMessage = string.Empty;

            List<string> clinicNames = controller.GetClinicNamesByPostalCode(postalCode);

            if(clinicNames.Count > 0)
            {
                replyMessage = "Nearby clinics found: ";
            }
            else
            {
                replyMessage = "No nearby clinics found";
            }

            foreach (string name in clinicNames)
            {
                replyMessage += "\n" + name;
            }

            return replyMessage;
        }

        public string GetBookingInfo(string referenceNumber)
        {
            return controller.GetBookingInfo(referenceNumber);

        }

        public string getErrorMessage()
        {
            return "Incorrect input. Please input again.\n";
        }
    }
}
