using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class ClientesModel : PageModel
    {
        private IClientesNegocio? iClientesNegocio;

        [BindProperty] public List<Clientes>? Lista { get; set; }
        [BindProperty] public Clientes? Cliente { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ClientesModel()
        {
            iClientesNegocio = new ClientesNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iClientesNegocio?.Consultar();
                Cliente = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Cliente = new Clientes();
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Cliente = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtGuardar()
        {
            try
            {
                if (Cliente == null) return;
                if (Cliente.Id == 0)
                    Cliente = iClientesNegocio!.Guardar(Cliente);
                else
                    Cliente = iClientesNegocio!.Actualizar(Cliente);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Cliente = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Cliente == null) return;
                iClientesNegocio!.Eliminar(Cliente.Id);
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
