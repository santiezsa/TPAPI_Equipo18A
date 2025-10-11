using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listarImagenesPorArticulo(int idArticulo)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, IdArticulo, ImagenUrl from Imagenes where IDArticulo = @id");
                datos.setearParametro("@id", idArticulo);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Imagen img = new Imagen();
                    img.Id = (int)datos.Lector["Id"];
                    img.IdArticulo = (int)datos.Lector["IdArticulo"];
                    img.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    lista.Add(img);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
            return lista;
        }

        public void agregarImagen(int idArticulo, string imagenUrl)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.setearParametro("@ImagenUrl", imagenUrl);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarImagenes(int idArticulo, List<Imagen> imagenes)
        {
            foreach (var imagen in imagenes)
            {
                agregarImagen(idArticulo, imagen.ImagenUrl);
            }
        }

        public void eliminarImagenesPorArticulo(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void actualizarImagen(int idImagen, string nuevaUrl)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE IMAGENES SET ImagenUrl = @ImagenUrl WHERE Id = @Id");
                datos.setearParametro("@ImagenUrl", nuevaUrl);
                datos.setearParametro("@Id", idImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }


}
