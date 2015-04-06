using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
