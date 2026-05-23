# PharmaSoft
### Autores
- Randy Joel Acosta Tejada
- Diana Carolina Hidalgo

# 💊 PharmaSoft - Sistema de Gestión Farmacéutica


**PharmaSoft** es una aplicación de escritorio integral y moderna diseñada para automatizar y optimizar las operaciones diarias de una farmacia. Desarrollado en **Windows Forms (.NET)** bajo una arquitectura sólida por capas y utilizando **Entity Framework Core**, el sistema permite centralizar desde el control de inventarios críticos y alertas de caducidad hasta la facturación al detalle, cuentas por cobrar y análisis financiero en pesos dominicanos (RD$).

---

## 🔐 Módulos del Sistema

### 1. Control de Acceso (Login)
* Sistema seguro de autenticación de usuarios mediante credenciales.
* Opción para visibilizar la contraseña ingresada para mejorar la experiencia de usuario.
* Restricción de funciones según el rol asignado (ej: Administrador en Farmacia Central).


### 2. Panel de Control (Dashboard)
* Vista general inmediata de la salud del negocio.
* Indicadores clave en tiempo real: volumen total de stock actual, monto total acumulado en ventas y balance de cuentas pendientes por cobrar.
* Menú de navegación lateral intuitivo para acceder rápidamente a todas las operaciones.



### 3. Gestión de Inventario y Productos
* Catálogo detallado de medicamentos con codificación única (código de barras/SKU), nombre comercial y laboratorio fabricante (Genfar, Sanofi, etc.).
* Control de existencias exactas y precios de venta al público.
* **Alerta de Caducidad:** Registro y monitoreo estricto de las fechas de vencimiento de cada lote para evitar pérdidas y garantizar la seguridad sanitaria.



### 4. Punto de Venta (Point of Sale - POS)
* Interfaz ágil para la facturación y procesamiento de ventas.
* Carrito de compras dinámico que desglosa automáticamente el Medicamento ID, Lote ID, precio unitario, cantidad y subtotal.
* Soporte para múltiples tipos de factura (Al Contado, Crédito) y tipos de comprobantes fiscales (Consumidor Final, entre otros).
* Cálculo automatizado de subtotales y totales generales.



### 5. Reportes de Ventas e Impresión
* Filtro avanzado de transacciones por rango de fechas.
* Historial detallado con ID de transacción, fecha/hora exacta, método de pago utilizado (Efectivo, Tarjeta) y estado de la venta.
* Cómputo automático de la cantidad de ventas del periodo y los ingresos brutos generados.
* **Módulo de impresión:** Capacidad de exportar e imprimir facturas directamente en formato PDF.



### 6. Cuentas por Cobrar y Clientes
* Módulo financiero para el seguimiento de ventas a crédito.
* Monitoreo de balances pendientes, montos iniciales y fechas límites de vencimiento por cada Venta ID.
* Gestión completa de clientes que incluye registro de datos personales (Nombre, RNC/Cédula, Teléfono, Dirección).
* **Control de Riesgo:** Asignación y validación paramétrica de límites de crédito por cliente para proteger el flujo de caja del negocio.



## 🗂️ Arquitectura del Proyecto

El proyecto está estructurado de manera limpia utilizando el patrón de diseño por capas para separar las responsabilidades de datos, lógica de negocio y presentación:



PharmaSoft/
│
├── 📂 PharmaSoft.Data (Capa de Acceso a Datos)
│   ├── 📂 Entities/
│   │   ├── Usuario.cs
│   │   ├── Medicamento.cs
│   │   ├── Cliente.cs
│   │   ├── Venta.cs
│   │   ├── DetalleVenta.cs
│   │   └── CuentaPorCobrar.cs
│   │
│   ├── PharmaSoftContext.cs (Contexto de EF Core)
│   └── App.config
│
└── 📂 PharmaSoft.UI (Capa de Presentación)
    └── 📂 Forms/
        ├── LoginForm.cs
        ├── MainMenu.cs (Panel de Control)
        ├── InventarioForm.cs
        ├── NuevaVentaForm.cs
        ├── ReportesForm.cs
        └── ClientesForm.cs
