using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Xunit;
using Microsoft.EntityFrameworkCore;
using PostBinar.Domain.Users;
using PostBinar.Persistence.DbContects;
using Microsoft.Extensions.Options;
using PostBinar.Infrastructure.Authorization;
using System.Threading;

public class UserDbSpamTests
{
    [Fact]
    public void InsertManyUsers_ParallelFor()
    {
        // ⚠️ Убедись, что строка подключения указывает на ТЕСТОВУЮ базу!
        var options = new DbContextOptionsBuilder<PostBinarDbContext>()
            .UseNpgsql("User ID = postbinar_user; Password = udrY8q7hSBCKFVjNBOJ7sCR65A81zG5a; Host = dpg-d39sctje5dus73bo2vcg-a.oregon-postgres.render.com; Port = 5432; Database = postbinar")
            .Options;

        var mockAuthOptions = Options.Create(new AuthorizationOptions { });

        var faker = new Faker<User>()
            .CustomInstantiator(f =>
                User.Create(
                    f.Name.FirstName(),
                    f.Name.LastName(),
                    f.Internet.Email(),
                    f.Internet.Password(),
                    f.Random.Int(1, 5)
                ).Value
            );

        int totalUsers = 10_00;
        int batchSize = 1_000;
        int parallelTasks = totalUsers / batchSize;

        Parallel.For(0, parallelTasks, i =>
        {
            using var db = new PostBinarDbContext(options, mockAuthOptions);

            var fakeUsers = faker.Generate(batchSize);
            db.Users.AddRange(fakeUsers);
            db.SaveChanges();
        });

        // проверяем результат
        using var checkDb = new PostBinarDbContext(options, mockAuthOptions);
        var count = checkDb.Users.Count();

        Console.WriteLine($"✅ Всего пользователей в базе: {count}");
        Assert.True(count >= totalUsers);
    }
}
