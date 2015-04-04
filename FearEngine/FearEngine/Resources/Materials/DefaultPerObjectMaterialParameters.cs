using FearEngine.Lighting;
using SharpDX;
using System.Collections.Generic;

namespace FearEngine.Resources
{
    public struct DefaultPerObjectMaterialParameters
    {
        private Dictionary<string, Matrix> matList;

        public DefaultPerObjectMaterialParameters(
            Matrix world,
            Matrix worldInvTrans,
            Matrix WorldViewProjection)
        {
            matList = new Dictionary<string, Matrix>();
            matList.Add("gWorld", world);
            matList.Add("gWorldInvTranspose", worldInvTrans);
            matList.Add("gWorldViewProj", WorldViewProjection);
        }

        public void ApplyToMaterial(Material material)
        {
            foreach (string key in matList.Keys)
            {
                material.SetParameterValue(key, matList[key]);
            }
        }
    }
}
