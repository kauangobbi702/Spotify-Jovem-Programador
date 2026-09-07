using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyRecom.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoMoodMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtividadeId",
                table: "Midias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmocaoId",
                table: "Midias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    IdAtividade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.IdAtividade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Emocoes",
                columns: table => new
                {
                    IdEmocao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emocoes", x => x.IdEmocao);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Atividades",
                columns: new[] { "IdAtividade", "Nome" },
                values: new object[,]
                {
                    { 1, "Caminhando" },
                    { 2, "Cozinhando" },
                    { 3, "Jogando" },
                    { 4, "Estudando" },
                    { 5, "Relaxando" },
                    { 6, "Trabalhando" }
                });

            migrationBuilder.InsertData(
                table: "Emocoes",
                columns: new[] { "IdEmocao", "Nome" },
                values: new object[,]
                {
                    { 1, "Alegre" },
                    { 2, "Triste" },
                    { 3, "Energetico" },
                    { 4, "Motivado" },
                    { 5, "Reflexivo" },
                    { 6, "Preguicoso" },
                    { 7, "Enfurecido" }
                });

            migrationBuilder.Sql("UPDATE `Midias` SET `AtividadeId` = 1, `EmocaoId` = 1 WHERE `AtividadeId` = 0 OR `EmocaoId` = 0;");

            migrationBuilder.CreateIndex(
                name: "IX_Midias_AtividadeId",
                table: "Midias",
                column: "AtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Midias_EmocaoId",
                table: "Midias",
                column: "EmocaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Midias_Atividades_AtividadeId",
                table: "Midias",
                column: "AtividadeId",
                principalTable: "Atividades",
                principalColumn: "IdAtividade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Midias_Emocoes_EmocaoId",
                table: "Midias",
                column: "EmocaoId",
                principalTable: "Emocoes",
                principalColumn: "IdEmocao",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Midias_Atividades_AtividadeId",
                table: "Midias");

            migrationBuilder.DropForeignKey(
                name: "FK_Midias_Emocoes_EmocaoId",
                table: "Midias");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Emocoes");

            migrationBuilder.DropIndex(
                name: "IX_Midias_AtividadeId",
                table: "Midias");

            migrationBuilder.DropIndex(
                name: "IX_Midias_EmocaoId",
                table: "Midias");

            migrationBuilder.DropColumn(
                name: "AtividadeId",
                table: "Midias");

            migrationBuilder.DropColumn(
                name: "EmocaoId",
                table: "Midias");
        }
    }
}
