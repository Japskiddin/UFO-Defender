namespace UFO_Defense.Scripts.Managers
{
    public interface IGameManager
    {
        ManagerStatus Status { get; }
        void Startup();
    }
}