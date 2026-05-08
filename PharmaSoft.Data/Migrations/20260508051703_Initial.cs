using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaSoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__F353C1C5E037566A", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RNC_Cedula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LimiteCredito = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__71ABD0A7DAC6EF43", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__61266BB9F87D2A0F", x => x.ProveedorID);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    VentaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ventas__5B41514C6BD66B9A", x => x.VentaID);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    CompraID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorID = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TotalCompra = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TipoPago = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NumeroFacturaProveedor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Compras__067DA725CB505B59", x => x.CompraID);
                    table.ForeignKey(
                        name: "FK_Compras_Proveedores",
                        column: x => x.ProveedorID,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorID");
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    MedicamentoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoBarras = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PrincipioActivo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CategoriaID = table.Column<int>(type: "int", nullable: false),
                    ProveedorID = table.Column<int>(type: "int", nullable: false),
                    RequiereReceta = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    PrecioCompra = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: true, defaultValue: 10)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicame__003D65F308DB0ECC", x => x.MedicamentoID);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Categorias",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID");
                    table.ForeignKey(
                        name: "FK_Medicamentos_Proveedores",
                        column: x => x.ProveedorID,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorID");
                });

            migrationBuilder.CreateTable(
                name: "CuentasPorCobrar",
                columns: table => new
                {
                    CxCID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaID = table.Column<int>(type: "int", nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    MontoInicial = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SaldoPendiente = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CuentasP__1884E33C3B157190", x => x.CxCID);
                    table.ForeignKey(
                        name: "FK_CxC_Clientes",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID");
                    table.ForeignKey(
                        name: "FK_CxC_Ventas",
                        column: x => x.VentaID,
                        principalTable: "Ventas",
                        principalColumn: "VentaID");
                });

            migrationBuilder.CreateTable(
                name: "CuentasPorPagar",
                columns: table => new
                {
                    CxPID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraID = table.Column<int>(type: "int", nullable: false),
                    ProveedorID = table.Column<int>(type: "int", nullable: false),
                    MontoInicial = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SaldoPendiente = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CuentasP__2B8D7CE83D0A6A21", x => x.CxPID);
                    table.ForeignKey(
                        name: "FK_CxP_Compras",
                        column: x => x.CompraID,
                        principalTable: "Compras",
                        principalColumn: "CompraID");
                    table.ForeignKey(
                        name: "FK_CxP_Proveedores",
                        column: x => x.ProveedorID,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorID");
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompras",
                columns: table => new
                {
                    DetalleCompraID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraID = table.Column<int>(type: "int", nullable: false),
                    MedicamentoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioCostoUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleC__F7FC189A4B103DD5", x => x.DetalleCompraID);
                    table.ForeignKey(
                        name: "FK_DetalleC_Compras",
                        column: x => x.CompraID,
                        principalTable: "Compras",
                        principalColumn: "CompraID");
                    table.ForeignKey(
                        name: "FK_DetalleC_Medicamentos",
                        column: x => x.MedicamentoID,
                        principalTable: "Medicamentos",
                        principalColumn: "MedicamentoID");
                });

            migrationBuilder.CreateTable(
                name: "LotesInventario",
                columns: table => new
                {
                    LoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicamentoID = table.Column<int>(type: "int", nullable: false),
                    NumeroLote = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    CantidadActual = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LotesInv__E6EAE6F88F0AB7ED", x => x.LoteID);
                    table.ForeignKey(
                        name: "FK_Lotes_Medicamentos",
                        column: x => x.MedicamentoID,
                        principalTable: "Medicamentos",
                        principalColumn: "MedicamentoID");
                });

            migrationBuilder.CreateTable(
                name: "PagosClientes",
                columns: table => new
                {
                    PagoClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CxCID = table.Column<int>(type: "int", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MontoPagado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PagosCli__7A06804746EB57F7", x => x.PagoClienteID);
                    table.ForeignKey(
                        name: "FK_Pagos_CxC",
                        column: x => x.CxCID,
                        principalTable: "CuentasPorCobrar",
                        principalColumn: "CxCID");
                });

            migrationBuilder.CreateTable(
                name: "PagosProveedores",
                columns: table => new
                {
                    PagoProveedorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CxPID = table.Column<int>(type: "int", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MontoPagado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PagosPro__D09663A5632D04E3", x => x.PagoProveedorID);
                    table.ForeignKey(
                        name: "FK_Pagos_CxP",
                        column: x => x.CxPID,
                        principalTable: "CuentasPorPagar",
                        principalColumn: "CxPID");
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    DetalleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaID = table.Column<int>(type: "int", nullable: false),
                    LoteID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleV__6E19D6FAEBC23E91", x => x.DetalleID);
                    table.ForeignKey(
                        name: "FK_Detalle_Lotes",
                        column: x => x.LoteID,
                        principalTable: "LotesInventario",
                        principalColumn: "LoteID");
                    table.ForeignKey(
                        name: "FK_Detalle_Ventas",
                        column: x => x.VentaID,
                        principalTable: "Ventas",
                        principalColumn: "VentaID");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Clientes__554ADC6A51A518E9",
                table: "Clientes",
                column: "RNC_Cedula",
                unique: true,
                filter: "[RNC_Cedula] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProveedorID",
                table: "Compras",
                column: "ProveedorID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorCobrar_ClienteID",
                table: "CuentasPorCobrar",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorCobrar_VentaID",
                table: "CuentasPorCobrar",
                column: "VentaID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorPagar_CompraID",
                table: "CuentasPorPagar",
                column: "CompraID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorPagar_ProveedorID",
                table: "CuentasPorPagar",
                column: "ProveedorID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_CompraID",
                table: "DetalleCompras",
                column: "CompraID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_MedicamentoID",
                table: "DetalleCompras",
                column: "MedicamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_LoteID",
                table: "DetalleVentas",
                column: "LoteID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaID",
                table: "DetalleVentas",
                column: "VentaID");

            migrationBuilder.CreateIndex(
                name: "IX_LotesInventario_MedicamentoID",
                table: "LotesInventario",
                column: "MedicamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_CategoriaID",
                table: "Medicamentos",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_ProveedorID",
                table: "Medicamentos",
                column: "ProveedorID");

            migrationBuilder.CreateIndex(
                name: "UQ__Medicame__F61589C8B5D33220",
                table: "Medicamentos",
                column: "CodigoBarras",
                unique: true,
                filter: "[CodigoBarras] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PagosClientes_CxCID",
                table: "PagosClientes",
                column: "CxCID");

            migrationBuilder.CreateIndex(
                name: "IX_PagosProveedores_CxPID",
                table: "PagosProveedores",
                column: "CxPID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCompras");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "PagosClientes");

            migrationBuilder.DropTable(
                name: "PagosProveedores");

            migrationBuilder.DropTable(
                name: "LotesInventario");

            migrationBuilder.DropTable(
                name: "CuentasPorCobrar");

            migrationBuilder.DropTable(
                name: "CuentasPorPagar");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
