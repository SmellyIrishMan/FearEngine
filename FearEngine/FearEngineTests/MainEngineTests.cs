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

            //When
            FearEngineImpl engine = gameFactory.CreateAndRunFearGame(new MockFearGame());

            //Then
            Assert.IsTrue(true);
        }
    }
}
