using ProjectFacebook.ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.ApplicationLayer.Services.Abstraction
{
    public interface ILikeService
    {
        Task Like(LikeDTO model);
        Task UnLike(LikeDTO model);
    }
}
