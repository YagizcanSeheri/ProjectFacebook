using ProjectFacebook.ApplicationLayer.Models.DTOs;
using ProjectFacebook.ApplicationLayer.Models.VMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.ApplicationLayer.Services.Abstraction
{
    public interface IPostService
    {
        Task<List<TimeLineVM>> GetTimeLine(int userId, int pageIndex);
        Task AddPost(SendPostDTO model);
        Task<PostDetailVM> PostDetail(int id, int userId);
        Task<List<TimeLineVM>> UserPosts(string userName, int id, int pageIndex);
        Task DeletePost(int id, int userId);
    }
}
