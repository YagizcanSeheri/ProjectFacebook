using ProjectFacebook.DomainLayer.Entities.Abstraction;
using ProjectFacebook.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectFacebook.DomainLayer.Entities.Concrete
{
    public class Follow : IBaseEntity
    {
        public DateTime CreateDate { get => DateTime.Now; private set { } }

        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }

        public int FollowerId { get; set; }
        [ForeignKey("FollowerId")]
        [InverseProperty("Followers")]
        public AppUser Follower { get; set; }


        public int FollowingId { get; set; }
        [ForeignKey("FollowingId")]
        [InverseProperty("Following")]
        public AppUser Following { get; set; }

    }
}
