using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fundacion.Diplomado.WebApi.Data;
using Fundacion.Diplomado.WebApi.Models;

namespace Fundacion.Diplomado.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private Context _context;

        public StudentController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Student> Get()
        {
            return _context.Students.ToList();
            //return new List<Student>();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}