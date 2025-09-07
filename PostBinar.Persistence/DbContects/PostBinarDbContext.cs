using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Domain.Categorys;
using PostBinar.Domain.Comments;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Notes;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.DbContects;

public sealed class PostBinarDbContext : DbContext, IPostBinarDbContext, IUnitOfWork
{
    public PostBinarDbContext(DbContextOptions<PostBinarDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();

    public DbSet<Project> Projects =>  Set<Project>();

    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    public DbSet<ProjectMembership> ProjectMemberships => Set<ProjectMembership>();

    public DbSet<Note> Notes => Set<Note>();

    public DbSet<FileStorage> FileStorages => Set<FileStorage>();

    public DbSet<Comment> Comments => Set<Comment>();

    public DbSet<NoteCategory> NoteCategories => Set<NoteCategory>();

    public DbSet<TaskCategory> TaskCategories => Set<TaskCategory>();

    public DbSet<Specialization> Specializations => Set<Specialization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostBinarDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
