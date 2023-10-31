using CoffesFlavor.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffesFlavor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendaService _relatorioVendaService;

        public AdminRelatorioVendasController(RelatorioVendaService relatorioVendaService)
        {
            _relatorioVendaService = relatorioVendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate,
            DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioVendaService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }

    }
}
