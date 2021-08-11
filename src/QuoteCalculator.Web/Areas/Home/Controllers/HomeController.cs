using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.App.Quotes.Commands;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.App.Quotes.Queries;
using QuoteCalculator.Web.Code;

namespace QuoteCalculator.Web.Areas.Home.Controllers
{
    [Menu("Quote")]
    [Area("Home")]
    public class HomeController : BaseController
    {
        private readonly IQuoteCommand quoteCommand;
        private readonly IQuoteQuery quoteQuery;

        public HomeController(IQuoteCommand quoteCommand, IQuoteQuery quoteQuery)
        {
            this.quoteCommand = quoteCommand;
            this.quoteQuery = quoteQuery;
        }

        [HttpGet("")]
        public IActionResult Quote()
        {
            return View();
        }

        [HttpPost("")]
        public IActionResult Quote(QuoteDetailModel model)
        {
            if (ModelState.IsValid)
            {
                quoteQuery.Execute(model);
                return View("Apply", model);
            }

            Failure = "Unable to validate quote. Please try again.";
            return View(model);
        }

        [HttpPost("editquote")]
        public IActionResult EditQuote(QuoteDetailModel model)
        {
            return View(model);
        }

        [HttpPost("apply")]
        public IActionResult Apply(QuoteDetailModel model)
        {
            quoteCommand.Execute(model);
            Success = $"Thank you {model.FullName}! Your loan application is under review.";
            return RedirectToAction("Quote");
        }
    }
}
