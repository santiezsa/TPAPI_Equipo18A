using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TPAPI_equipo_18A.Models
{
    public class ArticuloDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int idMarca { get; set; }
        public int idCategoria { get; set; }
        public decimal Precio { get; set; }
        public List<Imagen> Imagenes { get; set; }
    }
}