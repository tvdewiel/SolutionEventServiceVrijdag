using EventServiceBL.Managers;
using EventServiceBL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventServiceREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private VisitorManager VM;

        public VisitorController(VisitorManager vM)
        {
            VM = vM;
        }

        [HttpGet]
        public ActionResult<List<Visitor>> GetAll() 
        { 
            try
            {
                return Ok(VM.GetAllVisitors());
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{id}")]
        public ActionResult<Visitor> Get(int id)
        {
            try
            {
                return Ok(VM.GetVisitor(id));
            }
            catch (Exception ex) { return NotFound(); }
        }
        [HttpPost]
        public ActionResult<Visitor> Post([FromBody] Visitor visitor)
        {
            try
            {
                VM.RegisterVisitor(visitor);
                VM.SubscribeVisitor(visitor);
                return CreatedAtAction(nameof(Get),new { id = visitor.Id }, visitor);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!VM.ExistsVisitor(id))
                {
                    return NotFound();
                }
                VM.UnsubscribeVisitor(VM.GetVisitor(id));
                return NoContent();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Visitor visitor)
        {
            if (visitor.Id!= id) { return BadRequest(); }
            try
            {
                if (VM.ExistsVisitor(id))
                {
                    VM.UpdateVisitor(visitor);
                    return Ok(visitor);
                }
                else return NotFound();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
