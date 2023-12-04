using N3_Self_Service.Domain.Model;

namespace N3_Self_Service.Infrastructure.Repository
{
    public static class UsuarioRepositorio
    {
        public static Usuario Get(string nomeUsuario, string senha)
        {
            var usuarios = new List<Usuario>();
            {
                usuarios.Add(new Usuario { Id = 1, Nome = "jessica", Senha = "jessica", Funcao = "gerente" });
                usuarios.Add(new Usuario { Id = 2, Nome = "roecker", Senha = "roecker", Funcao = "funcionario" });

                var usuarioEncontrado = usuarios
                    .Where(x => x.Nome.ToLower() == nomeUsuario.ToLower() && x.Senha == senha)
                    .FirstOrDefault();

                return usuarioEncontrado;
            }
        }

    }
}
