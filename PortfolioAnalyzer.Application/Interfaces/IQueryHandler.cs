using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Interfaces
{
    public interface IQueryHandler<T> where T : class
    {
        public IEnumerable<T> Handle(T query);

    }
}
