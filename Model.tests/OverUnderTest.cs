using System;
using Xunit;
using Model;
using Controller;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Model.tests
{
    public class OverUnderTest
    {
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session();
            String sInput = oSession.OnMessage("hello");
            Assert.Contains("Welcome", sInput);
        }
        [Fact]
        public void TestHelp()
        {
            Session oSession = new Session();
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("help");
            Assert.Contains("commands", sInput.ToLower());
        }
        [Fact]
        public void TestAccountInfoFirstMessage()
        {
            Session oSession = new Session();
            oSession.OnMessage("hello");
            oSession.OnMessage("help");
            String sInput = oSession.OnMessage("book");
            Assert.Contains("first name", sInput.ToLower());
        }
        //[Fact]
        //public void TestBookingInfoClinic()
        //{
        //    Session oSession = new Session();
        //    oSession.OnMessage("hello");
        //    oSession.OnMessage("help");
        //    oSession.OnMessage("book");
        //    oSession.OnMessage("mike");
        //    oSession.OnMessage("jimm");
        //    oSession.OnMessage("smith");
        //    oSession.OnMessage("10/05/1992");
        //    oSession.OnMessage("Woodstock");
        //    oSession.OnMessage("N4T0H1");
        //    String sInput = oSession.OnMessage("1");
        //    Assert.Contains("choose a clinic", sInput.ToLower());
        //    Assert.Contains("Southwestern Clinic", sInput.ToLower());

        //}
    }
}
