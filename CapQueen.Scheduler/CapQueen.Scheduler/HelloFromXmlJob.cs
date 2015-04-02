using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapQueen.Scheduler
{
    public class HelloFromXmlJob : IJob
    {
        private static ILog _log = LogManager.GetLogger(typeof(HelloFromXmlJob)); 
        public void Execute(IJobExecutionContext context)
        {
            _log.Info(string.Format("HelloFromXmlJob {0}", DateTime.Now));
            throw new Exception("What's up");
        }
    }
}
