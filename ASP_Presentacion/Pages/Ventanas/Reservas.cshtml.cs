using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private IReservasNegocio? iReservasNegocio;

        [BindProperty] public List<Reservas>? Lista { get; set; }
        [BindProperty] public Reservas? Reserva { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ReservasModel()
        {
            iReservasNegocio = new ReservasNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iReservasNegocio?.Consultar();
                Reserva = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Reserva = new Reservas();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Reserva == null) return;
                if (Reserva.Id == 0)
                    Reserva = iReservasNegocio!.Guardar(Reserva);
                else
                    Reserva = iReservasNegocio!.Actualizar(Reserva);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Reserva = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Reserva = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Reserva == null) return;
                iReservasNegocio!.Eliminar(Reserva.Id);
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
