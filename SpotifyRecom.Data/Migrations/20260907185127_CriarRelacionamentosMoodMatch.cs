using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyRecom.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriarRelacionamentosMoodMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicaAtividade",
                columns: table => new
                {
                    MidiaId = table.Column<int>(type: "int", nullable: false),
                    AtividadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaAtividade", x => new { x.MidiaId, x.AtividadeId });
                    table.ForeignKey(
                        name: "FK_MusicaAtividade_Atividades_AtividadeId",
                        column: x => x.AtividadeId,
                        principalTable: "Atividades",
                        principalColumn: "IdAtividade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicaAtividade_Midias_MidiaId",
                        column: x => x.MidiaId,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MusicaEmocao",
                columns: table => new
                {
                    MidiaId = table.Column<int>(type: "int", nullable: false),
                    EmocaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaEmocao", x => new { x.MidiaId, x.EmocaoId });
                    table.ForeignKey(
                        name: "FK_MusicaEmocao_Emocoes_EmocaoId",
                        column: x => x.EmocaoId,
                        principalTable: "Emocoes",
                        principalColumn: "IdEmocao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicaEmocao_Midias_MidiaId",
                        column: x => x.MidiaId,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaAtividade_AtividadeId",
                table: "MusicaAtividade",
                column: "AtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaEmocao_EmocaoId",
                table: "MusicaEmocao",
                column: "EmocaoId");

            migrationBuilder.Sql("INSERT INTO `MusicaAtividade` (`MidiaId`, `AtividadeId`) SELECT `IdMidia`, `AtividadeId` FROM `Midias` WHERE `AtividadeId` <> 0;");
            migrationBuilder.Sql("INSERT INTO `MusicaEmocao` (`MidiaId`, `EmocaoId`) SELECT `IdMidia`, `EmocaoId` FROM `Midias` WHERE `EmocaoId` <> 0;");

            migrationBuilder.DropForeignKey(
                name: "FK_Midias_Atividades_AtividadeId",
                table: "Midias");

            migrationBuilder.DropForeignKey(
                name: "FK_Midias_Emocoes_EmocaoId",
                table: "Midias");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicaAtividade");

            migrationBuilder.DropTable(
                name: "MusicaEmocao");

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
    }
}
