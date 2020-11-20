using ProjectFacebook.DomainLayer.Entities.Abstraction;
using ProjectFacebook.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.DomainLayer.Entities.Concrete
{
    public class Post : IBaseEntity
    {
        public Post()
        {
            Likes = new List<Like>();
            Shares = new List<Share>();
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<Like> Likes { get; set; }
        public List<Share> Shares { get; set; }
        public List<Comment> Comments { get; set; }

        public DateTime CreateDate { get => DateTime.Now; private set { } }

        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
