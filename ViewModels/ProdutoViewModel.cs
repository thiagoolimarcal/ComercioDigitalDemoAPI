using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioDigitalDemoAPI.ViewModels
{
    public class ProdutoViewModel
    {
        public string Nome { get; set; }

        public decimal Valor { get; set; }
    }
}