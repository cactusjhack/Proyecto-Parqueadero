using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class VehiculosModel : PageModel
    {
        private IVehiculosNegocio? iVehiculosNegocio;

        [BindProperty] public List<Vehiculos>? Lista { get; set; }
        [BindProperty] public Vehiculos? Vehiculo { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public VehiculosModel()
        {
            iVehiculosNegocio = new VehiculosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iVehiculosNegocio?.Consultar();
                Vehiculo = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Vehiculo = new Vehiculos();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Vehiculo == null) return;
                if (Vehiculo.Id == 0)
                    Vehiculo = iVehiculosNegocio!.Guardar(Vehiculo);
                else
                    Vehiculo = iVehiculosNegocio!.Actualizar(Vehiculo);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Vehiculo = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Vehiculo = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Vehiculo == null) return;
                iVehiculosNegocio!.Eliminar(Vehiculo.Id);
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
