using LK_TEST.Models;
using System;
using System.ServiceModel;

namespace LK_TEST
{
    [ServiceContract]
    public interface IUserInfoProvider
    {
        [OperationContract]
        [FaultContract(typeof(UserNotFound))]
        UserInfo GetUserInfo(Guid userId);
    }
}
