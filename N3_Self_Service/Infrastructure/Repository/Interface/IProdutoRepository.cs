using N3_Self_Service.Domain.Model;

namespace N3_Self_Service.Infrastructure.Repository.Interface
{
    public interface IProdutoRepository
    {
        Task AtualizarProdutoAsync(int id, ProdutoModel produto);
        Task<IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int idCategoria);
        Task<IEnumerable<ProdutoModel>> GetProdutosPorQuantidadePedidoAsync(int quantidade);
        Task<ProdutoModel> ObterProdutoPorIdAsync(int id);
        Task RegistrarPedidoAsync(PedidoModel pedido);
        Task RegistrarProdutoAsync(ProdutoModel produto);
        Task DeletarProdutoAsync(int id);
    }
}