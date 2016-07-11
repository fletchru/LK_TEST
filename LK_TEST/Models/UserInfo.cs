﻿using System;
using System.Runtime.Serialization;

namespace LK_TEST.Models
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public bool? AdvertisingOptIn { get; set; }

        [DataMember]
        public string CountryIsoCode { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public string Locale { get; set; }
    }
}
