using DAL.Domain;
using DAL.Infrastructure;
using DAL.Infrastructure.Repositories;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineForum.UnitTests.RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public async Task UserRepository_GetAll_ReturnsAllValuesAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new UserRepository(context);

                var users = await repo.GetAllAsync();

                Assert.AreEqual(2, users.Count());
            }
        }

        [Test]
        public async Task UserRepository_GetById_ReturnsSingleValueAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new UserRepository(context);

                var user = await repo.GetByIdAsync(15);

                Assert.AreEqual(15, user.Id);
                Assert.AreEqual("admin", user.ApplicationUser.UserName);
                Assert.AreEqual(1000, user.Rating);
                Assert.AreEqual("dinaiovcheva@gmail.com", user.ApplicationUser.Email);
            }
        }

        [Test]
        public async Task UserRepository_CreateAsync_AddsValueInDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new UserRepository(context);
                string id = Guid.NewGuid().ToString();

                context.Users.Add(new ApplicationUser
                {
                    Id = id,
                    UserName = "usernew",
                    NormalizedUserName = "USERNEW",
                    Email = "dinaiovcheva@gmail.com",
                    NormalizedEmail = "DINAIOVCHEVA@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "auser",
                    SecurityStamp = Guid.NewGuid().ToString()

                });

                await repo.CreateAsync(new UserProfile()
                {
                    Id = 20,
                    IsActive = true,
                    Rating = 100,
                    RegistrationDate = DateTime.Now,
                    ApplicationUserId = id
                });

                await context.SaveChangesAsync();

                Assert.AreEqual (3, context.UserProfiles.Count());
                Assert.AreEqual(3, context.Users.Count());
            }
        }

        [Test]
        public async Task UserRepository_DeleteAsync_DeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new UserRepository(context);
                var user = await repo.GetByIdAsync(15);

                repo.Remove(user);
                await context.SaveChangesAsync();

                Assert.AreEqual(1, context.UserProfiles.Count());
            }
        }

        [Test]
        public async Task UserRepository_UpdateAsync_DeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new UserRepository(context);
                var profile = await repo.GetByIdAsync(15);
                profile.Rating = 10;

                repo.Update(profile);
                await context.SaveChangesAsync();

                var user = await repo.GetByIdAsync(15);

                Assert.AreEqual(15, user.Id);
                Assert.AreEqual(10, user.Rating);
            }
        }
    }
}
