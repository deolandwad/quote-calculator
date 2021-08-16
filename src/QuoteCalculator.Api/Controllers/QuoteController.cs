using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.App.Quotes.Commands;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.App.Quotes.Queries;

namespace QuoteCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteCommand quoteCommand;
        private readonly IQuoteQuery quoteQuery;

        public QuoteController(IQuoteCommand quoteCommand, IQuoteQuery quoteQuery)
        {
            this.quoteCommand = quoteCommand;
            this.quoteQuery = quoteQuery;
        }

        // GET api/<QuoteController>
        [HttpGet]
        public IActionResult Get([FromBody] QuoteDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            quoteQuery.Execute(model);

            return Ok(model);
        }

        // POST api/<QuoteController>
        [HttpPost]
        public IActionResult Post([FromBody] QuoteDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            quoteCommand.Execute(model);

            return NoContent();
        }
    }
}
