using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using LK_TEST.Models;

namespace LK_TEST.Controllers.Tests
{
    [TestClass()]
    public class SyncProfileRequestControllerTests
    {
        [TestMethod()]
        public void PutTest()
        {
            string baseAddress = "http://localhost:9000/";

            WebApp.Start<Startup>(url: baseAddress);

            int result1 = PutUser(baseAddress, Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121"), Guid.NewGuid(), true, "RU", DateTime.Now, "ru");
            int result2 = PutUser(baseAddress, Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121"), Guid.NewGuid(), false, "RU", DateTime.Now, "ru");
            int result3 = PutUser(baseAddress, Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121"), Guid.NewGuid(), false, "RU2", DateTime.Now, "ru");

            int result4 = PutUser(baseAddress, Guid.Parse("B810F630-462E-4188-A16C-FCE751909D96"), Guid.NewGuid(), true, "US", DateTime.Now, "en-US222");

            int result5 = PutUser(baseAddress, Guid.Parse("28A02BFE-784C-4132-9D89-33FD3A947D43"), Guid.NewGuid(), true, "EU2", DateTime.Now, "en");

            Assert.IsTrue(result1 == 200 && result2 == 200 && result3 == 406 && result4 == 406 && result5 == 406);
        }

        public static int PutUser(string baseAddress, Guid userId, Guid requestId, bool advertisingOptIn, string countryIsoCode, DateTime dateModified, string locale)
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

            return (int)response.StatusCode;            
        }
    }
}