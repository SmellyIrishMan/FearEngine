﻿using FearEngine;
using FearEngine.GameObjects;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.Resources.Materials;
using FearEngine.Timer;
using SharpDX.Direct3D11;
using SharpDX;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using SharpDX.Direct3D;

namespace FearEngineExeTest
{
    class SimpleMainEntry
    {
        static int Main(string[] args)
        {
            FearGameFactory appFactory = new FearGameFactory();
            FearGame game = new TestGame();
            appFactory.CreateAndRunFearGame(game);

            return 0;
        }
    }

    class TestGame : FearEngine.FearGame
    {
        Scene scene;
        GameObject cam;
        SharpDX.Toolkit.Graphics.GraphicsDevice device;

        public void Startup(FearEngineImpl engine)
        {
            cam = engine.GameObjectFactory.CreateGameObject("Camera");
            device = engine.Device;

            scene = engine.SceneFactory.CreateSceneWithSingleLight(
                engine.CameraFactory.CreateDebugCamera(cam),
                engine.LightFactory.CreateDirectionalLight());

            GameObject cube = new BaseGameObject("Cube");
            Mesh mesh = engine.Resources.GetMesh("BOX");
            Material material = engine.Resources.GetMaterial("NormalLit");

            SceneObject litCube = new SceneObject(cube, mesh, material);

            scene.AddSceneObject(litCube);
        }

        public void Update(GameTimer gameTime)
        {
            cam.Update(gameTime);
        }

        public void Draw(GameTimer gameTime)
        {
            scene.Render(gameTime);

            MipGenerationSimple2LevelsOn2DTexture();
        }

        public void Shutdown()
        {
        }

        int numOfMips = 3;

        private void MipGenerationSimple2LevelsOn2DTexture()
        {
            //Given
            Material computeShader = LoadComputeShader(device);

            Texture2DDescription emptyTextureDesc = CreateTextureDescription();
            SharpDX.Direct3D11.Texture2D emptyTexture = new SharpDX.Direct3D11.Texture2D(device, emptyTextureDesc);

            //ShaderResourceViewDescription srvDesc = CreateSRVDescription();
            //ShaderResourceView srv = new ShaderResourceView(device, emptyTexture, srvDesc);

            UnorderedAccessViewDescription uavDesc = CreateUAVDescription(0);
            UnorderedAccessView uavMip0 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            uavDesc = CreateUAVDescription(1);
            UnorderedAccessView uavMip1 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            uavDesc = CreateUAVDescription(2);
            UnorderedAccessView uavMip2 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            //When
            computeShader.SetParameterResource("gOutput", uavMip0);
            computeShader.SetParameterValue("fillColour", new Vector4(0.1f, 0.2f, 0.3f, 1.0f));
            computeShader.Apply();
            device.Dispatch(1, 64, 1);

            computeShader.SetParameterResource("gOutput", uavMip1);
            computeShader.SetParameterValue("fillColour", new Vector4(0.0f, 0.5f, 0.0f, 1.0f));
            computeShader.Apply();
            device.Dispatch(1, 32, 1);

            computeShader.SetParameterResource("gOutput", uavMip2);
            computeShader.SetParameterValue("fillColour", new Vector4(0.7f, 0.4f, 0.15f, 1.0f));
            computeShader.Apply();
            device.Dispatch(1, 16, 1);

            Texture2DDescription bufferDesc = CreateLocalBufferDesc();
            SharpDX.Direct3D11.Texture2D localbuffer = new SharpDX.Direct3D11.Texture2D(device, bufferDesc);

            device.Copy(emptyTexture, localbuffer);

            DataStream data = new DataStream(8 * 64 * 64, true, true);
            DataBox box = ((DeviceContext)device).MapSubresource(localbuffer, 0, 0, MapMode.Read, MapFlags.None, out data);

            Half4 quickTest;
            for (int i = 0; i < 64 * 64; i++)
            {
                quickTest = data.ReadHalf4();
                if (quickTest.X != 0 || quickTest.Y != 0 || quickTest.Z != 0 || quickTest.W != 0)
                {
                    FearEngine.Logger.FearLog.Log("Found something; " + quickTest.ToString());
                }
            }

            ((DeviceContext)device).UnmapSubresource(localbuffer, 0);
        }

        private static Material LoadComputeShader(SharpDX.Toolkit.Graphics.GraphicsDevice device)
        {
            MaterialLoader loader = new MaterialLoader(new SharpDXGraphicsDevice(device));
            ResourceInformation info = new MaterialResourceInformation();
            info.UpdateInformation("Filepath", "C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Shaders\\ComputeTest.fx");
            info.UpdateInformation("Technique", "FillTexture");

            return (Material)loader.Load(info);
        }

        private Texture2DDescription CreateTextureDescription()
        {
            Texture2DDescription desc = new Texture2DDescription();
            desc.Width = 64;
            desc.Height = 64;
            desc.MipLevels = numOfMips;
            desc.ArraySize = 1;
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.SampleDescription.Count = 1;
            desc.SampleDescription.Quality = 0;
            desc.Usage = ResourceUsage.Default;
            desc.BindFlags = BindFlags.UnorderedAccess | BindFlags.ShaderResource;
            desc.CpuAccessFlags = CpuAccessFlags.None;
            desc.OptionFlags = ResourceOptionFlags.None;

            return desc;
        }

        private ShaderResourceViewDescription CreateSRVDescription()
        {
            ShaderResourceViewDescription desc = new ShaderResourceViewDescription();
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.Dimension = ShaderResourceViewDimension.Texture2D;
            desc.Texture2D.MipLevels = numOfMips;
            desc.Texture2D.MostDetailedMip = 0;
            return desc;
        }

        private UnorderedAccessViewDescription CreateUAVDescription(int mipSlice)
        {
            UnorderedAccessViewDescription desc = new UnorderedAccessViewDescription();
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.Dimension = UnorderedAccessViewDimension.Texture2D;
            desc.Texture2D.MipSlice = mipSlice;
            return desc;
        }

        private Texture2DDescription CreateLocalBufferDesc()
        {
            Texture2DDescription desc = new Texture2DDescription();
            desc.Width = 64;
            desc.Height = 64;
            desc.MipLevels = numOfMips;
            desc.ArraySize = 1;
            desc.Format = SharpDX.DXGI.Format.R16G16B16A16_Float;
            desc.SampleDescription.Count = 1;
            desc.SampleDescription.Quality = 0;
            desc.Usage = ResourceUsage.Staging;
            desc.BindFlags = BindFlags.None;
            desc.CpuAccessFlags = CpuAccessFlags.Read;
            desc.OptionFlags = ResourceOptionFlags.None;

            return desc;
        }
    }
}