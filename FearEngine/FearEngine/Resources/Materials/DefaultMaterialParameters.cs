using System.Collections.Generic;

namespace FearEngine.Resources.Materials
{
    public class DefaultMaterialParameters
    {
        public enum Param
        {
            World,
            WorldViewProj,
            WorldInvTranspose,
            EyeW,
            DirLight,
        }

        public static Dictionary<Param, string> ParamToName = new Dictionary<Param, string>()
        {
            {Param.World, "gWorld"},
            {Param.WorldViewProj, "gWorldViewProj"},
            {Param.WorldInvTranspose, "gWorldInvTranspose"},
            {Param.EyeW, "gEyeW"},
            {Param.DirLight, "gDirLight"},
        };
    }
}
