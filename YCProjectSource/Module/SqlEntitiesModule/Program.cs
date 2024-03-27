using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Module.DTOsModule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .Configure( app=> app.Run(context => 
                        context.Response.WriteAsync("Hello World"))))
                    .Build()
                    .Run();
        }
    }
}