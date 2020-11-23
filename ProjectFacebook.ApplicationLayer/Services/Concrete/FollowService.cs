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
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FollowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Follow(FollowDTO model)
        {
            var isExsistFollow = await _unitOfWork.Follow.FirstOrDefault(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);
            if (isExsistFollow == null)
            {
                var follow = _mapper.Map<FollowDTO, Follow>(model);
                await _unitOfWork.Follow.Add(follow);
                await _unitOfWork.Commit();
            }
        }

        public async Task<List<int>> FollowerList(int id)
        {
            var followerList = await _unitOfWork.Follow.GetFilteredList(
                selector: y => y.FollowerId,
                predicate: x => x.FollowerId == id);

            return followerList;
        }

        public async Task<List<int>> FollowingList(int id)
        {
            var followingList = await _unitOfWork.Follow.GetFilteredList(
                selector: y => y.FollowingId,
                predicate: x => x.FollowingId == id);

            return followingList;
        }

        public async Task<bool> isFollowing(FollowDTO model)
        {
            var isExistFollow = await _unitOfWork.Follow.Any(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);

            return isExistFollow;
        }

        public async Task UnFollow(FollowDTO model)
        {
            var isExsistFollow = await _unitOfWork.Follow.FirstOrDefault(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);

            _unitOfWork.Follow.Delete(isExsistFollow);
            await _unitOfWork.Commit();
        }
    }
}
