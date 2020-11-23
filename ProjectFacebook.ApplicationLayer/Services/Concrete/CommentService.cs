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
    public class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddComment(AddCommentDTO model)
        {
            var comment = _mapper.Map<AddCommentDTO, Comment>(model);

            await _unitOfWork.Comment.Add(comment);
            await _unitOfWork.Commit();
        }
    }
}
