using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fundacion.Diplomado.WebApi.Models;

namespace Fundacion.Diplomado.WebApi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) :base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
