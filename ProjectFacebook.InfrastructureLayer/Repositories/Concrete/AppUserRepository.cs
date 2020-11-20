using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.DomainLayer.Repositories.Abstraction;
using ProjectFacebook.InfrastructureLayer.Context;
using ProjectFacebook.InfrastructureLayer.Repositories.Concrete.Kernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.InfrastructureLayer.Repositories.Concrete
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
