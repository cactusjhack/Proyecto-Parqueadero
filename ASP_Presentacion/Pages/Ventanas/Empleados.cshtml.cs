using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class EmpleadosModel : PageModel
    {
        private IEmpleadosNegocio? iEmpleadosNegocio;

        [BindProperty] public List<Empleados>? Lista { get; set; }
        [BindProperty] public Empleados? Empleado { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public EmpleadosModel()
        {
            iEmpleadosNegocio = new EmpleadosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iEmpleadosNegocio?.Consultar();
                Empleado = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Empleado = new Empleados();
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Empleado = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Empleado == null) return;
                if (Empleado.Id == 0)
                    Empleado = iEmpleadosNegocio!.Guardar(Empleado);
                else
                    Empleado = iEmpleadosNegocio!.Actualizar(Empleado);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Empleado = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Empleado == null) return;
                iEmpleadosNegocio!.Eliminar(Empleado.Id);
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
