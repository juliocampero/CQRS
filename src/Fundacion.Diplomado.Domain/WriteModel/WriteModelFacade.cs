namespace Fundacion.Diplomado.Domain.WriteModel
{
    public class WriteModelFacade : IHandleCommand<PedidoCommand>
    {
        public IEntregarPizzas PedidoStore { get; }

        public WriteModelFacade(IEntregarPizzas bookingStore)
        {
            this.PedidoStore = bookingStore;
        }

        public void Handle(PedidoCommand command)
        {
            this.PedidoStore.EntregarLaPizza(command);
        }
    }

    public interface IHandleCommand<T>
    {
        void Handle(T command);
    }
}