using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
  // Attribute based routing (mapping endpoint routing)
  [Route("api/[controller]")] // http:localhost:5000/api/values
  [ApiController]
  public class ValuesController : ControllerBase
  {
    // Need to inject datacontext into class to access database values 
    private readonly DataContext _context;
    public ValuesController(DataContext context)
    {
      _context = context;
    }

    // GET api/values
    [HttpGet]
    public IActionResult GetValues() // Using IAction to remove enumerable output
    {
      // Using local instantiated variable to store the database and access it as a 
      // list to access the database
      var values = _context.Values.ToList();

      // Return Http 200 Ok response with values
      return Ok(values);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public IActionResult GetValue(int id)
    {
      // Using First or default to return either null or the first possible value
      var value = _context.Values.FirstOrDefault(x => x.Id == id);

      return Ok(value);
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
