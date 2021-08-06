using Microsoft.AspNetCore.Mvc;
using MyNETCoreAPI.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Controllers
{
    /// <summary>
    /// This class is to force the database context constructor to trigger and create our database the first time.
    /// </summary>
    [ApiController]
    [Route("api/testdatabase")]
    public class DummyController:ControllerBase
    {
        private readonly CityInfoContext _ctx;

        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
