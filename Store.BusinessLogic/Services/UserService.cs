using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.Roles;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private UserManager<Users> _userManager;
        private RoleManager<Roles> _roleManager;
        public UserService(UserManager<Users> userManager,
                           RoleManager<Roles> roleManager,
                           IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public UserModel GetAllUsers(UserFilterData userFilter)
        {
            UserModel userModel = new UserModel();

            Func<Users, bool> predicate = (u => (userFilter.Email == null || u.Email.ToLower().Contains(userFilter.Email.ToLower())) &&
                                                (userFilter.Firstname == null || u.FirstName.ToLower().Contains(userFilter.Firstname.ToLower())) &&
                                                (userFilter.Lastname == null || u.LastName.ToLower().Contains(userFilter.Lastname.ToLower())) &&
                                                (userFilter.Username == null || u.UserName.ToLower().Contains(userFilter.Username.ToLower())) &&
                                                (userFilter.EmailConfirmed == null || u.EmailConfirmed == userFilter.EmailConfirmed) &&
                                                (userFilter.LockoutEnabled == null || u.LockoutEnabled == userFilter.LockoutEnabled) &&
                                                (userFilter.Role == null || u.UserInRoles.Where(uir => uir.Role.Name.ToLower().Contains(userFilter.Role.ToLower())).Any()));

            var usersFromRepo = _userManager.Users.Where(predicate).ToList();
            
            if(usersFromRepo == null)
            {
                userModel.Errors.Add("No users found");
                return userModel;
            }

            foreach (var user in usersFromRepo)
            {
                //Map from Users to UserModelItem
                userModel.Users.Add(_mapper.Map<UserModelItem>(user));
            }

            return userModel;
        }

        public async Task<UserModel> GetUserById(string id)
        {
            var userModel = new UserModel();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                userModel.Errors.Add("User not found");
                return userModel;
            }
            var userItem = _mapper.Map<UserModelItem>(user);
            userItem.Roles = await _userManager.GetRolesAsync(user);

            userModel.Users.Add(userItem);
            return userModel;
        }

        public async Task<UserModel> GetUserByName(string username)
        {
            var userModel = new UserModel();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                userModel.Errors.Add("User not found");
                return userModel;
            }
            var userItem = _mapper.Map<UserModelItem>(user);
            userItem.Roles = await _userManager.GetRolesAsync(user);

            userModel.Users.Add(userItem);
            return userModel;
        }

        public async Task<UserModel> CreateUser(SignUpData signUpData)
        {
            var userModel = new UserModel();
            if (await _userManager.FindByNameAsync(signUpData.Username) != null)
            {
                userModel.Errors.Add($"User with email '{signUpData.Email}' has already created");
                return userModel;
            }

            //Create user 
            var result = await _userManager.CreateAsync(_mapper.Map<Users>(signUpData), signUpData.Password);
            if(!result.Succeeded)
            {
                userModel.Errors.Add($"User with email '{signUpData.Email}' hasn`t created");
                return userModel;
            }

            //Find user
            var user = await _userManager.FindByNameAsync(signUpData.Username);

            //Add user to role
            result = await _userManager.AddToRoleAsync(user, "Client");
            if (!result.Succeeded)
            {
                userModel.Errors.Add($"Role 'Client' doesn`t exists");
                return userModel;
            }

            //Unlock user
            result = await _userManager.SetLockoutEnabledAsync(user, false);
            if (!result.Succeeded)
            {
                userModel.Errors.Add($"Unlock has failed");
                return userModel;
            }

            userModel.Users.Add(_mapper.Map<UserModelItem>(user));
            return userModel;
        }

        public async Task<UserModel> EditUser(SignUpData signUpData)
        {
            var user = await _userManager.FindByEmailAsync(signUpData.Email);
            var userModel = new UserModel();
            if (user == null)
            {
                userModel.Errors.Add($"User with email '{signUpData.Email}' is not found");
                return userModel;
            }
            
            user.FirstName = signUpData.Firstname;
            user.LastName = signUpData.Lastname;
            var result = await _userManager.UpdateAsync(user);
            
            if (!result.Succeeded)
            {
                userModel.Errors.Add($"Updating user '{signUpData.Username}' has failed");
                return userModel;
            }

            userModel.Users.Add(_mapper.Map<UserModelItem>(user));
            return userModel;
        }

        public async Task<UserModel> DeleteUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var userModel = new UserModel();
            var result = await _userManager.DeleteAsync(user);
            
            if(!result.Succeeded)
            {
                userModel.Errors.Add($"User '{username}' is not found");
                return userModel;
            }

            userModel.Users.Add(_mapper.Map<UserModelItem>(user));
            return userModel;
        }

        public async Task<UserModel> BlockUser(string username, bool enabled)
        {
            var user = await _userManager.FindByNameAsync(username);
            var userModel = new UserModel();
            var result = await _userManager.SetLockoutEnabledAsync(user, enabled);
            
            if(!result.Succeeded)
            {
                userModel.Errors.Add($"User '{username}' is not found");
                return userModel;
            }

            userModel.Users.Add(_mapper.Map<UserModelItem>(user));
            return userModel;
        }

        public async Task<RoleModel> CreateRole(RoleModelItem role)
        {
            var rolename = await _roleManager.FindByNameAsync(role.Role);
            var roleModel = new RoleModel();
            var result = await _roleManager.CreateAsync(rolename);
            
            if(!result.Succeeded)
            {
                roleModel.Errors.Add($"Role '{role.Role}' has already created");
                return roleModel;
            }

            roleModel.Roles.Add(role);
            return roleModel;
        }

        public async Task<RoleModel> RemoveRole(RoleModelItem role)
        {
            var rolename = await _roleManager.FindByNameAsync(role.Role);
            var roleModel = new RoleModel();
            var result = await _roleManager.DeleteAsync(rolename);
            if(!result.Succeeded)
            {
                roleModel.Errors.Add($"Role '{role.Role}' is not found");
                return roleModel;
            }

            roleModel.Roles.Add(role);
            return roleModel;
        }

        public async Task<RoleModel> AddUserToRole(UserRoleModelItem userRole)
        {
            var user = await _userManager.FindByNameAsync(userRole.Username);
            var roleModel = new RoleModel();
            if(user == null)
            {
                roleModel.Errors.Add($"User '{userRole.Username}' is not found");
                return roleModel;
            }

            var result = await _userManager.AddToRoleAsync(user, userRole.Role);
            if (!result.Succeeded)
            {
                roleModel.Errors.Add($"Role '{userRole.Role}' is not found");
                return roleModel;
            }

            roleModel.Users.Add(userRole);
            return roleModel;
        }

        public async Task<RoleModel> RemoveUserFromRole(UserRoleModelItem userRole)
        {
            var user = await _userManager.FindByNameAsync(userRole.Username);
            var roleModel = new RoleModel();
            if (user == null)
            {
                roleModel.Errors.Add($"User '{userRole.Username}' is not found");
                return roleModel;
            }

            var result = await _userManager.RemoveFromRoleAsync(user, userRole.Role);
            if (!result.Succeeded)
            {
                roleModel.Errors.Add($"Role '{userRole.Role}' is not found");
                return roleModel;
            }

            roleModel.Users.Add(userRole);
            return roleModel;
        }
    }
}
