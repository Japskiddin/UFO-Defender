public interface IGameManager
{
    // Перечисление, которое нужно обрабатывать.
    ManagerStatus Status { get; }
    void Startup();
}