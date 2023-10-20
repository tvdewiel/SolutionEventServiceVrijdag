using EventServiceBL.Exceptions;
using EventServiceBL.Managers;
using EventServiceBL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventServiceREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private EventManager EM;
        private VisitorManager VM;

        public EventController(EventManager eM, VisitorManager vM) 
        {
            VM = vM;
            EM = eM;
        }

        [HttpGet("{name}")]
        public ActionResult<Event> Get(string name)
        {
            try
            {
                return Ok(EM.GetEvent(name));
            }
            catch (EventModelException) { return NotFound(name); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet]
        [Route("location")]
        public ActionResult<List<Event>> GetWithLocation([FromQuery] string location) 
        { 
            try
            {
                return Ok(EM.GetEventsForLocation(location));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet]
        [Route("date")]
        public ActionResult<List<Event>> GetWithDate([FromQuery] string date)
        {
            try
            {
                return Ok(EM.GetEventsForDate(DateTime.Parse(date)));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        [Route("{name}/visitor")]
        public ActionResult<Event> SubscribeVisitor(string name, [FromBody] int visitorId)
        {
            try
            {
                Visitor visitor = VM.GetVisitor(visitorId);
                Event ev = EM.GetEvent(name);
                EM.SubscribeVisitor(visitor, ev);
                return CreatedAtAction(nameof(Get), new { name = name }, ev);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
