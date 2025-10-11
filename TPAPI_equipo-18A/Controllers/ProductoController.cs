using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using TPAPI_equipo_18A.Models;

namespace TPAPI_equipo_18A.Controllers
{
    public class ProductoController : ApiController
    {
        ArticuloNegocio negocio;

        // GET: api/Producto
        public IEnumerable<Articulo> Get()
        {
            negocio = new ArticuloNegocio();
            return negocio.listar();
        }

        // GET: api/Producto/5
        public Articulo Get(int id)
        {
            negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.listar();

            return lista.Find(articulo => articulo.Id == id);
        }

        // POST: api/Producto
        public void Post([FromBody] ArticuloDto art)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();

            MarcasNegocio marcaNegocio = new MarcasNegocio();
            CategoriasNegocio categoriaNegocio = new CategoriasNegocio();

            nuevo.Codigo = art.Codigo;
            nuevo.Nombre = art.Nombre;
            nuevo.Descripcion = art.Descripcion;
            nuevo.Marca = new Marca { Id = art.IdMarca };
            nuevo.Categoria = new Categoria { Id = art.IdCategoria };
            nuevo.Precio = art.Precio;

            negocio.agregar(nuevo);
        }

        // POST: api/Producto/Imagen/{idArticulo}
        [HttpPost]
        [Route("api/Producto/Imagen/{idArticulo}")]
        public void Post(int idArticulo, [FromBody] ImagenDto img)
        {
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            negocio = new ArticuloNegocio();

            imagenNegocio.agregarImagen(idArticulo, img.ImagenUrl);
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody] ArticuloDto art)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();

            // Asigno ID
            nuevo.Id = id;

            nuevo.Codigo = art.Codigo;
            nuevo.Nombre = art.Nombre;
            nuevo.Descripcion = art.Descripcion;
            nuevo.Marca = new Marca { Id = art.IdMarca };
            nuevo.Categoria = new Categoria { Id = art.IdCategoria };
            nuevo.Precio = art.Precio;

            negocio.modificar(nuevo);
        }


        // DELETE: api/Producto/5
        public void Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.eliminar(id);
        }
    }
}
