﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobHub.Core.Contracts;
using JobHub.Core.Models.Company;
using JobHub.Infrastructure.Data.Common;
using JobHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobHub.Core.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly IRepository repo;

        public CompanyService(IRepository _repo)
        {
            repo=_repo;
        }

        public async Task Create(AddCompanyViewModel model,string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UserCompanies)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var company = new Company()
            {
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Description = model.Description,
            };
            await repo.AddAsync(company);
            user.UserCompanies.Add(new UserCompany()
            {
                UserId = userId,
                CompanyId = company.Id,
                User = user,
                Company = company
            });
            await repo.SaveChangesAsync();
        }
        
        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await repo.All<Company>()
                .AnyAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<CompanyViewModel>> GetMineAsync(string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UserCompanies)
                .ThenInclude(uc => uc.Company)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UserCompanies
                .Select(uc => new CompanyViewModel()
                {
                    Id = uc.Company.Id,
                    Name = uc.Company.Name,
                    Description = uc.Company.Description
                });
        }
    }
}
