using ComercioDigitalDemoAPI.DAL;
using ComercioDigitalDemoAPI.Models;
using ComercioDigitalDemoAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ComercioDigitalDemoAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ComercioDigitalController : ControllerBase
    {
        private readonly ILogger<ComercioDigitalController> _logger;

        public ComercioDigitalController(ILogger<ComercioDigitalController> logger)
        {
            _logger = logger;
        }

        #region [ Produtos ]

        [HttpPost]
        [Consumes("application/json")]
        [Route("produtos")]
        public IActionResult IncluirProduto([FromBody] ProdutoViewModel model)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);

                Produto produto = new Produto
                {
                    Nome = model.Nome,
                    Valor = Math.Truncate(model.Valor)
                };

                produto.Id = comercioDal.IncluirProduto(produto);
                return Created($"v1/produtos/{produto.Id}", produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("produtos")]
        public IActionResult ListarProdutos()
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<Produto> produtos = comercioDal.ListarProdutos();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("produtos/{id}")]
        public IActionResult ObterProduto([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Produto produto = comercioDal.ObterProduto(id);
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [Route("produtos/{id}")]
        public IActionResult AlterarProduto([FromBody] ProdutoViewModel model, [FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Produto produto = comercioDal.ObterProduto(id);

                if (produto == null) return NotFound();

                produto.Nome = model.Nome;
                produto.Valor = model.Valor;
                comercioDal.AlterarProduto(produto);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("produtos/{id}")]
        public IActionResult DeletarProduto([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Produto produto = comercioDal.ObterProduto(id);

                if (produto == null) return NotFound();

                comercioDal.DeletarProduto(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        #endregion

        #region [ Cliente ]

        [HttpPost]
        [Consumes("application/json")]
        [Route("clientes")]
        public IActionResult IncluirCliente([FromBody] ClienteViewModel model)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);

                Cliente cliente = new Cliente
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone
                };

                cliente.Id = comercioDal.IncluirCliente(cliente);
                return Created($"v1/clientes/{cliente.Id}", cliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("clientes")]
        public IActionResult ListarClientes()
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<Cliente> clientes = comercioDal.ListarClientes();
                return Ok(clientes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("clientes/{id}")]
        public IActionResult ObterCliente([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Cliente cliente = comercioDal.ObterCliente(id);
                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [Route("clientes/{id}")]
        public IActionResult AlterarCliente([FromBody] ClienteViewModel model, [FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Cliente cliente = comercioDal.ObterCliente(id);

                if (cliente == null) return NotFound();

                cliente.Email = model.Email;
                cliente.Telefone = model.Telefone;
                comercioDal.AlterarCliente(cliente);
                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("clientes/{id}")]
        public IActionResult DeletarCliente([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Cliente cliente = comercioDal.ObterCliente(id);

                if (cliente == null) return NotFound();

                comercioDal.DeletarProduto(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        #endregion

        #region [ Pedido ]

        [HttpPost]
        [Consumes("application/json")]
        [Route("pedidos")]
        public IActionResult IncluirPedido([FromBody] PedidoViewModel model)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);

                Pedido pedido = new Pedido
                {
                    Titulo = model.Titulo,
                    Numero = comercioDal.ObterNovoNumeroPedido(),
                    ClienteId = model.ClienteId
                };

                pedido.Id = comercioDal.IncluirPedido(pedido);
                return Created($"v1/pedidos/{pedido.Id}", pedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("pedidos")]
        public IActionResult ListarPedidos()
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<Pedido> pedidos = comercioDal.ListarPedidos();
                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("pedidos/{id}")]
        public IActionResult ObterPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Pedido pedido = comercioDal.ObterPedido(id);
                return Ok(pedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [Route("pedidos/{id}")]
        public IActionResult AlterarPedido([FromBody] PedidoViewModel model, [FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Pedido pedido = comercioDal.ObterPedido(id);

                if (pedido == null) return NotFound();

                pedido.Titulo = model.Titulo;
                pedido.Numero = model.Numero;
                comercioDal.AlterarPedido(pedido);
                return Ok(pedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("pedidos/{id}")]
        public IActionResult DeletarPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Pedido pedido = comercioDal.ObterPedido(id);

                if (pedido == null) return NotFound();

                comercioDal.DeletarPedido(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        #endregion

        #region [ ItemPedido ]

        [HttpPost]
        [Consumes("application/json")]
        [Route("itens")]
        public IActionResult IncluirItensPedido([FromBody] List<ItemPedido> model)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(true);

                List<ItemPedido> itensPedido = new List<ItemPedido>();

                model.ForEach(m => itensPedido.Add(new ItemPedido
                {
                    Pedido = new Pedido { Id = m.Pedido.Id },
                    Produto = new Produto { Id = m.Produto.Id },
                    Quantidade = m.Quantidade,
                }));

                itensPedido.ForEach(i => i.Id = comercioDal.IncluirItemPedido(i));
                return Ok(itensPedido);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("itens/{id}")]
        public IActionResult ObterItemPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                ItemPedido itemPedido = comercioDal.ObterItemPedido(id);
                return Ok(itemPedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("itens/pedido/{pedidoId}")]
        public IActionResult ListarItensPorPedido([FromRoute] Guid pedidoId)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<ItemPedido> pedidos = comercioDal.ListarItensPorPedido(pedidoId);
                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("itens/{id}")]
        public IActionResult DeletarItemPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                ItemPedido itemPedido = comercioDal.ObterItemPedido(id);

                if (itemPedido == null) return NotFound();

                comercioDal.DeletarItemPedido(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        #endregion
    }
}