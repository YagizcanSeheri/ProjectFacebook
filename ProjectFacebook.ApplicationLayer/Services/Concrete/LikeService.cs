using AutoMapper;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using ProjectFacebook.ApplicationLayer.Services.Abstraction;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.DomainLayer.UnitOfWork.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.ApplicationLayer.Services.Concrete
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Like(LikeDTO model)
        {
            var isLiked = await _unitOfWork.Like.FirstOrDefault(x => x.AppUserId == model.AppUserId && x.PostId == model.PostId);

            if (isLiked == null)
            {
                var like = _mapper.Map<LikeDTO, Like>(model);
                await _unitOfWork.Like.Add(like);
                await _unitOfWork.Commit();
            }
        }

        public async Task UnLike(LikeDTO model)
        {
            var isLiked = await _unitOfWork.Like.FirstOrDefault(x => x.AppUserId == model.AppUserId && x.PostId == model.PostId);
            _unitOfWork.Like.Delete(isLiked);
            await _unitOfWork.Commit();
        }
    }
}
