using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using System.Configuration;


namespace CapitalQuotes
{
    using SKAPI;
    class Program
    {
        static void Main(string[] args)
        {
            string skID = ConfigurationManager.AppSettings["SK.User"];
            string skPWD = ConfigurationManager.AppSettings["SK.Password"];
            try
            {
                // Unity.
                var unity = new UnityConfig();
                var container = unity.RegisterDependencies();

                // Inject Logger.
                Logger _logger = container.Resolve<Logger>();

                // Inject CapitalConnection.
                CapitalConnection _capitalConn = container.Resolve<CapitalConnection>();
                int result = _capitalConn.Logon(skID, skPWD).Result;
                if (result != 0)
                {
                    Console.WriteLine(result);
                }
                Thread.Sleep(3000);
                _capitalConn.ConnectToQuoteServer();
                
                
                // Keep console open.
                //Console.ReadLine();
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
