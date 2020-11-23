using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Models.DTOs
{
    public class AddCommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AppUserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
