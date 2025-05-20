using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Suco de Laranja','Suco de laranha 200ml',10.50, 'sucodelaranja.jpg',50,now(),1)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Lanche de frango','Lanche de frango com salada',22.50, 'lanchefrango.jpg',10,now(),1)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Pudim','Pudim de leite condensado 100g',7.50, 'pudim.jpg',20,now(),1)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
