using FearEngine.GameObjects;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;

namespace FearEngine.Scene
{
    public struct SceneObject
    {
        private GameObject obj;
        private Mesh mesh;
        private Material material;

        public SceneObject(GameObject o, Mesh m, Material mat)
        {
            obj = o;
            mesh = m;
            material = mat;
        }

        public GameObject GameObject { get{ return obj; } }
        public Mesh Mesh { get { return mesh; } }
        public Material Material { get { return material; } }
    }
}
