using act2_4.Data;
using act2_4.Domain;

namespace act2_4.Service
{
    public class ProductoService
    {
        private readonly IProductoRepository _repository;
        public ProductoService()
        {
            _repository = new ProductoRepository();
        }

        public List<Producto> GetAll()
        {
            return _repository.GetAll();
        }
        public Producto GetById(int id)
        {
            return _repository.GetById(id);
        }
        public void AddProduct(Producto producto)
        {
            _repository.AddProduct(producto);
        }
        public void EditProduct(Producto producto)
        {
            _repository.EditProduct(producto);

        }
        public bool DeleteProduct(int id)
        {
            var productoExists = _repository.GetById(id);
            if (productoExists != null)
            {
                return _repository.DeleteProduct(id);

            }
            return false;


        }

    }
}
