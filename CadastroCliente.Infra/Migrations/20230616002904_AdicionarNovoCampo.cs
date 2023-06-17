using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroCliente.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarNovoCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensDeServico_Clientes_ClienteId",
                table: "OrdensDeServico");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_OrdensDeServico_OrdemDeServicoId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_OrdemDeServicoId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_OrdensDeServico_ClienteId",
                table: "OrdensDeServico");

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "OrdensDeServico",
                type: "decimal(65,30)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "OrdensDeServico");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_OrdemDeServicoId",
                table: "Servicos",
                column: "OrdemDeServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDeServico_ClienteId",
                table: "OrdensDeServico",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensDeServico_Clientes_ClienteId",
                table: "OrdensDeServico",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_OrdensDeServico_OrdemDeServicoId",
                table: "Servicos",
                column: "OrdemDeServicoId",
                principalTable: "OrdensDeServico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
