using ProjectFacebook.ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.ApplicationLayer.Services.Abstraction
{
    public interface IFollowService
    {
        Task Follow(FollowDTO model);
        Task UnFollow(FollowDTO model);
        Task<bool> isFollowing(FollowDTO model);

        Task<List<int>> FollowingList(int id);
        Task<List<int>> FollowerList(int id);
    }
}
