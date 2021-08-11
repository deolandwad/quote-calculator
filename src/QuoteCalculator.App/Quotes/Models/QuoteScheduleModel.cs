using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteCalculator.App.Quotes.Models
{
    public class QuoteScheduleModel
    {
        public int PaymentNo { get; set; }
        public double Payment { get; set; }
        public double Principal { get; set; }
        public double Interest { get; set; }
        public double Balance { get; set; }
    }
}
