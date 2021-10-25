using ComercioDigitalDemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ComercioDigitalDemoAPI.DAL
{
    public class ComercioDal : ComumDal
    {
        public ComercioDal(bool usarTransacao) : base(usarTransacao) { }

        public async Task<object> IncluirProduto(Produto produto)
        {
            string sql = @"insert into Produtos (Id, Nome, Valor) OUTPUT inserted.Id
                           values (NEWID(), @nome, @valor)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("valor", produto.Valor);
            return await cmd.ExecuteScalarAsync();
        }

        public async Task<object> IncluirCliente(Cliente cliente)
        {
            string sql = @"insert into Clientes (Id, Nome, Email, Telefone) OUTPUT inserted.Id 
                           values (NEWID(), @nome, @email, @telefone)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("nome", cliente.Nome);
            cmd.Parameters.AddWithValue("email", cliente.Email);
            cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
            return await cmd.ExecuteScalarAsync();
        }

        public async Task<int> ObterNovoNumeroPedido()
        {
            string query = "select coalesce(max([Numero]), 0) + 1 as Numero from Pedidos";
            using SqlCommand cmd = new SqlCommand(query, Conexao);
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public async Task<object> IncluirPedido(Pedido pedido)
        {
            string sql = @"insert into Pedidos (Id, Titulo, Numero, ClienteId) OUTPUT inserted.Id 
                           values (NEWID(), @titulo, @numero, @clienteId)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("titulo", pedido.Titulo);
            cmd.Parameters.AddWithValue("numero", pedido.Numero);
            cmd.Parameters.AddWithValue("clienteId", pedido.ClienteId);
            return await cmd.ExecuteScalarAsync();
        }

        public async Task<List<Pedido>> ListarPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();

            string sql = @"select p.Id, p.Titulo, p.Numero, c.Id as ClienteId, c.Nome as NomeCliente
                           from Pedidos p inner join Clientes c on p.ClienteId = c.Id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());

            foreach (DataRow row in retornoQuery.Rows)
            {
                pedidos.Add(row);
            }

            return pedidos;
        }

        public async Task<Pedido> ObterPedido(Guid id)
        {
            Pedido pedido;

            string sql = "select Id, Titulo, Numero, ClienteId from Pedidos where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());
            pedido = retornoQuery.Rows[0];
            return pedido;
        }

        public async Task<int> AlterarPedido(Pedido pedido)
        {
            string sql = @"update Pedidos set Titulo = @titulo where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", pedido.Id);
            cmd.Parameters.AddWithValue("titulo", pedido.Titulo);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeletarPedido(Guid id)
        {
            string sql = @"delete from Pedidos where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<object> IncluirItemPedido(ItemPedido itemPedido)
        {
            string sql = @"insert into ItensPedido (Id, PedidoId, ProdutoId, Quantidade) OUTPUT inserted.Id
                           values (NEWID(), @pedidoId, @produtoId, @quantidade)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao, Transacao);
            cmd.Parameters.AddWithValue("pedidoId", itemPedido.PedidoId);
            cmd.Parameters.AddWithValue("produtoId", itemPedido.ProdutoId);
            cmd.Parameters.AddWithValue("quantidade", itemPedido.Quantidade);
            return await cmd.ExecuteScalarAsync();
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            string sql = "select Id, Nome, Valor from Produtos";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());

            foreach (DataRow row in retornoQuery.Rows)
            {
                produtos.Add(row);
            }

            return produtos;
        }

        public async Task<Produto> ObterProduto(Guid id)
        {
            Produto produto;

            string sql = "select Id, Nome, Valor from Produtos where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());
            produto = retornoQuery.Rows[0];
            return produto;
        }

        public async Task<int> AlterarProduto(Produto produto)
        {
            string sql = @"update Produtos set Nome = @nome, Valor = @valor where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", produto.Id);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("valor", produto.Valor);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeletarProduto(Guid id)
        {
            string sql = @"delete from Produtos where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            List<Cliente> cliente = new List<Cliente>();

            string sql = "select Id, Nome, Email, Telefone from Clientes";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());

            foreach (DataRow row in retornoQuery.Rows)
            {
                cliente.Add(row);
            }

            return cliente;
        }

        public async Task<Cliente> ObterCliente(Guid id)
        {
            Cliente cliente;

            string sql = "select Id, Nome, Email, Telefone from Clientes where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());
            cliente = retornoQuery.Rows[0];
            return cliente;
        }

        public async Task<int> AlterarCliente(Cliente cliente)
        {
            string sql = @"update Clientes set Nome = @nome, Email = @email, Telefone = @telefone where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("nome", cliente.Nome);
            cmd.Parameters.AddWithValue("id", cliente.Id);
            cmd.Parameters.AddWithValue("email", cliente.Email);
            cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeletarCliente(Guid id)
        {
            string sql = @"delete from Clientes where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<ItemPedido> ObterItemPedido(Guid id)
        {
            ItemPedido itemPedido;

            string sql = @"select i.Id , p2.Nome, p2.Valor, i.PedidoId, i.ProdutoId, p1.Titulo as TituloPedido, p2.Nome as NomeProduto, i.Quantidade
                           from ItensPedido i inner join Pedidos p1 on i.PedidoId = p1.Id
                           inner join Produtos p2 on i.ProdutoId = p2.Id
                           where i.Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());
            itemPedido = retornoQuery.Rows[0];
            return itemPedido;
        }

        public async Task<List<Pedido>> ListarPedidosPorCliente(int clienteId)
        {
            List<Pedido> pedidos = new List<Pedido>();

            string sql = @"select Id, Titulo, Numero from Pedidos where ClienteId = @clienteId";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("clienteId", clienteId);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());

            foreach (DataRow row in retornoQuery.Rows)
            {
                pedidos.Add(row);
            }

            return pedidos;
        }

        public async Task<List<ItemPedido>> ListarItensPorPedido(Guid pedidoId)
        {
            List<ItemPedido> pedidos = new List<ItemPedido>();

            string sql = @"select i.Id, p2.Valor, i.PedidoId, i.ProdutoId, p1.Titulo as TituloPedido, p2.Nome as NomeProduto, i.Quantidade
                           from ItensPedido i inner join Pedidos p1 on i.PedidoId = p1.Id
                           inner join Produtos p2 on i.ProdutoId = p2.Id
                           where p1.Id = @pedidoId";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("pedidoId", pedidoId);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(await cmd.ExecuteReaderAsync());

            foreach (DataRow row in retornoQuery.Rows)
            {
                pedidos.Add(row);
            }

            return pedidos;
        }

        public async Task<int> DeletarItemPedido(Guid id)
        {
            string sql = @"delete from ItensPedido where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            return await cmd.ExecuteNonQueryAsync();
        }
    }
}