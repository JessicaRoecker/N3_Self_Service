namespace N3_Self_Service.Domain.Model
{
    public class ProdutoModel
    {
        public int CodProduto { get; set; }
        public string NomeProduto { get; set; }
        public int QtdeProduto { get; set; }
        public CategoriaModel Categoria { get; set; } = new CategoriaModel();



    }
}
