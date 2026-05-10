using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class CargosModel : PageModel
    {
        private ICargosNegocio? iCargosNegocio;

        [BindProperty] public List<Cargos>? Lista { get; set; }
        [BindProperty] public Cargos? Cargo { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public CargosModel()
        {
            iCargosNegocio = new CargosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iCargosNegocio?.Consultar();
                Cargo = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Cargo = new Cargos();
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Cargo = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtGuardar()
        {
            try
            {
                if (Cargo == null) return;
                if (Cargo.Id == 0)
                    Cargo = iCargosNegocio!.Guardar(Cargo);
                else
                    Cargo = iCargosNegocio!.Actualizar(Cargo);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Cargo = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Cargo == null) return;
                iCargosNegocio!.Eliminar(Cargo.Id);
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
