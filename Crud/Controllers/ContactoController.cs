using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ContactoBL contactoBL;
        public ContactoController(ContactoBL contactoBL)
        {
            this.contactoBL = contactoBL;
        }
        public IActionResult Index()
        {
            var lista = contactoBL.Lista();
            return View(lista);
        }
    }
}
