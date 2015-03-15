using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Resources.Managment
{
    public interface Resource
    {
        bool IsLoaded();

        void Dispose();
    }
}
