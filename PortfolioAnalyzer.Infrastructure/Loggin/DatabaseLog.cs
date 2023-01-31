using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Infrastructure.Logging
{
    public class DatabaseLog
    {
        public ObjectId Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
