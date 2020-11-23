using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.ApplicationLayer.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IFollowService _followService;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IFollowService followService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _followService = followService;
        }
        public async Task DeleteUser(params object[] parameters)
        {
            await _unitOfWork.ExecuteSqlRaw("spDeleteUsers {0}", parameters);
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            return result;
        }

        #region ExternalLogin
        public AuthenticationProperties ExternalLogin(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }
        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignIn(string provider, string key)
        {
            return await _signInManager.ExternalLoginSignInAsync(provider, key, isPersistent: false, bypassTwoFactor: true);
        }

        public async Task<IdentityResult> ExternalRegister(ExternalLoginInfo info, ExternalLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            IdentityResult result;
            if (user != null)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }
            else
            {
                model.Principal = info.Principal;
                user = _mapper.Map<AppUser>(model);
                result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                }
            }
            return result;
        }

        #endregion
        public async Task<EditProfileDTO> GetById(int id)
        {
            var user = await _unitOfWork.AppUser.GetById(id);

            return _mapper.Map<EditProfileDTO>(user);
        }
        public async Task EditUser(EditProfileDTO model)
        {
            var user = await _unitOfWork.AppUser.GetById(model.Id);
            if (user != null)
            {
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    user.ImagePath = ("/images/users/" + user.UserName + ".jpg");
                    _unitOfWork.AppUser.Update(user);
                    await _unitOfWork.Commit();
                }

                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    await _userManager.UpdateAsync(user);
                }
                if (model.UserName != user.UserName)
                {
                    var isUserNameExist = await _userManager.FindByNameAsync(model.UserName);

                    if (isUserNameExist == null)
                    {
                        await _userManager.SetUserNameAsync(user, model.UserName);
                        user.UserName = model.UserName;
                        await _signInManager.SignInAsync(user, isPersistent: true);
                    }
                }
                if (model.Name != user.Name)
                {
                    user.Name = model.Name;
                    _unitOfWork.AppUser.Update(user);
                    await _unitOfWork.Commit();
                }
                if (model.Email != user.Email)
                {
                    var isEmailExist = await _userManager.FindByEmailAsync(model.Email);
                    if (isEmailExist == null)
                        await _userManager.SetEmailAsync(user, model.Email);
                }

            }
        }

        public async Task<ProfileSummaryDTO> GetByName(string userName)
        {
            var user = await _unitOfWork.AppUser.GetFilteredFirstOrDefault(
                selector: y => new ProfileSummaryDTO
                {
                    UserName = y.UserName,
                    ImagePath = y.ImagePath,
                    //PostCount = y.Posts.Count,
                    FollowersCount = y.Followers.Count,
                    FollowingsCount = y.Following.Count
                },
                predicate: x => x.UserName == userName);

            return user;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int> UserIdFromName(string userName)
        {
            var user = await _unitOfWork.AppUser.GetFilteredFirstOrDefault(
                selector: x => x.Id,
                predicate: x => x.UserName == userName);

            return user;
        }
        public async Task<List<SearchUserDTO>> SearchUser(string keyword, int pageIndex)
        {
            var users = await _unitOfWork.AppUser.GetFilteredList(
                selector: x => new SearchUserDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    ImagePath = x.ImagePath
                },
                predicate: x => x.UserName.Contains(keyword) || x.Name.Contains(keyword),
                pageIndex: pageIndex,
                pageSize: 10);

            return users;
        }
        public async Task<List<FollowListVM>> UserFollowings(int id, int pageIndex)
        {

            List<int> followings = await _followService.FollowingList(id);

            var followingsList = await _unitOfWork.AppUser.GetFilteredList(selector: y => new FollowListVM
            {
                Id = y.Id,
                ImagePath = y.ImagePath,
                UserName = y.UserName,
            },
                predicate: x => followings.Contains(x.Id),
                include: x => x
               .Include(z => z.Followers),
                pageIndex: pageIndex);
            return followingsList;

        }

        public async Task<List<FollowListVM>> UserFollowers(int id, int pageIndex)
        {

            List<int> followers = await _followService.FollowerList(id);

            var followersList = await _unitOfWork.AppUser.GetFilteredList(selector: y => new FollowListVM
            {
                Id = y.Id,
                ImagePath = y.ImagePath,
                UserName = y.UserName,
            },
                predicate: x => followers.Contains(x.Id),
                include: x => x
               .Include(z => z.Followers),
                pageIndex: pageIndex);
            return followersList;

        }

    }
}
