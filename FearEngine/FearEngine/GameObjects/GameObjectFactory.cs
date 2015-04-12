using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.GameObjects
{
    public interface GameObjectFactory
    {
        BaseGameObject CreateGameObject(string name);
    }
}
