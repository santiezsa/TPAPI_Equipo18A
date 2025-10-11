using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;
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
        public void Post([FromBody]ArticuloDto art)
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

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}
