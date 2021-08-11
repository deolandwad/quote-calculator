using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.App.Loans.Commands;
using QuoteCalculator.App.Loans.Models;
using QuoteCalculator.App.Loans.Queries;
using QuoteCalculator.Web;
using QuoteCalculator.Web.Code;

namespace WTWMiniApp.Web.Areas.Contacts.Controllers
{
    [Menu("Loan")]
    [Route("loans")]
    [Area("Loans")]
    public class LoanController : BaseController
    {
        private readonly ILoanCommand loanCommand;
        private readonly ILoanQuery loanQuery;

        public LoanController(ILoanCommand loanCommand, ILoanQuery loanQuery)
        {
            this.loanCommand = loanCommand;
            this.loanQuery = loanQuery;
        }

        [HttpGet]
        public IActionResult List(LoanListModel model)
        {
            loanQuery.Execute(model);
            return View(model);
        }

        [HttpGet("{id}")]
        public IActionResult Detail(LoanDetailModel model)
        {
            loanQuery.Execute(model);
            return View(model);
        }

        [HttpGet("edit/{id?}")]
        public IActionResult Edit(int id)
        {
            var model = new EditLoanModel { Id = id };
            loanQuery.Execute(model);
            return View(model);
        }

        [HttpPost("edit/{id?}")]
        public IActionResult Edit(EditLoanModel model)
        {
            if (ModelState.IsValid)
            {
                loanCommand.Execute(model);
                Success = "Loan has been saved.";
                return RedirectToAction("List");
            }

            Failure = "Unable to save loan. Please try again.";
            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(DeleteLoanModel model)
        {
            loanCommand.Execute(model);
            Success = "Loan has been deleted.";
            return RedirectToAction("List");
        }
    }
}
