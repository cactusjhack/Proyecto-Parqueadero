using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class TarifasModel : PageModel
    {
        private ITarifasNegocio? iTarifasNegocio;

        [BindProperty] public List<Tarifas>? Lista { get; set; }
        [BindProperty] public Tarifas? Tarifa { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TarifasModel()
        {
            iTarifasNegocio = new TarifasNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iTarifasNegocio?.Consultar();
                Tarifa = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Tarifa = new Tarifas();
            Borrando = false;
        }
        public void OnPostBtGuardar()
        {
            try
            {
                if (Tarifa == null) return;
                if (Tarifa.Id == 0)
                    Tarifa = iTarifasNegocio!.Guardar(Tarifa);
                else
                    Tarifa = iTarifasNegocio!.Actualizar(Tarifa);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Tarifa = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Tarifa = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Tarifa == null) return;
                iTarifasNegocio!.Eliminar(Tarifa.Id);
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
