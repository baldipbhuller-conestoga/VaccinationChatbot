using Model;
using System;
using System.Globalization;

namespace Controller
{
    public class Session
    {
        public enum State
        {
            WELCOMING, HELP, BEGINACCOUNT, BOOK, SHOW, CANCEL, GET
        }

        private State nCur = State.WELCOMING;

        private MessageHandler msgHandler;
        private int infoCounter;
        private Account account;
        private Booking booking;
        private string referenceNumberForCancelOrGet;

        public Session(){
            msgHandler = new MessageHandler();
            infoCounter = 0;
            account = new Account();
            booking = new Booking();
            referenceNumberForCancelOrGet = string.Empty;
        }

        public String OnMessage(String sInMessage)
        {
            String sMessage = "Welcome to Vaccination Chabot, what can I help you with?";

            //if the message is help then change the state to help
            if (msgHandler.isMessageHelp(sInMessage))
            {
                this.nCur = State.HELP;
            }

            switch (this.nCur)
            {
                case State.WELCOMING:
                    this.nCur = State.HELP;
                    break;

                case State.HELP:
                    sMessage = msgHandler.EvaluateMessage(sInMessage, ref this.nCur);

                    // if the state was change to book
                    // then reset account and booking object
                    if(this.nCur == State.BOOK)
                    {
                        account = new Account();
                        booking = new Booking();
                    }

                    break;

                case State.BEGINACCOUNT:
                    bool isAccountInfoComplete = formulateAccount(sInMessage, infoCounter);

                    if (isAccountInfoComplete)
                    {
                        this.nCur = State.BOOK;
                        infoCounter = 0;
                        sMessage = msgHandler.GatherBookingInfo(infoCounter, account.PostalCode);
                        
                    }
                    else
                    {
                        sMessage = msgHandler.GatherInfo(infoCounter);
                        infoCounter++;
                    }

                    break;

                case State.BOOK:
                    bool isBookingInfoComplete = formulateBooking(sInMessage, infoCounter);

                    if (isBookingInfoComplete)
                    {
                        //save account and booking, reply with reference number
                        sMessage = msgHandler.RegisterAccountAndBook(account, booking);
                    }
                    else
                    {
                        infoCounter++;
                        sMessage = msgHandler.GatherBookingInfo(infoCounter, account.PostalCode);
                        
                    }

                    break;

                case State.SHOW:
                    sMessage = msgHandler.ShowNearbyClinic(sInMessage);

                    break;

                case State.CANCEL:
                    if (string.IsNullOrEmpty(referenceNumberForCancelOrGet))
                    {
                        referenceNumberForCancelOrGet = sInMessage;
                        sMessage = msgHandler.ConfirmCancel(sInMessage);
                    }
                    //this means we have already asked the reference and just want to confirm and cancel
                    else
                    {
                        sMessage = msgHandler.FinalizeCancel(referenceNumberForCancelOrGet, sInMessage);
                        //reset referenceNumber variable
                        referenceNumberForCancelOrGet = string.Empty;
                    }

                    break;

                case State.GET:
                    sMessage = msgHandler.GetBookingInfo(sInMessage);
                    this.nCur = State.HELP;

                    break;
            }
            System.Diagnostics.Debug.WriteLine(sMessage);
            return sMessage;
        }

        public bool formulateAccount(string sInMessage, int infoCounter)
        {
            bool isAccountInfoComplete = false;
            switch (infoCounter)
            {
                case 0:
                    account.FName = sInMessage;
                    break;
                case 1:
                    account.MName = sInMessage;
                    break;
                case 2:
                    account.LName = sInMessage;
                    break;
                case 3:
                    account.Birthdate = DateTime.ParseExact(sInMessage, "mm/dd/yyyy", CultureInfo.InvariantCulture);
                    break;
                case 4:
                    account.City = sInMessage;
                    break;
                case 5:
                    account.PostalCode = sInMessage;
                    isAccountInfoComplete = true;
                    break;
            }

            return isAccountInfoComplete;
        }

        public bool formulateBooking(string sInMessage, int infoCounter)
        {
            bool isBookingComplete = false;
            switch (infoCounter)
            {
                case 0:
                    booking.DoseType = Convert.ToInt32(sInMessage);
                    break;
                case 1:
                    booking.ClinicID = msgHandler.GetClinicID(sInMessage);
                    break;
                case 2:
                    booking.AppoinmentDateTime = DateTime.ParseExact(sInMessage, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
                    isBookingComplete = true;
                    break;
            }

            return isBookingComplete;
        }

    }
}
