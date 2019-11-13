using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ApplicationContext _db;
        private IUserService _userService;

        public AccountController(ApplicationContext db, 
                                 UserManager<Users> um, 
                                 RoleManager<Roles> rm, 
                                 SignInManager<Users> sim)
        {
            _userService = new UserService(db, um, rm, sim);
        }

        [HttpGet]
        public async Task<IEnumerable<UserModelItem>> GetUsers()
        {
            return _userService.GetAllUsers().Items;
        }
    }
}