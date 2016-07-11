using System;
using System.Linq;
using LK_TEST.Controllers;
using LK_TEST.Models;
using System.ServiceModel;
using Serilog;

namespace LK_TEST
{
    public class UserInfoProvider : IUserInfoProvider
    {
        public UserInfo GetUserInfo(Guid userId)
        {
            var userInfo = SyncProfileRequestController.usersList.FirstOrDefault(x => x.UserId == userId);

            if (userInfo != null)
            {
                UserInfo result = new UserInfo();

                result.UserId = userInfo.UserId;
                result.AdvertisingOptIn = userInfo.AdvertisingOptIn;
                result.CountryIsoCode = userInfo.CountryIsoCode;
                result.DateModified = userInfo.DateModified;
                result.Locale = userInfo.Locale;

                Log.Information("Information of user {@User} successfully retrived", result);
                return result;
            }
            else
            {
                UserNotFound fault = new UserNotFound();
                                
                fault.Reason = "User not found with ID: " + userId;

                Log.Error(fault.Reason);
                throw new FaultException<UserNotFound>(fault, fault.Reason);
            }                       
        }
    }    
}