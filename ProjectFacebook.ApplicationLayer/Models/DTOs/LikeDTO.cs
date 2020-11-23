using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Models.DTOs
{
    public class LikeDTO
    {
        public int AppUserId { get; set; }
        public int PostId { get; set; }
        public bool isExist { get; set; }
    }
}
