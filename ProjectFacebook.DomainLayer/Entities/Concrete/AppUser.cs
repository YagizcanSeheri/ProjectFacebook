using Microsoft.AspNetCore.Identity;
using ProjectFacebook.DomainLayer.Entities.Abstraction;
using ProjectFacebook.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectFacebook.DomainLayer.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {
        public AppUser()
        {
            Posts = new List<Post>();
            Shares = new List<Share>();
            Likes = new List<Like>();
            Followers = new List<Follow>();
            Following = new List<Follow>();
            Comments = new List<Comment>();
        }

        public DateTime CreateDate { get => DateTime.Now; private set { } }

        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }

        public string Name { get; set; }
        public string ImagePath { get; set; } = "/images/users/default.jpg"; 

        public List<Comment> Comments { get; set; }
        public List<Share> Shares { get; set; }
        public List<Like> Likes { get; set; }
        public List<Post> Posts { get; set; }

        [InverseProperty("Follower")]
        public List<Follow> Followers { get; set; }

        [InverseProperty("Following")]
        public List<Follow> Following { get; set; }


    }
}
