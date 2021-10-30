using System;
using System.Collections.Generic;
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
        public string EvaluateMessage(string sInMessage, State state)
        {
            string replyMessage = string.Empty;

            if (sInMessage.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                replyMessage = helpMessage;
            }
            else
            {
                replyMessage = "Please input a valid command";
            }

            return replyMessage;
        }
    }
}
