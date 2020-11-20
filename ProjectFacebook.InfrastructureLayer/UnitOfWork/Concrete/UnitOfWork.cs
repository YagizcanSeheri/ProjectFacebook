using Microsoft.EntityFrameworkCore;
using ProjectFacebook.DomainLayer.Repositories.Abstraction;
using ProjectFacebook.DomainLayer.UnitOfWork.Abstraction;
using ProjectFacebook.InfrastructureLayer.Context;
using ProjectFacebook.InfrastructureLayer.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.InfrastructureLayer.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            this._db = applicationDbContext ?? throw new ArgumentNullException("db can't be null");
        }


        private IPostRepository _postRepository;
        public IPostRepository Post { get { return _postRepository ?? (_postRepository = new PostRepository(_db)); } }



        private ICommentRepository _commentRepository;
        public ICommentRepository Comment { get { return _commentRepository ?? (_commentRepository = new CommentRepository(_db)); } }



        private IAppUserRepository _appUserRepository;
        public IAppUserRepository AppUser { get { return _appUserRepository ?? (_appUserRepository = new AppUserRepository(_db)); } }


        private IFollowRepository _followRepository;
        public IFollowRepository Follow { get { return _followRepository ?? (_followRepository = new FollowRepository(_db)); } }



        private ILikeRepository _likeRepository;
        public ILikeRepository Like { get { return _likeRepository ?? (_likeRepository = new LikeRepository(_db)); } }


        private IShareRepository _shareRepository;
        public IShareRepository Share { get { return _shareRepository ?? (_shareRepository = new ShareRepository(_db)); } }


        private bool isDisposed = false;

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _db.DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }

        public async Task ExecuteSqlRaw(string sql, params object[] parameters)
        {
            await _db.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }
    }
}
