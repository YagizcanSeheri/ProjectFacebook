using ProjectFacebook.DomainLayer.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.DomainLayer.UnitOfWork.Abstraction
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IPostRepository Post { get; }
        IFollowRepository Follow { get; }
        IAppUserRepository AppUser { get; }
        ILikeRepository Like { get; }
        IShareRepository Share { get; }
        ICommentRepository Comment { get; }



        Task ExecuteSqlRaw(string sql, params object[] parameters);


        Task Commit();
    }
}
