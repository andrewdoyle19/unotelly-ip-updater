using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace UnotellyIpUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<IpAddressUpdaterService>(s =>
                {
                    s.ConstructUsing(name => new IpAddressUpdaterService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("Update your Unotelly Account Automatically with your new ipaddress");
                x.SetDisplayName("Unotelly Ip Address Updater");
                x.SetServiceName("Unotelly IpAddressUpdater");
            });
        }
    }
}
