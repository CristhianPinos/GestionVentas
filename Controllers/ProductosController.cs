

namespace GestionVentas.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GestionVentas.Models;
    internal class ProductosController
    {
        private ProductosModel modeloProducto = new ProductosModel();

        public List<ProductosModel> todos()
        {
            return modeloProducto.todos();
        }
        public ProductosModel uno(ProductosModel producto)
        {
            return modeloProducto.uno(producto);
        }
        public string insertar(ProductosModel producto)
        {
            return modeloProducto.insertar(producto);
        }
        public string actualizar(ProductosModel producto)
        {
            return modeloProducto.actualizar(producto);
        }
        public string eliminar(ProductosModel producto)
        {
            return modeloProducto.eliminar(producto);
        }
    }
}
