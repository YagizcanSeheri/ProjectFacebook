using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Models.DTOs
{
    public class ProfileSummaryDTO
    {
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }
        
        
        //public int PostCount { get; set; }
    }
}
