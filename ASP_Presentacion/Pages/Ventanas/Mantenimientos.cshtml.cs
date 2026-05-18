using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class MantenimientosModel : PageModel
    {
        private IMantenimientosNegocio? iMantenimientosNegocio;

        [BindProperty] public List<Mantenimientos>? Lista { get; set; }
        [BindProperty] public Mantenimientos? Mantenimiento { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public MantenimientosModel()
        {
            iMantenimientosNegocio = new MantenimientosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iMantenimientosNegocio?.Consultar();
                Mantenimiento = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Mantenimiento = new Mantenimientos();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Mantenimiento == null) return;
                if (Mantenimiento.Id == 0)
                    Mantenimiento = iMantenimientosNegocio!.Guardar(Mantenimiento);
                else
                    Mantenimiento = iMantenimientosNegocio!.Actualizar(Mantenimiento);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Mantenimiento = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Mantenimiento = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Mantenimiento == null) return;
                iMantenimientosNegocio!.Eliminar(Mantenimiento.Id);
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
