using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ODataFunctions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Students : ODataController
    {
        private static readonly List<Student> students = new List<Student>()
        {
            new Student(){Id = Guid.NewGuid(), Name = "Angela Anaconda", Score = 1 },
            new Student(){Id = Guid.NewGuid(), Name = "Berta Bresley", Score = 2 },
            new Student(){Id = Guid.NewGuid(), Name = "Caesar Aiago", Score = 3 },
            new Student(){Id = Guid.NewGuid(), Name = "Donny Denver", Score = 4 },
            new Student(){Id = Guid.NewGuid(), Name = "Esther Ebblebrew", Score = 5 },
            new Student(){Id = Guid.NewGuid(), Name = "Frank Footlong", Score = 6 }
        };

        private static readonly List<Student> teachersPets = students.Where(s => s.Score > 4).ToList();

        private readonly ILogger<Students> logger;

        public Students(ILogger<Students> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Student> Get()
        {
            return students.AsQueryable();
        }

        public SingleResult<Student> Get([FromODataUri] Guid key)
        {
            return SingleResult.Create(students.Where(x => x.Id == key).AsQueryable());
        }

        [HttpGet]
        public IQueryable<Student> TeachersPets()
        {
            return teachersPets.AsQueryable();
        }
    }
}
