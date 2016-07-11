using LK_TEST.Models;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Description;
using Serilog;

namespace LK_TEST
{
    public class Program
    {
        static void Main()
        {
            // Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile("logs\\myapp-{Date}.txt")
                .CreateLogger();

            // Start OWIN host
            string baseAddress = "http://localhost:9000/";
            WebApp.Start<Startup>(url: baseAddress);

            // Users
            PutUser(baseAddress, Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121"), Guid.NewGuid(), true, "RU", DateTime.Now, "ru");
            PutUser(baseAddress, Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121"), Guid.NewGuid(), false, "RU", DateTime.Now, "ru");

            PutUser(baseAddress, Guid.Parse("B810F630-462E-4188-A16C-FCE751909D96"), Guid.NewGuid(), true, "US", DateTime.Now, "en-US222");
            PutUser(baseAddress, Guid.Parse("28A02BFE-784C-4132-9D89-33FD3A947D43"), Guid.NewGuid(), true, "EU2", DateTime.Now, "en");

            // Create the ServiceHost.
            Uri baseWcfAddress = new Uri("http://localhost:8000");

            using (ServiceHost host = new ServiceHost(typeof(UserInfoProvider), baseWcfAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                //host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                //Console.WriteLine("The service is ready at {0}", baseWcfAddress);
                //Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();

                Log.CloseAndFlush();
            }            
        }

        private static void PutUser(string baseAddress, Guid userId, Guid requestId, bool advertisingOptIn, string countryIsoCode, DateTime dateModified, string locale)
        {
            // Create HttpCient and make a request
            HttpClient client = new HttpClient();

            SyncProfileRequest syncProfileRequest = new SyncProfileRequest();
            syncProfileRequest.UserId = userId;
            syncProfileRequest.RequestId = requestId;
            syncProfileRequest.AdvertisingOptIn = advertisingOptIn;
            syncProfileRequest.CountryIsoCode = countryIsoCode;
            syncProfileRequest.DateModified = dateModified;
            syncProfileRequest.Locale = locale;

            var response = client.PutAsJsonAsync(baseAddress + "import.json", syncProfileRequest).Result;

            //Console.WriteLine(response);
            //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }
}
