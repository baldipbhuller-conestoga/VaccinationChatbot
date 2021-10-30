using System;

namespace Controller
{
    public class Session
    {
        public enum State
        {
            WELCOMING, HELP, BOOK
        }

        private State nCur = State.WELCOMING;

        private MessageHandler msgHandler;

        public Session(){
            msgHandler = new MessageHandler();
        }

        public String OnMessage(String sInMessage)
        {
            String sMessage = "Welcome to Vaccination Chabot, what can I help you with?";
            switch (this.nCur)
            {
                case State.WELCOMING:
                    this.nCur = State.HELP;
                    break;
                case State.HELP:
                    sMessage = msgHandler.EvaluateMessage(sInMessage, this.nCur);
                    break;
                case State.BOOK:
                    sMessage = "Please provide your first name";
                    break;
            }
            System.Diagnostics.Debug.WriteLine(sMessage);
            return sMessage;
        }

    }
}
