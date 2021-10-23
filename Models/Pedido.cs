using ComercioDigitalDemoAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioDigitalDemoAPI.Models
{
    public class Pedido : PedidoViewModel
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }

        public static implicit operator Pedido(DataRow row)
        {
            Pedido pedido = new Pedido();
            pedido.Id = (Guid)row["Id"];
            pedido.Titulo = Convert.ToString(row["Titulo"]);
            pedido.Numero = Convert.ToInt32(row["Numero"]);
            pedido.ClienteId = (Guid)row["ClienteId"];
            return pedido;
        }
    }
}