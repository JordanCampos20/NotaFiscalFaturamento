﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotaFiscalFaturamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_ProdutoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Produtos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Produtos");
        }
    }
}
