using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class UsuariosModel : PageModel
    {
        private IUsuariosNegocio? iUsuariosNegocio;

        [BindProperty] public List<Usuarios>? Lista { get; set; }
        [BindProperty] public Usuarios? Usuario { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public UsuariosModel()
        {
            iUsuariosNegocio = new UsuariosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iUsuariosNegocio?.Consultar();
                Usuario = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Usuario = new Usuarios();
            Borrando = false;
        }
        public void OnPostBtGuardar()
        {
            try
            {
                if (Usuario == null) return;
                if (Usuario.Id == 0)
                    Usuario = iUsuariosNegocio!.Guardar(Usuario);
                else
                    Usuario = iUsuariosNegocio!.Actualizar(Usuario);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Usuario = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Usuario = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Usuario == null) return;
                iUsuariosNegocio!.Eliminar(Usuario.Id);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}
