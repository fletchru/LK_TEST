using LK_TEST.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Serilog;

namespace LK_TEST.Controllers
{
    public class SyncProfileRequestController : ApiController
    {
        public static List<SyncProfileRequest> usersList = new List<SyncProfileRequest>();

        // PUT
        public object Put([FromBody]SyncProfileRequest user)
        {
            if (usersList.Any(x => x.UserId == user.UserId))
            {
                // Update
                try
                {
                    RegionInfo userRI = new RegionInfo(user.CountryIsoCode);                    
                }
                catch (System.ArgumentException)
                {
                    Log.Error("Failed update of the user {@User}. Incorrect format of CountryIsoCode: {A}", user, user.CountryIsoCode);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }

                try
                {                    
                    CultureInfo userCI = new CultureInfo(user.Locale);
                }
                catch (System.ArgumentException)
                {
                    Log.Error("Failed update of the user {@User}. Incorrect format of Locale: {A}", user, user.Locale);                    
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }

                var oldUser = usersList.FirstOrDefault(x => x.UserId == user.UserId);

                oldUser.RequestId = user.RequestId;
                oldUser.AdvertisingOptIn = user.AdvertisingOptIn;
                oldUser.CountryIsoCode = user.CountryIsoCode;
                oldUser.DateModified = user.DateModified;
                oldUser.Locale = user.Locale;

                Log.Information("User {A} updated to {@User}", user.UserId, user);
                return Request.CreateResponse(HttpStatusCode.OK, new { user = user });
            }
            else
            {
                // Add
                try
                {
                    RegionInfo userRI = new RegionInfo(user.CountryIsoCode);
                }
                catch (System.ArgumentException)
                {
                    Log.Error("Failed adding of a new user {@User}. Incorrect format of CountryIsoCode: {A}", user, user.CountryIsoCode);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }

                try
                {
                    CultureInfo userCI = new CultureInfo(user.Locale);
                }
                catch (System.ArgumentException)
                {
                    Log.Error("Failed adding of a new of user {@User}. Incorrect format of Locale: {A}", user, user.Locale);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }

                usersList.Add(user);
                Log.Information("New user {A} added {@User}", user.UserId, user);
                return Request.CreateResponse(HttpStatusCode.OK, new { user = user });
            }            
        }
    }
}