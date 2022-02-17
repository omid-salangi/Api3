﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }
        //public async Task<bool> AddUser(string username, string fullname, string password, int age ,string email, CancellationToken cancellationToken)
        //{
        //    User user = new User()
        //    {
        //        USerName = username,
        //        FullName = fullname,
        //        Password = password,
        //        age = age,
        //        Email = email
        //    };
        //    try
        //    {
        //        await _context.Users.AddAsync(user, cancellationToken);
        //        await _context.SaveChangesAsync(cancellationToken);
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }

        //    return true;

        //}

        //public async Task<User> GetByUsername(string username, string password, CancellationToken cancellationToken)
        //{
        //    return  await _context.Users.Where(u => u.UserName == username && u.password == password).FirstOrDefaultAsync(cancellationToken);
        //}

        public async Task<bool> Any(string username)
        {
            var res =await _context.Users.Where(u => u.UserName == username.ToUpper()).ToListAsync();
            if (res.Count == 0)
            {
                return false;
            }

            return true;
        }

        //public async Task UpdateSecurityStamp(User user , CancellationToken cancellationToken)
        //{
        //    user.Securitystamp = new Guid();
        //    await _context.SaveChangesAsync(cancellationToken);
        //}

        public async Task<User> GetUserById(string id , CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id, cancellationToken);
        }

        public async Task UpdateLastLogin(string id , CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id, cancellationToken);
            user.lastLoginDate=DateTimeOffset.Now;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
