using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N3_Self_Service.Domain.Model;
using N3_Self_Service.Infrastructure.Repository.Interface;

namespace N3_Self_Service.Application.Controller
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost("registrar-produto")]
        public async Task<IActionResult> RegistrarProduto(ProdutoModel produto)
        {
            // Lógica assíncrona para registrar um novo produto
            // ...
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            await _produtoRepository.ObterProdutoPorIdAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, ProdutoModel produto)
        {
           await _produtoRepository.AtualizarProdutoAsync(id, produto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            await _produtoRepository.DeletarProdutoAsync(id);
            return Ok();
        }

        [HttpPost("registrar-pedido")]
        [Authorize]
        public async Task<IActionResult> RegistrarPedido(PedidoModel pedido)
        {
            await _produtoRepository.RegistrarPedidoAsync(pedido);
            return Ok();
        }

        [HttpGet("categoria/{idCategoria}")]
        public async Task<IActionResult> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = await _produtoRepository.GetProdutosPorCategoriaAsync(idCategoria);
            return Ok(produtos);
        }

        [HttpGet("quantidade-pedido/{quantidade}")]
        public async Task<IActionResult> GetProdutosPorQuantidadePedido(int quantidade)
        {
            var produtos = await _produtoRepository.GetProdutosPorQuantidadePedidoAsync(quantidade);
            return Ok(produtos);
        }
    }

}
