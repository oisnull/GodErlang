using GodErlang.Entity.Extends;
using GodErlang.Entity.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using GodErlang.Entity;
using GodErlang.Common;

namespace GodErlang.Service
{
    public class UserService : BaseService
    {
        public UserService(GodErlangEntities db) : base(db)
        {
        }

        public SessionUser Login(string account, string password)
        {
            if (string.IsNullOrEmpty(account?.Trim()))
                throw new Exception("Account name can't be empty");
            if (string.IsNullOrEmpty(password?.Trim()))
                throw new Exception("Account password can't be empty");

            IQueryable<User> query = db.User;
            if (CommonTools.isValidPhone(account))
            {
                query = query.Where(u => u.PhoneNum == account);
            }
            else if (CommonTools.isValidEmail(account))
            {
                query = query.Where(u => u.Email == account);
            }
            else
            {
                throw new Exception($"The account name of {account} is invaild.");
            }

            User user = query.FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User does not exist.");
            }
            if (user.Password != CommonTools.GetMd5Str32(password))
            {
                throw new Exception("User password error.");
            }

            return new SessionUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNum = user.PhoneNum,
            };
        }

        public User Save(string account, string password)
        {
            User user = new User()
            {
                //Id = 0,
                Name = $"GE-{Common.RandomText.GetNum(6)}",
                Password = CommonTools.GetMd5Str32(password),
                Sex = UserSex.Unknow,
                State = UserState.Normal,
                CreateTime = DateTime.Now,
                //HeadImage = null
            };

            IQueryable<User> query = db.User;
            if (CommonTools.isValidPhone(account))
            {
                user.PhoneNum = account;
                query = query.Where(u => u.PhoneNum == account);
            }
            else if (CommonTools.isValidEmail(account))
            {
                user.Email = account;
                query = query.Where(u => u.Email == account);
            }
            else
            {
                throw new Exception($"The account name of {account} is invaild.");
            }

            if (query.Count() > 0)
            {
                throw new Exception($"The account name of {account} already exists.");
            }

            db.User.Add(user);
            db.SaveChanges();

            return user;
        }
    }
}
