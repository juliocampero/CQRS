namespace Fundacion.Diplomado.Domain
{
    public interface ISendCommands
    {
        void Send<T>(T command) where T : ICommand;
    }
}