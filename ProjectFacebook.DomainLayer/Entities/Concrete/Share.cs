using ProjectFacebook.DomainLayer.Entities.Abstraction;
using ProjectFacebook.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.DomainLayer.Entities.Concrete
{
    public class Share : IBaseEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public DateTime CreateDate { get => DateTime.Now; private set { } }

        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
