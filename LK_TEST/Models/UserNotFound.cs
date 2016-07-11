using System.Runtime.Serialization;

namespace LK_TEST.Models
{
    [DataContract]
    public class UserNotFound
    {
        [DataMember]
        public string Reason { get; set; }
    }
}
