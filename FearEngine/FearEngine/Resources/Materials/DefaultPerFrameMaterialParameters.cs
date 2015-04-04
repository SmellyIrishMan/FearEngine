using FearEngine.Lighting;
using SharpDX;
using System.Collections.Generic;

namespace FearEngine.Resources
{
    public struct DefaultPerFrameMaterialParameters
    {
        private Dictionary<string, Vector3> vec3List;
        private Dictionary<string, DirectionalLight> lights;

        public DefaultPerFrameMaterialParameters(
            Vector3 eyePosWorld,
            DirectionalLight light)
        {
            vec3List = new Dictionary<string, Vector3>();
            vec3List.Add("gEyeW", eyePosWorld);

            lights = new Dictionary<string, DirectionalLight>();
            lights.Add("gDirLight", light);
        }

        public void ApplyToMaterial(Material material)
        {
            foreach (string key in vec3List.Keys)
            {
                material.SetParameterValue(key, vec3List[key]);
            }

            foreach (string key in lights.Keys)
            {
                material.SetParameterValue(key, lights[key]);
            }
        }
    }
}
