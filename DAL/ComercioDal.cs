using ComercioDigitalDemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ComercioDigitalDemoAPI.DAL
{
    public class ComercioDal : ComumDal
    {
        public ComercioDal(bool usarTransacao) : base(usarTransacao) { }

        public Guid IncluirProduto(Produto produto)
        {
            string sql = @"insert into Produtos (Id, Nome, Valor) OUTPUT inserted.Id
                           values (NEWID(), @nome, @valor)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("valor", produto.Valor);
            return (Guid)cmd.ExecuteScalar();
        }

        public Guid IncluirCliente(Cliente cliente)
        {
            string sql = @"insert into Clientes (Id, Nome, Email, Telefone) OUTPUT inserted.Id 
                           values (NEWID(), @nome, @email, @telefone)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("nome", cliente.Nome);
            cmd.Parameters.AddWithValue("email", cliente.Email);
            cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
            return (Guid)cmd.ExecuteScalar();
        }

        public int ObterNovoNumeroPedido()
        {
            string query = "select coalesce(max([Numero]), 0) + 1 as Numero from Pedidos";
            using SqlCommand cmd = new SqlCommand(query, Conexao);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public Guid IncluirPedido(Pedido pedido)
        {
            string sql = @"insert into Pedidos (Id, Titulo, Numero, ClienteId) OUTPUT inserted.Id 
                           values (NEWID(), @titulo, @numero, @clienteId)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("titulo", pedido.Titulo);
            cmd.Parameters.AddWithValue("numero", pedido.Numero);
            cmd.Parameters.AddWithValue("clienteId", pedido.ClienteId);
            return (Guid)cmd.ExecuteScalar();
        }

        public List<Pedido> ListarPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();

            string sql = @"select p.Id, p.Titulo, p.Numero, c.Id as ClienteId
                           from Pedidos p inner join Clientes c on p.ClienteId = c.Id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());

            foreach (DataRow row in retornoQuery.Rows)
            {
                pedidos.Add(row);
            }

            return pedidos;
        }

        public Pedido ObterPedido(Guid id)
        {
            Pedido pedido;

            string sql = "select Id, Titulo, Numero, ClienteId from Pedidos where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());
            pedido = retornoQuery.Rows[0];
            return pedido;
        }

        public void AlterarPedido(Pedido pedido)
        {
            string sql = @"update Pedidos set Titulo = @titulo where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", pedido.Id);
            cmd.Parameters.AddWithValue("titulo", pedido.Titulo);
            cmd.ExecuteNonQuery();
        }

        public void DeletarPedido(Guid id)
        {
            string sql = @"delete from Pedidos where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public Guid IncluirItemPedido(ItemPedido itemPedido)
        {
            string sql = @"insert into ItensPedido (Id, PedidoId, ProdutoId, Quantidade) OUTPUT inserted.Id
                           values (NEWID(), @pedidoId, @produtoId, @quantidade)";

            using SqlCommand cmd = new SqlCommand(sql, Conexao, Transacao);
            cmd.Parameters.AddWithValue("pedidoId", itemPedido.Pedido.Id);
            cmd.Parameters.AddWithValue("produtoId", itemPedido.Produto.Id);
            cmd.Parameters.AddWithValue("quantidade", itemPedido.Quantidade);
            return (Guid)cmd.ExecuteScalar();
        }

        public List<Produto> ListarProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            string sql = "select Id, Nome, Valor from Produtos";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());

            foreach (DataRow row in retornoQuery.Rows)
            {
                produtos.Add(row);
            }

            return produtos;
        }

        public Produto ObterProduto(Guid id)
        {
            Produto produto;

            string sql = "select Id, Nome, Valor from Produtos where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());
            produto = retornoQuery.Rows[0];
            return produto;
        }

        public void AlterarProduto(Produto produto)
        {
            string sql = @"update Produtos set Nome = @nome, Valor = @valor where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", produto.Id);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("valor", produto.Valor);
            cmd.ExecuteNonQuery();
        }
        
        public void DeletarProduto(Guid id)
        {
            string sql = @"delete from Clientes where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> cliente = new List<Cliente>();

            string sql = "select Id, Nome, Email, Telefone from Clientes";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());

            foreach (DataRow row in retornoQuery.Rows)
            {
                cliente.Add(row);
            }

            return cliente;
        }

        public Cliente ObterCliente(Guid id)
        {
            Cliente cliente;

            string sql = "select Id, Nome, Email, Telefone from Clientes where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());
            cliente = retornoQuery.Rows[0];
            return cliente;
        }

        public void AlterarCliente(Cliente cliente)
        {
            string sql = @"update Clientes set Email = @email, Telefone = @telefone where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", cliente.Id);
            cmd.Parameters.AddWithValue("email", cliente.Email);
            cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
            cmd.ExecuteNonQuery();
        }

        public void DeletarCliente(int id)
        {
            string sql = @"delete from Clientes where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public ItemPedido ObterItemPedido(Guid id)
        {
            ItemPedido itemPedido;

            string sql = @"select i.Id , p2.Nome, p2.Valor, i.Quantidade
                           from ItensPedido i inner join Pedidos p1 on i.PedidoId = p1.Id
                           inner join Produtos p2 on i.ProdutoId = p2.Id
                           where i.Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());
            itemPedido = retornoQuery.Rows[0];
            return itemPedido;
        }


        public List<Pedido> ListarPedidosPorCliente(int clienteId)
        {
            List<Pedido> pedidos = new List<Pedido>();

            string sql = @"select Id, Titulo, Numero from Pedidos where ClienteId = @clienteId";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("clienteId", clienteId);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());

            foreach (DataRow row in retornoQuery.Rows)
            {
                pedidos.Add(row);
            }

            return pedidos;
        }

        public List<ItemPedido> ListarItensPorPedido(Guid pedidoId)
        {
            List<ItemPedido> pedidos = new List<ItemPedido>();

            string sql = @"select i.Id , p2.Nome, p2.Valor, i.Quantidade
                           from ItensPedido i inner join Pedidos p1 on i.PedidoId = p1.Id
                           inner join Produtos p2 on i.ProdutoId = p2.Id
                           where p1.Id = @pedidoId";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("pedidoId", pedidoId);
            using DataTable retornoQuery = new DataTable();
            retornoQuery.Load(cmd.ExecuteReader());

            foreach (DataRow row in retornoQuery.Rows)
            {
                pedidos.Add(row);
            }

            return pedidos;
        }

        public void DeletarItemPedido(Guid id)
        {
            string sql = @"delete from ItensPedido where Id = @id";

            using SqlCommand cmd = new SqlCommand(sql, Conexao);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }
}