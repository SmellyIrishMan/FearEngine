using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine;
using FearEngineTests.FullScaleProjects.Games;

namespace FearEngineTests
{
    [TestClass]
    public class FullScaleTests
    {
        [TestMethod]
        public void BlankWindow()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new BlankWindowDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BasicCube()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new CubeDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TexturedCube()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new TextureCubeDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void NormalMappedMesh()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new NormalMappedMeshDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SimpleScene()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new SimpleSceneDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShaderModelComparisonScene()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new ShaderModelComparisonDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShadowScene()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new ShadowsDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IBLMaterialScene()
        {
            //Given
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new IBLMaterialDemo();

            //Then
            appFactory.CreateAndRunFearGame(game);

            //When
            Assert.IsTrue(true);
        }
    }
}
