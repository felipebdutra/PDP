using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Infrastructure.Logging
{
    public class LogBuilder
    {
        private StringBuilder _stringBuilder = new();

        private ILogStrategy _logStrategy;

        public LogBuilder()
        {

        }

        public LogBuilder(ILogStrategy logStrategy)
        {
            _logStrategy = logStrategy;
        }

        public void Append(string message)=> _stringBuilder.Append(message);
        
        public void Clear() => _stringBuilder.Clear();

        public void WriteLog(ILogStrategy logStrategy = null, string message = null)
        {
            if(logStrategy is not null)
                _logStrategy = logStrategy;

            if(!string.IsNullOrEmpty(message))
            {
                WriteLog(message);
                return;
            }

            _logStrategy.WriteLog(_stringBuilder);
        
            _stringBuilder.Clear();
        }

        public void WriteLog(string message)
        {
            _stringBuilder.Append(message);
            _logStrategy.WriteLog(_stringBuilder);
            _stringBuilder.Clear();
        }
    }
}
