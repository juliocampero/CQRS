namespace Fundacion.Diplomado.Domain
{
    public interface IEvent : IMessage
    {
        // public int Version; // no time for Event Sourcing here.
    }
}