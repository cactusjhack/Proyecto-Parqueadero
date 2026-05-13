using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class EspaciosModel : PageModel
    {
        private IEspaciosNegocio? iEspaciosNegocio;

        [BindProperty] public List<Espacios>? Lista { get; set; }
        [BindProperty] public Espacios? Espacio { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public EspaciosModel()
        {
            iEspaciosNegocio = new EspaciosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iEspaciosNegocio?.Consultar();
                Espacio = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Espacio = new Espacios();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Espacio == null) return;
                if (Espacio.Id == 0)
                    Espacio = iEspaciosNegocio!.Guardar(Espacio);
                else
                    Espacio = iEspaciosNegocio!.Actualizar(Espacio);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Espacio = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Espacio = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Espacio == null) return;
                iEspaciosNegocio!.Eliminar(Espacio.Id);
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
