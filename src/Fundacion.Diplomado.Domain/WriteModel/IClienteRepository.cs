namespace Fundacion.Diplomado.Domain.WriteModel
{
    // TODO: find a better name following Vaughn VERNON's reco (I do something...) or keep the reference to repository to help people understanding?
    public interface IClienteRepository
    {
        bool YaEsCliente(string indetificadorCliente);
        void CrearClient(string indetificadorCliente);
    }
}