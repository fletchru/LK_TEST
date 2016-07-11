﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LK_TESTTests.WcfServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfServiceReference.IUserInfoProvider")]
    public interface IUserInfoProvider {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserInfoProvider/GetUserInfo", ReplyAction="http://tempuri.org/IUserInfoProvider/GetUserInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(LK_TEST.Models.UserNotFound), Action="http://tempuri.org/IUserInfoProvider/GetUserInfoUserNotFoundFault", Name="UserNotFound", Namespace="http://schemas.datacontract.org/2004/07/LK_TEST.Models")]
        LK_TEST.Models.UserInfo GetUserInfo(System.Guid userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserInfoProvider/GetUserInfo", ReplyAction="http://tempuri.org/IUserInfoProvider/GetUserInfoResponse")]
        System.Threading.Tasks.Task<LK_TEST.Models.UserInfo> GetUserInfoAsync(System.Guid userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserInfoProviderChannel : LK_TESTTests.WcfServiceReference.IUserInfoProvider, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserInfoProviderClient : System.ServiceModel.ClientBase<LK_TESTTests.WcfServiceReference.IUserInfoProvider>, LK_TESTTests.WcfServiceReference.IUserInfoProvider {
        
        public UserInfoProviderClient() {
        }
        
        public UserInfoProviderClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserInfoProviderClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserInfoProviderClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserInfoProviderClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public LK_TEST.Models.UserInfo GetUserInfo(System.Guid userId) {
            return base.Channel.GetUserInfo(userId);
        }
        
        public System.Threading.Tasks.Task<LK_TEST.Models.UserInfo> GetUserInfoAsync(System.Guid userId) {
            return base.Channel.GetUserInfoAsync(userId);
        }
    }
}
