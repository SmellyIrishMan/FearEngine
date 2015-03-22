using FearEngine.Resources.Managment.Loaders;
using System.Collections.Generic;

namespace FearEngine.Resources.ResourceFiles
{
    public interface ResourceStorage
    {
        ResourceInformation GetInformationByName(string name);

        void StoreInformation(ResourceInformation information);
    }
}
