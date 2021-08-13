using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.App.Loans.Commands;
using QuoteCalculator.App.Loans.Models;
using QuoteCalculator.App.Loans.Queries;

namespace QuoteCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanCommand loanCommand;
        private readonly ILoanQuery loanQuery;

        public LoanController(ILoanCommand loanCommand, ILoanQuery loanQuery)
        {
            this.loanCommand = loanCommand;
            this.loanQuery = loanQuery;
        }

        // GET: api/<LoanController>
        [HttpGet]
        public IActionResult Get()
        {
            var model = new LoanListModel();
            loanQuery.Execute(model);

            if (model.Loans.Count == 0)
            {
                return NotFound();
            }

            return Ok(model.Loans);
        }

        // GET api/<LoanController>/5
        [HttpGet("{loanId}")]
        public IActionResult Get(int loanId)
        {
            var model = new LoanDetailModel() { Id = loanId };
            loanQuery.Execute(model);

            return Ok(model);
        }

        // POST api/<LoanController>
        [HttpPost]
        public IActionResult Post([FromBody] EditLoanModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            loanCommand.Execute(model);

            return NoContent();
        }

        // PUT api/<LoanController>/5
        [HttpPut("{loanId}")]
        public IActionResult Put(int loanId, [FromBody] EditLoanModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = loanId;
            loanCommand.Execute(model);

            return NoContent();

        }

        // DELETE api/<LoanController>/5
        [HttpDelete("{loanId}")]
        public IActionResult Delete(int loanId)
        {
            var model = new DeleteLoanModel() { Id = loanId };
            loanCommand.Execute(model);

            return NoContent();
        }
    }
}
