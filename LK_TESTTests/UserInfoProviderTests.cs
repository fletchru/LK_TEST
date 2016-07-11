using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using System;
using LK_TEST.Controllers.Tests;
using System.ServiceModel;
using LK_TEST.Models;
using System.ServiceModel.Description;
using LK_TESTTests.WcfServiceReference;

namespace LK_TEST.Tests
{
    [TestClass()]
    public class UserInfoProviderTests
    {
        [TestMethod()]
        public void GetUserInfoTest()
        {
            // System.Web.Http (ApiController)
            string baseAddress = "http://localhost:9000/";
            WebApp.Start<Startup>(url: baseAddress);

            Guid userId = Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121");            
            bool advertisingOptIn = true;
            string countryIsoCode = "RU";
            DateTime dateModified = DateTime.Now;
            string locale = "ru";

            int result1 = SyncProfileRequestControllerTests.PutUser(baseAddress, userId, Guid.NewGuid(), advertisingOptIn, countryIsoCode, dateModified, locale);

            // WCF
            Uri baseWcfAddress = new Uri("http://localhost:8000");

            ServiceHost host = new ServiceHost(typeof(UserInfoProvider), baseWcfAddress);

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
            
            // WCF client           
            UserInfoProviderClient client = new UserInfoProviderClient();
            
            // Get data test
            string result2 = "";
            var answer1 = client.GetUserInfo(Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121"));
            if (userId == answer1.UserId &&                
                advertisingOptIn == answer1.AdvertisingOptIn &&
                countryIsoCode == answer1.CountryIsoCode &&
                dateModified == answer1.DateModified &&
                locale == answer1.Locale)
            {
                result2 = "OK";
            }

            // Get error with wrong user ID
            string result3 = "";
            try
            {
                client.GetUserInfo(Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8120"));
            }
            catch (FaultException<UserNotFound> e)
            {
                if (e.Reason.ToString().Contains("User not found with ID: cc06a00e-74ef-4df5-93de-7ebfbe4c8120"))
                {
                    result3 = "OK";
                }
            }

            // Test of user update
            bool advertisingOptInUpdated = false;
            string countryIsoCodeUpdated = "US";
            DateTime dateModifiedUpdated = DateTime.Now;
            string localeUpdated = "en-US";

            int result4 = SyncProfileRequestControllerTests.PutUser(baseAddress, userId, Guid.NewGuid(), advertisingOptInUpdated, countryIsoCodeUpdated, dateModifiedUpdated, localeUpdated);

            string result5 = "";
            var answer2 = client.GetUserInfo(Guid.Parse("CC06A00E-74EF-4DF5-93DE-7EBFBE4C8121"));
            if (userId == answer2.UserId &&
                advertisingOptInUpdated == answer2.AdvertisingOptIn &&
                countryIsoCodeUpdated == answer2.CountryIsoCode &&
                dateModifiedUpdated == answer2.DateModified &&
                localeUpdated == answer2.Locale)
            {
                result5 = "OK";
            }

            //Console.WriteLine("The service is ready at {0}", baseWcfAddress);
            //Console.WriteLine("Press <Enter> to stop the service.");
            //Console.ReadLine();

            // Close the ServiceHost.
            host.Close();

            Assert.IsTrue(result1 == 200 && result2 == "OK" && result3 == "OK" && result4 == 200 && result5 == "OK");
        }
    }
}