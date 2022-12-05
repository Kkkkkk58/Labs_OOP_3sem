using System.Globalization;
using Banks.Console;
using Banks.Console.ReadWrite;
using Banks.Console.ReadWrite.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Services;
using Banks.Services.Abstractions;
using Banks.Tools;
using Banks.Tools.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();
using IServiceScope scope = host.Services.CreateScope();

IServiceProvider services = scope.ServiceProvider;
try
{
    services.GetRequiredService<App>().Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddScoped<IFastForwardingClock, BasicFastForwardingClock>(_ =>
                new BasicFastForwardingClock(DateTime.Now));
            services.AddScoped<ICentralBank, CentralBank>(x =>
                new CentralBank(x.GetRequiredService<IFastForwardingClock>()));
            services.AddTransient<IAccountFactory, BankAccountFactory>(x =>
                new BankAccountFactory(x.GetRequiredService<IFastForwardingClock>(), new GregorianCalendar()));
            services.AddTransient<IReader, BasicConsoleReader>();
            services.AddTransient<IWriter, BasicConsoleWriter>();
            services.AddScoped<App>();
        });
}