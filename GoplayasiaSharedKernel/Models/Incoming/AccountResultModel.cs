using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models.Incoming
{
    public class AccountResultModel
    {
        public UserModel User { get; set; }
        public UserModel ParentUser { get; set; }
        public List<UserModel> Users { get; set; }
        public List<RegionModel> Regions { get; set; }
        public List<NationalityModel> Nationalities { get; set; }
        public List<NatureOfWorkModel> NaturesOfWork { get; set; }
        public List<SourceOfIncomeModel> SourcesOfIncome { get; set; }
    }
}
