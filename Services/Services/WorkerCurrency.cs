using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.Microservices.BackgroundWorkers;
using Common.Utilities;
using Data;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCrontab;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Services
{
    public class WorkerCurrency : ScheduledHostedServiceBase
    {

        // static IAPI proxyCheckerAPI;
        // static IAPI proxyCheckerAPI_2;
        // public List<ProxyCheckerLib.Classes.Proxy> ProxiesChecked;

        public WorkerCurrency(IServiceScopeFactory serviceScopeFactory, ILogger<WorkerCurrency> logger)
            :base(serviceScopeFactory, logger)
        {

        }

        protected override string Schedule => "* */8 * * * *";

        protected override string DisplayName => "WorkerCurrency";

        protected override bool IsExecuteOnServerRestart => false;

        protected override bool IncludingSeconds => true;

        protected override async Task<Task> ProcessInScopeAsync(IServiceProvider serviceProvider, CancellationToken token)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                // HttpClient client = new HttpClient();
                // HttpResponseMessage response = await client.GetAsync("https://freecurrencyapi.net/api/v2/latest?apikey=111e0970-7d82-11ec-81d8-3171ddb98b11");
                // if (response.IsSuccessStatusCode)
                // {
                //     response.EnsureSuccessStatusCode();
                //     string responseBody = await response.Content.ReadAsStringAsync();
                //     dynamic dyn = JsonConvert.DeserializeObject(responseBody);
                //     Dictionary<string, double> values = JsonConvert.DeserializeObject<Dictionary<string, double>>(dyn.data.ToString());
                //     List<Currency> currencies = new List<Currency>();
                //     currencies.Add(new Currency()
                //     {
                //         Code = "USD",
                //         Value = 1
                //     });
                //     foreach (var item in values)
                //     {
                //         Console.WriteLine(item.Key);
                //         Console.WriteLine(item.Value);
                //         
                //         currencies.Add(new Currency()
                //         {
                //             Code = item.Key,
                //             Value = item.Value
                //         });
                //     }
                //     
                //     
                // }
                //
                
                
                
                
                //DB PROXY CHECK
                // List<Proxy> db_proxies = context.Proxies.ToList();
                // List<string> proxiesListForCheck = new List<string>();
                // foreach (Proxy jsonProxy in db_proxies)
                // {
                //     proxiesListForCheck.Add($"{jsonProxy.Ip}:{jsonProxy.Port}");
                // }
                //
                // await ProxyChecker_Async(proxiesListForCheck);
                //
                // Console.WriteLine("End db proxy checking");
                //
                //
                // //NEW PROXY
                // HttpClient client = new HttpClient();
                // HttpResponseMessage response = await client.GetAsync("https://www.proxyscan.io/api/proxy?format=json&last_check=5000&uptime=30&ping=300&limit=20&type=http,https");
                // response.EnsureSuccessStatusCode();
                // string responseBody = await response.Content.ReadAsStringAsync();
                // List<JsonProxyItem> proxyItems = JsonConvert.DeserializeObject<List<JsonProxyItem>>(responseBody.ToString());
                //
                //
                // List<string> proxiesList = new List<string>();
                // foreach(JsonProxyItem jsonProxy in proxyItems)
                // {
                //     proxiesList.Add($"{jsonProxy.Ip}:{jsonProxy.Port}");
                // }
                //
                // await ProxyChecker_2_Async(proxiesList);


                //Console.WriteLine("End update currency");
            }


            return Task.CompletedTask;
        }

        //
        // private async Task ProxyChecker_Async(List<string> proxiesList)
        // {
        //     try
        //     {
        //         proxyCheckerAPI = API.CreateBuilder().SetProxyList(proxiesList).Build();
        //         proxyCheckerAPI.ProxyListWorked.CollectionChanged += ProxyListWorked_CollectionChanged;
        //         await proxyCheckerAPI.TestProxy();
        //     }
        //     catch (System.Net.WebException e)
        //     {
        //         Console.WriteLine($"Message: {e.Message}\nSourse: {e.Source}");
        //         await ProxyChecker_Async(proxiesList);
        //     }
        // }
        //
        // private void ProxyListWorked_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        // {
        //     var lastElement = proxyCheckerAPI.ProxyListWorked.Last();
        //
        //     if (lastElement.proxyStatus == ProxyCheckerLib.Enums.ProxyStatus.Dead)
        //     {
        //
        //         using (var context = new ApplicationDbContext())
        //         {
        //             var proxy = context.Proxies.FirstOrDefault(i => i.Ip == lastElement.Address && i.Port == lastElement.Port);
        //             context.Proxies.Remove(proxy);
        //             context.SaveChanges();
        //         }
        //
        //     }
        // }
        //
        // private async Task ProxyChecker_2_Async(List<string> proxiesList)
        // {
        //     try
        //     {
        //         proxyCheckerAPI_2 = API.CreateBuilder().SetProxyList(proxiesList).Build();
        //         proxyCheckerAPI_2.ProxyListWorked.CollectionChanged += ProxyListWorked_CollectionChanged_2;
        //         await proxyCheckerAPI_2.TestProxy();
        //     }
        //     catch (System.Net.WebException e)
        //     {
        //         Console.WriteLine($"Message: {e.Message}\nSourse: {e.Source}");
        //         await ProxyChecker_2_Async(proxiesList);
        //     }  
        // }
        //
        // private void ProxyListWorked_CollectionChanged_2(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        // {
        //     var lastElement = proxyCheckerAPI_2.ProxyListWorked.Last();
        //
        //     if (lastElement.proxyStatus == ProxyCheckerLib.Enums.ProxyStatus.Ok && lastElement.Time <= 500)
        //     {
        //
        //         using (var context = new ApplicationDbContext())
        //         {
        //
        //             int dbProxy = context.Proxies.Where(i => i.Ip == lastElement.Address && i.Port == lastElement.Port).Count();
        //
        //             if (dbProxy == 0)
        //             {
        //                 //New proxy
        //
        //                 Proxy proxyNew = new()
        //                 {
        //                     Country = lastElement.Country,
        //                     Ip = lastElement.Address,
        //                     Port = lastElement.Port,
        //                     Anonymous = lastElement.proxyAnonymous,
        //                     LastSpeed = lastElement.proxySpeed,
        //                     LastStatus = lastElement.proxyStatus,
        //                     ProxyVersion = lastElement.proxyVersion,
        //                     AddProxyToDb = DateTime.Now
        //                 };
        //
        //                 context.Add(proxyNew);
        //                 context.SaveChanges();
        //             }
        //
        //         }
        //
        //     }
        // }

    }
}