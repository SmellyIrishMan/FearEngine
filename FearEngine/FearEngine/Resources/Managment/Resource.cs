namespace FearEngine.Resources.Management
{
    public interface Resource
    {
        bool IsLoaded();

        void Dispose();
    }
}
