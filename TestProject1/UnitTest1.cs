using ComputerGameLibrary;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLineToGame1()
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Game game =mw.LineToGame("The Legend of Zelda: Ocarina of Time, Nintendo 64,\"November 23, 1998\", \"As a young boy, Link is tricked by Ganondorf, the King of the Gerudo Thieves.The evil human uses Link to gain access to the Sacred Realm, where he places his tainted hands on Triforce and transforms the beautiful Hyrulean landscape into a barren wasteland.Link is determined to fix the problems he helped to create, so with the help of Rauru he travels through time gathering the powers of the Seven Sages.\",99,9.1");

            Assert.AreEqual(game.Name, "The Legend of Zelda: Ocarina of Time");

        }
        [Test]
        public void TestLineToGame2()
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Game game = mw.LineToGame("The Legend of Zelda: Ocarina of Time, Nintendo 64,\"November 23, 1998\", \"As a young boy, Link is tricked by Ganondorf, the King of the Gerudo Thieves.The evil human uses Link to gain access to the Sacred Realm, where he places his tainted hands on Triforce and transforms the beautiful Hyrulean landscape into a barren wasteland.Link is determined to fix the problems he helped to create, so with the help of Rauru he travels through time gathering the powers of the Seven Sages.\",99,9.1");

            Assert.AreEqual(game.Platform, "Nintendo 64");

        }
    }
}