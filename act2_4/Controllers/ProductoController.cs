using act2_4.Data;
using act2_4.Domain;
using act2_4.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace act2_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _service;

        public ProductoController()
        {
            _service = new ProductoService();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (_service.GetAll().Count == 0)
            {
                return NotFound("Lista Vacia");
            }else
            {
                return Ok(_service.GetAll());
            }
        }

        [HttpGet("/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (_service.GetById(id).Id == 0)
            {
                return NotFound("Produto Inexistente");
            }
            else
            {
                return Ok(_service.GetById(id));
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody]Producto product)
        {
            _service.AddProduct(product);
            return Ok();
        }
      
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.DeleteProduct(id);
            if (result)
            {
                return Ok("Producto dado de baja");
            }
            return NotFound("Producto No Existente");
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Producto producto,int id)
        {
            var productoExists = _service.GetById(id);
            if (productoExists != null)
            {
                _service.EditProduct(producto);

                return Ok();
            }
            return BadRequest();
        }


    }
}
