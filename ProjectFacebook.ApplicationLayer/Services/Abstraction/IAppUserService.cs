using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using ProjectFacebook.ApplicationLayer.Models.VMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFacebook.ApplicationLayer.Services.Abstraction
{
    public interface IAppUserService
    {
        Task DeleteUser(params object[] parameters);
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task LogOut();
        Task<int> UserIdFromName(string username);
        AuthenticationProperties ExternalLogin(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfo();
        Task<SignInResult> ExternalLoginSignIn(string provider, string key);
        Task<IdentityResult> ExternalRegister(ExternalLoginInfo info, ExternalLoginDTO model);
        Task<EditProfileDTO> GetById(int id);
        Task EditUser(EditProfileDTO model);
        Task<ProfileSummaryDTO> GetByName(string UserName);
        Task<List<FollowListVM>> UserFollowings(int id, int pageIndex);
        Task<List<FollowListVM>> UserFollowers(int id, int pageIndex);
        Task<List<SearchUserDTO>> SearchUser(string keyword, int pageIndex);
    }
}
