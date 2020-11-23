using ProjectFacebook.ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.ApplicationLayer.Services.Abstraction
{
    public interface ICommentService
    {
        Task AddComment(AddCommentDTO model);
    }
}
