namespace PostBinar.Persistence.DbContects;

public class DbInitializer
{
    public static void Initialize(PostBinarDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
