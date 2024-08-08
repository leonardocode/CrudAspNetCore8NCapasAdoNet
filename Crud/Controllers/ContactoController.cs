using CapaEntidades;
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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var (contactos, errorMessage) = await _contactoBL.Lista();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    TempData["ErrorMessage"] = errorMessage;
                    return View(new List<Contacto>());
                }
                return View(contactos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa controlador: Contacto, método Index");
                TempData["ErrorMessage"] = "Ocurrió un error al cargar la lista de contactos.";
                return View(new List<Contacto>());
            }
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return View(contacto);
            }

            try
            {
                var (success, errorMessage) = await _contactoBL.Insertar(contacto);

                if (success)
                {
                    TempData["SuccessMessage"] = "Contacto creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                    return View(contacto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa controlador: Contacto, método Crear");
                ModelState.AddModelError("", "Ocurrió un error al crear el contacto. Por favor, inténtelo de nuevo.");
                return View(contacto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var (contacto, errorMessage) = await _contactoBL.ObtenerPorId(id);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    TempData["ErrorMessage"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }
                return View(contacto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa controlador: Contacto, método Editar (GET)");
                TempData["ErrorMessage"] = "Ocurrió un error al cargar el contacto para editar.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return View(contacto);
            }

            try
            {
                var (success, errorMessage) = await _contactoBL.Actualizar(contacto);

                if (success)
                {
                    TempData["SuccessMessage"] = "Contacto actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                    return View(contacto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa controlador: Contacto, método Editar (POST)");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el contacto. Por favor, inténtelo de nuevo.");
                return View(contacto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            try
            {
                var (contacto, errorMessage) = await _contactoBL.ObtenerPorId(id);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    TempData["ErrorMessage"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }
                return View(contacto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa controlador: Contacto, método Detalles");
                TempData["ErrorMessage"] = "Ocurrió un error al cargar los detalles del contacto.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var (contacto, errorMessage) = await _contactoBL.ObtenerPorId(id);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    TempData["ErrorMessage"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }
                return View(contacto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa controlador: Contacto, método Eliminar (GET)");
                TempData["ErrorMessage"] = "Ocurrió un error al cargar el contacto para eliminar.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(Contacto contacto)
        {
            try
            {
                var (success, errorMessage) = await _contactoBL.Eliminar(contacto.Id);

                if (success)
                {
                    TempData["SuccessMessage"] = "Contacto eliminado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = errorMessage;
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa controlador: Contacto, método EliminarConfirmado");
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar el contacto. Por favor, inténtelo de nuevo.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
