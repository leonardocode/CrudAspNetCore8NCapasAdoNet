using CapaEntidades;
using CapaNegocio;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TipoMedicamentoBL tipoMedicamentoBL = new TipoMedicamentoBL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //se llama https://localhost:7226/home/Saludo
        public string Saludo()
        {
            return "Hola desde c#";
        }

        //se llama https://localhost:7226/home/numeroEntero
        public int numeroEntero()
        {
            return 10;
        }

        //se llama https://localhost:7226/home/numeroDecimal
        public double numeroDecimal()
        {
            return 5.6;
        }

        //https://localhost:7226/home/saludarNombre/?nombre=leonardo
        public string saludarNombre(string nombre)
        {
            return "Bienvenido, " + nombre;
        }

        //https://localhost:7226/home/saludarNombreApellido/?nombre=leonardo&apellido=amaya
        public string saludarNombreApellido(string nombre, string apellido)
        {
            return "Bienvenido, Nombre: " + nombre+ " Apellido: "+apellido;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<TipoMedicamentoPrueba> Lista()
        {
            return tipoMedicamentoBL.Lista();
        }
    }
}
