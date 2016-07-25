﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using Mpdp.Data.Extension;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;
using Mpdp.Services.Abstract;
using Mpdp.Services.Utilities;

namespace Mpdp.Services
{
  public class MembershipServices : IMembershipServices
  {
    #region Variables
    private readonly IEntityBaseRepository<User> _userRepository;
    private readonly IEntityBaseRepository<Role> _roleRepository;
    private readonly IEntityBaseRepository<UserRole> _userRoleRepository;
    private readonly IEncryptionServices _encryptionServices;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    public MembershipServices(IEntityBaseRepository<User> userRepository, IEntityBaseRepository<Role> roleRepository,
       IEntityBaseRepository<UserRole> userRoleRepository, IEncryptionServices encryptionServices, IUnitOfWork unitOfWork)
    {
      _userRepository = userRepository;
      _roleRepository = roleRepository;
      _userRoleRepository = userRoleRepository;
      _encryptionServices = encryptionServices;
      _unitOfWork = unitOfWork;
    }

    #region IMembershipService Implementation

    public MembershipContext ValidateUser(string username, string password)
    {
      var membershipCtx = new MembershipContext();

      var user = _userRepository.GetSingleByUsername(username);
      if (user != null && IsUserValid(user, password))
      {
        var userRoles = GetUserRoles(user.Username);
        membershipCtx.User = user;

        var identity = new GenericIdentity(user.Username);
        membershipCtx.Principal = new GenericPrincipal(
            identity,
            userRoles.Select(x => x.Name).ToArray());
      }

      return membershipCtx;
    }

    public User CreateUser(string username, string email, string password, int[] roles)
    {
      var existingUser = _userRepository.GetSingleByUsername(username);

      if (existingUser != null)
      {
        throw new Exception("Username is already in use");
      }

      var passwordSalt = _encryptionServices.CreateSalt();

      var user = new User()
      {
        Username = username,
        Salt = passwordSalt,
        Email = email,
        IsLocked = false,
        HashedPassword = _encryptionServices.EncryptPassword(password, passwordSalt),
        DateCreated = DateTime.Now,    
      };

      _userRepository.Add(user);
      _unitOfWork.Commit();

      if (roles != null || roles.Length > 0)
      {
        foreach (var role in roles)
        {
          AddUserToRole(user, role);
        }
      }

      _unitOfWork.Commit();

      return user;
    }

    public bool UpdatePassword(string username, string oldPassword, string newPassword)
    {
      var existingUser = _userRepository.GetSingleByUsername(username);

      if (IsUserValid(existingUser, oldPassword))
      {
        existingUser.Salt = _encryptionServices.CreateSalt();
        existingUser.HashedPassword = _encryptionServices.EncryptPassword(newPassword, existingUser.Salt);

        _unitOfWork.Commit();

        return true;;
      }

      return false;
    }

    public string ResetPassword(string email)
    {
      User existingUser = _userRepository.FindBy(u => u.Email.Equals(email)).FirstOrDefault();

      //todo: this should be throw an exception
      if (existingUser == null) return null;

      var newPassword = Membership.GeneratePassword(8, 2);
      existingUser.Salt = _encryptionServices.CreateSalt();
      existingUser.HashedPassword = _encryptionServices.EncryptPassword(newPassword, existingUser.Salt);

      _unitOfWork.Commit();

      return newPassword;
    }

    public User GetUser(int userId)
    {
      return _userRepository.GetSingle(userId);
    }

    public List<Role> GetUserRoles(string username)
    {
      List<Role> _roles = new List<Role>();

      var existingUser = _userRepository.GetSingleByUsername(username);

      if (existingUser != null)
      {
        _roles.AddRange(existingUser.UserRoles.Select(role => role.Role));
      }

      return _roles.Distinct().ToList();
    }
    #endregion

    #region Helper Method

    private void AddUserToRole(User user, int roleId)
    {
      var role = _roleRepository.GetSingle(roleId);

      if (role == null)
      {
        throw new ApplicationException("Role doesn't exist");
      }

      var userRole = new UserRole()
      {
        RoleId = role.Id,
        UserId = user.Id
      };
      _userRoleRepository.Add(userRole);
    }

    private bool IsPasswordValid(User user, string password)
    {
      return string.Equals(_encryptionServices.EncryptPassword(password, user.Salt), user.HashedPassword);
    }

    private bool IsUserValid(User user, string password)
    {
      if (IsPasswordValid(user, password))
      {
        return true;
      }

      return false;
    }
    #endregion
  }
}
