using System;

namespace ComercioDigitalDemoAPI.ViewModels
{
    public class ItemPedidoViewModel
    {
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}