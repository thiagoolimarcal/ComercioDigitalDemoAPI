using ComercioDigitalDemoAPI.ViewModels;
using System;
using System.Data;

namespace ComercioDigitalDemoAPI.Models
{
    public class Cliente : ClienteViewModel
    {
        public Guid Id { get; set; }

        public static implicit operator Cliente(DataRow row)
        {
            Cliente cliente = new Cliente();
            cliente.Id = (Guid)row["Id"];
            cliente.Nome = Convert.ToString(row["Nome"]);
            cliente.Email = Convert.ToString(row["Email"]);
            cliente.Telefone = Convert.ToString(row["Telefone"]);
            return cliente;
        }
    }
}