using Microsoft.AspNetCore.Mvc;

namespace QuoteCalculator.Web.Code
{
    public abstract class BaseController : Controller
    {
        public string Success { set => TempData["Success"] = value; get => TempData["Success"]?.ToString(); }
        public string Failure { set => TempData["Failure"] = value; get => TempData["Failure"]?.ToString(); }
    }
}
