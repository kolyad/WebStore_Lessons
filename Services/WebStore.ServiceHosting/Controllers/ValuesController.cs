using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.TestWebApi)]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _Values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value - {i:00}")
            .ToList();

        [HttpGet] // GET http://localhost:5001/api/values
        public IEnumerable<string> Get() => _Values;


        [HttpGet("{id}")] // GET http://localhost:5001/api/values/5
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            if (id >= _Values.Count)
            {
                return NotFound();
            }

            return _Values[id];
        }

        [HttpPost] // POST http://localhost:5001/api/values
        [HttpPost("add")] // POST // http://localhost:5001/api/values/add
        public ActionResult Post([FromBody] string value)
        {
            _Values.Add(value);
            return CreatedAtAction(nameof(Get), new { id = _Values.Count - 1 });
        }

        [HttpPut("{id}")] // PUT http://localhost:5001/api/values/5
        [HttpPut("edit/{id}")] // PUT http://localhost:5001/api/values/edit/5
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            if (id >= _Values.Count)
            {
                return NotFound();
            }
            _Values[id] = value;
            return Ok();
        }

        [HttpDelete("{id}")] // DELETE http://localhost:5001/api/values/5
        [HttpDelete("delete/{id}")] // DELETE http://localhost:5001/api/values/delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            if (id >= _Values.Count)
            {
                return NotFound();
            }
            _Values.RemoveAt(id);
            return Ok();
        }
    }
}
