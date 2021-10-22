using ComercioDigitalDemoAPI.ViewModels;
using System;
using System.Data;

namespace ComercioDigitalDemoAPI.Models
{
    public class Produto : ProdutoViewModel
    {
        public Guid Id { get; set; }

        public static implicit operator Produto(DataRow row)
        {
            Produto produto = new Produto();
            produto.Id = (Guid)row["Id"];
            produto.Nome = Convert.ToString(row["Nome"]);
            produto.Valor = Convert.ToDecimal(row["Valor"]);
            return produto;
        }
    }
}