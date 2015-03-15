using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngineTests.FullScaleProjects.Games;

namespace FearEngineTests
{
    [TestClass]
    public class FullScaleTests
    {
        [TestMethod]
        public void BasicCube()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new BasicCubeGame();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BasicTeapot()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new BasicTeapotGame();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }
    }
}
