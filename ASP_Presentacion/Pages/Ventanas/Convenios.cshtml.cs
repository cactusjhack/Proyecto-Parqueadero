using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class ConveniosModel : PageModel
    {
        private IConveniosNegocio? iConveniosNegocio;

        [BindProperty] public List<Convenios>? Lista { get; set; }
        [BindProperty] public Convenios? Convenio { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ConveniosModel()
        {
            iConveniosNegocio = new ConveniosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iConveniosNegocio?.Consultar();
                Convenio = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Convenio = new Convenios();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Convenio == null) return;
                if (Convenio.Id == 0)
                    Convenio = iConveniosNegocio!.Guardar(Convenio);
                else
                    Convenio = iConveniosNegocio!.Actualizar(Convenio);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Convenio = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Convenio = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Convenio == null) return;
                iConveniosNegocio!.Eliminar(Convenio.Id);
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
