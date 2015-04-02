using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CapQueen.Scheduler
{
    public class HelloJob : IJob
    {
        private static ILog _log = LogManager.GetLogger(typeof(HelloJob)); 
        public void Execute(IJobExecutionContext context)
        {
            var id = Guid.NewGuid().ToString();
            Console.WriteLine(string.Format("Hello World From Hello Job {0} id={1}", DateTime.Now, id));
            Thread.Sleep(12000);
            Console.WriteLine(string.Format("Hello World From Hello Job {0} id={1} END", DateTime.Now, id));

            _log.Error("HelloJob ");
        }
    }
}
