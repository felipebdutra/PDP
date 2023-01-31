using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Infrastructure.Logging
{
    public interface ILogStrategy
    {
        void WriteLog(StringBuilder message);
    }
}
