using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;

namespace FearEngineTests
{
    [TestClass]
    public class MainEngineTests
    {
        [TestMethod]
        public void SimpleStartupAndShutdown()
        {
            //Given
            FearGameFactory gameFactory = new FearGameFactory();
            MockFearGame mockGame = new MockFearGame();

            //When
            FearEngineImpl engine = gameFactory.CreateAndRunFearGame(mockGame);

            //Then
            Assert.IsTrue(true);
        }
    }
}
