namespace FearEngine.GameObjects
{
    public interface GameObjectFactory
    {
        BaseGameObject CreateGameObject(string name);
    }
}
