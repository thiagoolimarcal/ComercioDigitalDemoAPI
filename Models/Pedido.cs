﻿using ComercioDigitalDemoAPI.ViewModels;
using System;
using System.Data;

namespace ComercioDigitalDemoAPI.Models
{
    public class Pedido : PedidoViewModel
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public string NomeCliente { get; set; }

        public static implicit operator Pedido(DataRow row)
        {
            Pedido pedido = new Pedido();
            pedido.Id = (Guid)row["Id"];
            pedido.Titulo = Convert.ToString(row["Titulo"]);
            pedido.Numero = Convert.ToInt32(row["Numero"]);
            pedido.ClienteId = (Guid)row["ClienteId"];
            pedido.NomeCliente = row.Table.Columns.Contains("NomeCliente") ? Convert.ToString(row["NomeCliente"]) : string.Empty;
            return pedido;
        }
    }
}