using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Fundacion.Diplomado.Web.Models;
using Fundacion.Diplomado.Web.Helper;

namespace Fundacion.Diplomado.Web.Controllers
{
    public class HomeController : Controller
    {
        StudentAPI _api = new StudentAPI();

        public async Task<IActionResult> Index()
        {
            List<Student> student = new List<Student>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Student");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<List<Student>>(result);
            }

            return View(student);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
