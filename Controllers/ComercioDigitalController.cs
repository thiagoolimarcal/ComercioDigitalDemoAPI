using ComercioDigitalDemoAPI.DAL;
using ComercioDigitalDemoAPI.Models;
using ComercioDigitalDemoAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> IncluirProduto([FromBody] ProdutoViewModel model)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);

                Produto produto = new Produto
                {
                    Nome = model.Nome,
                    Valor = Math.Truncate(model.Valor)
                };

                var id = await comercioDal.IncluirProduto(produto);
                produto.Id = (Guid)id;
                return Created($"v1/produtos/{produto.Id}", produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> ListarProdutos()
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<Produto> produtos = await comercioDal.ListarProdutos();
                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("produtos/{id}")]
        public async Task<IActionResult> ObterProduto([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                var produto = await comercioDal.ObterProduto(id);
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
        public async Task<IActionResult> AlterarProduto([FromBody] ProdutoViewModel model, [FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Produto produto = await comercioDal.ObterProduto(id);

                if (produto == null) return NotFound();

                produto.Nome = model.Nome;
                produto.Valor = model.Valor;
                await comercioDal.AlterarProduto(produto);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("produtos/{id}")]
        public async Task<IActionResult> DeletarProduto([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Produto produto = await comercioDal.ObterProduto(id);

                if (produto == null) return NotFound();

                await comercioDal.DeletarProduto(id);
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
        public async Task<IActionResult> IncluirCliente([FromBody] ClienteViewModel model)
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

                var id = await comercioDal.IncluirCliente(cliente);
                cliente.Id = (Guid)id;
                return Created($"v1/clientes/{cliente.Id}", cliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("clientes")]
        public async Task<IActionResult> ListarClientes()
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<Cliente> clientes = await comercioDal.ListarClientes();
                return Ok(clientes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("clientes/{id}")]
        public async Task<IActionResult> ObterCliente([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Cliente cliente = await comercioDal.ObterCliente(id);
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
        public async Task<IActionResult> AlterarCliente([FromBody] ClienteViewModel model, [FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Cliente cliente = await comercioDal.ObterCliente(id);

                if (cliente == null) return NotFound();

                cliente.Nome = model.Nome;
                cliente.Email = model.Email;
                cliente.Telefone = model.Telefone;
                await comercioDal.AlterarCliente(cliente);
                return Ok(cliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("clientes/{id}")]
        public async Task<IActionResult> DeletarCliente([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Cliente cliente = await comercioDal.ObterCliente(id);

                if (cliente == null) return NotFound();

                await comercioDal.DeletarCliente(id);
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
        public async Task<IActionResult> IncluirPedido([FromBody] PedidoViewModel model)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);

                Pedido pedido = new Pedido
                {
                    Titulo = model.Titulo,
                    Numero = await comercioDal.ObterNovoNumeroPedido(),
                    ClienteId = model.ClienteId
                };

                var id = await comercioDal.IncluirPedido(pedido);
                pedido.Id = (Guid)id;
                return Created($"v1/pedidos/{pedido.Id}", pedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("pedidos")]
        public async Task<IActionResult> ListarPedidos()
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<Pedido> pedidos = await comercioDal.ListarPedidos();
                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("pedidos/{id}")]
        public async Task<IActionResult> ObterPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Pedido pedido = await comercioDal.ObterPedido(id);
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
        public async Task<IActionResult> AlterarPedido([FromBody] string titulo, [FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Pedido pedido = await comercioDal.ObterPedido(id);

                if (pedido == null) return NotFound();

                pedido.Titulo = titulo;
                await comercioDal.AlterarPedido(pedido);
                return Ok(pedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("pedidos/{id}")]
        public async Task<IActionResult> DeletarPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                Pedido pedido = await comercioDal.ObterPedido(id);

                if (pedido == null) return NotFound();

                await comercioDal.DeletarPedido(id);
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
        public async Task<IActionResult> IncluirItensPedido([FromBody] ItemPedidoViewModel model)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);

                ItemPedido itemPedido = new ItemPedido
                {
                    PedidoId = model.PedidoId,
                    ProdutoId = model.ProdutoId,
                    Quantidade = model.Quantidade,
                };

                var id = await comercioDal.IncluirItemPedido(itemPedido);
                itemPedido.Id = (Guid)id;
                return Ok(itemPedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("itens/{id}")]
        public async Task<IActionResult> ObterItemPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                ItemPedido itemPedido = await comercioDal.ObterItemPedido(id);
                return Ok(itemPedido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("itens/pedido/{pedidoId}")]
        public async Task<IActionResult> ListarItensPorPedido([FromRoute] Guid pedidoId)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                List<ItemPedido> pedidos = await comercioDal.ListarItensPorPedido(pedidoId);
                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpDelete]
        [Route("itens/{id}")]
        public async Task<IActionResult> DeletarItemPedido([FromRoute] Guid id)
        {
            try
            {
                using ComercioDal comercioDal = new ComercioDal(false);
                ItemPedido itemPedido = await comercioDal.ObterItemPedido(id);

                if (itemPedido == null) return NotFound();

                await comercioDal.DeletarItemPedido(id);
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