using act2_4.Domain;
using Banco.Utils;
using System.Collections.Generic;
using System.Data;

namespace act2_4.Data
{
    public interface IProductoRepository
    {
        List<Producto> GetAll();
        void AddProduct(Producto producto);
        void EditProduct(Producto producto);
        bool DeleteProduct(int id);
        Producto GetById(int id);

    }
    public class ProductoRepository : IProductoRepository
    {
        //private static readonly List<Producto> _products = new List<Producto>();
        public ProductoRepository() { }

        public List<Producto> GetAll()
        {
            var lst = new List<Producto>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("Sp_GetAllProductos", null);
            foreach (DataRow item in dt.Rows) 
            {
                var producto = new Producto();
                producto.Id = (int)item["codigo"];
                producto.Nombre = item["n_producto"].ToString();
                producto.Precio = (decimal)item["precio"];
                producto.Stock = (int)item["stock"];
                producto.estaActivo = (bool)item["esta_activo"];
                lst.Add(producto);
            }

            return lst;

        }
        public Producto GetById(int id)
        {

            var lstParam = new List<ParameterSQL>
            {
                new ParameterSQL("id", id)
            };

            var dt = DataHelper.GetInstance().ExecuteSPQuery("Sp_GetProductosById", lstParam);
            var producto = new Producto();

            foreach (DataRow item in dt.Rows)
            {
                producto.Id = (int)item["codigo"];
                producto.Nombre = item["n_producto"].ToString();
                producto.Precio = (decimal)item["precio"];
                producto.Stock = (int)item["stock"];
                producto.estaActivo = (bool)item["esta_activo"];
            }

            return producto;
        }

        public void AddProduct(Producto producto)
        {
            var lst = new List<ParameterSQL>
            {
                new ParameterSQL("nombre",producto.Nombre),
                new ParameterSQL("precio",producto.Precio),
                new ParameterSQL("stock",producto.Stock)
            };

            var result = DataHelper.GetInstance().ExecuteSPDML("Sp_InsertProduct", lst);


        }
        public void EditProduct(Producto producto)
        {
            var lst = new List<ParameterSQL>
            {
                new ParameterSQL("@id", producto.Id),
                 new ParameterSQL("@nombre", producto.Nombre),
                  new ParameterSQL("@precio", producto.Precio),
                   new ParameterSQL("@esta_activo", producto.estaActivo),
                    new ParameterSQL("@stock", producto.Stock)
            };

            int filas = DataHelper.GetInstance().ExecuteSPDML("Sp_UpdateProductos", lst);


        }
        public bool DeleteProduct(int id)
        {
            var lst = new List<ParameterSQL>
            {
                new ParameterSQL("@id", id)
            };

            var result = DataHelper.GetInstance().ExecuteSPDML("Sp_BajarProducto", lst) == 1;

            return result;

        }
    }
}



//// notas personales

//// web => api
///cliente / servidor
/// ------/ controller <=> service <=> repository <=> DataHelper / ProjectDbContext (base de datos)


///1.crear factura y obtener el id en el FacturaRepository inician la transaccion => try&catch
///2.Una vez que tiene el id, van a llamar a detalleRepository, addDetalle(List<DetalleFactura> df,int facturaId)
///3. detalleRepository.addDetalle iteren cada detalle y le asignel la facturaId, tambien agregar un try&catch
///4.Guardan como siempre

//////
///

