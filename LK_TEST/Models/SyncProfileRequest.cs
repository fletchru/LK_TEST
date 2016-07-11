using System;

namespace LK_TEST.Models
{
    public class SyncProfileRequest : MyAccountRequestBase
    {
        public bool? AdvertisingOptIn { get; set; }

        public string CountryIsoCode { get; set; }

        public DateTime DateModified { get; set; }

        public string Locale { get; set; }
    }
}
