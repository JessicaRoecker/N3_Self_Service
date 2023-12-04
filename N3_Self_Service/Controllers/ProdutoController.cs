using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N3_Self_Service.Domain.Model;
using N3_Self_Service.Infrastructure.Repository;
using N3_Self_Service.Infrastructure.Repository.Interface;

namespace N3_Self_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Usuario model)
        {
            var usuario = UsuarioRepositorio.Get(model.Nome, model.Senha);
            if(usuario == null)
            {
                return NotFound(new {message = "Usuario ou senha invalidos"});
            }
             
            var token = TokenService.GerarToken(usuario);
            usuario.Senha = "";
            return Ok( new
            {
                usuario = usuario,
                token = token
            });
        }



        [HttpPost("registrar-produto")]
        public async Task<IActionResult> RegistrarProduto(ProdutoModel produto)
        {
            // Lógica assíncrona para registrar um novo produto
            await _produtoRepository.RegistrarProdutoAsync(produto);

            return Ok();
        }

        [HttpGet("Obter Produdo por Id")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            // Lógica assíncrona para obter um produto por ID
            var produto = await _produtoRepository.ObterProdutoPorIdAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPut("Atualizar Produto")]
        [Authorize(Roles = "funcionario, gerente")]
        public async Task<IActionResult> AtualizarProduto(int id, ProdutoModel produto)
        {
            // Lógica assíncrona para atualizar um produto
            await _produtoRepository.AtualizarProdutoAsync(id, produto);
            return Ok();
        }


        [HttpDelete("Deletar Produto")]
        [Authorize(Roles = "gerente")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            // Lógica assíncrona para deletar um produto
            await _produtoRepository.DeletarProdutoAsync(id);
            return Ok();
        }

        [HttpPost("Registrar Pedido")]
        public async Task<IActionResult> RegistrarPedido(PedidoModel pedido)
        {
            await _produtoRepository.RegistrarPedidoAsync(pedido);
            return Ok();
        }



        [HttpGet("Obter Produto por Categoria")]
        public async Task<IActionResult> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = await _produtoRepository.GetProdutosPorCategoriaAsync(idCategoria);
            return Ok(produtos);
        }

        [HttpGet("Obter produtos por quantidade")]
        public async Task<IActionResult> GetProdutosPorQuantidadePedido(int quantidade)
        {
            var produtos = await _produtoRepository.GetProdutosPorQuantidadePedidoAsync(quantidade);
            return Ok(produtos);
        }
    }

}
