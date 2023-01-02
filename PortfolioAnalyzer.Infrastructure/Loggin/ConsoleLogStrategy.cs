﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Infrastructure.Loggin
{
    public class ConsoleLogStrategy : ILogStrategy
    {
        public void WriteLog(StringBuilder message)
        {
            Console.WriteLine(message);
        }
    }
}
