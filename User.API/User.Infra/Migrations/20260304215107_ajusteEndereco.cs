using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajusteEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_UserId",
                table: "Enderecos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Enderecos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_UserId",
                table: "Enderecos",
                newName: "IX_Enderecos_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Enderecos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_UsuarioId",
                table: "Enderecos",
                newName: "IX_Enderecos_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_UserId",
                table: "Enderecos",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
