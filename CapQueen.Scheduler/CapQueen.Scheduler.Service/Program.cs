
using Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace CapQueen.Scheduler.Service
{
    class Program
    {
        static void Main()
        {           
            // change from service account's dir to more logical one
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();

                x.SetDescription(Configuration.ServiceDescription);//服务说明
                x.SetDisplayName(Configuration.ServiceDisplayName);//服务显示名称
                x.SetServiceName(Configuration.ServiceName);//服务名称
                x.StartAutomatically();//自动启动
                x.Service(factory =>
                {
                    QuartzServer server = new QuartzServer();
                    server.Initialize();
                    return server;
                });//启动关联服务
            });
        }
    }
}
