using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ContactoBL _contactoBL;

        public ContactoController(ILogger<ContactoController> logger, ContactoBL contactoBL)
        {
            this._logger = logger;
            _contactoBL = contactoBL;
        }
        public IActionResult Index()
        {
            try
            {
                var lista = _contactoBL.Lista();
                return View(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en la capa controlador: Contacto, metodo Index"+ ex.Message);
                throw new Exception(ex.Message);
            }
           
        }
    }
}
