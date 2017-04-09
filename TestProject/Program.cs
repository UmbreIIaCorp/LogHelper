using LogService.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject {
    class Program {
        static void Main(string[] args) {
            TextOutput to = new TextOutput();
            LogHelper.Envinronment context = new LogHelper.Envinronment();

            var connect = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=LogService.LogContext;Integrated Security=True");
            var db = new LogService.LogContext(connect);
            
            context.Add("db",db);
            context.Add("entity", new Log());
            context.Add("Target", "ExampleTag");
            context.Add("Date", DateTime.Now);

            to.RegisterWriter(Console.Out);
            to.RegisterWriter(new LogHelper.LogWriter(context));

            to.Write("message 1");
            to.Write("message 2");
            to.Write("message 3");
            to.Write("message 4");
            to.Write("message 5");
            to.Write("message 6");


            Console.ReadKey();
        }
    }
}
