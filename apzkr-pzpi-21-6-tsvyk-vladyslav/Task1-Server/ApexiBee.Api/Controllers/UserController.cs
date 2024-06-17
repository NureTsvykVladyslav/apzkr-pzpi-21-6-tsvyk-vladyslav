using ApexiBee.Application.DTO.User;
using ApexiBee.Application.Interfaces;
using ApexiBee.Domain.Models;
using ApexiBee.Persistance.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApexiBee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public UserController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpGet("managersInfo")]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await userManager.GetUsersInRoleAsync("manager");
            foreach (var manager in managers)
            {
                manager.UserAccount = await userService.GetUserAccountById(manager.UserAccountId);
            }

            var managersInfos = managers.Select(e => new { e.Id, e.UserAccountId, e.Email, e.UserName, e.UserAccount.FirstName, e.UserAccount.LastName });
            return Ok(managersInfos);
        }

        [HttpGet("beekeepersInfo")]
        public async Task<IActionResult> GetAllBeekeepers()
        {
            var beekeepers = await userManager.GetUsersInRoleAsync("beekeeper");
            foreach (var beekeeper in beekeepers)
            {
                beekeeper.UserAccount = await userService.GetUserAccountById(beekeeper.UserAccountId);
            }

            var beekeepersInfos = beekeepers.Select(e => new { e.Id, e.UserAccountId, e.Email, e.UserName, e.UserAccount.FirstName, e.UserAccount.LastName });
            return Ok(beekeepersInfos);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel userData)
        {
            var userAccount = new UserAccount()
            {
                Id = userData.UserAccountId,
                FirstName = userData.FirstName,
                LastName = userData.LastName
            };

            await userService.UpdateUser(userAccount);
            return Ok();
        }

        [HttpGet("info/{accountId}")]
        public async Task<IActionResult> GetUserInfoByAccountId(Guid accountId)
        {
            var info = await this.userService.GetUserAccountById(accountId);
            return Ok(info);
        }
    }
}
