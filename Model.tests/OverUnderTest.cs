using System;
using Xunit;
using Model;

namespace Model.tests
{
    public class OverUnderTest
    {
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session();
            String sInput = oSession.OnMessage("hello");
            Assert.True(sInput.Contains("Welcome"));
        }
        [Fact]
        public void TestShawarama()
        {
            Session oSession = new Session();
            String sInput = oSession.OnMessage("hello");
            Assert.True(sInput.ToLower().Contains("shawarama"));
        }
        [Fact]
        public void TestSize()
        {
            Session oSession = new Session();
            String sInput = oSession.OnMessage("hello");
            Assert.True(sInput.ToLower().Contains("size"));
        }
        [Fact]
        public void TestLarge()
        {
            Session oSession = new Session();
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("large");
            Assert.True(sInput.ToLower().Contains("protein"));
            Assert.True(sInput.ToLower().Contains("large"));
        }
        [Fact]
        public void TestChicken()
        {
            Session oSession = new Session();
            oSession.OnMessage("hello");
            oSession.OnMessage("large");
            String sInput = oSession.OnMessage("chicken");
            Assert.True(sInput.ToLower().Contains("toppings"));
            Assert.True(sInput.ToLower().Contains("large"));
            Assert.True(sInput.ToLower().Contains("chicken"));
        }
    }
}
