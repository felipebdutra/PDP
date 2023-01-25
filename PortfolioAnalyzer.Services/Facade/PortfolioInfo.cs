using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Services.Facade
{
    public class PortfolioInfo
    {
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalPortfolio { get; set; }
    }
}
