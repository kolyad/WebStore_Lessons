using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService _valuesService;

        public WebAPIController(IValuesService valuesService)
        {
            _valuesService = valuesService;
        }

        public IActionResult Index()
        {
            var values = _valuesService.Get();
            return View(values);
        }
    }
}
