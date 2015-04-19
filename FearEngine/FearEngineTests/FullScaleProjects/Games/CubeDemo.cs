using FearEngine;
using FearEngine.Resources.Meshes;
using FearEngine.Scenes;
using FearEngine.GameObjects;
using FearEngine.Resources.Materials;
using FearEngine.Timer;
using SharpDX.Direct3D11;
using SharpDX;
using FearEngine.Resources.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using SharpDX.Direct3D;

namespace FearEngineTests.FullScaleProjects.Games
{
    public class CubeDemo : FearEngine.FearGame
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
            scene.Render( gameTime );

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

            ShaderResourceViewDescription srvDesc = CreateSRVDescription();
            ShaderResourceView srv = new ShaderResourceView(device, emptyTexture, srvDesc);

            UnorderedAccessViewDescription uavDesc = CreateUAVDescription(0);
            UnorderedAccessView uavMip0 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            uavDesc = CreateUAVDescription(1);
            UnorderedAccessView uavMip1 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            uavDesc = CreateUAVDescription(2);
            UnorderedAccessView uavMip2 = new UnorderedAccessView(device, emptyTexture, uavDesc);

            BufferDescription bufferDesc = CreateLocalBufferDesc();
            SharpDX.Direct3D11.Buffer localbuffer = new SharpDX.Direct3D11.Buffer(device, bufferDesc);

            int mipSize = 0;
            int subIndex = emptyTexture.CalculateSubResourceIndex(1, 0, out mipSize);
            //When
            computeShader.SetParameterResource("gOutput", uavMip0);
            computeShader.SetParameterValue("fillColour", new Vector4(0.1f, 0.2f, 0.3f, 1.0f));

            computeShader.Apply();

            //IS THIS ACTUALLY DOING ANYTHING AT ALL? Do we need to set up more of the pipeline before this will actually work??
            device.Dispatch(1, 32, 1);

            device.Copy(emptyTexture, 1, localbuffer, 0);

            DataStream data = new DataStream(8 * 32 * 32, true, true);
            ((DeviceContext)device).MapSubresource(localbuffer, MapMode.Read, MapFlags.None, out data);


            Half4 quickTest;
            for (int i = 0; i < 32 * 32; i++)
            {
                quickTest = data.ReadHalf4();
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
            Texture2DDescription desc;
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

        private BufferDescription CreateLocalBufferDesc()
        {
            BufferDescription desc = new BufferDescription();
            desc.Usage = ResourceUsage.Staging;
            desc.BindFlags = BindFlags.None;
            desc.SizeInBytes = 8 * 32 * 32;
            desc.CpuAccessFlags = CpuAccessFlags.Read;
            desc.StructureByteStride = 8;

            return desc;
        }
    }
}
