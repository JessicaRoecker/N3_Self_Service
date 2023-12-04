using System.Data;
using System.Data.Common;
using N3_Self_Service.Domain.Model;
using N3_Self_Service.Infrastructure.Data;
using N3_Self_Service.Infrastructure.Repository.Interface;

namespace N3_Self_Service.Infrastructure.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDatabaseConnection _connection;

        public ProdutoRepository(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task RegistrarProdutoAsync(ProdutoModel produto)
        {
            if (produto.Categoria == null || produto.Categoria.IdCategoria <= 0)
            {
                
                return;
            }

            await _connection.ExecuteAsync(
                "INSERT INTO Produtos (NomeProduto, QtdeProduto, IdCategoria) VALUES (@NomeProduto, @QtdeProduto, @IdCategoria)",
                new
                {
                     produto.NomeProduto,
                     produto.QtdeProduto,
                     produto.Categoria.IdCategoria
                });
        }


        public async Task<ProdutoModel> ObterProdutoPorIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<ProdutoModel>(
                "SELECT * FROM Produtos WHERE CodProduto = @Id",
                new { Id = id });
        }

        public async Task DeletarProdutoAsync(int id)
        {
            await _connection.ExecuteAsync(
                "DELETE FROM Produtos WHERE CodProduto = @Id",
                new { Id = id });
        }

        public async Task AtualizarProdutoAsync(int id, ProdutoModel produto)
        {
            produto.CodProduto = id;
            await _connection.ExecuteAsync(
                "UPDATE Produtos SET NomeProduto = @NomeProduto, QtdeProduto = @QtdeProduto, IdCategoria = @IdCategoria WHERE CodProduto = @CodProduto",
                produto);
        }

        public async Task RegistrarPedidoAsync(PedidoModel pedido)
        {
            var produto = await _connection.QueryFirstOrDefaultAsync<ProdutoModel>(
                "SELECT * FROM Produtos WHERE CodProduto = @CodProduto",
                new { pedido.CodProduto });

            if (produto == null)
            {

                return;
            }

            int qtdeRegistrar;

            if (produto.QtdeProduto <= 3)
            {
                qtdeRegistrar = 4;
            }
            else if (produto.QtdeProduto > 3 && produto.QtdeProduto < 7)
            {
                qtdeRegistrar = 3;
            }
            else
            {
                // Não registre o pedido se a quantidade não atender aos critérios
                return;
            }

            var novoPedido = new PedidoModel
            {
                CodProduto = pedido.CodProduto,
                QtdePedido = qtdeRegistrar
            };

            // Execute a query Dapper para inserir o novo pedido
            await _connection.ExecuteAsync(
                "INSERT INTO Pedidos (CodProduto, QtdePedido) VALUES (@CodProduto, @QtdePedido)",
                novoPedido);

            // Execute a query Dapper para atualizar a quantidade do produto
            await _connection.ExecuteAsync(
                "UPDATE Produtos SET QtdeProduto = QtdeProduto - @QtdeRegistrar WHERE CodProduto = @CodProduto",
                new { QtdeRegistrar = qtdeRegistrar, pedido.CodProduto });
        }

        public async Task<IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int idCategoria)
        {
            // Execute a query Dapper para obter produtos por categoria
            return await _connection.QueryAsync<ProdutoModel>(
                "SELECT * FROM Produtos WHERE IdCategoria = @IdCategoria",
                new { IdCategoria = idCategoria });
        }

        public async Task<IEnumerable<ProdutoModel>> GetProdutosPorQuantidadePedidoAsync(int quantidade)
        {
            // Execute a query Dapper para obter produtos por quantidade de pedido
            return await _connection.QueryAsync<ProdutoModel>(
                "SELECT p.* FROM Produtos p " +
                "JOIN Pedidos pe ON p.CodProduto = pe.CodProduto " +
                "GROUP BY p.CodProduto " +
                "HAVING SUM(pe.QtdePedido) = @Quantidade",
                new { Quantidade = quantidade });
        }

    }


}
