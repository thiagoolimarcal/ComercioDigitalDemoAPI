using ComercioDigitalDemoAPI.ViewModels;
using System;
using System.Data;

namespace ComercioDigitalDemoAPI.Models
{
    public class ItemPedido : ItemPedidoViewModel
    {
        public Guid Id { get; set; }

        public static implicit operator ItemPedido(DataRow row)
        {
            ItemPedido itemPedido = new ItemPedido();
            itemPedido.Id = (Guid)row["Id"];
            itemPedido.PedidoId = (Guid)row["PedidoId"];
            itemPedido.ProdutoId = (Guid)row["ProdutoId"];

            //itemPedido.Produto = new Produto
            //{
            //    Nome = Convert.ToString(row["Nome"]),
            //    Valor = Convert.ToDecimal(row["Valor"])
            //};

            itemPedido.Quantidade = Convert.ToInt32(row["Quantidade"]);
            return itemPedido;
        }
    }
}