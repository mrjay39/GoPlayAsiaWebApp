using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models.Incoming
{
    public class MobileAppVersionResultModel
    {
        public string LatestPlayerVersion { get; set; }
        public string LatestAdminVersion { get; set; }
        public string PlayerLink { get; set; }
        public string AdminLink { get; set; }
    }
}
