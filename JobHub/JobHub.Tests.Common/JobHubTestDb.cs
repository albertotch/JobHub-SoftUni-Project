﻿using JobHub.Infrastructure.Data;
using JobHub.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHub.Tests.Common
{
    public  class JobHubTestDb
    {


        public Company FirstCompany { get; set; } = null!;
        public Company SecondCompany { get; set; } = null!;

        public Category SoftwareCategory { get; set; } = null!;
        public Category DesignCategory { get; set; } = null!;

        public Job FirstJob { get; set; } = null!;
        public Job SecondJob { get; set; } = null!;
        public Job ThirdJob { get; set; } = null!;

        private void SeedDatabase(ApplicationDbContext dbContext)
        {
            this.FirstCompany = new Company
            {
                Id = 1,
                Name = "First Company",
                City = "Sofia",
                Description="First Company description",
                Email="company1@gmail.com",
                PhoneNumber="+359891111111",
                Jobs=new List<Job>() { FirstJob,ThirdJob}
            };
            dbContext.Add(this.FirstCompany);

            this.SecondCompany = new Company
            {
                Id = 1,
                Name = "Second Company",
                City = "Blagoevgrad",
                Description = "Second Company description",
                Email = "company2@gmail.com",
                PhoneNumber = "+359892222222",
                Jobs=new List<Job>() { SecondJob}
            };
            dbContext.Add(SecondCompany);

            SoftwareCategory = new Category
            {
                Id = 1,
                Label = "Software"
            };
            dbContext.Add(SoftwareCategory);

            DesignCategory = new Category
            {
                Id = 2,
                Label = "Design"
            };
            dbContext.Add(DesignCategory);

            FirstJob = new Job
            {
                Id = 1,
                Title = "First Job",
                City = "Sofia",
                Description = "First job description",
                Salary = 1000,
                CategoryId = SoftwareCategory.Id,
                Category = SoftwareCategory,
                CompanyId = FirstCompany.Id,
                Company = FirstCompany,
                CreatedDate= DateTime.ParseExact(DateTime.Now.ToString(CultureInfo.InvariantCulture), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            };
            dbContext.Add(FirstJob);

            SecondJob = new Job
            {
                Id = 2,
                Title = "Second Job",
                City = "Blagoevgrad",
                Description = "Second job description",
                Salary = 2000,
                CategoryId = DesignCategory.Id,
                Category = DesignCategory,
                CompanyId = SecondCompany.Id,
                Company = SecondCompany,
                CreatedDate = DateTime.ParseExact(DateTime.Now.ToString(CultureInfo.InvariantCulture), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            };

            dbContext.Add(SecondJob);

            ThirdJob = new Job
            {
                Id = 3,
                Title = "Third Job",
                City = "Varna",
                Description = "Third job description",
                Salary = 3000,
                CategoryId = DesignCategory.Id,
                Category = DesignCategory,
                CompanyId = FirstCompany.Id,
                Company = FirstCompany,
                CreatedDate = DateTime.ParseExact(DateTime.Now.ToString(CultureInfo.InvariantCulture), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            };
            dbContext.Add(ThirdJob);
        }
    }
}