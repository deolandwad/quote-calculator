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

        // POST api/<QuoteController>
        [HttpPost]
        public IActionResult Post([FromBody] QuoteDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            quoteQuery.Execute(model);

            return Ok(model);
        }

        // PUT api/<QuoteController>/5
        [HttpPut("{quoteId}")]
        public IActionResult Put(int quoteId, [FromBody] QuoteDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = quoteId;
            quoteCommand.Execute(model);

            return NoContent();
        }
    }
}
