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
        public HttpResponseMessage Post([FromBody] ArticuloDto art)
        {
            try
            {
                if (art == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El cuerpo de la solicitud no puede ser nulo.");
                }
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo nuevo = new Articulo();
                MarcasNegocio marcaNegocio = new MarcasNegocio();
                CategoriasNegocio categoriaNegocio = new CategoriasNegocio();

                if (string.IsNullOrEmpty(art.Codigo))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "El código de artículo no puede ser nulo.");
                    }
                nuevo.Codigo = art.Codigo;

                if (string.IsNullOrEmpty(art.Nombre))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El nombre del artículo no puede ser nulo.");
                }
                nuevo.Nombre = art.Nombre;

                if (string.IsNullOrEmpty(art.Descripcion))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La descripción del artículo no puede ser nula.");
                }
                nuevo.Descripcion = art.Descripcion;
                nuevo.Marca = new Marca { Id = art.IdMarca };
                nuevo.Categoria = new Categoria { Id = art.IdCategoria };

                if (art.Precio <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El precio del artículo no puede ser menor o igual a cero.");
                }
                nuevo.Precio = art.Precio;

                Marca marca = marcaNegocio.listar().Find(x => x.Id == art.IdMarca);
                Categoria categoria = categoriaNegocio.listar().Find(x => x.Id == art.IdCategoria);

                if (marca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe.");

                if (categoria == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe.");

                negocio.agregar(nuevo);

                return Request.CreateResponse(HttpStatusCode.Created, "Articulo agregado.");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al crear el producto: " + ex.Message.ToString());
            }
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
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDto art)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El ID del artículo no puede ser menor o igual a cero.");
                }

                if (art == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El cuerpo de la solicitud no puede ser nulo.");
                }

                // Asigno ID

                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo nuevo = new Articulo();
                MarcasNegocio marcaNegocio = new MarcasNegocio();
                CategoriasNegocio categoriaNegocio = new CategoriasNegocio();
                List<Articulo> lista = negocio.listar();
                nuevo.Id = id;
                if (negocio.listar().Find(x => x.Id == id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontró el artículo con el ID proporcionado.");
                }

                if (string.IsNullOrEmpty(art.Codigo))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El código de artículo no puede ser nulo.");
                }
                nuevo.Codigo = art.Codigo;

                if (string.IsNullOrEmpty(art.Nombre))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El nombre del artículo no puede ser nulo.");
                }
                nuevo.Nombre = art.Nombre;

                if (string.IsNullOrEmpty(art.Descripcion))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La descripción del artículo no puede ser nula.");
                }
                nuevo.Descripcion = art.Descripcion;
                nuevo.Marca = new Marca { Id = art.IdMarca };
                nuevo.Categoria = new Categoria { Id = art.IdCategoria };

                if (art.Precio <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El precio del artículo no puede ser menor o igual a cero.");
                }
                nuevo.Precio = art.Precio;

                Marca marca = marcaNegocio.listar().Find(x => x.Id == art.IdMarca);
                Categoria categoria = categoriaNegocio.listar().Find(x => x.Id == art.IdCategoria);

                if (marca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe." + art.IdMarca );

                if (categoria == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe.");

                negocio.modificar(nuevo);

                return Request.CreateResponse(HttpStatusCode.Created, "Articulo modificado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al modificar el producto: " + ex.Message.ToString());
            }
        }


        // DELETE: api/Producto/5
        public void Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.eliminar(id);
        }
    }
}
