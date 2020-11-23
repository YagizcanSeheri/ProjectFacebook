using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using ProjectFacebook.ApplicationLayer.Models.VMs;
using ProjectFacebook.ApplicationLayer.Services.Abstraction;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.DomainLayer.UnitOfWork.Abstraction;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectFacebook.ApplicationLayer.Services.Concrete
{
    public class PostService:IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFollowService _followService;
        private readonly IAppUserService _appUserService;

        public PostService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IFollowService followService,
                            IAppUserService appUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _followService = followService;
            _appUserService = appUserService;
        }

        public async Task AddPost(SendPostDTO model)
        {
            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                if (image.Width! > 600)
                    image.Mutate(x => x.Resize(600, 0));
                Guid name = Guid.NewGuid();
                image.Save("wwwroot/images/tweets/" + name + ".jpg");
                model.ImagePath = ("/images/tweets/" + name + ".jpg");
            }

            var post = _mapper.Map<SendPostDTO, Post>(model);

            await _unitOfWork.Post.Add(post);
            await _unitOfWork.Commit();
        }

        public async Task DeletePost(int id, int userId)
        {
            var post = await _unitOfWork.Post.FirstOrDefault(x => x.Id == id);

            if (userId == post.AppUserId)
            {
                _unitOfWork.Post.Delete(post);
                await _unitOfWork.Commit();
            }
        }

        public async Task<List<TimeLineVM>> GetTimeLine(int userId, int pageIndex)
        {
            List<int> followings = await _followService.FollowingList(userId);

            var posts = await _unitOfWork.Post.GetFilteredList(
                selector: x => new TimeLineVM
                {
                    Id = x.Id,
                    Text = x.Text,
                    ImagePath = x.ImagePath,
                    AppUserId = x.AppUserId,
                    LikeCounts = x.Likes.Count,
                    CommentCounts = x.Comments.Count,
                    ShareCounts = x.Shares.Count,
                    CreateDate = x.CreateDate,
                    UserName = x.AppUser.UserName,
                    UserImage = x.AppUser.ImagePath,
                    isLiked = x.Likes.Any(y => y.AppUserId == userId)
                },
                orderBy: x => x.OrderByDescending(y => y.CreateDate),
                predicate: x => followings.Contains(x.AppUserId),
                include: x => x.Include(z => z.AppUser).ThenInclude(z => z.Followers).Include(z => z.Likes),
                pageIndex: pageIndex);

            return posts;
        }

        public Task<PostDetailVM> PostDetail(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeLineVM>> UserPosts(string userName, int id, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
