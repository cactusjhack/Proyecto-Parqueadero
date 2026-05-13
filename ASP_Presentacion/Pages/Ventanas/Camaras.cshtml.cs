using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class CamarasModel : PageModel
    {
        private ICamarasNegocio? iCamarasNegocio;

        [BindProperty] public List<Camaras>? Lista { get; set; }
        [BindProperty] public Camaras? Camara { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public CamarasModel()
        {
            iCamarasNegocio = new CamarasNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iCamarasNegocio?.Consultar();
                Camara = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Camara = new Camaras();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Camara == null) return;
                if (Camara.Id == 0)
                    Camara = iCamarasNegocio!.Guardar(Camara);
                else
                    Camara = iCamarasNegocio!.Actualizar(Camara);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Camara = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Camara = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Camara == null) return;
                iCamarasNegocio!.Eliminar(Camara.Id);
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
