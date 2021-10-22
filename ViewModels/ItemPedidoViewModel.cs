using ComercioDigitalDemoAPI.Models;

namespace ComercioDigitalDemoAPI.ViewModels
{
    public abstract class ItemPedidoViewModel
    {
        public Produto Produto { get; set; }
        public Pedido Pedido { get; set; }
        public int Quantidade { get; set; }
    }
}